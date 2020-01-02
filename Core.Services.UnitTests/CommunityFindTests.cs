using KNews.Core.Persistence;
using KNews.Core.Services.CommunityRequests;
using KNews.Core.Services.CommunityValidators;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests
{
    [TestFixture]
    public class CommunityFindTests
    {
        private const int CommunityCount = 10;
        private const int UsersCount = 10;
        private const int PostsCount = 10;

        private NewsContext _testContext;

        public CommunityFindTests()
        {
            _testContext = new TestNewsContextBuilder()
                .SetUsers(UsersCount)
                .SetCommunities(CommunityCount)
                .SetPosts(PostsCount)
                .Build();
        }

        [Test]
        public async ValueTask FindValidation()
        {
            var request = new CommunityFindRequest();
            var validator = new CommunityFindValidator();
            var result = await validator.ValidateAsync(request);
            Assert.True(result.IsValid);
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