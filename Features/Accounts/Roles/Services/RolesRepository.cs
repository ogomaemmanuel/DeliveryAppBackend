using DeliveryAppBackend.Data;
using DeliveryAppBackend.Features.Accounts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Features.Accounts.Roles.Services
{
    public class RolesRepository : Repository<AppRole, int>,IRolesRepository
    {
        public RolesRepository(DeliveryAppDbContext context) : base(context)
        {

        }
    }
}
