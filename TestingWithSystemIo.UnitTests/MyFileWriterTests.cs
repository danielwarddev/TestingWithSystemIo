using FluentAssertions;
using NSubstitute;
using System.IO.Abstractions;

namespace TestingWithSystemIo.UnitTests;
public class MyFileWriterTests
{
    private readonly MyFileWriter _writer;
    private readonly IFileSystem _fileSystem = Substitute.For<IFileSystem>();

    public MyFileWriterTests()
    {
        _writer = new MyFileWriter(_fileSystem);
    }

    [Fact]
    public async Task GetAllTxtFiles_Returns_All_Txt_File_Paths_In_Directory()
    {
        var directoryPath = "blah";
        var expectedFilePaths = new string[] { "file.txt", "anotherfile.txt" };
        var allFilePaths = new List<string>(expectedFilePaths) { "badfile.txt.doc", "badfile.jpg" };
        _fileSystem.Directory.GetFiles(directoryPath).Returns(allFilePaths.ToArray());

        var actualFilePaths = await _writer.GetAllTxtFiles(directoryPath);

        expectedFilePaths.Should().BeEquivalentTo(actualFilePaths);
    }
}
