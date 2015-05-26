# psake Build Script for SnapDb

# To build, run psake (https://github.com/psake/psake) in a PowerShell window. This will build and unit test both DEBUG and 
# RELEASE configurations. You can also run a specific build task: psake {TaskName}


properties {
	$solutionName = "OpenStates-CS"
	
	$baseDir = Resolve-Path .
	$slnFile = "$baseDir\$solutionName.sln"
	
	$nunitDir = ".\packages\NUnit.Runners.2.6.4\tools"
	$openCoverDir = ".\packages\OpenCover.4.5.3723"
	$reportGeneratorDir = ".\packages\ReportGenerator.2.1.4.0"

    $defaultConfig = "Release"
}

Task default -depends Build, Release

Task Build {
	Compile "Debug"
	UnitTest "Debug"
	echo "Build Complete"
}

Task Release {
    Compile "Release"
	UnitTest "Release"
    echo "Release Complete"
}

Task ReleaseBuild {
    Compile "Release"
}

Task NuGetPack -depends ReleaseBuild{
	echo "Nuget Pack Started"
	NuGetPackCsproj .\OpenStatesApi
}

Task CodeCoverage {
    # Run OpenCover
    md "$baseDir\CodeCoverage" -Force
    $user = $env:username
    cmd /c "$openCoverDir\OpenCover.Console.exe -target:$nunitDir\nunit-console-x86.exe -targetargs:`"OpenStatesApi.Tests.dll /noshadow /exclude:`"Spike`" `" -targetdir:.\OpenStatesApi.Tests\bin\Release -output:.\CodeCoverage\results.unit.xml -filter:`"+[OpenStatesApi]* -[OpenStatesApi]*Exception`" -register:user"
    # Run ReportGenerator
    exec { & $reportGeneratorDir\ReportGenerator.exe -reports:"$baseDir\CodeCoverage\Results.*.xml" -targetdir:$baseDir\CodeCoverage -reporttypes:Html }
}


function Compile( $buildConfig ) {
	# Install solution-level NuGet packages
	.\.nuget\nuget.exe install .\.nuget\packages.config -o packages
	# Clean
	exec { msbuild /ds $slnFile /p:Configuration=$buildConfig /p:VisualStudioVersion=12.0 /t:Clean }
	# Build
	exec { msbuild /ds $slnFile /p:Configuration=$buildConfig /p:VisualStudioVersion=12.0 }
}

function UnitTest( $test_configuration ) {
	$testAssemblies = (Get-ChildItem "$baseDir" "*Tests.dll" -Recurse -Name | Select-String "bin\\$test_configuration")
	echo "Test Assemblies: $testAssemblies"
	foreach($test_asm_name in $testAssemblies) {
		exec { & $nunitDir\nunit-console-x86.exe $baseDir\$test_asm_name /nodots /labels /exclude:"Spike" }
	}
}

function NuGetPackCsproj($project_dir) {
	$csproj = Get-Item $project_dir\*.csproj | Sort-Object LastWriteTime -Descending | Select-Object -first 1
	.\.nuget\nuget.exe pack $csproj -Prop Configuration=Release
}