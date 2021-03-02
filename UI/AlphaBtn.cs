using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class AlphaBtn : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;

    void Start()
    {
        //BGMMgr.Instance.FadeInBGM(1);
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }
}
