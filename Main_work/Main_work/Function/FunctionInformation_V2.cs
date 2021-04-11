using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main_work.Function.FunctionPart;
using Main_work.HelpClasses;

namespace Main_work.Function
{
    public class FunctionInformation_V2
    {
        private List<Term> _terms;
        private List<string> _variables;

        public FunctionInformation_V2(string function)
        {
            _variables = new List<string>();
            function = function.Replace(" ", "");

            List<string> termList = new List<string>();
            int bracketCount = 0;
            string tmpStr = "";

            for (int iter = 0; iter < function.Length; iter++)
            {
                tmpStr += function[iter];
                if (function[iter] == '(') bracketCount++;
                if (function[iter] == ')') bracketCount--;
                if (bracketCount < 0) throw new Exception("Конструктор FunctionInformation_V2()\nОшибка расположения скобок. Число закрывающих скобок превысило число открывающих");

                if ((function[iter] == '+' || function[iter] == '-') && bracketCount == 0)
                {
                    termList.Add(tmpStr);
                    tmpStr = "";
                }
            }

            if (!string.IsNullOrEmpty(tmpStr))
                termList.Add(tmpStr);

            var termsInfo = ParseHelper.GetTermsInfo(termList, true);

            _terms = new List<Term>();
            foreach (var termI in termsInfo)
                _terms.Add(new Term(termI.termValue, termI.operation));
            
            foreach (Term term in _terms)
            {
                var tmpList = term.GetVariables().Where(it => !_variables.Contains(it)).ToList();
                if (tmpList.Count > 0)
                    _variables.AddRange(tmpList);
            }
        }

        public double GetValue(Dictionary<string, double> variablesValue)
        {
            double result = 0.0;            

            foreach (var term in _terms)
                result = Operations.GetNewValue(result, term.GetValue(variablesValue), term.TermOperation);
            return result;
        }
        
        public double GetValue()
        {
            double result = 0.0;
            foreach (Term iter in _terms)
                result = Operations.GetNewValue(result, iter.GetValue(), iter.TermOperation);

            return result;
        }

        public void FixValue(string variable, double value)
        {
            foreach (Term iter in _terms)
            {
                if (iter.GetVariables().Any(it => it == variable))
                    iter.FixValue(variable, value);
            }
        }

        public void CanselFix()
        {
            foreach (Term iter in _terms)
            {
                foreach (string variable in iter.GetVariables())
                    iter.CanselFix(variable);
            }
        }

        public void CanselFix(string variable)
        {
            foreach (Term iter in _terms)
            {
                if (iter.GetVariables().Any(it => it == variable))
                    iter.CanselFix(variable);
            }
        }

        public List<string> GetVariables()
        {
            return _variables;
        }

        /// <summary>
        /// Для тестирования
        /// </summary>
        /// <returns></returns>
        public int GetTermCount()
        {
            return _terms.Count;
        }
    }
}
