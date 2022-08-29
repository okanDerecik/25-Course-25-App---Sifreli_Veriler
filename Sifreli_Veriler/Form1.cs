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

namespace Sifreli_Veriler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-K0C08G8;Initial Catalog=Test;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLVERILER",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource=dt;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = TxtAd.Text;
            byte[] addizi = ASCIIEncoding.ASCII.GetBytes(ad);
            string adsifre = Convert.ToBase64String(addizi);

            string soyad = TxtSoyad.Text;
            byte[] soyaddizi = ASCIIEncoding.ASCII.GetBytes(soyad);
            string soyadsifre = Convert.ToBase64String(soyaddizi);

            string mail = TxtMail.Text;
            byte[] maildizi = ASCIIEncoding.ASCII.GetBytes(mail);
            string mailsifre = Convert.ToBase64String(maildizi);

            string sifre = TxtSifre.Text;
            byte[] sifredizi = ASCIIEncoding.ASCII.GetBytes(sifre);
            string sifresifre = Convert.ToBase64String(sifredizi);

            string hesapno = TxtHesapNo.Text;
            byte[] hesapnodizi = ASCIIEncoding.ASCII.GetBytes(hesapno);
            string hesapnosifre = Convert.ToBase64String(hesapnodizi);

            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLVERILER (AD,SOYAD,MAIL,SIFRE,HESAPNO) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1", adsifre);
            komut.Parameters.AddWithValue("@p2", soyadsifre);
            komut.Parameters.AddWithValue("@p3", mailsifre);
            komut.Parameters.AddWithValue("@p4", sifresifre);
            komut.Parameters.AddWithValue("@p5", hesapnosifre);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veriler Eklendi");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "AD";
            dataGridView1.Columns[2].Name = "SOYAD";
            dataGridView1.Columns[3].Name = "MAIL";
            dataGridView1.Columns[4].Name = "SIFRE";
            dataGridView1.Columns[5].Name = "HESAPNO";

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLVERILER", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                byte[] adcozum = Convert.FromBase64String(dr[1].ToString());
                string ad = ASCIIEncoding.ASCII.GetString(adcozum);
                byte[] soyadcozum = Convert.FromBase64String(dr[2].ToString());
                string soyad = ASCIIEncoding.ASCII.GetString(soyadcozum);
                byte[] mailcozum = Convert.FromBase64String(dr[3].ToString());
                string mail = ASCIIEncoding.ASCII.GetString(mailcozum);
                byte[] sifrecozum = Convert.FromBase64String(dr[4].ToString());
                string sifre = ASCIIEncoding.ASCII.GetString(sifrecozum);
                byte[] hesapnocozum = Convert.FromBase64String(dr[5].ToString());
                string hesapno = ASCIIEncoding.ASCII.GetString(hesapnocozum);

                string[] veriler = { dr[0].ToString(), ad, soyad, mail, sifre, hesapno };
                dataGridView1.Rows.Add(veriler);
            }
            }
        }
}
