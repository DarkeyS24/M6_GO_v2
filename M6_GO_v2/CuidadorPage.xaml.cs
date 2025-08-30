using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace M6_GO_v2;

public partial class CuidadorPage : ContentPage
{
	private DateTime dataAtual;

	public CuidadorPage()
	{
		InitializeComponent();
		dataAtual = DateTime.Now;
	}

	public void SetCalendary()
	{
		Calendar.Children.Clear();
		Calendar.RowDefinitions.Clear();
		Calendar.ColumnDefinitions.Clear();

        for (int i = 0; i < 7; i++)
        {
			Calendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)});
        }

		//var dias = GetDiasDoMes();
		//var totalLinhas = (int)Math.Ceiling(dias.Count / 7);
    }

  //  private object GetDiasDoMes()
  //  {
		//var primerDiaMes = new DateTime(dataAtual.Year, dataAtual.Month, 1);
		//var diasMes = DateTime.DaysInMonth(primerDiaMes.Year, primerDiaMes.Month);
		//var ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);

		//var diaSemanaInicio = (int)primerDiaMes.DayOfWeek;
		//var ajusteInicio = diaSemanaInicio == 0 ? 6 : diaSemanaInicio - 1;
		//var dataInicio = primerDiaMes.AddDays(-ajusteInicio);

		//var diaSemanaFinal = (int)ultimoDiaMes.DayOfWeek;
		//var ajusteFinal = diaSemanaFinal == 0 ? 0 : 7 - diaSemanaFinal;
		//var dataFinal = ultimoDiaMes.AddDays(ajusteFinal);
  //  }

    private async void OnTapToLogOut(object sender, TappedEventArgs e)
    {
		var id = Preferences.Default.Get("userId",0);
		if(id > 0)
		{
			id = 0;
		}
		await Shell.Current.GoToAsync("//LoginPage");
    }
}