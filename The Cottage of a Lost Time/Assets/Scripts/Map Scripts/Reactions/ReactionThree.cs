using UnityEngine;

public class ReactionThree : MonoBehaviour, ITargetReaction
{
    private static GameObject[] checkpoints;
    private static GameObject target;
    private static int currentCheck = 0;

    private void Start()
    {
        Transform gO = GameObject.Find("WallCheckpoints").transform;
        checkpoints = new GameObject[gO.childCount];

        for (int i = 0; i < gO.childCount; i++)
            checkpoints[i] = gO.GetChild(i).gameObject;

        target = GameObject.Find("WallPuzzle3");
    }

    public void React()
    {
        target.SetActive(false);
    }

    public static void ReactPart()
    {
        target.transform.position = checkpoints[currentCheck++].transform.position;

        if (currentCheck >= checkpoints.Length)
            currentCheck = 0;
    }
}
