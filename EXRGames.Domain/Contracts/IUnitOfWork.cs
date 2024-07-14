using System.Data;

namespace EXRGames.Domain.Contracts {
    public interface IUnitOfWork {
        IDbTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
