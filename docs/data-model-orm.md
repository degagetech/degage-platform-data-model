<p style="display:inline">




![img](logo.png)

## 数据模型-ORM ##

### v1.0

这是一个简单开箱即用的 ORM（应该算是 SQLHelper） 框架，它源自于大二时期对重复编写 CRUD 的厌烦感。 

所以我当时希望尽可能以最少的代码去实现 CRUD 操作，简单、快捷。

#### 目录

[如何使用](#howuse)

- [查询](#query)
- [插入](#插入)
- [更新](#更新)
- [删除](#删除)



------

##### 如何使用 <a id="howuse"> </a>

您可以通过 [此处](https://github.com/degagetech/degage-platform-data-model/tree/master/src/Core/Degage.DataModel.Orm.Example) 找到以下演示相关的代码。

首先您需要告诉我们数据库连接字符串，以用于初始化。

```c#
 var connStr = "Data Source = test.db; UTF8Encoding = True;";
 SQLiteDbProvider proivder = new SQLiteDbProvider("MyName", connStr);
```

另外也许您还有一个模型类，专门用于存储对象。

```c#
   public class TestUser
   {
       public String Id { get; set; }
       public String Name { get; set; }
       public Int32 Age { get; set; }
       public DateTime Born { get; set; }
       public String Descrption { get; set; }
   }
```

它的定义很简单，假如我们一直遵循C#中的匈牙利命名法以及在数据库中通过小写字母加下划线的方式命名，那么你不需要提供更多信息，ORM 会推导映射到数据库表以及字段上。



接下来我们尝试操作数据。

###### 查询操作 <a id="query"> </a>

现在我们想要获取用户表中的数据，您可以这么写：

``` C#
  var userInfos = proivder.Select<TestUser>().ToList();
```

框架会完成连接以及数据的处理，我们只需要使用模型类的数据对象。

当然，筛选是必不可少的











------



##### v2.0



****
若有兴趣一起完善此工具，您可以通过TIM扫描下面的二维码，添加时请备注您最得意的开源项目的地址。

<p>
<img width='150'  src="contact-tim.jpg" >
</p>