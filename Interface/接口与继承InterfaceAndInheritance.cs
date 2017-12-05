//Q: 接口与继承有的语法很相似，他们有什么关系？
//A: 除了语法相似，他们没有半毛钱关系，并且语法也有不同。如C#不支持多重继承，以下写法错误：

public class Gun : RangeWeapon, MordenWeapon    //C#不支持多重继承
{
  
}

  //然而一个类可以拥有多个接口，以下写法正确：

public class Gun : IRangeWeapon, IMordenWeapon    //这不是多重继承，只是声明Gun类有多个接口
{

}

//Q: 如果我声明一个衍生类有多个接口怎么办？
//A: 接口、基类写在一起即可。没有试过先后顺序有什么差别，但是正常一半都先写基类，再列出接口，以下写法正确：

public class Gun : Weapon, IRangeWeapon, IMordenWeapon    //基类、多个接口混写没关系
{

}

//暂时想到这么多，最后更新2017/12/4
