using LabTec.Common.Interfaces;
using LabTec.Core.Entities;
using LabTec.Infrastructure.Repositories;
using Moq;
using System.Xml;

namespace LabTec.Tests.Infrastructure
{
    public class SampleTests
    {
        private readonly SampleRepository _repository;

        public SampleTests()
        {
            _repository = new SampleRepository();
        }

        [Fact]
        public async Task AddAsync_ReturnsNewSample()
        {
            #region Arrange

            var id = Guid.NewGuid();
            var sample = new Sample(id, "sample", Guid.NewGuid(), 1);

            #endregion

            #region Act

            var result = await _repository.AddAsync(sample);

            #endregion

            #region Assert

            Assert.True(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(id, result.Value.Id);

            #endregion
        }

        [Fact]
        public async Task AddAsync_Duplicate_ReturnsFailure()
        {
            #region Arrange

            var id = Guid.NewGuid();
            var sample = new Sample(id, "sample", Guid.NewGuid(), 1);
            var dupIdSample = new Sample(id, "same_id", Guid.NewGuid(), 1);
            var dupNameSample = new Sample(Guid.NewGuid(), "sample", Guid.NewGuid(), 1);

            #endregion

            #region Act

            await _repository.AddAsync(sample);
            var dupIdResult = await _repository.AddAsync(dupIdSample);
            var dupNameResult = await _repository.AddAsync(dupNameSample);

            #endregion

            #region Assert

            Assert.False(dupIdResult.Success);
            Assert.False(dupNameResult.Success);

            #endregion
        }
    }
}