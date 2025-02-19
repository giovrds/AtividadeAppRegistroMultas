using AppRegistroMultas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRegistroMultas.Contexto
{
    public class MultaContext
    {
        private string dados_conexao;
        private MySqlConnection conexao = null;

        public MultaContext()
        {
            dados_conexao = "server=localhost;port = 3306;database=bd_registro_multa;user=root;password=34241610@Gi;Persist Security Info = False;Connect Timeout=300;";
            conexao = new MySqlConnection(dados_conexao);
        }//fim do método construtor

        public List<Multa> ListarMultas()
        {
            List<Multa> listaMultasParaExportar = new List<Multa>();
            string sql = "SELECT * FROM MULTA"; 
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                conexao.Open();
                MySqlDataReader dados = comando.ExecuteReader();

                while (dados.Read())
                {
                    Multa multa = new Multa();
                    multa.Id = Convert.ToInt32(dados["Id"]);
                    multa.Descricao = dados["Descricao"].ToString();
                    multa.ValorMulta = decimal.Parse(dados["ValorMulta"].ToString());
                    multa.VeiculoId = int.Parse(dados["VeiculoId"].ToString());
                    listaMultasParaExportar.Add(multa);
                }
                conexao.Close();       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            return listaMultasParaExportar;
        }//fim do método para consultar e listar Multas

        public void InserirMulta(Multa multa)
        {
            string sql = "INSERT INTO MULTA (Descricao, ValorMulta, VeiculoId) VALUES (@Descricao,@ValorMulta, @VeiculoId)";

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@Descricao", multa.Descricao);
                comando.Parameters.AddWithValue("@ValorMulta", multa.ValorMulta);
                comando.Parameters.AddWithValue("@VeiculoId", multa.VeiculoId);

                conexao.Open(); 
                int LinhasAfestadas = comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir multa: " + ex.Message);
            }

            finally
            {
                conexao.Close();
            }
        }//fim do método para inserir Multa

        public void AtualizarMulta(Multa multa)
        {      
            string sql = "UPDATE MULTA SET Descricao = @Descricao, ValorMulta = @ValorMulta, VeiculoId = @VeiculoId WHERE Id = @Id";

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@Descricao", multa.Descricao);
                comando.Parameters.AddWithValue("@ValorMulta", multa.ValorMulta);
                comando.Parameters.AddWithValue("@VeiculoId", multa.VeiculoId);
                comando.Parameters.AddWithValue("@Id", multa.Id);

                conexao.Open();
                int LinhasAfestadas = comando.ExecuteNonQuery();

                if (LinhasAfestadas > 0)
                {
                    MessageBox.Show("Multa atualizada com sucesso!");
                }

                else
                {
                    MessageBox.Show("Nenhum registro foi atualizado. Verifique o ID informado.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar multa: " + ex.Message);
            }

            finally
            {
                conexao.Close(); 
            }
        } // fim do Atualizar Multa

        public void DeletarMulta(Multa multa)
        {
            string sql = "DELETE FROM MULTA WHERE Id = @Id";

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@Descricao", multa.Descricao);
                comando.Parameters.AddWithValue("@ValorMulta", multa.ValorMulta);
                comando.Parameters.AddWithValue("@VeiculoId", multa.VeiculoId);
                comando.Parameters.AddWithValue("@Id", multa.Id);
                conexao.Open();
                int linhasAfetadas = comando.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Multa deletada com sucesso!");
                }
                else
                {
                    MessageBox.Show("Nenhum registro foi deletado. Verifique o ID informado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar multa: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        } //fim do Deletar Multa

    }//fim da classe MultaContext

}//fim do namespace
