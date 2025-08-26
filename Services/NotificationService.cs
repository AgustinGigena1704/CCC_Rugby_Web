using MudBlazor;

namespace CCC_Rugby_Web.Services
{
    public interface INotificationService
    {
        void SetSnackbar(ISnackbar snackbar);
        void ShowSuccess(string message, int duration = 5000);
        void ShowInfo(string message, int duration = 5000);
        void ShowWarning(string message, int duration = 7000);
        void ShowError(string message, int duration = 10000);
        void ShowCustom(string message, Severity severity, int duration = 5000, bool requireInteraction = false);
    }

    public class NotificationService : INotificationService
    {
        private ISnackbar? _snackbar;

        public void SetSnackbar(ISnackbar snackbar)
        {
            snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            _snackbar = snackbar;
        }

        public void ShowSuccess(string message, int duration = 5000)
        {
            ShowCustom(message, Severity.Success, duration);
        }

        public void ShowInfo(string message, int duration = 5000)
        {
            ShowCustom(message, Severity.Info, duration);
        }

        public void ShowWarning(string message, int duration = 7000)
        {
            ShowCustom(message, Severity.Warning, duration);
        }

        public void ShowError(string message, int duration = 10000)
        {
            ShowCustom(message, Severity.Error, duration, true);
        }

        public void ShowCustom(string message, Severity severity, int duration = 5000, bool requireInteraction = false)
        {
            if (_snackbar == null)
            {
                Console.WriteLine($"Snackbar not initialized. Message: {message}");
                return;
            }

            _snackbar.Add(message, severity, config =>
            {
                config.RequireInteraction = requireInteraction;
                config.ShowCloseIcon = true;
                config.VisibleStateDuration = duration;
                config.ShowTransitionDuration = 300;
                config.HideTransitionDuration = 300;
            });
        }
    }
}
