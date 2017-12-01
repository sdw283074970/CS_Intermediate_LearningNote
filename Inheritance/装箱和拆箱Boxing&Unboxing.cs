//Q: 什么是拆箱和装箱？
//A: 之前忘了在哪个章节有解释过，那就再详细解释一次。了解拆箱和装箱之前需要了解数据结构，知道数据是储存在什么地方的。计算机将数据分为两种，一种是值类型，
  //一种是引用类型，了解了这两种类型后才能解释拆箱和装箱。

//Q: 什么是值类型？
//A: 值类型(Value type)储存在栈(stack)中，如字节(byte)、整型(int)、浮点型(float)、字符型(char)、布尔值型(bool)、结构类型(struct type)都是属于值类
  //型。什么是栈？当程序执行的时候，一小部分有限的内存空间会被分配给程序的每一个线程，这些空间就用来储存以上基本类型(primitive types)的简单值。所有储
  //存在栈中的值的寿命都是临时的，寿命短。当程序块退出时，这些值就消失了。

//Q: 什么是引用类型？
//A: 引用类型(Reference type)储存在堆中(heap)，所有类如Object、Array、String、DbMigrator等等都是引用类型，接口、数组也是引用类型。什么是堆？今后会
  //开一些新篇来解释记录什么是堆栈。在这里只用知道堆是程序分配的另一块储存空间，堆中储存的值比在栈中储存得要久一些，不随代码块的关闭而被删除，会一直留在
  //堆中直到程序关闭或被垃圾回收器删除。
  
//Q: 这些为什么重要？
//A: 必须理解我们写的代码是什么才能更好的提升程序效率。如之前举的上转型例子，一个堆中的实例有相同的重复的名字，代码如下：

static void Main(string[] args)
{
  var circle = new Cicle();
  Shape shape = circle;
}

//在上例中，circle和shape指向的其实是同一个对象，即Circle的实例，这个实例储存在堆中，而Circle circle和Shape shape储存在栈中，它们都只是指针，都指向
  //了同一个引用类型地址。同样，Object类是有所有类的基类，那么我们也可以直接有以下代码：
  
static void Main(string[] args)
{
  var circle = new Circle();
  Object shape = circle;   //把Shape类换成Object类
}

//这一步操作其实就是将实例circle的引用(Circle)转换成了Object引用。这时等号右边都是储存在堆中的引用类型，但是如果我们把右边的引用类型换成值类型，会发
  //生什么有趣的事情？会发生装箱。

//Q: 装箱操作的实质是什么？
//A: 装箱是指将值类型的值转换成Object引用对象。我们知道值类型的值是储存在栈上，Object引用本身是一个指针，也储存在栈上，但是这个指针指向的值储存在堆上，
  //换句话说，装箱将值类型的值打包放入了堆中成为了一个对象，并通过Object Reference引用。如以下代码：
  
  int number = 10;
  object obj = number;    //装箱操作
  object objNum = 10;   //或者这样也是装箱

//在这里需要知道装箱是一个功耗十分大的操作，应该避免

//Q: 什么是拆箱的本质？
//A: 拆箱即是装箱反过来，将在堆中打包成对象的值类型值转换成本来的样子。以下代码就能说明：

  object obj = 10;
  int number = (int)obj;    //通过强制转换类型拆箱
  
//Q: 实际情况中哪些时候会用到装箱和拆箱？
//A: 最熟悉的莫过于ArrayList()类。这个类将其所有成员都打包装箱以Object的形式储存在堆中，换句话说，无论什么类型的值，都可以混杂装在同一个ArrayList中。
  //如以下代码：
  
static void Main(string[] args)
{
  var list = new ArrayList();
  list.Add(1);
  list.Add("sdw");
  list.Add(DateTime.Today);
}
  
//同样，要取出这种ArrayList中的值需要对应拆箱，不再赘述。为了节省资源避免装箱拆箱，可以不用ArrayList改用C#最强大的泛型，如List<T>等等。

//暂时想到这么多，最后更新2017/11/30
