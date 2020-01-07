using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;

namespace KNews.Core.Services.UnitTests
{
    public static class TestTools
    {
        public static readonly CoreDomainOptions CoreOptions = new CoreDomainOptions()
        {
            UpdateAvailablePeriod = TimeSpan.FromHours(1),
            DefaultCommunityID = 1
        };

        public static ILogger<T> MockLogger<T>() => Mock.Of<ILogger<T>>();

        public static IDistributedCache MockCache() => Mock.Of<IDistributedCache>();

        public static IValidator<T> MockSuccessValidator<T>()
        {
            var mock = new Mock<IValidator<T>>();
            mock.Setup(e => e.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            return mock.Object;
        }

        public static IValidator<T> MockFailureValidator<T>()
        {
            var mock = new Mock<IValidator<T>>();
            mock.Setup(e => e.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            return mock.Object;
        }
    }
}