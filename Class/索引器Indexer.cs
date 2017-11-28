//Q: 什么是索引器？
//A: 索引器是一种在以列表形式储存值的类中访问元素的方法。这句话如何理解？我们之间很早其实就接触过索引器，如在列表中、数组中访问具体位置的元素，这些其实
  //都是早据定义在List、int[]等类中的索引器。索引器建立了一个key-value一对一映像的关系，如list[0]只会返回第一个元素等。
  
//Q: 为什么要用索引器？
//A: 索引器能更加方便快捷的查找到我们需要的值。如果不用索引器，就只能用类中的方法来达到同样的效果。一些类中包含了集合的语义，如HTTP cookie，这是一种
  //在用户端和web服务器中传输的文件，每一次对web服务器的请求都会传输一次，用来分辨不同的用户，我们可以在cookie中储存一些用户的资料、设置，在这样的
  //cookie中我们就有一个名称和值字典(Dictionary)，这就是集合语义。我们可以通过索引器来搜索其中的键-值对应关系，代码如以下所示：
  
  var cookie = new HttpCookie();    //实例化一个HttpCookie类
  cookie.Expire = DateTime.Today.AddDays(5);    //设置一个过期时间
  
  cookie["name"] = "sdw";   //通过索引器来建立一个key-value的唯一对应关系，即"name"-"sdw"
  var name = cookie["name"];    //通过索引器来获得与key对应的值，即"sdw"
  
//如果不用索引器，那么旧的用HttpCookie类中的方法来实现key-value的对应关系，如一下代码所示：

  var cookie = new HttpCookie();    //实例化一个HttpCookie类
  cookie.Expire = DateTime.Today.AddDays(5);    //设置一个过期时间
  
  cookie.SetItem("name", "sdw");    //通过SetItem方法来设置key-value的对应关系
  var name = cookie.GetItem("name");    //通过GetItem方法来获得与key对应的值

//明显使用索引器方便快捷易懂，所以要用索引器。

//Q: 什么声明一个索引器？
//A: 索引器其实就是一个属性，如果想让一个类的实例像Lis或ArrayList那样使用索引器就能索引元素，那么就应该在这个类中添加一个索引器属性，如以下代码：

public class Example
{
  public string this[string key]  //与属性一样，必须确定索引器的类型与getter方法返回值相同，注意关键词"this"和后面的方括号，是声明索引器的标志
  {
    get { ... }   //获唯一对应取值的逻辑
    set { ... }   //设置唯一对应值的逻辑
  }
}

//以上索引器表示为，一个Example类的实例可以通过一个类型为string的键来设置/取得一个对应的值。具体什么类型的键什么类型的值根据情况确定，
  //如List的键的类型为int，值的类型为object，所以其源代码(非泛型)中的索引器大概长这样：

public class List
{
  public object this[int key]
  {
    get { ... }
    set { ... }
  }
}

//Q: 既然字典、列表是集合语义，它们都自带了索引器，为什么我们还要自己声明索引器？
//A: 还是为了方便。如果不自己声明索引器，那么就需要通过这个类的实例调用字典、列表的索引器，通常情况下实现都极其麻烦，因为字段都是私有无法直接访问，
  //还需要写两个方法GetItem和SetItem来获得/设置值，多此一举。举一个完整的Http Cookoie索引器例子：

public class HttpCookie
{
  private readonly Dictionary<string, string> _dictionary;  //作为一个正常字段，按照OOP原则，设为私有，字典类一般设为只读类型。
  
  public HttpCookie()
  {
    _dictionary = new Dictionary<string, string>();   //在构造器中实例化+初始化字典
  }
  
  public string this[string key]    //声明一个类中的索引器，有了这个索引器就能直接在类的实例后面加[]索引
  {
    get { return _dictionary[key];}   //取得值的逻辑
    set { _dictionary[key] = value;}    //设置值的逻辑
  }
}//手工的HttpCookie类

static void Main(string[] args)
{
  var cookie = new HttpCookie();    //实例化一个HttpCookie
  cookie["name"] = "sdw";   //直接使用索引器建立一个键值对应关系
  var name = cookie["name"];    //直接使用索引器获取对应键的值
}

//以上简明易懂如何声明、为何要在类中声明索引器。

//暂时想到这么多，最后更新2017/11/28
