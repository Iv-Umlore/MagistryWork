using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}