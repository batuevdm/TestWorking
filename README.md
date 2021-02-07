# Приложение Контакты

Платформа: ASP.NET MVC

Используется принцип Code first. Для развертывания базы данных нужно выполнить команду в Package Manager
```
Update-Database
```

Данные для подключения к БД находятся в файле *TestWorking/Web.config* строка 72
```
<add name="ContactsContext" connectionString="data source=.\SQLEXPRESS;initial catalog=ContactsDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
```

Используются библиотеки:
* JQuery 3.5.1 для динамической работы с HTML элементами
* Bootstrap 4.6.0 для создания макета и внешнего вида
* Entity Framework 6.1.3 для работы с базой данных
* .NET Framework 4.7.2
* СУБД MS SQL Server 2019 Express
