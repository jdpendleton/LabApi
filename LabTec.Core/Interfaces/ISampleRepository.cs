using LabTec.Common.Interfaces;
using LabTec.Common.Utilities;
using LabTec.Core.Entities;

namespace LabTec.Core.Interfaces
{
    public interface ISampleRepository : IRepository<Sample>
    {
        Task<Result<Sample>> GetByNameAsync(string name);
    }
}