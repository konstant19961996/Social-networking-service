using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SphinxConnector.NativeApi;
using SphinxConnector.FluentApi;
using System.Data.SqlClient;

namespace ПАПС
{
    public partial class Form4 : Form
    {
        SphinxClient sphinxClient = new SphinxClient("127.0.0.1", 9312);

        SqlConnection cnn1 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_1base;Integrated Security=True");
        SqlConnection cnn2 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_2base;Integrated Security=True");
        SqlConnection cnn3 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_3base;Integrated Security=True");
        SqlConnection cnn4 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_4base;Integrated Security=True");
        SqlConnection cnn5 = new SqlConnection(@"Data Source=KEY;Initial Catalog=_5base;Integrated Security=True");
        SqlConnection _CurrentCNN;

        SqlCommand cmd = new SqlCommand();
        int[] _ArrPeopleId = new int [50];
        string a;
   
        
    public Form4()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
           
        }

            
            
        

        private void Form4_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                 //Установим параметры поиска по возрасту

              /*  case 0:
                sphinxClient.SearchOptions.SetFilterRange<SphinxInteger>("Age", 18, 25);
                break;

                case 1:  
                sphinxClient.SearchOptions.SetFilterRange<SphinxInteger>("Age", 25, 40);
                break;

                case 2:
                sphinxClient.SearchOptions.SetFilterRange<SphinxInteger>("Age", 40, 60);
                break;

                case 3:
                sphinxClient.SearchOptions.SetFilterRange<SphinxInteger>("Age", 60, 80);
                break;*/

                case 0:
                    {
                        int a, b;
                        a = Convert.ToInt32(textBox5.Text);
                        b = Convert.ToInt32(textBox6.Text);
                        sphinxClient.SearchOptions.SetFilterRange<SphinxInteger>("Age", a, b);
                    }
                    break;

                case 1:
                    {
                        int b;
                        b = Convert.ToInt32(textBox6.Text);
                        sphinxClient.SearchOptions.SetFilterRange<SphinxInteger>("Age", b, 80);
                    }
                    break;

                case 2:
                    {
                        int b;
                        b = Convert.ToInt32(textBox6.Text);
                        sphinxClient.SearchOptions.SetFilterRange<SphinxInteger>("Age", 18, b);
                    }
                    break;

            }

            string Search; int i = 0;
            listBox1.Items.Clear();
            Search = textBox1.Text + ' ' + textBox2.Text + ' ' + textBox3.Text + ' ' + textBox4.Text;
            sphinxClient.Version = SphinxVersion.V207;
            SphinxSearchResult ResUser1 = sphinxClient.Query(Search, "user1");
            SphinxSearchResult ResUser2 = sphinxClient.Query(Search, "user2");
            SphinxSearchResult ResUser3 = sphinxClient.Query(Search, "user3");
            SphinxSearchResult ResUser4 = sphinxClient.Query(Search, "user4");
            SphinxSearchResult ResUser5 = sphinxClient.Query(Search, "user5");

            //textBox1.Text = sphinxClient.Query(Search, "user1").ToString();

            foreach (SphinxMatch match in ResUser1.Matches)
            {
                i++;
                SplitEnter(match, i);
            }
            foreach (SphinxMatch match in ResUser2.Matches)
            {
                i++;
               SplitEnter(match, i);
            }
            foreach (SphinxMatch match in ResUser3.Matches)
            {
                i++;
                SplitEnter(match, i);
            }
            foreach (SphinxMatch match in ResUser4.Matches)
            {
                i++;
                SplitEnter(match, i);
            }
            foreach (SphinxMatch match in ResUser5.Matches)
            {
                i++;
               SplitEnter(match, i);
            }

        }

        public void SplitEnter(SphinxMatch match, int i)
        {
            
            string s = Convert.ToString(match);
            
            //textBox1.Text = s;
            char r = ':';
            char r1 = ' ';
            string[] f = s.Split(r);
            string[] h = f[1].Split(r1);
            int id = Convert.ToInt32(h[1]);
            NamePeople(id);
            _ArrPeopleId[i] = id;
       
        }

        public void NamePeople(int id)
        {
            if ((id >= 0) && (id <= 100000))
            {
                _CurrentCNN = cnn1;
            }
            if ((id >= 100000) && (id <= 200000))
            {
                _CurrentCNN = cnn2;
            }
            if ((id >= 200000) && (id <= 300000))
            {
                _CurrentCNN = cnn3;
            }
            if ((id >= 300000) && (id <= 400000))
            {
                _CurrentCNN = cnn4;
            }
            if ((id >= 400000) && (id <= 500000))
            {
                _CurrentCNN = cnn5;
            }
            string Name = "", SName = "";
            cmd.Connection = _CurrentCNN;
            _CurrentCNN.Open();
            cmd.CommandText = "select Family, Name from Users where id=\'" + id + "\';";
            SqlDataReader readId1 = cmd.ExecuteReader();
            while (readId1.Read())
            {
               
                Name += readId1[0];
                SName += readId1[1];

                listBox1.Items.Add(Name + ' ' + SName);
            }
           _CurrentCNN.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool _Flag = true;
           // string a = listBox1.SelectedItem.ToString();
            int id = listBox1.SelectedIndex;
            int i =0;
            while(_Flag)
            {
                if (i == id)
                {
                    Form3 f = new Form3(_ArrPeopleId[i]);
                    f.Show();
                    _Flag = false;
                    i = 0;
                }
                i++;
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sphinxClient.SearchOptions.ResetFilters();//чистим фильтры каждый раз когда меняем значение возраста
        }

       
    }
}
