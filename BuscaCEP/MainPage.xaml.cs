using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace BuscaCEP
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCep.Text))
                    throw new InvalidOperationException(message: "Você precisa informar o CEP");

                    lblLogradouro.Text =
                    lblBairro.Text =
                    lblLocalidade.Text =
                    lblUF.Text =
                    lblDDD.Text = "Carregando..";
                    

                    var viaCepResult = await new HttpClient().GetFromJsonAsync<ViaCepDto>(requestUri: $"https://viacep.com.br/ws/{txtCep.Text}/json/")
                    ??
                    throw new InvalidOperationException(message: "Algo não deu certo ao consultar o CEP");

                    if(viaCepResult.erro)
                    throw new InvalidOperationException(message: "Algo não deu certo ao consultar o CEP");

                        lblLogradouro.Text = viaCepResult.logradouro;
                        lblBairro.Text = viaCepResult.bairro;
                        lblLocalidade.Text = viaCepResult.localidade;
                        lblUF.Text = viaCepResult.uf;
                        lblDDD.Text = viaCepResult.ddd;
                    
                
            }catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
            
        }

        class ViaCepDto
        {

            public bool erro { get; set; }

            public string cep { get; set; }
            public string logradouro { get; set; }
            public string complemento { get; set; }
            public string unidade { get; set; }
            public string bairro { get; set; }
            public string localidade { get; set; }
            public string uf { get; set; }
            public string estado { get; set; }
            public string regiao { get; set; }
            public string ibge { get; set; }
            public string gia { get; set; }
            public string ddd { get; set; }
            public string siafi { get; set; }
        }

    }

}
