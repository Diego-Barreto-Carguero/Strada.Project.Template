FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /sln

ENV appname=StradaTempalte
ENV manifestpath=StradaTempalte
ENV dockerfile=Dockerfile

COPY ./*.sln ./NuGet.Config ./

COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done


RUN dotnet restore --configfile ./NuGet.Config
COPY . .
WORKDIR /sln/src/Strada.Templates.WebApi
RUN dotnet publish --configuration Release --output /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
ARG APP_VERSION=1.0.0
ENV APP_VERSION=$APP_VERSION
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "./Strada.Templates.WebApi.dll"]
