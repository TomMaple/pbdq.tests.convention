using System.Linq;
using Pbdq.Core.Users.UnitTests.Infrastructure;
using Should;
using Xunit;

namespace Pbdq.Core.Users.UnitTests.ConventionsTests
{
    public class runnability_conventions
    {
        [Fact]
        public void each_interface_has_at_least_one_implementation()
        {
            var interfaces = ConventionsHelper.GetAllAssemblies().GetInterfaces();

            var concreteTypes = ConventionsHelper.GetAllAssemblies().GetClasses().Where(x => x.IsAbstract == false)
                .ToList();

            foreach (var @interface in interfaces)
                concreteTypes.Any(x => @interface.IsAssignableFrom(x)).ShouldBeTrue();
        }
    }
}