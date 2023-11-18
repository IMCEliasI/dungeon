using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Change_Language : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Current_language.LANGUAGE = name;
        All_Upgrades.ManualChangedLang = true;
        SaveLoad.SaveNewData();
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }
}
