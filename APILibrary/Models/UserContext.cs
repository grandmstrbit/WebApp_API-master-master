﻿using APILibrary.DataSeeders;
using Microsoft.EntityFrameworkCore;

namespace APILibrary.Models;

/// <summary>
/// Class UsersContext.
/// Implements the <see cref="DbContext" />
/// Implements the <see cref="APILibrary.Models.IUserContext" />
/// </summary>
/// <seealso cref="DbContext" />
/// <seealso cref="APILibrary.Models.IUserContext" />
public class UsersContext : DbContext, IUserContext
{
    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>The users.</value>
    public DbSet<User?> Users { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersContext" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {

    }

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks><para>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run. However, it will still run when creating a compiled model.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
    /// examples.
    /// </para></remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder = DataSeederUser.SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}