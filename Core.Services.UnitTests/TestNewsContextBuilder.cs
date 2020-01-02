using KNews.Core.Entities;
using KNews.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace KNews.Core.Services.UnitTests
{
    public class TestNewsContextBuilder
    {
        private NewsContext _testContext;

        private int _usersCount;
        private Func<int, User> _usersFunc;

        private int _communitiesCount;
        private Func<int, Community> _communitiesFunc;

        private int _postsCount;
        private Func<int, Post> _postsFunc;

        private Func<int, int, EXUserCommunityType?> _userCommunityFunc;
        private Func<int, int, bool> _postCommunityFunc;

        public TestNewsContextBuilder SetUsers(int count, Func<int, User> func = null)
        {
            _usersCount = count;
            _usersFunc = func;
            return this;
        }

        public TestNewsContextBuilder SetCommunities(int count, Func<int, Community> func = null)
        {
            _communitiesCount = count;
            _communitiesFunc = func;
            return this;
        }

        public TestNewsContextBuilder SetPosts(int count, Func<int, Post> func = null)
        {
            _postsCount = count;
            _postsFunc = func;
            return this;
        }

        public TestNewsContextBuilder SetUserInCommunity(Func<int, int, EXUserCommunityType?> func)
        {
            _userCommunityFunc = func;
            return this;
        }

        public TestNewsContextBuilder SetPostInCommunity(Func<int, int, bool> func)
        {
            _postCommunityFunc = func;
            return this;
        }

        public NewsContext Build()
        {
            var testContextOptions = new DbContextOptionsBuilder<NewsContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;

            _testContext = new NewsContext(testContextOptions);

            for (int i = 0; i < _usersCount; i++)
            {
                var user = (_usersFunc == null)
                    ? new User()
                    {
                        FirstName = $"FirstName {i}",
                        LastName = $"LastName {i}",
                        MiddleName = $"MiddleName {i}",
                        About = $"About {i}",
                    } : _usersFunc(i);
                _testContext.Users.Add(user);
            }

            for (int i = 0; i < _communitiesCount; i++)
            {
                var community = (_communitiesFunc == null)
                    ? new Community()
                    {
                        Title = $"Community {i}",
                        Rules = $"Rule {i}",
                        Description = $"Description {i}"
                    }
                    : _communitiesFunc(i);
                _testContext.Communities.Add(community);
            }

            for (int i = 0; i < _postsCount; i++)
            {
                var post = (_postsFunc == null) ?
                    new Post()
                    {
                        Title = $"",
                        Content = $"Content {i} Full Content",
                        ShortContent = $"Content {i}",
                        Status = (EPostStatus)(i % 4),
                    } : _postsFunc(i);
                _testContext.Posts.Add(post);
            }

            if (_userCommunityFunc != null)
                for (int ui = 0; ui < _usersCount; ui++)
                {
                    for (int ci = 0; ci < _communitiesCount; ci++)
                    {
                        var state = _userCommunityFunc(ui, ci);
                        if (state == null) continue;
                        _testContext.XCommunityUsers.Add(new XCommunityUser(ui, ci, state.Value));
                    }
                }

            if (_postCommunityFunc != null)
                for (int pi = 0; pi < _postsCount; pi++)
                {
                    for (int ci = 0; ci < _communitiesCount; ci++)
                    {
                        var state = _postCommunityFunc(pi, ci);
                        if (state) continue;
                        _testContext.XCommunityPosts.Add(new XCommunityPost(pi, ci));
                    }
                }

            _testContext.SaveChanges();

            return _testContext;
        }
    }
}