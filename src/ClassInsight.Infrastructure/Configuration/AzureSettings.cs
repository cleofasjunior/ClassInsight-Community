namespace ClassInsight.Infrastructure.Configuration;

public class AzureAiSettings
{
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
}

public class AzureOpenAiSettings
{
    public string Endpoint { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
}