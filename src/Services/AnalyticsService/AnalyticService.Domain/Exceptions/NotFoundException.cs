namespace AnalyticService.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, string key) :
        base("Entity with name " + entityName + " and key " + key + " not found")
    {
    }
}