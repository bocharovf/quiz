[![Build Status](https://bocharov-f.visualstudio.com/quiz/_apis/build/status/Build%20DEV%20branch?branchName=dev)](https://bocharov-f.visualstudio.com/quiz/_build/latest?definitionId=4&branchName=dev)

# Quiz

### The platform to create, share and pass quizzes.

Fully functional demo application with Docker support written with ASP.NET Core 2.0 and Angular 5.

The main project goals are:
* demonstrate my skills and knowledge
* write clean code and do everything qualitatively
* play with all that new and awesome stuff I am interested in

## Status
Under active development. Try it yourself on [quiz.bocharovf.ru](http://quiz.bocharovf.ru)

## Documentation

Application consists of several components:
* **QuizService** - ASP.NET Core 2.0 Web Api application. Represents web service which handles most part of business logic. See [QuizService documentation](src/QuizService/README.md) for details.
* **QuizClient** - Angular 5 single page application to interact with user. See [QuizClient documentation](src/QuizClient/README.md) for details.
* **Database** - PostgreSQL database to store application data.

Details about CI/CD process could be found in [continuous integration and delivery](docs/continuous%20integration%20and%20delivery.md) page.

See the [get started](docs/get%20started.md) guide to build, test and run application quickly.
