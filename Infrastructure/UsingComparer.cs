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
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Pbdq.Tests.Conventions.Infrastructure
{
    internal class UsingComparer : IComparer<UsingDirectiveSyntax>
    {
        public static UsingComparer Instance { get; } = new UsingComparer();

        public int Compare(UsingDirectiveSyntax using1, UsingDirectiveSyntax using2)
        {
            if (using1 == using2) return 0;

            return CompareIfNamespaces(using1, using2)
                   ?? CompareIfStatics(using1, using2)
                   ?? CompareIfAliases(using1, using2)
                   ?? CompareNames(using1, using2);
        }

        private static int? CompareIfNamespaces(UsingDirectiveSyntax using1, UsingDirectiveSyntax using2)
        {
            var using1IsNamespace = using1 != null
                                    && using1.Alias == null
                                    && !using1.StaticKeyword.IsKind(SyntaxKind.StaticKeyword);

            var using2IsNamespace = using2 != null
                                    && using2.Alias == null
                                    && !using2.StaticKeyword.IsKind(SyntaxKind.StaticKeyword);

            if (using1IsNamespace && using2IsNamespace == false)
                return -1;

            if (using1IsNamespace == false && using2IsNamespace)
                return 1;

            return null;
        }

        private static int? CompareIfStatics(UsingDirectiveSyntax using1, UsingDirectiveSyntax using2)
        {
            var using1IsStatic = using1 != null && using1.StaticKeyword.IsKind(SyntaxKind.StaticKeyword);
            var using2IsStatic = using2 != null && using2.StaticKeyword.IsKind(SyntaxKind.StaticKeyword);

            if (using1IsStatic && using2IsStatic == false)
                return -1;

            if (using1IsStatic == false && using2IsStatic)
                return 1;

            return null;
        }

        private static int? CompareIfAliases(UsingDirectiveSyntax using1, UsingDirectiveSyntax using2)
        {
            var using1IsAlias = using1 != null && using1.Alias != null;
            var using2IsAlias = using2 != null && using2.Alias != null;

            if (using1IsAlias && using2IsAlias == false)
                return -1;

            if (using1IsAlias == false && using2IsAlias)
                return -1;

            if (using1IsAlias)
            {
                var aliasComparisonResult =
                    StringComparer.CurrentCulture.Compare(using1.Alias.ToString(), using2.Alias.ToString());

                if (aliasComparisonResult != 0)
                    return aliasComparisonResult;
            }

            return null;
        }

        private static int CompareNames(UsingDirectiveSyntax using1, UsingDirectiveSyntax using2)
        {
            var nameText1 = using1.Name.ToString();
            var nameText2 = using2.Name.ToString();

            var using1IsSystemName = nameText1.StartsWith("System");
            var using2IsSystemName = nameText2.StartsWith("System");

            if (using1IsSystemName && using2IsSystemName == false)
                return -1;

            if (using1IsSystemName == false && using2IsSystemName)
                return 1;

            return StringComparer.CurrentCulture.Compare(nameText1, nameText2);
        }
    }
}