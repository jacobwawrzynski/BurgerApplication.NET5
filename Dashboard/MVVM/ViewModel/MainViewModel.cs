using Dashboard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dashboard.MVVM.ViewModel
{
   class MainViewModel : ObservableObject
   {
      public RelayCommand DeliveryCommand { get; set; }
      public RelayCommand CartCommand { get; set; }
      public RelayCommand SalesCommand { get; set; }
      public RelayCommand RaportCommand { get; set; }
      public RelayCommand MainCommand { get; set; }


      public DeliveryViewModel DeliveryVM { get; set; }
      public CartViewModel CartVM { get; set; }
      public RaportViewModel RaportVM { get; set; }
      public SalesViewModel SalesVM { get; set; }
      public MainViewModel MainVM { get; set; }

      
      private object _currentView;
      public object CurrentView
      {
         get { return _currentView; }
         set
         { 
            _currentView = value;
            OnPropertyChanged();
         }
      }


      public MainViewModel()
      {
         SalesVM = new SalesViewModel();
         CartVM = new CartViewModel();
         RaportVM = new RaportViewModel();
         DeliveryVM = new DeliveryViewModel();


         SalesCommand = new RelayCommand(command => { CurrentView = SalesVM; });
         CartCommand = new RelayCommand(command => { CurrentView = CartVM; });
         RaportCommand = new RelayCommand(command => { CurrentView = RaportVM; });
         DeliveryCommand = new RelayCommand(command => { CurrentView = DeliveryVM; });
      }
   }
}
