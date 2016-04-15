using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DockerComposeDemo.Config;
using DockerComposeDemo.Models;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json;

namespace DockerComposeDemo.Repositories
{
    public class CustomersRepository: ICustomersRepository
    {
        private const string CustomerService = "customer/";
        private readonly IOptions<Settings> _settingsOptions;
        public CustomersRepository(IOptions<Settings> settingsOptions)
        {
            _settingsOptions = settingsOptions;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var client = new HttpClient { BaseAddress = new Uri(_settingsOptions.Value.ServiceBaseAddress) };
            var response = await client.GetAsync(CustomerService);
            response.EnsureSuccessStatusCode();
            var customersJson = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(customersJson);
            return customers;
        }
    }
}