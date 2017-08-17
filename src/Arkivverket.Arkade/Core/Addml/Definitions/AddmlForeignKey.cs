using System.Collections.Generic;
using System.Text;

namespace Arkivverket.Arkade.Core.Addml.Definitions
{
    public class AddmlForeignKey
    {
        private const string CombinedKeyDelimiter = "|";

        private static int _counter;

        /// <summary>
        ///     The name of the foreign key defined in the ADDML. Autogenerated if not present in the ADDML.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The reference field on the other side, e.g. the primary key.
        /// </summary>
        public List<FieldIndex> ForeignKeyReferenceIndexes { get; set; }

        /// <summary>
        ///     The definition objects of the indexes in ForeignKeyReferenceIndexes.
        /// </summary>
        public List<AddmlFieldDefinition> ForeignKeyReferenceFields { get; set; }

        /// <summary>
        ///     The foreign key fields e.g. the personId foreign key referencing a person table with an id field.
        /// </summary>
        public List<FieldIndex> ForeignKeyIndexes { get; set; }

        /// <summary>
        ///     The definition objects of the foreign keys on the current record.
        /// </summary>
        public List<AddmlFieldDefinition> ForeignKeys { get; }

        /// <summary>
        ///     Holds all the values of this foreign key. Combined foreign keys are concatenated to a string separated by a
        ///     delimiter, currently the pipe (|).
        /// </summary>
        public HashSet<string> Values { get; }


        public AddmlForeignKey(string name)
        {
            Name = InitializeName(name);

            ForeignKeyReferenceIndexes = new List<FieldIndex>();
            ForeignKeyReferenceFields = new List<AddmlFieldDefinition>();
            ForeignKeys = new List<AddmlFieldDefinition>();
            Values = new HashSet<string>();
        }

        private string InitializeName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }
            _counter++;
            return "fk" + _counter;
        }

        public string GetForeignKeyReferenceIndexesAsString()
        {
            return CombineFieldIndexesToString(ForeignKeyReferenceIndexes);
        }

        public void AddValue(List<AddmlForeignKeyValue> foreignKeyValues)
        {
            string value = CombineValuesToSingleValue(foreignKeyValues);
            Values.Add(value);
        }

        private string CombineValuesToSingleValue(List<AddmlForeignKeyValue> foreignKeyValues)
        {
            var builder = new StringBuilder();
            foreach (AddmlForeignKeyValue foreignKeyValue in foreignKeyValues)
            {
                if (builder.Length != 0)
                {
                    builder.Append(CombinedKeyDelimiter);
                }
                builder.Append(foreignKeyValue.Value);
            }
            return builder.ToString();
        }

        public string GetForeignKeyIndexesAsString()
        {
            return CombineFieldIndexesToString(ForeignKeyIndexes);
        }

        private string CombineFieldIndexesToString(List<FieldIndex> fieldIndexes)
        {
            var builder = new StringBuilder();
            foreach (FieldIndex index in fieldIndexes)
            {
                if (builder.Length != 0)
                {
                    builder.Append(CombinedKeyDelimiter);
                }
                builder.Append(index);
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Foreign key name: ").AppendLine(Name);

            builder.Append("fields:");
            ForeignKeyIndexes.ForEach(fk => builder.AppendLine(fk.ToString()));

            builder.Append("references:");
            ForeignKeyReferenceIndexes.ForEach(fkRef => builder.AppendLine(fkRef.ToString()));

            return builder.ToString();
        }
    }

    public class AddmlForeignKeyValue
    {
        public FieldIndex Index { get; set; }
        public string Value { get; set; }

        public AddmlForeignKeyValue(FieldIndex index, string value)
        {
            Index = index;
            Value = value;
        }
    }
}