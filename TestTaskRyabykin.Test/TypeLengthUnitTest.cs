using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = TestTaskRyabykin.Test.CSharpAnalyzerVerifier<TestTaskRyabykin.TypeLengthAnalyzer>;

namespace TestTaskRyabykin.Test
{
    [TestClass]
    public class TypeLengthUnitTest
    {

        [TestMethod]
        public async Task TypeClass_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{

}
");
        }

        [TestMethod]
        public async Task TypeClass_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class [|C12345678910|]
{

}
");
        }

        [TestMethod]
        public async Task TypeStruct_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

struct Program
{
    int x;
}
");
        }

        [TestMethod]
        public async Task AnonymousType_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    public void f()
    {
        var F12345678910 = new { X = ""test"" };
    }
}
");
        }

        [TestMethod]
        public async Task TypeStruct_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

struct [|S12345678910|]
{
    string s;
}
");
        }

        [TestMethod]
        public async Task DifferentTypes_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"
class C
{
    public void f()
    {
        var F12345678910 = new { X = ""test"" };
    }
}

class [|C12345678910|]
{
    string s;
    public void f()
    {
        var F12345678910 = new { X = ""test"" };
    }
}

struct S
{

}

interface [|I12345678910|]
{

}


");
        }
    }
}