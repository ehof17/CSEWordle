using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System;
using System.Linq;
namespace AuthenticationService
{
    public class Service1 : IService1
    {

        // ---------- Login attempts ----------
        private static readonly DataContractSerializer attemptSer =
            new DataContractSerializer(typeof(LoginAttemptList));
        private static LoginAttemptList LoadAttempts(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return new LoginAttemptList();
            using (var r = XmlReader.Create(new StringReader(xml)))
            {
                return (LoginAttemptList)attemptSer.ReadObject(r);
            }
        }
        private static string SaveAttempts(LoginAttemptList list)
        {
            using (var sw = new StringWriter())
            {
                using (var w = XmlWriter.Create(sw))
                {
                    attemptSer.WriteObject(w, list);
                    w.Flush();
                }

                return sw.ToString();
            }
        }

        // ---------- Users ----------
        private static readonly DataContractSerializer userSer =
            new DataContractSerializer(typeof(UserList));
        private static Dictionary<string, string> LoadUsers(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (var r = XmlReader.Create(new StringReader(xml)))
            {
                var list = (UserList)userSer.ReadObject(r);
                return list.ToDictionary(u => u.Username,
                                    u => u.HashedPassword,
                                    StringComparer.OrdinalIgnoreCase);
            }



        }
        private static string SaveUsers(Dictionary<string, string> users)
        {
            var list = new UserList(
                users.Select(kv => new User
                {
                    Username = kv.Key,
                    HashedPassword = kv.Value
                }));
            using (var sw = new StringWriter())
            {
                using (var w = XmlWriter.Create(sw))
                {
                    userSer.WriteObject(w, list);
                    w.Flush();
                }

                return sw.ToString();
            }
        }

        // rMax attempts: 5 in a 10 minute window
        private const int MAX_ATTEMPTS = 5;
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(10);

        public LoginResponse Login(string username,
                                   string hashedPassword,
                                   string usersXml,
                                   string attemptsXml)
        {
            // decipher data
            var users = LoadUsers(usersXml);
            var attempts = LoadAttempts(attemptsXml);

            // check if the request to log in is the first of the day
            bool firstToday = !attempts.Any(a => a.Username.Equals(username,
                                       StringComparison.OrdinalIgnoreCase) &&
                                       a.UtcTime.ToLocalTime().Date == DateTime.Now.Date);
            // Check if request is blocked -> too many incorrect attempts in the alloted window
            bool blocked = attempts
                .Count(a => a.Username.Equals(username,
                                              StringComparison.OrdinalIgnoreCase) &&
                            !a.Success &&
                            a.UtcTime >= DateTime.UtcNow - WINDOW) >= MAX_ATTEMPTS;

            if (blocked)
                return new LoginResponse
                {
                    Result = new AuthResult
                    {
                        Success = false,
                        Message = "Too many login attempts. Please try again later."
                    },
                    AttemptsXml = attemptsXml
                };

            bool validUser = users.TryGetValue(username, out var storedHash);
            bool successAuth = validUser && storedHash == hashedPassword;

            // record this attempt
            attempts.Add(new LoginAttempt
            {
                Username = username,
                Success = successAuth,
                UtcTime = DateTime.UtcNow
            });

            // prepare response
            var resp = new LoginResponse
            {
                Result = new AuthResult
                {
                    Success = successAuth,
                    Message = successAuth
                                ? "Login successful."
                                // If first log in attempt of the day, give additional details
                                : firstToday
                                    ? (validUser ? "Invalid password." : "Invalid username.")
                                    : "Invalid username or password."
                },
                AttemptsXml = SaveAttempts(attempts)
            };
            return resp;
        }

        public RegisterResponse Register(string username,
                                         string password,
                                         string usersXml)
        {
            var users = LoadUsers(usersXml);

            string err = ValidateUsername(username, users);
            if (!string.IsNullOrEmpty(err))
                return new RegisterResponse
                {
                    Result = new AuthResult { Success = false, Message = err },
                    UsersXml = usersXml
                };

            users[username] = password;

            return new RegisterResponse
            {
                Result = new AuthResult { Success = true, Message = "Registration successful." },
                UsersXml = SaveUsers(users)
            };
        }

        private static string ValidateUsername(string username,
                                               Dictionary<string, string> users)
        {
            if (users.ContainsKey(username)) return "User already exists.";
            if (username.Contains(":") || username.Contains(","))
                return "Username cannot include ':' or ','.";
            return string.Empty;
        }
    }
}