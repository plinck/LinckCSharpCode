using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVATwentyQuestions
{
    class absClassInherited : absClass
    {
        override public void DisplayValue(string value)
        {
            Console.WriteLine(value);
        }
    }
}
