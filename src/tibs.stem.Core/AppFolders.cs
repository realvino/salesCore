using Abp.Dependency;

namespace tibs.stem
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string TempFileDownloadFolder { get; set; }

        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string ProductFilePath { get; set; }

        public string FindFilePath { get; set; }

        public string ProductGroupFilePath { get; set; }

        public string ProductSubGroupFilePath { get; set; }
        public string ProfilePath { get; set; }

    }
}