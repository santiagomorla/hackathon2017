using Sitecore;
using Sitecore.Data.Items;
using Hackathon.XA.Feature.Social.Models;
using Hackathon.XA.Feature.Social.Providers;
using Hackathon.XA.Feature.Social.Providers.Models;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using System;
using System.Collections.Generic;
using Sitecore.Diagnostics;

namespace Hackathon.XA.Feature.Social.Repositories
{
    public class InstagramRepository : ModelRepository, IInstagramRepository, IModelRepository, IAbstractRepository<IRenderingModelBase>
    {
        public InstagramRepository()
        {
        }

        public override IRenderingModelBase GetModel()
        {
            Log.Info("Iniciando Instagram", this);

            InstagramRenderingModel instagramRenderingModel = new InstagramRenderingModel();

            try
            {
                this.FillBaseProperties(instagramRenderingModel);
                instagramRenderingModel.InstagramItems = this.GetInstagramItems(this.Rendering.DataSourceItem);
                instagramRenderingModel.Title = this.GetTitle();
            }
            catch (Exception exception)
            {
                Log.Error("Could not Get Instagram Items", exception, this);
            }

            return instagramRenderingModel;
        }

        protected string GetTitle()
        {
            if (this.Rendering.DataSourceItem == null)
            {
                return string.Empty;
            }
            if (this.ContentRepository.GetItem(this.Rendering.DataSourceItem[Templates.Instagram.Fields.InstagramApp] ?? string.Empty) == null)
            {
                return null;
            }
            return this.Rendering.DataSourceItem[Templates.Instagram.Fields.Title];
        }

        protected List<InstagramItem> GetInstagramItems(Item datasourceItem)
        {
            if (datasourceItem == null)
            {
                Log.Error("Instagram: this.Rendering.DataSourceItem is null", this);
                return null;
            }
            Item item = this.ContentRepository.GetItem(datasourceItem[Templates.Instagram.Fields.InstagramApp] ?? string.Empty);
            if (item == null)
            {
                Log.Error("Instagram: InstagramApp Item is null", this);
                return null;
            }

            int mediaLimit = MainUtil.GetInt(datasourceItem[Templates.Instagram.Fields.MediaLimit], 1);
            int cacheInterval = MainUtil.GetInt(datasourceItem[Templates.Instagram.Fields.CacheInterval], 0);

            return (new InstagramTimelineProvider(new SxaInstagramConfig()
            {
                AccessToken = item[Templates.InstagramApp.Fields.AccessToken],
            })).GetFeedItems(cacheInterval, mediaLimit);
        }
    }
}