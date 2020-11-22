# Contact Manager API
Contact Manager Web API project using .net core 3.1, entity framework core and sql server.


#How to run
1. Update the connection string in appsettings.json
2. Create the database using DatabaseCreationScript.sql or create a database named ContactManager and run the migration script in the Migrations folder using Update-Database command
   in the package manager console.
3. Set ContactManager.Api project as startup project in visual studio and run the application using F5 key.
4. This will open a swaggar url where you can test the api.
5. To create new contact use post method with request in the following format;
```json
    {
      "firstName": "Rajesh",
      "lastName": "Kumar",
      "email": "abc@gmail.com",
      "phoneNumber": "0000000000",
      "status": "Active"
    }
```
6. To update an existing contact use put method with request in the following format:
```json
    {
      "contactId": 1,	
      "firstName": "Rajesh",
      "lastName": "Kumar",
      "email": "abc@gmail.com",
      "phoneNumber": "111111111",
      "status": "Inactive"
    }
```

#Project structure
Contact manager application has following projects
1. ContactManager.Api: Interacts with repository to perform CRUD operation
2. ContactManager.Api.Tests: Contains unit test cases for ContactManager.Api
3. ContactManager.Common: Contains JsonFileDataAttribute class which is used for unit testing purspose
4. ContactManager.Data: Contains Dbcontext, repository and migrations
5. ContactManager.Entities: Contains model classes
6. ContactManager.Interfaces: Contains Interfaces used in the application




