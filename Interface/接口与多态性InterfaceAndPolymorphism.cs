//Q: 什么是多态性？
//A: 多态性(Polymorphism)在之前专门开了一章详解，再次总结一下。当针对不同的对象执行同一个(同名)操作时，能使用这些对象对这一操作的自己的解释，从而产生
  //不同的输出。如之前的复写例子：
  
static void Main(string[] args)
{
  var list = new List<Shape>();
  var circle = new Circle();    //实例化一个Circle类，这里复写了Draw()方法
  var rectangle = new Rectangle();    //实例化一个Rectangle类，这里复写了Draw()方法
  
  list.Add(circle);   //添加Circle实例并自动上转型
  list.Add(rectangle);    //同上

  foreach (var shape in list)
  {
    Draw();   //通过迭代列表中的对象调用他们同名方法Draw()，虽然同名，但是却是两个不同的方法，同样的迭代不同的输出，即体现了多态性
  }
}

//Q: 接口如何帮助实现多态性？
//A: 多种类可以拥有同种接口，即这些类都有同名的方法。继承中的复写产生的同名方法可以提现多态性，接口的也可以，且原理相同。我们可以把拥有相同接口的类的
  //实例都装进同一个类中。如IList<ILogger>列表中可以装入所有有ILogger接口的实例，无论是ConsoleLogger类还是FileLogger还是其他类都可以。
  //继续打Log的例子，以下是上一次在“接口与扩展性”中写的主类源代码：

public class DbMigrator
 {
   private readonly ILogger _logger;   //声明私有化ILogger字段，独立注入的一部分
   
   public DbMigrator(ILogger logger)   //这里采用的技术名称叫独立注入。指通过这种操作让这个类变得独立，任何需要直接访问其他类的情况将通过接口沟通
   {
     _logger = logger;
   }
   
   public void Migrate(string errorMessage, string infoMessage)
   {
     _logger.LogErorr(errorMessage);    //调用有ILogger接口的类中的LogErorr方法，不同的实例实施方法不同
     _logger.LogInfo(infoMessage);    //同上
   }
 }

//在接口与可扩展性中，我们可以通过独立注入随意更换有ILogger接口的实例，调用同名但不同实施的方法。
//但现在需求更新，我们不是替换，现在要加入不同的打Log方法，即又要有控制台输出日志，又要在文件系统中输出日志。这时一个ILogger字段就不够用了，我们需要
  //将其替换成一个包含接口类型的列表IList<ILogger>，代码如下：

public class DbMigrator
 {
   private readonly IList<ILogger> _logger;   //声明一个私有IList<ILogger>列表字段
   
   public DbMigrator(IList<ILogger> logger)   
   {
     _logger = logger;   //独立注入，IList<T>是接口，接口不能实例化，与之前相同，这里需要传入一个有IList<T>接口的类
   }
   
   public void Migrate(string errorMessage, string infoMessage)
   {
     foreach (var logger in _logger)   //迭代_logger列表中的所有实例
     {
       logger.LogErorr(errorMessage);    //调用有ILogger列表中的实例中的LogErorr方法，不同的实例实施方法不同，通过迭代再次体现了多态性
       logger.LogInfo(infoMessage);    //同上
     }
   }
 }

//接下来看非常重要的主函数对IList<ILogger>的操作：

  static void Main(string[] args)
  {
    IList<ILogger> logger = new List<ILogger>();    //IList<T>是接口，接口不能被实例化，但是可以将拥有这个接口的类实例化，如List<T>
    logger.Add(new ConsoleLogger());    //向列表中添加拥有ILogger接口的类，下同
    logger.Add(new FileLogger());
    
    var dbMigrator = new DbMigrator(logger); 
    dbMigrator.Migrate("Error!", "Infomation.");   //Migrate()方法将迭代列表中所有对象并执行其中的方法，同样的迭代不同的输出，体现了多态性
  }

//Q: 为什么要用IList<ILogger>？直接使用List<ILogger>并实例化不行吗？
//A: 可以是可以，但是这样就损失了可扩展性。IList<T>和List<T>看起来很像，只差一个字母，但是前者是接口，后者是一个类，如果在主程序中不使用IList<T>而
  //使用List<T>，那么构造器中就只能传入List<T>的实例作为参数。用IList<T>的话就可以在主程序构造器中传入所有有IList<T>接口的类，如一个自定义列表类
  //LoggerList<T>等，当然也包括List<T>。

//暂时先到这么多，最后更新2017/12/5
