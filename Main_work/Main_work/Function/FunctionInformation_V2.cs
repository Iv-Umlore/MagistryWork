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
            List<string> termList = function.Split(new string[1] { "+" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            termList = ParseHelper.CorrectSeparationByBracket(termList, "+");

            foreach (var term in termList)
            {
                int index = termList.IndexOf(term);

                var tmpTermList = term.Split(new string[1] { "-" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                tmpTermList = ParseHelper.CorrectSeparationByBracket(tmpTermList, "-");

                termList.RemoveAt(index);
                termList.InsertRange(index, tmpTermList);
            }

            var termsInfo = ParseHelper.GetTermsInfo(termList);

            _terms = new List<Term>();
            foreach (var termI in termsInfo)
                _terms.Add(new Term(termI.termValue, termI.operation));
            
            foreach (Term term in _terms)
            {
                var tmpV = term.GetVariables();
                foreach (var iter in tmpV)
                    if (_variables.All(it => it != iter))
                        _variables.Add(iter);
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
    }
}
