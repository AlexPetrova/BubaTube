using BubaTube.Factory.Contracts;
using BubaTube.Helpers.JSON;
using System.Text;

namespace BubaTube.Factory
{
    public class JSONHelperFactory : IJSONHelperFactory
    {
        public JSONHelperFactory()
        {
        }

        public JSONObject CreateJSONObjectInstance()
        {
            return new JSONObject(new StringBuilder());
        }

        public JSONBuilder CreateJSONBuilderInstance()
        {
            return new JSONBuilder(new StringBuilder());
        }
    }
}
