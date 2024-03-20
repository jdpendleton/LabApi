using LabApi.Business.Interfaces;
using LabApi.Business.DTOs;
using LabApi.Business.Exceptions;
using LabApi.Data.Models;
using LabApi.Data.Interfaces;

namespace LabApi.Business.Services
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleService(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<IEnumerable<SampleDTO>> GetSamplesAsync()
        {
            var samples = await _sampleRepository.GetSamplesAsync();

            var sampleDTOs = samples.Select(sample => new SampleDTO(sample));

            return sampleDTOs;
        }

        public async Task<SampleDTO> CreateSampleAsync(string name, Guid owner, int position)
        {
            var duplicate = await _sampleRepository.GetSampleByNameAsync(name);
            if (duplicate != null)
            {
                throw new DuplicateSampleException() { Name = name };
            }
            var sample = new Sample()
            {
                Name = name,
                Owner = owner,
                CheckedOut = false,
                Position = position,
                Archived = false
            };
            await _sampleRepository.AddSampleAsync(sample);
            return new SampleDTO(sample);
        }

        public async Task<SampleDTO> CheckoutSampleAsync(string name)
        {
            var sample = await _sampleRepository.GetSampleByNameAsync(name)
                ?? throw new SampleNotFoundException() { Name = name };
            if (sample.Archived)
            {
                throw new SampleArchivedException() { Name = name };
            }
            if (sample.CheckedOut)
            {
                throw new RedundantSampleRequestException() { Name = name, CheckedOut = true };
            }
            sample.CheckedOut = true;
            await _sampleRepository.UpdateSampleAsync(sample);
            return new SampleDTO(sample);
        }

        public async Task<SampleDTO> CheckinSampleAsync(string name)
        {
            var sample = await _sampleRepository.GetSampleByNameAsync(name)
                ?? throw new SampleNotFoundException() { Name = name };
            if (sample.Archived)
            {
                throw new SampleArchivedException() { Name = name };
            }
            if (!sample.CheckedOut)
            {
                throw new RedundantSampleRequestException() { Name = name, CheckedOut = false };
            }
            sample.CheckedOut = false;
            await _sampleRepository.UpdateSampleAsync(sample);
            return new SampleDTO(sample);
        }

        public async Task<SampleDTO> MoveSampleAsync(string name, int position)
        {
            var sample = await _sampleRepository.GetSampleByNameAsync(name)
                ?? throw new SampleNotFoundException() { Name = name };
            if (sample.CheckedOut)
            {
                throw new SampleCheckedOutException() { Name = name };
            }
            if (sample.Archived)
            {
                throw new SampleArchivedException() { Name = name };
            }
            if (sample.Position == position)
            {
                throw new RedundantSampleRequestException() { Name = name, Position = position };
            }
            sample.Position = position;
            await _sampleRepository.UpdateSampleAsync(sample);
            return new SampleDTO(sample);
        }

        public async Task<SampleDTO> ArchiveSampleAsync(string name)
        {
            var sample = await _sampleRepository.GetSampleByNameAsync(name)
                ?? throw new SampleNotFoundException() { Name = name };
            if (sample.CheckedOut)
            {
                throw new SampleCheckedOutException() { Name = name };
            }
            if (sample.Archived)
            {
                throw new RedundantSampleRequestException() { Name = name, Archived = true };
            }
            sample.Archived = true;
            await _sampleRepository.UpdateSampleAsync(sample);
            return new SampleDTO(sample);
        }
    }
}
