﻿using System.Threading.Tasks;
using CodeConverter.Tests.TestRunners;
using Xunit;

namespace CodeConverter.Tests.CSharp
{
    public class RootNamespaceTests : ConverterTestBase
    {
        public RootNamespaceTests() : base("TheRootNamespace")
        {
        }

        [Fact]
        public async Task RootNamespaceIsExplicit()
        {
            // Auto comment testing not used since it can't handle the added namespace
            await TestConversionVisualBasicToCSharpWithoutComments(@"Class AClassInRootNamespace
End Class

Namespace NestedWithinRoot
    Class AClassInANamespace
    End Class
End Namespace",
                @"namespace TheRootNamespace
{
    class AClassInRootNamespace
    {
    }

    namespace NestedWithinRoot
    {
        class AClassInANamespace
        {
        }
    }
}");
        }

        [Fact]
        public async Task RootNamespaceIsExplicitWithSingleClass()
        {
            // Auto comment testing not used since it can't handle the added namespace
            await TestConversionVisualBasicToCSharpWithoutComments(@"Class AClassInRootNamespace
End Class",
                @"namespace TheRootNamespace
{
    class AClassInRootNamespace
    {
    }
}");
        }

        [Fact]
        public async Task RootNamespaceIsExplicitForSingleNamespace()
        {
            // Auto comment testing not used since it can't handle the added namespace
            await TestConversionVisualBasicToCSharpWithoutComments(@"
Namespace NestedWithinRoot
    Class AClassInANamespace
    End Class
End Namespace",
                @"namespace TheRootNamespace
{
    namespace NestedWithinRoot
    {
        class AClassInANamespace
        {
        }
    }
}");
        }

        [Fact]
        public async Task RootNamespaceNotAppliedToFullyQualifiedNamespace()
        {
            // Auto comment testing not used since it can't handle the added namespace
            await TestConversionVisualBasicToCSharpWithoutComments(@"
Namespace Global.NotNestedWithinRoot
    Class AClassInANamespace
    End Class
End Namespace",
                @"namespace NotNestedWithinRoot
{
    class AClassInANamespace
    {
    }
}");
        }

        [Fact]
        public async Task RootNamespaceOnlyAppliedToUnqualifiedMembers()
        {
            // Auto comment testing not used since it can't handle the added namespace
            await TestConversionVisualBasicToCSharpWithoutComments(@"
Class AClassInRootNamespace
End Class

Namespace Global.NotNestedWithinRoot
    Class AClassInANamespace
    End Class
End Namespace

Namespace NestedWithinRoot
    Class AClassInANamespace
    End Class
End Namespace",
                @"namespace NotNestedWithinRoot
{
    class AClassInANamespace
    {
    }
}

namespace TheRootNamespace
{
    class AClassInRootNamespace
    {
    }

    namespace NestedWithinRoot
    {
        class AClassInANamespace
        {
        }
    }
}");
        }
    }
}