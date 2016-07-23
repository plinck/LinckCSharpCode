using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVATwentyQuestions
{
    class virtClassInherited : virtClass
    {
        public override void DisplayValue(string value)
        {
            Console.WriteLine(value.ToUpper());
        }
    }
}
