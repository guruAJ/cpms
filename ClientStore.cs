using CPMS_AUTO.Data;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO
{
    public class ClientStore : IClientStore
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ClientStore(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ClientStore");
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _context.Clients.First(t => t.ClientId == clientId);

            client.MapDataFromEntity();
            return Task.FromResult(client.Client);
        }


    }
}
