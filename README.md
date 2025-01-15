# Запуск бэка фронта и базы
docker-compose up --build

# Запуск компонентов по отдельности
## Запуск базы
docker run --name some-postgres -p 5432:5432 -e POSTGRES_PASSWORD=mysecretpassword -d postgres

## Запуск приложения в докере
docker build -t dotnet-app .
docker run -p 5256:5256 --name dotnet-app-cotnainer dotnet-app:latest

## Ручной запуск приложения
dotnet ef database update
dotnet run

## Для создания новой миграции ##
dotnet ef migrations add InitialCreate 