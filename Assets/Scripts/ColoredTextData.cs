using UnityEngine;

public class ColoredTextData
{
    private readonly string _text;
    private readonly Color _color;

    public ColoredTextData(string text, Color color)
    {
        _text = text;
        _color = color;
    }

    public string Text => _text;
    public Color Color => _color;
}