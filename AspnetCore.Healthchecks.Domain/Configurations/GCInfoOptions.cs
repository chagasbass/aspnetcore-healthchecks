namespace AspnetCore.Healthchecks.Domain.Configurations
{
    public class GCInfoOptions
    {
        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
        public static string AllocatedMemory { get; set; }
        public static string TotalAvailableMemory { get; set; }
        public static string MaxMemory { get; set; }

        public GCInfoOptions() { }

        public static void DisposeGcInfoOptions()
        {
            AllocatedMemory = string.Empty;
            TotalAvailableMemory = string.Empty;
        }
    }
}
