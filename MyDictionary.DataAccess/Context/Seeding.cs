using Microsoft.EntityFrameworkCore;
using MyDictionary.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.DataAccess.Context
{
    public class Seeding
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User[] { new User { Id = 1, Name = "LNS" } });
        }
    }
}
