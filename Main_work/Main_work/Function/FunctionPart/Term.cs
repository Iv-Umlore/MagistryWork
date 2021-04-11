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
        Operation _termOperation;

        public Term(string functionPart, Operation termOperation)
        {
            _termOperation = termOperation;

            functionPart = functionPart.Replace(" ", "");

            if (functionPart.Contains("((") || functionPart.Contains("))"))
                throw new Exception("Функционал с двойными скобками не реализован на данный момент. Исключите из записи (( или ))");

            List<string> myTerms = ParseHelper.GetTermsByBrackets(functionPart);
            var parts = ParseHelper.GetTermsInfo(myTerms);

            _parts = new List<SimplePart>();
            foreach (var part in parts)
                _parts.Add(new SimplePart(part.termValue, part.operation));

            foreach (var part in _parts)
            {
                var tmpList = part.GetVariables().Where(it => !_variables.Contains(it)).ToList();
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
            return 0.0;
        }

        public double GetValue()
        {
            return 0.0;
        }

    }
}
