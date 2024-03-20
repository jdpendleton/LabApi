using LabApi.Data.Models;

namespace LabApi.Business.DTOs
{
    public class SampleDTO
    {
        public SampleDTO(Sample sample)
        {
            this.Name = sample.Name;
            this.Owner = sample.Owner;
            this.CheckedOut = sample.CheckedOut;
            this.Position = sample.Position;
            this.Archived = sample.Archived;
        }

        public string Name { get; set; }
        public Guid Owner { get; set; }
        public bool CheckedOut { get; set; }
        public int Position { get; set; }
        public bool Archived { get; set; }
    }
}
