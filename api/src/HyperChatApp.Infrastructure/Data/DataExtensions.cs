using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HyperChatApp.Infrastructure.Data;

public static class DataExtensions
{
  public static void AddDbContexts(this IServiceCollection serviceCollection, string connectionString)
  {
    serviceCollection.AddDbContext<AppDbContext>(config => config.UseSqlServer(connectionString));
  }
}
