namespace Lab.ODataEntityTypeConditions.Extensions;

public static class LoggerExtensions
{
    public static void LogInformation<T>(this ILogger logger, string message, params object[] param)
    {
        // ReSharper disable once StructuredMessageTemplateProblem
#pragma warning disable CA2017
        logger.LogInformation("{class}:::{message}",typeof(T).Name, message, param);
#pragma warning restore CA2017
    }
}