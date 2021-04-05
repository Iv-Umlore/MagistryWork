using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_work.HelpClasses
{
    static public class Operations
    {
        static List<string> GetOperations()
        {
            return new List<string> { "+", "-", "*", "/", "^", "sin", "cos" };
        }

        static Operation GetOperation(string operation)
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
    }
}
