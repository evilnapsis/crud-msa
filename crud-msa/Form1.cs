using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace crud_msa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("Id","Id");
            dataGridView1.Columns.Add("Nombre","Nombre");
            dataGridView1.Columns.Add("Apellido", "Apellido");
            dataGridView1.Columns.Add("Direccion", "Direccion");
            dataGridView1.Columns.Add("Email", "Email");

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[3].Width = 150;

            ContactForm.data = dataGridView1;

            load();
        }

        public static void load()
        {
            ContactForm.data.Rows.Clear();
            OleDbDataReader r = MSAConnection.read("select * from contact");
            while (r.Read())
            {
                ContactForm.data.Rows.Add(r.GetInt32(0), r.GetString(1), r.GetString(2), r.GetString(3), r.GetString(4));
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContactForm.action = "add";
            ContactForm c = new ContactForm();
            c.ShowDialog();
            load();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                ContactForm.id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                ContactForm.action = "update";
                ContactForm c = new ContactForm();
                c.ShowDialog();
                load();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Invalido!!");

            }
        }

        private void recargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoWindow iw = new InfoWindow();
            iw.ShowDialog();
        }
    }
}
