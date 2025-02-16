using AppRegistroMultas.Contexto;
using AppRegistroMultas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRegistroMultas.Formulario
{
    public partial class FormDeletarMulta : Form
    {
        private List<Veiculo> listaVeiculos = new List<Veiculo>();
        private List<Multa> listaMultas = new List<Multa>();

        public FormDeletarMulta()
        {
            InitializeComponent();
            //listaVeiculos = Context.ListaVeiculos.ToList();
            MultaContext context = new MultaContext();
            listaMultas = context.ListarMultas();

            cbVeiculo.DataSource = listaMultas.ToList();
            cbVeiculo.DisplayMember = "Descricao";
            cbVeiculo.ValueMember = "Id";
            cbVeiculo.SelectedIndex = -1; //combobox em branco
        }

        private void btDeletar_Click(object sender, EventArgs e)
        {
            if (cbVeiculo.SelectedIndex > -1)
            {
                int idMultaSelecionada = (int)cbVeiculo.SelectedValue;

                var multaSelecionada = listaMultas.FirstOrDefault(m => m.Id == idMultaSelecionada);
                multaSelecionada.ValorMulta = Convert.ToDecimal(txtValor.Text);
                multaSelecionada.Descricao = txtDescricao.Text;

                MultaContext context = new MultaContext();
                context.DeletarMulta(multaSelecionada);

                MessageBox.Show($"ID:{multaSelecionada.Id} ATUALIZADO COM SUCESSO!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMarca.Clear();
                txtModelo.Clear();
                txtPlaca.Clear();
                txtDescricao.Clear();
                txtValor.Clear();
            }
        }

        private void cbVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVeiculo.SelectedIndex > -1)
            {
                var multaSelecionada = listaMultas[cbVeiculo.SelectedIndex];

                txtDescricao.Text = multaSelecionada.Descricao;
                txtValor.Text = multaSelecionada.ValorMulta.ToString("N2");

                VeiculoContext veiculoContext = new VeiculoContext();
                var veiculo = veiculoContext.ListarVeiculos().FirstOrDefault(v => v.Id == multaSelecionada.VeiculoId);

                if (veiculo != null)
                {
                    txtModelo.Text = veiculo.Modelo;
                    txtMarca.Text = veiculo.Marca;
                    txtPlaca.Text = veiculo.Placa;
                }
                else
                {
                    txtModelo.Clear();
                    txtMarca.Clear();
                    txtPlaca.Clear();
                }
            }
            else
            {
                txtDescricao.Clear();
                txtValor.Clear();
                txtModelo.Clear();
                txtMarca.Clear();
                txtPlaca.Clear();
            }
        }
    }
}