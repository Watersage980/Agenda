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
        }

        private void btnInserir_Click(object sender, EventArgs e)
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
            mostrar();
        }
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
    }
}
