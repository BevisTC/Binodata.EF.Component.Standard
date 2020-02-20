# Binodata.EF.Component.Standard
Binodata Entity framework component standard for .net core


### Read me
* Biniodata  Data Base Common Handle

###  Add Data Sample


```<C#>
IUnitofWork unitOfWork = EFUnitOfWorkFactory.GetUnitOfWork<Context>();
IGenericRepository<Customer> repo = unitOfWork.GetGenericRepository<Customer>();
repo.Add(new Customer(){Name = "B", BirthDay = "1966/01/05"});
unitOfWork.Save();

unitOfWork.Dispose();
```


### Update Data Sample


```<C#>

var customer = SelectFromDB(x => x.Name == "B");
customer.Name = "C";

IUnitofWork unitOfWork = EFUnitOfWorkFactory.GetUnitOfWork<Context>();
IGenericRepository<Customer> repo = unitOfWork.GetGenericRepository<Customer>();
repo.Edit(customer);
unitOfWork.Save();

unitOfWork.Dispose();
```



### Query Data Sample


```<C#>



IUnitofWork unitOfWork = EFUnitOfWorkFactory.GetUnitOfWork<Context>();
IGenericRepository<Customer> repo = unitOfWork.GetGenericRepository<Customer>();
repo.QueryByCondition(x => x.Name == "B");
unitOfWork.Dispose();
```
