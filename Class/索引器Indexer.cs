//Q: 什么是索引器？
//A: 索引器是一种在以列表形式储存值的类中访问元素的方法。这句话如何理解？我们之间很早其实就接触过索引器，如在列表中、数组中访问具体位置的元素，这些其实
  //都是早据定义在List、int[]等类中的索引器。索引器建立了一个key-value一对一映像的关系，如list[0]只会返回第一个元素等。
  
//Q: 为什么要用索引器？
//A: 索引器能更加方便快捷的查找到我们需要的值。如果不用索引器，就只能用类中的方法来达到同样的效果。如一些类中包含了集合的语义，如HTTP cookie，这是一种
  //在用户端和web服务器中传输的文件，每一次对web服务器的请求都会传输一次，用来分辨不同的用户，我们可以在cookie中储存一些用户的资料、设置，在这样的
  //cookie中我们就有一个名称和值的列表。如果我们要实现一个HTTP cookie的概念，那么代码如以下所示：
  
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
//A: 索引器其实就是一个属性。
