using System.Text.Json;
using M6_GO_v2.Models;

namespace M6_GO_v2;

public partial class CuidadorPage : ContentPage
{
	private DateTime dataAtual;
	private HttpClient http;
	private List<Atendimento> atendimentosList = new List<Atendimento>();

	public CuidadorPage(HttpClient http)
	{
		InitializeComponent();
		dataAtual = DateTime.Now;
		this.http = http;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await SetComboBox();
        await SetCalendary();
    }

    public async Task SetCalendary()
	{
		Calendar.Children.Clear();
		Calendar.RowDefinitions.Clear();
		Calendar.ColumnDefinitions.Clear();

		for (int i = 0; i < 7; i++)
		{
			Calendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
		}

		var dias = await GetDiasDoMes();
		var totalLinhas = (int)Math.Ceiling(dias.Count / 7.0);

		for (int i = 0; i < totalLinhas; i++)
		{
			Calendar.RowDefinitions.Add(new RowDefinition { Height = 60 });
		}

		for (int i = 0; i < dias.Count; i++)
		{
			var dia = dias[i];
			var linha = i / 7;
			var col = i % 7;

			var frame = CriarFrame(dia);
			Grid.SetRow(frame, linha);
			Grid.SetColumn(frame, col);
			Calendar.Children.Add(frame);
		}
	}

	private Frame CriarFrame(CalendaryDay dia)
	{
		var frame = new Frame
		{
			BackgroundColor = dia.CorFundo,
			BorderColor = Colors.LightGray,
			CornerRadius = 5,
			Padding = 5,
			Margin = 2,
			HasShadow = false,
		};

		var label = new Label
		{
			Text = dia.Day,
			TextColor = dia.CorTexto,
			FontSize = 16,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
		};

		if (dia.TemAtendimento)
		{
			label.FontAttributes = FontAttributes.Bold;
		}

		frame.Content = label;

		var tap = new TapGestureRecognizer();
		tap.Tapped += (s, e) => OnDayClick(dia.Data);
		frame.GestureRecognizers.Add(tap);
		return frame;
	}

	private async Task SetComboBox()
	{
		var response = await http.GetAsync("Atendimentos");
		if (response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadAsStringAsync();
			var atends = JsonSerializer.Deserialize<List<Atendimento>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			var id = Preferences.Default.Get("userId", 0);
			if (id == 0)
			{
                id = Preferences.Default.Get("tempId", 0);
            }
			atends = atends.Where(a => a.CuidadorId == id).ToList();

            atendimentosList = new List<Atendimento>(atends);

			var list = atends.Select(a => a.Idoso.IdNavigation.Nome).Distinct().ToList();
			var temp = new List<Atendimento>();
            foreach (var item in list)
            {
				temp.Add(atendimentosList.FirstOrDefault(a => a.Idoso.IdNavigation.Nome == item));
            }
			atendimentosList = temp;

            idosoPicker.ItemsSource = list;
			idosoPicker.SelectedIndex = 0;
		}
	}
    private void OnDayClick(DateTime data)
    {

    }

    private async Task<List<CalendaryDay>> GetDiasDoMes()
	{
		var dias = new List<CalendaryDay>();
		var primerDiaMes = new DateTime(dataAtual.Year, dataAtual.Month, 1);
		var ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);

		var diaSemanaInicio = (int)primerDiaMes.DayOfWeek;
		var ajusteInicio = diaSemanaInicio == 0 ? 6 : diaSemanaInicio - 1;
		var dataInicio = primerDiaMes.AddDays(-ajusteInicio);

		var diaSemanaFinal = (int)ultimoDiaMes.DayOfWeek;
		var ajusteFinal = diaSemanaFinal == 0 ? 0 : 7 - diaSemanaFinal;
		var dataFinal = ultimoDiaMes.AddDays(ajusteFinal);

		var actualDate = DateTime.Today;

		var response = await http.GetAsync("Atendimentos");
		if (response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadAsStringAsync();
			var atends = JsonSerializer.Deserialize<List<Atendimento>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

			var id = Preferences.Default.Get("userId", 0);

			if (id == 0)
			{
                id = Preferences.Default.Get("tempId", 0);
            }

            if (idosoPicker.SelectedIndex >= 0)
			{
                atends = atends.Where(a => a.IdosoId == atendimentosList[idosoPicker.SelectedIndex].IdosoId && a.CuidadorId == id && a.DataHora.Month == dataAtual.Month && a.DataHora.Year == dataAtual.Year).ToList();
			}
			else
			{
                atends = atends.Where(a => a.CuidadorId == id).ToList();
            }

            for (var data = dataInicio; data <= dataFinal; data = data.AddDays(1))
				{
					var ehDomesAStual = data.Month == dataAtual.Month && data.Year == dataAtual.Year;
				var tematendmento = atends.Any(a => DateOnly.FromDateTime(a.DataHora.Date) == DateOnly.FromDateTime(data.Date));

					var tipoData = data.Date < dataAtual.Date ? TipoAtendimeto.Passada
						: data.Date == dataAtual.Date ? TipoAtendimeto.Presente : TipoAtendimeto.Futura;

					var cortexto = ehDomesAStual ? Colors.Black : Colors.Gray;
					var corfundo = Color.FromArgb("#FFFFFF");

					if (tematendmento)
					{
						corfundo = tipoData switch
						{
							TipoAtendimeto.Passada => Color.FromArgb("#4CAF50"),
							TipoAtendimeto.Presente => Color.FromArgb("#FF9800"),
							TipoAtendimeto.Futura => Color.FromArgb("#2196F3"),
							_ => Color.FromArgb("#FFFFFF")
						};
						cortexto = Colors.White;
					}

					dias.Add(new CalendaryDay
					{
						Data = data,
						MesAtual = ehDomesAStual,
						TemAtendimento = tematendmento,
						Tipo = tipoData,
						CorFundo = corfundo,
						CorTexto = cortexto
					});
				}
		}
		return dias;
	}

	private async void OnTapToLogOut(object sender, TappedEventArgs e)
    {
		var id = Preferences.Default.Get("userId",0);
		if(id > 0)
		{
			id = 0;
		}
		await Shell.Current.GoToAsync("//LoginPage");
    }

    private void OnItemChanged(object sender, EventArgs e)
    {
		SetCalendary();
    }

    private void Prev(object sender, EventArgs e)
    {
		dataAtual = dataAtual.AddMonths(-1);
		anoLbl.Text = dataAtual.ToString("MMMM yyyy");
		SetCalendary();
    }

    private void Next(object sender, EventArgs e)
    {
        dataAtual = dataAtual.AddMonths(1);
        anoLbl.Text = dataAtual.ToString("MMMM yyyy");
        SetCalendary();
    }
}