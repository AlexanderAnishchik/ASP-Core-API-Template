FROM microsoft/dotnet:2.0.5-sdk-2.1.4 AS base
WORKDIR /app
EXPOSE 3452

FROM microsoft/dotnet:2.0.5-sdk-2.1.4 AS build
WORKDIR /src
COPY . .
RUN dotnet restore -nowarn:msb3202,nu1503
WORKDIR /src/Project.WebAPI
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Project.WebAPI.dll"]