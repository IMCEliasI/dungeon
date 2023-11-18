using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    float bias = 0f;
    void Update()
    {
        bias = 5f * Time.unscaledDeltaTime;
        transform.localPosition = transform.localPosition + new Vector3(0, bias);

        if(transform.localPosition.y >= -2f)
        {
            transform.localPosition = new(transform.localPosition.x, -27);
        }
    }
}
