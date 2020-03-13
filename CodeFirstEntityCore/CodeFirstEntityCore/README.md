Open Project:

0. Enter without code then create a Github repository and finally add new project in the same path

1. Choose .net core web application

2. Write your Solution Name

3. Select ASP.NET Core 2.1

4. Choose MVC template

5. Change authetication to Individual User Accounts

Models [One To Many]: 

1. Add class: Player.cs

2. Add class: Team.cs

3. Add: using System.ComponentModel.DataAnnotations;

4. Add props to Team and Player

appsettings.json:

1. Change the database name and the server of default connection string 

2. Add a mysql connection string to ConnectionStrings

Contexts:

1. Add DbSets of Player and Teams

Initializer:

1. Add the dummy data code

2. Add DummyData.Initialize(app) at Startup.cs;

MySql:

1. Manage NuGet Packages: Install Pomelo.EntityFrameworkCore.MySql 2.1.1 version

2. Add MySqlDbContext

3. Add useMySql at Startup.cs

4. Add private methods at DummyData.cs to add initial data to mysql context

Migrations:

1. open Package Manager Console

1. add-migration nhl0 -context ApplicationDbContext

2. update-database -context ApplicationDbContext

3. add-migration nhl1 -context MySqlDbContext

4. update-database -context MySqlDbContext

5. run the app to seed from DummyData.cs

MySQL 8.0 Command Line Client:

1. http://www.programmersought.com/article/309390683/

MVC Core Controller:

1. Select: MVC Controller with views, using Entity Framework

2. Add Controller

3. Select Model and Context for scaffolding for each model.

Views:

1. Open Shared folder, and open the file: _Layout.cshtml

2. <li><a asp-area="" asp-controller="Home" asp-action="About">About</a></li>
  
3. <li><a asp-area="" asp-controller="Home" asp-action="Contact">Contact</a></li>

Github and Team Explorer:

1. https://docs.microsoft.com/en-us/archive/blogs/benjaminperkins/setting-up-and-using-github-in-visual-studio-2017