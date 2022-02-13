#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Diary.UI/E-Diary.UI.csproj", "E-Diary.UI/"]
RUN dotnet restore "E-Diary.UI/E-Diary.UI.csproj"
COPY . .
WORKDIR "/src/E-Diary.UI"
RUN dotnet build "E-Diary.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Diary.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Diary.UI.dll"]