FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY microwave-benner.Server/microwave-benner.Server.csproj ./microwave-benner.Server/
COPY microwave-benner.Application/microwave-benner.Application.csproj ./microwave-benner.Application/
COPY microwave-benner.Domain/microwave-benner.Domain.csproj ./microwave-benner.Domain/
COPY microwave-benner.Infra.Data/microwave-benner.Infra.Data.csproj ./microwave-benner.Infra.Data/
COPY microwave-benner.Infra.Ioc/microwave-benner.Infra.Ioc.csproj ./microwave-benner.Infra.Ioc/

RUN dotnet restore ./microwave-benner.Server/microwave-benner.Server.csproj

COPY microwave-benner.Server/ ./microwave-benner.Server/
COPY microwave-benner.Application/ ./microwave-benner.Application/
COPY microwave-benner.Domain/ ./microwave-benner.Domain/
COPY microwave-benner.Infra.Data/ ./microwave-benner.Infra.Data/
COPY microwave-benner.Infra.Ioc/ ./microwave-benner.Infra.Ioc/

RUN dotnet build ./microwave-benner.Server/microwave-benner.Server.csproj -c Release

RUN dotnet publish ./microwave-benner.Server/microwave-benner.Server.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "microwave-benner.Server.dll"]
