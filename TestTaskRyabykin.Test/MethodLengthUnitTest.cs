using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = TestTaskRyabykin.Test.CSharpAnalyzerVerifier<TestTaskRyabykin.MethodLengthAnalyzer>;

namespace TestTaskRyabykin.Test
{
    [TestClass]
    public class MethodLengthUnitTest
    {

        [TestMethod]
        public async Task Method_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    public void f() {}
}
");
        }

        [TestMethod]
        public async Task Method_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    [|public int f12345678910()
    {
        return 123;
    }|]
}
");
        }

        [TestMethod]
        public async Task DifferentMethods_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    [|public int f12345678910()
    {
        return 123;
    }|]

    [|private void g12345678910() {}|]

    public int f()
    {
    return 123;
    }
}
");
        }
    }
}