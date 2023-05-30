#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get update && \
    apt-get install -y curl apt-transport-https
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash - && \
    apt-get install -y nodejs

# FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
# RUN apt-get update
# RUN apt-get install -y curl
# RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
# RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
# RUN apt-get update && \
#     apt-get install -y curl apt-transport-https
# RUN curl -sL https://deb.nodesource.com/setup_14.x | bash - && \
#     apt-get install -y nodejs

WORKDIR /src
COPY ["Wizzi/Wizzi.csproj", "Wizzi/"]
RUN dotnet restore "Wizzi.csproj"
COPY . .
WORKDIR "/src/Wizzi"
RUN dotnet build "Wizzi.csproj" -c Release -o /app/build
RUN npm i -g sass
RUN npm update
# RUN npm audit fix




