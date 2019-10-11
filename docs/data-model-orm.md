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
- [更新](#update)
- [删除](#delete)

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

另外筛选条件也是支持 SQL 的，但是我们不希望参数的值被直接拼接，而是传入框架由我们处理。

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

现在我们需要向表中插入一条记录：

```c#
   TestUser newUser = /...
   var affect = proivder.Insert(newUser).ExecuteNonQuery();
```

但是请注意，框架不会检查特定字段是否符合限制（比如向非空的字段插入空），而是在发生错误后将异常抛出，用于你调式、处理。

批量插入：

```c#
 proivder.BatchInsert(objs)..ExecuteNonQuery();
```

在这里你需要注意，不同数据库驱动程序，对每次能提交的 SQL 大小都具有限制，使用批量插入，我们很容易在不经意间超出此限制（每次插入过多的对象，另外对象字段的个数也会影响）。

###### 更新操作 <a id="update"> </a>

假设我们需要更新某一个用户的描述信息：

```c#
affect = proivder.
  Update<TestUser>
  (
    () => 
    new TestUser 
     {
       Descrption = "New Desc " + DateTime.Now.ToString() 
     }
   ).Where(t => t.Id == "1").ExecuteNonQuery();
```

可以看到，代码复杂了些，但是并不晦涩，后面的 *Where* 条件容易理解，而 *Update* 的第一个参数通过构造一个 TestUser 对象，方便你设置需要更新的字段，新的值可以是常量也可以是另外一个函数调用、表达式。

###### 删除操作 <a id="delete"> </a>

当我们需要删除一些数据的时候：

```c#
 affect = proivder.Delete<TestUser>().
            Where(t => t.Name == "John Wang").
            ExecuteNonQuery();
```

###### 支持的操作以及正确的调用顺序 <a id="api"> </a>

Level 越小的操作，应该置于调用链越前的位置。

**Level:0**    返回值均为 IDriver<T>

- Select<T>()

- Query<T>(String sql)

- Insert<T>(T obj);

- BatchInsert<T>(IEnumable<T> objs)

- Delete<T>()

- Update<T>(Expression<Func<T>> regenerator)

  

**Level:1**   返回值均为 IDriver<T>

- Where(Expression<Func<T, Boolean>> predicate)

- Where(String whereSql, IEnumerable<DbParameter> dbParameters = null)

- OrWhere(Expression<Func<T, Boolean>> predicate)

- In<T>(Expression<Func<T, Object>> filter,params Object[] values)

- OrIn<T>(Expression<Func<T, Object>> filter, Object[] values)

- JoinOn<T1>(Expression<Func<T, T1, Boolean>> predicate)    

- JoinWhere<T1>(Expression<Func<T, T1, Boolean>> predicate) 

- IDriver<T> Page<T>(Expression<Func<T, Object>> sorter, Int32 start, Int32   count, Boolean asc = true)

   请注意，此操作目前只适用于 MySql 数据库，尚未适配其他数据库，若有需要您可以向  IDriver<T> 接口添   加扩展方法。

  

**Level:3**

- ISelectVector<T> ExecuteReader(String connectionString = null)  

  override1：DbTransaction transaction

  override2：DbConnection connection

- Object ExecuteScalar(String connectionString = null)

  同上

- Int32 ExecuteNonQuery(DbTransaction transaction)

  同上

- List<T> ToList<T>(String connectionString = null)

  同上

- T[] ToArray<T>(String connectionString = null) 

  同上

- T ToFirstOrDefault<T>(String connectionString = null) 

  同上

-  Int32 Count(String columnName = null, DbConnection connection = null)

  

------



### v2.0（积极开发的版本）



****
若有兴趣一起完善此框架，您可以通过TIM扫描下面的二维码，添加时请备注您最得意的开源项目的地址。

<p>
<img width='150'  src="contact-tim.jpg" >
</p>