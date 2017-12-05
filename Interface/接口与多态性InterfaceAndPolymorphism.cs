//Q: 什么是多态性？
//A: 多态性(Polymorphism)在之前专门开了一章详解，再次总结一下。当一个方法被执行时，能够基于运行时的实际情况选择在不同对象中的同名方法执行并产生不同的输出，
  //我们就称种现象为多态性。如之前的例子：
  
static void Main(string[] args)
{
  var list = new List<Shape>();
  var circle = new Circle();    //实例化一个Circle类，这里复写了Draw()方法
  var rectangle = new Rectangle();    //实例化一个Rectangle类，这里复写了Draw()方法
  
  list.Add(circle);   //添加Circle实例并自动上转型
  list.Add(rectangle);    //同上

  foreach (var shape in list)
  {
    Draw();   //通过迭代列表中的对象调用他们同名方法Draw()，虽然同名，但是却是两个不同的方法，一个在circle中一个在rectangle中，即体现了多态性
  }
}

//Q: 接口如何帮助实现多态性？
//A: 多种类可以拥有同种接口，即这些类都有同名的方法。继承中的复写产生的同名方法可以提现多态性，接口的也可以，且原理相同。我们可以把拥有相同接口的类的
  //实例都装进同一个类中。如IList<ILogger>列表中可以装入所有有ILogger接口的实例，无论是ConsoleLogger类还是FileLogger还是其他类都可以。
