# Ecommerce

Asp.NetCore 3.1 (Individual User Account) Web Application (MVC)
Install 3 Class Library
-	Model
-	DataAccess(DA)
-	Utility
Nugget Packages –
-	Microsoft.AspNetCore.Mvc (Model,DA)
-	Microsoft.AspNetCore.Mvc.NewtonsoftJson (Ecommerce)
-	Microsoft.EntityFrameworkCore(DA)
-	Microsoft.Extensions.Identity.Stores(DA,Model)
Add new Area – Admin
Add new Area – Customer
Delete Model and Data from Admin and Customer Area
Move Default HomeController to Customer Area
Add [Area("Customer")] on top of the HomeController
Move Default Home View to Customer Area
Copy (_ViewImports & _ViewStart) to Customer and Admin Area
Startup.cs –
pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
Change the _ViewStart Layout of Admin and Customer area to 
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}

Move ErrorViewModel to Ecommerce.Model and
Move Data folder to Ecommerce.DataAccess
Rename ApplicationDbContext namespace Ecommerce.DataAccess.Data
Delete Migration Folder
Add 3 Folder Migration , Initializer & Repository
Add Dependency on Ecommerce -  DA and Model
Add Dependency on Ecommerce.DataAccess -  Model and Utility
The Structure will Look like this
 
 


