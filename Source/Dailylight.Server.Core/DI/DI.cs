using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace Dailylight.Server.Core
{
    public static class DI
    {
        public static ApplicationDbContext ApplicationDbContext => Framework.Service<ApplicationDbContext>();
    }
}
