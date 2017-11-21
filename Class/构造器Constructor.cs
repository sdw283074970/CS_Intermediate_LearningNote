//Q: 什么是构造器？
//A: 构造器(Constructor)又称构造函数/方法，是类的组成之一。类由两个部分组成，属性/字段，以及函数/方法，构造函数总是为第一个函数/方法。

//Q: 构造函数有什么用？
//A: 构造函数是用来初始化该类的实例。个人更详细的解释为，构造函数通过获取实例化的同时传入的参数来对该实例的属性经行调整和初始化，即对该实例化对象初始化。
  //由于获取的参数可能是有限、统一或固定的，所以我们可以在构造函数中使用参数对类的实例对象属性经行调整。如以下代码：
  
puclic class Example
{
  public int Number1;
  public int Number2;
  
  public Example(int n)
  {
    Number1 = n * 2;
    Number2 = n * 2;
  }
}

static void Main(string[] args)
{
  var example = new Example(2);
  Console.WriteLine("{0}, {1}", example.Number1, example.Number2);
}

//以上例子简单清晰明了，实例Example类的时候只传入一个参数n，在构造器中我们可以通过分别调整n来对其两个属性Number1和Number2进行赋值。

//Q: 构造函数应该怎么声明？
//A: 为了让代码简洁清楚可维护，一般有一个共识，即在声明类的内部成员的时候，遵循先声明所有属性/变量，再构造器，再其他函数的顺序。且构造函数决定了实例化
  //对象时传入参数的类型、格式。声明构造函数时，通常为public访问修饰符+类的名字+参数结构，方法内部写具体初始化方法，如：

  public Example(int n)
  {
    Number1 = n * 2;
    Number2 = n * 2;
  }
  
//如果不需要传入参数，且不用构造器来初始化实例对象属性，那么可以省略构造函数，默认会有以下构造函数生成：
  
  public Example()
  {
  }
  
//当然构造器也能重载(overloading)，即同时声明多个传入参数结构不同的构造函数，在实例化使用构造器时，通过用户传入的参数结构来调用对应的构造方法。如：

public class Example
{
  public int Number1;
  public int Number2;

  public Example(int n) //声明第一种构造器
  {
    Number1 = n * 2;
    Number2 = n * 2;
  }
  
    public Example(int n1, int n2)  //声明第二种构造器
  {
    Number1 = n1 * 2;
    Number2 = n2 * 2;
  }
}

static void Main(string[] args)
{
  var example1 = new Example(2);  //符合第一种构造器参数结构，则调用第一种构造方法
  var example2 = new Example(3, 5);   //福尔第二种构造器参数结构，则调用第二种构造方法
  Console.WriteLine("{0}, {1}", example1.Number1, example1.Number2);
  Console.WriteLine("{0}, {1}", example2.Number1, example2.Number2);
}

//一个快速写构造函数的方法为，在Visual Studio中，输入ctor加两下TAB键自动构造该类的构造器。
