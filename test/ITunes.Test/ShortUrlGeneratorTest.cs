using ITunes.UrlShortener.Services;
using Xunit;

namespace ITunes.Test
{
    public class ShortUrlGeneratorTest
    {
        [Fact]
        public void ShortUrlGenerator_NotNull()
        {
            var code = ShortUrlGenerator.Generate();
            Assert.NotNull(code);
        }
        
        [Fact]
        public void ShortUrlGenerator_Code_Length_Greater_Then_One()
        {
            var code = ShortUrlGenerator.Generate();
            Assert.True(code.Length > 1, "code length to be greater than 1");
        }
    }
}
