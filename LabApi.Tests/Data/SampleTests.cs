using LabApi.Data.Models;
using LabApi.Data.Repositories;

namespace LabApi.Tests.Data
{
    public class SampleTests
    {
        [Fact]
        public async Task WhenGetSample_ThenReturnNull()
        {
            #region Arrange

            var sampleRepo = new SampleRepository();

            #endregion

            #region Act

            var result = await sampleRepo.GetSampleByNameAsync("Test");

            #endregion

            #region Assert

            Assert.Null(result);

            #endregion
        }

        [Fact]
        public async Task WhenGetSamples_ThenReturnEmpty()
        {
            #region Arrange

            var sampleRepo = new SampleRepository();

            #endregion

            #region Act

            var result = await sampleRepo.GetSamplesAsync();

            #endregion

            #region Assert

            Assert.Empty(result);

            #endregion
        }

        [Fact]
        public async Task WhenAddAndGetSample_ThenReturnSample()
        {
            #region Arrange

            var sampleRepo = new SampleRepository();
            var sample = new Sample()
            {
                Name = "Test",
                Owner = Guid.NewGuid(),
                Position = 1
            };

            #endregion

            #region Act

            await sampleRepo.AddSampleAsync(sample);
            var result = await sampleRepo.GetSampleByNameAsync(sample.Name);

            #endregion

            #region Assert

            Assert.NotNull(result);

            #endregion
        }
    }
}