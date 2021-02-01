using System.Data;

namespace Macerus.Plugins.Features.Data.Api
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
