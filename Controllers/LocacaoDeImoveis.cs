using locacaoDeImoveis.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace locacaoDeImoveis.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class LocacaoDeImoveis : ControllerBase
  {


    private readonly ILogger<LocacaoDeImoveis> _logger;

    public LocacaoDeImoveis(ILogger<LocacaoDeImoveis> logger)
    {
      _logger = logger;
    }

    [EnableCors("AnotherPolicy")]
    [HttpPost("criar")]
    public int Criar([FromBody] imoveisDTO imovel)
    {
      var service = new Repository();

      var retorno = service.QueryInsertImovel(imovel);

      return retorno;
    }

    [EnableCors("Policy1")]
    [HttpGet("trazer")]
    public List<imoveisDTO> Trazer()
    {
      var service = new Repository();
      var retorno = service.QuerySelectIAllmoveis();

      return retorno;
    }

    [EnableCors("Policy1")]
    [HttpGet("endereco/{cep}")]
    public async Task<ViaCepDTO> TrazerEnderecoAsync(string cep)
    {
      var service = new Services();
      var retorno = await service.GetEnderecoAsync($"https://viacep.com.br/ws/", cep +"/json/");

      return retorno;
    }

    [EnableCors("Policy1")]
    [HttpGet("trazer/{id}")]
    public imoveisDTO TrazerUm(int id)
    {
      var service = new Repository();
      var retorno = service.QuerySelectIOneImovel(id);

      return retorno;
    }


    [EnableCors("AnotherPolicy")]
    [HttpDelete("deletar/{id}")]
    public int Deletar(int id)
    {
      var service = new Repository();
      var retorno = service.QueryDeleteOneImovel(id);

      return retorno;
    }

    [EnableCors("AnotherPolicy")]
    [HttpPut("atualizar/{id}")]
    public int Atualiza([FromBody] imoveisDTO imovel, int id)
    {
      imovel.imovel_id = id;
      var service = new Repository();
      var retorno = service.QueryUpdatetImovel(imovel);

      return retorno;
    }
  }
}
