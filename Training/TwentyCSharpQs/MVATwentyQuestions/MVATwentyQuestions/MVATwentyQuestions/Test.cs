using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVATwentyQuestions
{
    class Test
    {
        public string _name = "";

        public Test(string name)
        {
            _name = name;
        }

        public virtual void ConvertToUpper()
        {
            _name = _name.ToUpper();
        }

        //public abstract void ConvertToLower();
    }
}
