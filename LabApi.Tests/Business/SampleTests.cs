using LabApi.Business.Exceptions;
using LabApi.Business.Services;
using LabApi.Data.Interfaces;
using LabApi.Data.Models;
using Moq;

namespace LabApi.Tests.Business
{
    public class SampleTests
    {
        [Fact]
        public async Task WhenCreateSample_ThenSampleCreated()
        {
            #region Arrange

            var sampleRepoMock = new Mock<ISampleRepository>();

            var sample = new Sample()
            {
                Name = "Test",
                Owner = Guid.NewGuid(),
                Position = 1
            };

            Sample? nullSample = null;
            sampleRepoMock
                .Setup(repo => repo.GetSampleByNameAsync(sample.Name))
                .ReturnsAsync(nullSample);

            var sampleService = new SampleService(sampleRepoMock.Object);

            #endregion

            #region Act

            var result = await sampleService.CreateSampleAsync(sample.Name, sample.Owner, sample.Position);

            #endregion

            #region Assert

            Assert.Equal(sample.Name, result.Name);

            #endregion
        }

        [Fact]
        public async Task WhenCreateSampleDuplicate_ThenThrowsException()
        {
            #region Arrange

            var sampleRepoMock = new Mock<ISampleRepository>();

            var sample = new Sample()
            {
                Name = "Test",
                Owner = Guid.NewGuid(),
                Position = 1
            };

            Sample? existingSample = new Sample();
            sampleRepoMock
                .Setup(repo => repo.GetSampleByNameAsync(sample.Name))
                .ReturnsAsync(existingSample);

            var sampleService = new SampleService(sampleRepoMock.Object);

            #endregion

            #region Act and Assert

            await Assert.ThrowsAsync<DuplicateSampleException>(async ()
                => await sampleService.CreateSampleAsync(sample.Name, sample.Owner, sample.Position));

            #endregion
        }
    }
}