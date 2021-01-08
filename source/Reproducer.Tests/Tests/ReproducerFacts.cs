namespace Reproducer.Tests
{
    using FluentAssertions;
    using Xunit;

    using Reproducer;

    public class ReproducerFacts
    {
        [Fact]
        public void Reproduces_DoesSomething()
        {
            var counter = new Counter(1);

            counter.Count.Should().Be(1);
        }
    }
}
