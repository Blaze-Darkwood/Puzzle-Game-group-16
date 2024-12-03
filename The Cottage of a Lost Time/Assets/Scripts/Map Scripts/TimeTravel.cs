using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Linq;
using UnityEngine.UIElements;
using UnityEngine.UI;

enum MapTimes
{
    PastMap = 6,
    FutureMap = 7
}

public class TimeTravel : MonoBehaviour
{
    [SerializeField] private CanvasGroup crystalHud;
    [SerializeField] private GameObject[] futureMap;
    [SerializeField] private GameObject[] pastMap;

    private void Start()
    {
        futureMap = FindGameObjectsInLayer((int)MapTimes.FutureMap);
        pastMap = FindGameObjectsInLayer((int)MapTimes.PastMap);
    }


    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
}
