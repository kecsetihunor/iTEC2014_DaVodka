using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace iTEC.Providers
{
    public class DisplayValueAttribute : Attribute
    {
        public String Value { get; private set; }

        public DisplayValueAttribute(String value)
        {
            Value = value;
        }
    }

    public static class EnumExtensions
    {
        public static String GetDisplayValue(this Enum value)
        {
            var type = value.GetType();
            var info = type.GetField(value.ToString());
            var attrs = info.GetCustomAttributes(typeof(DisplayValueAttribute), false) as DisplayValueAttribute[];
            return (attrs.Length > 0)
                ? attrs[0].Value
                : "";
        }
    }

    public static class DataUriSchemeProvider
    {
        private enum DataType
        {
            [DisplayValue("image/png")]
            ImagePNG
        }

        public static String CreatePNGImage(String path, Int32 width = 0, Int32 height = 0)
        {
            using (var bitmap = new Bitmap(path))
            {
                if (width > 0 && height > 0)
                {
                    float scale = Math.Min(width / bitmap.Width, height / bitmap.Height);
                    var scaleWidth = (int)(bitmap.Width * scale);
                    var scaleHeight = (int)(bitmap.Height * scale);
                    using (var image = new Bitmap(width, height))
                    {
                        using (var graphics = Graphics.FromImage(image))
                        {
                            graphics.FillRectangle(Brushes.AliceBlue, new Rectangle(0, 0, width, height));
                            graphics.DrawImage(bitmap, new Rectangle(0, 0, width, height));
                            graphics.Save();
                        }
                        return CreatePNGImage(image);
                    }
                }
                else
                {
                    return CreatePNGImage(bitmap);
                }
            }
        }

        public static String CreatePNGImage(Bitmap image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return CreateData(DataType.ImagePNG, stream.ToArray());
            }
        }

        public static String CreatePNGImage(Byte[] buffer)
        {
            return CreateData(DataType.ImagePNG, buffer);
        }

        private static String CreateData(DataType type, Byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                return null;
            }
            return "data:" + type.GetDisplayValue() + ";base64," + Convert.ToBase64String(buffer);
        }
    }
}