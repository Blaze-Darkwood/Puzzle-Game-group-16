using UnityEditor.ShaderGraph;
using UnityEngine;

public class CrystalColor : MonoBehaviour
{
    [SerializeField] private Material crystalColor;
    private Color customColor;
    void Start()
    {
        customColor = new Color((Color.red.r + Color.blue.r) / 2, (Color.red.g + Color.blue.g) / 2, (Color.red.b + Color.blue.b) / 2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            crystalColor.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            crystalColor.color = Color.blue;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            crystalColor.color = customColor;
        }
    }
}
