using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripleTriad.Infrastructure
{
    public interface IPersistenceRepository
    {
        Task SaveData(Dictionary<int, string> jsonDatas);
        Task<Dictionary<int, string>> LoadData();
    }
}