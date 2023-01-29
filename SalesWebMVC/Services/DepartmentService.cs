using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;
        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;

        }

        public ICollection<Department> FindAll()
        {
            return _context.Department.OrderBy(c => c.Name).ToList();
        }
        

    }
}
