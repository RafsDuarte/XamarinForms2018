using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using APP01_ConsultarCEP.Servico;
using APP01_ConsultarCEP.Servico.Modelo;
namespace APP01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            // Botão BuscarCEP
            BOTAO.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            // Lógica programa.
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} de {3}, {0}, {1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
               
            }
        }

        // Validações.
        private bool isValidCEP(string cep)
        {
            bool valido = true;
            int NovoCEP = 0;

            if (cep.Length != 8 && !int.TryParse(cep, out NovoCEP))
            {
                // Mensagem ERRO.
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 caracteres e ser composto apenas por números.", "OK");
                valido = false;
            }
            else if (cep.Length != 8)
            {
                // Mensagem ERRO.
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }
            else if (!int.TryParse(cep, out NovoCEP))
            {
                // Mensagem ERRO.
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }
            return valido;
        }
    }
}
