# SocialApp-927

In order to create the database, open terminal in ServerAPIProject and run the following command:
```
dotnet ef database update --startup-project ../ServerApiProject
```
If you want to revert to the initial state of the database:
```
dotnet ef database drop -f --startup-project ../ServerApiProject
dotnet ef database update --startup-project ../ServerApiProject
```
