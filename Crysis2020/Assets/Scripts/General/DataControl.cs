using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataControl 
{
    private const string useSmartAccelerationName = "useSmartAcceleration";
    private const string gSensCorrectiveName = "gSensCorrective";
    private const string gSensCoefName = "gSensCoef";
    private const string musicName = "useMusic";
    private const string openLevelsName = "openLevels";
    private const string currentCameraName = "currentCamera";
    private const string acceptFreeRunName = "acceptFreeRun";


    public static void SaveData()
    {
        
        PlayerPrefs.SetInt(useSmartAccelerationName, BoolToInt(GameStats.useSmartAcceleration));
        PlayerPrefs.SetFloat(gSensCoefName, GameStats.gSensCoef);
        PlayerPrefs.SetFloat(gSensCorrectiveName, GameStats.gSensCorrective);
        PlayerPrefs.SetInt(musicName, BoolToInt(GameStats.useMusic));
        PlayerPrefs.SetInt(openLevelsName, GameStats.openLevels);
        PlayerPrefs.SetInt(currentCameraName, GameStats.GetCurrentCamera());
        PlayerPrefs.SetInt(acceptFreeRunName, BoolToInt(GameStats.acceptFreeRun)); 
        PlayerPrefs.Save();

    }

    public static void LoadData()
    {
        if (PlayerPrefs.HasKey(useSmartAccelerationName))
        {
            GameStats.useSmartAcceleration = IntToBool(PlayerPrefs.GetInt(useSmartAccelerationName));
        }

        if (PlayerPrefs.HasKey(gSensCoefName))
        {
            GameStats.gSensCoef = PlayerPrefs.GetFloat(gSensCoefName);
        }
        if (PlayerPrefs.HasKey(musicName))
        {
            GameStats.useMusic = IntToBool(PlayerPrefs.GetInt(musicName));
        }
        if (PlayerPrefs.HasKey(openLevelsName))
        {
            GameStats.openLevels = PlayerPrefs.GetInt(openLevelsName);
        }
        if (PlayerPrefs.HasKey(currentCameraName))
        {
            GameStats.SetCurrentCamera(PlayerPrefs.GetInt(currentCameraName));
        }
        if (PlayerPrefs.HasKey(acceptFreeRunName))
        {
            GameStats.acceptFreeRun = IntToBool(PlayerPrefs.GetInt(acceptFreeRunName));
        }
        if (PlayerPrefs.HasKey(gSensCorrectiveName))
        {
            GameStats.gSensCorrective = PlayerPrefs.GetFloat(gSensCorrectiveName);
        }

    }

    private static bool IntToBool(int var)
    {
        if (var == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static int BoolToInt(bool var)
    {
        if (var)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}
