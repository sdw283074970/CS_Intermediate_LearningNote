//Q: 什么是测试性？
//A: 有利用接口的测试在这里主要指单元测试(Unit test)，属于自动化测试(Automated testing)的一种。单元测试是一个很详细的领域，需要另开篇章探讨，这里
  //简短的介绍下，简而言之，单元测试是指我们编写另一个程序来测试我们的主程序。通过编写测试程序，我们就能免去花费大量时间手动测试程序的每一个功能，而
  //测试程序将自动完成所有需求的测试。单元测试的原理为，假设除了测试的目标类以外所有的类都正常工作，我们就只用针对被测试类编写测试代码。
  
//Q: 接口在自动化测试中扮演了什么角色？
//A: 接口可以降低类之间的耦合性，让类能被隔离测试。我们以一个接近实际情况的例子来说明这个问题。假设有一个订单处理项目，我们需要编写一个程序来处理订单。
  //程序需求为：对订单发货情况经行处理，如果订单已发货，则显示已发货；如果没发货，则更改订单的发货属性，要求在属性中添加运费和预计发货日期，预计发货日期
  //为处理订单日期的第二天。以下为这段程序的源代码，我们将使用单元测试来测试这段代码的OrderProcessor类：
  //首先是Shipment类，这个类作为Order类的一个属性，储存了运费、寄送日期等数据
  
public class Shipment
{
  public int Cost { get; set; }
  public DateTime ShippingDate { get; set; }
}

  //然后是Order类，这个类包括了订单价格、运输属性和发货状态

public class Order
{
  public int TotalPrice { get; set; }
  public bool IsShipped { get; set; }
  public Shipment Shipment { get; set; }
}
  
  //接下来是ShippingCalculator类，这个类主要包括了一个方法，用来根据订单价格计算运费
  
public class ShippingCalculator
{
  public int CalculateShipping(Order order)
  {
    if (order.TotalPrice > 30)
      return order.TotalPrice * 0.1
    else
      return 0;
  }
}

  //最后是OrderProcessor类，用来处理订单，实现需求
  
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

  //主函数代码为
  
static void Main(string[] args)
{
  var orderProcessor = new OrderProcessor();    //实例化一个订单处理器
  var order = new Order {ShippmentDate =  Date.Now, TotalPrice = 100};    //捏造一个订单
  orderProcessor.Process(order);    //处理订单
}
  
  //以上为我们的主程序代码。接下来新建一个测试程序，在VS中右键项目解决方案，添加一个Unit Test Project，命名为Name.UnitTests，将会生成以下框架：

using System;
using Microsoft.VisualStudio.TestTools.UnitTestin;  //VS自带的测试工具命名空间

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

  //在VS单元测试中，微软测试运行器将监视所有类及成员，并执行所有有测试特性的测试类和测试方法。
  //回到OrderProcessor主程序，如果我们要测试OrderProcessor类，我们就要隔离它，意味着要假设其他类都正常工作，只围绕这一个类写代码，即单元测试。所以，
  //单元测试是指专注于程序中一个单元的功能，不考虑其他类的影响。在这个例子中，OrderProcessor类严重依赖于ShippingCalculator类，由于这种紧耦合，我们
  //不能真正隔离OrderProcessor类，如果继续经行单元测试，这种紧耦合将会是一个大问题。如何解决？

  //这就需要引入接口。
  
  //如何用接口降低类之间的耦合程度？在此例中，ShippingCalculator有一个函数，我们可以声明一个接口使其名义上与这个函数具有相同的功能，用这个接口替换
  //ShippingCalculator类在OrderProcessor类的位置，然后再让ShippingCalculator中的函数来实现这个功能。这样就通过接口将ShippingCalculator从
  //OrderProcessor中剥离出来，让OrderProcessor可以被隔离。
  
  //如何实现？我们可以先声明一个接口，这个接口指向ShippingCalculator类及其中的方法，所以接口以IShippingCalculator命名，代码如下：

public interface IShippingCalculator
{
  int CalculateShipping(Order order);   //接口中的成员，只有类型、签名，没有逻辑
}

  //一个接口仅仅定义了一个接口所要提供的功能、能力，没有具体的实施方法。在这里IShippingCalculator仅仅定义了ShippingCalculator类具有的功能。
  //但如何指向这个功能的实施者？只用在源类中声明有这个接口即可，声明方法如下：

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
  
  //现在我们的ShippingCalculator类中伸出了一个可以代替它功能的IshippingCalculator接口，如何替换掉原类在OrderProcessor中的位置？

pulic class OrderProcessor
{
  private readonly IShippingCalculator _shippingCalculator;  //私有类型替换为接口类型
  
  public OrderProcesor(IShippingCalculator shippingCalculator)  //构造器参数传入一个接口
  {
    _shippingCalculator = shippingCalculator;   //在构造器中初始化私有类型的接口
  }
  
  //通过以上操作我们就通过接口替换掉了所有写死的类，即松耦合。当我们改变计算方法的时候，只要方法名字不变，其他怎么变都没有关系
  
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

  //那么问题又来了，既然在类中只声明了接口，只有虚的功能，那么实施的具体类即ShippingCalculator放在哪里？通过怎样才能链接到OrderProcssor类中？
  //答案就在构造器中。我们看到虽然构造器参数类型为接口类型，理论上应该传入一个接口实例，但是接口实例是不存在的。这里的接口实例实际上是任何指拥有该
  //类型接口的实例，在这里即ShippingCalculator的实例。主函数代码如下：

static void Main(string[] args)
{
  var orderProcessor = new OrderProcessor(new ShippingCalculator());    //实例化一个订单处理器
  var order = new Order {ShippmentDate =  Date.Now, TotalPrice = 100};    //捏造一个订单
  orderProcessor.Process(order);    //处理订单
}










