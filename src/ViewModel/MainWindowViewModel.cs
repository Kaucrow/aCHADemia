using Acadnetmy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Acadnetmy.MVVM;

namespace Acadnetmy.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            
        }

        private Item selectedItem;

        public Item SelectedItem
        {
            get { return selectedItem; }
            set {
                selectedItem = value;
                OnPropertyChanged();
            }
        }
    }
}
