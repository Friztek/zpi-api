public class PostgresDbConfiguration
{
    public string ConnectionString { get; set; }

    public string ApiVersion { get; set; }

    public string DefaultDatabase { get; set; }

    public TimeSpan? CommandTimeout { get; set; }

    public bool EnableAutomaticMigration { get; set; }

    public Version PostgresApiVersion
    {
        get
        {
            Version result;
            return Version.TryParse(ApiVersion, out result) ? result : new Version(12, 0);
        }
    }
}