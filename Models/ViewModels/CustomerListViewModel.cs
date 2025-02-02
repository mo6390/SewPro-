// SewPro/ViewModels/CustomerListViewModel.cs
using SewPro.Models;
using System.Collections.Generic;

namespace SewPro.ViewModels
{
    public class CustomerListViewModel
    {
        public List<Customer> Customers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchQuery { get; set; }
    }
}
