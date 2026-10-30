namespace FlexViewer
{
    /// <summary>
    /// Shared helper for copying bundled MauiAsset files to app storage, used by
    /// the PDF and Report sample pages. The Report Service page doesn't need it
    /// — its document comes from a remote SSRS server, not a local asset.
    /// </summary>
    internal static class ViewerAssets
    {
        public const string DefaultPdfFileName = "DefaultDocument.pdf";
        public const string SqliteDbFileName = "C1NWind.db";
        public const string FlexReportFileName = "FlexCommonTasks.flxr";

        /// <summary>
        /// Copies a MauiAsset from the app package to <see cref="Microsoft.Maui.Storage.FileSystem.AppDataDirectory"/>
        /// on first use, and returns its on-disk path. Subsequent calls with the
        /// same <paramref name="logicalName"/> return the existing copy.
        /// </summary>
        /// <param name="logicalName">The MauiAsset's LogicalName, as declared in the csproj.</param>
        /// <returns>The absolute path to the copied file.</returns>
        public static async Task<string> EnsureAssetCopiedAsync(string logicalName)
        {
            var dest = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, logicalName);

            if (!File.Exists(dest))
            {
                using var src = await FileSystem.OpenAppPackageFileAsync(logicalName);
                using var dst = File.Create(dest);
                await src.CopyToAsync(dst);
            }

            return dest;
        }
    }
}
