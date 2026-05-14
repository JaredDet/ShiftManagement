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

$BASE_URL = "http://localhost:$env:APP_PORT"

Write-Host "Creando empresas..."

$company1 = Invoke-RestMethod `
    -Method POST `
    -Uri "$BASE_URL/api/companies" `
    -ContentType "application/json" `
    -Body '{
        "name": "Tech Solutions"
    }'

$company2 = Invoke-RestMethod `
    -Method POST `
    -Uri "$BASE_URL/api/companies" `
    -ContentType "application/json" `
    -Body '{
        "name": "Retail Group"
    }'

Write-Host "Empresa 1:" $company1.id
Write-Host "Empresa 2:" $company2.id

Write-Host "Creando sucursales..."

Invoke-RestMethod `
    -Method POST `
    -Uri "$BASE_URL/api/branches" `
    -ContentType "application/json" `
    -Body "{
        `"companyId`": `"$($company1.id)`",
        `"name`": `"Sucursal Centro`",
        `"address`": `"Av. Principal 123`"
    }"

Invoke-RestMethod `
    -Method POST `
    -Uri "$BASE_URL/api/branches" `
    -ContentType "application/json" `
    -Body "{
        `"companyId`": `"$($company1.id)`",
        `"name`": `"Sucursal Norte`",
        `"address`": `"Calle Norte 456`"
    }"

Invoke-RestMethod `
    -Method POST `
    -Uri "$BASE_URL/api/branches" `
    -ContentType "application/json" `
    -Body "{
        `"companyId`": `"$($company2.id)`",
        `"name`": `"Sucursal Mall`",
        `"address`": `"Mall Plaza 789`"
    }"

Write-Host ""
Write-Host "Seed completado."