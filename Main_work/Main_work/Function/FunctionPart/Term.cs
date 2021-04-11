using Main_work.HelpClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Main_work.Function.FunctionPart
{
    public class Term
    {
        private List<string> firstSeparator;

        private List<string> _variables;
        private List<SimplePart> _parts;
        public Operation TermOperation { get; private set; }

        public Term(string functionPart, Operation termOperation)
        {
            TermOperation = termOperation;
            _variables = new List<string>();
            functionPart = functionPart.Replace(" ", "");
            
            List<string> myTerms = ParseHelper.GetTermsByBrackets(functionPart);
            var parts = (myTerms.Count > 1) ? 
                ParseHelper.GetTermsInfo(myTerms) : 
                new List<TermInfo> { new TermInfo { termValue = myTerms[0], operation = Operation.Plus } };

            _parts = new List<SimplePart>();
            foreach (var part in parts)
                _parts.Add(new SimplePart(part.termValue, part.operation));

            foreach (var part in _parts)
            {
                var tmpList = part.GetVariables().Where(it => !_variables.Contains(it)).ToList();
                if (tmpList.Count > 0)
                    _variables.AddRange(tmpList);
            }

        }

        public List<string> GetVariables()
        {
            return _variables;
        }
        
        public void FixValue(string variable, double value)
        {
            foreach (var part in _parts)
                if (part.GetVariables().Contains(variable))
                    part.FixValue(variable, value);
        }

        public void CanselFix(string variable)
        {
            foreach (var part in _parts)
                if (part.GetVariables().Contains(variable))
                    part.CanselFix(variable);
        }
        
        public double GetValue(Dictionary<string, double> variablesValue)
        {
            double result = 0.0;

            foreach(var part in _parts)
            {
                foreach (var pair in variablesValue)
                    part.FixValue(pair.Key, pair.Value);
                result = Operations.GetNewValue(result, part.GetValue(), part.MyOperation);
                foreach (var pair in variablesValue)
                    part.CanselFix(pair.Key);
            }

            return result;
        }

        public double GetValue()
        {
            double result = 0.0;
            foreach (var part in _parts)
                result = Operations.GetNewValue(result, part.GetValue(), part.MyOperation);

            return result;
        }

    }
}
