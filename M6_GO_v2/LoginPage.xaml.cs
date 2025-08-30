using System.Net.Mail;
using System.Text;
using System.Text.Json;
using M6_GO_v2.Models;

namespace M6_GO_v2;

public partial class LoginPage : ContentPage
{
	private readonly HttpClient http;
	private Random random;
	private int pin;
	private Usuario usuarioAtual = new Usuario();


    public LoginPage(HttpClient http)
	{
		InitializeComponent();
		this.http = http;
		random = new Random();
	}
    private async void OnClickToLogin(object sender, EventArgs e)
    {
		if (string.IsNullOrWhiteSpace(senha.Text) || string.IsNullOrWhiteSpace(usuario.Text))
		{
			DisplayAlert("Dados Invalidos", "Insira as suas credenciais", "Ok");
			return;
		}
		else
		{
			UserAuth user = new UserAuth() 
			{
				Email = usuario.Text,	
				Senha = senha.Text
			};
			var json = JsonSerializer.Serialize(user);
			var response = await http.PostAsync("Usuarios", new StringContent(json, Encoding.UTF8, "application/json"));
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var usuario = JsonSerializer.Deserialize<Usuario>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
				if (usuario != null)
				{
					usuarioAtual = usuario;

					loginLayout.IsVisible = false;
					pinLayout.IsVisible = true;
					pin = random.Next(100, 1000);
					labelPin.Text = pin.ToString();
                }
			}
			else
			{
				await DisplayAlert("Credenciais Invalidas", "Usuario não registrado no sistema","Ok");
			}
		}
    }

    public class UserAuth
    {
        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;
    }

    private void OnCancelToLogin(object sender, EventArgs e)
    {
        SwapPages();
    }

    private async void OnConfirmToLogin(object sender, EventArgs e)
    {
		if (pin == int.Parse(entryPin.Text))
		{
			if (check.IsChecked)
			{
				Preferences.Default.Set("userId", usuarioAtual.Id);
			}
			else
			{
                Preferences.Default.Set("tempId", usuarioAtual.Id);
            }

			if (usuarioAtual.Id > 7)
			{
				indicator.IsVisible = true;
				indicator.IsRunning = true;
				Task.Delay(5000);
				await Shell.Current.GoToAsync("//IdosoPage");
			}
			else
			{
				indicator.IsVisible = true;
				indicator.IsRunning = true;
				Task.Delay(5000);
				await Shell.Current.GoToAsync("//CuidadorPage");
			}
            SwapPages();
        }
		else
        {
            SwapPages();
        }
    }

    private void SwapPages()
    {
        usuario.Text = string.Empty;
        senha.Text = string.Empty;
        check.IsChecked = false;
		entryPin.Text = string.Empty;

        pinLayout.IsVisible = false;
        loginLayout.IsVisible = true;
    }
}