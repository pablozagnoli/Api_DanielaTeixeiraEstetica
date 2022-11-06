using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace locacaoDeImoveis.DTO
{
  public class clientesDTO
  {
    public int imovel_id { get; set; }
    public string descricao_imovel { get; set; }
    public DateTime data_de_edicao { get; set; }
    public int? status { get; set; }
    public int? priority { get; set; }
    public string description { get; set; }
    public int? MyProperty { get; set; }
    public DateTime data_de_criacao { get; set; }

  }
}
