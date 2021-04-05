using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_work.Function.FunctionPart
{
    public class Term
    {
        private List<string> _variables;
        private List<SimplePart> _parts;

        public Term(string functionPart)
        {

        }

        public List<string> GetVariables()
        {
            return _variables;
        }
        
        public void FixValue(string variable, double value)
        {

        }

        public double GetValue()
        {
            return 0.0;
        }

        public void CanselFix(string variable)
        {

        }
    }
}
