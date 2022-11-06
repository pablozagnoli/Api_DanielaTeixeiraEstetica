using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using locacaoDeImoveis.DTO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace locacaoDeImoveis.Controllers
{
  public class Repository
  {

    #region Variaveis
    static string connString = "Server=localhost;Database=locacaoDeImoveis;Uid=root;Pwd=PERNAlonGA2#";
    private MySqlConnection mConn;
    private MySqlDataAdapter mAdapter;
    private DataSet mDataSet;
    #endregion

    public List<imoveisDTO> QuerySelectIAllmoveis()
    {
      var retorno = ConnectMySqlServerSelect(query: "SELECT * FROM IMOVEIS", table: "IMOVEIS");

      string imoveisJson = JsonConvert.SerializeObject(retorno);

      var imoveisList = JsonConvert.DeserializeObject<List<imoveisDTO>>(imoveisJson);

      return imoveisList;
    }

    public imoveisDTO QuerySelectIOneImovel(int id)
    {
      var retorno = ConnectMySqlServerSelect(query: @$"SELECT * FROM IMOVEIS 
                                                                WHERE imovel_id = {id}", table: "IMOVEIS");

      string imoveisJson = JsonConvert.SerializeObject(retorno);

      var imoveisList = JsonConvert.DeserializeObject<List<imoveisDTO>>(imoveisJson).FirstOrDefault();

      return imoveisList;
    }

    public int QueryDeleteOneImovel(int id)
    {
      var retorno = ConnectMySqlExecuteQuery(query: @$"DELETE FROM IMOVEIS 
                                                                WHERE imovel_id = {id}");

      return retorno;
    }

    public int QueryInsertImovel(imoveisDTO imovel)
    {
      var listDeImoveis = QuerySelectIAllmoveis();
      imovel.imovel_id = listDeImoveis.Select(x => x.imovel_id).Max() + 1;
      if (imovel.imovel_id < 1 || imovel.imovel_id == null)
      {
        imovel.imovel_id = 1;
      }

      var retorno = ConnectMySqlExecuteQuery(query: $@"INSERT INTO IMOVEIS 
                                                                   (
                                                                   imovel_id,
                                                                   titulo_imovel,
                                                                   descricao_imovel,
                                                                   endereco_imovel,
                                                                   valor_imovel,
                                                                   cep_imovel,
                                                                   status,
                                                                   priority
                                                                   )
                                                                   VALUES 
                                                                   (
                                                                   {imovel.imovel_id},
                                                                   '{imovel.titulo_imovel}',
                                                                   '{imovel.descricao_imovel}',
                                                                   '{imovel.endereco_imovel}',
                                                                   '{imovel.valor_imovel}',
                                                                   '{imovel.cep_imovel}',
                                                                   1,
                                                                   2
                                                                   );
                                                                   ");

      return retorno;
    }

    public int QueryUpdatetImovel(imoveisDTO imovel)
    {
      var date = System.DateTime.Now.Date.ToString().Split(' ');
      var retorno = ConnectMySqlExecuteQuery(query: $@"UPDATE IMOVEIS SET
                                                                   titulo_imovel = '{imovel.titulo_imovel}',
                                                                   descricao_imovel = '{imovel.descricao_imovel}',
                                                                   endereco_imovel = '{imovel.endereco_imovel}',
                                                                   valor_imovel = '{imovel.valor_imovel}',
                                                                   cep_imovel = '{imovel.cep_imovel}',
                                                                   data_de_edicao = STR_TO_DATE('{date[0].ToString().Replace('/', ' ')}', '%d %m %Y')
                                                                   WHERE imovel_id = {imovel.imovel_id}
                                                                   ");

      return retorno;
    }

    private DataTable ConnectMySqlServerSelect(string query, string table)
    {
      mDataSet = new DataSet();
      mConn = new MySqlConnection(connString);
      mConn.Open();

      if (mConn.State == ConnectionState.Open)
      {

        mAdapter = new MySqlDataAdapter(query, mConn);
        mAdapter.Fill(mDataSet, table);

        return mDataSet.Tables[table];
      }
      return null;
    }

    private int ConnectMySqlExecuteQuery(string query)
    {
      mDataSet = new DataSet();
      mConn = new MySqlConnection(connString);
      mConn.Open();

      var comm = mConn.CreateCommand();
      comm.CommandText = query;
      var result = comm.ExecuteNonQuery();
      
      mConn.Close();

      return result;
    }
  }
}
