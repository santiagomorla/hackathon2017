using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.XA.Feature.Social.Providers.Models
{
    public class InstagramResponseObject
    {
        public List<InstagramData> data { get; set; }
    }

    public class InstagramData
    {
        public string link { get; set; }

        public LikesComments likes { get; set; }

        public ImageList images { get; set; }

        public Caption caption { get; set; }

        public LikesComments comments { get; set; }
    }

    public class LikesComments
    {
        public int count { get; set; }
    }

    public class ImageList
    {
        public Image standard_resolution { get; set; }
    }

    public class Image
    {
        public int width { get; set; }

        public int height { get; set; }

        public string url { get; set; }
    }

    public class Caption
    {
        public string text { get; set; }

        public double created_time { get; set; }
    }
}