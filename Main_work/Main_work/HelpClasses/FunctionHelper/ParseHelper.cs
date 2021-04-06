using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_work.HelpClasses
{
    public struct TermInfo
    {
        public string termValue;
        public Operation operation;
    }

    public static class ParseHelper
    {
        public static List<string> GetTermsByBrackets(string term)
        {
            var tmp = term.Split('(').ToList();
            List<string> myTerms = new List<string>();

            foreach (var t in tmp)
            {
                if (term.Any(it => it == ')'))
                    myTerms.AddRange(GetBracketsTerms(t));
                else
                    myTerms.Add(t);
            }

            return myTerms;

        }

        public static List<TermInfo> GetTermsInfo(List<string> myTerms)
        {
            List<TermInfo> result = new List<TermInfo>();

            var tmpList = Operations.GetOperations();

            Operation PrevOperation = Operation.Unknown;

            for (int iter = 0; iter < myTerms.Count; iter++)
            {
                Operation Start = Operation.Unknown;
                Operation End = Operation.Unknown;

                foreach (var operation in tmpList)
                {
                    //todo Terms: Проверить правильность заполнения этой шляпы
                    if (myTerms[iter].StartsWith(operation))
                        Start = Operations.GetOperation(operation);

                    if (myTerms[iter].EndsWith(operation))
                        End = Operations.GetOperation(operation);
                }

                myTerms[iter] = ClearFromSEOperations(myTerms[iter], Start, End);

                if (PrevOperation != Operation.Unknown)
                    Start = PrevOperation;
                PrevOperation = End;

                var tmp = new TermInfo()
                {
                    termValue = myTerms[iter],
                    operation = Start
                };
                result.Add(tmp); 
            }

            return result;
        }

        private static List<string> GetBracketsTerms(string term)
        {
            int index = term.LastIndexOf(')');
            List<string> result = new List<string>();

            //todo Terms: Проверить не попадает ли скобка в итоговые листы
            result.Add(term.Substring(0, index - 1));
            result.Add(term.Substring(index + 1));

            return result;
        }
        
        private static string ClearFromSEOperations(string str, Operation Start, Operation End)
        {
            str = str.Substring(Operations.ConvertToString(Start).Length);
            str = str.Substring(0, str.Length - Operations.ConvertToString(End).Length);

            return str;
        }
    }
}
