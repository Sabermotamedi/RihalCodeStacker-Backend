
# RihalCodeStacker-Backend

This Repository is related to the Rihal Backend Challenge, where I put my server-side development code.

Rihal Backend Challenge (https://github.com/rihal-om/rihal-codestacker/tree/main)

### Intro

First of all, I want to say my sincere gratitude goes out to the **Rihal team** for developing and expanding software engineering knowledge and culture. In the tech community, your efforts foster growth and learning.

### My code

It was my objective to tackle this challenge utilizing Clean Architecture, CQRS, and to incorporate practical test cases as a concept. Fluent Validation played a crucial role in ensuring robust input validation.

Moreover, it's worth mentioning that ASP.NET 8 was the chosen language for this implementation. Additionally, the project has been dockerized, allowing for easy deployment and utilization through Docker Compose.

### Track My development

On the issue tab, you can see the **25 issues** I created for this project. Most of those are closed, while others are open. Additionally, each issue was developed on its own branch, and each branch was then merged into the develop branch via **Pull Request**, which are possible in PR, however I was unable to complete the CI/CD process because of time constraints.

 `Additionally, I made sure to document my solution for each unfinished issue as a comment directly on the respective issue.`

  ### How to run on Visual Studio  

- install Visual Studio 2022 latest, or .Net 8 SDK

- install PostgreSQL

- set connection string into the appsettings.Development.json

- set web layer as Startup Project

- Run project with F5

### How to run on Docker

 - Clone the project
 
 - Be sure docker desktop or cli is installed on your machine  
 
 - open CMD on to the project path
 
 -  run this command  =>   `docker-compose --project-name rihalapp  up`
     
 - If you face with any error during the application startup , do the
   command again
 
 ### How to test it?

- after run the application
- application will open automaticly
- swagger url is (https://localhost:5001/api/index.html?url=/api/specification.json)
- go to the User section
- open login url
- set useCookies **false** 
- set useSessionCookies **false** 
- dot need to set twoFactorCode value
- use defult admin Username and Password
  
      Username: administrator@localhost    
      Password: Administrator1!
- Bearer token will generate
- Click on Authorize button and enter you token by this format 
- 
      Bearer GeneretedToken

- go to the Movie part
- **call GET api/movies**
- You will recive all of the movies data 

### Test case

you can go to the tests part and run each test that you want, by running the functional test, you can do CRUD process on a real data base, for testing **Url Eextractor** and **UnscrambleMovie** check **Infrastructure.IntegrationTests** layer.

`Docker is required for tests to pass. Therefore, make sure that Docker Desktop is opened (Windows).`

 ### Architecture Physiology

 - Domain: All the entities are in this layer (Movie, Memory, ...)
 - Application: All of the Application logics are in to this project (Get Memory, Create Movie, Update Photos)
 - Infrastructure: All of the Infrastructure and Third-party services are in this layer, Identity, EF, Text Service, Cloud Service
 - Web: In this layer you can found Minimal API (End points)


### Technologies

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) &  [Respawn](https://github.com/jbogard/Respawn)