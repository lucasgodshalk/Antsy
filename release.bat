dotnet pack -c Release --output nupkgs

nuget push nupkgs/antsy.<version>.nupkg -source https://www.nuget.org/api/v2/package