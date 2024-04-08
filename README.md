
# RihalCodeStacker-Backend

This Repository is related to the Rihal Backend Challenge, where I put my server-side development code.

### Intro

First of all, I want to say my sincere gratitude goes out to the **Rihal team** for developing and expanding software engineering knowledge and culture. In the tech community, your efforts foster growth and learning.

### My code

It was my goal to implement this challenge in Clean Architecture, CQRS, and also to write a practical test case as a concept. Fluent validation was used to validate inputs for validation. This project is docketized and you can use it by running Docker compose up.

### Track My development

On the issue tab, you can see the **25 issues** I created for this project. Most of those are closed, while others are open. Additionally, each issue was developed on its own branch, and each branch was then merged into the develop branch via **Pull Request**, which are possible in PR, however I was unable to complete the CI/CD process because of time constraints.

### How to run

 - install Visual Studio 2022 latest, or .Net 8 SDK
 - install PostgreSQL
 - set connection string into the appsettings.Development.json
 - set web layer as Startup Project
 - Run project with F5
 
 ### Architecture Physiology

 - Domain: All the entities are in this layer (Movie, Memory, ...)
 - Application: All of the Application logics are in to this project (Get Memory, Create Movie, Update Photos)
 - Infrastructure: All of the Infrastructure and Third-party services are in this layer, Identity, EF, Text Service, Cloud Service
 - Web: In this layer you can found Minimal API (End points)

### Test case

you can go to the tests part and run each test that you want, by running the functional test, you can do CRUD process on a real data base, for testing **Url Eextractor** and **UnscrambleMovie** check **Infrastructure.IntegrationTests** layer.

### Technologies

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) &  [Respawn](https://github.com/jbogard/Respawn)
