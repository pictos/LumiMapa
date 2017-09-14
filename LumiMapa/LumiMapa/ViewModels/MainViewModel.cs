using LumiMapa.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace LumiMapa.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value;OnPropertyChanged(); GetUserCommand.ChangeCanExecute(); }
        }

        public static Map MeuMapa;
        public Command GetUserCommand { get; }
        public ObservableCollection<Luminosidade> Luminosidades { get; set; }

        public MainViewModel()
        {
            MeuMapa = new Map();
            GetUserCommand = new Command(async () => await ExecuteGetUserCommand(),() => !IsBusy);
            Luminosidades = new ObservableCollection<Luminosidade>();
            MeuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-21.53081506, -42.6340127),
                Distance.FromMeters(50)));
        }

        async Task ExecuteGetUserCommand()
        {
            if(!IsBusy)
            {
               Exception Error = null;
                try
                {
                    IsBusy = true;

                    var Repository = new Repository();
                    var Itens = await Repository.PegarTabela();
                    Luminosidades.Clear();
                    foreach (var leitura in Itens)
                    {
                        var posicao = new Position(leitura.Lat, leitura.Longi);
                        var pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = posicao,
                            Label = "Leitura",
                            Address =$"Valor de leitura: {leitura.Valor}"
                        };
                        MeuMapa.Pins.Add(pin);
                        Luminosidades.Add(leitura);
                    }                  
                    
                }
                catch (Exception ex)
                {

                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                }
                if(Error!=null)
                    await App.Current.MainPage.DisplayAlert("Erro!", Error.Message, "Ok");
            }
            return;
        }
    }
}
