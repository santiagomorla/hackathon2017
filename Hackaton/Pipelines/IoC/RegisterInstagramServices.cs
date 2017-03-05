using Hackathon.XA.Feature.Social.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.XA.Foundation.IOC.Pipelines.IOC;

namespace Hackathon.XA.Feature.Social.Pipelines.IoC
{
    public class RegisterInstagramServices : IocProcessor
    {
        public override void Process(IocArgs args)
        {
            args.ServiceCollection.AddTransient<IInstagramRepository, InstagramRepository>();
        }
    }
}