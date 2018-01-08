//Q: 什么是方法的覆写？
//A: 方法覆写(Method Overriding)是指更改从基类继承过来的方法的具体执行/实现。通俗的说，方法的覆写发生在衍生类中，其目的是完全更改继承自基类的某些方法
  //的具体内容。肯定会有人搞不清楚覆写(Overriding)和重载(Overloading)，这是两个几乎不相干的概念。之前在类的介绍中就介绍了重载，重载是指一个同名的方法
  //但传递进去的参数不同，或签名不同，而这里的覆写是指在衍生类中更改继承过来的方法的操作，两者完全不一样。
  
//Q: 什么情况下需要覆写？如何实现覆写？
//A: 基类的有些方法传到派生类后有时候会显得毫无意义。如一个基类Shape会有一个Draw()方法，其两个派生类Circle和Rectangle都会继承这一方法，然而画一个圆
  //和画一个矩形明显是不同的，并且基类中的"画一个图形"本身也无意义。这个时候就需要用到覆写，在Circle中Draw()方法画圆，在Rectangle中Draw()方法画矩形。
  
  //覆写通过关键词virtual(虚)和关键词override实现。在基类中的方法前加入virtual修饰符，即标明这是一个虚方法，可以被覆写，同时在派生类中的同名方法前加
  //override修饰符，标明这是一个覆写的基类方法。如以下代码所示：
  
public class Shape
{
  public virtual void Draw()    //Shape类中的draw()方法
  {
    //这里为draw的默认方法
  }
}

public class Circle : Shape   //Circle继承Shape类
{
  public override void Draw()   //覆写Draw()方法，注意必须同名
  {
    Console.WriteLine("Draw a circle.");    //模拟画圆的逻辑
  }
}

public class Rectangle : Shape    //Rectangle继承Shape类
{
  public override void Draw()   //在Rectangle类中覆写Draw方法
  {
    Console.WriteLine("Draw a rectangle");    //模拟画矩形的逻辑
  }
}

//在主函数的调用如下：

static void Main(string[] args)
{
  var circle = new Circle();
  var rectangle = new Rectangle();
  circle.Draw();
  rectangle.Draw();
}//可以看到两个实例都调用的都是继承自基类的方法，但是覆写过。同名，但执行结果不一样，会分别输出一个圆和矩形。

//Q: 为什么一定要覆写？为什么不能直接分别声明一个DrawCircle和DrawRectangle的方法？
//A: 以上仅仅介绍了覆写的概念，覆写与重新声明方法的最大不同在于其方法名不用改变，这意味着我们可以节省很多代码，还能提升可维护性。
  //节省代码和提升维护性可在以下体现，如果分别声明方法，代码如下：

static void Main(string[] args)
{
  var list = new List<Shape>();   //声明一个只装Shape类成员的列表
  var circle = new Circle();    //实例化一个Circle类，假设这里没有用覆写，而是声明了一个DrawCircle()的新方法
  var rectangle = new Rectangle();    //实例化一个Rectangle类，假设这里没有用覆写，而是声明了一个DrawRectangle()的新方法
  
  list.Add(circle);   //在列表中添加circle，因为Circle起源自Shape，这里自动上转型
  list.Add(rectangle);    //同上
  
  //由于Draw方法名字不同，我们没法通过遍历列表的代码来统一调用Draw()方法。为了实现遍历调用，我们必须建立一个枚举(enum)来标记每一种图形类的类别，
  //再通过switch语句实现遍历调用，这里枚举类型的代码省略
  
  foreach (var shape in list)
  {
    switch (shape.Type)   //shape.Type为枚举属性，设枚举列表名称为ShapeTyple
    {
      case ShapeTyple.Circle    //当列表中的枚举类型等于圆的时候
        DrawCircle();   //执行画圆的方法
        break;    //终止此次循环
      case ShapeType.Rectangle    //当列表中的枚举类型等于矩形的时候
        DrawCirlce();   //执行画矩形的方法
        break;    //终止此次循环
    }
  }
}

//可以看出，画一个列表的图非常复杂。如果我们要添加一个三角形类，坚持不用复写，要更改的地方就多了。首先，得在枚举类型列表中加一个三角形类，方便在switch
  //语句中使用；然后还要更改switch语句等等，可维护性极低。如果用了复写，那么代码可简化为以下：

static void Main(string[] args)
{
  var list = new List<Shape>();
  var circle = new Circle();    //实例化一个Circle类，这里复写了Draw()方法
  var rectangle = new Rectangle();    //实例化一个Rectangle类，这里复写了Draw()方法
  
  list.Add(circle);   //添加Circle实例并自动上转型
  list.Add(rectangle);    //同上

  foreach (var shape in list)
  {
    Draw();   //由于Draw方法名字相同，我们遍历的时候就不用switch语句，直接调用Draw()方法
  }
}

//可以看到与旧版本相比，明显提升了使用效率，节省了大量代码。同时，如果有新类的加入，通过复写，除了向列表添加成员的操作，再无其他，体现了搞维护性。

//在这里做一个小节，所谓多态性(Polymorphism)是指，当针对不同的对象执行同一个(同名)操作时，能使用这些对象对这一操作的自己的解释，从而产生不同的输出。
  //列表中的迭代就是体现多态性的典型例子。

//暂时想到这么多，最后更新2017/12/1
