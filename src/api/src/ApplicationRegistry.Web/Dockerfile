#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/api/src/ApplicationRegistry.Web/ApplicationRegistry.Web.csproj", "src/api/src/ApplicationRegistry.Web/"]
COPY ["src/api/src/ApplicationRegistry.Domain/ApplicationRegistry.Domain.csproj", "src/api/src/ApplicationRegistry.Domain/"]
COPY ["src/api/src/ApplicationRegistry.Tools.FileHelpers/ApplicationRegistry.Tools.FileHelpers.csproj", "src/api/src/ApplicationRegistry.Tools.FileHelpers/"]
COPY ["src/api/src/ApplicationRegistry.Infrastructure/ApplicationRegistry.Infrastructure.csproj", "src/api/src/ApplicationRegistry.Infrastructure/"]
COPY ["src/api/src/ApplicationRegistry.Application/ApplicationRegistry.Application.csproj", "src/api/src/ApplicationRegistry.Application/"]
COPY ["src/api/src/ApplicationRegistry.CQRS.Abstraction/ApplicationRegistry.CQRS.Abstraction.csproj", "src/api/src/ApplicationRegistry.CQRS.Abstraction/"]
COPY ["src/api/src/ApplicationRegistry.Tools.Cryptography/ApplicationRegistry.Tools.Cryptography.csproj", "src/api/src/ApplicationRegistry.Tools.Cryptography/"]
COPY ["src/api/src/ApplicationRegistry.Tools.Graph/ApplicationRegistry.Tools.Graph.csproj", "src/api/src/ApplicationRegistry.Tools.Graph/"]
RUN dotnet restore "src/api/src/ApplicationRegistry.Web/ApplicationRegistry.Web.csproj"
COPY . .
WORKDIR "/src/src/api/src/ApplicationRegistry.Web"
RUN dotnet build "ApplicationRegistry.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApplicationRegistry.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApplicationRegistry.Web.dll"]