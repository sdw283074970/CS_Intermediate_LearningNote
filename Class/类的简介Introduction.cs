//Q: 什么是类？
//A: 类是一个宽泛的抽象概念，是对一些具有共同特征的对象的抽象。如猪，牛，羊都是胎生动物，我们把它们划为哺乳动物类。在程序设计中，类也具有相似的概念，
  //被用来实现信息的封装，类主要封装两种信息，一种是数据(data，由字段表示)，另一种是行为(behavior，由函数/方法表示)。封装信息的目的是为了实例化成对象。
  
//Q: 什么是对象？
//A: 对象是类的具体，反过来，类也是对象的抽象。对象我们看得见摸得着，每个实例化的对象都保存有独立的信息。如一群家猪是猪这一类的实例，也是具体的对象，
  //每头猪大小不同，重量不同，长得也不一样，但都是猪。
  
//Q: 如何新建类？
//A: 先说C#中的程序结构。最上一层为命名空间(namespace)，可以理解为类的大类；中间一层为类；最里一层为方法/函数、字段属性。如以下代码表示：

namespace ThisIsNamespace
{
  public class ThisIsClass
  {
    public string ThisIsProperty;
    
    public string ThisIsMethod()
    {
      return “This is return type.”;
    }
  }
}

//声明一个类，仿照上例遵循以下格式即可：

AccessModifier class ClassName
{
}

//Q: 命名上有没有格式？
//A: 作为合格的程序员，要遵循代码干净可读的原则。对于类名、字段名、方法名有一个广泛的共识，即PascalCase 和 camelCase。
  //PascalCase命名方式为所有首写字母大写，中间不留空格，如PascalCase;
  //camelCase命名方式为第一个字母小写，而后所有单词首写字母为大写，中间不留空格，如camelCase。
  //微软建议简单的变量/字段命名用camelCase，高级的变量/方法命名用PascalCase，但是实际上，共识分类得更加细致。

//Q: 如何实例化对象？
//A: 用关键词new，遵循以下格式：

ClassName objectName = new ClassName(parameters);

//由于其中有重复的ClassName，我们可以用关键词var来降低重复性。var会自动根据类名为将要实例化的对象指定类名，如：

var objectName = new ClassName(parameters);

//Q: 如何访问类中的成员？
//A: 非静态类在实例化的对象后加"."即可访问。如：

objectName.Property;
objectName.Method(parameters);

//静态类不能被实例化，则可直接类名加"."访问成员，如：

Console.WriteLine();

//Q: 什么是静态类和静态成员？
//A: 静态类为标记有static的类，静态成员则是为标记有static的成员。静态类只能拥有静态成员，并且不能被new实例化，但可以直接调用其中的方法。

//Q: 为什么要有静态类和静态成员？
//A: 在实际情况中，有时候要创造一些不操作实例数据且不与程序中的其他对象关联的方法，这些方法就可以就声明为静态方法，用静态类来封装这些静态方法就能直接
  //调用，从而避免创造一些不必要的实例，优化整个程序。如一个人类：

public class Person
{
  public string Name;
  
  public Person Parse(string str)   //返回类型为一个实例
  {
    var person = new Person();
    person.Name = str;
    return person;
  }
}

//当我们需要调用Person类中的Parse方法来给Name赋值时，就需要首先创造一个实例，通过这个实例调用Parse方法。但是，Parse方法不是void方法，是一个返回为
  //一个Person实例的方法，所以我们需要重新声明一个p来保存这个实例。我们就有两个一样的Person类实例，这太傻逼了，如下：

static void Main(string[] args)
{
  var person = new Person();    //Person实例1
  var p = person.Parse("sdw");    //Person实例2
}

//有很多方法来改善这种情况，其中对其他代码影响最小的改法为将Parse改为静态方法，如：

public class Person
{
  public string Name;
  
  public static Person Parse(string str)  //将这个方法改为静态方法
  {
    var person = new Person();
    person.Name = str;
    return person;
  }
}
  
//这样就能避免得首先实例化Person类才能调用Parse方法，改为直接调用，如下：

static void Main(string[] args)
{
  var person = Person.Parse("sdw");
}

//创造静态成员因为要彰显成员的独特性(singleton)，意思为在内存中有且仅有一个该类/成员的实例，既然已经被实例了，那么就可以直接访问。反之，
  //如果一个非静态的类包含静态成员，那么该类的实例将不能访问这些静态方法，但是还是可以直接通过这个非静态类来访问//其中的静态成员的。
  //需注意，静态类的方法不能访问实例的方法和变量，但是反过来是可以的。

//Q: 既然用非静态类可以封装静态方法，那么为什么还要用静态类封装静态方法？
//A: 这个涉及到OOP规范，毕竟静态类的封装和使用是面向过程而不是面向对象，OOP在更多的时候要与接口衔接，个人觉得少用静态类。另外，由于静态类及其成员在
  //加载的时候就储存在内存，所以其会全程保持存在。

//暂时想到这么多，最后更新2017/11/20


