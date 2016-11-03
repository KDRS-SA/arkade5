﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Arkivverket.Arkade.Core.Addml
{
    public class FixedFormatReader
    {
        private readonly Dictionary<string, List<int>> _fieldLengthsPerRecordDefinition;
        private readonly int? _identifierLength;
        private readonly int? _identifierStartPosition;
        private readonly int _recordLength;
        private readonly StreamReader _streamReader;

        public FixedFormatReader(StreamReader streamReader, FixedFormatDefinition fixedFormatDefinition)
        {
            _streamReader = streamReader;
            _recordLength = fixedFormatDefinition.RecordLength;

            if (fixedFormatDefinition.RecordDefinitions.Count < 1)
            {
                throw new ArgumentException(
                    "fixedFormatDefinition.RecordDefinitions must contain at least 1 FixedFormatRecordDefinition");
            }

            // If only one recordDefinition and recordIdentifier is not set, set it to empty string
            if (fixedFormatDefinition.RecordDefinitions.Count == 1 && fixedFormatDefinition.RecordDefinitions[0].RecordIdentifier == null)
            {
                fixedFormatDefinition.RecordDefinitions[0].RecordIdentifier = "";
            }


            _fieldLengthsPerRecordDefinition = new Dictionary<string, List<int>>();
            foreach (FixedFormatRecordDefinition f in fixedFormatDefinition.RecordDefinitions)
            {
                List<int> fieldLengths = f.FieldLengths;

                _fieldLengthsPerRecordDefinition.Add(f.RecordIdentifier, fieldLengths);

                int sumOfFieldLengths = fieldLengths.Sum();
                if (sumOfFieldLengths != _recordLength)
                {
                    throw new ArgumentException("Sum of field lengths (" + sumOfFieldLengths +
                                                ") does not match record length (" + _recordLength + ")");
                }
            }

            _identifierStartPosition = fixedFormatDefinition.IdentifierStartPosition;
            _identifierLength = fixedFormatDefinition.IdentifierLength;

            if ((_identifierStartPosition.HasValue && !_identifierLength.HasValue) ||
                (!_identifierStartPosition.HasValue && _identifierLength.HasValue))
            {
                throw new ArgumentException("Both IdentifierStartPosition and IdentifierLength must be set");
            }

            if ((fixedFormatDefinition.RecordDefinitions.Count <= 1) && _identifierStartPosition.HasValue &&
                _identifierLength.HasValue)
            {
                throw new ArgumentException(
                    "fixedFormatDefinition.RecordDefinitions must contain more than 1 FixedFormatRecordDefinition if identifier values are set");
            }
        }

        public bool HasMoreRecords()
        {
            return !_streamReader.EndOfStream;
        }

        public Tuple<string, List<string>> GetNextValue()
        {
            // Read bytes according to recordLength
            int len = _recordLength;
            char[] buffer = new char[len];
            int read = _streamReader.ReadBlock(buffer, 0, len);

            if (read != len)
            {
                throw new IOException("Unable to read a full record of " + _recordLength + " characters. Could only read " + read + " characters");
            }

            string recordString = new string(buffer);

            List<int> fieldLengths;
            string recordIdentifier;
            if (_identifierStartPosition.HasValue && _identifierLength.HasValue)
            {
                recordIdentifier = recordString.Substring(_identifierStartPosition.Value, _identifierLength.Value);
                if (!_fieldLengthsPerRecordDefinition.ContainsKey(recordIdentifier))
                {
                    throw new Exception("No such recordIdentifier");
                }
                fieldLengths = _fieldLengthsPerRecordDefinition[recordIdentifier];
            }
            else
            {
                recordIdentifier = "";
                fieldLengths = _fieldLengthsPerRecordDefinition.Values.First();
            }

            List<string> fields = GetFields(recordString, fieldLengths);

            return new Tuple<string, List<string>>(recordIdentifier, fields);
        }


        private List<string> GetFields(string recordString, List<int> fieldLengths)
        {
            List<string> fields = new List<string>();
            int offset = 0;
            foreach (int fieldLength in fieldLengths)
            {
                string fieldString = recordString.Substring(offset, fieldLength);
                offset += fieldLength;
                fields.Add(fieldString);
            }
            return fields;
        }

        public class FixedFormatDefinition
        {
            public int? IdentifierStartPosition;
            public int? IdentifierLength;
            public int RecordLength; // Support for different records lengths per record definition?
            public List<FixedFormatRecordDefinition> RecordDefinitions;
        }

        public class FixedFormatRecordDefinition
        {
            public string RecordIdentifier;
            public List<int> FieldLengths;
        }
    }
}