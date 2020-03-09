FROM mcr.microsoft.com/dotnet/core/runtime:3.1

COPY publish/ app/

ENTRYPOINT ["dotnet", "app/CastZone.Api.dll"]
