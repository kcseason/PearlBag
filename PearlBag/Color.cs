using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearlBag
{
    class BtnColor
    {
        public BtnColor(Color cl)
        {
            Color = cl;
        }
        public BtnColor()
        {
            _name = "";
            _value = "blank";
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _name = ColorUtility.GetColorText(_value);
                _color = ColorUtility.GetColor(_value);
            }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _value = Color.Name;
                _name = ColorUtility.GetColorText(_value);
            }
        }
    }

    class ColorUtility
    {
        public static List<Color> GetColor4Canvas()
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(ColorUtility.BlackBG());
            colorList.Add(ColorUtility.BlueBG());
            colorList.Add(ColorUtility.DarkBlueBG());
            colorList.Add(ColorUtility.DarkGreenBG());
            colorList.Add(ColorUtility.LimeBG());
            colorList.Add(ColorUtility.GrayBG());
            colorList.Add(ColorUtility.PinkBG());
            colorList.Add(ColorUtility.DeepPinkBG());
            colorList.Add(ColorUtility.GoldBG());
            colorList.Add(ColorUtility.PurpleBG());
            colorList.Add(ColorUtility.MilkWhiteBG());
            colorList.Add(ColorUtility.RedBG());
            colorList.Add(ColorUtility.DarkRedBG());

            return colorList;
        }

        public static List<Color> GetColor4Pearl()
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(ColorUtility.Blue());
            colorList.Add(ColorUtility.DarkBlue());
            colorList.Add(ColorUtility.Black());
            colorList.Add(ColorUtility.DarkGreen());
            colorList.Add(ColorUtility.Lime());
            colorList.Add(ColorUtility.Gray());
            colorList.Add(ColorUtility.Pink());
            colorList.Add(ColorUtility.DeepPink());
            colorList.Add(ColorUtility.Gold());
            colorList.Add(ColorUtility.Purple());
            //colorList.Add(ColorUtility.White());
            colorList.Add(ColorUtility.MilkWhite());
            colorList.Add(ColorUtility.Red());
            colorList.Add(ColorUtility.DarkRed());

            return colorList;
        }

        public static Color Blue()
        {
            Color cl = Color.DodgerBlue;
            return cl;
        }
        public static Color BlueBG()
        {
            Color cl = Color.DeepSkyBlue;
            return cl;
        }
        public static Color DarkBlue()
        {
            Color cl = Color.DarkBlue;
            return cl;
        }
        public static Color DarkBlueBG()
        {
            Color cl = Color.Blue;
            return cl;
        }

        public static Color Black()
        {
            Color cl = Color.Black;
            return cl;
        }

        public static Color BlackBG()
        {
            Color cl = Color.DimGray;
            return cl;
        }

        public static Color DarkGreen()
        {
            Color cl = Color.DarkGreen;
            return cl;
        }
        public static Color DarkGreenBG()
        {
            Color cl = Color.Green;
            return cl;
        }

        public static Color Lime()
        {
            Color cl = Color.Lime;
            return cl;
        }
        public static Color LimeBG()
        {
            Color cl = Color.ForestGreen;
            return cl;
        }
        public static Color Gray()
        {
            Color cl = Color.SaddleBrown;
            return cl;
        }
        public static Color GrayBG()
        {
            Color cl = Color.Chocolate;
            return cl;
        }
        public static Color Pink()
        {
            Color cl = Color.Pink;
            return cl;
        }
        public static Color PinkBG()
        {
            Color cl = Color.HotPink;
            return cl;
        }
        public static Color DeepPink()
        {
            Color cl = Color.DeepPink;
            return cl;
        }
        public static Color DeepPinkBG()
        {
            Color cl = Color.PaleVioletRed;
            return cl;
        }
        public static Color Gold()
        {
            Color cl = Color.Gold;
            return cl;
        }
        public static Color GoldBG()
        {
            Color cl = Color.Orange;
            return cl;
        }
        public static Color DarkOrange()
        {
            Color cl = Color.DarkOrange;
            return cl;
        }
        public static Color Purple()
        {
            Color cl = Color.Purple;
            return cl;
        }
        public static Color PurpleBG()
        {
            Color cl = Color.DarkViolet;
            return cl;
        }
        public static Color White()
        {
            Color cl = Color.White;
            return cl;
        }

        public static Color MilkWhite()
        {
            Color cl = Color.PapayaWhip;
            return cl;
        }
        public static Color MilkWhiteBG()
        {
            Color cl = Color.FloralWhite;
            return cl;
        }
        public static Color Red()
        {
            Color cl = Color.Red;
            return cl;
        }
        public static Color RedBG()
        {
            Color cl = Color.IndianRed;
            return cl;
        }
        public static Color DarkRed()
        {
            Color cl = Color.DarkRed;
            return cl;
        }
        public static Color DarkRedBG()
        {
            Color cl = Color.Firebrick;
            return cl;
        }
        public static Color Invisible()
        {
            Color cl = Color.Transparent;
            return cl;
        }

        public static string GetColorText(string color)
        {
            switch (color)
            {
                case "DodgerBlue": return "蓝色";
                case "DeepSkyBlue": return "蓝色";
                case "DarkBlue": return "深蓝";
                case "Blue": return "深蓝";
                case "Red": return "纯红";
                case "IndianRed": return "纯红";
                case "DarkRed": return "深红";
                case "Firebrick": return "深红";
                case "Black": return "黑色";
                case "DimGray": return "黑色";
                case "DarkGreen": return "深绿";
                case "Green": return "深绿";
                case "Lime": return "亮绿色";
                case "ForestGreen": return "亮绿色";
                case "Pink": return "粉红色";
                case "HotPink": return "粉红色";
                case "DeepPink": return "深粉红";
                case "PaleVioletRed": return "深粉红";
                case "SaddleBrown": return "咖啡色";
                case "Chocolate": return "咖啡色";
                case "Gold": return "金黄色";
                case "Orange": return "金黄色";
                case "DarkOrange": return "橙色";
                case "Purple": return "紫色";
                case "DarkViolet": return "紫色";
                case "White": return "白色";
                case "PapayaWhip": return "乳白色";
                case "FloralWhite": return "乳白色";
                case "Transparent": return "透明";
            }
            return "未知颜色";
        }

        public static Color GetColor(string color)
        {
            return System.Drawing.ColorTranslator.FromHtml(color);
        }
    }
}
