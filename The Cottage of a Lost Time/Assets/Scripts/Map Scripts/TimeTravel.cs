using System.Collections;
using System.Linq;
using UnityEngine;

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
    [SerializeField] private bool inTheFuture = true;
    [SerializeField] private float transitionTime = 4;

    private int transitioning = 0;
    private bool triggered = false;

    private void Start()
    {
        futureMap = FindGameObjectsInLayer((int)MapTimes.FutureMap);
        pastMap = FindGameObjectsInLayer((int)MapTimes.PastMap);

        foreach (GameObject item in futureMap)
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        if (triggered)
        {
            crystalHud.alpha += 1 / transitionTime * Time.deltaTime * transitioning;

            if (crystalHud.alpha == 1)
            {
                transitioning = -1;
            }
            else if (crystalHud.alpha == 0)
            {
                transitioning = 0;
                triggered = false;
            }
        }
    }

    public void Travel()
    {
        StartCoroutine(nameof(CrystalTransition));
        transitioning = 1;
        triggered = true;
    }

    private void ChangeTime()
    {
        if (!inTheFuture)
        {
            foreach (GameObject item in futureMap)
            {
                item.SetActive(true);
            }

            foreach (GameObject item in pastMap)
            {
                item.SetActive(false);
            }

            inTheFuture = true;
        }
        else if (inTheFuture)
        {
            foreach (GameObject item in futureMap)
            {
                item.SetActive(false);
            }

            foreach (GameObject item in pastMap)
            {
                item.SetActive(true);
            }

            inTheFuture = false;
        }
    }

    private IEnumerator CrystalTransition()
    {
        yield return new WaitForSeconds(transitionTime);
        ChangeTime();
        transitionTime /= 2;
    }

    private GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        return goArray.Where(x => x.layer == layer).ToArray();
    }
}
