using AppLanchonete.Services;
using AppLanchonete.Validations;
using System.ComponentModel.DataAnnotations;

namespace AppLanchonete.Pages;

public partial class InscricaoPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;

    public InscricaoPage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;

    }

    private async void BtnSignup_Clicked(object sender, EventArgs e)
    {
        if (await _validator.Validar(EntNome.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text))
        {
            var response = await _apiService.RegistrarUsuario(EntNome.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text);

            if (!response.HasError)
            {
                await DisplayAlert("Aviso", "Sua conta foi criada com sucesso!!!", "OK");
                await Navigation.PushAsync(new LoginPage(_apiService));
            }
            else
            {
                await DisplayAlert("Erro", "Algo deu errado!!!", "Cancelar");
            }
        }
        else
        {
            string mensagemErro = "";
            mensagemErro += _validator.NomeErro != null ? $"\n- {_validator.NomeErro}" : "";
            mensagemErro += _validator.EmailErro != null ? $"\n- {_validator.EmailErro}" : "";
            mensagemErro += _validator.TelefoneErro != null ? $"\n- {_validator.TelefoneErro}" : "";
            mensagemErro += _validator.SenhaErro != null ? $"\n- {_validator.SenhaErro}" : "";

            await DisplayAlert("Erro", mensagemErro, "OK");
        }

        
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_apiService));
    }
}