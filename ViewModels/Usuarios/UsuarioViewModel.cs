using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        
        private UsuarioService uService;
        public ICommand AutenticarCommand { get; set; }
        public UsuarioViewModel()
        {
            uService = new UsuarioService();
            InicializarComands();
        }

        public void InicializarComands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
        }

        private string login = string.Empty;
        public string Login 
        { 
            get { return login; } 
            set
            {  
                login = value;
                OnPropertyChanged();
            }
        }

        private string senha = string.Empty;
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Userame = Login;
                u.PasswordString = Senha;

                Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem-vindo(a) {uAutenticado.Userame}";

                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Userame);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);
                    Preferences.Set("UsuarioToken", uAutenticado.Token);

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Dados incorretos :( ", "Ok");
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        
    }
}
