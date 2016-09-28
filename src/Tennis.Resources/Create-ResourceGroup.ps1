# This creates an Azure Resource Group based on project name, environment and location.
# Once Azure resource group is created, this returns the resource group name created.
Param(
    [string] [Parameter(Mandatory=$true)] $SubscriptionId,
    [string] [Parameter(Mandatory=$false)] $Name = "app-rg-dev",
    [string] [Parameter(Mandatory=$false)] $Location = "Australia Southeast",
    [switch] $UseServicePrincipal,
    [string] [Parameter(Mandatory=$false)] $TenantId,
    [string] [Parameter(Mandatory=$false)] $ServicePrincipalName,
    [string] [Parameter(Mandatory=$false)] $Password
)

# Login
Write-Host "Verifying credentials ..." -ForegroundColor Green

if ($UseServicePrincipal -eq $true)
{
    $username = $ServicePrincipalName
    $securepassword = ConvertTo-SecureString $Password -AsPlainText -Force
    $credential = New-Object PSCredential -ArgumentList $username, $securepassword

    $msg = Login-AzureRmAccount -SubscriptionId $SubscriptionId -ServicePrincipal -TenantId $TenantId -Credential $credential

    Remove-Variable username
    Remove-Variable securepassword
    Remove-Variable credential
}
else
{
    $msg = Login-AzureRmAccount -SubscriptionId $SubscriptionId
}

Write-Host "Credentials verified" -ForegroundColor Green

# Resource Group
$rg = @{ Name = $Name.ToLowerInvariant(); Location = $Location }

Write-Host "Creating Azure Resource Group ..." -ForegroundColor Green

$exists = Get-AzureRmResourceGroup -Name $rg.Name -Location $rg.Location -ErrorVariable ex -ErrorAction SilentlyContinue
if ($exists -eq $null)
{
    $msg = New-AzureRmResourceGroup -Name $rg.Name -Location $rg.Location
}

# Return
$rg

Write-Host "Azure Resource Group created" -ForegroundColor Green

# Release
Remove-Variable msg
Remove-Variable exists
Remove-Variable ex
Remove-Variable rg
