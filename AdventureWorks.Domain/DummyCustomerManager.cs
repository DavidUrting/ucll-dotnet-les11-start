using AdventureWorks.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorks.Domain
{
    public class DummyCustomerManager : ICustomerManager
    {
        private static List<Customer> _dummyData = new List<Customer>();

        static DummyCustomerManager()
        {
            _dummyData.Add(new Customer() { Id = 1, FirstName = "Dummy1", LastName = "Dummy1", Email = "dummy1@example.com" });
            _dummyData.Add(new Customer() { Id = 2, FirstName = "Dummy2", LastName = "Dummy2", Email = "dummy2@example.com" });
        }

        public Customer GetCustomer(int id)
        {
            return _dummyData.Where(c => c.Id == id).FirstOrDefault();
        }

        public IList<Customer> SearchCustomers(string keyword)
        {
            keyword = keyword.ToLower();
            return _dummyData.Where(c => c.FirstName.ToLower().Contains(keyword)
                || c.LastName.ToLower().Contains(keyword)
                || c.Email.ToLower().Contains(keyword)).ToList();
        }

        public Customer InsertCustomer(Customer customer)
        {
            customer.Id = _dummyData.Select(c => c.Id).Max() + 1;
            _dummyData.Add(customer);
            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            Customer storedCustomer = _dummyData.Where(c =>  c.Id == customer.Id).FirstOrDefault();
            storedCustomer.FirstName = customer.FirstName;
            storedCustomer.LastName = customer.LastName;
            storedCustomer.Email = customer.Email;
            return storedCustomer;
        }

        public void DeleteCustomer(int id)
        {
            _dummyData = _dummyData.Where(c => c.Id != id).ToList();
        }
    }
}
