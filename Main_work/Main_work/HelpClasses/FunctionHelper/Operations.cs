using System;
using System.Collections.Generic;


namespace Main_work.HelpClasses
{
    static public class Operations
    {
        static public List<string> GetOperations()
        {
            return new List<string> { "+", "-", "*", "/", "^", "sin", "cos" };
        }

        static public Operation GetOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                    return Operation.Plus;
                case "-":
                    return Operation.Minus;
                case "*":
                    return Operation.Myltiplication;
                case "/":
                    return Operation.Div;
                case "^":
                    return Operation.Degree;
                case "sin":
                    return Operation.Sin;
                case "cos":
                    return Operation.Cos;
                default:
                    return Operation.Unknown;
            }
        }

        static public string ConvertToString(Operation operation)
        {
            switch (operation)
            {
                case Operation.Plus:
                    return "+";
                case Operation.Minus:
                    return "-";
                case Operation.Myltiplication:
                    return "*";
                case Operation.Div:
                    return "/";
                case Operation.Degree:
                    return "^";
                case Operation.Sin:
                    return "sin";
                case Operation.Cos:
                    return "cos";
                default:
                    return "";
            }

        }

        static public double GetNewValue(double lastValue, double changeValue, Operation op)
        {
            switch (op)
            {
                case Operation.Plus:
                    return lastValue + changeValue;
                case Operation.Minus:
                    return lastValue - changeValue;
                case Operation.Myltiplication:
                    return lastValue * changeValue;
                case Operation.Div:
                    return lastValue / changeValue;
                case Operation.Degree:
                    return Math.Pow(lastValue, changeValue);
                case Operation.Sin:
                    return Math.Sin(changeValue);
                case Operation.Cos:
                    return Math.Cos(changeValue);
                default:
                    throw new Exception("Operations.GetNewValue(d,d,O): Неизвестная операция");
            }
        }

    }
}