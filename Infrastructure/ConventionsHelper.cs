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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Pbdq.Core.Users.Domain.Docs;

namespace Pbdq.Tests.Conventions.Infrastructure
{
    static class ConventionsHelper
    {
        internal static IEnumerable<Assembly> GetAllAssemblies()
        {
            // TODO: return all the assemblies
            // e.g.
            //yield return typeof(Assembly1Type).Assembly;
            //yield return typeof(Assembly2Type).Assembly;
            //yield return typeof(Assembly3Type).Assembly;
            yield return typeof(ConventionsHelper).Assembly;
        }

        internal static IEnumerable<Assembly> GetTestAssemblies()
        {
            yield return typeof(ConventionsHelper).Assembly;
        }

        internal static IEnumerable<Type> GetInterfaces(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(asm => asm.GetTypes()).Where(t => t.IsInterface);
        }

        internal static IEnumerable<Type> GetClasses(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(asm => asm.GetTypes()).Where(t => t.IsClass);
        }

        internal static IEnumerable<string> GetCSharpSourcePaths()
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", ".."));
            var filePaths = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

            return filePaths;
        }

        internal static IEnumerable<string> GetProjectFiles()
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", ".."));
            var filePaths = Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories);

            return filePaths;
        }
    }
}