using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;

public class ArrowEffect : MonoBehaviour
{
    [SerializeField] private float _minRotation = 0;
    [SerializeField] private float _maxRotation = 0;

    [SerializeField] private Vector2 _minScale = Vector2.zero;
    [SerializeField] private Vector2 _maxScale = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(_minRotation, _maxRotation));
        transform.localScale = new Vector2((Random.value > 0.5 ? -1 : 1) *  Random.Range(_minScale.x, _maxScale.y), Random.Range(_minScale.y, _maxScale.y));
    }
}
