using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kazakh : MonoBehaviour
{
    public Dictionary<string, string> inscriptions = new()
    {
        ["Play_TXT"] = "Ойнау",
        ["Upgrades_TXT"] = "Жақсартулар",
        ["Endless_TXT"] = "шексіздік",
        ["Dead_TXT"] = "соңы",
        ["Ads_TXT"] = "Қайта туылу",
        ["Restart_TXT"] = "Қайта",
        ["Back_TXT"] = "Артқа",
        ["Settings_TXT"] = "Параметрлер",
        ["Victory_TXT"] = "Жеңіс",
        ["VMenu_TXT"] = "Мәзірде",
        ["ESC_TXT"] = "Мәзірде",
        //// Начало улучшений
        ["HP_TXT"] = "Жақсартудың әр деңгейі үшін + 10% HP алыңыз",
        ["HP_Head_TXT"] = "Денсаулық",
        ["Damage_TXT"] = "Жақсартудың әр деңгейі үшін + 10% зиян алыңыз. 5 деңгейде БОНУС",
        ["Damage_Head_TXT"] = "Зақым",
        ["Revive_TXT"] = "Қосымша өмір алыңыз!",
        ["Revive_Head_TXT"] = "Қайта өрлеу",
        ["Run_TXT"] = "Жақсартудың әр деңгейі үшін + 10% жылдамдықты алыңыз",
        ["Run_Head_TXT"] = "Жылдамдық",
        ["Coins_Head_TXT"] = "Монеталар",
        ["Coins_TXT"] = "Әрбір жақсарту деңгейі үшін барлық кейінгі монеталарға +10% алыңыз",
        ["Eye_Head_TXT"] = "Қараңғылық көзі",
        ["Eye_TXT"] = "Қараңғы зындан аймағында көру радиусын арттырыңыз",
        ["Jerk_Head_TXT"] = "Серпіліс",
        ["Jerk_TXT"] = "Алда жауларға зиян келтіру арқылы серпіліс жасауға мүмкіндік алыңыз",
        ["Double_Head_TXT"] = "Екінші секіру",
        ["Double_TXT"] = "Екінші секіруге мүмкіндік алыңыз",
        ////
        ["Press_TXT"] = "Бастау үшін LMB түймесін басыңыз",
        ["Volume_TXT"] = "Көлемі",
        ["Hardmode_TXT"] = "Күрделі режим",
        ["JerkBut_TXT"] = "Серпіліс пернесі",
        ["PressF_TXT"] = "Басыңыз F",
        ["NotAvail_TXT"] = "қол жетімді емес",
        ["AlreadyTaken_TXT"] = "Кілт бос емес, басқасын таңдаңыз",
    };
}
