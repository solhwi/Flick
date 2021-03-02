using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    public void OnClickReset()
    {
        float growX = transform.localScale.x - 15f * Time.deltaTime;
        float growY = transform.localScale.y - 15f * Time.deltaTime;

        transform.localScale = new Vector3(growX, growY, transform.localScale.z);
    }
}
