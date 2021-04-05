using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_work.HelpClasses
{
    public class Interval
    {
        public Interval(double valueStart, double xCoord, double size = 0, double potential = 0)
        {
            StartValue = valueStart;
            XCoordValue = xCoord;
            Size = size;
            Potential = potential;
            Characteristic = size;
        }

        public double StartValue {get; set; }

        public double XCoordValue { get; set; }

        public double Size { get; set; }

        public double Characteristic { get; set; }

        public double Potential { get; set; }

        public void CalculatePotential(double finishValue, double finishCoord)
        {
            Potential = Math.Abs((finishValue - StartValue)) / (finishCoord - XCoordValue);
        }

    }
}
