using DiceGaming.Core.Models;
using System.Collections.Generic;

namespace DiceGaming.Core.Repositories
{
    public interface IBetRepository
    { 
        BetDto Get(int id);
        IEnumerable<BetDto> GetBets(int userId, int skip, int take, string orderby, string filter);
        BetDto Create(BetDto bet);
        void Delete(int id);
    }
}
