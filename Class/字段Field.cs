//Q: 什么是字段？
//A: 字段(Field)是在类中声明的变量，用来储存数据。

//Q: 如何声明一个字段？
//A: 通常来说，字段类型+字段名称即声明了一个字段。如：

public class Example
{
  int i;    //声明了一个类型为int的变量/字段，命名为i
  List<Order> Orders;   //声明了一个包含Order类型的列表字段，命名为Orders
}

//以上字段都没有被初始化，未经过初始化的字段不能引用。什么是字段的初始化？正如刚才所说，字段是用来储存数据的，数据可以是一些实在的数据，
  //也可以是空值(null，但注意值类型的字段/变量是不能为空的，除非是可空类型，在以后的高级篇章有详细说明)，正确为字段赋值即为字段的初始化。

//Q: 如何初始化字段？在哪里初始化字段？
//A: 对于值类型的字段来说，为其赋值即将其初始化。对于引用型的字段来说，我们使用关键词new来实例化，在传入了必要的参数后即初始化，如一下代码：

public class Example
{
  int i = 0;    //将值类型的i赋值0，则i初始化
  List<Order> Orders = new List<Order>();   //通过关键字new，将引用类型的Orders实例化，这里没有需要传入的参数，等于说实例化即初始化
}

//一些程序员认为只有在变量/字段依赖外部传入参数赋值的情况下，才使用构造器初始化字段，在构造器(Constructor)一节中有提过。如以下代码：

Public class Example
{
  int i;    //先声明一个int类型的i变量
  List<Order> Orders = new List<Order>();   //不依赖外部参数赋值，直接实例化/初始化其为一个空列表
  public Example(int a)
  {
    this.i = a;    //依赖外部参数a赋值，即可在构造其中将i初始化
  }
}

//Q: 什么是只读类型字段？
//A: 修饰符只读(readonly)保证了被修饰的字段只会被赋值一次，而后不可更改。

//Q: 为什么要有只读修饰符？为什么我们需要用到这个？
//A: 为了保证数据安全。如在其他方法中可能需要调用到这个字段，如果我们不想其内容被覆盖/丢失，那么我们就需要用到readonly修饰符。



