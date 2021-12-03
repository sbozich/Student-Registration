using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace studentreg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FLoad();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-N5IF2SJ\\SQLEXPRESS; Initial Catalog=gcbt;Integrated Security=SSPI;");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool Mode = true;
        string sql;

        public void FLoad()
        {
            try
            {
                sql = "select * from student";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while(read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getID(String id)
        {
            sql = "select * from student where id= '"+ id + "'     ";
            
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while(read.Read())
            {
                txtIme.Text = read[1].ToString();
                txtPredmet.Text = read[2].ToString();
                txtNaknada.Text = read[3].ToString();



            }
            con.Close();
        }


        //if the mode is true means add record otherwise update

        private void button1_Click(object sender, EventArgs e)
        {
            string ime = txtIme.Text;
            string predmet = txtPredmet.Text;
            string naknada = txtNaknada.Text;

            if (Mode == true)
            {
                sql = "insert into student(stime, predmet, naknada) values(@stime, @predmet, @naknada)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stime", ime);
                cmd.Parameters.AddWithValue("@predmet", predmet);
                cmd.Parameters.AddWithValue("@naknada", naknada);
                MessageBox.Show("Entry successful");
                cmd.ExecuteNonQuery();

                txtIme.Clear();
                txtPredmet.Clear();
                txtNaknada.Clear();
                txtIme.Focus();

            }

            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update student set stime=@stime, predmet=@predmet, naknada=@naknada where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stime", ime);
                cmd.Parameters.AddWithValue("@predmet", predmet);
                cmd.Parameters.AddWithValue("@naknada", naknada);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record updated");
                cmd.ExecuteNonQuery();

                txtIme.Clear();
                txtPredmet.Clear();
                txtNaknada.Clear();
                txtIme.Focus();
                button1.Text = "Save";
                Mode = true;


            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==dataGridView1.Columns["Edit"].Index && e.RowIndex >=0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from student where id =@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtIme.Clear();
            txtPredmet.Clear();
            txtNaknada.Clear();
            txtIme.Focus();
            button1.Text = "Save";
            Mode = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            data d = new data();
            d.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
