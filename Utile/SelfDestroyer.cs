using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float delayTime
    {
        get { return _delayTime; }
        set { _delayTime = value; }
    }

    [SerializeField] private float _delayTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
    }


    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(_delayTime);

        Destroy(gameObject);
    }
}
