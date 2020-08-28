using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using StudentDataService.Entity.POCO;
using StudentDataService.Entity.POCO.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Context
{
    public class EntityContext : DbContext, IEntityContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties();
                if (properties != null)
                {
                    foreach (var property in properties)
                    {
                        var uniqueKeys = GetUniqueKeyAttributes(entityType, property);
                        foreach (var uniqueKey in uniqueKeys.Where(x => x.Order == 0))
                        {
                            if (String.IsNullOrWhiteSpace(uniqueKey.GroupId)) // Single column Unique Key
                            { entityType.AddIndex(property).IsUnique = true; }
                            else // Multiple column Unique Key
                            {
                                var mutableProperties = new List<IMutableProperty>();
                                properties.ToList().ForEach(x =>
                                {
                                    var uks = GetUniqueKeyAttributes(entityType, x);
                                    if (uks != null)
                                    {
                                        foreach (var uk in uks)
                                        {
                                            if ((uk != null) && (uk.GroupId == uniqueKey.GroupId))
                                            { mutableProperties.Add(x); }
                                        }
                                    }
                                });

                                entityType.AddIndex(mutableProperties).IsUnique = true;
                            }
                        }
                    }
                }

                base.OnModelCreating(modelBuilder);
            }
        }

        private static IEnumerable<UniqueKeyAttribute> GetUniqueKeyAttributes(IMutableEntityType entityType, IMutableProperty property)
        {
            if (entityType == null)
            { throw new ArgumentNullException(nameof(entityType)); }
            else if (entityType.ClrType == null)
            { throw new ArgumentNullException(nameof(entityType.ClrType)); }
            else if (property == null)
            { throw new ArgumentNullException(nameof(property)); }
            else if (property.Name == null)
            { throw new ArgumentNullException(nameof(property.Name)); }

            var propInfo = entityType.ClrType.GetProperty(
                property.Name,
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly);

            if (propInfo == null)
            { return null; }

            return propInfo.GetCustomAttributes<UniqueKeyAttribute>();
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<StudentToGroupEntity> StudentToGroup { get; set; }
    }
}