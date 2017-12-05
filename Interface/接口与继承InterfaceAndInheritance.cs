//Q: 接口与继承有的语法很相似，他们有什么关系？
//A: 在类与接口的关系中，除了语法相似，他们与继承没有半毛钱关系，并且语法也有不同。如C#不支持类的多重继承，以下写法错误：

public class Gun : RangeWeapon, MordenWeapon    //C#不支持多重继承
{
  
}

  //然而一个类可以拥有多个接口，以下写法正确：

public class Gun : IRangeWeapon, IMordenWeapon    //这不是多重继承，只是声明Gun类有多个接口
{

}

//Q: 如果我声明一个衍生类有多个接口怎么办？
//A: 接口、基类写在一起即可。没有试过先后顺序有什么差别，但是正常一半都先写基类，再列出接口，以下写法正确：

public class Gun : Weapon, IRangeWeapon, IMordenWeapon    //这也不是多重继承基类、多个接口混写没关系
{

}

//暂时想到这么多，最后更新2017/12/4

//Q: 接口之间可以有继承关系吗？
//A: 接口之间的继承是实打实的继承关系，不仅可以像类与类那样继承，还能多重继承。如以下代码：

public interface ITest : ITest1, ITest2
  {
    void Send();
    void Print();
    void Draw();
  }

public interface ITest2
  {
    void Wtf();
  }

public interface ITest1
  {
    void Speak();
  }

//继承的核心在于代码复用，上例中ITest就拥有ITest1和ITest2中的所有代码，即ITest完整版本为：

public interface ITest
  {
    void Speak();
    void Wtf();
    void Send();
    void Print();
    void Draw();
  }

//同理，在声明拥有ITest接口的类中，必须有完整的以上五种方法。

//最后更新2017/12/5
