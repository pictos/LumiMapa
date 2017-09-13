using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LumiMapa.Model
{
    public class Repository
    {
        public async Task<List<Luminosidade>> PegarTabela()
        {
            var Service = new Services.AzureServices<Luminosidade>();
            var Itens = await Service.PegaTable();
            return Itens.ToList();
        }
    }
}
