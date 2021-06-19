using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private readonly SalesWebMVCContext _context;
        private readonly bool haveData;
        private readonly List<Department> departments = new List<Department>();
        private readonly List<Seller> sellers = new List<Seller>();
        private readonly List<SalesRecord> salesRecords = new List<SalesRecord>();

        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
            haveData = _context.Department.Any() || _context.SalesRecord.Any() || _context.Seller.Any();
        }

        public void Seed()
        {
            if (haveData) return;

            SetDepartments();
            SetSellers();
            SetSalesRecords();

            _context.Department.AddRange(departments);
            _context.Seller.AddRange(sellers);
            _context.SalesRecord.AddRange(salesRecords);

            _context.SaveChanges();
        }

        private void SetSalesRecords()
        {
            salesRecords.Add(new SalesRecord(1, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, sellers[0]));
            salesRecords.Add(new SalesRecord(2, new DateTime(2018, 09, 4), 7000.0, SaleStatus.Billed, sellers[4]));
            salesRecords.Add(new SalesRecord(3, new DateTime(2018, 09, 13), 4000.0, SaleStatus.Canceled, sellers[3]));
            salesRecords.Add(new SalesRecord(4, new DateTime(2018, 09, 1), 8000.0, SaleStatus.Billed, sellers[0]));
            salesRecords.Add(new SalesRecord(5, new DateTime(2018, 09, 21), 3000.0, SaleStatus.Billed, sellers[2]));
            salesRecords.Add(new SalesRecord(6, new DateTime(2018, 09, 15), 2000.0, SaleStatus.Billed, sellers[0]));
            salesRecords.Add(new SalesRecord(7, new DateTime(2018, 09, 28), 13000.0, SaleStatus.Billed, sellers[1]));
            salesRecords.Add(new SalesRecord(8, new DateTime(2018, 09, 11), 4000.0, SaleStatus.Billed, sellers[3]));
            salesRecords.Add(new SalesRecord(9, new DateTime(2018, 09, 14), 11000.0, SaleStatus.Pending, sellers[5]));
            salesRecords.Add(new SalesRecord(10, new DateTime(2018, 09, 7), 9000.0, SaleStatus.Billed, sellers[5]));
            salesRecords.Add(new SalesRecord(11, new DateTime(2018, 09, 13), 6000.0, SaleStatus.Billed, sellers[1]));
            salesRecords.Add(new SalesRecord(12, new DateTime(2018, 09, 25), 7000.0, SaleStatus.Pending, sellers[2]));
            salesRecords.Add(new SalesRecord(13, new DateTime(2018, 09, 29), 10000.0, SaleStatus.Billed, sellers[3]));
            salesRecords.Add(new SalesRecord(14, new DateTime(2018, 09, 4), 3000.0, SaleStatus.Billed, sellers[4]));
            salesRecords.Add(new SalesRecord(15, new DateTime(2018, 09, 12), 4000.0, SaleStatus.Billed, sellers[0]));
            salesRecords.Add(new SalesRecord(16, new DateTime(2018, 10, 5), 2000.0, SaleStatus.Billed, sellers[3]));
            salesRecords.Add(new SalesRecord(17, new DateTime(2018, 10, 1), 12000.0, SaleStatus.Billed, sellers[0]));
            salesRecords.Add(new SalesRecord(18, new DateTime(2018, 10, 24), 6000.0, SaleStatus.Billed, sellers[2]));
            salesRecords.Add(new SalesRecord(19, new DateTime(2018, 10, 22), 8000.0, SaleStatus.Billed, sellers[4]));
            salesRecords.Add(new SalesRecord(20, new DateTime(2018, 10, 15), 8000.0, SaleStatus.Billed, sellers[5]));
            salesRecords.Add(new SalesRecord(21, new DateTime(2018, 10, 17), 9000.0, SaleStatus.Billed, sellers[1]));
            salesRecords.Add(new SalesRecord(22, new DateTime(2018, 10, 24), 4000.0, SaleStatus.Billed, sellers[3]));
            salesRecords.Add(new SalesRecord(23, new DateTime(2018, 10, 19), 11000.0, SaleStatus.Canceled, sellers[1]));
            salesRecords.Add(new SalesRecord(24, new DateTime(2018, 10, 12), 8000.0, SaleStatus.Billed, sellers[4]));
            salesRecords.Add(new SalesRecord(25, new DateTime(2018, 10, 31), 7000.0, SaleStatus.Billed, sellers[2]));
            salesRecords.Add(new SalesRecord(26, new DateTime(2018, 10, 6), 5000.0, SaleStatus.Billed, sellers[3]));
            salesRecords.Add(new SalesRecord(27, new DateTime(2018, 10, 13), 9000.0, SaleStatus.Pending, sellers[0]));
            salesRecords.Add(new SalesRecord(28, new DateTime(2018, 10, 7), 4000.0, SaleStatus.Billed, sellers[2]));
            salesRecords.Add(new SalesRecord(29, new DateTime(2018, 10, 23), 12000.0, SaleStatus.Billed, sellers[4]));
            salesRecords.Add(new SalesRecord(30, new DateTime(2018, 10, 12), 5000.0, SaleStatus.Billed, sellers[1]));
        }

        private void SetSellers()
        {
            sellers.Add(new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, departments[0]));
            sellers.Add(new Seller(2, "Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3500.0, departments[1]));
            sellers.Add(new Seller(3, "Alex Gray", "alex@gmail.com", new DateTime(1988, 1, 15), 2200.0, departments[0]));
            sellers.Add(new Seller(4, "Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0, departments[3]));
            sellers.Add(new Seller(5, "Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 4000.0, departments[2]));
            sellers.Add(new Seller(6, "Alex Pink", "pink@gmail.com", new DateTime(1997, 3, 4), 3000.0, departments[1]));
        }

        private void SetDepartments()
        {
            departments.Add(new Department(1, "Computers"));
            departments.Add(new Department(2, "Electronics"));
            departments.Add(new Department(3, "Fashion"));
            departments.Add(new Department(4, "Books"));
        }
    }
}
