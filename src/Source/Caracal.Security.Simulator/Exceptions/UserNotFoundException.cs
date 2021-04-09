using System;
using System.Diagnostics.CodeAnalysis;

namespace Caracal.Security.Simulator.Exceptions {
    [ExcludeFromCodeCoverage]
    public class UserNotFoundException: UnauthorizedAccessException { }
}