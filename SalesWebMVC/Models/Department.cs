using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> LstSellers { get; set; } = new List<Seller>();

        public Department()
        {

        }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller s)
        {
            LstSellers.Add(s);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return (from s in LstSellers
                    select s.TotalSales(initial, final)
                    ).Sum();


            //double t = LstSellers.Sum(s => s.TotalSales(initial, final));
        }



    }
}
