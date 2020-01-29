﻿using System.Collections.Generic;
using Arkivverket.Arkade.Core.Base.Addml.Definitions;
using Arkivverket.Arkade.Core.Resources;
using Arkivverket.Arkade.Core.Testing;
using Arkivverket.Arkade.Core.Util;

namespace Arkivverket.Arkade.Core.Base.Addml.Processes
{
    public class A_03_AnalyseFindExtremeRecords : AddmlProcess
    {
        public static readonly TestId _id = new TestId(TestId.TestKind.Addml, 3);

        public const string Name = "Analyse_FindExtremeRecords";

        private readonly Dictionary<RecordIndex, MinAndMax> _minAndMaxRecordLength
            = new Dictionary<RecordIndex, MinAndMax>();

        private readonly List<TestResult> _testResults = new List<TestResult>();

        public override TestId GetId()
        {
            return _id;
        }

        public override string GetName()
        {
            return Name;
        }

        public override string GetDescription()
        {
            return Messages.AnalyseFindExtremeRecordsDescription;
        }

        public override TestType GetTestType()
        {
            return TestType.ContentAnalysis;
        }

        protected override List<TestResult> GetTestResults()
        {
            return _testResults;
        }

        protected override void DoRun(FlatFile flatFile)
        {
        }

        protected override void DoRun(Record record)
        {
            string recordValue = record.Value;
            int recordLength = recordValue.Length;

            RecordIndex index = record.Definition.GetIndex();
            if (!_minAndMaxRecordLength.ContainsKey(index))
            {
                _minAndMaxRecordLength.Add(index, new MinAndMax());
            }

            MinAndMax minAndMaxValue = _minAndMaxRecordLength[index];
            minAndMaxValue.NewValue(recordLength);
        }

        protected override void DoEndOfFile()
        {
            foreach (KeyValuePair<RecordIndex, MinAndMax> entry in _minAndMaxRecordLength)
            {
                RecordIndex index = entry.Key;
                MinAndMax minAndMaxValue = entry.Value;
                string minLengthString = minAndMaxValue.GetMin()?.ToString() ?? "<no value>";
                string maxLengthString = minAndMaxValue.GetMax()?.ToString() ?? "<no value>";

                _testResults.Add(new TestResult(ResultType.Success, AddmlLocation.FromRecordIndex(index),
                    string.Format(Messages.AnalyseFindExtremeRecordsMessage, maxLengthString, minLengthString)));
            }

            _minAndMaxRecordLength.Clear();
        }

        protected override void DoRun(Field field)
        {
        }
    }
}