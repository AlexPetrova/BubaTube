using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubaTube.Helpers.JSON
{
    public class JSONObject
    {
        private StringBuilder content;

        public JSONObject(StringBuilder content)
        {
            this.content = content;
        }

        public void AddProperty(string name, string value)
        {
            this.content.AppendFormat(@"{""{0}"": ""{1}""}", name, value);
        }
        
        public override string ToString()
        {
            return this.content.ToString(); 
        }
    }
}
