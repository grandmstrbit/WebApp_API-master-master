using Microsoft.EntityFrameworkCore;

namespace APILibrary.Models;

/// <summary>
/// Interface IUserContext
/// </summary>
public interface IUserContext
{
    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>The users.</value>
    DbSet<User?> Users { get; set; }
}