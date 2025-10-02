# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos .csproj primeiro para aproveitar o cache de camadas
COPY Quadra.Api/Quadra.Api.csproj ./Quadra.Api/
COPY Core/*.csproj ./Core/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Services/*.csproj ./Services/

# Restaura os pacotes NuGet
RUN dotnet restore "Quadra.Api/Quadra.Api.csproj"

# Copia todo o resto do código
COPY . .

# Build do projeto
WORKDIR "/src/Quadra.Api"
RUN dotnet build "Quadra.Api.csproj" -c Release -o /app/build

# Publicação
RUN dotnet publish "Quadra.Api.csproj" -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Quadra.Api.dll"]