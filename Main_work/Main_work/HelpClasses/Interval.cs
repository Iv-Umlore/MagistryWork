using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_work.HelpClasses
{
    public class Interval
    {
        public Interval(double valueStart, double xCoord, double intervalSize = 0)
        {
            StartValue = valueStart;
            XCoordValue = xCoord;
            Size = intervalSize;
        }

        public double StartValue {get; set; }

        public double XCoordValue { get; set; }

        public double Size { get; set; }
    }
}
