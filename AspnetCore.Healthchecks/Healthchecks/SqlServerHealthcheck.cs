using AspnetCore.Healthchecks.Domain.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Healthchecks
{
    /// <summary>
    /// Checador de Saúde para Banco de dados Sql Server
    /// </summary>
    public class SqlServerHealthcheck : IHealthCheck
    {
        private readonly BaseConfigurationOptions _baseConfigurationOptions;

        public SqlServerHealthcheck(IOptionsMonitor<BaseConfigurationOptions> options)
        {
            _baseConfigurationOptions = options.CurrentValue;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            using var conexao = new SqlConnection(_baseConfigurationOptions.DatabaseConn);

            try
            {
                await conexao.OpenAsync();
            }
            catch
            {
                return new HealthCheckResult(
                HealthStatus.Unhealthy,
                description: HealthNames.SQLSERVER_DESCRIPTION);
            }

            return new HealthCheckResult(
               HealthStatus.Healthy,
               description: HealthNames.SQLSERVER_DESCRIPTION);
        }
    }
}
