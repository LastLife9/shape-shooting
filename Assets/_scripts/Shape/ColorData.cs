using System.Collections.Generic;

[System.Serializable]
public class ColorData
{
    public float r;
    public float g;
    public float b;
}

[System.Serializable]
public class ColorDataList
{
    public List<ColorData> colors;
}