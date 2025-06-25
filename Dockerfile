# Используем официальный .NET SDK для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем весь проект и публикуем в Release режиме в папку out
COPY . ./
RUN dotnet publish -c Release -o out

# Используем официальный ASP.NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out ./

# Запускаем приложение
ENTRYPOINT ["dotnet", "AleksGeoToursReviewsApi.dll"]
