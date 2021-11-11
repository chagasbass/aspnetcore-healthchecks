using System;

namespace AspnetCore.Healthchecks.Domain.Converters
{
    /// <summary>
    /// Extensão para efetuar a conversão para mostrar o valor de memória com seus
    /// respectivas unidades
    /// </summary>
    public static class MemoryConverterExtensions
    {
        public static string ConvertMemorySize(long size)
        {
            var newSize = Convert.ToDouble(size);

            var units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };

            var mod = 1024.0;

            var iSizeCount = 0;

            while (newSize >= mod)
            {
                newSize /= mod;
                iSizeCount++;
            }

            return Math.Round(newSize) + units[iSizeCount];
        }
    }
}
