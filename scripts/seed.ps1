$envContent = Get-Content ".env"

foreach ($line in $envContent) {
    if ($line -match "^\s*#") {
        continue
    }

    if ($line -match "=") {
        $name, $value = $line -split "=", 2
        [System.Environment]::SetEnvironmentVariable(
            $name.Trim(),
            $value.Trim()
        )
    }
}

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

Write-Host "`nINICIANDO SEED PIPELINE"

$ctx = @{
    BaseUrl = "http://localhost:$env:APP_PORT"
    Users = @()
    Branches = @()
    StaffUsers = @()
}

$org = Join-Path $PSScriptRoot "seed.organization.ps1"
$id  = Join-Path $PSScriptRoot "seed.identity.ps1"
$st  = Join-Path $PSScriptRoot "seed.staff.ps1"

if (!(Test-Path $org)) { throw "Missing: $org" }
if (!(Test-Path $id))  { throw "Missing: $id" }
if (!(Test-Path $st))  { throw "Missing: $st" }

$ctx = & $org $ctx
$ctx = & $id  $ctx
$ctx = & $st  $ctx

Write-Host "`nSEED COMPLETADO"