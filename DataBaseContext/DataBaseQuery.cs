﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using DataBaseContext.Entities;
namespace DataBaseContext
{
    static class DataBaseQuery
    {
        public static bool AddAddressToDataBase(Address address)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Addresses.Add(address);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddRestaurantToDataBase(Restaurant restaurant)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Restaurants.Add(restaurant);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddStaffToDataBase(Staff staff)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Staff.Add(staff);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddAllergenToDataBase(Allergen allergen)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Allergens.Add(allergen);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddCustomerToDataBase(Customer customer)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
        public static bool AddDeliveryToDataBase(Delivery delivery)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Delivery.Add(delivery);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddInvoiceToDataBase(Invoice invoice)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddDiscountCodeToDataBase(Discount_Code discount_Code)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Discount_Codes.Add(discount_Code);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddOrderToDataBase(Order order)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddProductToDataBase(Product product)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddProduct_OrderToDataBase(Product_Order product_Order)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Products_Orders.Add(product_Order);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddProduct_AllergenToDataBase(Product_Allergen product_Allergen)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Products_Allergens.Add(product_Allergen);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static bool AddReportsToDataBase(Report report)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Reports.Add(report);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static List<Product> DownloadProductsFromOrder(Order order)
        {
            using (var db = new AppDbContext())
            {
                return (from p in db.Products
                        join po in db.Products_Orders on p.Id equals po.Id_Product
                        where po.Id_Order == order.Id
                        select p).ToList();
            }

        }
        public static List<Invoice> DownloadInvoice()
        {
            using (var db = new AppDbContext())
            {
                return (from i in db.Invoices
                        select i).ToList();
            }
        }
        public static Invoice DownloadInvoiceAt(int index)
        {
            using (var db = new AppDbContext())
            {
                return (from i in db.Invoices
                        where i.Id == index
                        select i).LastOrDefault();
            }
        }
    }
}
