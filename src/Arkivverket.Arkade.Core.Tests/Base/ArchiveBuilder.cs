﻿using System.IO;
using Arkivverket.Arkade.Core.Base;
using Moq;

namespace Arkivverket.Arkade.Core.Tests.Base
{
    public class ArchiveBuilder
    {
        private ArchiveType _archiveType = ArchiveType.Noark5;
        private ArchiveDetails _archiveDetails;

        private Uuid _uuid = Uuid.Random();
        private DirectoryInfo _workingDirectoryContent;
        private DirectoryInfo _workingDirectory;

        public ArchiveBuilder WithUuid(string uuid)
        {
            _uuid = Uuid.Of(uuid);
            return this;
        }

        public ArchiveBuilder WithUuid(Uuid uuid)
        {
            _uuid = uuid;
            return this;
        }

        public ArchiveBuilder WithWorkingDirectoryRoot(string workingDirectory)
        {
            _workingDirectory = new DirectoryInfo(workingDirectory);
            return this;
        }

        public ArchiveBuilder WithWorkingDirectoryExternalContent(string workingDirectory)
        {
            _workingDirectoryContent = new DirectoryInfo(workingDirectory);
            return this;
        }

        public ArchiveBuilder WithWorkingDirectoryExternalContent(DirectoryInfo workingDirectory)
        {
            _workingDirectoryContent = workingDirectory;
            return this;
        }

        public ArchiveBuilder WithArchiveType(ArchiveType archiveType)
        {
            _archiveType = archiveType;
            return this;
        }

        public ArchiveBuilder WithArchiveDetails(string standardVersion)
        {
            var mock = new Mock<ArchiveDetails>(Build());
            mock.Setup(x => x.ArchiveStandard).Returns(standardVersion);
            _archiveDetails = mock.Object;
            return this;
        }

        public Archive Build()
        {
            var archive = new Archive(_archiveType, _uuid, new WorkingDirectory(_workingDirectory, _workingDirectoryContent));
            return archive;
        }
    }
}