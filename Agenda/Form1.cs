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
                //acessa o banco para obter os dados e adicionar os novos ao inserir o comando de insert utilizando uma string
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
                //limpa os campos dépois de executar com sucesso
                limpar();
            }   
            else if (btnInserir.Text=="NOVO")
            {
                //limpa tudo para inserir novas informações
                limpar();
            }
            mostrar();
        }

        private void dgwTabela_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //permite selecionar dados e habilita os botões de deletar e atualizar
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
            //confirma se o usuário realmente deseja excluir os dados
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente excluir", "Confirmação", MessageBoxButtons.YesNo))
            {
                //deleta os ddos selecionados utilizando o comando delete através de uma string
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
            //confirma se deseja atualizar os dados
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente atualizar", "Confirmação", MessageBoxButtons.YesNo))
            {
                //atualiza os dados utilizando o comando update através de uma string
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
            //permite que a tabela acesse o banco para mostrar todos os dados presentes
            try
            {
                //mostra todos os dados através do comando select * utilizando uma string
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
            //limpa todas as text box, reseta os botões de deletar e atualizar para seu estado oculto, e restaura o texto do botão inserir
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
            //verifica se o campo de pesquisa está preenchido e/ou se os campos de nome e mail estão incompletos
            if (txtPesquisar.Text !="")
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
            //permite procurar dados específicos utilizando a primeira lera do nome ou email
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
