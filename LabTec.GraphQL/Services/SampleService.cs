using LabTec.Core.Interfaces;
using LabTec.Core.Entities;

namespace LabTec.GraphQL.Services
{
    public class SampleService
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleService(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<Common.Utilities.Result<IEnumerable<Sample>>> GetAllAsync()
        {
            var result = await _sampleRepository.GetAllAsync();
            return result;
        }

        public async Task<Common.Utilities.Result<Sample>> GetByIdAsync(Guid id)
        {
            var result = await _sampleRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<Common.Utilities.Result<Sample>> GetByNameAsync(string name)
        {
            var result = await _sampleRepository.GetByNameAsync(name);
            return result;
        }

        public async Task<Common.Utilities.Result<Sample>> CreateAsync(string name, Guid owner, int position)
        {
            var sample = new Sample(Guid.NewGuid(), name, owner, position);

            var result = await _sampleRepository.AddAsync(sample);
            return result;
        }

        public async Task<Common.Utilities.Result<Sample>> MoveAsync(string name, int position)
        {
            var result = await _sampleRepository.GetByNameAsync(name)
                ?? Common.Utilities.Result<Sample>.Fail("Sample was not found.");

            if (!result.Success)
                return result;

            var moveResult = result.Value.Move(position);

            if (!moveResult.Success)
                return moveResult;

            var updateResult = await _sampleRepository.UpdateAsync(moveResult.Value);
            return updateResult;
        }

        public async Task<Common.Utilities.Result<Sample>> CheckOutAsync(string name)
        {
            var result = await _sampleRepository.GetByNameAsync(name)
                ?? Common.Utilities.Result<Sample>.Fail("Sample was not found.");

            if (!result.Success)
                return result;

            var checkOutResult = result.Value.CheckOut();

            if (!checkOutResult.Success)
                return checkOutResult;

            await _sampleRepository.UpdateAsync(checkOutResult.Value);
            return result;
        }

        public async Task<Common.Utilities.Result<Sample>> CheckInAsync(string name)
        {
            var result = await _sampleRepository.GetByNameAsync(name)
                ?? Common.Utilities.Result<Sample>.Fail("Sample was not found.");

            if (!result.Success)
                return result;

            var CheckInResult = result.Value.CheckIn();

            if (!CheckInResult.Success)
                return CheckInResult;

            await _sampleRepository.UpdateAsync(CheckInResult.Value);
            return result;
        }

        public async Task<Common.Utilities.Result<Sample>> ArchiveAsync(string name)
        {
            var result = await _sampleRepository.GetByNameAsync(name)
                ?? Common.Utilities.Result<Sample>.Fail("Sample was not found.");

            if (!result.Success)
                return result;

            var archiveResult = result.Value.Archive();

            if (!archiveResult.Success)
                return archiveResult;

            await _sampleRepository.UpdateAsync(archiveResult.Value);
            return result;
        }
    }
}
