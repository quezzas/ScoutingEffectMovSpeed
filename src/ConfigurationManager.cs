using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ScoutingEffectMovSpeed
{
    internal class ConfigurationManager
    {
        private static readonly string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string rootPath = Directory.GetParent(Directory.GetParent(assemblyPath).ToString()).ToString();
        private static readonly string CONFIG_FILE_PATH = Path.Combine(rootPath, "CTXP.config");

        private static readonly bool DEFAULT_SEMScostumSpEXP = true;
        private static readonly float DEFAULT_SEMSFactor = 0.002f;
        private static readonly string DEFAULT_SEMSappliedto = "1";

        public bool SEMScostumSpEXP = true;
        public float SEMSFactor = 0.002f;
        public string SEMSappliedto = "1";

        public bool GatherConfigurationData()
        {
            SEMScostumSpEXP = DEFAULT_SEMScostumSpEXP;
            SEMSFactor = DEFAULT_SEMSFactor;
            SEMSappliedto = DEFAULT_SEMSappliedto;

            if (!File.Exists(CONFIG_FILE_PATH))
            {
                return false;
            }

            foreach (string item in File.ReadLines(CONFIG_FILE_PATH))
            {
                if (item.StartsWith("#") || item.Length <= 1)
                    continue;

                string[] array = item.Split('=');
                if (array.Length != 2)
                    continue;

                try
                {
                    if (array[0] == "SEMScostumSpEXP" && array[1] != "true")
                    {
                        SEMScostumSpEXP = false;
                    }
                    if (array[0] == "SEMSFactor")
                    {
                        float num = float.Parse(array[1], CultureInfo.InvariantCulture);
                        if (num > 50f) num = 50f;
                        if (num < 0f) num = 0f;
                        SEMSFactor = num;
                    }
                    if (array[0] == "SEMSappliedto")
                    {
                        SEMSappliedto = array[1];
                    }
                }
                catch (Exception)
                {
                    SEMScostumSpEXP = DEFAULT_SEMScostumSpEXP;
                    SEMSFactor = DEFAULT_SEMSFactor;
                    SEMSappliedto = DEFAULT_SEMSappliedto;
                    return false;
                }
            }
            return true;
        }
    }
}
