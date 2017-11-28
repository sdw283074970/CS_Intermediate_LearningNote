//Q: 什么是继承？
//A: 继承(Inheritance)是一种两个类之间的关系，通俗地讲继承是指一个类拥有另一个类的全部代码，只要标明它们为继承关系，则不用将继承的代码再写一次。换句话
  //说，继承为一个类基于另一个发展而来，会随着被继承的类变化而变化。被继承的类成为基类(Base)或父类(Parent)，继承的类称为衍生类(Derived)或子类(Child)。
  //如，人类继承哺乳动物类，哺乳动物类为基类，人类为子类。

//Q: 为什么要用继承？有哪些好处？
//A: 最大的好处在于代码复用(re-use)。如果严格按照一类一类继承下来，我们在新建一个类的时候就不用重新写那些重复的代码。另一个强大的好处是继承可以提供
  //多态性的行为，这个以后讨论，请查阅本套笔记目录。本片主要阐述如何通过继承代码复用。举个例子，在PPT中，我们可以进行改变对象如文字框、图形、图片的大小、
  //复制等操作，不考虑继承的情况下，在定义文字框、图形、图片等具体类的时候，我们需要分别在这些中写更改大小和复制的方法。有了继承，我们可以让这些类从其他
  //类中继承这一改变大小、复制的方法，即在写小类之前可以先写一个众多小类都具备的方法的大类，然后让这些小类都继承这个大类的方法。

//Q: 如何使用继承？
//A: 继续以上面PPT为例，先建立一个基类PresentationObject，好让其他类继承，实现如以下代码：
  
public class PresentationObject
{
  public int Width { get; set; }    //这个类定义一个宽属性
  public int Height { get; set;}    //定义一个长属性
  
  public void Copy    //这个类有一个复制的方法
  {
    Console.WriteLine("Copied");    //复制逻辑，用控制台代替表示
  }
  
  public void Change    //有一个改变大小的方法
  {
    Console.WriteLine("Changed");   //改变大小逻辑，用控制台表示
  }
}

//再建立一个文字框类，在类名后加冒号再加被继承类的名称则为继承

public class TextBox : PresentationObject   //继承后自动拥有PresentationObject类中的所有字段、属性、构造器、方法
{
  public int FontSize { get; set; }   //TextBox特有的属性
  public string FontName { get; set; }  ////TextBox特有的属性

  public void Edit(string str)    //再加上一个TextBox类特有的方法
  {
    Console.WriteLine("Words changed");   //改变文字逻辑，用控制台表示
  }
}

//再建立一个图形类Circle，同样继承自PresentationObject类

public class Circle : PresentationObject   //继承后自动拥有PresentationObject类中的所有字段、属性、构造器、方法
{
  public Color color { get; set; }    //图形类特有的属性

  public void ColorChange(Color c)    //为图形类加上一个改变颜色的特有的方法
  {
    Console.WriteLine("Words changed");   //改变颜色逻辑，用控制台表示
  }
}

//接下来在主函数观察结果

static void Main(string[] args)
{
  var textBox = new TextBox();
  var circle = new Circle();
  var red = new Color("red");
  
  textBox.Copy();
  textBox.Change();
  textBox.Edit("sdw");
  
  circle.Copy();
  circle.Change();
  circle.ColorChange(red); 
}

//我们可以看出，textBox和circle可以访问所有PresentationObject的方法，因为它们都是起源自PresentationObject类的，同样它们也能访问自己特有的方法，
  //而其他兄弟类则不受影响。明显感觉省了很多代码，这就是继承的强大之处。

//暂时想到这么多，最后更新2017/11/28
