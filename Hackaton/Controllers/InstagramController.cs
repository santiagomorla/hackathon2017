using Hackathon.XA.Feature.Social.Repositories;
using Sitecore.XA.Foundation.Mvc.Controllers;

namespace Hackathon.XA.Feature.Social.Controllers
{
    public class InstagramController : StandardController
    {
        private readonly IInstagramRepository _repository;
        public InstagramController(IInstagramRepository repository)
        {
            _repository = repository;
        }
        protected override object GetModel()
        {
            return _repository.GetModel();
        }
    }
}