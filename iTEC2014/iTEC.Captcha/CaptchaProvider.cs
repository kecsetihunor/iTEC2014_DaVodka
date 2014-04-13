using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace iTEC.Captcha
{
    public static class CaptchaProvider
    {
        private class CaptchaQueueEntry
        {
            public Int32 Id { get; set; }
            public DateTime Date { get; set; }
            public String Text { get; set; }
        }

        private static Int32 id;
        private static List<CaptchaQueueEntry> queue;
        private static Random random;
        private static String characters;

        static CaptchaProvider()
        {
            id = 1;
            queue = new List<CaptchaQueueEntry>();
            random = new Random();
            characters = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
        }

        public static CaptchaImage CreateCaptcha(Int32 width, Int32 height)
        {
            var text = GenerateText(characters, 5);

            var captcha = new CaptchaImage()
            {
                Id = ++id,
                Image = GenerateImage(text, width, height)
            };

            if (!CaptchaEntryExists(id))
            {
                lock (queue)
                {
                    queue.Add(new CaptchaQueueEntry()
                    {
                        Id = captcha.Id,
                        Date = DateTime.Now,
                        Text = text
                    });
                }
            }

            return captcha;
        }

        public static Boolean Validate(Int32 id, String text)
        {
            lock (queue)
            {
                var entry = GetCaptchaEntry(id);
                var valid = entry != null;
                if (valid)
                {
                    valid = entry.Text.Equals(text, StringComparison.InvariantCultureIgnoreCase);
                    queue.Remove(entry);
                }
                return valid;
            }
        }

        private static Boolean CaptchaEntryExists(Int32 id)
        {
            return GetCaptchaEntry(id) != null;
        }

        private static CaptchaQueueEntry GetCaptchaEntry(Int32 id)
        {
            return queue.FirstOrDefault(x => x.Id == id);
        }

        private static String GenerateText(String characters, Int32 count)
        {
            var text = new StringBuilder(count);

            for (var index = 0; index < count; ++index)
            {
                text.Append(characters[random.Next(characters.Length - 1)]);
            }

            return text.ToString();
        }

        private static Bitmap GenerateImage(String text, Int32 width, Int32 height)
        {
            var V = 4F;

            var fonts = new List<FontFamily>
            {
                new FontFamily("Times New Roman"),
                new FontFamily("Georgia"),
                new FontFamily("Arial"),
                new FontFamily("Comic Sans MS")
            };
            var color = Color.FromArgb(0x34, 0x98, 0xdb);
            var background = Color.White;
            var family = fonts[random.Next(fonts.Count - 1)];

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                using (var brush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, background))
                {
                    graphics.FillRectangle(brush, rect);

                    var format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip,
                        Trimming = StringTrimming.None
                    };

                    format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, text.Length) });

                    var size = RectangleF.Empty;
                    Single fontSize = rect.Height + 1;
                    Font font = null;
                    do
                    {
                        if (font != null)
                        {
                            font.Dispose();
                        }
                        fontSize--;
                        font = new Font(family, fontSize, FontStyle.Bold);
                        size = graphics.MeasureCharacterRanges(text, font, rect, format)[0].GetBounds(graphics);
                    } while (size.Width > rect.Width || size.Height > rect.Height);

                    var path = new GraphicsPath();
                    path.AddString(text, font.FontFamily, (Int32)font.Style, graphics.DpiY * font.Size / 72, rect, format);

                    PointF[] points =
                    {
                        new PointF(
                            random.Next(rect.Width)/V,
                            random.Next(rect.Height)/V),
                        new PointF(
                            rect.Width - random.Next(rect.Width)/V,
                            random.Next(rect.Height)/V),
                        new PointF(
                            random.Next(rect.Width)/V,
                            rect.Height - random.Next(rect.Height)/V),
                        new PointF(
                            rect.Width - random.Next(rect.Width)/V,
                            rect.Height - random.Next(rect.Height)/V)
                    };

                    var matrix = new Matrix();
                    matrix.Translate(0F, 0F);
                    path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

                    using (var hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, color))
                    {
                        graphics.FillPath(hatchBrush, path);

                        var max = Math.Max(rect.Width, rect.Height);
                        for (var index = 0; index < (Int32)(rect.Width * rect.Height / 30F); ++index)
                        {
                            var x = random.Next(rect.Width);
                            var y = random.Next(rect.Height);
                            var w = random.Next(max / 50);
                            var h = random.Next(max / 50);
                            graphics.FillEllipse(hatchBrush, x, y, w, h);
                        }
                    }
                    font.Dispose();
                }
                graphics.Save();
            }
            return bitmap;
        }
    }
}
