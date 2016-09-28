# This uploads ARM linked templates to storage account

Param(
    [string] [Parameter(Mandatory=$true)] $SubscriptionId,
    [string] [Parameter(Mandatory=$true)] $ResourceGroupName,
    [string] [Parameter(Mandatory=$true)] $ProjectName,
    [string] [Parameter(Mandatory=$true)] $Environment,
    [string] [Parameter(Mandatory=$true)] $ResourcePath,
    [string] [Parameter(Mandatory=$true)] $StorageAccountName,
    [string] [Parameter(Mandatory=$false)] $ContainerName = "templates",
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

$msg = Set-AzureRmCurrentStorageAccount -ResourceGroupName $ResourceGroupName -Name $StorageAccountName

# Templates
$templates = Get-ChildItem .\src\$ResourcePath\Templates\*.json -Exclude ("base-*.json", "master-*.json", "*.params.json")
foreach($template in $templates)
{
    $filePath = $template.FullName
    $fileName = $template.Name

    Write-Host "Uploading $fileName to Azure Storage Account ..." -ForegroundColor Green

    $msg = Set-AzureStorageBlobContent -Container $ContainerName -File $filePath -Force

    Write-Host "$fileName uploaded" -ForegroundColor Green
}

# Parameters
$segments = $ResourceGroupName.Split("-")
$prjName = $ProjectName
$envName = $Environment

$templates = Get-ChildItem .\src\$ResourcePath\Templates\*.params.json -Exclude ("base-*.json", "master-*.json")
foreach($template in $templates)
{
    $filePath = $template.FullName
    $fileName = $template.Name

    $json = Get-Content -Path $filePath | ConvertFrom-Json
    $json.parameters | Add-Member -MemberType NoteProperty -Name projectName -Value @{ value = $prjName }
    $json.parameters | Add-Member -MemberType NoteProperty -Name environment -Value @{ value = $envName }

    $json | ConvertTo-Json -Depth 99 | Out-File -FilePath $filePath -Encoding utf8

    Write-Host "Uploading $fileName to Azure Storage Account ..." -ForegroundColor Green

    $msg = Set-AzureStorageBlobContent -Container $ContainerName -File $filePath -Force

    Write-Host "$fileName uploaded" -ForegroundColor Green
}

Remove-Variable segments
Remove-Variable prjName
Remove-Variable envName
Remove-Variable json
Remove-Variable templates
Remove-Variable template
Remove-Variable filePath
Remove-Variable fileName
Remove-Variable msg
