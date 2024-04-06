using LabTec.Core.Entities;

namespace LabTec.GraphQL.DTOs
{
    public class SampleDTO
    {
        public string name { get; set; }
        public Guid owner { get; set; }
        public int position { get; set; }
        public bool checkedOut { get; set; }
        public bool archived { get; set; }

        public SampleDTO(Sample sample)
        {
            name = sample.Name;
            owner = sample.Owner;
            position = sample.Position;
            checkedOut = sample.CheckedOut;
            archived = sample.Archived;
        }
    }
}
