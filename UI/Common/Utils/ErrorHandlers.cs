using MainModule.Common;
using System.IO;
using System.Windows;

namespace UI.Common.Utils;

public static class ErrorHandlers
{
    public static void HandleUISettingsFileError()
    {
        MessageBox.Show((string)Application.Current.FindResource("ui_settings_file_error_message"),
                            (string)Application.Current.FindResource("ui_settings_file_error_header"),
                            MessageBoxButton.OK, MessageBoxImage.Error);

        File.Delete(Constants.UIUserSettingsFilePath);
        MiscFunctions.RestartApplication();
    }

    public static void HandleImageConvertionError()
    {
        MessageBox.Show((string)Application.Current.FindResource("image_convertion_error_message"),
                        null,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
    }

    public static void HandleUpdateCheckError(string exceptionMessage)
    {
        MessageBox.Show((string)Application.Current.FindResource("update_check_error_message") + "Exception: " + exceptionMessage,
                        null,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
    }
}
