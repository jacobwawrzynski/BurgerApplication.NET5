using DataBaseContext;
using DataBaseContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.MVVM.View
{
   /// <summary>
   /// Interaction logic for AdminPanelEmpView.xaml
   /// </summary>
   public partial class AdminPanelEmpView : UserControl
   {
      public AdminPanelEmpView()
      {
         InitializeComponent();
      }

      private void SearchBoxEmp_KeyUp(object sender, KeyEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            int value;
            if (SearchBoxEmp.Text != "" && int.TryParse(SearchBoxEmp.Text, out value))
            {
               var all = from i
                          in db.Staff
                         select i.Id;

               if (all.Contains(int.Parse(SearchBoxEmp.Text)))
               {
                  var pr = (from employees
                           in db.Staff
                            where int.Parse(SearchBoxEmp.Text) == employees.Id
                            select employees).FirstOrDefault();

                  var adr = (from addresses
                             in db.Addresses
                             where pr.Id_Address == addresses.Id
                             select addresses).First();

                  var res = (from restaurants
                             in db.Restaurants
                             where pr.Id_Restaurant == restaurants.Id_Address
                             select restaurants).First();

                  if (pr != null)
                  {
                     LoginField.Text = pr.Login;
                     PasswordField.Text = pr.Password;
                     ForenameField.Text = pr.Name;
                     LastNameField.Text = pr.Last_Name;
                     PeselField.Text = pr.Pesel;
                     EmailField.Text = pr.Email;
                     CreationDateField.Text = pr.Creation_Date.ToString();
                     RoleField.Text = pr.Role;
                     CityField.Text = adr.City;
                     ZipCodeField.Text = adr.Zip_Code;
                     StreetField.Text = adr.Street;
                     FlatNumberField.Text = adr.Apartment_Number;
                     HouseNumberField.Text = adr.House_Number;
                     RestaurantField.Text = res.Id_Address.ToString();
                     AddressField.Text = adr.Id.ToString();
                  }

               }
            }

         }
      }

      private void Add_Emp_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            Staff employee = new Staff();
            Address address = new Address();
            Restaurant restaurant = new Restaurant();

            employee.Login = LoginField.Text;
            employee.Password = PasswordField.Text;
            employee.Name = ForenameField.Text;
            employee.Last_Name = LastNameField.Text;
            employee.Pesel = PeselField.Text;
            employee.Role = RoleField.Text;
            employee.Id_Address = int.Parse(AddressField.Text);
            employee.Id_Restaurant = restaurant.Id_Address;
            DataBaseQuery.AddRestaurantToDataBase(restaurant);
            DataBaseQuery.AddStaffToDataBase(employee);

            address.City = CityField.Text;
            address.Zip_Code = ZipCodeField.Text;
            address.Street = StreetField.Text;
            address.House_Number = HouseNumberField.Text;
            address.Apartment_Number = FlatNumberField.Text;
            DataBaseQuery.AddAddressToDataBase(address);


            
         }
      }

      private void Remove_Emp_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var pr = from employees
                     in db.Staff
                     where employees.Id == int.Parse(SearchBoxEmp.Text)
                     select employees;

            foreach (var item in pr)
            {
               db.Staff.Remove(item);
            }
            db.SaveChanges();

         }
      }

      private void Save_Emp_Btn_Click(object sender, RoutedEventArgs e)
      {
         using (var db = new AppDbContext())
         {
            var pr = (from employees
                           in db.Staff
                      where employees.Id == int.Parse(SearchBoxEmp.Text)
                      select employees).FirstOrDefault();

            var adr = (from addresses
                             in db.Addresses
                       where pr.Id_Address == addresses.Id
                       select addresses).First();

            var res = (from restaurants
                            in db.Restaurants
                       where pr.Id_Restaurant == restaurants.Id_Address
                       select restaurants).First();

            if (pr != null)
            {
               pr.Login = LoginField.Text;
               pr.Password = PasswordField.Text;
               pr.Pesel = PeselField.Text;
               pr.Name = ForenameField.Text;
               pr.Last_Name = LastNameField.Text;
               pr.Role = RoleField.Text;
               pr.Email = EmailField.Text;
               adr.Street = StreetField.Text;
               adr.City = CityField.Text;
               adr.Apartment_Number = FlatNumberField.Text;
               adr.House_Number = HouseNumberField.Text;
               adr.Zip_Code = ZipCodeField.Text;
               res.Id_Address = int.Parse(RestaurantField.Text);
               pr.Id_Address = int.Parse(AddressField.Text);
               db.SaveChanges();
            }

         }
      }

   }
}
