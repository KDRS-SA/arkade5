using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Arkivverket.Arkade.Core.Logging;
using Arkivverket.Arkade.Core.Testing;
using Arkivverket.Arkade.Core.Util;

namespace Arkivverket.Arkade.Core.Base.Noark5
{
    public class Noark5TestEngine : ITestEngine
    {
        private readonly ITestProvider _testProvider;
        private readonly IStatusEventHandler _statusEventHandler;

        public Noark5TestEngine(ITestProvider testProvider, IStatusEventHandler statusEventHandler)
        {
            _testProvider = testProvider;
            _statusEventHandler = statusEventHandler;
        }

        public event EventHandler<ReadElementEventArgs> ReadStartElementEvent;
        public event EventHandler<ReadElementEventArgs> ReadAttributeEvent;
        public event EventHandler<ReadElementEventArgs> ReadElementValueEvent;
        public event EventHandler<ReadElementEventArgs> ReadEndElementEvent;


        public TestSuite RunTestsOnArchive(TestSession testSession)
        {
            List<IArkadeStructureTest> structureTests = RunStructureTests(testSession.Archive);

            List<INoark5Test> contentTests = RunContentTests(testSession.Archive);

            var testSuite = new TestSuite();
            AddTestToTestSuite(contentTests, testSuite);
            AddTestToTestSuite(structureTests, testSuite);
            return testSuite;
        }

        private static void AddTestToTestSuite(IEnumerable<IArkadeTest> tests, TestSuite testSuite)
        {
            foreach (var test in tests)
                testSuite.AddTestRun(test.GetTestRun());
        }

        private List<INoark5Test> RunContentTests(Archive archive)
        {
            List<INoark5Test> contentTests = _testProvider.GetContentTests(archive);

            SubscribeTestsToReadElementEvent(contentTests);

            ArchiveXmlFile archiveStructureFile = archive.GetArchiveXmlFile(ArkadeConstants.ArkivstrukturXmlFileName);

            using (var reader = XmlReader.Create(archiveStructureFile.AsStream()))
            {
                RaiseEventStartParsingFile();

                var path = new Stack<XmlElement>();

                while (ReadNextNode(reader))
                {
                    if (reader.IsEmptyElement)
                        continue;

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            var xmlElement = new XmlElement(reader.LocalName, 1, 1);
                            
                            path.Push(xmlElement);
                            RaiseReadStartElementEvent(CreateReadElementEventArgs(reader, path));
                            break;
                        case XmlNodeType.Attribute:
                            RaiseReadAttributeEvent(CreateReadElementEventArgs(reader, path));
                            break;
                        case XmlNodeType.Text:
                            RaiseReadElementValueEvent(CreateReadElementEventArgs(reader, path));
                            break;
                        case XmlNodeType.EndElement:
                            path.Pop();
                            RaiseReadEndElementEvent(CreateReadElementEventArgs(reader, path));
                            _statusEventHandler.RaiseEventRecordProcessingStopped();
                            break;
                        case XmlNodeType.XmlDeclaration:
                        case XmlNodeType.ProcessingInstruction:
                        case XmlNodeType.Comment:
                        case XmlNodeType.Whitespace:
                            break;
                    }
                }
                RaiseEventFinishedParsingFile();
            }
            return contentTests;
        }

        private static bool ReadNextNode(XmlReader reader)
        {
            return reader.MoveToNextAttribute() || reader.Read();
        }

        private List<IArkadeStructureTest> RunStructureTests(Archive archive)
        {
            List<IArkadeStructureTest> structureTests = _testProvider.GetStructureTests();
            foreach (var test in structureTests)
            {
                string testName = ArkadeTestInfoProvider.GetDisplayName(test);

                try
                {
                    _statusEventHandler.RaiseEventOperationMessage(testName, "", OperationMessageStatus.Started);
                    test.Test(archive);

                    var errorTestResults = test.GetTestRun().Results.Where(r => r.IsError());
                    if (errorTestResults.Any())
                    {
                        var message = new StringBuilder();

                        foreach (var result in errorTestResults)
                            message.AppendLine().AppendLine(result.Location + " - " + result.Message);

                        _statusEventHandler.RaiseEventOperationMessage(testName, message.ToString(),
                            OperationMessageStatus.Error);
                    }
                    else
                        _statusEventHandler.RaiseEventOperationMessage(testName, "", OperationMessageStatus.Ok);
                }
                catch (Exception)
                {
                    _statusEventHandler.RaiseEventOperationMessage(testName, "", OperationMessageStatus.Error);
                    throw;
                }
            }
            return structureTests;
        }

        private void RaiseEventStartParsingFile()
        {
            _statusEventHandler.RaiseEventFileProcessingStarted(
                new FileProcessingStatusEventArgs(ArkadeConstants.ArkivstrukturXmlFileName, ArkadeConstants.ArkivstrukturXmlFileName));
        }

        private void RaiseEventFinishedParsingFile()
        {
            _statusEventHandler.RaiseEventFileProcessingFinished(
                new FileProcessingStatusEventArgs(ArkadeConstants.ArkivstrukturXmlFileName, ArkadeConstants.ArkivstrukturXmlFileName, true));
        }

        private static ReadElementEventArgs CreateReadElementEventArgs(XmlReader reader, Stack<XmlElement> path)
        {
            return new ReadElementEventArgs(reader.Name, reader.Value, new ElementPath(path.ToList()));
        }

        private void SubscribeTestsToReadElementEvent(List<INoark5Test> testsForArchive)
        {
            foreach (var test in testsForArchive)
            {
                ReadStartElementEvent += test.OnReadStartElementEvent;
                ReadAttributeEvent += test.OnReadAttributeEvent;
                ReadElementValueEvent += test.OnReadElementValueEvent;
                ReadEndElementEvent += test.OnReadEndElementEvent;
            }
        }

        private void RaiseReadStartElementEvent(ReadElementEventArgs readElementEventArgs)
        {
            var handler = ReadStartElementEvent;
            handler?.Invoke(this, readElementEventArgs);
        }

        private void RaiseReadAttributeEvent(ReadElementEventArgs readElementEventArgs)
        {
            var handler = ReadAttributeEvent;
            handler?.Invoke(this, readElementEventArgs);
        }

        private void RaiseReadElementValueEvent(ReadElementEventArgs readElementEventArgs)
        {
            var handler = ReadElementValueEvent;
            handler?.Invoke(this, readElementEventArgs);
        }

        private void RaiseReadEndElementEvent(ReadElementEventArgs readElementEventArgs)
        {
            var handler = ReadEndElementEvent;
            handler?.Invoke(this, readElementEventArgs);
        }
    }

    public class XmlElement
    {
        public string Name { get; }
        public int Row { get; }
        public int Column { get; }

        public XmlElement(string name, int row, int column)
        {
            Name = name;
            Row = row;
            Column = column;
        }
    }
}
