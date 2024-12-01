FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG TARGETARCH
WORKDIR /src

# copy everything and build app
COPY BibleReadings2/. ./BibleReadings2/
COPY BibleReadings2.Repository/. ./BibleReadings2.Repository/
COPY BibleReadings2.Repository.Json/. ./BibleReadings2.Repository.Json/

RUN dotnet publish -a $TARGETARCH BibleReadings2/BibleReadings2.csproj -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /src/out ./
USER $APP_UID

ENTRYPOINT ["./BibleReadings2"]
