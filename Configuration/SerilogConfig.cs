namespace Configuration
{
    public class SerilogConfig
    {
        public MinimumLevelConfig         MinimumLevel { get; set; }
        public List<WriteToConfig>        WriteTo      { get; set; }
        public List<string>               Enrich       { get; set; }
        public Dictionary<string, string> Properties   { get; set; }
    }

    public class MinimumLevelConfig
    {
        public string Default { get; set; }
    }

    public class WriteToConfig
    {
        public string                     Name { get; set; }
        public Dictionary<string, object> Args { get; set; }
    }
}