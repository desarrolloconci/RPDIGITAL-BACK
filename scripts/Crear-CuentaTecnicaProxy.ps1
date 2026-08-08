<#
.SYNOPSIS
    Crea (si no existe) la cuenta tecnica en BH_USERS que usa ApiGestionTurnosTW
    para autenticarse contra apirpdigital al hacer de puente para el portal de
    medicos (natanet-medicos-front/natanet-medicos-api).

.DESCRIPTION
    Llama a POST /api/Login/register de apirpdigital, que es quien hashea la
    contrasena (BCrypt + salt) exactamente como espera el login real -- por eso
    este script NO inserta directo en la base, siempre pasa por el endpoint.

    Es idempotente: si ya existe un usuario con el mismo UserName, no crea uno
    nuevo (evita duplicados si se corre mas de una vez, por ejemplo despues de
    un refresh del ambiente de desarrollo que borra los datos de prueba).

    Despues de crear la cuenta, hay que configurar en ApiGestionTurnosTW la
    variable de entorno Apirpdigital__ServicePassword con la MISMA contrasena
    que se use aca (y Apirpdigital__ServiceUsername si se cambia el usuario).

.PARAMETER BaseUrl
    URL base de apirpdigital. Ejemplos:
      Desarrollo (SRV-DESA02): http://localhost:7174  (o la URL real del server)
      Produccion (SRV-SQL02):  https://<host-real-de-produccion>

.PARAMETER Password
    Contrasena de la cuenta tecnica. OBLIGATORIA, sin default -- usar una
    contrasena DISTINTA en desarrollo y en produccion, nunca la misma.

.PARAMETER UserName
    Usuario tecnico. Default: svc-proxy-apigestionturnos

.EXAMPLE
    # Desarrollo
    .\Crear-CuentaTecnicaProxy.ps1 -BaseUrl "http://localhost:7174" -Password "SvcProxy2026-Fuerte"

.EXAMPLE
    # Produccion (usar una contrasena propia y fuerte, distinta a la de desarrollo)
    .\Crear-CuentaTecnicaProxy.ps1 -BaseUrl "https://apirpdigital.produccion.interno" -Password "otra-contrasena-fuerte"
#>
param(
    [Parameter(Mandatory = $true)]
    [string]$BaseUrl,

    [Parameter(Mandatory = $true)]
    [string]$Password,

    [string]$UserName = "svc-proxy-apigestionturnos",
    [string]$Name = "Servicio",
    [string]$LastName = "ProxyApiGestionTurnos",
    [string]$Email = "svc-proxy-apigestionturnos@concicarpinella.com.ar",
    [string]$Role = "ServicioInterno"
)

$ErrorActionPreference = "Stop"
$BaseUrl = $BaseUrl.TrimEnd("/")

Write-Host "Verificando si '$UserName' ya existe en $BaseUrl ..."

try {
    $usuarios = Invoke-RestMethod -Method Get -Uri "$BaseUrl/api/Login" -ErrorAction Stop
}
catch {
    Write-Host "No se pudo consultar GET /api/Login (se intenta crear igual): $($_.Exception.Message)" -ForegroundColor Yellow
    $usuarios = @()
}

$existente = $usuarios | Where-Object {
    $_.user_name -eq $UserName -or $_.User_name -eq $UserName
} | Select-Object -First 1

if ($existente) {
    $idExistente = if ($null -ne $existente.ID) { $existente.ID } else { $existente.id }
    Write-Host "Ya existe una cuenta con UserName '$UserName' (ID $idExistente) - no se crea de nuevo." -ForegroundColor Green
    Write-Host "Si necesitas resetear la contrasena, usa PUT /api/Login/$UserName en su lugar."
    exit 0
}

$body = @{
    Name      = $Name
    Last_name = $LastName
    Email     = $Email
    User_name = $UserName
    Password  = $Password
    Role      = $Role
} | ConvertTo-Json

Write-Host "Creando cuenta tecnica '$UserName' ..."
$resultado = Invoke-RestMethod -Method Post -Uri "$BaseUrl/api/Login/register" -ContentType "application/json" -Body $body

Write-Host "Cuenta creada:" -ForegroundColor Green
$resultado | Format-List

Write-Host ""
Write-Host "IMPORTANTE: configurar en ApiGestionTurnosTW la variable de entorno" -ForegroundColor Cyan
Write-Host "  Apirpdigital__ServiceUsername = $UserName"
Write-Host "  Apirpdigital__ServicePassword = (la contrasena que acabas de usar)"
Write-Host "para que el proxy pueda loguearse con esta cuenta."
