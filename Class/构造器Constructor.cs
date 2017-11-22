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

//Q: 什么情况下我们需要用构造器初始化字段/属性/变量？
//A: 一个类可能有非常多的字段/属性/变量，并不一定是所有的都需要初始化。只有对于这个类的实例非常重要的字段、必须通过外部参数传递赋值的字段才需要放在
  //构造器初始化。比如一个类中的一个字段在初始化之前就已经确定它一定是某个值，我们就没有必要放在构造器里初始化，直接在声明的时候就赋值了。

//Q: 什么情况下必须初始化字段？
//A: 简而言之，任何类中的需要访问到的实例都应该在构造阶段就初始化，即在构造其中声明实例，否则直接访问可能会造成程序崩溃。如以下例子：

public class Example
{
  public List<NewExample> newExampleList;  //设NewExample为一个自定义类，newExample为包含一系列NewExample的列表
}

static void Main(string[] args)
{
  var example = new Example();
  var newExample = new NewExample();
  example.newExampleList.Add(newExample);   //程序会在此处崩溃
}

//以上程序会崩溃，因为在Example类的实例example中，newExampleList并没有被实例化。解决的办法除了在声明newExampleList的时候将其实例化以外，
  //也可以用构造器将其实例化。具体用哪个看个人喜好，但注意一定要统一标准。代码如下：

public class Example
{
  //public List<NewExample> newExampleLis = new List<NewExample>()t;    //在构造器初始化或这在里初始化(实例化)
  public List<NewExample> newExampleList;
  
  public Example()
  {
    newExampleList = new List<NewExample>();
  }
}

//Q: 当类中有多个构造函数，是否每个继承函数都需要初始化它们需要的字段？不同的构造函数之间相同的字段初始化方法相同会产生冲突吗？
//A: 不会冲突，系统在类实例化的时候匹配其构造器结构，找出对应的方法执行，必须有所有的必要字段初始化方法。但是这些构造函数中的方法有包含关系，
  //及构造器B中的部分方法是A中的全部方法，构造器C中的方法是B中全部方法等，我们可以用类似于继承的写法继承前构造其中的方法，免去重复步骤。如以下代码：

Public class Example
{
  public int Number1;
  public int Number2;
  public List<NewExample> newExampleList;
  
  public Example()  //声明默认构造器
  {
    newExampleList = new List<NewExample>();
  }
  
    public Example(int n) //声明第二种构造器
  {
    newExampleList = new List<NewExample>();  //包含第一种构造器方法
    Number1 = n * 2;
  }
  
    public Example(int n, int m)  //声明第三种构造器
  {
    newExampleList = new List<NewExample>();  //包含第一种构造器方法
    Number1 = n * 2;  //包含第二种构造器方法
    Number2 = m * 2;
  }
}

//在以上这种情况下，我们可以改写为：

Public class Example
{
  public int Number1;
  public int Number2;
  public List<NewExample> newExampleList;
  
  public Example()  //声明默认构造器
  {
    newExampleList = new List<NewExample>();
  }
  
    public Example(int n) //声明第二种构造器
      : this()  //继承第一种无参数构造器方法
  {
    Number1 = n * 2;
  }
  
    public Example(int n, int m)  //声明第三种构造器
      : this(n) //继承第二种有一个参数的构造器方法，自然也包括了五三出构造器方法
  {
    Number2 = m * 2;
  }
}

//但是注意，并不推荐这种连环继承，容易出漏子，牵一发则动全身。最多考虑继承默认的无参数构造器就好了。

//暂时想到这么多，最后更新2017/11/21
