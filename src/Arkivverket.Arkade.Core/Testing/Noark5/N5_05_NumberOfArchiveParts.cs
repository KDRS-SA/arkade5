using System.Collections.Generic;
using System.Linq;
using Arkivverket.Arkade.Core.Base.Noark5;
using Arkivverket.Arkade.Core.Resources;
using Arkivverket.Arkade.Core.Util;

namespace Arkivverket.Arkade.Core.Testing.Noark5
{
    public class N5_05_NumberOfArchiveParts : Noark5XmlReaderBaseTest
    {
        public static readonly TestId _id = new TestId(TestId.TestKind.Noark5, 5);

        private readonly Dictionary<string, int> _archivepartsPerArchive = new Dictionary<string, int>();
        private string _currentArchiveSystemId = string.Empty;


        public override TestId GetId()
        {
            return _id;
        }

        public override TestType GetTestType()
        {
            return TestType.ContentAnalysis;
        }

        protected override List<TestResult> GetTestResults()
        {
            var testResults = new List<TestResult>
            {
                new TestResult(ResultType.Success, new Location(string.Empty), string.Format(
                    Noark5Messages.TotalResultNumber, _archivepartsPerArchive.Values.Sum()
                ))
            };

            if (_archivepartsPerArchive.Count > 1)
            {
                foreach (KeyValuePair<string, int> archivesCountAtLevel in _archivepartsPerArchive)
                {
                    var testResult = new TestResult(ResultType.Success, new Location(string.Empty), string.Format(
                        Noark5Messages.NumberOfArchivePartsMessage_ArchivepartsInArchive,
                        archivesCountAtLevel.Key, archivesCountAtLevel.Value));

                    testResults.Add(testResult);
                }
            }

            return testResults;
        }


        protected override void ReadStartElementEvent(object sender, ReadElementEventArgs eventArgs)
        {
            if (eventArgs.NameEquals("arkivdel"))
            {
                if (_archivepartsPerArchive.ContainsKey(_currentArchiveSystemId))
                    _archivepartsPerArchive[_currentArchiveSystemId]++;
                else
                    _archivepartsPerArchive.Add(_currentArchiveSystemId, 1);
            }
        }

        protected override void ReadAttributeEvent(object sender, ReadElementEventArgs eventArgs)
        {
        }

        protected override void ReadElementValueEvent(object sender, ReadElementEventArgs eventArgs)
        {
            if (eventArgs.Path.Matches("systemID", "arkiv"))
                _currentArchiveSystemId = eventArgs.Value;
        }

        protected override void ReadEndElementEvent(object sender, ReadElementEventArgs eventArgs)
        {
            if (eventArgs.NameEquals("arkiv"))
                _currentArchiveSystemId = string.Empty;
        }
    }
}
