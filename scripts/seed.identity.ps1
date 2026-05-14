param($ctx)

Write-Host "`nCreando usuarios..."

$password = "Str0ng!Pass123"

function Create-User($companyId, $name, $email) {

    try {

        return Invoke-RestMethod `
            -Method POST `
            -Uri "$($ctx.BaseUrl)/api/users" `
            -ContentType "application/json" `
            -Body (@{
                companyId = $companyId
                name = $name
                email = $email
                password = $password
            } | ConvertTo-Json -Depth 10)
    }
    catch {

        Write-Host "Error creando usuario: $email"
        Write-Host $_.Exception.Message

        throw
    }
}

# =========================
# INIT
# =========================
$ctx.Users = [System.Collections.Generic.List[object]]::new()

# =========================
# SYSTEM USERS
# =========================
$ctx.Users.Add((
    Create-User `
        $ctx.Company1.id `
        "Admin One" `
        "admin1@tech.com"
))

$ctx.Users.Add((
    Create-User `
        $ctx.Company1.id `
        "Manager One" `
        "manager1@tech.com"
))

$ctx.Users.Add((
    Create-User `
        $ctx.Company1.id `
        "Supervisor One" `
        "supervisor1@tech.com"
))

$ctx.Users.Add((
    Create-User `
        $ctx.Company2.id `
        "Admin Two" `
        "admin2@retail.com"
))

$ctx.Users.Add((
    Create-User `
        $ctx.Company2.id `
        "Manager Two" `
        "manager2@retail.com"
))

# =========================
# STAFF USERS
# =========================
for ($i = 1; $i -le 15; $i++) {

    $companyId = if ($i % 2 -eq 0) {
        $ctx.Company2.id
    }
    else {
        $ctx.Company1.id
    }

    $ctx.Users.Add((
        Create-User `
            $companyId `
            "Staff $i" `
            "staff$i@company.com"
    ))
}

Write-Host "Usuarios creados: $($ctx.Users.Count)"

if ($ctx.Users.Count -ne 20) {
    throw "Seed inválido: se esperaban 20 usuarios"
}

# =========================
# ASSIGN ROLE
# =========================
Write-Host "`nAsignando roles..."

function Assign-Role($userId, $role, $branchId = $null) {

    try {

        Invoke-RestMethod `
            -Method POST `
            -Uri "$($ctx.BaseUrl)/api/users/$userId/roles" `
            -ContentType "application/json" `
            -Body (@{
                role = $role
                branchId = $branchId
            } | ConvertTo-Json -Depth 10) | Out-Null
    }
    catch {

        Write-Host "Error asignando rol $role al usuario $userId"
        Write-Host $_.Exception.Message

        throw
    }
}

# =========================
# SYSTEM ROLES
# =========================
Assign-Role $ctx.Users[0].id "CompanyAdmin"
Assign-Role $ctx.Users[1].id "BranchManager"
Assign-Role $ctx.Users[2].id "Supervisor"
Assign-Role $ctx.Users[3].id "CompanyAdmin"
Assign-Role $ctx.Users[4].id "BranchManager"

# =========================
# STAFF ROLES
# =========================
$ctx.StaffUsers = [System.Collections.Generic.List[object]]::new()

$i = 0
$branchCount = $ctx.Branches.Count

foreach ($user in ($ctx.Users | Select-Object -Skip 5)) {

    $branch = $ctx.Branches[$i % $branchCount]

    Assign-Role `
        $user.id `
        "Staff" `
        $branch.id

    $staffUser = [PSCustomObject]@{
        id = $user.id
        email = $user.email
        companyId = $user.companyId
        branchId = $branch.id
    }

    $ctx.StaffUsers.Add($staffUser)

    $i++
}

Write-Host "`nStaff asignado: $($ctx.StaffUsers.Count)"

return $ctx