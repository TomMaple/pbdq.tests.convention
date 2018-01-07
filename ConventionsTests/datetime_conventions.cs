/*************************************************************************************************/
/*                                             pbdq                                              */
/*                                       CONVENTION TESTS                                        */
/*                                                                                               */
/* Some conventions tests to use in project as a part of automated testing system.               */
/* USE FOR FREE according to MIT LICENSE and leave this header and a namespace.                  */
/*                                                                                               */
/*                                                              tom(dot)maple(at)outlook(dot)com */
/*************************************************************************************************/

using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pbdq.Core.Users.UnitTests.Infrastructure;
using Should;
using Xunit;

namespace Pbdq.Core.Users.UnitTests.ConventionsTests
{
    public class datetime_conventions
    {
        [Fact]
        public void DateTime_Now_is_never_used()
        {
            var sourcePaths = ConventionsHelper.GetCSharpSourcePaths()
                .Where(path => path.EndsWith("\\Time.cs") == false);

            foreach (var sourcePath in sourcePaths)
            {
                var source = File.ReadAllText(sourcePath);
                var tree = CSharpSyntaxTree.ParseText(source);
                var memberAccesses = tree.GetRoot().DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                var dateTimeNowInvocations = memberAccesses.Where(x =>
                    x.Expression.Parent.ToString().Equals("datetime.now", StringComparison.OrdinalIgnoreCase));

                dateTimeNowInvocations.ShouldBeEmpty();
            }
        }
    }
}