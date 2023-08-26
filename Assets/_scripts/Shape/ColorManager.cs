using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;

    public Color[] Colors;
    private static List<ColorData> colors = new List<ColorData>();

    private void Awake()
    {
        Instance = this;

        LoadColorsFromJSON();
    }

    [ContextMenu("Write colors to JSON")]
    private void AddColors()
    {
        for (int i = 0; i < Colors.Length; i++)
        {
            Color curentColor = Colors[i];
            colors.Add(new ColorData { r = curentColor.r, b = curentColor.b, g = curentColor.g });
        }

        string json = JsonUtility.ToJson(new ColorDataList { colors = colors }, true);
        File.WriteAllText(Application.dataPath + "/colors.json", json);
    }

    private List<ColorData> LoadColorsFromJSON()
    {
        string path = Application.dataPath + "/colors.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ColorDataList colorDataList = JsonUtility.FromJson<ColorDataList>(json);
            colors = colorDataList.colors;
            return colors;
        }
        else
        {
            AddColors();
            LoadColorsFromJSON();
            return colors;
        }
    }

    public Color[] GetColorsArray()
    {
        if (colors.Count == 0) LoadColorsFromJSON();

        Color[] colorsArray = new Color[colors.Count];
        for (int i = 0; i < colorsArray.Length; i++)
        {
            colorsArray[i] = new Color(colors[i].r, colors[i].g, colors[i].b);
        }
        return colorsArray;
    }

    public Color GetRandomColor()
    {
        if (colors.Count == 0) LoadColorsFromJSON();

        ColorData colorData = colors[Random.Range(0, colors.Count)];
        Color color = new Color(colorData.r, colorData.g, colorData.b);
        return color;
    }
}