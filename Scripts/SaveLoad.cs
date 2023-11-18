using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveLoad : MonoBehaviour
{
    void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            LoadSaveCloud();
        }
    }

    private void OnEnable() => YandexGame.GetDataEvent += LoadSaveCloud;
    private void OnDisable() => YandexGame.GetDataEvent -= LoadSaveCloud;

    public void Load() => YandexGame.LoadProgress();
    public static void SaveNewData()
    {
        YandexGame.savesData.Coins = All_Upgrades.Coins;
        YandexGame.savesData.HP_modificator = All_Upgrades.HP_modificator;
        YandexGame.savesData.HP_Upgrade_Count = All_Upgrades.HP_Upgrade_Count;
        YandexGame.savesData.Coins_modificator = All_Upgrades.Coins_modificator;
        YandexGame.savesData.Coins_Upgrade_Count = All_Upgrades.Coins_Upgrade_Count;
        YandexGame.savesData.Damage_modificator = All_Upgrades.Damage_modificator;
        YandexGame.savesData.Damage_Upgrade_Count = All_Upgrades.Damage_Upgrade_Count;
        YandexGame.savesData.Speed_modificator = All_Upgrades.Speed_modificator;
        YandexGame.savesData.Speed_Upgrade_Count = All_Upgrades.Speed_Upgrade_Count;
        YandexGame.savesData.Eye_modificator = All_Upgrades.Eye_modificator;
        YandexGame.savesData.Eye_Upgrade_Count = All_Upgrades.Eye_Upgrade_Count;
        YandexGame.savesData.Jerk = All_Upgrades.Jerk;
        YandexGame.savesData.lives = All_Upgrades.lives;
        YandexGame.savesData.Double = All_Upgrades.Double;
        YandexGame.savesData.volume = All_Upgrades.volume;
        YandexGame.savesData.Hardmode = All_Upgrades.Hardmode;
        YandexGame.savesData.ManualChangedLang = All_Upgrades.ManualChangedLang;

        YandexGame.SaveProgress();
    }
    void LoadSaveCloud()
    {
        All_Upgrades.Coins = YandexGame.savesData.Coins;
        All_Upgrades.HP_modificator = YandexGame.savesData.HP_modificator;
        All_Upgrades.HP_Upgrade_Count = YandexGame.savesData.HP_Upgrade_Count;
        All_Upgrades.Coins_modificator = YandexGame.savesData.Coins_modificator;
        All_Upgrades.Coins_Upgrade_Count = YandexGame.savesData.Coins_Upgrade_Count;
        All_Upgrades.Damage_modificator = YandexGame.savesData.Damage_modificator;
        All_Upgrades.Damage_Upgrade_Count = YandexGame.savesData.Damage_Upgrade_Count;
        All_Upgrades.Speed_modificator = YandexGame.savesData.Speed_modificator;
        All_Upgrades.Speed_Upgrade_Count = YandexGame.savesData.Speed_Upgrade_Count;
        All_Upgrades.Eye_modificator = YandexGame.savesData.Eye_modificator;
        All_Upgrades.Eye_Upgrade_Count = YandexGame.savesData.Eye_Upgrade_Count;
        All_Upgrades.Jerk = YandexGame.savesData.Jerk;
        All_Upgrades.lives = YandexGame.savesData.lives;
        All_Upgrades.Double = YandexGame.savesData.Double;
        All_Upgrades.volume = YandexGame.savesData.volume;
        All_Upgrades.Hardmode = YandexGame.savesData.Hardmode;
        All_Upgrades.ManualChangedLang = YandexGame.savesData.ManualChangedLang;

    }
}
