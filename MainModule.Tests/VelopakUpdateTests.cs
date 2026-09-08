using Velopack;
using Velopack.Locators;
using Velopack.Sources;

namespace MainModule.Tests;

public class VelopakUpdateTests
{
    [Fact]
    private async Task ShouldCheckForUpdates()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        var testFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "VelopackTest");
        Directory.CreateDirectory(testFolder);

        try
        {
            var locator = new TestVelopackLocator("AssetOps", "1.0.0", tempDir);

            var source = new GithubSource("https://github.com/ADRIANTEJA/Trading-Journal", null, false);

            var updateManager = new UpdateManager(source, null, locator);

            // Act
            var updateInfo = await updateManager.CheckForUpdatesAsync();

            // Assert
            Assert.NotNull(updateInfo);
            Assert.True(updateInfo.TargetFullRelease.Version > locator.CurrentlyInstalledVersion);
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
        

    }
}
