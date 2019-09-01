﻿using UnityEngine;
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
}
