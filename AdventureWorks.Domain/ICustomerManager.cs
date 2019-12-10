using AdventureWorks.Domain.Models;
using System.Collections.Generic;

namespace AdventureWorks.Domain
{
    public interface ICustomerManager
    {
        IList<Customer> SearchCustomers(string keyword);
        Customer GetCustomer(int id);

        Customer InsertCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
