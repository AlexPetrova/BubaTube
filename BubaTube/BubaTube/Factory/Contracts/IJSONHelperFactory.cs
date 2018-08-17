using BubaTube.Helpers.JSON;

namespace BubaTube.Factory.Contracts
{
    public interface IJSONHelperFactory
    {
        JSONObject CreateJSONObjectInstance();

        JSONBuilder CreateJSONBuilderInstance();
    }
}
