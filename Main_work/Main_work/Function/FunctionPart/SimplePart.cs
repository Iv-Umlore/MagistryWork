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


        public SimplePart(string str, Operation operation = Operation.Plus)
        {
            Part = str;
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
            if (str.Contains("sin") && IsFinal)
            {
                IsFinal = false;
                List<string> parts = str.Split(new string[] { "sin" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                _parts = new List<SimplePart>();
                foreach (var part in parts)
                    _parts.Add(new SimplePart(part, Operation.Sin));
            }

            //todo SimplePart: Тесты на эту срань
            if (str.Contains("cos") && IsFinal)
            {
                IsFinal = false;
                List<string> parts = str.Split(new string[] { "cos" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                _parts = new List<SimplePart>();
                foreach (var part in parts)
                    _parts.Add(new SimplePart(part, Operation.Cos));
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

            _variables = new List<string>();
            if (IsFinal)
            {
                if (str[0] >= '0' && str[0] <= '9')
                {
                    IsValue = true;
                    str = str.Replace('.', ',');
                    if (!double.TryParse(str, out Value))
                        throw new Exception("Конструктор SimplePart:\nОшибка парсинга числа, убедитесь, что код работает верно и что это число: " + str);
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

        public double GetValue(Dictionary<string, double> variablesValue)
        {
            return 0.0;
        }

        public void FixValue(string variable, double value)
        {
            if (_variables.All(it => it != variable)) return;

            if (IsFinal && Part == variable)
            {
                IsValue = true;
                Value = value;
            }
            else
            {
                foreach (var part in _parts)
                    if (part._variables.Contains(variable))
                        part.FixValue(variable, value);
            }
        }
        
        public void CanselFix(string variable)
        {
            if (IsFinal && Part == variable)
                IsValue = false;
            else
                foreach (var part in _parts)
                    if (part._variables.Contains(variable))
                        part.CanselFix(variable);
        }
        
        public double GetValue()
        {
            if (IsFinal && !IsValue)
                throw new Exception("SimplePart.GetValue()\nError: Попытка получить значение из незаданного объекта: " + Part);

            if (IsFinal)
            {
                return Value;
            }

            double result = 0.0;
            foreach (var part in _parts)
            {
                Operation currOp = part.MyOperation;
                double val = part.GetValue();

                result = Operations.GetNewValue(result, val, currOp);

            }

            return result;
        }

    }
}
