using LabTec.Common.Utilities;
using LabTec.Core.Entities;
using LabTec.Core.Interfaces;

namespace LabTec.Infrastructure.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        private readonly List<Sample> _samples;

        public SampleRepository()
        {
            _samples = new List<Sample>();
        }

        public async Task<Result<Sample>> AddAsync(Sample sample)
        {
            var idResult = await GetByIdAsync(sample.Id);
            if (idResult.Success)
            {
                return Result<Sample>.Fail($"Sample already exists with the id '{sample.Id}'");
            }

            var nameResult = await GetByNameAsync(sample.Name);
            if (nameResult.Success)
            {
                return Result<Sample>.Fail($"Sample already exists with the id '{sample.Name}'");
            }

            _samples.Add(sample);
            return Result<Sample>.Ok(sample);
        }

        public async Task<Result> DeleteAsync(Sample sample)
        {
            var success = _samples.Remove(sample);
            if (!success)
            {
                Result.Fail("Sample not found.");
            }

            return Result.Ok();
        }

        public async Task<Result<IEnumerable<Sample>>> GetAllAsync()
        {
            return Result<IEnumerable<Sample>>.Ok(_samples);
        }

        public async Task<Result<Sample>> GetByIdAsync(Guid id)
        {
            var index = _samples.FindIndex(s => s.Id == id);

            if (index == -1)
            {
                return Result<Sample>.Fail("Sample not found.");
            }

            var sample = _samples[index];

            return Result<Sample>.Ok(sample);
        }

        public async Task<Result<Sample>> GetByNameAsync(string name)
        {
            var index = _samples.FindIndex(s => s.Name == name);

            if (index == -1)
            {
                return Result<Sample>.Fail("Sample not found.");
            }

            var sample = _samples[index];

            return Result<Sample>.Ok(sample);
        }

        public async Task<Result<Sample>> UpdateAsync(Sample sample)
        {
            var index = _samples.FindIndex(s => s.Id == sample.Id);

            if (index == -1)
            {
                return Result<Sample>.Fail("Sample not found.");
            }

            _samples[index] = sample;

            return Result<Sample>.Ok(sample);
        }
    }
}
