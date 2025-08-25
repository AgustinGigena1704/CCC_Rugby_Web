using System.Collections.Concurrent;

namespace CCC_Rugby_Web.Services
{
    public interface ILoadingService
    {
        bool IsLoading { get; }
        int LoadingCount { get; }
        event Action? OnLoadingStateChanged;
        void StartLoading();
        void StopLoading();
        void ClearAll();
    }

    public class LoadingService : ILoadingService
    {
        private int _loadingCount = 0;
        private readonly object _lock = new object();

        public bool IsLoading => _loadingCount > 0;

        public int LoadingCount => _loadingCount;

        public event Action? OnLoadingStateChanged;

        public void StartLoading()
        {
            bool wasLoading;
            bool isLoadingNow;

            lock (_lock)
            {
                wasLoading = _loadingCount > 0;
                _loadingCount++;
                isLoadingNow = _loadingCount > 0;
            }

            // Solo notificar si cambió el estado general de loading
            if (!wasLoading && isLoadingNow)
            {
                OnLoadingStateChanged?.Invoke();
            }
        }

        public void StopLoading()
        {
            bool wasLoading;
            bool isLoadingNow;

            lock (_lock)
            {
                wasLoading = _loadingCount > 0;
                if (_loadingCount > 0)
                {
                    _loadingCount--;
                }
                isLoadingNow = _loadingCount > 0;
            }

            // Solo notificar si cambió el estado general de loading
            if (wasLoading && !isLoadingNow)
            {
                OnLoadingStateChanged?.Invoke();
            }
        }

        public void ClearAll()
        {
            bool wasLoading;

            lock (_lock)
            {
                wasLoading = _loadingCount > 0;
                _loadingCount = 0;
            }

            if (wasLoading)
            {
                OnLoadingStateChanged?.Invoke();
            }
        }
    }

    // Extensión para usar con using statements
    public static class LoadingServiceExtensions
    {
        public static IDisposable LoadingScope(this ILoadingService loadingService)
        {
            return new LoadingScope(loadingService);
        }
    }

    // Clase helper para usar con using statements
    public class LoadingScope : IDisposable
    {
        private readonly ILoadingService _loadingService;
        private bool _disposed = false;

        public LoadingScope(ILoadingService loadingService)
        {
            _loadingService = loadingService;
            _loadingService.StartLoading();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _loadingService.StopLoading();
                _disposed = true;
            }
        }
    }
}