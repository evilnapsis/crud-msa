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
    public partial class ContactForm : Form
    {
        public static string action = "";
        public static string id;
        public string na, la, ad, ph, em;
        public static DataGridView data;

        public ContactForm()
        {
            InitializeComponent();

          

            if (action == "update") {
                OleDbDataReader r = MSAConnection.read("select * from contact where id=" + id);
                while (r.Read())
                {
                    firstname.Text = r.GetString(1);
                    lastname.Text = r.GetString(2);
                    address.Text = r.GetString(3);
                    email.Text = r.GetString(5);
                    phone.Text = r.GetString(4);
                    break;
                }

            }
            else if (action == "add") {
                delete.Enabled = false;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (firstname.Text != "")
            {
                if (action == "add")
                {
                    MSAConnection.execute("insert into contact (firstname,lastname,address,email,phone) values (\"" + firstname.Text + "\",\"" + lastname.Text + "\",\"" + address.Text + "\",\"" + email.Text + "\",\"" + phone.Text + "\")");
                    firstname.Text = lastname.Text = address.Text = phone.Text = email.Text = "";
                    MessageBox.Show("Agregado Exitosamente!!");
                }
                else if (action == "update") {
                    MSAConnection.execute("update contact set firstname=\"" + firstname.Text + "\",lastname=\"" + lastname.Text + "\",address=\"" + address.Text + "\",email=\"" + email.Text + "\",phone=\"" + phone.Text + "\" where id="+id);
                    //firstname.Text = lastname.Text = address.Text = phone.Text = email.Text = "";
                    MessageBox.Show("Actualizado Exitosamente!!");
                
                }

            }
            else { MessageBox.Show("Campos Obligatorio: Nombre"); }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            MSAConnection.execute("delete from contact where id=" + id);
            MessageBox.Show("Eliminado Exitosamente!!");
            Form1.load();
            Dispose();
        }
    }
}
