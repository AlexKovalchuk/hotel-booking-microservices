# 1
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
  # 2
WORKDIR /app
  # 3
EXPOSE 8080
  
  # 4
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
  # 5
WORKDIR /src
  # 6
COPY ["Booking.Api/Booking.Api.csproj", "Booking.Api/"]
  # 7
RUN dotnet restore "Booking.Api/Booking.Api.csproj"
  
  # 8
COPY . .
  # 9
WORKDIR "/src/Booking.Api"
  # 10
RUN dotnet build "Booking.Api.csproj" -c Release -o /app/build
  
  # 11
FROM build AS publish
  # 12
RUN dotnet publish "Booking.Api.csproj" -c Release -o /app/publish
  
  # 13
FROM base AS final
  # 14
WORKDIR /app
  # 15
COPY --from=publish /app/publish .
  # 16
ENTRYPOINT ["dotnet", "Booking.Api.dll"]
