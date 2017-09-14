using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using StackExchange.Redis;
using System.Diagnostics;

using System.Net;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

//using StackExchange.Redis;
//using ServiceStack.Redis;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//

//
namespace ПАПС
{

    public partial class Form1 : Form
    {
     
        public int time = 0,id = 0, id_server=0;
        public string login, password;
        ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect("127.0.0.1");
            

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

        public Form1()
        {
            InitializeComponent();
        }
        public void Show_info()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            login = textBox1.Text.ToString();
            password = textBox2.Text.ToString();
            cmd.Connection = cnn;
            cmd.CommandText = "select * from logpass where login=\'" + login + "\' and pass=\'" + password + "\';";
            cnn.Open();
            SqlDataReader readId = cmd.ExecuteReader();
            while (readId.Read())
            {
                if (login == readId[0].ToString())
                {
                    id = Convert.ToInt32(readId[2]);
                    id_server = Convert.ToInt32(readId[3]);
                    textBox3.Text = Convert.ToString(readId[0]);
                    //MessageBox.Show(login);
                }
            }
            if (id == 0)
            {
                MessageBox.Show("Wrong information", "Error!");

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

                
                textBox4.Text += info[0];
                textBox5.Text += info[1];
                textBox6.Text += info[2];
                textBox7.Text += info[3];
                richTextBox2.Text += info[4];
                

            }
            else {
                
                cmdu.Connection = CurrCNN;
                cmdu.CommandText = "select * from Users where id=\'" + id + "\';";
                SqlDataReader readId1 = cmdu.ExecuteReader();
                while (readId1.Read())
                {
                    id = Convert.ToInt32(readId1[0]);
                    textBox4.Text += readId1[1];
                    textBox5.Text += readId1[2];
                    textBox6.Text += readId1[3];
                    textBox7.Text += readId1[4];
                    richTextBox2.Text += readId1[5] + "\n";
                }

                //IDatabase SetDb = redisConn.SetDatabase();
                readDb.StringSet("user_id/" + id.ToString(), textBox4.Text + '|' + textBox5.Text + '|'
                                    + textBox6.Text + '|' + textBox7.Text + '|' + richTextBox2.Text);
                readId1.Close();

            }

            stopwatch.Stop();
            label4.Text = Convert.ToString(stopwatch.Elapsed);
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
        private void button1_Click(object sender, EventArgs e)
        {
   
            Show_info();
       
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Form2 f2 = new Form2();
           // f2.Show();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            listBox1.Items.Clear();
            label4.Text = "";
            time = 0;
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
