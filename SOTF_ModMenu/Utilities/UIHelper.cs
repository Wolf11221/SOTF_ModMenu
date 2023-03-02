using Il2CppSystem;
using UnityEngine;

namespace SOTF_ModMenu.Utilities
{
    public static class UIHelper
    {
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
            return Button(MakeEnable(text, state));
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
    }
}
