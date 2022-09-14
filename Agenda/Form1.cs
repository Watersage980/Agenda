﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mostrar();
        }
        string continua = "yes";

        private void btnInserir_Click(object sender, EventArgs e)
        {
            verificarvazio();
            if (continua == "yes")
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=agenda;uid=root;pwd=;port=3306";
                        cnn.Open();
                        MessageBox.Show("Inserido com sucesso!");
                        string sql = "insert into contatos (nome, email) values ('" + txtNome.Text + "', '" + txtEmail.Text + "')";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                limpar();
            }        
            mostrar();
        }

        private void dgwTabela_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwTabela.CurrentRow.Index != -1)
            {
                txtID.Text = dgwTabela.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgwTabela.CurrentRow.Cells[1].Value.ToString();
                txtEmail.Text = dgwTabela.CurrentRow.Cells[2].Value.ToString();
                //btnInserir.Text = "NOVO";
            }
        }
       
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente excluir", "Confirmação", MessageBoxButtons.YesNo))
            {

                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=agenda;uid=root;pwd=;port=3306";
                        cnn.Open();
                        string sql = "Delete from contatos where idContato = '" + txtID.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" Deletado com sucesso! ");

                    }
                    limpar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
            mostrar();

        }
        //métodos
        void mostrar()
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=agenda;uid=root;pwd=;port=3306";
                    cnn.Open();
                    string sql = "Select * from contatos";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adpter = new MySqlDataAdapter(sql, cnn);
                    adpter.Fill(table);
                    dgwTabela.DataSource = table;

                    dgwTabela.AutoGenerateColumns = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        void limpar()
        {
            txtID.Clear();
            txtEmail.Clear();
            txtNome.Clear();
        }
        void verificarvazio()
        {
            if (txtEmail.Text != "" && txtNome.Text != "")
            {
                continua = "yes";
            }
            else
            {
                continua = "no";
                MessageBox.Show("Insira todos os dados parça");
            }
        }

    }
}
