# Запуск базы
docker run --name some-sostgres -p 5432:5432 -e POSTGRES_PASSWORD=mysecretpassword -d postgres

# manual app launch
dotnet ef database update\
dotnet run

# docker app launch
docker build -t dotnet-app .
docker run -p 5256:5256 --name dotnet-app-cotnainer dotnet-app:latest
