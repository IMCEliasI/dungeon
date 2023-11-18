using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class English : MonoBehaviour
{
    public Dictionary<string, string> inscriptions = new()
    {
        ["Play_TXT"] = "Play",
        ["Upgrades_TXT"] = "Upgrades",
        ["Endless_TXT"] = "Endless",
        ["Dead_TXT"] = "END",
        ["Ads_TXT"] = "Revive",
        ["Restart_TXT"] = "Restart",
        ["Back_TXT"] = "Back",
        ["Settings_TXT"] = "Settings",
        ["Victory_TXT"] = "Victory",
        ["VMenu_TXT"] = "To menu",
        ["ESC_TXT"] = "To menu",
        //// Начало улучшений
        ["HP_TXT"] = "Get +10% to HP for each level",
        ["HP_Head_TXT"] = "Health",
        ["Damage_TXT"] = "Get +10% damage for each level. At level 5 BONUS",
        ["Damage_Head_TXT"] = "Damage",
        ["Revive_TXT"] = "Get an extra life!",
        ["Revive_Head_TXT"] = "Revive",
        ["Run_TXT"] = "Get +10% to speed for each level",
        ["Run_Head_TXT"] = "Speed",
        ["Coins_Head_TXT"] = "Coins",
        ["Coins_TXT"] = "Get +10% to all subsequent coins for each level",
        ["Eye_Head_TXT"] = "Eye of Darkness",
        ["Eye_TXT"] = "Increase the radius of vision in the dark zone of the dungeon",
        ["Jerk_Head_TXT"] = "Jerk",
        ["Jerk_TXT"] = "Get the opportunity to make a dash, dealing damage to the enemies ahead",
        ["Double_Head_TXT"] = "Second jump",
        ["Double_TXT"] = "Get the opportunity to make a second jump",
        ////
        ["Press_TXT"] = "Click LMB to start",
        ["Volume_TXT"] = "Volume",
        ["Hardmode_TXT"] = "HARDMODE",
        ["JerkBut_TXT"] = "The jerk key",
        ["PressF_TXT"] = "Press F",
        ["NotAvail_TXT"] = "Unavailable",
        ["AlreadyTaken_TXT"] = "Key is busy, select another",
    };
}
