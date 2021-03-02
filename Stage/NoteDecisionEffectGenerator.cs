using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDecisionEffectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _noteEffect = null;
    [SerializeField] private GameObject _hitEffect = null;
    [SerializeField] private int _createCount = 0;
    [SerializeField] private GameObject _decisionLine = null;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    EventManager.instance.AddListener<NoteDecisionEvent>(NoteDecisionEvent);
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void NoteDecisionEvent(Note note)
    {
        for (int i = 0; i < _createCount; i++)
        {
            Instantiate(_noteEffect, new Vector3(note.transform.position.x, _decisionLine.transform.position.y), Quaternion.identity);
            Instantiate(_hitEffect, new Vector3(note.transform.position.x, _decisionLine.transform.position.y), Quaternion.identity);
        }
    }
}
