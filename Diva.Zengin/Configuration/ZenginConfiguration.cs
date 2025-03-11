namespace Diva.Zengin.Configuration;

public record ZenginConfiguration
{
    public FileFormat FileFormat { get; set; } = FileFormat.Zengin;
}