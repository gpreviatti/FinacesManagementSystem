##########
# STAGE 1
##########
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /app

# Copy files
COPY . .

# Run restore and publish
RUN dotnet restore "./Web/Web.csproj"  --force --no-cache \
 && dotnet publish "./Web/Web.csproj" -c Release -o /app/publish

###########
# STAGE 2
###########
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
ENV TZ=America/Sao_Paulo
WORKDIR /app

RUN apk add tzdata \
 && ln -sf /usr/share/zoneinfo/${TZ} /etc/localtime \
 && echo "${TZ}" > /etc/timezone

COPY --from=build /app/publish .

# Use the following instead for Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Web.dll
