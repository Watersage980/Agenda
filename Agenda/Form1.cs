using MySql.Data.MySqlClient;
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
            btnDeletar.Visible = false;
            btnUpdate.Visible = false;
        }
        string continua = "yes";

        private void btnInserir_Click(object sender, EventArgs e)
        {
            verificarvazio();
            if (continua == "yes" && btnInserir.Text=="INSERIR")
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
            else if (btnInserir.Text=="NOVO")
            {
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
                btnInserir.Text = "NOVO";
                btnDeletar.Visible = true;
                btnUpdate.Visible = true;
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
                        string sql = "Delete from contatos where idContatos = '" + txtID.Text + "'";
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente atualizar", "Confirmação", MessageBoxButtons.YesNo))
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=agenda;uid=root;pwd=;port=3306";
                        cnn.Open();
                        string sql = "Update contatos set nome='" + txtNome.Text + "', email='" + txtEmail.Text + "' where idContatos='" + txtID.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Atualizado com sucesso!");
                    }

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
            txtPesquisar.Clear();
            btnInserir.Text = "INSERIR";
            btnDeletar.Visible = false;
            btnUpdate.Visible = false;
        }
        void verificarvazio()
        {
            if(txtPesquisar.Text !="")
            {
                continua = "no";
            }
            else if (txtEmail.Text == "" || txtNome.Text == "")
            {
                continua = "no";
                MessageBox.Show("Insira todos os dados parça");               
            }
            else
            {
                continua = "yes";
            }
        }

       

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=agenda;uid=root;pwd=;port=3306";
                    cnn.Open();
                    string sql;

                    if (rbEmail.Checked)
                    {
                        sql = "Select * from contatos where email Like'" + txtPesquisar.Text + "%'";
                    }
                    else
                    {
                        sql = "Select * from contatos where nome Like'" + txtPesquisar.Text + "%'";
                    }

                    MySqlCommand cmd = new MySqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
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

           if (txtPesquisar != null)
            {
                btnInserir.Text = "NOVO";
            }
        }
    }
}
