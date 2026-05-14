param($ctx)

Write-Host "`nCreando empresas..."

function Post-Json($url, $body) {
    return Invoke-RestMethod `
        -Method POST `
        -Uri $url `
        -ContentType "application/json" `
        -Body ($body | ConvertTo-Json -Depth 10)
}

# =========================
# COMPANIES
# =========================
$ctx.Company1 = Post-Json "$($ctx.BaseUrl)/api/companies" @{
    name = "Tech Solutions"
}

$ctx.Company2 = Post-Json "$($ctx.BaseUrl)/api/companies" @{
    name = "Retail Group"
}

Write-Host "Company1: $($ctx.Company1.id)"
Write-Host "Company2: $($ctx.Company2.id)"

if (-not $ctx.Company1.id) { throw "Company1 no fue creada" }
if (-not $ctx.Company2.id) { throw "Company2 no fue creada" }

Write-Host "`nCreando sucursales..."

$ctx.Branches += Post-Json "$($ctx.BaseUrl)/api/branches" @{
    companyId = $ctx.Company1.id
    name = "Sucursal Centro"
    address = "Av. Principal 123"
}

$ctx.Branches += Post-Json "$($ctx.BaseUrl)/api/branches" @{
    companyId = $ctx.Company1.id
    name = "Sucursal Norte"
    address = "Calle Norte 456"
}

$ctx.Branches += Post-Json "$($ctx.BaseUrl)/api/branches" @{
    companyId = $ctx.Company2.id
    name = "Sucursal Mall"
    address = "Mall Plaza 789"
}

Write-Host "Branches creadas: $($ctx.Branches.Count)"

return $ctx