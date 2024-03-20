using LabApi.Business.DTOs;

namespace LabApi.Business.Interfaces
{
    public interface ISampleService
    {
        public Task<IEnumerable<SampleDTO>> GetSamplesAsync();
        public Task<SampleDTO> CreateSampleAsync(string name, Guid owner, int position);
        public Task<SampleDTO> CheckoutSampleAsync(string name);
        public Task<SampleDTO> CheckinSampleAsync(string name);
        public Task<SampleDTO> MoveSampleAsync(string name, int position);
        public Task<SampleDTO> ArchiveSampleAsync(string name);
    }
}