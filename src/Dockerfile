FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Diary.API/E-Diary.API.csproj", "E-Diary.API/"]
COPY ["E-Diary.Infrastructure.Persistence/E-Diary.Infrastructure.Persistence.csproj", "E-Diary.Infrastructure.Persistence/"]
COPY ["E-Diary.Infrastructure.PubSub/E-Diary.Infrastructure.PubSub.csproj", "E-Diary.Infrastructure.PubSub/"]
COPY ["E-Diary.Core/E-Diary.Core.csproj", "E-Diary.Core/"]
COPY ["E-Diary.Domain/E-Diary.Domain.csproj", "E-Diary.Domain/"]
COPY ["E-Diary.Utils/E-Diary.Utils.csproj", "E-Diary.Utils/"]
RUN dotnet restore "E-Diary.API/E-Diary.API.csproj"
COPY . .
WORKDIR "/src/E-Diary.API"
RUN dotnet build "E-Diary.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Diary.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Diary.API.dll"]