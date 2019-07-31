using System;
using System.Collections.Generic;
using System.Linq;

namespace Arkivverket.Arkade.Core.Base.Noark5
{
    public class ReadElementEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ElementPath Path { get; set; }

        public ReadElementEventArgs(string name, string value, ElementPath path)
        {
            Name = name;
            Value = value;
            Path = path;
        }

        public bool NameEquals(string inputName)
        {
            return StringEquals(Name, inputName);
        }

        private bool StringEquals(string input1, string input2)
        {
            return string.Equals(input1, input2, StringComparison.CurrentCultureIgnoreCase);
        }
    }

    public class ElementPath
    {
        private readonly List<XmlElement> _path;

        public ElementPath(List<XmlElement> path)
        {
            _path = path;
        }

        public bool Matches(params string[] elementNames)
        {
            if (!_path.Any())
                return false;

            var matches = true;
            for (var i = 0; i < elementNames.Length; i++)
            {
                matches = matches && string.Equals(elementNames[i], _path[i].Name, StringComparison.CurrentCultureIgnoreCase);
            }
            return matches;
        }

        public XmlElement GetParent()
        {
            return _path.Count > 1 ? _path[1] : null;
        }
        
        public int GetSameElementSubLevel(int level = 1)
        {
            if (_path[level].Equals(_path[0]))
                level = GetSameElementSubLevel(level + 1);

            return level;
        }
    }
}