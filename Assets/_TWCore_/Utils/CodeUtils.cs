using System;
using UnityEngine;

namespace TWCore.Utils
{
    public static class CodeUtils
    {
        public static TextMesh CreateWorldText(string text, Transform parent = null,
            Vector3 localPosition = default(Vector3), Quaternion localRotation = default(Quaternion),
            int fontSize = 40, UnityEngine.Color? color = null, TextAnchor textAnchor = default(TextAnchor), 
            TextAlignment textAlignment = default(TextAlignment), int sortingOrder = 0)
        {
            if (color == null) color = UnityEngine.Color.white;

            var gameObject = new GameObject("World_Text", typeof(TextMesh));
            var transform = gameObject.transform;
            var textMesh = gameObject.GetComponent<TextMesh>();
            
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.rotation = localRotation;
            
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = (UnityEngine.Color)color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            
            return textMesh;
        }

        public static Vector3 GetScreenPositionToWorldPosition(Vector3 worldPos)
        {
            var v = Camera.main.ScreenToWorldPoint(worldPos);
            v.z = 0f;
            return v;
        }

        public static Vector3 GetWorldPositionToScreenPosition(Vector3 screenPos)
        {
            var v = Camera.main.WorldToScreenPoint(screenPos);
            v.z = 0f;
            return v;
        }

        public static Vector3 GetMouseWorldPosition()
        {
            var v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = 0f;
            return v;
        }

        #region COLOR UTILS
        // Returns 00-FF, value 0->255
        public static string Dec_to_Hex(int value)
        {
            return value.ToString("X2");
        }

        // Returns 0-255
        public static int Hex_to_Dec(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        // Returns a hex string based on a number between 0->1
        public static string Dec01_to_Hex(float value)
        {
            return Dec_to_Hex((int)Mathf.Round(value * 255f));
        }

        // Returns a float between 0->1
        public static float Hex_to_Dec01(string hex)
        {
            return Hex_to_Dec(hex) / 255f;
        }

        // Get Hex Color FF00FF
        public static string GetStringFromColor(UnityEngine.Color color)
        {
            string red = Dec01_to_Hex(color.r);
            string green = Dec01_to_Hex(color.g);
            string blue = Dec01_to_Hex(color.b);
            return red + green + blue;
        }

        // Get Hex Color FF00FFAA
        public static string GetStringFromColorWithAlpha(UnityEngine.Color color)
        {
            string alpha = Dec01_to_Hex(color.a);
            return GetStringFromColor(color) + alpha;
        }

        // Sets out values to Hex String 'FF'
        public static void GetStringFromColor(UnityEngine.Color color, out string red, out string green, out string blue, out string alpha)
        {
            red = Dec01_to_Hex(color.r);
            green = Dec01_to_Hex(color.g);
            blue = Dec01_to_Hex(color.b);
            alpha = Dec01_to_Hex(color.a);
        }

        // Get Hex Color FF00FF
        public static string GetStringFromColor(float r, float g, float b)
        {
            string red = Dec01_to_Hex(r);
            string green = Dec01_to_Hex(g);
            string blue = Dec01_to_Hex(b);
            return red + green + blue;
        }

        // Get Hex Color FF00FFAA
        public static string GetStringFromColor(float r, float g, float b, float a)
        {
            string alpha = Dec01_to_Hex(a);
            return GetStringFromColor(r, g, b) + alpha;
        }

        // Get Color from Hex string FF00FFAA
        public static UnityEngine.Color GetColorFromString(string color)
        {
            float red = Hex_to_Dec01(color.Substring(0, 2));
            float green = Hex_to_Dec01(color.Substring(2, 2));
            float blue = Hex_to_Dec01(color.Substring(4, 2));
            float alpha = 1f;
            if (color.Length >= 8)
            {
                // Color string contains alpha
                alpha = Hex_to_Dec01(color.Substring(6, 2));
            }
            return new UnityEngine.Color(red, green, blue, alpha);
        }
        #endregion

        #region STRING VALIDATOR UTILS

        public static string GetUppercaseAlphabetAsString() => "ABCDEFGHIJKLMNOPQRSTUVXYZ";
        public static string GetLowercaseAlphabetAsString() => "abcdefghijklmnopqrstuvxyz";
        public static string GetFullAlphabetAsString() => "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ";

        #endregion

        #region STRING MANIPULATION UTILS
        public static string NumberToOrdinal(int number)
        {
            switch (number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number + "th";
            }

            switch (number % 10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }
        #endregion
    }
}
