//Q: 什么是方法的复写？
//A: 方法复写(Method Overriding)是指更改从基类继承过来的方法的具体执行/实现。通俗的说，方法的复写发生在衍生类中，其目的是完全更改继承自基类的某些方法
  //的具体内容。肯定会有人搞不清楚复写(Overriding)和重载(Overloading)，这是两个几乎不相干的概念。之前在类的介绍中就介绍了重载，重载是指一个同名的方法
  //但传递进去的参数不同，或签名不同，而这里的复写是指在衍生类中更改继承过来的方法的操作，两者完全不一样。
  
//Q: 什么情况下需要复写？如何实现复写？
//A: 基类的有些方法传到派生类后有时候会显得毫无意义。如一个基类Shape会有一个Draw()方法，其两个派生类Circle和Rectangle都会继承这一方法，然而画一个圆
  //和画一个矩形明显是不同的，并且基类中的"画一个图形"本身也无意义。这个时候就需要用到复写，在Circle中Draw()方法画圆，在Rectangle中Draw()方法画矩形。
  
  //复写通过关键词virtual(虚)和关键词override实现。在基类中的方法前加入virtual修饰符，即标明这是一个虚方法，可以被复写，同时在派生类中的同名方法前加
  //override修饰符，标明这是一个复写的基类方法。如以下代码所示：
  
public class Shape
{
  public virtual void Draw()    //Shape类中的draw()方法
  {
    //这里为draw的默认方法
  }
}

public class Circle : Shape   //Circle继承Shape类
{
  public override void Draw()   //复写Draw()方法，注意必须同名
  {
    Console.WriteLine("Draw a circle.");    //模拟画圆的逻辑
  }
}

public class Rectangle : Shape    //Rectangle继承Shape类
{
  public override void Draw()   //在Rectangle类中复写Draw方法
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
}//可以看到两个实例都调用的都是继承自基类的方法，但是复写过。同名，但执行结果不一样，会分别输出一个圆和矩形。




















