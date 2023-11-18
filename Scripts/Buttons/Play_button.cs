using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Play_button : MonoBehaviour, IPointerDownHandler
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioListener.pause = true;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
