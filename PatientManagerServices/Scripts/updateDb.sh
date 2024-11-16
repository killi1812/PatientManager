#!/bin/bash
/home/fran/.dotnet/tools/dotnet-ef database update --project PatientManagerServices/PatientManagerServices.csproj --startup-project PatientManagerApp/PatientManagerApp.csproj --context PatientManagerServices.Models.PmDbContext --configuration Debug --framework net7.0 

/home/fran/.dotnet/tools/dotnet-ef migrations add --project PatientManagerServices/PatientManagerServices.csproj --startup-project PatientManagerApp/PatientManagerApp.csproj --context PatientManagerServices.Models.PmDbContext --configuration Debug --framework net7.0 {{name}} --output-dir Migrations
/home/fran/.dotnet/tools/dotnet-ef migrations remove --project PatientManagerServices/PatientManagerServices.csproj --startup-project PatientManagerApp/PatientManagerApp.csproj --context PatientManagerServices.Models.PmDbContext --configuration Debug --framework net7.0 
