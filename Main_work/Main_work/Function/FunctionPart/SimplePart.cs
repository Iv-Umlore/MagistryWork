using Main_work.HelpClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Main_work.Function.FunctionPart
{
    /// <summary>
    /// Класс представляющий собой один из множителей Term
    /// </summary>
    public class SimplePart
    {
        public bool IsFinal;
        public bool IsValue;
                
        public Operation MyOperation { get; set; } 

        public string Part { get; set; }

        private double Value = 0.0;

        private List<SimplePart> _parts;
        private List<string> _variables;


        public SimplePart(string str, Operation operation)
        {
            IsFinal = true;
            if (str.Contains("("))
            {
                IsFinal = false;
                List<string> parts = ParseHelper.GetTermsByBrackets(str);

                var pInfo = ParseHelper.GetTermsInfo(parts);

                _parts = new List<SimplePart>();
                foreach (var part in pInfo)
                    _parts.Add(new SimplePart(part.termValue, part.operation));
            }
            //todo SimplePart: Тесты на эту срань
            if (str.Contains("+") && IsFinal)
            {
                IsFinal = false;
                List<string> parts = str.Split('+').ToList();

                _parts = new List<SimplePart>();
                foreach (var part in parts)
                    _parts.Add(new SimplePart(part, Operation.Plus));
            }

            //todo SimplePart: Тесты на эту срань
            if (str.Contains("-") && IsFinal)
            {
                IsFinal = false;
                string[] sep = new string[1];
                sep[0] = "-";
                List<string> parts = str.Split(sep, StringSplitOptions.RemoveEmptyEntries).ToList();

                _parts = new List<SimplePart>();
                foreach (var part in parts)
                    _parts.Add(new SimplePart(part, Operation.Minus));
            }

            //todo SimplePart: Тесты на эту срань
            if (str.Contains("*") && IsFinal)
            {
                IsFinal = false;
                List<string> parts = str.Split('*').ToList();

                _parts = new List<SimplePart>();
                foreach (var part in parts)
                    _parts.Add(new SimplePart(part, Operation.Myltiplication));
            }

            //todo SimplePart: Тесты на эту срань
            if (str.Contains("/") && IsFinal)
            {
                IsFinal = false;
                List<string> parts = str.Split('/').ToList();

                _parts = new List<SimplePart>();
                foreach (var part in parts)
                    _parts.Add(new SimplePart(part, Operation.Div));
            }

            //todo SimplePart: Тесты на эту срань
            if (str.Contains("^") && IsFinal)
            {
                IsFinal = false;
                List<string> parts = str.Split('^').ToList();

                _parts = new List<SimplePart>();
                foreach (var part in parts)
                    _parts.Add(new SimplePart(part, Operation.Degree));
            }
            MyOperation = operation;

            if (IsFinal)
            {
                Part = str;
                if (str[0] >= '0' && str[0] <= '9')
                {
                    IsValue = true;
                    double.TryParse(str, out Value);
                }
                else
                {
                    IsValue = false;
                    _variables.Add(str);
                }

            }
            else
            {
                foreach (var part in _parts)
                {
                    var tmpList = part.GetVariables().Where(it => !_variables.Contains(it)).ToList();
                    _variables.AddRange(tmpList);
                }
            }
        }

        public List<string> GetVariables()
        {
            return _variables;
        }
    }
}
