using KNews.Core.Persistence;
using KNews.Core.Services.Communities.Handlers;
using KNews.Core.Services.Communities.Validators;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests.Communities.Handlers
{
    [TestFixture]
    public class CommunityFindRequestHandlerTest
    {
        private const int CommunityCount = 10;
        private const int UsersCount = 10;
        private const int PostsCount = 10;

        private CoreContext _testContext;

        public CommunityFindRequestHandlerTest()
        {
            _testContext = new TestNewsContextBuilder()
                .SetUsers(UsersCount)
                .SetCommunities(CommunityCount)
                .SetPosts(PostsCount)
                .Build();
        }

        [TestCase("Community", CommunityCount)]
        [TestCase("Community 1", 1)]
        [TestCase("Not exists Community Title", 0)]
        [TestCase("Description", CommunityCount)]
        [TestCase("Description 1", 1)]
        [TestCase("Not exists Community Title Description", 0)]
        public async ValueTask Find(string text, int verify)
        {
            var handler = new CommunityFindRequestHandler(
                _testContext,
                TestTools.MockLogger<CommunityFindRequestHandler>(),
                TestTools.MockSuccessValidator<CommunityFindRequest>(),
                TestTools.MockCache());

            var request = new CommunityFindRequest()
            {
                Text = text
            };

            var response = await handler.Handle(request, new CancellationToken());
            Assert.AreEqual(response.Result.ToList().Count, verify);
        }
    }
}