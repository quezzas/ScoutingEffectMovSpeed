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

        private static readonly bool DEFAULT_gainBonusXP = true;
        private static readonly float DEFAULT_bonusXpFactor = 1f;
        private static readonly float DEFAULT_speedFactor = 0.002f;
        private static readonly bool DEFAULT_useFlatSpeedBonus = false;
        private static readonly int[] DEFAULT_appliesTo = {1, 0, 0, 0, 0};

        public bool gainBonusXP = true;
        public float bonusXpFactor = 1f;
        public float speedFactor = 0.002f;
        public bool useFlatSpeedBonus = false;
        public int[] appliesTo = {1, 0, 0, 0, 0};

        public string GatherConfigurationData()
        {
            gainBonusXP = DEFAULT_gainBonusXP;
            speedFactor = DEFAULT_speedFactor;
            appliesTo = DEFAULT_appliesTo;

            if (!File.Exists(CONFIG_FILE_PATH))
            {
                return "Config file does not exist";
            }

            int lineNumber = -1;
            foreach (string item in File.ReadLines(CONFIG_FILE_PATH))
            {
                lineNumber++;
                if (item.StartsWith("#") || item.Length <= 1)
                    continue;

                // Invalid Line
                string[] array = item.Split('=');
                if (array.Length != 2) 
                    return "Invalid line: " + item + " @ Line number: " + lineNumber;

                switch (array[0].Trim())
                {
                    case "gainBonusXP":
                        try
                        {
                            gainBonusXP = array[1].Trim() == "true";
                        }
                        catch (Exception)
                        {
                            reset();
                            return "Invalid 'gainBonusXP' value";
                        }
                        break;
                    case "bonusXpFactor":
                        try
                        {
                            float num = float.Parse(array[1].Trim(), CultureInfo.InvariantCulture);
                            bonusXpFactor = num < 0 ? 0 : num > 1 ? 1 : num;
                        }
                        catch (Exception)
                        {
                            reset();
                            return "'bonusXpFactor' Invalid";
                        }
                        break;
                    case  "speedFactor":
                        try
                        {
                            float num = float.Parse(array[1].Trim(), CultureInfo.InvariantCulture);
                            speedFactor = num < 0f ? 0f : num > 50f ? 50f : num;
                        }
                        catch (Exception)
                        {
                            reset();
                            return "'speedFactor' Invalid";
                        }
                        break;
                    case "useFlatSpeedBonus":
                        try
                        {
                            useFlatSpeedBonus = array[1].Trim() == "true";
                        }
                        catch (Exception)
                        {
                            reset();
                            return "Invalid 'useFlatSpeedBonus' value";
                        }
                        break;
                    case  "appliesTo":
                        try
                        {
                            if (array[0] == "appliesTo")
                            {
                                string[] groups = array[1].Split(';');
                                appliesTo = new int[] { 0, 0, 0, 0, 0 };
                                foreach (string group in groups)
                                {
                                    int groupIndex = int.Parse(group.Trim(), CultureInfo.InvariantCulture) - 1;
                                    if (groupIndex < 0 || groupIndex > 4) throw new Exception();
                                    appliesTo[groupIndex] = 1;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            reset();
                            return "'appliesTo' Invalid";
                        }
                        break;
                    default:
                        reset();
                        return "Invalid option name: " + array[0] + " @ Line number: " + lineNumber;
                }
            }
            return "";
        }

        private void reset()
        {
            gainBonusXP = DEFAULT_gainBonusXP;
            speedFactor = DEFAULT_speedFactor;
            useFlatSpeedBonus = DEFAULT_useFlatSpeedBonus;
            appliesTo = DEFAULT_appliesTo;
        }
    }
}
