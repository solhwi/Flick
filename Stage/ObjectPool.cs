using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField] private GameObject poolingObjectPrefab;
    Queue<Note> poolingObjectQueue = new Queue<Note>();

    private void Awake()
    {
        Instance = this;
        //Initialize(ReadCSV.Instance.noteList.Count);
    }

    public void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Note CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Note>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Note GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(Instance.transform);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(Instance.transform);
            return newObj;
        }
    }
    public static void ReturnObject(Note obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}

