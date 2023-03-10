using Il2CppSystem;
using UnityEngine;

namespace SOTF_ModMenu.Utilities
{
    public static class UIHelper
    {
        public static GUIStyle StringStyle { get; set; } = new(GUI.skin.label);
        private static float
            x, y,
            width, height,
            margin,
            controlHeight,
            controlDist,
            nextControlY;

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
            GUI.Box(new Rect(x, y, width, height), text);
        }

        private static Rect NextControlRect()
        {
            Rect r = new Rect(x + margin, nextControlY + y, width - margin * 2, controlHeight);
            nextControlY += controlHeight + controlDist;
            return r;
        }

        public static string MakeEnable(string text, bool state)
        {
            return string.Format("{0}{1}", text, state ? "<color=green>ON</color>" : "<color=red>OFF</color>");
        }

        public static bool Button(string text, bool state)
        {
            bool newState  = Button(MakeEnable(text, state));
            return newState != state;
        }

        public static bool Button(string text)
        {
            return GUI.Button(NextControlRect(), text);
        }

        public static void Label(string text, float value, int decimals = 2)
        {
            Label(string.Format("{0}{1}", text, Math.Round(value, 2).ToString()));
        }

        public static void Label(string text)
        {
            GUI.Label(NextControlRect(), text);
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
            GUI.Label(new Rect(centered ? (position - vector) / 2f : position, vector), guicontent, StringStyle);
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
    }
}
