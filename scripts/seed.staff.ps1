param($ctx)

Write-Host "`nCreando módulo Staff..."

# =========================
# DEBUG
# =========================
Write-Host "`nDEBUG STAFF USERS RAW"
$ctx.StaffUsers | Format-List * | Out-String | Write-Host

# =========================
# VALIDACIONES
# =========================
if (-not $ctx.StaffUsers) {
    throw "No existen StaffUsers en el contexto."
}

# =========================
# INIT EMPLOYEES
# =========================
if (-not ($ctx.PSObject.Properties.Name -contains "Employees")) {

    $ctx | Add-Member `
        -MemberType NoteProperty `
        -Name Employees `
        -Value @()
}

# =========================
# HELPERS
# =========================
function Post-Json($url, $body) {

    return Invoke-RestMethod `
        -Method POST `
        -Uri $url `
        -ContentType "application/json" `
        -Body ($body | ConvertTo-Json -Depth 10)
}

# =========================
# POSITIONS
# =========================
Write-Host "`nCreando posiciones..."

$developerPosition = Post-Json "$($ctx.BaseUrl)/api/positions" @{
    companyId = $ctx.Company1.id
    name = "Backend Developer"
    description = "Encargado del backend"
}

$managerPosition = Post-Json "$($ctx.BaseUrl)/api/positions" @{
    companyId = $ctx.Company1.id
    name = "Branch Manager"
    description = "Encargado de sucursal"
}

$sellerPosition = Post-Json "$($ctx.BaseUrl)/api/positions" @{
    companyId = $ctx.Company2.id
    name = "Sales Assistant"
    description = "Ventas y atención al cliente"
}

$ctx | Add-Member `
    -MemberType NoteProperty `
    -Name DeveloperPositionId `
    -Value $developerPosition.id `
    -Force

$ctx | Add-Member `
    -MemberType NoteProperty `
    -Name ManagerPositionId `
    -Value $managerPosition.id `
    -Force

$ctx | Add-Member `
    -MemberType NoteProperty `
    -Name SellerPositionId `
    -Value $sellerPosition.id `
    -Force

Write-Host "Positions creadas correctamente"

# =========================
# CREATE EMPLOYEE
# =========================
function Create-Employee(
    $userId,
    $companyId,
    $branchId,
    $positionId
) {

    $body = @{
        userId = "$userId"
        companyId = "$companyId"
        mainBranchId = "$branchId"
        mainPositionId = "$positionId"
    }

    Write-Host "`nREQUEST BODY"
    $body | ConvertTo-Json -Depth 10 | Write-Host

    return Invoke-RestMethod `
        -Method POST `
        -Uri "$($ctx.BaseUrl)/api/employees" `
        -ContentType "application/json" `
        -Body ($body | ConvertTo-Json -Depth 10)
}

# =========================
# EMPLOYEES
# =========================
Write-Host "`nCreando employees..."

Write-Host "`n===================="
Write-Host "STAFF USERS DEBUG"
Write-Host "===================="

Write-Host "`nCTX TYPE:"
$ctx.GetType().FullName

Write-Host "`nSTAFF USERS TYPE:"
$ctx.StaffUsers.GetType().FullName

Write-Host "`nSTAFF USERS COUNT:"
$ctx.StaffUsers.Count

Write-Host "`nSTAFF USERS RAW JSON:"
$ctx.StaffUsers | ConvertTo-Json -Depth 10 | Write-Host

foreach ($user in $ctx.StaffUsers) {

    if (-not $user) {
        Write-Host "Usuario null, saltando..."
        continue
    }

    $userId = $user.id
    $email = $user.email
    $companyId = $user.companyId
    $branchId = $user.branchId

    Write-Host "`n===================="
    Write-Host "CREATING EMPLOYEE"
    Write-Host "UserId:" $userId
    Write-Host "Email:" $email
    Write-Host "CompanyId:" $companyId
    Write-Host "BranchId:" $branchId

    if (-not $userId) {
        Write-Host "Usuario inválido, saltando..."
        continue
    }

    $positionId = $null

    if ($companyId -eq $ctx.Company1.id) {

        $positionId = $ctx.DeveloperPositionId
    }
    elseif ($companyId -eq $ctx.Company2.id) {

        $positionId = $ctx.SellerPositionId
    }
    else {

        throw "Empresa no reconocida para usuario $email"
    }

    Write-Host "PositionId:" $positionId

    try {

        $employee = Create-Employee `
            $userId `
            $companyId `
            $branchId `
            $positionId

        if ($employee) {

            $ctx.Employees += $employee

            Write-Host "Employee creado correctamente"
        }
    }
    catch {

        Write-Host "`nERROR CREANDO EMPLOYEE"
        Write-Host $_.Exception.Message

        if ($_.ErrorDetails.Message) {

            Write-Host "`nDETAILS:"
            Write-Host $_.ErrorDetails.Message
        }
    }
}

Write-Host "`nEmployees creados correctamente: $($ctx.Employees.Count)"

return $ctx