cd log4net.Raygun

msbuild log4net.Raygun.csproj /t:Build /p:Configuration="Release 3.5"
msbuild log4net.Raygun.csproj /t:Build /p:Configuration="Release 4.0"
msbuild log4net.Raygun.csproj /t:Build;Package;FixDependencies /p:Configuration="Release 4.5"

cd ..