using System;
using System.Windows;
using System.Windows.Input;
using Velopack;
using Velopack.Sources;

namespace UI.Windows;

/// <summary>
/// Interaction logic for ConfirmUpdateWindow.xaml
/// </summary>
public partial class ConfirmUpdateWindow : Window
{
    public ConfirmUpdateWindow()
    {
        InitializeComponent();
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
        catch (Exception)
        {
            MessageBox.Show("An error occurred while checking for updates.");
        } 
    }

    private void CancelButtonClickHandler(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
