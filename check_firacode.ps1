# Script para verificar la instalación de Fira Code
Write-Host "Verificando instalación de Fira Code..." -ForegroundColor Green

# Verificar si Fira Code está instalada
$fontPath = "C:\Windows\Fonts\FiraCode-Regular.ttf"
$fontInstalled = Test-Path $fontPath

if ($fontInstalled) {
    Write-Host "✅ Fira Code está instalada correctamente" -ForegroundColor Green
    Write-Host "Ubicación: $fontPath" -ForegroundColor Cyan
} else {
    Write-Host "❌ Fira Code no está instalada" -ForegroundColor Red
    Write-Host ""
    Write-Host "Para instalar Fira Code:" -ForegroundColor Yellow
    Write-Host "1. Ve a: https://github.com/tonsky/FiraCode/releases" -ForegroundColor White
    Write-Host "2. Descarga el archivo ZIP más reciente" -ForegroundColor White
    Write-Host "3. Extrae los archivos .ttf" -ForegroundColor White
    Write-Host "4. Haz clic derecho en cada archivo .ttf y selecciona 'Instalar'" -ForegroundColor White
    Write-Host "5. Reinicia Cursor" -ForegroundColor White
    Write-Host ""
    Write-Host "O usa Chocolatey (si está instalado):" -ForegroundColor Yellow
    Write-Host "choco install firacode" -ForegroundColor White
}

Write-Host ""
Write-Host "Configuración de Cursor aplicada:" -ForegroundColor Green
Write-Host "✅ .vscode/settings.json - Configuración del workspace" -ForegroundColor Cyan
Write-Host "✅ .cursor/settings.json - Configuración específica de Cursor" -ForegroundColor Cyan
Write-Host "✅ FIRACODE_SETUP.md - Documentación completa" -ForegroundColor Cyan

Write-Host ""
Write-Host "Para aplicar los cambios:" -ForegroundColor Yellow
Write-Host "1. Reinicia Cursor completamente" -ForegroundColor White
Write-Host "2. Abre cualquier archivo de código" -ForegroundColor White
Write-Host "3. Verifica que los operadores como =>, !=, <= muestren ligaduras" -ForegroundColor White

Write-Host ""
Write-Host "Presiona cualquier tecla para continuar..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
