#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN  sed -i s@/deb.debian.org/@/mirrors.huaweicloud.com/@g /etc/apt/sources.list
RUN  apt-get clean
RUN  apt-get update
RUN apt-get update && apt-get install -y \
    libopenblas-dev \
    liblapack-dev \
    libx11-6 \
	libgdiplus \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FaceAspnetcore/FaceAspnetcore.csproj", "FaceAspnetcore/"]
RUN dotnet restore "FaceAspnetcore/FaceAspnetcore.csproj"
COPY . .
WORKDIR "/src/FaceAspnetcore"
RUN dotnet build "FaceAspnetcore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FaceAspnetcore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV LD_LIBRARY_PATH=$LD_LIBRARY_PATH:/app
COPY ["Face.Sdk.ArcFace/Sdks/Linuxx64/*.so", "/app"]

#COPY ["Face.Sdk.ArcFace/Sdks/Linuxx64/*.so", "/usr/lib"]

ENTRYPOINT ["dotnet", "FaceAspnetcore.dll"]