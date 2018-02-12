FROM microsoft/aspnetcore-build:2.0 AS build-env
SHELL ["/bin/bash", "-c"]

WORKDIR /app
COPY . ./

RUN dotnet restore QuizService.sln && dotnet publish QuizService.sln -c Release -o obj/Docker/publish


FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/QuizService/obj/Docker/publish .

ENTRYPOINT ["dotnet", "QuizService.dll"]
