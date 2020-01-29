using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkivverket.Arkade.GUI.Models
{
    public class SelectableTest : BindableBase
    {
        private string _Name;
        private bool _Checked;
        private uint _Id;

        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        public bool Checked
        {
            get { return _Checked; }
            set { SetProperty(ref _Checked, value); }
        }

        public uint Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }
    }
}
