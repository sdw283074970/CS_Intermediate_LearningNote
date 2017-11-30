//Q: 当我们继承一个类的时候，构造器也会跟着被继承吗？
//A: 基类的构造器不被继承。

//Q: 那基类还需要重新声明构造器吗？要怎么保证继承的类正常实例化？
//A: 需要，但是需要依照情况先向基类构造器传递参数，保证基类能顺利实例化后再写衍生类自己的构造器。当衍生类的实例初始化时，基类的构造器总是被优先执行，
  //然后才是衍生类新声明的构造器，如代码所示：

public class Vehicle    //声明一个基类Vehicle
{
  public Vehicle()    //基类的无参数构造器
  {
    Console.Writeline("Vehicle is initailized.");    //模拟构造器逻辑
  }
}

public class Car : Vehicle    //声明一个Vehicle的衍生类Car
{
  public Car()    //声明衍生类的无参数构造器
  {
    Console.Writeline("Car is initailized");    //模拟构造器逻辑
  }
}

static void Main(string[] args)
{
  var car = new Car();    //直接实例化派生类
}//输出为，基类的构造器先执行，然后派生类的构造器再执行。

//Q: 如果基类的构造器要求有参数传入且没有无参数构造器，衍生类该如何实例化？衍生类的构造器该如何声明？
//A: 实例化衍生类的的过程是这样的，首先编译器实例化基类，再实例化衍生类。问题所说的情况，如果基类构造器必须要求参数而衍生类实例化的时候没有参数能传递给
  //基类，那就不能让基类实例化，进而无法实例化衍生类，编译器会报错。解决问题的方法即想方设法，通过衍生类的构造器，传递相应的参数给基类。改变一下基类：
  
public class Vehicle    //基类Vehicle
{
  public Vehicle(string plateNumber)    //基类不再有无参数构造器，有一个需求string的参数传递
  {
    Console.Writeline("Plate is {0}", plateNumber);    //模拟构造器逻辑
  }
}

//接着，使用base关键词向基类的构造器传递参数：

public class Car : Vehicle    //声明一个Vehicle的衍生类Car
{
  public Car(string brand)    //声明Car自己的构造器
      : base("SDW283074970")//向基类的构造器传递参数
  {
    Console.Writeline("Car is {0}", brand);    //模拟构造器逻辑
  }
}

//或者通过衍生类的构造器传递参数给基类的构造器：

public class Car : Vehicle    //声明一个Vehicle的衍生类Car
{
  public Car(string brand, string plateNumber)    //声明Car自己的构造器，加上基类构造器需要的参数
      : base(plateNumber)//向基类的构造器传递参数
  {
    Console.Writeline("Car is {0}", brand);    //模拟构造器逻辑
  }
}

//这样在主函数中就可以这样实例化衍生类：

static void Main(string[] args)
{
  var car = new Car("BWM", "SDW283074970");
}//输出结果为：车牌号SDW283074970，车的商标是BWM。

//暂时想到这么多，最后更新2017/11/29
