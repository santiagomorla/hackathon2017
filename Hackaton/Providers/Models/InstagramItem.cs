using Sitecore;
using Sitecore.Globalization;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
namespace Hackathon.XA.Feature.Social.Providers.Models
{
    public class InstagramItem
    {
        public string Link { get; set; }

        public int Likes { get; set; }

        public string ImageUrl { get; set; }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public string Text { get; set; }

        public double Timestamp { get; set; }

        public int Comments { get; set; }

        public string HtmlEmbed { get; set; }
    }
}