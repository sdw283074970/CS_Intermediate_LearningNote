//Q: 什么是方法？
//A: 方法(Method)又称作函数，是类的组成部分，不能独立存在于namespace中。广义地说，方法包含了一系列可执行语句，用来实现某些目的，如一个算法。

//Q: 方法要怎么声明？
//A: 谈声明方法之前要了解方法的组成。一个类下的方法，由访问权限修饰符+返回类型+名称+参数+代码块{}组成，代码块中为具体的执行方法。
  //访问权限修饰符决定了谁拥有权限可以调用这个方法，今后章节详细讨论；
  //任何一个方法都应该有一个类型，可以是任何非静态类型。如果代码块中没有返回类型，仅仅是一些执行语句，那么用void代替，以为空返回类型；
  //名称+参数统称方法的签名(Signature)，换句话说，方法的签名由方法名称、参数数量、参数类型组成。其中参数数量不用明确写出。如：
  
  public class Example
  {
    public void Go(int x, int y)
    {
    }
  }
  
//以上的方法可解释为，这是一个访问权限为公共public、返回类型为空、名字为Go、参数数量为2、参数类型都是int的方法，其中 Go(int x, int y) 被称为签名。

//Q: 在同一个类中可以有很多名称相同但传入参数不同的签名么？
//A: 可以，这即是方法的重载(Overload)。如：

Public class Example
{
  public int Name(int n){}
  public int Name(string str){}
  public int Name(string str, int n){}
}

//当调用这个类中的方法的时候，如VS会提示有三种重载，传入对应的参数就能激活对应的方法。如：

static void Main(string[] args)
{
  var example = new Example();
  var v1 = example.Name(1);
  var v2 = example.Name("name");
  var v3 = example.Name("name", 1);
}

//另外我们也可以在同一个类中声明方法重载的时候调用之前的同名不同签名方法，这样可以省略很多重复性的代码，让代码更整洁也更易于debug。如：

Public class Example
{
  public int Name(int n)
  {
    return n;
  }
  public int Name(string str)
  {
    return Name(0);
  }
  public int Name(string str, int n)
  {
    return Name("Whats this");
  }
}//随便举的例子，内容略搅，但是一旦看懂就真的明白了。

//如果传入的参数为一组同类型的参数，这组参数可能会很长，意味着可能会在声明方法的时候重复写很多参数，如：

  public int Name(int n1, int n2, int n3, int n4, ...){}

//隐约觉得这么些重复性太高，应该有什么简化方法，即能闻到“代码异味(code smell)”。这种情况下可以使用数组参数代替以上参数，如：

  public int Name(int[] number){}
    
//在调用这个方法的时候，在参数传递的位置用上一组实例化的数组就好了，即：

  var example = new Example();
  var v = example.Name(new int[]{1, 2, 3, 4, ...});

//同样我们可以闻到一丝代码异味。每一次都要实例一个数组作为参数传递，既不效率，写得也复杂。我们可以进一步简化，通过params修饰符。

//Q: 什么是params修饰符？用在哪里？
//A: params修饰符是一种也是最有用的一种参数修饰符，同样的参数修饰符还有ref、out等，除了params修饰符以外其他的尽量不要用。稍后会做讨论。
  //参数修饰符用在声明方法的时候，放在在参数类型之前，一次只能使用一个参数修饰符。params修饰符只能用在当参数是一维数组的情况，如上例。使用了
  //params修饰符的效果为，在调用该方法的时候不必再实例化参数数组，直接输入对应类型的数组即可，如以下代码：

public class Example
{
  public int Name(params int[] numbers){}  
}

static void Main(string[] args)
{
  var example = new Example();
  var v1 = example.Name(new int[]{1, 2, 3, 4, ...});    //仍然可以当params修饰符不存在一样坚持实例化参数数组
  var v2 = example.Name(3, 5, 7);   //用了params修饰符后，可以直接省略参数数组的实例化过程
}
    
//Q: 什么是ref修饰符？这是干什么的？
//A: 当看到一段程序中有ref或者out修饰符的时候，等于说闻到了强烈的代码异味。举个例子，算一下最后a等于多少：

public class BadExample
{
  public void BadThing(int a)   //声明一个返回类型为空的方法
  {
    a += 2;
    Console.WriteLine(a);
  }
}

static void Main(string[] args)
{
  var badExample = new BadExample();
  var a = 1;
  badExample.BadThing(a);
  Console.WriteLine(a);
}

//以上程序会输出两个a的值。结果为3和1，为什么不一样？
//原因很简单。在主函数中的a是值类型，当一个方法引用a的时候，仅仅是将a的值拷贝到函数里面，形成一个本地值，仅在函数中起作用，函数中调用控制台方法，
  //输出自然是3。但是回到主函数，原来的a的位置并没有被影响。
//我们可以使用ref修饰符来改变将第二个输出也变成3。于params的用法类似，如下代码：

public class BadExample
{
  public void BadThing(ref int a)   //在参数类型之前放一个ref修饰符
  {
    a += 2;
    Console.WriteLine(a);
  }
}

static void Main(string[] args)
{
  var badExample = new BadExample();
  var a = 1;
  badExample.BadThing(ref a);   //同样得在调用的相同位置放置一个ref
  Console.WriteLine(a);   //这一次第二个输出也为3
}
    
//总而言之，ref的作用为，将传入参数进行标记引用，即将原变量于方法中的本地变量相连接，方法中的本地变量改变，原始变量也跟着改变。
//这实在非常蠢非常绕，极其容易把代码的值弄得一团糟，请极力避免使用ref修饰符。

//Q: 那什么是out修饰符？有什么用？
//A: out修饰符会返回一个值给方法调用者，即使这是一个void方法。无论在声明方法的时候有多少个参数，只要有out修饰符，所有的参数的最终值都会返回给
  //调用者，也就是说可以有一堆值会被返回。这同样是一个非常蠢的设计，如果一个方法需要返回多个值给调用者，更好的方法是将这些值封装在不同的有返回类型
  //的类中，在.NET中只有极其稀有的时候会用到out(通常是为了读懂某些方法的重载)，绝不要主动使用这个修饰符。
  //out与ref修饰符的使用格式完全一样，这里不做赘述。一个例子详解out:

public class BadExample
{
  public void BadThing(out int a, out int b)
  {
    a = 1;
    b = 2;
  }
}

static void Main(string[] args)
{
  int x;
  int y;
  badExample.BadThing(out x, out y);
  Console.WriteLine("{0},{1}", x, y);
}
    
//输出结果为1，2。与ref不同的时候，ref要求传入参数必须初始化，out则没这个要求。

//暂时想到这么多，最后更新2017/11/22
