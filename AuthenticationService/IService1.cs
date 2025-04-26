using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuthenticationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        LoginResponse Login(string username, string password, string userXml, string logAttempts);
        [OperationContract]
        RegisterResponse  Register(string username, string password, string userXml);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class User
    {
        private string username;
        private string hashedPassword;

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string HashedPassword
        {
            get { return hashedPassword; }
            set { hashedPassword = value; }
        }
    }
    [CollectionDataContract(Name = "Users", ItemName = "User")]
    public class UserList : List<User>
    {
        public UserList()               
        {
        }
        public UserList(IEnumerable<User> collection) : base(collection)
        {
        }
    }
    [DataContract]
    public class LoginAttempt
    {
        [DataMember(Order = 0)] public DateTime UtcTime { get; set; }
        [DataMember(Order = 1)] public string Username { get; set; }
        [DataMember(Order = 2)] public bool Success { get; set; }
    }
    [CollectionDataContract(Name = "LoginAttempts",
                            ItemName = "Attempt")]
    public class LoginAttemptList : List<LoginAttempt> { }

    [DataContract]
    public class AuthResult
    {
        [DataMember(Order = 0)]
        public bool Success { get; set; }

        [DataMember(Order = 1)]
        public string Message { get; set; }

    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember] public AuthResult Result { get; set; }
        // Return the Attempts data for client
        [DataMember] public string AttemptsXml { get; set; }
    }

    [DataContract]
    public class RegisterResponse
    {
        [DataMember] public AuthResult Result { get; set; }
        [DataMember] public string UsersXml { get; set; }
    }
}
