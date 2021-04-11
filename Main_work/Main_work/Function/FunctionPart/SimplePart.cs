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
        /// <summary>
        /// Показатель что знак, который делит выражение находится внутри скобок
        /// </summary>
        private bool IsSingleBracket;

        public bool IsFinal;
        public bool IsValue;
                
        public Operation MyOperation { get; set; } 

        public string Part { get; set; }

        private double Value = 0.0;

        private List<SimplePart> _parts;
        private List<string> _variables;


        public SimplePart(string str, Operation operation = Operation.Plus)
        {
            IsSingleBracket = false;

            str = str.Replace(" ", "");
            Part = str;
            IsFinal = true;
            {
                _parts = GetSimpleParts(str, true);

                if (str.Contains("(") && (IsFinal || IsSingleBracket))
                {
                    IsFinal = false;
                    List<string> parts = ParseHelper.GetTermsByBrackets(str);

                    var pInfo = ParseHelper.GetTermsInfo(parts);

                    _parts = new List<SimplePart>();
                    foreach (var part in pInfo)
                        _parts.Add(new SimplePart(part.termValue, part.operation));
                }

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

        //todo SimplePart: Тесты на эту срань
        private List<SimplePart> GetPartByOperations(string str, Operation operation)
        {
            var result = new List<SimplePart>();

            switch (operation)
            {
                case Operation.Plus:
                    {
                        List<string> parts = str.Split('+').ToList();
                        var count = parts.Count;

                        parts = CorrectTermsByBrackets(parts, "+");

                        if (count == parts.Count || !IsSingleBracket)
                        foreach (var part in parts)
                            result.Add(new SimplePart(part, operation));
                        IsSingleBracket = false;
                        break;
                    }
                case Operation.Minus:
                    {
                        bool startWithMinus = str.StartsWith("-");
                        List<string> parts = str.Split(new string[1] { "-" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var count = parts.Count;

                        parts = CorrectTermsByBrackets(parts, "-");

                        if (count == parts.Count || !IsSingleBracket)
                        {
                            for (int iter = 0; iter < parts.Count; iter++)
                                if (iter != 0 || startWithMinus)
                                    result.Add(new SimplePart(parts[iter], operation));
                                else result.Add(new SimplePart(parts[iter]));
                            
                        }
                        IsSingleBracket = false;
                        break;
                    }
                case Operation.Myltiplication:
                    {
                        List<string> parts = str.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var count = parts.Count;

                        parts = CorrectTermsByBrackets(parts, "*");

                        if (count == parts.Count || !IsSingleBracket)
                            for (int iter = 0; iter < parts.Count; iter++)
                                if (iter != 0)
                                    result.Add(new SimplePart(parts[iter], operation));
                                else result.Add(new SimplePart(parts[iter]));
                        IsSingleBracket = false;
                        break;
                    }
                case Operation.Div:
                    {
                        List<string> parts = str.Split(new string[1] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var count = parts.Count;

                        parts = CorrectTermsByBrackets(parts, "/");

                        if (count == parts.Count || !IsSingleBracket)
                            for (int iter = 0; iter < parts.Count; iter++)
                                if (iter != 0)
                                    result.Add(new SimplePart(parts[iter], operation));
                                else result.Add(new SimplePart(parts[iter]));
                        IsSingleBracket = false;
                        break;
                    }
                case Operation.Degree:
                    {
                        List<string> parts = str.Split(new string[1] { "^" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var count = parts.Count;

                        parts = CorrectTermsByBrackets(parts, "^");

                        if (count == parts.Count || !IsSingleBracket)
                            for (int iter = 0; iter < parts.Count; iter++)
                                if (iter != 0)
                                    result.Add(new SimplePart(parts[iter], operation));
                                else result.Add(new SimplePart(parts[iter]));
                        IsSingleBracket = false;
                        break;
                    }
                case Operation.Sin:
                    {
                        List<string> parts = str.Split(new string[1] { "sin" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var count = parts.Count;

                        parts = CorrectTermsByBrackets(parts, "sin");

                        if (count == parts.Count || !IsSingleBracket)
                            for (int iter = 0; iter < parts.Count; iter++)
                                result.Add(new SimplePart(parts[iter], operation));
                        IsSingleBracket = false;
                        break;
                    }
                case Operation.Cos:
                    {
                        List<string> parts = str.Split(new string[1] { "cos" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var count = parts.Count;

                        parts = CorrectTermsByBrackets(parts, "cos");

                        if (count == parts.Count || !IsSingleBracket)
                            for (int iter = 0; iter < parts.Count; iter++)
                                result.Add(new SimplePart(parts[iter], operation));
                        IsSingleBracket = false;
                        break;
                    }
                default:
                    throw new Exception("SimplePart.GetPartByOperations(). Bad operation");
            }

            return result;

        }

        private List<string> CorrectTermsByBrackets(List<string> list, string operation)
        {

            int length = list.Count;

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

            if (list.Count == 1)
                IsSingleBracket = true;

            if (length == list.Count)
                IsFinal = false;

            return list;
        }

        private int GetCharCount(string str, char chr)
        {
            int result = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i] == chr) result++;

            return result;
        }

        private List<SimplePart> GetSimpleParts(string str, bool IsConstructor = false)
        {
            var result = new List<SimplePart>();
            
            if (str.Contains("+") && IsFinal)
                result = GetPartByOperations(str, Operation.Plus);

            if (str.Contains("-") && IsFinal)
                result = GetPartByOperations(str, Operation.Minus);

            if (str.Contains("*") && IsFinal)
                result = GetPartByOperations(str, Operation.Myltiplication);

            if (str.Contains("/") && IsFinal)
                result = GetPartByOperations(str, Operation.Div);

            if (str.Contains("sin") && IsFinal)
                result = GetPartByOperations(str, Operation.Sin);

            if (str.Contains("cos") && IsFinal)
                result = GetPartByOperations(str, Operation.Cos);

            if (str.Contains("^") && IsFinal)
                result = GetPartByOperations(str, Operation.Degree);

            if (result.Count == 0 && !IsConstructor)
                result.Add(new SimplePart(str));

            return result;

        }

    }
}
