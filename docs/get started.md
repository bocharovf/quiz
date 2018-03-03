# Get started

## Requirements
To work with application you should have:
* Visual Studio 2017 edition of your choice.
* Angular 5 development environment configured. Follow [Angular 5 quickstart](https://angular.io/guide/quickstart).
* Instance of PostgreSQL. Here we will use Docker PostgreSQL image for that.
* Docker - optionally, if you want to run application in production mode or run PostgreSQL in container.

## Run in prod mode
The simplest way to run application is via Docker. Run following command in **src** folder:
```bash
docker-compose -f docker-compose.yml up
```
It will pull latest images from Docker Hub and start all components in the single network. Application will be available on 80 port on your local machine - http://localhost:80.

## Build
Open and build **src/QuizService/QuizService.sln** in Visual Studio.

Install package dependencies running following commands in **src/QuizClient** folder:
```bash
npm install
npm build
```

## Debug
In production application works inside Docker containers. But for debug and development purposes it's easer to start each component separately without Docker. See below instructions for component.

### Database
QuizService depends on PostgreSQL instance. To configure it check the **DatabaseConnectionString** setting in **src/QuizService/QuizService/appsettings.json**. You could replace host name with your PostgreSQL instance host name.

Alternatively you could run PostgresSQL as docker container. To do so setup host alias **database** on your development machine to point localhost (in C:\Windows\System32\drivers\etc\hosts):
```
127.0.0.1 database
```
And use Docker PostgreSQL image to run database locally:
```bash
docker run -d -p 5432:5432 postgres
```

Database named **quiz** will be created and populated automatically on first QuizServce start.

### QuizService
Run **src/QuizService/QuizService.sln** in debug mode. It will start service on IIS development server on http://localhost:56712.

### QuizClient
Execute following command in **src/QuizClient** folder:
```bash
ng serve
```
It will start development server on http://localhost:4200.

**Note:** by default client application in debug mode expects to communicate with service on port 56712. You could change it in **src/QuizClient/src/environments/environment.ts**.

## Test
To run QuizService tests through Visual Studio - open Test Explorer and run required tests.
To run QuizService tests through console execute following command in **src/QuizService** folder
```bash
dontet test
```
Use Angular CLI to run QuizClient tests. Launch following command in **src/QuizClient** folder
``` bash
ng test
```
**Note:** tests are executed in both Chrome browser and PhantomJS, so you should have both installed.

## Build Docker images
You could build docker images by running following command in **src** folder:
```bash
docker-compose -f docker-compose.yml -f docker-compose.dev.yml build
```
It will build QuizService and QuizClient images in production mode.