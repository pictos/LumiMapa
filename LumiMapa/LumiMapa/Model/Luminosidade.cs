using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace LumiMapa.Model
{
    [DataTable("MedidorLuminosidade")]
    public class Luminosidade
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Latitude")]
        public double Lat { get; set; }
        [JsonProperty("Longitude")]
        public double Longi { get; set; }        
        [JsonProperty("Valor")]
        public double Valor { get; set; }
        [Version]
        public string AzureVersion { get; set; }
    }
}
