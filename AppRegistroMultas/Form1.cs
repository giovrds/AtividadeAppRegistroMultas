using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRegistroMultas.Formulario;

namespace AppRegistroMultas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btCadastroVeiculo_Click(object sender, EventArgs e)
        {
            FormCadastro form = new FormCadastro(); 
            form.ShowDialog();
        }

        private void btCadastroMulta_Click(object sender, EventArgs e)
        {
            FormCadastroMulta form = new FormCadastroMulta();
            form.ShowDialog();
        }

        private void btConsulta_Click(object sender, EventArgs e)
        {
            FormConsultaVeiculo form = new FormConsultaVeiculo();
               form.ShowDialog();
        }

        private void btAtualizar_Click(object sender, EventArgs e)
        {
            FormAtualizarVeiculo form = new FormAtualizarVeiculo();
            form.ShowDialog();
        }

        private void btAtualizarMulta_Click(object sender, EventArgs e)
        {
            FormAtualizarMulta form = new FormAtualizarMulta();
            form.ShowDialog();
        }

        private void btDeletarMulta_Click(object sender, EventArgs e)
        {
            FormDeletarMulta form = new FormDeletarMulta();
            form.ShowDialog();
        }
    }
}
