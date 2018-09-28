namespace tibs.stem
{
    public interface IAppFolders
    {
        string TempFileDownloadFolder { get; }

        string SampleProfileImagesFolder { get; }

        string WebLogsFolder { get; set; }        
       
        string ProductFilePath { get; set; }

        string FindFilePath { get; set; }

        string ProductGroupFilePath { get; set; }

        string ProductSubGroupFilePath { get; set; }
        string ProfilePath { get; set; }

    }
}