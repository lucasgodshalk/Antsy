dotnet pack -c Release --output nupkgs

nuget push nupkgs/antsy.<version>.nupkg -s https://www.nuget.org/api/v2/package