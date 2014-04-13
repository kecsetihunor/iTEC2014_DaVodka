using System;
using System.ComponentModel.DataAnnotations;

namespace iTEC.WebApplication.Captcha.Models
{
    public class CaptchaSizeModel
    {
        public Int32 Width { get; set; }
        public Int32 Height { get; set; }
    }

    public class CaptchaModel
    {
        [Required]
        public Int32 Id { get; set; }
    }

    public class CaptchaImageModel : CaptchaModel
    {
        [Required]
        public String Image { get; set; }
    }

    public class CaptchaTextModel : CaptchaModel
    {
        [Required]
        public String Text { get; set; }
    }
}