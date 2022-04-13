using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = TestTaskRyabykin.Test.CSharpAnalyzerVerifier<TestTaskRyabykin.FieldLengthAnalyzer>;

namespace TestTaskRyabykin.Test
{
    [TestClass]
    public class FieldLengthUnitTest
    {

        [TestMethod]
        public async Task FieldIsAssigned_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    int abc = 10;
}
");
        }

        [TestMethod]
        public async Task FieldIsNotAssigned_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    int abc;
}
");
        }

        [TestMethod]
        public async Task FieldIsAssigned_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    [|int x12345678910 = 10;|]
}
");
        }

        [TestMethod]
        public async Task FieldIsNotAssigned_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    [|string x12345678910;|]
}
");
        }


        [TestMethod]
        public async Task FieldIsAssignedx2_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    [|[|int x12345678910 = 10, abcd = 11, y12345678910 = 13;|]|]
}
");
        }

        [TestMethod]
        public async Task FieldIsNotAssignedx2_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    [|[|string x12345678910, abcd, y12345678910;|]|]
}
");
        }

        [TestMethod]
        public async Task DifferentFields_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    int abc = 10;
    [|string x12345678910 = ""Hello"";|]
    [|int y12345678910;|]
    [|[|int z12345678910 = 1, abcdef, w12345678910;|]|]
}
");
        }
    }
}