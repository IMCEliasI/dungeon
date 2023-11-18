using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Russian : MonoBehaviour
{
    public Dictionary<string, string> inscriptions = new()
    {
        ["Play_TXT"] = "Играть",
        ["Upgrades_TXT"] = "Улучшения",
        ["Endless_TXT"] = "Бесконечно",
        ["Dead_TXT"] = "КОНЕЦ",
        ["Ads_TXT"] = "Возродиться",
        ["Restart_TXT"] = "Заново",
        ["Back_TXT"] = "Назад",
        ["Settings_TXT"] = "Настройки",
        ["Victory_TXT"] = "ПОБЕДА",
        ["VMenu_TXT"] = "В меню",
        ["ESC_TXT"] = "В меню",
        //// Начало улучшений
        ["HP_TXT"] = "Получите +10% к ХП за каждый уровень улучшения",
        ["HP_Head_TXT"] = "Здоровье",
        ["Damage_TXT"] = "Получите +10% к урону за каждый уровень улучшения. На 5 уровне БОНУС",
        ["Damage_Head_TXT"] = "Урон",
        ["Revive_TXT"] = "Получите дополнительную жизнь!",
        ["Revive_Head_TXT"] = "Возрождение",
        ["Run_TXT"] = "Получите +10% к скорости за каждый уровень улучшения",
        ["Run_Head_TXT"] = "Скорость",
        ["Coins_Head_TXT"] = "Монеты",
        ["Coins_TXT"] = "Получите +10% ко всем последующим монетам за каждый уровень улучшения",
        ["Eye_Head_TXT"] = "Око тьмы",
        ["Eye_TXT"] = "Увеличьте радиус зрения в тёмной зоне подземелья",
        ["Jerk_Head_TXT"] = "Рывок",
        ["Jerk_TXT"] = "Получите возможность совершать рывок, нанося урон врагам впереди",
        ["Double_Head_TXT"] = "Второй прыжок",
        ["Double_TXT"] = "Получите возможность совершать второй прыжок",
        ////
        ["Press_TXT"] = "Нажмите ЛКМ, чтобы начать",
        ["Volume_TXT"] = "Громкость",
        ["Hardmode_TXT"] = "Сложный режим",
        ["JerkBut_TXT"] = "Клавиша рывка",
        ["PressF_TXT"] = "Нажмите F",
        ["NotAvail_TXT"] = "Недоступно",
        ["AlreadyTaken_TXT"] = "Клавиша занята, выберите другую",
    };
}
