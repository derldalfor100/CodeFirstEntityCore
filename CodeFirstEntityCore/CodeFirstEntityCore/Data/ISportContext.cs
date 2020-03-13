using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodeFirstEntityCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirstEntityCore.Data
{
    public interface ISportContext
    {
        DbSet<Team> Teams { get; set; }
        DbSet<Player> Players { get; set; }
    }
}
