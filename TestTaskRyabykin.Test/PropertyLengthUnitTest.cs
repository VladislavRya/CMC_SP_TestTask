using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = TestTaskRyabykin.Test.CSharpAnalyzerVerifier<TestTaskRyabykin.PropertyLengthAnalyzer>;

namespace TestTaskRyabykin.Test
{
    [TestClass]
    public class PropertyLengthUnitTest
    {

        [TestMethod]
        public async Task PropertyGet_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    public string Abc { get; }
}
");
        }

        [TestMethod]
        public async Task PropertySet_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    private string abc;
    public string Abc { set { abc = value; } }
}
");
        }

        [TestMethod]
        public async Task PropertyGetSet_NoDiagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    private int abc;
    public int Abc { get { return abc; } set { abc = value; } }
}
");
        }

        [TestMethod]
        public async Task PropertyGet_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    [|public string X12345678910 { get; }|]
}
");
        }

        [TestMethod]
        public async Task PropertySet_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    private int x12345678910;
    [|public int X12345678910 { set { x12345678910 = value; } }|]
}
");
        }

        [TestMethod]
        public async Task PropertyGetSet_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    private string x12345678910;
    [|public string X12345678910 { 
        get { return x12345678910; }
        set { x12345678910 = value; }
    }|]
}
");
        }

        [TestMethod]
        public async Task DifferentProperties_Diagnostic()
        {
            await VerifyCS.VerifyAnalyzerAsync(@"

class Program
{
    private string x12345678910;
    private int abc = 10;
    [|public string X12345678910 { 
        get { return x12345678910; }
        set { x12345678910 = value; }
    }|]
    public int Abc { get; }
}
");
        }
    }
}