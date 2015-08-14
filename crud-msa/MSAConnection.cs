using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
namespace crud_msa
{
    class MSAConnection
    {
        public static OleDbConnection conexion = null;
        public static OleDbConnection getCon()
        {
            if (conexion == null)
            {
                try
                {
                    OleDbConnectionStringBuilder b = new OleDbConnectionStringBuilder();
                    b.Provider = "Microsoft.ACE.OLEDB.12.0";
                    b.DataSource = "contacts.accdb";
                    conexion = new OleDbConnection(b.ToString());
                    conexion.Open();
                }
                catch (OleDbException e)
                {
                    MessageBox.Show("Error de la base de datos: " + e.Message);
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        System.Environment.Exit(1);
                    }
                }
            }
            return conexion;
        }

        public static void execute(String sql) {
            OleDbCommand cmd = new OleDbCommand(sql, getCon());
            cmd.ExecuteNonQuery();
        }

        public static OleDbDataReader read(String sql) {
            OleDbCommand cmd = new OleDbCommand(sql, getCon());
            return cmd.ExecuteReader();
        }
    }
}
