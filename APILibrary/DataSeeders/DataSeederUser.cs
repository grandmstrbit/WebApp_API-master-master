using APILibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary.DataSeeders;

/// <summary>
/// Class DataSeederUser.
/// </summary>
public static class DataSeederUser
{
    /// <summary>
    /// Seeds the data.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    /// <returns>ModelBuilder.</returns>
    public static ModelBuilder SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "user1", Password = "user1", Email = "user1@stud.kai.ru" });
        modelBuilder.Entity<User>().HasData(new User { Id = 2, Name = "user2", Password = "user2", Email = "user2@stud.kai.ru" });
        modelBuilder.Entity<User>().HasData(new User { Id = 3, Name = "user3", Password = "user3", Email = "user3@stud.kai.ru" });
        modelBuilder.Entity<User>().HasData(new User { Id = 4, Name = "user4", Password = "user4", Email = "user4@stud.kai.ru" });
        modelBuilder.Entity<User>().HasData(new User { Id = 5, Name = "user5", Password = "user5", Email = "user5@stud.kai.ru" });
        modelBuilder.Entity<User>().HasData(new User { Id = 6, Name = "user6", Password = "user6", Email = "user6@stud.kai.ru" });
        modelBuilder.Entity<User>().HasData(new User { Id = 7, Name = "user7", Password = "user7", Email = "user7@stud.kai.ru" });
        modelBuilder.Entity<User>().HasData(new User { Id = 8, Name = "user8", Password = "user8", Email = "user8@stud.kai.ru" });

        return modelBuilder;
    }
}