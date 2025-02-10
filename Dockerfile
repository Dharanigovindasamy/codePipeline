# Use the official .NET SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the app and publish it
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build-env image
COPY --from=build-env /app/out ./

# Expose the ports you want to use (for HTTP and HTTPS)
EXPOSE 80 5005

# Environment variable for ASP.NET Core to listen on all IP addresses in the container
#ENV ASPNETCORE_URLS=http://+:80;https://+:443

#docker build -t dotnet-app .
#docker run -d -p 5005:5005 --name dotnet-container dotnet-app
#docker logs dotnet-container

# Command to run the application
ENTRYPOINT ["dotnet", "ecs-fargate.dll"]
