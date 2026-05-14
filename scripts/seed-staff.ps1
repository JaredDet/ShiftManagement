$envContent = Get-Content ".env"

foreach ($line in $envContent) {
    if ($line -match "^\s*#") { continue }

    if ($line -match "=") {
        $name, $value = $line -split "=", 2
        [System.Environment]::SetEnvironmentVariable(
            $name.Trim(),
            $value.Trim()
        )
    }
}

$BASE_URL = "http://localhost:$env:APP_PORT"

Write-Host "Obteniendo empresas..."

$companies = Invoke-RestMethod -Method GET -Uri "$BASE_URL/api/companies"

$company1 = $companies.companies[0]
$company2 = $companies.companies[1]

Write-Host "Empresa 1:" $company1.id
Write-Host "Empresa 2:" $company2.id

Write-Host "`nObteniendo sucursales..."

$company1Branches = Invoke-RestMethod -Method GET -Uri "$BASE_URL/api/branches?companyId=$($company1.id)"
$company2Branches = Invoke-RestMethod -Method GET -Uri "$BASE_URL/api/branches?companyId=$($company2.id)"

$branchCentro = $company1Branches.branches[0]
$branchNorte  = $company1Branches.branches[1]
$branchMall   = $company2Branches.branches[0]

Write-Host "Sucursal Centro:" $branchCentro.id
Write-Host "Sucursal Norte:" $branchNorte.id
Write-Host "Sucursal Mall:" $branchMall.id

Write-Host "`nCreando puestos..."

$developerPosition = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/positions" -ContentType "application/json" -Body (
    @{ companyId=$company1.id; name="Backend Developer"; description="Encargado del backend" } | ConvertTo-Json -Depth 10
)

$managerPosition = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/positions" -ContentType "application/json" -Body (
    @{ companyId=$company1.id; name="Branch Manager"; description="Encargado de sucursal" } | ConvertTo-Json -Depth 10
)

$sellerPosition = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/positions" -ContentType "application/json" -Body (
    @{ companyId=$company2.id; name="Sales Assistant"; description="Ventas y atención al cliente" } | ConvertTo-Json -Depth 10
)

Write-Host "`nCreando usuarios..."

$password = "JuanP@ss1"

$user1 = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/users" -ContentType "application/json" -Body (
    @{ companyId=$company1.id; name="Juan Perez"; email="juan@tech.com"; password=$password } | ConvertTo-Json -Depth 10
)

$user2 = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/users" -ContentType "application/json" -Body (
    @{ companyId=$company1.id; name="Maria Gonzalez"; email="maria@tech.com"; password=$password } | ConvertTo-Json -Depth 10
)

$user3 = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/users" -ContentType "application/json" -Body (
    @{ companyId=$company2.id; name="Pedro Ramirez"; email="pedro@retail.com"; password=$password } | ConvertTo-Json -Depth 10
)

Write-Host "`nCreando colaboradores (NUEVO DTO)..."

$employee1 = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/employees" -ContentType "application/json" -Body (
    @{
        userId = $user1.id
        companyId = $company1.id
        mainBranchId = $branchCentro.id
        mainPositionId = $developerPosition.id
    } | ConvertTo-Json -Depth 10
)

$employee2 = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/employees" -ContentType "application/json" -Body (
    @{
        userId = $user2.id
        companyId = $company1.id
        mainBranchId = $branchCentro.id
        mainPositionId = $managerPosition.id
    } | ConvertTo-Json -Depth 10
)

$employee3 = Invoke-RestMethod -Method POST -Uri "$BASE_URL/api/employees" -ContentType "application/json" -Body (
    @{
        userId = $user3.id
        companyId = $company2.id
        mainBranchId = $branchMall.id
        mainPositionId = $sellerPosition.id
    } | ConvertTo-Json -Depth 10
)

Write-Host "`nColaboradores:"
Write-Host "1:" $employee1.id
Write-Host "2:" $employee2.id
Write-Host "3:" $employee3.id

Write-Host "`nSeed completado correctamente."