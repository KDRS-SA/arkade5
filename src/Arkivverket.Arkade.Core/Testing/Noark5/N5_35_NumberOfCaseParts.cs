using System.Collections.Generic;
using Arkivverket.Arkade.Core.Base;
using Arkivverket.Arkade.Core.Base.Noark5;
using Arkivverket.Arkade.Core.Resources;
using Arkivverket.Arkade.Core.Util;

namespace Arkivverket.Arkade.Core.Testing.Noark5
{
    public class N5_35_NumberOfCaseParts : Noark5XmlReaderBaseTest
    {
        private readonly TestId _id;

        private readonly string _archiveStandard;
        private string _currentArchivePartSystemId;
        private int _totalNumberOfCaseParts;
        private readonly Dictionary<string, int> _casePartsPerArchivePart = new Dictionary<string, int>();

        public N5_35_NumberOfCaseParts(Archive archive)
        {
            _archiveStandard = archive.Details.ArchiveStandard;

            _id = new TestId(TestId.TestKind.Noark5, 35, _archiveStandard);
        }

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
                new TestResult(ResultType.Success, new Location(string.Empty),
                    string.Format(Noark5Messages.TotalResultNumber, _totalNumberOfCaseParts.ToString()))
            };

            foreach (KeyValuePair<string, int> casePartCount in _casePartsPerArchivePart)
            {
                if (casePartCount.Value > 0)
                    testResults.Add(new TestResult(ResultType.Success, new Location(string.Empty),
                        string.Format(Noark5Messages.NumberOf_PerArchivePart, casePartCount.Key, casePartCount.Value)));
            }

            return testResults;
        }


        protected override void ReadStartElementEvent(object sender, ReadElementEventArgs eventArgs)
        {
        }

        protected override void ReadAttributeEvent(object sender, ReadElementEventArgs eventArgs)
        {
        }

        protected override void ReadElementValueEvent(object sender, ReadElementEventArgs eventArgs)
        {
            if (eventArgs.Path.Matches("systemID", "arkivdel"))
            {
                _currentArchivePartSystemId = eventArgs.Value;
                _casePartsPerArchivePart.Add(_currentArchivePartSystemId, 0);
            }
        }

        protected override void ReadEndElementEvent(object sender, ReadElementEventArgs eventArgs)
        {
            if (IsCasePart(eventArgs))
                Count();
        }

        private bool IsCasePart(ReadElementEventArgs eventArgs)
        {
            return eventArgs.NameEquals("sakspart")
                   || _archiveStandard.Equals("5.5") && eventArgs.NameEquals("part");
        }

        private void Count()
        {
            _totalNumberOfCaseParts++;

            if (_casePartsPerArchivePart.ContainsKey(_currentArchivePartSystemId))
                _casePartsPerArchivePart[_currentArchivePartSystemId]++;
        }
    }
}
