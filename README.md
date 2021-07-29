# README #

### What is Mailer microservice ? ###

Mailer microservice contains functionalities regarding email services, especially:
* Integration with SMTP services
* Sending emails from external services
* Defining message templates
* Defining message receivers

Version 1.0.0

### How to set up local development environment ###

* API
To run and compile solution the .NET Core 3.1 is required and Visual Studio 2019.
After opening the solution should build without any errors.

* Database
Mailer microsrevice writes and reads data using local MS Sql Server database (LocalDB).
It is required to create new database with parameters same as defined ConnectionString property in the Api\Mailer.Api\appsettings.json file.
Database schema is updated via migrations defined in the project Api\Mailer.Migrations.
Migrations can be executed in Visual Studio setting up a default project as Mailer.Migrations with configuration set to Develop. Running Execute should run migrations.

* Tests
Tests are written using the XUnit framework. Running tests should be possible after building the application.

There are two types of tests int the Mailer microserviice - unit tests and integration tests.
Unit tests have no dependencies and verify only domain layer with business logic. They should cover up to 100% of code.
Integration tests verify application layer with all dependencies (database, SMTP server etc.). They should contain only happy path or some minor logic kept in the Application layer.
Business logic should be placed in the domain layer.
 
Tests should always be green.

### How to develop further the Mailer microservice###

Applicatioin is written using the ports and adapters architecture (https://herbertograca.com/2017/11/16/explicit-architecture-01-ddd-hexagonal-onion-clean-cqrs-how-i-put-it-all-together/).

* Mailer.Api
Controllers layer is the input layer of the application allowing integration with external clients (other applications/microservices). 
Only infrastructure code necessary to run the REST API should be placed here (no business logic).

* Mailer.Services
Application services layer used to integrate between client layers (e.g Mailer.Api) with the domain (Mailer.Domain).
In this layer there are services acting like facades, command handlers, query handlers, event handlers and adapters implementing defined domain ports.
Classes in this layer should rely on domain abstractions, not on infrastructer concrete classes.

* Mailer.Domain
Domain layer containing business logic. Should not contain any dependencies. Should define business abstractions related with business domain.

* Mailer.Infrastructure
Infrastructure layer implementing domain abstractions. Objects responsible for data persistance in database or communication with seperate services are implemented here.

* Other guidelines
In application StyleCop is used to keep same code quality and code design (spacings, empty lines and so on). StyleCop rules must be met, otherwise the application won't compile.

### Contact ###

* Repository owner
Owner of this repository is Miłosz Wieczorek, contact: manfred666@gmail.com

* Technical 
Technical supervision is maintained by Miłosz Wieczorek, contact: manfred666@gmail.com