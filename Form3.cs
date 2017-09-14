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

using StackExchange.Redis;
using System.Diagnostics;



namespace ПАПС
{
    public partial class Form3 : Form
    {
        public int time = 0, id = 0, id_server=0;
        public string login, password;

        ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect("localhost");

        SqlConnection cnn = new SqlConnection(@"Data Source=KEY;Initial Catalog=enter;Integrated Security=True");
        SqlConnection cnn1 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_1base;Integrated Security=True");
        SqlConnection cnn2 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_2base;Integrated Security=True");
        SqlConnection cnn3 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_3base;Integrated Security=True");
        SqlConnection cnn4 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_4base;Integrated Security=True");
        SqlConnection cnn5 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_5base;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlCommand cmdu = new SqlCommand();
        SqlCommand cmdf = new SqlCommand();
        SqlConnection CurrCNN;


        public Form3(int a)
        {
            InitializeComponent();
            id = a;
        }
        public void Show_info()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            cmd.Connection = cnn;
            cmd.CommandText = "select login, base from logpass where id_user=\'" + id + "\';";
            cnn.Open();
            SqlDataReader readId = cmd.ExecuteReader();
            while (readId.Read())
            {
                textBox1.Text += readId[0];
                id_server = Convert.ToInt32(readId[1]);
            }
            cnn1.Open();
            cnn2.Open();
            cnn3.Open();
            cnn4.Open();
            cnn5.Open();
            switch (id_server)
            {
                case 1:
                    CurrCNN = cnn1;
                    break;
                case 2:
                    CurrCNN = cnn2;
                    break;
                case 3:
                    CurrCNN = cnn3;
                    break;
                case 4:
                    CurrCNN = cnn4;
                    break;
                case 5:
                    CurrCNN = cnn5;
                    break;

            }

            IDatabase readDb = redisConn.GetDatabase();

            if (readDb.KeyExists("user_id/" + id.ToString()))//пляшем
            {
                char Razdel = '|';
                string text = readDb.StringGet("user_id/" + id.ToString());

                string[] info = text.Split(Razdel);

                
                textBox2.Text += info[0];
                textBox3.Text += info[1];
                textBox4.Text += info[2];
                textBox5.Text += info[3];
                richTextBox2.Text += info[4];


            }
            else {

                cmdu.Connection = CurrCNN;
                cmdu.CommandText = "select * from Users where id=\'" + id + "\';";
                SqlDataReader readId1 = cmdu.ExecuteReader();
                while (readId1.Read())
                {
                    id = Convert.ToInt32(readId1[0]);
                    textBox2.Text += readId1[1];
                    textBox3.Text += readId1[2];
                    textBox4.Text += readId1[3];
                    textBox5.Text += readId1[4];
                    richTextBox2.Text += readId1[5] + "\n";
                }

                //IDatabase SetDb = redisConn.SetDatabase();
                readDb.StringSet("user_id/" + id.ToString(),  textBox2.Text + '|' + textBox3.Text + '|'
                                    + textBox4.Text + '|' + textBox5.Text + '|' + richTextBox2.Text);
                readId1.Close();

            }
            stopwatch.Stop();
            label9.Text = Convert.ToString(stopwatch.Elapsed);

            //1 baza

            cmdf.Connection = cnn1;
            cmdf.CommandText = "select id_friends from Friends where id_users=\'" + id + "\'";
            SqlDataReader readId21 = cmdf.ExecuteReader();
            while (readId21.Read())
            {
                string s = Convert.ToString(readId21[0]);
                listBox1.Items.Add(s);

            }
            readId21.Close();

            //2 baza

            cmdf.Connection = cnn2;
            cmdf.CommandText = "select id_users from Friends where id_friends =\'" + id + "\'";
            SqlDataReader readId22 = cmdf.ExecuteReader();
            while (readId22.Read())
            {
                string s = Convert.ToString(readId22[0]);
                listBox1.Items.Add(s);
            }
            readId22.Close();


            //3 baza

            cmdf.Connection = cnn3;
            cmdf.CommandText = "select id_users from Friends where id_friends =\'" + id + "\'";
            SqlDataReader readId23 = cmdf.ExecuteReader();
            while (readId23.Read())
            {
                string s = Convert.ToString(readId23[0]);
                listBox1.Items.Add(s);
            }
            readId23.Close();


            //4 baza

            cmdf.Connection = cnn4;
            cmdf.CommandText = "select id_users from Friends where id_friends =\'" + id + "\'";
            SqlDataReader readId24 = cmdf.ExecuteReader();
            while (readId24.Read())
            {
                string s = Convert.ToString(readId24[0]);
                listBox1.Items.Add(s);
            }
            readId24.Close();


            //5 baza


            cmdf.Connection = cnn5;
            cmdf.CommandText = "select id_users from Friends where id_friends =\'" + id + "\'";
            SqlDataReader readId25 = cmdf.ExecuteReader();
            while (readId25.Read())
            {
                string s = Convert.ToString(readId25[0]);
                listBox1.Items.Add(s);
            }
            readId25.Close();

            cnn1.Close();
            cnn2.Close();
            cnn3.Close();
            cnn4.Close();
            cnn5.Close();

            cnn.Close();
            readId.Close();
            

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Show_info();          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                id = Convert.ToInt32(listBox1.SelectedItem);
                Form3 f = new Form3(id);
                f.Show();
            }
        }
    }
}
