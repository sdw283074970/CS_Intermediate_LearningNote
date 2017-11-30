//Q: 什么是类的上下转型？
//A: 在使用继承构建类时会出现这么一个问题，当从一个基类派生出一个衍生类的时候，新的衍生类其实也是属于基类的一种，因为他们有共同属性、方法，即派生类的
  //实例也是可以被看作是基类的实例。如一辆宝马车是小汽车类的实例，同时也是车辆类的实例，即符合A属于B，B属于C，则A属于C的逻辑。
  //如何让编译器知道什么时候我们把派生类的实例当做基类的实例操作？这就需要上转型(Upcasting)操作。反之，如果一个基类的实例刚好拥有所有派生类的属性，
  //我们也可以把基类的实例当作派生类的实例操作，即下转型(Downcasting)操作。
  
//Q: 上下操作看起来就是反过来一样，那么具体实施方法也是类似相反的吗？
//A: 完全不一样。上转型操作看起来很好理解，实际操作起来也确实简单，但是下转型操作就要复杂一些，关键在于被转型的实例必须符合转型成派生类的条件，即其部分
  //构造或全部构造必须完全派生类的构造相同，多一点没关系，少一个属性都不行。否则强行转换会失败，甚至导致程序运行崩溃，即不安全。
  
//Q: 如何实现上下转型？
//A: 先来看简单的上转型。上转型没有什么特别的语法，就是普通的赋值语句，以shape类为例，衍生出circle类，那么Circle类的实例也可以是Shape类的实例，将
  //Circle类的实例circle转换成Shape类的实例方法如下：
  
public class Shape    //声明一个Shape类
{
    public int Width { get; set; }    //宽属性
    public int Hight { get; set; }    //长属性
}

public class Circle : Shape   //声明一个Circle类，继承自Shape
{
    public int X { get; set; }    //X坐标属性
    public int Y { get; set; }    //y坐标属性
}

//在主类中的主函数如以下代码

static void Main(string[] args)
{
  var circle = new Circle();    //实例化一个Circle类
  Shape shape = circle;   //将实例circle转换成Shape类只需要声明一个Shap类字段，再将circle赋值进去即可
  Shape shape2 = new Circle();    //或者直接将实例转型
}//需要注意的是第一种上转型shape和circle虽然名字不同但是指向同一个内存，即一个内存有了两个名字；而第二种内存引用名称唯一

  //下转型有两种方法，一种是强制转型(不安全)，另一种是使用关键字as，继续以上例子，此时shape属于Shape类，强制转回Circle类如下：

  Circle anotherCircle = (circle)shape;

  //在这里我们知道shape是由circle转过来的，当然也可以转回去。但是如果在不清楚的情况下使用强制转换，被抛出一个InvalidCastException异常。另一个安全的
  //方法为使用as关键词。继续以上面代码为例下转型：

static void Main(string[] args)
{
  var circle = new Circle();
  Shape shape = circle;
  Circle anotherCircle = shape as Circle;   //用as将shape转为Circle类并储存在anotherCircle中。我们知道shape本来由Circle转换而来，所以一定成功
  Rectangle rect = shape as Rectangle;    //shape与Rectangle类没有半毛钱关系，转换会失败。但用as不会抛出异常，取而代之rect将为null
  
  if(anotherCircle != null)   //如果anotherCircle不为空，则说明转换成功
    Console.WriteLine("Transfer successed.");   //这里写转换后的正常逻辑
  else    //否则转换失败，但不会抛出异常
    Console.WriteLine("Transfer failed.");    //给出转换失败后的命令，或不给不执行
  
  if(rect != null)    //显然rect应该为空
    Console.WriteLine("Transfer successed.");
  else    //转换失败，但不会抛出异常
    Console.WriteLine("Transfer failed.");
}//输出结果为第一个成功，第二个不成功。

//Q: 转型后有什么不一样？shape和circle是同一个东西吗？
//A: 无论是上下转型，实例将只能访问当前类中含有的属性、方法。如以上例子中的circle，当circle从Circle类上转为Shape后，circle将不能访问坐标属性。
  //特别重要的是，在转换后，我们可以看到shape和circle名字不一样，但是它们都指向同一个内存，改变一个即改变另一个。如以下代码可以证明：

  static void Main(string[] args)
  {
     var circle = new Circle();
    Shape shape = circle;

    circle.Width = 200;
    shape.Width = 100;
    Console.WriteLine(circle.Width);    //结果是100
  }

//Q: 什么时候会用到上转型和下转型？
//A: 上转型经常用到，比如一个典型的情况是在参数传递中，需要传入的类型参数可以传递该参数类型的派生类，然后这个派生类就自动上转型成了基类。一个具体的例子
  //为Stream类是所有Steam的基类，派生出了FileStream等其他类，当我们实例化StreamReader时，其中一种构造器要求传入一个Stream类型的参数，这个时候我们可
  //以直接传入Stream的派生类如MemoryStream，它会自动上转型为Stream类，如以下代码：
  
  static void Main(string[] args)
  {
    StreamReader r = new StreamReader(new MemoryStream());   //StreamReader的构造器要求传入Stream类型参数，MemoryStream派生自Stream，将自动转型
  }

  //下转型的一个典型应用是在事件中，在高级篇章有详细介绍事件。在WPF或Xamarin中，一个按钮类可以包含一个按下的事件，这个事件会发布并被后台的主函数订阅，
  //主函数可以获取发送者对象，即这个按钮，通过按钮对象来调用按钮的属性，如按钮上的字。但是，标准的事件先将发送者上转型成object，订阅者获取到的将是
  //object类，无法调用发送者的方法。这个时候我们就需要将object类的发送者实例转回原来的类型实例。以下为订阅者代码：

  private void Button_Clicked(object sender, EventArgs e)   //订阅事件协议
  {
    Button button = sender as Button;   //使用as关键词经行安全的下转型
    if(button != null)    //button不为空则说明转型成功
      lable.Text = button.Text;   //调用访问转回Button类型的sender中的属性
  }

//暂时想到这么多，最后跟新2017/11/30
