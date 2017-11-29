//Q: 什么是组合关系？
//A: 组合(Composition)是指一种包含关系，即一个类包含另一个类，或者说拥有一个类(Has)另一个类。如汽车类包含(拥有)一个引擎类。

//Q: 组合关系有什么好处？
//A: 与继承类似，通过组合我们可以代码复用，而且更加的灵活。组合关系意味着松耦合设计。从什么地方能体现组合的灵活？
  //设想在一个实际项目中，我们设计一个类DbMigrator，用来实现数据库的迁移，具体表现为新建表、复制行列名称、复制数据等。因为一个迁移设计一步一步的行为，
  //为了追踪其过程，我们需要打log，即输出一个txt格式的操作日志。在这个项目中，我们还需要一个Installer类，用来在目标环境下安装应用程序，同样这个类也需要
  //打log来记录操作结果。我们可以看出，"打log"这个部分是两个类的共同组成部分。于是我们可以建立一个Logger类来实现打log的操作。并让两个类都包含Logger类。
  
//Q: 如何实现组合？有没有像继承那样特定的语法？
//A: 在C#中实现组合很简单，并没有特别的语法。只用两步即可建立一个组合关系。
  //1. 声明一个私有字段，代表所包含的类的实例；
  //2. 在构造器中通过参数传递将类的实例传入到声明的私有字段中。
  //以DbMigrator和Logger类为例，让DbMigrator包含Logger只用跟随以下代码：

public class Logger
{
  public void Log(string message)   //Log的公用方法，要想不同的类打出不同的log，只用调用这个方法的时候在参数处写对应的log就可以了
  {
    Console.WriteLine(message);   //打出message
  }
}

public class DbMigrator
{
  private readonly Logger _logger;   //声明一个Logger类的私有字段，将这个字段设为只读可以避免其被以外修改
  
  public DbMigrator(Logger logger)    //在构造其中将Logger类的实例logger作为参数获取
  {
    _logger = logger;   //将获取的实例logger传入到声明的私有字段_logger中
  }
  
  public void Migrate()
  {
    ...   //模拟迁移逻辑
    _logger.Log("Migration done");    //通过其包含的Logger类打出与数据库相关的log
  }
}

static void Main(string[] args)
{
  var logger = new Logger();    //在实例化宿主之前，要先实例化组件。因为实例化宿主的时候依赖这个组件。
  var dbMigrator = new DbMigrator(logger);    //实例化宿主(DbMigrator)，当然为了简化也可以直接在签名中实例化Logger
  
  dbMigrator.Migrate();   //调用迁移方法
}//输出结果为通过迁移方法Migrate打出的log。同样如果是Installer，只要在其安装方法中定义安装的log，那么调用的时候打出的也是安装的log。

//通过以上代码我们可以发现，两个类都需要Logger类，而Logger类我们只写了一次，通过组合我们把Logger安进了宿主中，体现了代码复用。
  //当打log的方法需要更改的时候，如从输出txt文件改成发email，那么我们只需要该Logger类就行了，宿主类完全不用变，者体现了组合的灵活性。

//暂时想到这么多，最后更新2017/11/29
