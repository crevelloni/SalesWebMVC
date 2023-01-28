using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Data
{
    public class SeedingServices
    {
        private readonly SalesWebMVCContext _context;

        public SeedingServices(SalesWebMVCContext salesWebMVCContext)
        {
            _context = salesWebMVCContext;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return;
            }

            var lstDepartment = new List<Department>
            {
                new Department(1, "Computers"),
                new Department(2, "Electronics"),
                new Department(3, "Fashion"),
                new Department(4, "Books")
             };

            var lstSellers = new List<Seller>
            {
                new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, lstDepartment[0]),
                new Seller(2, "Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3500.0, lstDepartment[1]),
                new Seller(3, "Alex Grey", "alex@gmail.com", new DateTime(1988, 1, 15), 2200.0, lstDepartment[2]),
                new Seller(4, "Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0, lstDepartment[3]),
                new Seller(5, "Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 4000.0, lstDepartment[1]),
                new Seller(6, "Alex Pink", "bob@gmail.com", new DateTime(1997, 3, 4), 3000.0, lstDepartment[0])
            };


            var lstRecords = new List<SalesRecord>
            {
              new SalesRecord(1, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, lstSellers[0]),
              new SalesRecord(2, new DateTime(2018, 09, 4), 7000.0, SaleStatus.Billed, lstSellers[4]),
              new SalesRecord(3, new DateTime(2018, 09, 13), 4000.0, SaleStatus.Canceled, lstSellers[3]),
              new SalesRecord(4, new DateTime(2018, 09, 1), 8000.0, SaleStatus.Billed, lstSellers[0]),
              new SalesRecord(5, new DateTime(2018, 09, 21), 3000.0, SaleStatus.Billed, lstSellers[2]),
              new SalesRecord(6, new DateTime(2018, 09, 15), 2000.0, SaleStatus.Billed, lstSellers[0]),
              new SalesRecord(7, new DateTime(2018, 09, 28), 13000.0, SaleStatus.Billed, lstSellers[1]),
              new SalesRecord(8, new DateTime(2018, 09, 11), 4000.0, SaleStatus.Billed, lstSellers[3]),
              new SalesRecord(9, new DateTime(2018, 09, 14), 11000.0, SaleStatus.Pending, lstSellers[5]),
              new SalesRecord(10, new DateTime(2018, 09, 7), 9000.0, SaleStatus.Billed, lstSellers[5]),
              new SalesRecord(11, new DateTime(2018, 09, 13), 6000.0, SaleStatus.Billed, lstSellers[1]),
              new SalesRecord(12, new DateTime(2018, 09, 25), 7000.0, SaleStatus.Pending, lstSellers[2]),
              new SalesRecord(13, new DateTime(2018, 09, 29), 10000.0, SaleStatus.Billed, lstSellers[3]),
              new SalesRecord(14, new DateTime(2018, 09, 4), 3000.0, SaleStatus.Billed, lstSellers[4]),
              new SalesRecord(15, new DateTime(2018, 09, 12), 4000.0, SaleStatus.Billed, lstSellers[0]),
              new SalesRecord(16, new DateTime(2018, 10, 5), 2000.0, SaleStatus.Billed, lstSellers[3]),
              new SalesRecord(17, new DateTime(2018, 10, 1), 12000.0, SaleStatus.Billed, lstSellers[0]),
              new SalesRecord(18, new DateTime(2018, 10, 24), 6000.0, SaleStatus.Billed, lstSellers[2]),
              new SalesRecord(19, new DateTime(2018, 10, 22), 8000.0, SaleStatus.Billed, lstSellers[4]),
              new SalesRecord(20, new DateTime(2018, 10, 15), 8000.0, SaleStatus.Billed, lstSellers[5]),
              new SalesRecord(21, new DateTime(2018, 10, 17), 9000.0, SaleStatus.Billed, lstSellers[1]),
              new SalesRecord(22, new DateTime(2018, 10, 24), 4000.0, SaleStatus.Billed, lstSellers[3]),
              new SalesRecord(23, new DateTime(2018, 10, 19), 11000.0, SaleStatus.Canceled, lstSellers[1]),
              new SalesRecord(24, new DateTime(2018, 10, 12), 8000.0, SaleStatus.Billed, lstSellers[4]),
              new SalesRecord(25, new DateTime(2018, 10, 31), 7000.0, SaleStatus.Billed, lstSellers[2]),
              new SalesRecord(26, new DateTime(2018, 10, 6), 5000.0, SaleStatus.Billed, lstSellers[3]),
              new SalesRecord(27, new DateTime(2018, 10, 13), 9000.0, SaleStatus.Pending, lstSellers[0]),
              new SalesRecord(28, new DateTime(2018, 10, 7), 4000.0, SaleStatus.Billed, lstSellers[2]),
              new SalesRecord(29, new DateTime(2018, 10, 23), 12000.0, SaleStatus.Billed, lstSellers[4]),
              new SalesRecord(30, new DateTime(2018, 10, 12), 5000.0, SaleStatus.Billed, lstSellers[1])
            };


            _context.Department.AddRange(lstDepartment);

            _context.Seller.AddRange(lstSellers);

            _context.SalesRecord.AddRange(lstRecords);

            _context.SaveChanges();


        }

    }
}
