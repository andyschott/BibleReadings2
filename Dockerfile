FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# copy everything and build app
COPY BibleReadings2/. ./BibleReadings2/
COPY BibleReadings2.Repository/. ./BibleReadings2.Repository/
COPY BibleReadings2.Repository.Json/. ./BibleReadings2.Repository.Json/

WORKDIR /src
RUN dotnet publish BibleReadings2/BibleReadings2.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /src/out ./
ENTRYPOINT ["dotnet", "BibleReadings2.dll"]
