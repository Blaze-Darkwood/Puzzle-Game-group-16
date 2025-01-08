using UnityEditor.ShaderGraph;
using UnityEngine;

public class CrystalColor : MonoBehaviour
{
    [SerializeField] private Material crystalColor;
    private Color customColor;
    private Color baseColor;
    private Color lightColor;

    private float timer = 1f;
    void Start()
    {
        baseColor = Color.red;
        lightColor = Color.blue;
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

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            crystalColor.color = baseColor;
            timer = 1f;
        }
    }

    public void ChangeColor()
    {
        crystalColor.color = customColor;
    }

    private void ChangeCustomColor()
    {
        customColor = new Color((baseColor.r + lightColor.r) / 2, (baseColor.g + lightColor.g) / 2, (baseColor.b + lightColor.b) / 2);
    }
}
