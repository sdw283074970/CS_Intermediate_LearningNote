//Q: 接口如何影响程序的扩展性？
//A: 首先，接口用于替代类中可能直接引用到的其他类，降低了程序的耦合性。不同的类可以拥有相同的接口，只要它们有接口声明的方法/函数/功能。这意味着
  //在不更改原类的代码情况下，我们可以随意在客户端(Client)或主函数中的接口参数位置插入各种有相同接口的不同的类，这一切只用改主函数或客户端的代码即可。
  //也就是说，通过接口我们可以通过最小的代码改变来更改/添加程序的功能。仍然以实际项目为例：
  
  //项目需求：设计一个数据库类及数据库迁移类，要求能够在各种地方输出迁移日志，如在控制台输出、文件系统中输出或其他现在不知道以后可能用到的系统中输出。
  //此项目的核心在于输出日志途径的不确定性，如果按照普通写法在控制台输出，如果某时刻要换成在其他系统中输入，那么就要更改大量已存在代码。而使用接口设计
  //将可以在最小化代码更改的基础上更换、添加符合要求的功能。
  
  //这是一个典型的OOP设计，我们可以先从接口入手，接口同时也是一个方法组成规范，代码如下：
  
public Interface ILogger
{
  void LogErorr(string message);    //事先规定包含这个接口的类必须有LogErorr方法
  void LogInfo(string message);   //事先规定包含这个接口的类必须有LogInfo方法
}

  //其次设计一个包含ILogger接口、通过控制台打log的服务类，注意要想包含一种接口，必须满足接口中声明的所有方法
  
public class ConsoleLogger : ILogger
{
  public void LogErorr(string message)
  {
    Console.WriteLine(message);   //方法LogErorr的逻辑
  }
  
  public void LogInfo(string message)
  {
    Conosle.WriteLine(message);   //方法LogInfo的逻辑
  }
}
  
  //然后设计客户类，即数据库迁移类
  
public class DbMigrator
 {
   private readonly ILogger _logger;   //声明私有化ILogger字段，依赖注入的一部分
   
   public DbMigrator(ILogger logger)   //这里采用的技术名称叫依赖注入。指通让客户类不依赖服务类，任何需要直接访问服务类的情况将通过接口沟通
   {
     _logger = logger;
   }
   
   public void Migrate(string errorMessage, string infoMessage)
   {
     _logger.LogErorr(errorMessage);    //调用有ILogger接口的类中的LogErorr方法，包含了具体执行逻辑
     _logger.LogInfo(infoMessage);    //同上
   }
 }

  //以上为主类。与单元测试类似，我们需要在主函数中主类构造器的参数位置传入一个有ILogger接口的类的实例，如ConsoleLogger类的实例
  
  static void Main(string[] args)
  {
    var dbMigrator = new DbMigrator(new ConsoleLogger());   //在DbMigrator构造器参数位置传入有ILogger接口的类的实例，即ConsoleLogger的实例
    dbMigrator.Migrate("Erorr occuried.", "This is a log.");  //调用Dbmigrator实例中的迁移方法
  }//输出结果为在控制台中打出两段Log
  
  //如果后来需求更新，我们不要在控制台中打Log，换成在文件系统中打Log怎么办？
  //由于接口的存在，我们不用改任何代码，直接新建一个有ILogger接口类，用于在文件系统中输出
  
public class FileLogger : ILogger
{
  pravite readonly string _path;
  
  public FileLogger(string path)    //从外界获取输出文件路径
  {
    _path = path;
  }
  
  public void LogErorr(string message)
  {
    Log(message);  //为了减少重复代码，将重复的代码移到Log方法中
  }
  
  public void LogInfo(string message)
  {
    Log(message);
  }
  
  public void Log(string message)
  {
    using(var streamWriter = new StreamWriter(_path, true)) //在文件系统中输出日志的的逻辑
    {
      streamWriter.WriteLine(message);
    }
  }
}
  
  //换一个地方打Log，只用在DbMigrator构造器接口参数位置替换上对应的类的实例即可
  
  static void Main(string[] args)
  {
    var dbMigrator = new DbMigrator(new FileLogger("C:\\log.txt"));  //由于FileLogger和ConsoleLogger一样，都有ILogger接口，所以都可以作为ILogger参数传入
    dbMigrator.Migrate("Erorr occuried.", "This is a log.");   //此例中这里的代码也不用改
  }//输出结果为在文件系统中输出了Log
  
  //以上我们可以看到这段程序的可扩展性，即可以随意更改、增加新的输出方法而不用更改程序源代码。在编程术语中，这个过程被称作“开放-关闭原则”，
  //即Open-close principal(OCP)，意思为一个类类不应该被更改，但是可以被扩展。
  
//Q: 在那些情况下需要用到接口优化程序？
//A: 并不是所有情况下都用接口来增强松耦合设计。有时候重新写一个类会更麻烦。一般来说，当一个类包含了某种算法，未来这种算法可能会更改、替换，那么
  //这是就应该使用接口来调用这个类，未来也方便替换。
  
//暂时想到这么多，最后更新2017/12/4
