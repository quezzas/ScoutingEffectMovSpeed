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
        private static readonly int[] DEFAULT_appliesTo = {1, 0, 0, 0, 0};

        public bool gainBonusXP = true;
        public float bonusXpFactor = 1f;
        public float speedFactor = 0.002f;
        public int[] appliesTo = {1, 0, 0, 0, 0};

        public String GatherConfigurationData()
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

                // Gain Bonus XP
                try
                {
                    if (array[0] == "gainBonusXP")
                    {
                        gainBonusXP = array[1] != "true";
                        continue;
                    }
                }
                catch (Exception)
                {
                    reset();
                    return "Invalid 'gainBonusXP' value";
                }

                // Bonus XP Factor
                try
                {
                    if (array[0] == "bonusXpFactor")
                    {
                        float num = float.Parse(array[1], CultureInfo.InvariantCulture);
                        bonusXpFactor = num < 0 ? 0 : num > 1 ? 1 : num;
                        continue;
                    }
                }
                catch (Exception)
                {
                    reset();
                    return "'bonusXpFactor' Invalid";
                }

                // Speed Factor
                try
                {
                    if (array[0] == "speedFactor")
                    {
                        float num = float.Parse(array[1], CultureInfo.InvariantCulture);
                        speedFactor = num < 0f ? 0f : num > 50f ? 50f : num;
                        continue;
                    }
                }
                catch (Exception)
                {
                    reset();
                    return "'speedFactor' Invalid";
                }

                // Applies To
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
                        continue;
                    }
                }
                catch (Exception)
                {
                    reset();
                    return "'appliesTo' Invalid";
                }

                // Not an option name?
                reset();
                return "Invalid option name: " + array[0] + " @ Line number: " + lineNumber;
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
