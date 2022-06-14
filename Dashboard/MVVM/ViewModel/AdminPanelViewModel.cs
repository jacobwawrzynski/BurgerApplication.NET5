using Dashboard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.MVVM.ViewModel
{
   class AdminPanelViewModel : ObservableObject
   {
      public RelayCommand EmployeeCommand { get; set; }
      public RelayCommand MenuCommand { get; set; }
      public RelayCommand RegularCustomerCommand { get; set; }

      public AdminPanelEmpViewModel AdminEmployeeVM { get; set; }
      public AdminPanelMenuViewModel AdminMenuVW { get; set; }
      public AdminPanelRegCusViewModel AdminRegCusVW { get; set; }

      private object _currentAdminView;
      public object CurrentAdminView
      {
         get { return _currentAdminView; }
         set
         {
            _currentAdminView = value;
            OnPropertyChanged();
         }
      }

      public AdminPanelViewModel()
      {
         AdminEmployeeVM = new AdminPanelEmpViewModel();
         AdminMenuVW = new AdminPanelMenuViewModel();
         AdminRegCusVW = new AdminPanelRegCusViewModel();

         EmployeeCommand = new RelayCommand(command => { CurrentAdminView = AdminEmployeeVM; });
         MenuCommand = new RelayCommand(command => { CurrentAdminView = AdminMenuVW; });
         RegularCustomerCommand = new RelayCommand(command => { CurrentAdminView = AdminRegCusVW; });
      }
   }
}
