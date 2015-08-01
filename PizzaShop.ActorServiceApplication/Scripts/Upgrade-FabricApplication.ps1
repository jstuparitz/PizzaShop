Param
(
    [String]
    $ApplicationPackagePath,

    [Switch]
    $DoNotCreateApplication,

    [Hashtable]
    $ApplicationParameter
)

$LocalFolder = (Split-Path $MyInvocation.MyCommand.Path)

$UtilitiesModulePath = "$LocalFolder\Utilities.psm1"
Import-Module $UtilitiesModulePath

if (!$ApplicationPackagePath)
{
    $ApplicationPackagePath = "$LocalFolder\..\pkg\Release"
}

$PathToImageStore = "PizzaShop\001"

Copy-ServiceFabricApplicationPackage -ApplicationPackagePath $ApplicationPackagePath -ImageStoreConnectionString fabric:ImageStore -ApplicationPackagePathInImageStore $PathToImageStore

Register-ServiceFabricApplicationType -ApplicationPathInImageStore $PathToImageStore

Start-ServiceFabricApplicationUpgrade -ApplicationName fabric:/PizzaShop -ApplicationTypeVersion 1.0.0.1 -HealthCheckStableDurationSec 60 -UpgradeDomainTimeoutSec 1200 -UpgradeTimeout 3000 -FailureAction Rollback -Monitored
