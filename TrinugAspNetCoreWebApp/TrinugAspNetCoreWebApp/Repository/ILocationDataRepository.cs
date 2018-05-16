namespace TrinugAspNetCoreWebApp.Repository
{
    public interface ILocationDataRepository
    {
        LocationDataModel LookupLocationByAlias(string alias);
    }
}
