using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Reflection;
using AppRegistroMultas;
using AppRegistroMultas.Models;

namespace AppRegistroMultas.Contexto
{
    public class VeiculoContext
    {
        private string dados_conexao;
        private MySqlConnection conexao = null;

        public VeiculoContext()
        {
            dados_conexao = "server=localhost;port = 3306;database=bd_registro_multa;user=root;password=34241610@Gi;Persist Security Info = False;Connect Timeout=300;";
            conexao = new MySqlConnection(dados_conexao);
        }//fim do método construtor

        public List<Veiculo> ListarVeiculos()
        {
            List<Veiculo> listaVeiculosParaExportar = new List<Veiculo>();
            string sql = "SELECT * FROM VEICULO";
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                conexao.Open();
                MySqlDataReader dados = comando.ExecuteReader(); 

                while (dados.Read())
                {
                    Veiculo veiculo = new Veiculo();
                    veiculo.Id = Convert.ToInt32(dados["Id"]);
                    veiculo.Marca = dados["Marca"].ToString();
                    veiculo.Modelo = dados["Modelo"].ToString();
                    veiculo.Placa = dados["Placa"].ToString();
                    listaVeiculosParaExportar.Add(veiculo);
                }
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            return listaVeiculosParaExportar;
        }//fim do método para consultar e listar Veículos

        public void InserirVeiculo(Veiculo veiculo)
        {
            string sql = "INSERT INTO VEICULO (Placa, Modelo, Marca) VALUES (@Placa,@Modelo, @Marca)"; 

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@Placa", veiculo.Placa);
                comando.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                comando.Parameters.AddWithValue("@Marca", veiculo.Marca);

                conexao.Open();
                int LinhasAfestadas = comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir veículo: " + ex.Message);
            }

            finally
            {
                conexao.Close();
            }
        }//fim do método para inserir veículos

        public void AtualizarVeiculo(Veiculo veiculo)
        {
            string sql = "UPDATE VEICULO SET Placa = @Placa, Modelo = @Modelo, Marca = @Marca WHERE Id = @Id";

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@Placa", veiculo.Placa);
                comando.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                comando.Parameters.AddWithValue("@Marca", veiculo.Marca);
                comando.Parameters.AddWithValue("@Id", veiculo.Id);

                conexao.Open();
                int LinhasAfestadas = comando.ExecuteNonQuery();

                if (LinhasAfestadas > 0)
                {
                    MessageBox.Show("Veículo atualizada com sucesso!");
                }

                else
                {
                    MessageBox.Show("Nenhum registro foi atualizado. Verifique o ID informado.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar veículo: " + ex.Message);
            }

            finally
            {
                conexao.Close();
            }
        } // fim do Atualizar veiculo

        public void DeletarVeiculo(Veiculo veiculo)
        {
            string sql = "DELETE FROM VEICULO WHERE Id = @Id";

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@Placa", veiculo.Placa);
                comando.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                comando.Parameters.AddWithValue("@Marca", veiculo.Marca);
                comando.Parameters.AddWithValue("@Id", veiculo.Id);

                conexao.Open();
                int LinhasAfestadas = comando.ExecuteNonQuery();

                if (LinhasAfestadas > 0)
                {
                    MessageBox.Show("Veículo atualizada com sucesso!");
                }
                else
                {
                    MessageBox.Show("Nenhum registro foi atualizado. Verifique o ID informado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar veículo: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        } // fim do deletar veiculo

    }//fim da classe VeiculoContext

}//fim do namespace
