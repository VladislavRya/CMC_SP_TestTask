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
        var [|x12345678910|] = 0;
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
        int [|x12345678910|];
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
        string [|x12345678910|], abcd, [|y12345678910|];
    }
}
");
        }


        [TestMethod]
        public async Task VariableInsideForeach_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        foreach (var x in a){}
    }
}
");
        }

        [TestMethod]
        public async Task VariableInsideFor_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        for (int i = 0; i < 10; ++i) { }
    }
}
");
        }

        [TestMethod]
        public async Task VariableInsideUsing_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        using (var x =  new StreamWriter(""xxxx"")) { }
    }
}
");
        }

        [TestMethod]
        public async Task VariableInsideForeach_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        foreach (var [|x12345678910|] in a){}
    }
}
");
        }

        [TestMethod]
        public async Task VariableInsideFor_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        for (int [|i12345678910|] = 0; i12345678910 < 10; ++i12345678910) { }
    }
}
");
        }

        [TestMethod]
        public async Task VariableInsideUsing_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
using System;
using System.IO;

class Program
{
    static void foo(int[] a)
    {
        using (var [|x12345678910|] =  new StreamWriter(""xxxx"")) { }
    }
}
");
        }
    }
}