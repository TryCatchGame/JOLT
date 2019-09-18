using UnityEngine;
internal static class GlobalProperties {
    internal static int TotalGemCount_Local {
        get {
            return PlayerPrefs.GetInt("Gem", 0);
        }
        set {
            PlayerPrefs.SetInt("Gem", value);
        }
    }

    internal static int HighScore_Local {
        get {
            return PlayerPrefs.GetInt("HighScore", 0);
        }
        set {
            PlayerPrefs.SetInt("HighScore", value);
        }
    }

    internal static string CurrentlyUsingSkinName_Local {
        get {
            return PlayerPrefs.GetString("UsingSkin", string.Empty);
        }
        set {
            PlayerPrefs.SetString("UsingSkin", value);
        }
    }

    /// <summary>
    /// True if the sound has been toggled as on.
    /// </summary>
    internal static bool SoundOn_Local {
        get {
            return PlayerPrefs.GetInt("Sound", 1) == 1;
        }
        set {
            PlayerPrefs.SetInt("Sound", value ? 1 : 0);
        }
    }
}
