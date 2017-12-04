//Q: 什么是测试性？
//A: 有利用接口的测试在这里主要指单元测试(Unit test)，属于自动化测试(Automated testing)的一种。单元测试是一个很详细的领域，需要另开篇章探讨，这里
  //简短的介绍下，简而言之，单元测试是指我们编写另一个程序来测试我们的主程序。通过编写测试程序，我们就能免去花费大量时间手动测试程序的每一个功能，而
  //测试程序将自动完成所有需求的测试。单元测试的原理为，假设除了测试的目标类以外所有的类都正常工作，我们就只用针对被测试类编写测试代码。
  
//Q: 接口在自动化测试中扮演了什么角色？
//A: 接口相当于是在单元测试与源程序之间的桥梁。我们以一个接近实际情况的例子来说明这个问题。假设有一个订单处理项目，我们需要编写一个程序来处理订单。
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



















