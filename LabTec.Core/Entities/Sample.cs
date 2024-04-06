using LabTec.Common.Utilities;

namespace LabTec.Core.Entities
{
    public class Sample
    {
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public Guid Owner { get; internal set; }
        public int Position { get; internal set; }
        public bool CheckedOut { get; internal set; }
        public bool Archived { get; internal set; }

        public Sample(Guid id, string name, Guid owner, int position, bool checkedOut = false, bool archived = false)
        {
            Id = id;
            Name = name;
            Owner = owner;
            Position = position;
            CheckedOut = checkedOut;
            Archived = archived;
        }

        public Result<Sample> Move(int position)
        {
            if (CheckedOut)
            {
                return Result<Sample>.Fail("Sample must be ckecked in to be moved.");
            }
            if (Archived)
            {
                return Result<Sample>.Fail("Sample is archived and cannot be moved.");
            }
            Position = position;
            return Result<Sample>.Ok(this);
        }

        public Result<Sample> CheckOut()
        {
            if (CheckedOut)
            {
                return Result<Sample>.Fail("Sample is already ckecked out.");
            }
            if (Archived)
            {
                return Result<Sample>.Fail("Sample is archived and cannot be checked out.");
            }
            CheckedOut = true;
            return Result<Sample>.Ok(this);
        }

        public Result<Sample> CheckIn()
        {
            if (!CheckedOut)
            {
                return Result<Sample>.Fail("Sample is already ckecked in.");
            }
            if (Archived)
            {
                return Result<Sample>.Fail("Sample is archived and cannot be checked in.");
            }
            CheckedOut = false;
            return Result<Sample>.Ok(this);
        }

        public Result<Sample> Archive()
        {
            if (CheckedOut)
            {
                return Result<Sample>.Fail("Sample must be ckecked in to be archived.");
            }
            if (Archived)
            {
                return Result<Sample>.Fail("Sample is already archived.");
            }
            Archived = true;
            return Result<Sample>.Ok(this);
        }
    }
}
