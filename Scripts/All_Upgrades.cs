using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Upgrades : MonoBehaviour
{
    public static int Coins = 0;

    //улучшения
    public static float HP_modificator = 1f;
    public static int HP_Upgrade_Count = 0;

    public static float Coins_modificator = 1f;
    public static int Coins_Upgrade_Count = 0;

    public static float Damage_modificator = 1f;
    public static int Damage_Upgrade_Count = 0;

    public static float Speed_modificator = 1f;
    public static int Speed_Upgrade_Count = 0;

    public static float Eye_modificator = 1f;
    public static int Eye_Upgrade_Count = 0;

    public static bool Jerk = false;

    public static int lives = 0;

    public static bool Double = false;
    /// да, тут ещё и глобальные переменные

    public static float volume = 1f;
    public static bool Hardmode = false;

    public static KeyCode JerkKey = KeyCode.LeftShift;
    public static bool ManualChangedLang = false;
}
