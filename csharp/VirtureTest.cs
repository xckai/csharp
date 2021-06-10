using System;
using System.Collections.Generic;
using System.Text;

namespace csharp
{
    interface IFunc
    {
        public void Func();
    }
    class A: IFunc
    {
        public virtual void Func() // 注意virtual,表明这是一个虚拟函数
        {
            Console.WriteLine("Func In A");
        }
    }

    class B : A // 注意B是从A类继承,所以A是父类,B是子类
    {
        public override void Func() // 注意override ,表明重新实现了虚函数
        {
            Console.WriteLine("Func In B");
        }
    }

    class C : B // 注意C是从A类继承,所以B是父类,C是子类
    {
    }

    class D : A ,IFunc // 注意B是从A类继承,所以A是父类,D是子类
    {
        public new void Func() // 注意new ，表明覆盖父类里的同名类，而不是重新实现
        {
            Console.WriteLine("Func In D");
        }
    }
}
