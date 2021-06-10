using System;
using System.Collections.Generic;
using System.Text;

namespace csharp
{
    public interface IPerson
    {
        void Say();
        void Hi();
    }
   public class Person: IPerson
    {
        public virtual void  Say()
        {
            Console.WriteLine("say");
        }

        public void Hi()
        {
            Console.WriteLine("hi");
        }
    }
}
