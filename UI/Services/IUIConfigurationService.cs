using Microsoft.Extensions.Configuration;
using UI.Settings;

namespace UI.Services;

public interface IUIConfigurationService
{
    void CreateDefaultSettings(UISettings uiSettingsModel);

    IConfiguration GetConfiguration();
}
