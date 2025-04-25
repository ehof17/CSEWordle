using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Hosting;
using Grpc.Core;


namespace AuthenticationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        private static readonly object attemptLock = new object();
        string userXmlPath = HostingEnvironment.MapPath("~/App_Data/Users.xml");
        string attemptsXmlPath = HostingEnvironment.MapPath("~/App_Data/LoginAttempts.xml");
        // Max attempts 5 in a 10 minute window.
        private const int maxAttempts = 5;
        private static readonly TimeSpan attemptWindow = TimeSpan.FromMinutes(10);
        private static readonly DataContractSerializer attemptSerializer = new DataContractSerializer(typeof(LoginAttemptList));


        // Gets all the log attempts from the xml file provided
        private LoginAttemptList LoadAttempts(string attemptsFilePath)
        {
            if (!File.Exists(attemptsFilePath))
                return new LoginAttemptList();

            lock (attemptLock)
            {
                using (var s = File.OpenRead(attemptsFilePath))  
                {
                    return (LoginAttemptList)attemptSerializer.ReadObject(s);
                }
            }
        }

        // Saves all the log attempts to a file
        private void SaveAttempts(LoginAttemptList attempts, string attemptsFilePath)
        {
            lock (attemptLock)
            {
                using (var s = File.Create(attemptsFilePath))   
                {
                    attemptSerializer.WriteObject(s, attempts);
                }                                              
            }
        }

        // Gets if the username was not attempted to be logged into today
        private bool IsFirstAttemptOfDay(string username, string attemptsFilePath)
        {
            var attempts = LoadAttempts(attemptsFilePath);
            DateTime today = DateTime.Now.Date;   

            return !attempts.Any(a =>
                a.Username == username &&
                a.UtcTime.ToLocalTime().Date == today);
        }


        // Gets if a particular username is blocked from logging in
        // They can only have 5 incorrect attempts within a 10 minute window
        private bool IsBlocked(string username, string attemptsFilePath)
        {
            var attempts = LoadAttempts(attemptsFilePath);
            DateTime cutoff = DateTime.UtcNow - attemptWindow;

            int failures = attempts.Count(a =>
                a.UtcTime >= cutoff &&
                a.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                !a.Success);

            return failures >= maxAttempts;
        }

        // Gets all previous attempts, adds a new one, and saves the combined xml
        private void RecordAttempt(string username,bool success, string attemptsFilePath)
        {
            var attempts = LoadAttempts(attemptsFilePath);
            attempts.Add(new LoginAttempt
            {
                UtcTime = DateTime.UtcNow,
                Username = username,
                Success = success
            });
            SaveAttempts(attempts, attemptsFilePath);
        }

        public AuthResult Login(string username, string hashedPassword, string userXmlPath, string logAttemptsPath) 
         {

            if (IsBlocked(username,logAttemptsPath))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Too many login attempts. Please try again later."
                };
            }

            var users = LoadUsers(userXmlPath);
            bool firstAttemptToday = IsFirstAttemptOfDay(username, logAttemptsPath);


            // if username is invalid
            if (!users.ContainsKey(username))
            {
                RecordAttempt(username,  false, logAttemptsPath);
                // Give more details if its the first attempt of the day
                return new AuthResult
                {
                    Success = false,
                    Message = firstAttemptToday ? "Invalid username." : "Invalid username or password.",
                };
               
            }


            // if password is valid
            string storedHashed = users[username];
            if (storedHashed == hashedPassword)
            {
                RecordAttempt(username, true, logAttemptsPath);
                return new AuthResult
                {
                    Success = true,
                    Message = "Login successful."
                };
               
            }
            else
            {
                RecordAttempt(username, false, logAttemptsPath);
                return new AuthResult
                {
                    Success = false,
                    Message = firstAttemptToday ? "Invalid password." : "Invalid username or password.",
                };
            }
        }
        public AuthResult Register(string username, string password, string userXmlPath)
        {
            // get all the valid users
            var users = LoadUsers(userXmlPath);

            string validationError = ValidateUsername(username, users);
            if (!string.IsNullOrEmpty(validationError))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = validationError
                };
               
            }

      
            SaveUser(userXmlPath, username, password);
            return new AuthResult
            {
                Success = false,
                Message = "Registration successful."
            };
           
        }



        private Dictionary<string, string> LoadUsers(string xmlPath)
        {
            if (!File.Exists(xmlPath))
            {
                // Create empty file with root Users tag
                var emptyList = new UserList();
                using (var stream = File.Create(xmlPath))
                {
                    var serializer = new DataContractSerializer(typeof(UserList));
                    serializer.WriteObject(stream, emptyList);
                }

            }
            Dictionary<string, string> users = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(xmlPath))
            {
                var serializer = new DataContractSerializer(typeof(UserList));
                using (var stream = File.OpenRead(xmlPath))
                {
                    var userList = (UserList)serializer.ReadObject(stream);
                    foreach (var user in userList)
                    {
                        users[user.Username] = user.HashedPassword;
                    }
                }
            }
            return users;
        }
        private void SaveUser(string xmlPath, string username, string hashedPassword)
        {
            UserList users = new UserList();

            if (File.Exists(xmlPath))
            {
                var serializer = new DataContractSerializer(typeof(UserList));
                using (var stream = File.OpenRead(xmlPath))
                {
                    users = (UserList)serializer.ReadObject(stream);
                }
            }

            users.Add(new User { Username = username, HashedPassword = hashedPassword });

            using (var stream = File.Create(xmlPath))
            {
                var serializer = new DataContractSerializer(typeof(UserList));
                serializer.WriteObject(stream, users);
            }
        



      
        }
        // Helper function ensures that the username doesn't already exist or includes an invalid character
        private string ValidateUsername(string username, Dictionary<string, string> existingUsers)
        {
            if (existingUsers.ContainsKey(username))
            {
                return "User already exists.";
            }
            if (username.Contains(":") || username.Contains(","))
            {
                return "Username cannot include characters ':' or ',' ";
            }
            return string.Empty; // Valid username since no errors
        }
        
      

    }


}

