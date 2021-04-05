using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main_work.Function.FunctionPart;

namespace Main_work.Function
{
    public class FunctionInformation_V2
    {
        private List<Term> _terms;
        private List<string> _variables;

        public FunctionInformation_V2(List<Term> terms)
        {
            _terms = terms;
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
            return 0.0;
        }
        
        public double GetValue()
        {
            double result = 0.0;
            foreach (Term iter in _terms)
            {
                result += iter.GetValue();
            }

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
