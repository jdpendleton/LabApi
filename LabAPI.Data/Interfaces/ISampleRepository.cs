using LabApi.Data.Models;

namespace LabApi.Data.Interfaces
{
    public interface ISampleRepository
    {
        Task<Sample?> GetSampleByNameAsync(string name);
        Task<IEnumerable<Sample>> GetSamplesAsync();
        Task AddSampleAsync(Sample sample);
        Task UpdateSampleAsync(Sample sample);
        Task DeleteSampleAsync(Sample sample);
    }
}