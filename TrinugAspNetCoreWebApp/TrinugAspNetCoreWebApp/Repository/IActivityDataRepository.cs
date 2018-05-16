namespace TrinugAspNetCoreWebApp.Repository
{
    public interface IActivityDataRepository
    {
        int CreateActivity(ActivityDataModel activity);
        ActivityDataModel GetActivity(int id);
    }
}
