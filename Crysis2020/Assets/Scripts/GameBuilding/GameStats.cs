using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameStats
{
    /// <summary>
    /// Текущее количество точек денег на планете
    /// </summary>
    private static int inGameMoneyPoint;
    /// <summary>
    /// собранная ваюта для трат
    /// </summary>
    private static int currency;

    private static int health;

    private static int enemyHealth;

    private static int virusesNow;

    private static int batsInGame;

    private static int basesInGame;

    private static int startLevel = 1;
    
    private static int currentCamera = 0;

    private static int baseHearts = 2;
    private static int currHearts;

    //
    public static bool useSmartAcceleration = false;
    public static float gSensCoef = 5f;
    public static float gSensDeadZone = 0.05f;
    public static float gSensCorrective = 0f;

    //

    public static bool useMusic = true;

    public static bool playClick = true;
    public static bool playAttackSound = true;

    public static int openLevels;

    public static bool acceptFreeRun=false;
    public static int freeRunPts;

    public static bool shootAviable = true;

    public static int GetHearts()
    {
        return currHearts;
    }

    public static void SetHearts(int count = -1)
    {
        if (count<0)
        {
            currHearts = baseHearts;
        }
        else
        {
            currHearts = count;
        }
    }

    public static void DecreaseHearts(int count = 1) 
    {
        
        UI_Control.Instance.MakeRedScreen();

        currHearts -= count;
        if (currHearts<=0)
        {
            UI_Control.Instance.ShowLooseMenu();
        }
    }




    public static int GetInGameMoneyPoint()
    {
        return inGameMoneyPoint;
    }
    public static void SetInGameMoneyPoint(int point)
    {
        inGameMoneyPoint = point;
    }
    public static void DecreaseInGameMoneyPoint(int cur = 1)
    {
        inGameMoneyPoint -= cur;
    }
    public static void IncreaseInGameMoneyPoint(int cur = 1)
    {
        inGameMoneyPoint += cur;
    }

    //
    public static int GetСurrency()
    {
        return currency;
    }
    public static void SetСurrency(int curr = 1)
    {
        currency = curr;
    }
    public static void DecreaseСurr(int cur = 1)
    {
        currency -= cur;
    }
    public static void IncreaseСurr(int cur = 1)
    {
        currency += cur;
    }

    //
    public static int GetHealth()
    {
        return health;
    }
    public static void SetHealth(int hp)
    {
        health = hp;
    }
    public static void IncreaseHealth(int count = 1)
    {
        health += count;
    }
    
    public static void DecreaseHealth(int count = 1, bool makeRed = true)
    {
        health -= count;
        if (makeRed)
        {
            UI_Control.Instance.MakeRedScreen();
        }
    }
    //
    public static int GetEnemyHealth()
    {
        return enemyHealth;
    }
    public static void SetEnemyHealth(int hp)
    {
        enemyHealth = hp;
    }
    public static void IncreaseEnemyHealth(int count = 1)
    {
        enemyHealth += count;
    }
    public static void DecreaseEnemyHealth(int count = 1, bool makeRed = true)
    {
        enemyHealth -= count;
        if (makeRed)
        {
            UI_Control.Instance.MakeRedScreen();
        }
    }

    //
    public static int GetVirusCount()
    {
        return virusesNow;
    }
    public static void SetVirus(int hp)
    {
        health = hp;
    }
    public static void DecreaseVirus(int cur = 1)
    {
        virusesNow -= cur;
    }
    public static void IncreaseVirus(int cur = 1)
    {
        virusesNow += cur;
    }

    //
    public static int GetBatsInGame()
    {
        return batsInGame;
    }
    public static void SetBat(int cur)
    {
        batsInGame = cur;
    }
    public static void DecreaseBat(int cur = 1)
    {
        batsInGame -= cur;
    }
    public static void IncreaseBat(int cur = 1)
    {
        batsInGame += cur;
    }

    //
    public static int GetBasesInGame()
    {
        return basesInGame;
    }
    public static void SetBasesInGame(int cur)
    {
        basesInGame = cur;
    }
    public static void DecreaseBasesInGame(int cur = 1)
    {
        basesInGame -= cur;
    }
    public static void IncreaseBasesInGame(int cur = 1)
    {
        basesInGame += cur;
    }




    ///////////////////////////////////////////////////////////////////////////////////////

    public override bool Equals(object obj)
    {
        throw new System.NotImplementedException();
    }

    public override int GetHashCode()
    {
        throw new System.NotImplementedException();
    }

    public static bool operator ==(GameStats left, GameStats right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GameStats left, GameStats right)
    {
        return !(left == right);
    }

    ////////
    public static int StartLevel { get => startLevel; set => startLevel = value; }

    ////

    public static int GetCurrentCamera() 
    {
        return currentCamera;
    }
    public static void  SetCurrentCamera(int camNum = 0)
    {
        currentCamera = camNum;
    }




}
