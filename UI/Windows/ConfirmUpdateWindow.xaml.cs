using System;
using System.Windows;
using System.Windows.Input;
using UI.Common.Utils;
using Velopack;
using Velopack.Sources;

namespace UI.Windows;

/// <summary>
/// Interaction logic for ConfirmUpdateWindow.xaml
/// </summary>
public partial class ConfirmUpdateWindow : Window
{
    private readonly UpdateInfo? _newVersion;

    public ConfirmUpdateWindow(UpdateInfo? newVersion)
    {
        _newVersion = newVersion;

        InitializeComponent();
    }

    private void OnLoadedHandler(object sender, RoutedEventArgs e)
    { 
        update_prompt_textblock.Text = (string)Application.Current.FindResource("update_window_prompt_part1") + 
                                        " " + _newVersion + " " + (string)Application.Current.FindResource("update_window_prompt_part2");
    }

    private void DragMoveHandler(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left) DragMove();
    }

    private void CloseProgramHandler(object sender, RoutedEventArgs e) => Close();

    private void MinimizeWindowHandler(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private async void OkButtonClickHandler(object sender, RoutedEventArgs e)
    {
        var updateManager = new UpdateManager(new GithubSource("https://github.com/ADRIANTEJA/Trading-Journal", null, false));

        try
        {
            var newVersion = await updateManager.CheckForUpdatesAsync();

            if (newVersion is null) return;

            await updateManager.DownloadUpdatesAsync(newVersion);

            updateManager.ApplyUpdatesAndRestart(newVersion);
        }
        catch (Exception ex)
        {
            ErrorHandlers.HandleUpdateCheckError(ex.Message);
        } 
    }

    private void CancelButtonClickHandler(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
