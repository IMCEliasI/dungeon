using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Description : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite Knife_blood;
    public Sprite Revive_Three_Hearts;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = transform.localScale * 1.2f;
        transform.Find("Desc").gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new(1, 1);
        transform.Find("Desc").gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.name == "HP" && All_Upgrades.Coins >= 100)
        {
            All_Upgrades.HP_modificator += 0.1f;
            All_Upgrades.Coins -= 100;

            All_Upgrades.HP_Upgrade_Count += 1;
            transform.Find("Level").GetComponent<Text>().text = All_Upgrades.HP_Upgrade_Count.ToString();
        }


        if (gameObject.name == "Revive" && All_Upgrades.lives < 2 && All_Upgrades.Coins >= 1000)
        {
            All_Upgrades.lives += 1;
            All_Upgrades.Coins -= 1000;

            transform.Find("Level").GetComponent<Text>().text = All_Upgrades.lives.ToString();
            if(All_Upgrades.lives == 2)
            {
                transform.Find("Level").GetComponent<Text>().text = "2(MAX)";
                GetComponent<Image>().sprite = Revive_Three_Hearts;
            }
        }


        if (gameObject.name == "Damage" && All_Upgrades.Coins >= 100)
        {
            All_Upgrades.Damage_modificator += 0.1f;
            All_Upgrades.Coins -= 100;

            All_Upgrades.Damage_Upgrade_Count += 1;
            transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Damage_Upgrade_Count.ToString();

            if(All_Upgrades.Damage_Upgrade_Count == 5)
            {
                GetComponent<Image>().sprite = Knife_blood;
            }
        }


        if (gameObject.name == "Run" && All_Upgrades.Coins >= 100)
        {
            All_Upgrades.Speed_modificator += 0.1f;
            All_Upgrades.Coins -= 100;

            All_Upgrades.Speed_Upgrade_Count += 1;
            transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Speed_Upgrade_Count.ToString();
        }

        if (gameObject.name == "Eye" && All_Upgrades.Coins >= 250)
        {
            All_Upgrades.Eye_modificator += 0.1f;
            All_Upgrades.Coins -= 250;

            All_Upgrades.Eye_Upgrade_Count += 1;
            transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Eye_Upgrade_Count.ToString();
        }

        if (gameObject.name == "Coins" && All_Upgrades.Coins >= 100)
        {
            All_Upgrades.Coins_modificator += 0.1f;
            All_Upgrades.Coins -= 100;

            All_Upgrades.Coins_Upgrade_Count += 1;
            transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Coins_Upgrade_Count.ToString();
        }

        if (gameObject.name == "Jerk" && !All_Upgrades.Jerk && All_Upgrades.Coins >= 2000)
        {
            All_Upgrades.Jerk = true;
            All_Upgrades.Coins -= 2000;

            transform.Find("Level").GetComponent<Text>().text = "(MAX)";
        }

        if (gameObject.name == "Double" && !All_Upgrades.Double && All_Upgrades.Coins >= 300)
        {
            All_Upgrades.Double = true;
            All_Upgrades.Coins -= 300;

            transform.Find("Level").GetComponent<Text>().text = "(MAX)";
        }
    }
    void Start()
    {
        if (gameObject.name == "Damage" && All_Upgrades.Damage_Upgrade_Count >= 5)
        { 
            transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Damage_Upgrade_Count.ToString(); 
            GetComponent<Image>().sprite = Knife_blood; 
        }
        if (gameObject.name == "Damage" && All_Upgrades.Damage_Upgrade_Count < 5) { transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Damage_Upgrade_Count.ToString(); }



        if (gameObject.name == "Revive" && All_Upgrades.lives == 2) 
        {
            transform.Find("Level").GetComponent<Text>().text = "2(MAX)";
            GetComponent<Image>().sprite = Revive_Three_Hearts; 
        }
        if (gameObject.name == "Revive" && All_Upgrades.lives != 2) { transform.Find("Level").GetComponent<Text>().text = All_Upgrades.lives.ToString(); }



        if(gameObject.name == "HP") { transform.Find("Level").GetComponent<Text>().text = All_Upgrades.HP_Upgrade_Count.ToString(); }
        if (gameObject.name == "Run") { transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Speed_Upgrade_Count.ToString(); }
        if (gameObject.name == "Coins") { transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Coins_Upgrade_Count.ToString(); }
        if (gameObject.name == "Eye") { transform.Find("Level").GetComponent<Text>().text = All_Upgrades.Eye_Upgrade_Count.ToString(); }

        if (gameObject.name == "Jerk" && All_Upgrades.Jerk) { transform.Find("Level").GetComponent<Text>().text = "(MAX)"; }
        if (gameObject.name == "Double" && All_Upgrades.Double) { transform.Find("Level").GetComponent<Text>().text = "(MAX)"; }
    }
}
