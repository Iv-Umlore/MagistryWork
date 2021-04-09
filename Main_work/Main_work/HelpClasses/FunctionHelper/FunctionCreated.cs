using Main_work.Function;
using Main_work.Function.FunctionPart;
using System.Collections.Generic;

namespace Main_work.HelpClasses
{
    static public class FunctionCreated
    {
        static public FunctionInformation_V2 GetFunction(string func)
        {
            // Нельзя парсить только по +- , тк. + и - могут быть внутри скобок
            return new FunctionInformation_V2(new List<Term>());
        }
    }
}
