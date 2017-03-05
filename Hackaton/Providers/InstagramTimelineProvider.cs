using Sitecore.Diagnostics;
using Hackathon.XA.Feature.Social.Providers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Hackathon.XA.Feature.Social.Providers
{
    public class InstagramTimelineProvider : AbstractFeedProvider<InstagramItem>, IDisposable
    {
        private const string ApiUrl= "https://api.instagram.com/v1/users/self/media/recent/?access_token={0}&count={1}";
        private const string EmbedUrl = "https://api.instagram.com/oembed/?url={0}";

        private readonly string _accessToken;

        protected override string CacheKey
        {
            get
            {
                return string.Concat("InstagramTimeline_", this._accessToken);
            }
        }
        

        public InstagramTimelineProvider(SxaInstagramConfig config) : this(config.AccessToken)
        {
           
        }

        public InstagramTimelineProvider(string accessToken)
        {
            this._accessToken = accessToken;
        }

        protected List<InstagramItem> ParseInstagramResponse(string response)
        {
            var list = new List<InstagramItem>();
            try
            {
                if (!string.IsNullOrEmpty(response))
                {
                    var instagramObject = JsonConvert.DeserializeObject<InstagramResponseObject>(response);

                    if (instagramObject != null)
                    {
                        foreach (var data in instagramObject.data)
                        {
                            var item = new InstagramItem();
                            item.Link = data.link;
                            item.Likes = data.likes.count;
                            item.ImageUrl = data.images.standard_resolution.url;
                            item.ImageWidth = data.images.standard_resolution.width;
                            item.ImageHeight = data.images.standard_resolution.height;
                            item.Text = data.caption.text;
                            item.Timestamp = data.caption.created_time;
                            item.Comments = data.comments.count;
                            item.HtmlEmbed = this.GetPostEmbed(data.link);

                            list.Add(item);
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Log.Error("Instagram: Error on ParseInstagramResponse", ex, this);
            }

            return list;
        }

        protected string ParseInstagramEmbedResponse(string response)
        {
            try
            {
                if (!string.IsNullOrEmpty(response))
                {
                    var instagramObject = JsonConvert.DeserializeObject<InstagramEmbed>(response);

                    if (instagramObject != null)
                    {
                        return instagramObject.html;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Instagram: Error on ParseInstagramEmbedResponse", ex, this);
            }

            return string.Empty;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            
        }
        
        protected override List<InstagramItem> GetFeedItemsInternal(int count)
        {
            List<InstagramItem> instagramItems = new List<InstagramItem>(); ;
            try
            {
                if (!string.IsNullOrEmpty(this._accessToken))
                {
                    instagramItems = this.GetUserPosts(count);
                }
            }
            catch (Exception exception)
            {
                Log.Error("Could not execute Instagram API", exception, this);
            }
            return instagramItems;
        }

        protected List<InstagramItem> GetUserPosts(int count)
        {
            var request = WebRequest.Create(string.Format(ApiUrl, this._accessToken, count));
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();
            if (dataStream == null) return null;

            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return ParseInstagramResponse(responseFromServer);
        }

        protected string GetPostEmbed(string link)
        {
            var request = WebRequest.Create(string.Format(EmbedUrl, link));
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();
            if (dataStream == null) return null;

            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return ParseInstagramEmbedResponse(responseFromServer);
        }
    }
}