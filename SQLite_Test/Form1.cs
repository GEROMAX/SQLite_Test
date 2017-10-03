using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLite_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=sample.db"))
                {
                    con.Open();
                    using (SQLiteCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "CREATE TABLE Sample (ID INTEGER PRIMARY KEY, Name NVARCHAR(128), Age INTEGER)";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Sample (Name, Age) VALUES('一郎', 10)";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Sample (Name, Age) VALUES('二郎', 20)";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Sample (Name, Age) VALUES('三郎', 30)";
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Sampleデータベース作成成功", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sampleデータベース作成失敗" + Environment.NewLine + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (SQLiteConnection con = new SQLiteConnection("Data Source=sample.db"))
            {
                DataSet ds = new DataSet();
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM Sample", con);
                da.Fill(ds, "Sample");

                this.dataGridView1.DataSource = ds.Tables["Sample"];
            }
        }
    }
}
