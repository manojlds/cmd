param(
    $tasks = @('Clean', 'Compile', 'UnitTests', 'NugetPackage'), 
    $buildNumber = "1.0.0",
    $buildMode = "Release"
    )

$script:scriptDir = split-path $MyInvocation.MyCommand.Path -parent

#Import-Module $script:scriptDir\BuildScripts\YDeliver.psm1
Invoke-YBuild $tasks -buildVersion $buildNumber -config @{ "conventions" = @{ "buildMode" = $buildMode } }