# This creates a service principal
# Once service principal is created, this returns relevant details for service principal.
Param(
    [string] [Parameter(Mandatory=$true)] $SubscriptionId,
    [string] [Parameter(Mandatory=$true)] $DisplayName,
    [string] [Parameter(Mandatory=$true)] $HomePage,
    [string] [Parameter(Mandatory=$true)] $IdentifierUris,
    [string] [Parameter(Mandatory=$true)] $Password,
    [string] [Parameter(Mandatory=$true)] $RoleDefinitionName = "Contributor"
)

# Login
Write-Host "Verifying credentials ..." -ForegroundColor Green

$msg = Login-AzureRmAccount -SubscriptionId $SubscriptionId

Write-Host "Credentials verified" -ForegroundColor Green

# Create AAD application
Write-Host "Creating Azure AD application ..." -ForegroundColor Green

$exists = Get-AzureRmADApplication -IdentifierUri $IdentifierUris
if ($exists -ne $null)
{
    Write-Host "Azure AD application already exists. Try a different name." -ForegroundColor Red

    # Release
    Remove-Variable msg
    Remove-Variable exists
    return
}

$app = New-AzureRmADApplication -DisplayName $DisplayName -HomePage $HomePage -IdentifierUris $IdentifierUris -Password $Password
#$app

Write-Host "Azure AD application created" -ForegroundColor Green

# Create service principal
Write-Host "Creating service principal ..." -ForegroundColor Green

$exists = Get-AzureRmADServicePrincipal -ServicePrincipalName $IdentifierUris
if ($exists -ne $null)
{
    Write-Host "Service principal already exists." -ForegroundColor Red

    # Release
    Remove-Variable msg
    Remove-Variable exists
    return
}

$sp = New-AzureRmADServicePrincipal -ApplicationId $app.ApplicationId
#$sp

Write-Host "Service principal created" -ForegroundColor Green

# Intentional delay for 10 secs
Start-Sleep -Seconds 10

# Assign AAD roles to service principal
Write-Host "Assigning Azure AD roles ..." -ForegroundColor Green

$exists = Get-AzureRmRoleAssignment -ServicePrincipalName $sp.ServicePrincipalName -ErrorVariable ex -ErrorAction SilentlyContinue
if ($exists -ne $null)
{
    Write-Host "Service principal already exists." -ForegroundColor Red

    # Release
    Remove-Variable msg
    Remove-Variable exists
    Remove-Variable ex
    return
}

$role = New-AzureRmRoleAssignment -ServicePrincipalName $sp.ServicePrincipalName -RoleDefinitionName $RoleDefinitionName
#$role

# Return
$result = @{ DisplayName = $DisplayName; IdentifierUris = $IdentifierUris; RoleDefinitionName = $RoleDefinitionName }

Write-Host "Azure AD role assigned" -ForegroundColor Green

$result

# Release
Remove-Variable msg
Remove-Variable exists
Remove-Variable ex
Remove-Variable sp
Remove-Variable role
Remove-Variable result
