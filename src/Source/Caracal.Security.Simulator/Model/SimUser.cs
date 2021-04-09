using System.Diagnostics.CodeAnalysis;
using Caracal.Security.Model;

namespace Caracal.Security.Simulator.Model {
    [ExcludeFromCodeCoverage]
    public record SimUser: User {
        public string FirstName { get; } 
        public string LastName { get; }
        
        public  string UserName { get; }
        public string Password { get; }
        public long TenantId { get; }
        public bool IsAuthorized { get; }

        public SimUser(
            long userId, 
            string firstName, 
            string lastName, 
            string username, 
            string password, 
            long tenantId, 
            bool isAuthorized = true) : base(userId) 
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Password = password;
            TenantId = tenantId;
            IsAuthorized = isAuthorized;
        }
    }
}