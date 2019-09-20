FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY btcsignal-webservice.csproj ./
RUN dotnet restore "./btcsignal-webservice.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "btcsignal-webservice.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "btcsignal-webservice.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "btcsignal-webservice.dll"]
