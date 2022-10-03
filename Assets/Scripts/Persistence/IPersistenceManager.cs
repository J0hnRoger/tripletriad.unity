using System;
using System.Threading.Tasks;

namespace TripleTriad.Persistence
{
    public interface IPersistenceManager
    {
        public Task Save();
        public Task Load();
        
        public Action OnGameLoaded { get; set; }
    }
}