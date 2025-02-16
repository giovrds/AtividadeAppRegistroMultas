﻿using AppRegistroMultas.Contexto;
using AppRegistroMultas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AppRegistroMultas.Formulario
{
    public partial class FormAtualizarMulta : Form
    {
        private List<Veiculo> listaVeiculos = new List<Veiculo>();
        private List<Multa> listaMultas = new List<Multa>();

        public FormAtualizarMulta()
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

        private void btAtualizar_Click(object sender, EventArgs e)
        {
            if (cbVeiculo.SelectedIndex > -1)
            {
                int idMultaSelecionada = (int)cbVeiculo.SelectedValue;
                var multaSelecionada = listaMultas.FirstOrDefault(m => m.Id == idMultaSelecionada);
                multaSelecionada.ValorMulta = Convert.ToDecimal(txtValor.Text);
                multaSelecionada.Descricao = txtDescricao.Text;

                MultaContext context = new MultaContext();
                context.AtualizarMulta(multaSelecionada);

                MessageBox.Show($"ID:{multaSelecionada.Id} ATUALIZADO COM SUCESSO!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cbVeiculo.SelectedIndex = -1;
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

                CarregarVeiculos(multaSelecionada.VeiculoId);
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
 

        private void CarregarVeiculos(int veiculoId)
        {
            VeiculoContext veiculoContext = new VeiculoContext();
            var veiculo = veiculoContext.ListarVeiculos().FirstOrDefault(v => v.Id == veiculoId);

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
    }
}

