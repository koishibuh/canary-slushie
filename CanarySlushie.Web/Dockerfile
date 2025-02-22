FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["CanarySlushie.Web/CanarySlushie.Web.csproj", "CanarySlushie.Web/"]
RUN dotnet restore "./CanarySlushie.Web/CanarySlushie.Web.csproj"
COPY . .
WORKDIR "/src/CanarySlushie.Web"
RUN dotnet build "CanarySlushie.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./CanarySlushie.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CanarySlushie.Web.dll"]