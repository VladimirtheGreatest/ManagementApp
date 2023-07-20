# Translator Management API and Frontend 🦜
This repository contains a Translator Management API built with ASP.NET Core and a simple ugliest possible frontend built with React and Typescript. The goal of this project is to manage translators and their jobs. The API allows users to create and manage translators and jobs, while the frontend provides a simple management of translators.

## Features
Refactored Code: The code has been refactored to improve readability, maintainability, and extensibility. The API is built with a focus on production quality(even though I dont think it is still ready) and follow best practices for software architecture.

## RESTful API Design:
The API is designed following RESTful principles, providing endpoints to create, read, update, translators and jobs(no delete requirements).

## Unit Tests
You can run then by clicking options Test in Visual Studio and => Run All Tests.
The API and services are thoroughly tested using Xunit, Moq, and FluentAssertions. The tests cover the important parts of the application, ensuring reliability and correctness.

## Backend Architecture
The backend is structured with separate projects for API, services, repository, and models:

TranslatorManagement.API: Contains the API controllers and mapping configurations. Implemented some custom generic middle ware so we caught any exceptions instead of trycatching everything individually. Exceptions were not defined in requirements so using some generic ones.

TranslatorManagement.Services: Contains business logic and service classes to handle translator and job operations.

TranslatorManagement.Data: Contains data access, entity models, migrations and EF config.

TranslatorManagement.Repository: Contains logic and repository classes to interact with the database. Sorry I changed SqlLite to SqlServer so can play with data.

TranslatorManagement.Repository.Contracts: Interfaces for repository.

TranslatorManagement.Tests: Contains unit tests for the API and services.

## Frontend
The frontend is built with React and Typescript, providing a simple user interface to interact with the API.

## Additional Features (Optional)
1.Track Translator Assignments: Endpoints have been designed to allow tracking translator assignments for jobs.  AssignTranslatorJob, GetTranslatorJobsById endpoints, also designed the database tables with 1 to many relationship for translator => translatorjobs.
2. Also only certified translators will be able to do the jobs.

## Running the API Application
To use this project, you must first clone or download this repository.

After downloading, open the solution in Visual Studio and set the startup project to TranslationManagement.Api. You will need to ensure that .NET6(I updated to.NET6 because it was .NET5 and thats deprecated) and SQL Server Express LocalDB are installed on your machine. If you do not have these installed, you can download it here https://dotnet.microsoft.com/en-us/download/dotnet/6.0, https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
Next, open the Package Manager Console in Visual Studio and run the command 'update-database' to create the local database.(Your package source set to all AND Default project set to TranslationManagement.Data, thats EF tools you have)
Run the application.The API schema and documentation are available through Swagger. After starting the application, it will navigate to url/swagger to access the Swagger UI. From here, you can explore the available endpoints and interact with the API directly, otherwise you can use tools such as Postman.

## Running th Application Frontend: 
Navigate to the ClientApp => translator-client folder and run npm install to install the required dependencies. Then run npm start to launch the frontend. The frontend will be available at http://localhost:3000. Remember if you want to run the front-end, first start the backend.

##
Overall, I really enjoy building this project, i think this application is ready to scale and support new features as needed.