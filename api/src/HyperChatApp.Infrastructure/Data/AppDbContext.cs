using System.Reflection;
using HyperChatApp.Core.Aggregates.MessageAggregate;
using HyperChatApp.Core.Aggregates.RoomAggregate;
using HyperChatApp.Core.Aggregates.UserInfoAggregate;
using Microsoft.EntityFrameworkCore;

namespace HyperChatApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {
  }

  public DbSet<UserInfo> UserInfos => Set<UserInfo>();
  public DbSet<Room> Rooms => Set<Room>();
  public DbSet<Message> Messages => Set<Message>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public void EnsureCreated()
  {
    Database.Migrate();
    Database.EnsureCreated();
  }
}
