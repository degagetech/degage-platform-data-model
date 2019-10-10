<p style="display:inline">
Hi,Every one.



![img](logo.png)

## 数据模型-ORM ##

### v1.0(只做修复性维护)

这是一个简单开箱即用的 ORM（应该算是 SQLHelper） 框架，它源自于大二时期对重复编写 CRUD 的厌烦感。 

所以我希望尽可能以最少的代码去实现 CRUD 操作，简单、快捷。

#### 目录

[如何使用](#howuse)

- [查询](#query)
- [插入](#insert)
- [更新](#更新)
- [删除](#删除)

- [支持的操作以及正确的调用顺序](#api)

------

##### 如何使用 <a id="howuse"> </a>

可以通过 [此处](https://github.com/degagetech/degage-platform-data-model/tree/master/src/Core/Degage.DataModel.Orm.Example) 找到以下演示相关的代码。

首先您需要告诉我们数据库连接字符串，以用于初始化。

```c#
 var connStr = "Data Source = test.db; UTF8Encoding = True;";
 SQLiteDbProvider proivder = new SQLiteDbProvider("MyName", connStr);
```

另外也许还需要一个模型类，专门用于存储对象。

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

它的定义很简单，假如我们一直遵循C#中的匈牙利命名法以及在数据库中通过小写字母加下划线的方式命名，那么你不需要提供更多信息，框架会推导并映射到数据库表以及字段上。



接下来我们尝试操作数据。

###### 查询操作 <a id="query"> </a>

现在我们想要获取用户表中的信息：

``` C#
  var userInfos = proivder.Select<TestUser>().ToList();
```

框架会完成连接以及数据的处理，我们只需要使用模型类的数据对象。

当然，筛选是必不可少的，你可以使用 Lambda 表达式。

```c#
var driver=  proivder.Select<TestUser>();
var userInfos = driver.Where(t => t.Id == "2").ToList();
```

重复组合条件

```c#
driver=driver.Where(t=>t.Id=="3" && t.Age=="18");
var userInfos = driver.ToList();
```

但是请注意，在同一个 *driver* 上调用 *Where*，它们之间始终为 和（And） 的关系，若你需要或，则应该调用 *OrWhere*：

```c#
driver=driver.OrWhere(t=>t.Id=="3" && t.Age=="18");
```

若你想继续使用 SQL 查询，调用 *Query* 传入你的 SQL 即可，框架会帮你处理好其他的一切，这结合的 SQL 的灵活性，但又节省了对结果集的处理代码：

```c#
userInfos = proivder.Query<TestUser>("select * from test_user where id=1").ToList();
```

但是，我们并不建议您这么做，使用框架一部分原因，在于屏蔽数据源的差异性，假如您的 SQL 具有特异性，也许会为后续的迁移工作带来麻烦，而框架会处理这些。

另外筛选条件也是支持 SQL 的，但是我们希望参数的值被直接拼接，而是传入框架由我们处理。

```c#
userInfos = proivder.Select<TestUser>().Where("id={0}","1").ToList();
```

在第一个字符串参数中通过类似 {0} 的占位符给需要的参数占位，第二个参数为 param Object[] 您根据需要传值即可（当然它存在装箱的问题）。



好了，我们从 SQL 中离开，回到 C#。

现在假设我们需要获取指定几个值的用户信息，该如何操作呢，比如，我们要获取一些年龄的用户：

```c#
userInfos = proivder.Select<TestUser>().In(t => t.Age, 18, 19, 20).ToList();
```

可以看到，首先需要指定字段，之后给出这些值即可。

更多支持的操作，可以点击 [此处](#api) 查看。



###### 插入操作 <a id="insert"> </a>

//...

###### 更新操作 <a id="update"> </a>

//...

###### 删除操作 <a id="delete"> </a>

//...

###### 支持的操作以及正确的调用顺序 <a id="api"> </a>

//...

------



### v2.0（积极开发的版本）



****
若有兴趣一起完善此工具，您可以通过TIM扫描下面的二维码，添加时请备注您最得意的开源项目的地址。

<p>
<img width='150'  src="contact-tim.jpg" >
</p>