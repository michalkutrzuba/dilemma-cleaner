using System.Globalization;
using DilemmaCleaner.Api.Web.Infrastructure.Cache;

namespace DilemmaCleaner.Api.Tests
{
    public abstract class Specification
    {
        protected abstract void EstablishContext();
        protected abstract void BecauseOf();

        protected virtual void AfterEach()
        {
        }

        protected readonly ICacheManager CacheManager = new FakeCacheManager();

        protected bool ScenarioExceptionsExpected;
        protected Exception ScenarioException;

        [SetUp]
        public void Execute()
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try
            {
                EstablishContext();
                BecauseOf();
            }
            catch (Exception exception)
            {
                if (!ScenarioExceptionsExpected)
                    throw;

                ScenarioException = exception;
            }
        }

        [TearDown]
        public void CleanUp()
        {
            AfterEach();
        }
    }
}
