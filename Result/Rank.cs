using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public GameObject[] Ranks;

    public void SetActiveRank()
    {
        Ranks[ResultSlave.Instance.Rank].SetActive(true);
    }
}
