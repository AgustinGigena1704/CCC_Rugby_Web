using MudBlazor;
using Microsoft.AspNetCore.Components;
using CCC_Rugby_Web.Components.Utils;
using System.Threading.Tasks;

namespace CCC_Rugby_Web.Services
{
    public interface INotificationService
    {
        void SetSnackbar(ISnackbar snackbar);
        void SetDialogService(IDialogService dialogService);
        void ShowSuccess(string message, int duration = 5000, string descripcion = null);
        void ShowInfo(string message, int duration = 5000, string descripcion = null);
        void ShowWarning(string message, int duration = 7000, string descripcion = null);
        void ShowError(string message, string descripcion, int duration = 10000);
        void ShowCustom(string message, Severity severity, int duration = 5000, bool requireInteraction = false, string descripcion = null);
    }

    public class NotificationService : INotificationService
    {
        private ISnackbar? _snackbar;
        private IDialogService? _dialogService;

        public void SetSnackbar(ISnackbar snackbar)
        {
            snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            _snackbar = snackbar;
        }

        public void SetDialogService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public void ShowSuccess(string message, int duration = 5000, string descripcion = null)
        {
            ShowCustom(message, Severity.Success, duration, false, descripcion);
        }

        public void ShowInfo(string message, int duration = 5000, string descripcion = null)
        {
            ShowCustom(message, Severity.Info, duration, false, descripcion);
        }

        public void ShowWarning(string message, int duration = 7000, string descripcion = null)
        {
            ShowCustom(message, Severity.Warning, duration, false, descripcion);
        }

        public void ShowError(string message, string descripcion, int duration = 10000)
        {
            ShowCustom(message, Severity.Error, duration, true, descripcion);
        }

        public void ShowCustom(string message, Severity severity, int duration = 5000, bool requireInteraction = false, string descripcion = null)
        {
            if (_snackbar == null)
            {
                Console.WriteLine($"Snackbar not initialized. Message: {message}");
                return;
            }

            bool interaction = requireInteraction || !string.IsNullOrEmpty(descripcion);

            _snackbar.Add(message, severity, config =>
            {
                config.RequireInteraction = interaction;
                config.ShowCloseIcon = true;
                config.VisibleStateDuration = duration;
                config.ShowTransitionDuration = 300;
                config.HideTransitionDuration = 300;
                if (!string.IsNullOrEmpty(descripcion) && _dialogService != null)
                {
                    config.OnClick = async (snackbar) =>
                    {
                        var parameters = new DialogParameters { ["Titulo"] = message, ["Descripcion"] = descripcion };
                        await _dialogService.ShowAsync<NotificationDialog>("Descripción", parameters);
                    };
                }
            });
        }
    }
}
