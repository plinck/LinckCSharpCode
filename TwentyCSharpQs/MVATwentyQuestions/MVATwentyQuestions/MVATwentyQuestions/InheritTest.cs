using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVATwentyQuestions
{
    class InheritTest : Test
    {
        //base("NewName");

        public InheritTest(string name) : base(ModifyBase(name))
        {
           
        }

        private static string ModifyBase(string newName)
        {
            return newName.ToUpper();
        }

        //public override void ConvertToLower()
        //{
        //    _name = _name.ToLower();
        //}
    }
}
