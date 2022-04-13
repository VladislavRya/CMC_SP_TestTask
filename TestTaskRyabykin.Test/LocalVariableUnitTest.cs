using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = TestTaskRyabykin.Test.CSharpAnalyzerVerifier<TestTaskRyabykin.LocalVariableLengthAnalyzer>;

namespace TestTaskRyabykin.Test
{
    [TestClass]
    public class LocalVariableUnitTest
    {

        [TestMethod]
        public async Task VariableIsAssigned_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    static void f()
    {
        int x = 0;
    }
}
");
        }

        [TestMethod]
        public async Task VariableIsNotAssigned_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    static void f()
    {
        string s;
    }
}
");
        }

        [TestMethod]
        public async Task VariableIsAssigned_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    static void f()
    {
        [|var x12345678910 = 0;|]
    }
}
");
        }

        [TestMethod]
        public async Task VariableIsNotAssigned_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    static void f()
    {
        [|int x12345678910;|]
    }
}
");
        }

        [TestMethod]
        public async Task NotVariable_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    public int x12345678910 = 10;
    static void f12345678910()
    {

    }
}
");
        }

        [TestMethod]
        public async Task VariableIsNotAssignedx2_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    static void f()
    {    
        [|[|string x12345678910, abcd, y12345678910;|]|]
    }
}
");
        }

        [TestMethod]
        public async Task DifferentVariables_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    static void f()
    {    
        [|string x12345678910, abcd;|]
        int x = 1;
        [|string s12345678910;|]
    }
}
");
        }

        [TestMethod]
        public async Task VariableInsideExpression_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        foreach (var x in a){}
        using (var x1 =  new StreamWriter(""xxxx""))
        {
        }
    }
}
");
        }

        [TestMethod]
        public async Task VariableInsideExpression_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        foreach ([|var x12345678910 in a|]){}
        using ([|var x10987654321 =  new StreamWriter(""xxxx"")|])
        {
        }
    }
}
");
        }
    }
}