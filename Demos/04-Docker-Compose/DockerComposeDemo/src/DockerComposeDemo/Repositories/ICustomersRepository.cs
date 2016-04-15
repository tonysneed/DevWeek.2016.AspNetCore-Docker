using System.Collections.Generic;
using System.Threading.Tasks;
using DockerComposeDemo.Models;

namespace DockerComposeDemo.Repositories
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
    }
}