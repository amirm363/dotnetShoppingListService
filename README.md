# dotnetWebService

A dotnet web api service that connects between a react client side application and a MySQL data base hosted on AWS.

Versions:
dotnet: 7.0.100
framework: .net 6.0
IDE: visual studio 2022


first step for running the procjet is to clonse the repository from github:
open cmd from the desired folder you want to save to project to and run the command "git clone https://github.com/amirm363/dotnetShoppingListService.git"

than you should get into the cloned project folder with the command "cd dotnetShoppingListService"
and run the command dotnet restore to restore all the project dependencies.

because its an assignment the database connection is already configured, but if you want the connection details to see the changes on the database, here they are:

Endpoint: shoppinglist.coo8a2vfng65.us-east-1.rds.amazonaws.com
Port: 3306
username: amirmeisner
password: amirm363

to run the project type in the cmd "dotnet run" and it will launch it.
