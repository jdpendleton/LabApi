using LabTec.Core.Entities;

namespace LabTec.Tests.Core
{
    public class SampleTests
    {
        [Fact]
        public void Move_ReturnsUpdatedPosition()
        {
            #region Arrange

            var newPosition = 2;
            var sample = new Sample(Guid.NewGuid(), "sample", Guid.NewGuid(), 1);

            #endregion

            #region Act

            var result = sample.Move(newPosition);

            #endregion

            #region Assert

            Assert.Equal(newPosition, sample.Position);
            Assert.Equal(newPosition, result.Value.Position);

            #endregion
        }

        [Fact]
        public void Move_CheckedOut_ReturnsFailure()
        {
            #region Arrange

            var oldPosition = 1;
            var sample = new Sample(Guid.NewGuid(), "sample", Guid.NewGuid(), oldPosition, true);

            #endregion

            #region Act

            var result = sample.Move(2);

            #endregion

            #region Assert

            Assert.Equal(oldPosition, sample.Position);
            Assert.False(result.Success);

            #endregion
        }

        [Fact]
        public void Move_Archived_ReturnsFailure()
        {
            #region Arrange

            var oldPosition = 1;
            var sample = new Sample(Guid.NewGuid(), "sample", Guid.NewGuid(), oldPosition, false, true);

            #endregion

            #region Act

            var result = sample.Move(2);

            #endregion

            #region Assert

            Assert.Equal(oldPosition, sample.Position);
            Assert.False(result.Success);

            #endregion
        }
    }
}