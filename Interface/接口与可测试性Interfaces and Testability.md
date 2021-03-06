# 接口与可测试性 Interfaces and Testability
本篇记录了如何使用接口让产品代码更具可测试性

### 什么是测试性？与接口有什么联系？
这里主要指单元测试(Unit test)，属于自动化测试(Automated testing)的一种。单元测试是一个很详细的领域，~~需要另开篇章探讨~~目前已开坑，请戳![传送门](https://bit.ly/2Kw4yKS)。这里也简短的介绍下。

简而言之，单元测试是指我们编写另一个程序来测试我们的主程序。通过编写测试程序，我们就能免去花费大量时间手动测试程序的每一个功能，而测试程序将自动完成所有需求的测试。单元测试的原理为，假设除了测试的目标类以外所有的类都正常工作，我们就只用针对被测试类编写测试代码，即**隔离测试**。但是做**隔离测试**前有个前提条件，即受测试类不能与其他类有强关系(如依赖关系)才能完全假设其他类能正常工作。

如果有怎么做测试？我们就需要接口在消除这些强关系的同时，还要保证其功能性。
  
### 接口在自动化测试中扮演了什么角色？
接口可以降低类之间的耦合性，让类能被隔离测试。我们以一个接近实际情况的例子来说明这个问题。

假设有一个订单处理项目，我们需要编写一个程序来处理订单。程序需求对订单发货情况经行处理，规则为：

* 如果订单已发货，则显示已发货
* 如果没发货，则更改订单的发货属性，要求在属性中添加运费和预计发货日期，预计发货日期为处理订单日期的第二天
  
以下为这段程序的源代码：
首先是`Shipment`类，这个类作为`Order`类的一个属性，储存了运费、寄送日期等数据，代码如下：
```c#
public class Shipment
{
  public int Cost { get; set; }
  public DateTime ShippingDate { get; set; }
}
```
然后是`Order`类，这个类包括了订单价格、运输属性和发货状态，代码如下:
```c#
public class Order
{
  private bool _isShipped;
  public int TotalPrice { get; set; }
  public Shipment Shipment { get; set; }
  public bool IsShipped 
  {
    get { return Shipment != null; }  //如果Shipment为空，则返回False，说明订单没有发货
    set { _isShipped = Value; }
  }
}
```
接下来是`ShippingCalculator`类，这个类主要包括了一个方法，用来根据订单价格计算运费，代码如下:
```c#
public class ShippingCalculator
{
  public int CalculateShipping(Order order)
  {
    if (order.TotalPrice > 30)
      return order.TotalPrice * 0.1;
    else
      return 0;
  }
}
```
最后是`OrderProcessor`类，用来处理订单，实现需求，代码如下:
```c#
pulic class OrderProcessor
{
  private readonly ShippingCalculator _shippingCalculator;  //声明一个运费计算器私有字段
  
  public OrderProcesor()
  {
    _shippingCalculator = new ShippingCalculator();   //构造器中实例化运费计算器，由于有readonly，其只能被实例化一次
  }
  
  public void Process(Order order)    //该类的处理方法
  {
    if (order.IsShipped)    //判定订单是否发货，若已发货，则手动抛出异常
      throw new InvalidOperationException("This order has been shipped.");
    else    //否则，为订单更新状态
      order.Shipment = new Shipment   //实例化订单的运输属性
        {
          Cost = _shippingCalculator.CalculateShipping(order);    //在运输属性中初始化花费，由运费计算器获得
          ShippingDate = DateTime.Today.Add(1);   //预计发货日期为处理日期的第二天
        }
  }
}
```
主函数代码为:
```c#
static void Main(string[] args)
{
  var orderProcessor = new OrderProcessor();    //实例化一个订单处理器
  var order = new Order {ShippmentDate =  Date.Now, TotalPrice = 100};    //捏造一个订单
  orderProcessor.Process(order);    //处理订单
}
```
以上为我们的主程序代码。接下来新建一个测试程序，在VS中右键项目解决方案，添加一个`Unit Test Project`，命名为`Name.UnitTests`，将会生成以下代码：
```c#
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;  //VS自带的测试工具命名空间

namespace Name.UnitTests  //单元测试命名空间
{
  [TestClass]   //特性(attribute)，一种标签，通过标记元数据指引某种程序能够找到有这些标签的对象并经行访问，在这里即标记UnitTest1为TestClass
  public class UnitTest1
  {
    [TestMethod]  //标记TestMethod1为TestMethod
    public void TestMethod1()
    {   
    }
  }
}
```
在VS单元测试中，微软测试运行器将监视所有类及成员，并执行所有有测试特性的测试类和测试方法。

回到`OrderProcessor`主程序，如果我们用单元测试来测试程序中的`OrderProcessor`类，就会碰到一些麻烦。要做单元测试，意味者要隔离这个类，即要可以假设其他类都正常工作，只围绕这一个类写测试代码。在这个例子中，`OrderProcessor`类严重依赖于`ShippingCalculator`类，由于这种紧耦合，我们不能真正隔离`OrderProcessor`类，如果继续测试，这种紧耦合将会是一个大问题。如何解决？

这就需要引入接口。
  
### 如何用接口降低类之间的耦合程度？
在此例中，`ShippingCalculator`有一个函数，我们可以声明一个接口使其名义上与这个函数具有相同的功能，用这个接口替换`ShippingCalculator`类在`OrderProcessor`类的位置，然后再让`ShippingCalculator`中的函数来实现这个功能。这样就通过接口将`ShippingCalculator`从OrderProcessor中剥离出来，让`OrderProcessor`可以被隔离。
  
要实现隔离，我们可以先声明一个接口，这个接口指向`ShippingCalculator`类及其中的方法，所以接口以`IShippingCalculato`r命名，代码如下：
```c#
public interface IShippingCalculator
{
  int CalculateShipping(Order order);   //接口中的成员，只有类型、签名，没有逻辑
}
```
一个接口仅仅定义了一个接口所要提供的功能、能力，没有具体的实施方法。在这里`IShippingCalculator`仅仅定义了`ShippingCalculator`类具有的功能。但如何指向这个功能的实施者？只用在源类中声明有这个接口即可，声明方法如下：
```c#
public class ShippingCalculator : IShippingCalculator   //用跟继承一样的语法声明拥有的接口，但注意者并不是继承，是完全不同的概念
{
  public int CalculateShipping(Order order)
  {
    if (order.TotalPrice > 30)
      return order.TotalPrice * 0.1
    else
      return 0;
  }
}
```
现在我们的`ShippingCalculator`类中伸出了一个可以代替它功能的`IshippingCalculator`接口，替换掉原类在`OrderProcessor`中位置的代码如下:
```c#
pulic class OrderProcessor
{
  private readonly IShippingCalculator _shippingCalculator;  //私有类型替换为接口类型
  
  public OrderProcesor(IShippingCalculator shippingCalculator)  //构造器参数传入一个接口
  {
    _shippingCalculator = shippingCalculator;   //在构造器中初始化私有类型的接口
  }
```
通过以上操作我们就通过接口替换掉了所有写死的类，即松耦合。当我们改变计算方法的时候，只要方法名字不变，其他怎么变都没有关系。如以下代码:
```c#
  public void Process(Order order)
  {
    if (order.IsShipped)
      throw new InvalidOperationException("This order has been shipped.");
    else 
      order.Shipment = new Shipment
        {
          Cost = _shippingCalculator.CalculateShipping(order);
          ShippingDate = DateTime.Today.Add(1); 
        }
  }
}//OrderProcessor的其他代码都没有变化
```
那么问题又来了，既然在类中只声明了接口，只有虚的功能，那么实施的具体类即`ShippingCalculator`放在哪里？通过怎样才能链接到`OrderProcssor`类中？答案就在构造器中。我们看到虽然构造器参数类型为接口类型，理论上应该传入一个接口实例，但是接口实例是不存在的。这里的接口实例实际上是任何指拥有该类型接口的实例，在这里即`ShippingCalculator`的实例。主函数代码如下：
```c#
static void Main(string[] args)
{
  var orderProcessor = new OrderProcessor(new ShippingCalculator());    //构造器将识别参数中的实例有一个IShippingCalculator接口
  var order = new Order {ShippmentDate =  Date.Now, TotalPrice = 100};    //捏造一个订单
  orderProcessor.Process(order);    //处理订单
}
```
在用接口替换掉`OrderProcessor`类中的其他类后，我们可以开始写测试程序来测试这个类。首先第一个要测试的是`Process`方法中的第一种情况，即当订单已发货时，应该抛出一个异常。回到`UnitTest1`单元测试代码，首先要更改脚本中测试类和方法的名字。测试类的名字构成为`“受测类+Tests”`，测试方法的名字构成为`“受测方法名_条件_预计结果”`。在这里测试类和测试方法名字分别为`OrderProcessorTests`和`Process_OrderIsShipped_ThrowAnException`。整个测试代码如下：
```c#
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testability;    //必须加载OrderProcessor类的命名空间才能访问OrderProcessor和其他类

namespace Name.UnitTests 
{
  [TestClass]
  public class OrderProcessorTests    //更改后的测试类名，可以清楚知道测试的是OrderProcessor类
  {
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]  //这个特性指出运行期望，具体为抛出一个异常类型为InvalidOperationException的异常
    public void Process_OrderIsShipped_ThrowAnException()   //更改后的测试方法名，可以清楚的看出受测方法、条件和期望
    {   
      //声明受测类实例，参数要求传入有IShipingCalculator接口的实例，我们传入临时的替代品
      var orderProcessor = new OrderProcessor(new FakeShippingCalculator);  
      var order = new Order
      {
        Shipment = new Shipment()   //将订单中的Shipment属性指向一个空的Shipment实例，Shipment字段本身就不为空，这样IsShipped就为真
      };
      orderProcessor.Process(order);  //这一步操作后我们希望抛出异常。
      //这里的断言测试为检验结果是否为抛出一个类型为InvalidOperationExcaption的异常。返回在这个方法前贴上一个ExpectedException特性即可
    }
  }
  
  pubic calss FakeShippingCalculator : IShippingCalculator  //声明ShippingCalculator的替代品，也拥有IShippingCalculator接口
  {
    public int CalculateShipping(Order order)   //VS会自动上接口中声明的方法
    {
      return 1;   //假设该类中的方法工作正确，直接返回一个正确的值用于测试
    }
  }
}
```
无论用哪一种单元测试环境，其核心都是一样的。步骤为：

1. 使用接口隔离受测类
2. 设置前置条件
3. 执行测试方法
4. 做断言测试

现在来执行这个测试类。在VS中点击`TEST-> All test`。然后可以看到所有测试通过。

接下来测试`OrderProcessor`类的第二种情况，即订单没有发货。我们希望`order.Shipment`能够正确的初始化。我们在测试类中添加一个新的测试方法，代码如下：
```c#
  [TestMethod]    //同样需要TestMethod特性修饰，标明这是一个测试方法
  public void Process_OrderIsNotShipped_SetShipmentPropertyOfTheOrder()   //在订单没有发货的情况下，希望Process方法正确初始化Shipment属性
  {
    var orderProcessor = new OrderProcessor(new FakeShippingCalculator);    
    var order = new Order();    //这里Shipment为空，则IsShipped为假，即设定前置条件为没有发货
    
    orderProcessor.Process(order);    //执行测试方法 
    
    //我们需要断言order中的Shipment属性是否被正确设置。做断言测试需要调用Assert类，在Microsoft.VisualStudio.TestTools.UnitTesting命名空间下
    //在此例中我们需要断言三个对象是否正确，即执行Process方法后IsShipped是否为真、Cost是否被正确计算、发货日是否为操作后的第二天
    Assert.IsTrue(order.IsShipped);   //首先我们要确定IsShipped是否为真
    Assert.AreEqual(1, order.Shipment.Cost);  //然后测试cost是否等于1，AreEqual()方法中第一个参数为理论值，第二个参数为实际值，即检验对象
    Assert.AreEqual(DayTime.Today.AddDays(1), order.Shipment.ShippingDate); //检验最后一个指标ShippingDate是否为第二天
  }//接着点击TEST->All test来测试。测试通过，代码没问题。
```
以上为接口在单元测试的应用。

### 接口结构很简单，有没有快速方法声明一个类的接口？
可以使用VS自带的接口抽取功能抽取接口。方法为邮件类->reactor->抽取->Interface。然后就有一个对话框，依照情况勾选抽取。

暂时想到这么多，最后更新2017/12/4
最后更新2018/05/03
