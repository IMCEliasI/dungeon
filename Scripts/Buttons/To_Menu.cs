using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class To_Menu : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioListener.pause = true;
        SaveLoad.SaveNewData();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    
}
