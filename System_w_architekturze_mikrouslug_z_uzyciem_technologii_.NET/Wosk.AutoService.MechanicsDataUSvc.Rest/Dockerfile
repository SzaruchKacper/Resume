#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Wosk.AutoService.MechanicsDataUSvc.Rest/Wosk.AutoService.MechanicsDataUSvc.Rest.csproj", "Wosk.AutoService.MechanicsDataUSvc.Rest/"]
COPY ["Wosk.AutoService.MechanicsDataUSvc.Logic/Wosk.AutoService.MechanicsDataUSvc.Logic.csproj", "Wosk.AutoService.MechanicsDataUSvc.Logic/"]
COPY ["Wosk.AutoService.MechanicsDataUSvc.Model/Wosk.AutoService.MechanicsDataUSvc.Model.csproj", "Wosk.AutoService.MechanicsDataUSvc.Model/"]
COPY ["Wosk.AutoService.MechanicsDataUSvc.Rest.Model/Wosk.AutoService.MechanicsDataUSvc.Rest.Model.csproj", "Wosk.AutoService.MechanicsDataUSvc.Rest.Model/"]
RUN dotnet restore "Wosk.AutoService.MechanicsDataUSvc.Rest/Wosk.AutoService.MechanicsDataUSvc.Rest.csproj"
COPY . .
WORKDIR "/src/Wosk.AutoService.MechanicsDataUSvc.Rest"
RUN dotnet build "Wosk.AutoService.MechanicsDataUSvc.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wosk.AutoService.MechanicsDataUSvc.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wosk.AutoService.MechanicsDataUSvc.Rest.dll"]