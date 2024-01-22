#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Relias.{{cookiecutter.solution_name}}.Cqrs.Api/Relias.{{cookiecutter.solution_name}}.Cqrs.Api.csproj", "src/Relias.{{cookiecutter.solution_name}}.Cqrs.Api/"]
COPY ["src/Relias.{{cookiecutter.solution_name}}.Cqrs.App/Relias.{{cookiecutter.solution_name}}.Cqrs.App.csproj", "src/Relias.{{cookiecutter.solution_name}}.Cqrs.App/"]
COPY ["src/Relias.{{cookiecutter.solution_name}}.Cqrs.Infra/Relias.{{cookiecutter.solution_name}}.Cqrs.Infra.csproj", "src/Relias.{{cookiecutter.solution_name}}.Cqrs.Infra/"]
COPY ["src/Relias.{{cookiecutter.solution_name}}.Cqrs.Domain/Relias.{{cookiecutter.solution_name}}.Cqrs.Domain.csproj", "src/Relias.{{cookiecutter.solution_name}}.Cqrs.Domain/"]
COPY ["src/Relias.{{cookiecutter.solution_name}}.Common/Relias.{{cookiecutter.solution_name}}.Common.csproj", "src/Relias.{{cookiecutter.solution_name}}.Common/"]
RUN dotnet restore "src/Relias.{{cookiecutter.solution_name}}.Cqrs.Api/Relias.{{cookiecutter.solution_name}}.Cqrs.Api.csproj"
COPY . .
WORKDIR "/src/src/Relias.{{cookiecutter.solution_name}}.Cqrs.Api"
RUN dotnet build "Relias.{{cookiecutter.solution_name}}.Cqrs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Relias.{{cookiecutter.solution_name}}.Cqrs.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Relias.{{cookiecutter.solution_name}}.Cqrs.Api.dll"]
