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
            List<string> result = new List<string>();

            int iter = 0;
            string tmpStr = "";
            while (iter < term.Length)
            {                
                if (term[iter] != '(') tmpStr += term[iter];
                else
                {
                    if (!string.IsNullOrEmpty(tmpStr))
                        result.Add(tmpStr);
                    tmpStr = "";

                    int bracketCount = 1;
                    
                    while(bracketCount > 0)
                    {
                        iter++;
                        tmpStr += term[iter];
                        if (term[iter] == '(') bracketCount++;
                        if (term[iter] == ')') bracketCount--;
                    }
                    tmpStr = tmpStr.Remove(tmpStr.Length - 1);
                    result.Add(tmpStr);
                    tmpStr = "";
                }
                iter++;
            }

            if (!string.IsNullOrEmpty(tmpStr))
                result.Add(tmpStr);

            return result;
        }

        public static List<TermInfo> GetTermsInfo(List<string> myTerms, bool IsFunction = false)
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
                
                if (PrevOperation != Operation.Unknown)
                    Start = PrevOperation;

                myTerms[iter] = ClearFromSEOperations(myTerms[iter], Start, End, IsFunction, Start == PrevOperation);
                
                if (Start == Operation.Unknown || (Start != Operation.Minus && Start != Operation.Plus) && IsFunction)
                    Start = Operation.Plus;

                PrevOperation = End;

                var tmp = new TermInfo()
                {
                    termValue = myTerms[iter],
                    operation = Start
                };
                if (!string.IsNullOrEmpty(tmp.termValue))
                    result.Add(tmp); 
            }

            return result;
        }
                
        static public List<string> CorrectSeparationByBracket(List<string> list, string operation)
        {
            for (int iter = 0; iter < list.Count; iter++)
                if (GetCharCount(list[iter], '(') > GetCharCount(list[iter], ')'))
                {
                    var tmp = list.LastOrDefault(it => GetCharCount(it, '(') < GetCharCount(it, ')'));
                    var index = list.IndexOf(tmp);

                    string fullStr = "";

                    for (int i = index; i >= iter; i--)
                        if (i > iter)
                            fullStr += list[iter + index - i] + operation;
                        else fullStr += list[iter + index - i];

                    list.RemoveRange(iter, index - iter + 1);

                    list.Insert(iter, fullStr);
                }

            return list;

        }

        static private int GetCharCount(string str, char chr)
        {
            int result = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i] == chr) result++;

            return result;
        }

        private static string ClearFromSEOperations(string str, Operation Start, Operation End, bool IsFunction, bool doubleOperations = false)
        {
            if (str.StartsWith(Operations.ConvertToString(Start)) && !doubleOperations && !IsFunction)
                str = str.Substring(Operations.ConvertToString(Start).Length);

            if (str.EndsWith(Operations.ConvertToString(End)))
                str = str.Substring(0, str.Length - Operations.ConvertToString(End).Length);

            return str;
        }
    }
}
