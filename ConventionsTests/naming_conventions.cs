/*************************************************************************************************/
/*                                             pbdq                                              */
/*                                       CONVENTION TESTS                                        */
/*                                                                                               */
/* Some conventions tests to use in project as a part of automated testing system.               */
/* USE FOR FREE according to MIT LICENSE and leave this header and a namespace.                  */
/*                                                                                               */
/*                                                              tom(dot)maple(at)outlook(dot)com */
/*************************************************************************************************/

using System.Linq;
using Pbdq.Core.Users.UnitTests.Infrastructure;
using Should;
using Xunit;

namespace Pbdq.Core.Users.UnitTests.ConventionsTests
{
    public class naming_conventions
    {
        [Fact]
        public void each_interface_name_starts_with_capital_I()
        {
            var interfaces = ConventionsHelper.GetAllAssemblies().GetInterfaces();

            var invalidNameInterfaces = interfaces.Where(t => t.Name.StartsWith("I") == false);
            invalidNameInterfaces.ShouldBeEmpty();
        }
    }
}