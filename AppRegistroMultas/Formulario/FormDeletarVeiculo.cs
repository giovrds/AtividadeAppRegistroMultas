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
    public partial class FormDeletarVeiculo : Form
    {
        int contExc = 0;
        List<Veiculo> ListaVeiculos = new List<Veiculo>();

        public FormDeletarVeiculo()
        {
            InitializeComponent();

            VeiculoContext veiculo = new VeiculoContext();
            ListaVeiculos = veiculo.ListarVeiculos();

            comboBox1.DataSource = ListaVeiculos.ToList();
            comboBox1.DisplayMember = "Placa";
            comboBox1.SelectedIndex = -1;
        }

        private void btDeletar_Click(object sender, EventArgs e)
        {
            var linhaSelec = comboBox1.SelectedIndex;

            if (linhaSelec > -1 && contExc > 0)
            {
                var veiculosSelec = ListaVeiculos[linhaSelec];
                veiculosSelec.Marca = txtMarca.Text;
                veiculosSelec.Modelo = txtModelo.Text;
                veiculosSelec.Placa = txtPlaca.Text;

                VeiculoContext context = new VeiculoContext();
                context.DeletarVeiculo(veiculosSelec);
                MessageBox.Show($"ID:{(veiculosSelec.Id).ToString()} " + "ATUALIZADO COM SUCESSO!", "2ºA INF", MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBox1.SelectedIndex = -1;
                txtMarca.Clear();
                txtModelo.Clear();
                txtPlaca.Clear();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var linhaSelec = comboBox1.SelectedIndex;

            if (linhaSelec > -1 && contExc > 0)
            {
                var veiculosSelec = ListaVeiculos[linhaSelec];
                txtMarca.Text = veiculosSelec.Marca;
                txtModelo.Text = veiculosSelec.Modelo;
                txtPlaca.Text = veiculosSelec.Placa;
            }
            contExc++;
        }
    }
}
