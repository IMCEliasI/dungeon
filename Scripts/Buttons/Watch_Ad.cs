using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class Watch_Ad : MonoBehaviour, IPointerDownHandler
{
    public YandexGame YaSDK;
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioListener.pause = true;
        YaSDK._RewardedShow(1);
    }
}
