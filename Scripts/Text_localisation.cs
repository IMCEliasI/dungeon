using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEditor;

public class Text_localisation : MonoBehaviour
{
    public void Reload_language()
    {
        string language = Current_language.LANGUAGE;
        if(language == "RUSSIAN")
        {
            Delete_Lang();
            gameObject.AddComponent<Russian>();
            GetComponent<Text>().text = GetComponent<Russian>().inscriptions[gameObject.name];
        }
        if(language == "ENGLISH")
        {
            Delete_Lang();
            gameObject.AddComponent<English>();
            GetComponent<Text>().text = GetComponent<English>().inscriptions[gameObject.name];
        }
        if (language == "KAZAKH")
        {
            Delete_Lang();
            gameObject.AddComponent<Kazakh>();
            GetComponent<Text>().text = GetComponent<Kazakh>().inscriptions[gameObject.name];
        }

    }

    void Delete_Lang()
    {
        if (GetComponent<Russian>()) { Destroy(GetComponent<Russian>()); }
        if (GetComponent<English>()) { Destroy(GetComponent<English>()); }
        if (GetComponent<Kazakh>()) { Destroy(GetComponent<Kazakh>()); }
    }

    void Start()
    {
        Reload_language();
    }
}
