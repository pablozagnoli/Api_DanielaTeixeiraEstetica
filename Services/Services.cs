using locacaoDeImoveis.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace locacaoDeImoveis.Controllers
{
  public class Services
  {

    public async Task<ViaCepDTO> GetEnderecoAsync(string baseUrl, string url)
    {
      try
      {
        var result = await GetAsync(baseUrl, url);
        var endereco = JsonConvert.DeserializeObject<ViaCepDTO>(result);

        return endereco;
      }
      catch (Exception)
      {
        return new ViaCepDTO()
        {
          cep = "",
          bairro = "ENDEREÇO NÃO ENCONTRADO",
          complemento = "",
          localidade = "",
          logradouro = "",
          uf  = ""
        };
      }

    }

    public static async Task<string> PostAsync(string baseUrl)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(baseUrl);
        var content = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("", "login")
            });
        var result = await client.PostAsync("/api/Membership/exists", content);
        string resultContent = await result.Content.ReadAsStringAsync();

        return resultContent;
      }
    }

    public static async Task<string> GetAsync(string baseUrl, string url)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(baseUrl);

        var result = await client.GetAsync(url);
        string resultContent = await result.Content.ReadAsStringAsync();

        return resultContent;
      }
    }
  }
}
