using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private List<Color> requiredColors;
    [SerializeField] private Levels levelReaction;

    private enum Levels { one, two, three };
    private ITargetReaction reaction;
    private Dictionary<Color, bool> colorPairs;
    private bool targetCompleted = false;

    private void Start()
    {
        colorPairs = new Dictionary<Color, bool>();

        foreach (Color c in requiredColors)
            colorPairs.Add(c, false);

        reaction = levelReaction switch
        {
            Levels.one => gameObject.AddComponent<ReactionOne>(),
            Levels.two => gameObject.AddComponent<ReactionTwo>(),
            Levels.three => gameObject.AddComponent<ReactionThree>(),
            _ => throw new System.NotImplementedException()
        };
    }

    public void HitTarget(Color color)
    {
        if (targetCompleted) return;

        bool completed = true;

        foreach (KeyValuePair<Color, bool> kvp in colorPairs)
            if (kvp.Key == color)
            {
                colorPairs[kvp.Key] = true;
                break;
            }

        foreach (KeyValuePair<Color, bool> kvp in colorPairs)
            if (!kvp.Value)
            {
                completed = false;
                break;
            }

        if (completed)
        {
            reaction.React();
            targetCompleted = true;
        }
    }
}
