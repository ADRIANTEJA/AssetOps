using MainModule.Common;
using MainModule.Common.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using UI.Settings;

namespace UI.Services;

public class UIConfigurationService : IUIConfigurationService
{
    private readonly string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                    Constants.ApplicationBaseFolderName,
                                                    Constants.ApplicationSettingsFolderName);

    /// <summary>
    /// Creates the default settings file if it does not exist.
    /// </summary>
    /// <param name="uiSettings"></param>
    public void CreateDefaultSettings(UISettings uiSettings)
    {
        if (!File.Exists(Constants.UIUserSettingsFilePath)) 
            JsonFileUtils.SerializeJsonFile(uiSettings, Constants.UIUserSettingsFilePath);
    }

    public IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile(Constants.UIUserSettingsFileName, optional: true, reloadOnChange: true).Build();
    }
}
