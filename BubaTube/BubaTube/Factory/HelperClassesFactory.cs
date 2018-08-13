﻿using BubaTube.Helpers.JSON;
using System.Text;

namespace BubaTube.Factory
{
    public class HelperClassesFactory
    {
        public static JSONObject CreateJSONObjectInstance()
        {
            return new JSONObject(new StringBuilder());
        }
    }
}
