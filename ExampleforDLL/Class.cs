using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;

namespace ExampleforDLL
{
    public class Class : BaseClass
    {
        [Category]
        public  void Method1()
        {

        }

        public void Method2()
        {

        }
        [ContextStatic]
        public void Method3()
        {

        }
    }
}
