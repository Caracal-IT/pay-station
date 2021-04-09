using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Caracal.Security.Simulator.Model;

namespace Caracal.Security.Simulator.Stores {
    [ExcludeFromCodeCoverage]
    public class UserStore: List<SimUser> {
        private UserStore() { }

        public static UserStore Create() {
            return new UserStore {
                new (3, "Joe", "Soap", "JoeS", "Password1", 100),
                new (5, "Randy", "Kane", "RandyK", "MySecret", 207),
                new (9, "Jane", "Smith", "JaneS", "Password1234", 100, isAuthorized: false),
                new (17, "Kate", "Jones", "KateJ", "Nothing", 207)
            };
        }
    }
}