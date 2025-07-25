using Microsoft.Extensions.Configuration;

namespace TaskbarCat.Services
{
    public class SettingsService
    {
        private readonly IConfiguration _configuration;

        public SettingsService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public int HorizontalOffset => _configuration.GetValue<int>("HorizontalOffset");
    }
}
