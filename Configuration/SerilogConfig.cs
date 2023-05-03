using Serilog.Events;

namespace Configuration;

public class Args
{
    public string path                   { get; set; }
    public string formatter              { get; set; }
    public int    fileSizeLimitBytes     { get; set; }
    public bool   rollOnFileSizeLimit    { get; set; }
    public string flushToDiskInterval    { get; set; }
    public int    retainedFileCountLimit { get; set; }
    public string rollingInterval        { get; set; }
    public string outputTemplate         { get; set; }
}

public class Serilog
{
    public string        MinimumLevel { get; set; }
    public List<WriteTo> WriteTo      { get; set; }
    public List<string>  Enrich       { get; set; }
}

public class WriteTo
{
    public string Name { get; set; }
    public Args   Args { get; set; }
}