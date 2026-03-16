using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ScoutingEffectMovSpeed
{
    public class ConfigurationManager
    {
        private static readonly string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string rootPath = Directory.GetParent(Directory.GetParent(assemblyPath).ToString()).ToString();
        private static readonly string CONFIG_FILE_PATH = Path.Combine(rootPath, "CTXP.config");

        private static readonly bool DEFAULT_SEMScostumSpEXP = true;
        private static readonly float DEFAULT_SEMSFactor = 0.002f;
        private static readonly string DEFAULT_SEMSappliedto = "1";
        private static readonly bool DEFAULT_gainBonusXP = true;
        private static readonly float DEFAULT_bonusXpFactor = 1f;
        private static readonly int[] DEFAULT_appliesTo = {1, 0, 0, 0, 0};

        public bool SEMScostumSpEXP = true;
        public float SEMSFactor = 0.002f;
        public string SEMSappliedto = "1";
        public bool gainBonusXP = true;
        public float bonusXpFactor = 1f;
        public int[] appliesTo = {1, 0, 0, 0, 0};

        public String GatherConfigurationData()
        {
            SEMScostumSpEXP = DEFAULT_SEMScostumSpEXP;
            SEMSFactor = DEFAULT_SEMSFactor;
            SEMSappliedto = DEFAULT_SEMSappliedto;
            gainBonusXP = DEFAULT_gainBonusXP;
            appliesTo = DEFAULT_appliesTo;

            if (!File.Exists(CONFIG_FILE_PATH))
            {
                return "Config file does not exist";
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
                    if (array[0] == "gainBonusXP" && array[1] != "true")
                    {
                        gainBonusXP = false;
                    }
                }
                catch (Exception)
                {
                    reset();
                    return "Invalid 'gainBonusXP' value";
                }

                    {
                        SEMScostumSpEXP = false;
                    }
                    if (array[0] == "SEMSFactor")
                try
                {
                    if (array[0] == "speedFactor")
                    {
                        float num = float.Parse(array[1], CultureInfo.InvariantCulture);
                        if (num > 50f) num = 50f;
                        if (num < 0f) num = 0f;
                        speedFactor = num;
                    }
                }
                catch (Exception)
                {
                    reset();
                    return "'speedFactor' Invalid";
                }

                try
                {
                    if (array[0] == "appliesTo")
                    {
                        string[] groups = array[1].Split(';');
                        appliesTo = new int[] { 0, 0, 0, 0, 0 };
                        foreach (string group in groups)
                        {
                            switch (group.Trim())
                            {
                                case "1": // Player
                                    appliesTo[0] = 1;
                                    break;
                                case "2": // Same faction
                                    appliesTo[1] = 1;
                                    break;
                                case "3": // Has scout
                                    appliesTo[2] = 1;
                                    break;
                                case "4": // All Lord Parties
                                    appliesTo[3] = 1;
                                    break;
                                case "5": // Player Clan
                                    appliesTo[4] = 1;
                                    break;
                                default: // huh?
                                    throw new Exception();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    reset();
                    return "'appliesTo' Invalid";
                }
            }
            return "";
        }
        
        private void reset()
        {
            gainBonusXP = DEFAULT_gainBonusXP;
            appliesTo = DEFAULT_appliesTo;
            speedFactor = DEFAULT_speedFactor;
            appliesTo = DEFAULT_appliesTo;
        }
    }
}
