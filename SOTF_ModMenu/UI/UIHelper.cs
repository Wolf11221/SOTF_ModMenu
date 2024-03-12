using Il2CppSystem;
using UnityEngine;
using Action = System.Action;

namespace SOTF_ModMenu.UI;

public class UIHelper
{
    public static GUIStyle StringStyle { get; set; } = new(GUI.skin.label);
    
    private static float
        x, y,
        width, height,
        margin,
        controlHeight,
        controlDist,
        nextControlY;
    
    private static Texture2D _panelOff;
    private static Texture2D _panelOn;

    private static GUIStyle _boxStyle;
    private static GUIStyle _titlebarStyle;
    public static GUIStyle TextFieldStyle;
    private static GUIStyle _centeredLabelStyle;
    public static GUIStyle ButtonStyle;
    public static GUIStyle SpawnerButtonStyle;
    public static GUIStyle PanelStyle;
    
    /*
     * To Do:
     * - Cleanup
     * - Make better gui creation or even fully automatic system
     * - Change how buttons work so the state bool gets changed in the button function
     * - Allow gui customization or preset style selection
     */
    
    public static void InitializeStyles()
    {
        _panelOff =  MakeTex(1, 1, new Color(82/255f, 82/255f, 82/255f));
        _panelOff.hideFlags = HideFlags.HideAndDontSave;
        
        _panelOn = MakeTex(1, 1, new Color(231 / 255f, 18 / 255f, 0 / 255f));
        _panelOn.hideFlags = HideFlags.HideAndDontSave;
        
        PanelStyle = new GUIStyle(GUI.skin.label);
        PanelStyle.normal.background = _panelOff;
        PanelStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;
        
        _boxStyle = new GUIStyle(GUI.skin.box);
        _boxStyle.normal.background = MakeTex(1, 1, new Color(42/255f, 42/255f, 42/255f));
        _boxStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;

        _titlebarStyle = new GUIStyle(GUI.skin.box);
        _titlebarStyle.normal.background = MakeTex(1, 1, new Color(231 / 255f, 18 / 255f, 0 / 255f));
        _titlebarStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;

        _centeredLabelStyle = new GUIStyle(GUI.skin.label);
        _centeredLabelStyle.alignment = TextAnchor.MiddleCenter;
        
        SpawnerButtonStyle = new GUIStyle(GUI.skin.button);
        SpawnerButtonStyle.normal.background = Texture2D.blackTexture;
        SpawnerButtonStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;
        SpawnerButtonStyle.hover.background = MakeTex(1, 1, new Color(18/255f, 18/255f, 18/255f));
        SpawnerButtonStyle.hover.background.hideFlags = HideFlags.HideAndDontSave;
        SpawnerButtonStyle.active.background = MakeTex(1, 1, new Color(18/255f, 18/255f, 18/255f));
        SpawnerButtonStyle.active.background.hideFlags = HideFlags.HideAndDontSave;
        
        ButtonStyle = new GUIStyle(GUI.skin.button);
        ButtonStyle.alignment = TextAnchor.MiddleLeft;
        ButtonStyle.padding = new RectOffset(4, 0, 0, 0);
        ButtonStyle.normal.background = Texture2D.blackTexture;
        ButtonStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;
        ButtonStyle.hover.background = MakeTex(1, 1, new Color(18/255f, 18/255f, 18/255f));
        ButtonStyle.hover.background.hideFlags = HideFlags.HideAndDontSave;
        ButtonStyle.active.background = MakeTex(1, 1, new Color(18/255f, 18/255f, 18/255f));
        ButtonStyle.active.background.hideFlags = HideFlags.HideAndDontSave;

        TextFieldStyle = new GUIStyle(GUI.skin.textField);
        TextFieldStyle.normal.background = MakeTex(2, 2, new Color(28/255f, 28/255f, 28/255f));
        TextFieldStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;
        TextFieldStyle.hover.background = MakeTex(2, 2, new Color(18/255f, 18/255f, 18/255f));
        TextFieldStyle.hover.background.hideFlags = HideFlags.HideAndDontSave;
        TextFieldStyle.focused.background = MakeTex(2, 2, new Color(28/255f, 28/255f, 28/255f));
        TextFieldStyle.focused.background.hideFlags = HideFlags.HideAndDontSave;
    }
    
