FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app
COPY . ./

RUN dotnet restore QuizService.sln && dotnet publish QuizService.sln -c Release -o obj/Docker/publish


FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/QuizService/obj/Docker/publish .

ENTRYPOINT ["dotnet", "QuizService.dll"]
