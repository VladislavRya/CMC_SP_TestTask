using System;

namespace ManualAnalyzersCheck
{
    class VeryLongClassName
    {
        int veryLongInt;
        int shortInt;
        int veryLongInt2, shortInt2, veryLongInt3;
        string veryLongString = "Hello", shortStr;

        public string VeryLongString
        {
            get { return veryLongString; }
            set { veryLongString = value; }
        }
            
        public int ShortInt
        {
            get { return shortInt; }
        }

        public void Func() { }
        public void VeryLongFunc() { }

    }
    
    class ShortClass
    {
        string veryLongString;

        public string VeryLongString
        {
            get { return veryLongString; }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int veryLongMainInt;
            int shortInt;
            int veryLongMainInt2, shortInt2, veryLongMainInt3, shortInt3;
        }
    }
}
