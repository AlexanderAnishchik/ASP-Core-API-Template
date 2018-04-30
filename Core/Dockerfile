FROM microsoft/aspnetcore-build:latest AS base
WORKDIR /app

FROM microsoft/aspnetcore-build:latest AS build
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
EXPOSE 80
RUN ls
ENTRYPOINT ["dotnet", "Project.WebAPI.dll"]