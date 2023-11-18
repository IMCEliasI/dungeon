using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Choose_Jerk : MonoBehaviour, IPointerDownHandler
{
    public GameObject TextOnBtn;
    public GameObject AlreadyTaken;
    private bool WaitKey = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        TextOnBtn.GetComponent<Text>().text = "";
        WaitKey = true;
    }

    void Update()
    {
        AlreadyTaken.GetComponent<Text>().color = new Color(AlreadyTaken.GetComponent<Text>().color.r, AlreadyTaken.GetComponent<Text>().color.g, AlreadyTaken.GetComponent<Text>().color.b, AlreadyTaken.GetComponent<Text>().color.a - Time.deltaTime / 3);
    }
    private void OnGUI()
    {
        if (WaitKey)
        {
            if (Event.current.isKey)
            {
                KeyCode Ck = Event.current.keyCode;
                if ((Ck != KeyCode.None) && (Ck != KeyCode.Space) && (Ck != KeyCode.W) && (Ck != KeyCode.A) && (Ck != KeyCode.S) && (Ck != KeyCode.D) && (Ck != KeyCode.F) && (Ck != KeyCode.Mouse0) && (Ck != KeyCode.Mouse1) && (Ck != KeyCode.LeftControl)) 
                {
                    TextOnBtn.GetComponent<Text>().text = Event.current.keyCode.ToString();
                    WaitKey = false;
                    All_Upgrades.JerkKey = Event.current.keyCode;
                }
                else
                {
                    AlreadyTaken.GetComponent<Text>().color = new Color(AlreadyTaken.GetComponent<Text>().color.r, AlreadyTaken.GetComponent<Text>().color.g, AlreadyTaken.GetComponent<Text>().color.b, 1);
                }
            }
        }
    }
}
