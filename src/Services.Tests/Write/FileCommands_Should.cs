using Moq;
using Services.Contracts.Wrappers;
using Services.Write;
using System;
using System.IO;
using Xunit;

namespace Services.Tests.Write
{
    public class FileCommands_Should
    {
        private static Func<string, FileMode, Stream> createFileStream = 
            (string path, FileMode mode) => new MemoryStream();

        [Fact] 
        public void Delete_FileExists()
        {
            var fileWrapperMock = new Mock<IFile>();
            fileWrapperMock
                .Setup(x => x.Exists("test"))
                .Returns(true);
            var fileCommands = new FileCommands(createFileStream, fileWrapperMock.Object);

            var isDeleted = fileCommands.Delete("test");

            Assert.True(isDeleted);
        }

        [Fact]
        public void Delete_FileDoesNotExist()
        {
            var fileWrapperMock = new Mock<IFile>();
            fileWrapperMock
                .Setup(x => x.Exists("test"))
                .Returns(false);
            var fileCommands = new FileCommands(createFileStream, fileWrapperMock.Object);

            var isDeleted = fileCommands.Delete("test");

            Assert.False(isDeleted);
        }
    }
}