    public static void Begin(string text, float _x, float _y, float _width, float _height, float _margin, float _controlHeight, float _controlDist)
    {
        x = _x;
        y = _y;
        width = _width;
        height = _height;
        margin = _margin;
        controlHeight = _controlHeight;
        controlDist = _controlDist;
        nextControlY = 20f;
        
        GUI.Box(new Rect(x, y, width, height), "", _boxStyle);
        GUI.Box(new Rect(x, y, width, 20), $"", _titlebarStyle);
        GUI.Label(new Rect(x, y, width, 20), $"<b>{text}</b>", _centeredLabelStyle);
    }

    private static Rect NextControlRect()
    {
        Rect r = new Rect(x + margin, nextControlY + y, width - margin * 2, controlHeight);
        nextControlY += controlHeight + controlDist;
        return r;
    }

    static Color onColor = new Color(231 / 255f, 18 / 255f, 0 / 255f);
    static Color offColor = Color.white;

    private static string MakeEnable(string text, bool state)
    {
        PanelStyle.normal.background = state ? _panelOn : _panelOff;
        
        Color textColor = state ? onColor : offColor;
        
        int r = (int)(textColor.r * 255f);
        int g = (int)(textColor.g * 255f);
        int b = (int)(textColor.b * 255f);
        
        string colorTag = $"#{r:X2}{g:X2}{b:X2}";
        
        return $"<color={colorTag}>{text}</color>";
    }
    
    public static void Button(string text, bool state, Action function)
    {
        Rect nextControlRect = NextControlRect();
        bool clicked = GUI.Button(nextControlRect, MakeEnable(text, state), ButtonStyle);
        GUI.Label(new Rect(nextControlRect.xMax - 6f, nextControlRect.y + 3f, 3f, nextControlRect.height - 6f), GUIContent.none, PanelStyle);
        if (clicked) function.Invoke();
    }
    
    public static bool Button(string text)
    {
        return GUI.Button(NextControlRect(), text, ButtonStyle);
    }
    
    public static void Label(string text, float value, int decimals = 2)
    {
        Label(string.Format("{0}{1}", text, Math.Round(value, 2).ToString()));
    }

    public static void Label(string text)
    {
        GUI.Label(NextControlRect(), text, _centeredLabelStyle);
    }

    public static float Slider(float val, float min, float max)
    {
        return GUI.HorizontalSlider(NextControlRect(), val, min, max);
    }
    
    /// <summary>
    /// Draw string on screen
    /// </summary>
    /// <param name="position"></param>
    /// <param name="label"></param>
    /// <param name="color"></param>
    /// <param name="fontSize"></param>
    /// <param name="centered"></param>
    /// <returns>returns true given no errors occured</returns>
    public static bool DrawString(Vector2 position, string label, Color color, int fontSize, bool centered = true)
    {
        GUI.color = color;
        StringStyle.fontSize = fontSize;
        StringStyle.normal.textColor = color;
        var guicontent = new GUIContent(label);
        var vector = StringStyle.CalcSize(guicontent);
        GUI.Label(new Rect(centered ? position - vector / 2f : position, vector), guicontent, StringStyle);
        return true;
    }
    public static void DrawLine(Vector2 start, Vector2 end, Color color, float width)
    {
        GUI.depth = 0;
        var num = (float)57.29577951308232;
        var vector = end - start;
        var num2 = num * Mathf.Atan(vector.y / vector.x);
        if (vector.x < 0f) num2 += 180f;
        var num3 = (int)Mathf.Ceil(width / 2f);
        GUIUtility.RotateAroundPivot(num2, start);
        GUI.color = color;
        GUI.DrawTexture(new Rect(start.x, start.y - num3, vector.magnitude, width), Texture2D.whiteTexture,
            ScaleMode.StretchToFill);
        GUIUtility.RotateAroundPivot(-num2, start);
    }

    public static void DrawBox(Vector2 position, Vector2 size, Color color, bool centered = true)
    {
        if (centered) position -= size / 2f;
        GUI.color = color;
        GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, ScaleMode.StretchToFill);
    }

    public static void DrawBoxOutline(Vector2 Point, float width, float height, Color color)
    {
        DrawLine(Point, new Vector2(Point.x + width, Point.y), color, 2f);
        DrawLine(Point, new Vector2(Point.x, Point.y + height), color, 2f);
        DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x + width, Point.y), color, 2f);
        DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x, Point.y + height), color, 2f);
    }
    
    private static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        var result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}