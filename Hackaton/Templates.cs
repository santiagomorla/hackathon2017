using Sitecore.Data;

namespace Hackathon.XA.Feature.Social
{
    public class Templates
    {
        public Templates()
        {
        }

        public struct Instagram
        {
            public static ID ID;

            static Instagram()
            {
                ID = ID.Parse("{2016114F-4C12-4CB7-8597-4D80737C24CA}");
            }

            public struct Fields
            {
                public readonly static ID Title;

                public readonly static ID MediaLimit;

                public readonly static ID CacheInterval;

                public readonly static ID InstagramApp;

                static Fields()
                {
                    Title = new ID("{7175082B-5FBF-4ADC-84C3-D51DDDD637F4}");
                    MediaLimit = new ID("{08499631-2D06-4A33-BF3A-9632F3DDEA9D}");
                    CacheInterval = new ID("{14B5E83D-C840-4581-B030-8AB2449ECC75}");
                    InstagramApp = new ID("{15394695-BBC4-41FF-AA81-F01B75F64C30}");
                }
            }
        }

        public struct InstagramApp
        {
            public static ID ID;

            static InstagramApp()
            {
                ID = ID.Parse("{A9898B40-F002-4B19-9F72-2748FC630F9B}");
            }

            public struct Fields
            {
                public readonly static ID AccessToken;

                static Fields()
                {
                    AccessToken = new ID("{CDEDF53F-47CD-45E3-BF66-19C228C57AA5}");
                }
            }
        }
    }
}