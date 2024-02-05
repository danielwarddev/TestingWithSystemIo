using System.IO.Abstractions;

namespace TestingWithSystemIo;

public class MyFileWriter
{
    private readonly IFileSystem _fileSystem;

    public MyFileWriter(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public async Task<IEnumerable<string>> GetAllTxtFiles(string directoryPath)
    {
        // Without using System.IO.Abstractions:
        // var allFilePaths = Directory.GetFiles(directoryPath);
        var allFilePaths = _fileSystem.Directory.GetFiles(directoryPath);

        // Note that I opt to still use the static Path class here
        // It's essentially just a string helper and would be both more hassle and a little pointless to mock
        return allFilePaths.Where(x => Path.GetExtension(x) == ".txt");
    }
}
