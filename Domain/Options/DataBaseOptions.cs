namespace Domain.Options
{
    public class DataBaseOptions
    {
        public DBType DBType { get; set; }
        public MemoryOptions? MemorySettings { get; set; }
        public PostgreOptions? PostgreSettings { get; set; }
    }
}
