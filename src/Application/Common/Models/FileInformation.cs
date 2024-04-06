namespace Rihal.ReelRise.Application.Common.Models;
public class FileInformation
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }

    public FileInformation(string name, string extension, long size)
    {
        Name = name;
        Extension = extension;
        Size = size;
    }
}
