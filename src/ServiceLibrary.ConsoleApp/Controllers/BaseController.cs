namespace ServiceLibrary.ConsoleApp.Controllers
{
    public abstract class BaseController
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