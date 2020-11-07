using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Main_work.HelpClasses
{
    public class FunctionInformation
    {
        private int _sinFirstParameter;
        private int _sinSecondParameter;
        private int _cosFirstParameter;
        private int _cosSecondParameter;

        public FunctionInformation()
        {
            _sinFirstParameter = 1;
            _sinSecondParameter = 1;
            _cosFirstParameter = 1;
            _cosSecondParameter = 1;
        }

        public bool SetPrivateParameter(string sinFirst, string sinSecond, string cosFirst, string cosSecond)
        {
            int fs = 0, ss = 0, fc = 0, sc = 0; 
            bool flag = true;
            flag = int.TryParse(sinFirst, out fs) &&
                int.TryParse(sinSecond, out ss) &&
                int.TryParse(cosFirst, out fc) &&
                int.TryParse(cosSecond, out sc);

            if (!flag)
                return false;

            _sinFirstParameter = fs;
            _sinSecondParameter = ss;
            _cosFirstParameter = fc;
            _cosSecondParameter = sc;
            return true;
        }

        public double GetValueByXCoord(double x)
        {
            return _sinFirstParameter * Math.Sin(x * _sinSecondParameter) + _cosFirstParameter * Math.Cos(x * _cosSecondParameter);
        }

        private void SetFunction()
        {

        }


    }
}
