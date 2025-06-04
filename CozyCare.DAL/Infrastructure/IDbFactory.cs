using CozyCare.DAL.DBContext;

namespace CozyCare.DAL.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CozyCareContext Init();
    }
}
