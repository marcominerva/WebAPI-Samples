namespace CalendarApi.BusinessLayer.Settings
{
    public class AppSettings
    {
        public bool IsRunningOnAzure { get; init; }

        public string StorageFolder { get; init; }

        public string ContainerName { get; init; }
    }
}
