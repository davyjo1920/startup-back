FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env
WORKDIR /App
ENV PORT=5256
ENV ASPNETCORE_URLS=http://+:5256

# RUN dotnet tool install --global dotnet-ef

# ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish  -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
EXPOSE 5256
WORKDIR /App
COPY --from=build-env /App/out .

ENV ASPNETCORE_URLS=http://+:5256

ENTRYPOINT ["dotnet", "TodoApi.dll"]