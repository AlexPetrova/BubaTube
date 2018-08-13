using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubaTube.Helpers.JSON
{
    public class JSONBuilder
    {
        private StringBuilder content;

        public JSONBuilder(StringBuilder content)
        {
            this.content = content;
        }

        public void AddJSONArray(string name, IEnumerable<JSONObject> objects)
        {
            this.content.AppendFormat(@"""{0}"":", name);

            this.content.Append("[");
            
            var asStringArray = objects
                .Select(x => x.ToString())
                .ToArray();

            this.content.Append(string.Join(',', asStringArray));

            this.content.Append("]");
        }
        
        public override string ToString()
        {
            this.content.Insert(0, "{");
            this.content.Append("}");

            return this.content.ToString();
        }
    }
}
