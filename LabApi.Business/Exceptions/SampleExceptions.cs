namespace LabApi.Business.Exceptions
{
    public class SampleNotFoundException : Exception
    {
        public string Name { get; internal set; }
    }

    public class DuplicateSampleException : Exception
    {
        public string Name { get; internal set; }
    }

    public class SampleCheckedOutException : Exception
    {
        public string Name { get; internal set; }
    }

    public class SampleArchivedException : Exception
    {
        public string Name { get; internal set; }
    }

    public class RedundantSampleRequestException : Exception
    {
        public string Name { get; internal set; }
        public bool? CheckedOut { get; internal set; }
        public int? Position { get; internal set; }
        public bool? Archived { get; internal set; }
    }
}
