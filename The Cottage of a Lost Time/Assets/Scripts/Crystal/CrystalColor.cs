using UnityEngine;

public class CrystalColor : MonoBehaviour
{
    [SerializeField] private Material crystalMaterial;
    private Color crystalColor;

    private void Start()
    {
        crystalColor = crystalMaterial.color;
        Debug.Log($"{name}: {crystalColor.r}, {crystalColor.g}, {crystalColor.b}");
    }

    public Color ChangeColor(Color _addColor)
    {
        if (_addColor != Color.white)
            return MixColors(_addColor, crystalColor);
        else
            return crystalColor;
    }

    private Color MixColors(params Color[] _colors)
    {
        Color _result = new(0, 0, 0);

        foreach (Color _c in _colors)
            _result += _c;

        Debug.Log(_result / _colors.Length);
        return _result / _colors.Length;
    }
}
