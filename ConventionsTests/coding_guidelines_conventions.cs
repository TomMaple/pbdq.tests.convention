/*************************************************************************************************/
/*                                             pbdq                                              */
/*                                       CONVENTION TESTS                                        */
/*                                                                                               */
/* Some conventions tests to use in project as a part of automated testing system.               */
/* USE FOR FREE according to MIT LICENSE and leave this header and a namespace.                  */
/*                                                                                               */
/*                                                              tom(dot)maple(at)outlook(dot)com */
/*************************************************************************************************/

using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pbdq.Core.Users.UnitTests.Infrastructure;
using Should;
using Xunit;

namespace Pbdq.Core.Users.UnitTests.ConventionsTests
{
    public class coding_guidelines_conventions
    {
        [Fact]
        public void usings_are_ordered_system_first_then_alphabetically()
        {
            var isFailed = false;
            var builder = new StringBuilder();

            foreach (var sourcePath in ConventionsHelper.GetCSharpSourcePaths())
            {
                var source = File.ReadAllText(sourcePath);
                var syntaxTree = CSharpSyntaxTree.ParseText(source);
                var usings = ((CompilationUnitSyntax) syntaxTree.GetRoot()).Usings;
                var sorted = usings.OrderBy(u => u, UsingComparer.Instance).ToList();

                if (usings.SequenceEqual(sorted) == false)
                {
                    isFailed = true;

                    builder.AppendLine($"Usings ordered incorrectly in '{sourcePath}'");
                    builder.Append("    (");
                    builder.Append(string.Join(", ", sorted));
                    builder.AppendLine(")");
                }
            }

            isFailed.ShouldBeFalse(builder.ToString());
        }

        [Fact]
        public void source_code_does_not_contain_tabs()
        {
            var isFailed = false;
            var builder = new StringBuilder();

            foreach (var sourcePath in ConventionsHelper.GetCSharpSourcePaths())
            {
                var source = File.ReadAllText(sourcePath);
                var syntaxTree = CSharpSyntaxTree.ParseText(source);

                var hasTabs =
                    syntaxTree.GetRoot()
                        .DescendantTrivia(descendIntoTrivia: true)
                        .Any(node => node.IsKind(SyntaxKind.WhitespaceTrivia) && node.ToString().IndexOf('\t') >= 0);

                if (hasTabs)
                {
                    isFailed = true;
                    builder.AppendLine($"Found tabs in file: '{sourcePath}'");
                }
            }

            isFailed.ShouldBeFalse(builder.ToString());
        }
    }
}