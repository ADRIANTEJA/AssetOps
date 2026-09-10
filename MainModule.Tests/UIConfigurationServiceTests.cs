using System.Windows;
using UI.Services;

namespace MainModule.Tests;

public class UIConfigurationServiceTests
{
    UIConfigurationService uiConfigService = new();

    [Fact]
    public void GetConfiguration_ShouldGetConfiguration()
    {
        try
        {
            Assert.True(uiConfigService.Configuration["IsDarkThemeOn"] != null
                        && uiConfigService.Configuration["Language"] != null
                        && uiConfigService.Configuration["FontSize"] != null);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            Assert.Fail(); 
        }
    }
}
