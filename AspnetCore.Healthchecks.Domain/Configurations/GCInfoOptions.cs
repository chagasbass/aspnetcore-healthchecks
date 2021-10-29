using System.Runtime.InteropServices;

namespace AspnetCore.Healthchecks.Domain.Configurations
{
    /// <summary>
    /// Classe que representa os dados para monitorar o consumo de memória da aplicação
    /// </summary>
    public class GCInfoOptions
    {
        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
        public static string AllocatedMemory { get; set; }
        public static string TotalAvailableMemory { get; set; }
        public static string MaxMemory { get; set; }
        public static string OperationalSystem { get; set; }
        public static string OperationalSystemArchitecture { get; set; }
        public static string ApplicationFramework { get; set; }

        public GCInfoOptions() { }

        public static void DisposeGcInfoOptions()
        {
            AllocatedMemory = string.Empty;
            TotalAvailableMemory = string.Empty;
        }

        public static void SetOperationalSystem()
        {
            OperationalSystemArchitecture = RuntimeInformation.OSArchitecture.ToString();
            OperationalSystem = RuntimeInformation.OSDescription;
            ApplicationFramework = RuntimeInformation.FrameworkDescription;
        }
    }
}
