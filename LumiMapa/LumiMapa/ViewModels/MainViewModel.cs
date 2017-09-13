using LumiMapa.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public Command GetUserCommand { get; }
        public ObservableCollection<Luminosidade> Luminosidades { get; set; }

        public MainViewModel()
        {
            GetUserCommand = new Command(async () => await ExecuteGetUserCommand(),() => !IsBusy);
            Luminosidades = new ObservableCollection<Luminosidade>();
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
