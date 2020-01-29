﻿using System.Collections.Generic;
using Arkivverket.Arkade.Core.Base.Addml.Definitions;
using Arkivverket.Arkade.Core.Base.Addml.Definitions.DataTypes;
using Arkivverket.Arkade.Core.Resources;
using Arkivverket.Arkade.Core.Testing;
using Arkivverket.Arkade.Core.Util;

namespace Arkivverket.Arkade.Core.Base.Addml.Processes
{
    public class A_36_ControlBooleanValue : AddmlProcess
    {
        public static readonly TestId _id = new TestId(TestId.TestKind.Addml, 36);

        public const string Name = "Control_Boolean_Value";

        private readonly Dictionary<FieldIndex, HashSet<string>> _nonBooleanValues
            = new Dictionary<FieldIndex, HashSet<string>>();

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
            return Messages.ControlBooleanValueDescription;
        }

        public override TestType GetTestType()
        {
            return TestType.ContentControl;
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
        }

        protected override void DoEndOfFile()
        {
            foreach (KeyValuePair<FieldIndex, HashSet<string>> entry in _nonBooleanValues)
            {
                FieldIndex fieldIndex = entry.Key;
                HashSet<string> nonBooleanValues = entry.Value;

                _testResults.Add(new TestResult(ResultType.Error, AddmlLocation.FromFieldIndex(fieldIndex),
                    string.Format(Messages.ControlBooleanValueMessage, string.Join(" ", nonBooleanValues))));
            }

            _nonBooleanValues.Clear();
        }

        protected override void DoRun(Field field)
        {
            string value = field.Value;

            DataType dt = field.Definition.Type;
            if (dt.GetType() != typeof(BooleanDataType))
            {
                return;
            }

            BooleanDataType dataType = (BooleanDataType) dt;
            if (dataType.IsValid(value))
            {
                return;
            }

            // value is illegal boolean value
            FieldIndex fieldIndeks = field.Definition.GetIndex();
            if (!_nonBooleanValues.ContainsKey(fieldIndeks))
            {
                _nonBooleanValues.Add(fieldIndeks, new HashSet<string>());
            }

            _nonBooleanValues[fieldIndeks].Add(value);
        }
    }
}