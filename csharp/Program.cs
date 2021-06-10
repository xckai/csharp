using System;
using Castle.DynamicProxy;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {

            IFunc a;
            IFunc b;
            IFunc d;
            d=new D();
            d.Func();
            b = new C();
            b.Func();
 
        }
    }

}
