namespace ServiceLibrary.ConsoleApp.Services
{
    public abstract class BaseService
    {
        /// <summary>
        /// Singleton for a ServiceProvider instance.
        /// </summary>
        public ServiceProvider ServiceProvider
        {
            get => ServiceProviderConfiguration.Instance.ServiceProvider;
        }
    }
}