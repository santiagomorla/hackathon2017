using System.Collections.Generic;
using Hackathon.XA.Feature.Social.Providers.Models;
using Sitecore.XA.Foundation.Mvc.Models;

namespace Hackathon.XA.Feature.Social.Models
{
    public class InstagramRenderingModel : RenderingModelBase
    {
        public string Title
        {
            get;
            set;
        }

        public List<InstagramItem> InstagramItems
        {
            get;
            set;
        }

        public InstagramRenderingModel()
        {
        }
    }
}