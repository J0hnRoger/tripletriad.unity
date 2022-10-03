
using System.Threading.Tasks;

namespace TripleTriad.Engine.Application
{
    public interface ILoaderManager
    {
        Task<Player> LoadCurrentPlayer();
        Task SavePlayer(Player current);
        Task SaveCurrentLevel(string levelName);
        
    }
}
