# Configuración de Fira Code en Cursor

Este proyecto está configurado para usar la fuente **Fira Code** en Cursor, la misma que usas en Visual Studio Community.

## Configuración Aplicada

### Archivos de Configuración
- `.vscode/settings.json` - Configuración específica del workspace
- `.cursor/settings.json` - Configuración específica para Cursor

### Configuración de Fuente
```json
{
    "editor.fontFamily": "'Fira Code', 'Cascadia Code', 'Consolas', 'Courier New', monospace",
    "editor.fontLigatures": true,
    "editor.fontSize": 14,
    "editor.fontWeight": "400",
    "editor.letterSpacing": 0.5,
    "editor.lineHeight": 1.5
}
```

## Instalación de Fira Code

Si no tienes Fira Code instalada en tu sistema, puedes instalarla de las siguientes maneras:

### Windows
1. **Descarga directa**: Ve a [Fira Code en GitHub](https://github.com/tonsky/FiraCode)
2. **Chocolatey**: `choco install firacode`
3. **Scoop**: `scoop install firacode`

### Instalación Manual
1. Descarga los archivos TTF desde [Fira Code Releases](https://github.com/tonsky/FiraCode/releases)
2. Extrae los archivos
3. Haz clic derecho en cada archivo `.ttf` y selecciona "Instalar"
4. Reinicia Cursor

## Características de Fira Code

- **Ligaduras**: Símbolos especiales para operadores como `=>`, `!=`, `<=`, etc.
- **Monospace**: Fuente de ancho fijo perfecta para programación
- **Legibilidad**: Optimizada para lectura de código
- **Soporte Unicode**: Compatible con caracteres especiales

## Fallback de Fuentes

Si Fira Code no está disponible, el sistema usará estas fuentes en orden:
1. Fira Code
2. Cascadia Code
3. Consolas
4. Courier New
5. Monospace (genérico)

## Configuración Adicional

### Terminal Integrado
El terminal integrado también está configurado para usar Fira Code:
```json
{
    "terminal.integrated.fontFamily": "'Fira Code', 'Cascadia Code', 'Consolas', 'Courier New', monospace",
    "terminal.integrated.fontSize": 13,
    "terminal.integrated.fontLigatures": true
}
```

### Configuración del Editor
- Tamaño de fuente: 14px
- Espaciado entre letras: 0.5px
- Altura de línea: 1.5
- Ligaduras habilitadas
- Tamaño de tabulación: 4 espacios

## Verificación

Para verificar que Fira Code está funcionando correctamente:

1. Abre cualquier archivo de código
2. Busca operadores como `=>`, `!=`, `<=`, `>=`
3. Deberías ver ligaduras especiales en lugar de caracteres separados

## Solución de Problemas

### La fuente no se aplica
1. Verifica que Fira Code esté instalada: `fc-list | grep -i fira`
2. Reinicia Cursor completamente
3. Verifica que los archivos de configuración estén en la ubicación correcta

### Ligaduras no aparecen
1. Asegúrate de que `"editor.fontLigatures": true` esté configurado
2. Verifica que estés usando Fira Code y no una fuente fallback
3. Algunos temas pueden afectar la visualización de ligaduras

## Personalización

Puedes ajustar la configuración modificando los archivos:
- `.vscode/settings.json` para configuración del workspace
- `.cursor/settings.json` para configuración específica de Cursor

### Ajustes Comunes
- Cambiar `editor.fontSize` para ajustar el tamaño
- Modificar `editor.letterSpacing` para el espaciado
- Ajustar `editor.lineHeight` para la altura de línea
