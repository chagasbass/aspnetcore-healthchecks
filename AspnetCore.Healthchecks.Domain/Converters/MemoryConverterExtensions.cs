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

            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };

            double mod = 1024.0;

            int i = 0;

            while (newSize >= mod)
            {
                newSize /= mod;
                i++;
            }

            return Math.Round(newSize) + units[i];
        }
    }
}
