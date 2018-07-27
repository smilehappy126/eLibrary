using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Common
{
    public class ConfigTool
    {
        public static string GetDBConnectionString(string connectKey )
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectKey].ConnectionString.ToString();
        }

        public static string GetAppSetting(string settingName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[settingName];
        }
    }
}
