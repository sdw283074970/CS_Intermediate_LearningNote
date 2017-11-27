//Q: 什么是属性？
//A: 属性(Property)也是类的成员，用来封装访问字段的getter和setter方法。

//Q: 为什么要用到属性？
//A: 为了更少、更清楚的代码来实现封装字段。

//Q: 属性要怎么写？
//A: 以下为在访问修饰符章节中最后引用的例子，可以发现一旦涉及到字段的封装，我们就需要以下这种模式化的代码。其实可以通过属性简化，以下为原代码：

public class Person
{
  private string _name;    //私有封装字段_name
  
  public void SetName(string name)    //设置一个public的赋值方法，用来在特别的情况更改Name的值，即setter
  {
    if (!String.IsNullOrEmpty(name))    //传入参数必须不为空才能成功设置
      this._name = name;
  }
  
  public string GetName()   //设置一个pubic的方法获得Name的值，即getter
  {
    return _name;
  }
}
  
//以上代码可以转换为属性版本如下：

public class Person
{
  private string _name;    //私有封装字段_name
  
  public string Name    //声明一个与字段类型相同(string)的Name属性，使用Pascal规则命名，必须为public，否则外部无法访问，属性就失去意义
  {
    get { return _name; }   //get方法，返回值类型必须与属性类型相同，在这里即string
    set { _name = value; }  //set方法，value代表了一切在等号左边的值，并将其赋值给_name
  }
}

//以上代码中的属性没有逻辑块，只是单纯的返回某个值和赋值。我们能使用自动属性进一步在属性中没有逻辑块的情况下简化书写，以上代码完全等同以下代码：

Public class Person
{
  public string Name { get; set; }    //自动属性。注意这里是public且是Pascal命名
}

//当C#编译器看到这里，就会自动的创建一个私有的字段，字段名称与属性名称一样，只是将第一个字母变小写并加了下划线前缀。
//当然，get和set后面的代码块是可以包含逻辑的。如果只想要getter或只想要setter，完全可以将另外一个省略不写。同样，属性中的getter和setter也可以
  //被访问修饰符修饰，如：

Public class Person
{
  public string Name { get; private set; }    //默认public，如果加上private访问修饰符，那么其他类将不能调用getter
}

//如果setter为私有类型修饰符，那要如何初始化？答案是在构造器初始化就行了，这样初始化的字段将不能被改变，只能访问调用/引用。

//Tips：快速打出自动属性：在VS中输入prop，然后TAB即可。

//暂时想到这么多，最后更新2017/11/27


