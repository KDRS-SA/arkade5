﻿using System;
using System.Collections.Generic;
using System.Xml;
using Arkivverket.Arkade.Core.Base;
using Arkivverket.Arkade.Core.Base.Noark5;
using Arkivverket.Arkade.Core.Resources;
using Arkivverket.Arkade.Core.Util;

namespace Arkivverket.Arkade.Core.Testing.Noark5
{
    public class N5_61_NumberOfChangesLogged : Noark5XmlReaderBaseTest
    {
        public static readonly TestId _id = new TestId(TestId.TestKind.Noark5, 61);

        private readonly Archive _archive;

        public N5_61_NumberOfChangesLogged(Archive archive)
        {
            _archive = archive;
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
            int numberOfChangesLogged = 0;

            var testResults = new List<TestResult>();

            string changelogFullFilename = _archive.WorkingDirectory.Content()
                .WithFile(ArkadeConstants.ChangeLogXmlFileName).FullName;

            try
            {
                var xmlTextReader = new XmlTextReader(changelogFullFilename);

                while (xmlTextReader.Read())
                    if (xmlTextReader.Name.Equals("endring") && xmlTextReader.IsStartElement())
                        numberOfChangesLogged++;

                xmlTextReader.Close();

                testResults.Add(new TestResult(ResultType.Success, new Location(ArkadeConstants.ChangeLogXmlFileName),
                    string.Format(Noark5Messages.TotalResultNumber, numberOfChangesLogged)));
            }
            catch (Exception)
            {
                testResults.Add(new TestResult(ResultType.Error, new Location(string.Empty),
                    string.Format(Noark5Messages.FileNotFound, ArkadeConstants.ChangeLogXmlFileName)));
            }

            return testResults;
        }

        protected override void ReadStartElementEvent(object sender, ReadElementEventArgs eventArgs)
        {
        }

        protected override void ReadAttributeEvent(object sender, ReadElementEventArgs eventArgs)
        {
        }

        protected override void ReadEndElementEvent(object sender, ReadElementEventArgs eventArgs)
        {
        }

        protected override void ReadElementValueEvent(object sender, ReadElementEventArgs eventArgs)
        {
        }
    }
}
