using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Security
{
    public class SecurityDBContext : IdentityDbContext
    {
        public SecurityDBContext(DbContextOptions<SecurityDBContext> options): base(options)
        {

        }

    }
}
