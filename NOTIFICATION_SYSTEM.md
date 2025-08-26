# Sistema de Notificaciones - CCC Rugby Web

## Descripción General

El sistema de notificaciones ha sido completamente refactorizado para usar el `NotificationService` en lugar del sistema anterior de manejo de excepciones. Esto proporciona una experiencia de usuario más consistente y un manejo de errores más limpio.

## Componentes del Sistema

### 1. NotificationService

**Ubicación**: `Services/NotificationService.cs`

**Funcionalidades**:
- `ShowSuccess(string message, int duration = 5000)` - Notificaciones de éxito
- `ShowInfo(string message, int duration = 5000)` - Notificaciones informativas
- `ShowWarning(string message, int duration = 7000)` - Advertencias
- `ShowError(string message, int duration = 10000)` - Errores
- `ShowCustom(string message, Severity severity, int duration = 5000, bool requireInteraction = false)` - Notificaciones personalizadas

**Configuración**:
- Posición: TopCenter
- Duración por defecto: 5 segundos (10 segundos para errores)
- Transiciones suaves: 300ms
- Icono de cierre habilitado

### 2. ErrorBoundary

**Ubicación**: `Components/Utils/ErrorBundary.razor`

**Funcionalidades**:
- Captura errores no manejados en componentes
- Muestra notificaciones de error automáticamente
- Botón de recuperación para reintentar la operación
- Mensajes de error user-friendly

## Implementación en Componentes

### Inyección del Servicio

```csharp
@inject INotificationService NotificationService
```

### Uso Básico

```csharp
// Éxito
NotificationService.ShowSuccess("Operación completada exitosamente");

// Información
NotificationService.ShowInfo("Procesando datos...");

// Advertencia
NotificationService.ShowWarning("Algunos datos pueden estar incompletos");

// Error
NotificationService.ShowError("Error al cargar los datos");
```

### Manejo de Excepciones

```csharp
try
{
    // Código que puede generar excepciones
    var response = await httpClient.GetAsync("api/data");
    if (response.IsSuccessStatusCode)
    {
        // Procesar respuesta exitosa
        NotificationService.ShowSuccess("Datos cargados correctamente");
    }
    else
    {
        NotificationService.ShowError($"Error en la respuesta: {response.ReasonPhrase}");
    }
}
catch (Exception ex)
{
    NotificationService.ShowError($"Error inesperado: {ex.Message}");
}
```

## Cambios Realizados

### Archivos Modificados

1. **MainLayout.razor**
   - Eliminado `IExceptionHandlerService`
   - Reemplazado manejo de excepciones por `NotificationService`
   - Simplificado métodos `LoadMenu()` y `LoadUserAvatar()`

2. **ListadoPedidos.razor**
   - Agregado `INotificationService`
   - Reemplazado `throw new Exception()` por notificaciones
   - Mejorado manejo de errores en `LoadTipoArticulos()`

3. **Login.razor**
   - Agregado `INotificationService`
   - Reemplazado variable `info` por notificaciones

4. **Perfil.razor**
   - Agregado `INotificationService`
   - Eliminado variable `error`
   - Mejorado manejo de errores en `UploadAvatar()` y `LoadUserAvatar()`

### Archivos Eliminados

1. **Services/ExeptionHandlerService.cs** - Ya no necesario

### Archivos Actualizados

1. **Program.cs** - Eliminada referencia a `ExceptionHandlerService`

## Ventajas del Nuevo Sistema

### 1. Consistencia
- Todas las notificaciones tienen el mismo estilo y comportamiento
- Posición y duración estandarizadas
- Transiciones suaves y profesionales

### 2. Simplicidad
- API simple y fácil de usar
- No requiere configuración compleja
- Eliminación de código boilerplate

### 3. Experiencia de Usuario
- Notificaciones no intrusivas
- Información clara y contextual
- Posibilidad de interacción (para errores críticos)

### 4. Mantenibilidad
- Código más limpio y legible
- Centralización del manejo de notificaciones
- Fácil personalización global

## Mejores Prácticas

### 1. Tipos de Notificación

- **Success**: Operaciones completadas exitosamente
- **Info**: Información general, estados de carga
- **Warning**: Advertencias, datos incompletos
- **Error**: Errores que requieren atención del usuario

### 2. Mensajes

- Usar lenguaje claro y directo
- Evitar jerga técnica
- Proporcionar contexto cuando sea necesario
- Ser específico sobre el problema

### 3. Duración

- **Success/Info**: 5 segundos (suficiente para leer)
- **Warning**: 7 segundos (más tiempo para advertencias)
- **Error**: 10 segundos (requiere interacción del usuario)

### 4. Manejo de Excepciones

```csharp
// ✅ Correcto
try
{
    await operation();
    NotificationService.ShowSuccess("Operación exitosa");
}
catch (HttpRequestException ex)
{
    NotificationService.ShowError("Error de conexión. Verifica tu internet.");
}
catch (Exception ex)
{
    NotificationService.ShowError($"Error inesperado: {ex.Message}");
}

// ❌ Incorrecto
throw new Exception("Error de usuario");
```

## Configuración Avanzada

### Personalización de Notificaciones

```csharp
NotificationService.ShowCustom(
    message: "Mensaje personalizado",
    severity: Severity.Info,
    duration: 3000,
    requireInteraction: false
);
```

### Configuración Global

El servicio se configura automáticamente en `MainLayout.razor`:

```csharp
protected override async Task OnInitializedAsync()
{
    NotificationService.SetSnackbar(Snackbar);
    // ...
}
```

## Migración de Código Existente

### Antes (ExceptionHandler)

```csharp
@inject IExceptionHandlerService ExceptionHandler

await ExceptionHandler.HandleExceptionsAsync(async () =>
{
    // código
});
```

### Después (NotificationService)

```csharp
@inject INotificationService NotificationService

try
{
    // código
    NotificationService.ShowSuccess("Operación exitosa");
}
catch (Exception ex)
{
    NotificationService.ShowError($"Error: {ex.Message}");
}
```

## Troubleshooting

### Problemas Comunes

1. **Notificaciones no aparecen**
   - Verificar que `NotificationService.SetSnackbar(Snackbar)` esté llamado
   - Comprobar que `MudSnackbarProvider` esté en el layout

2. **Notificaciones aparecen en posición incorrecta**
   - Verificar configuración en `NotificationService.SetSnackbar()`

3. **Duración incorrecta**
   - Verificar parámetros en las llamadas al servicio

### Debug

Para desarrollo, las notificaciones también se muestran en consola:

```csharp
Console.WriteLine($"Snackbar not initialized. Message: {message}");
```

## Conclusión

El nuevo sistema de notificaciones proporciona una experiencia de usuario más consistente y un código más mantenible. La eliminación del `ExceptionHandlerService` simplifica la arquitectura y reduce la complejidad del manejo de errores.
