using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Mysql ajouté précédement dans la référence
using MySql.Data.MySqlClient;

namespace Projetstage
{
    public partial class Form1 : Form
    {
        private string strcon = null;
        private MySqlConnection cn = null;
        private MySqlCommand cm = null;
        private MySqlDataReader dr = null;
        public Form1()
        {

            strcon = "SERVER=localhost;DATABASE=adepape36;UID=root;PWD=";
            InitializeComponent();


        }

        private void initrec(string sql)
        {
            cn = new MySqlConnection(strcon);
            cn.Open();
            cm = new MySqlCommand(sql, cn);

        }
        MySqlConnection connexion = null;
        bool Connecté = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Se connecter")
            {
                MySqlConnection connexion = new MySqlConnection("SERVER=localhost;DATABASE=adepape36;UID=root;PWD=");

                try
                {
                    connexion.Open();
                    button1.Text = "Se déconnecter";
                    Connecté = true;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
            else // se déconnecter
            {

                
                button1.Text = "Se connecter";
                Connecté = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxprenom.Text == "")
            {
                MessageBox.Show("Entrez un nom et un prénom.");
            }
            else if (textBoxNom.Text == "")

            {
                MessageBox.Show("Entrez un prénom.");
            }
            else
            {
                if (Connecté)
                {


                    string Sql = "INSERT INTO Utilisateurs(nom,prenom,adresse,codepostal,ville) VALUES ('" + textBoxNom.Text + "','" + textBoxprenom.Text + "','" + textboxAdresse.Text + "','" + textBoxcodepostal.Text + "','" + textBoxville.Text + "')";
                    initrec(Sql);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Vous avez bien été ajouté à la base de données.");

                }
                else
                {
                    MessageBox.Show("Vous n'êtes pas connecté à la base de données.");


                }

                listBox1.Items.Clear();
                initrec("SELECT * FROM Utilisateurs ");
                dr = cm.ExecuteReader();


                while (dr.Read() == true)
                {
                    listBox1.Items.Add(dr["Nom"]).ToString();

                }
            

            dr.Close();
            cn.Close();
        }
    } 
        

       

        private void Form1_Load(object sender, EventArgs e)
        {
            initrec("SELECT * FROM utilisateurs");
            dr = cm.ExecuteReader();

            while (dr.Read() == true) 
            {
                
                listBox1.Items.Add(dr["Nom"]).ToString();
                
            }

            dr.Close();
            cn.Close();
            textBoxID.Enabled = false;
            textBoxnom2.Enabled = false;
            textBoxprenom2.Enabled = false;
            textBoxadresse2.Enabled = false;
            textBoxcodepostal2.Enabled = false;
            textBoxville2.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlCommand commande = new MySqlCommand("SELECT * FORM Utilisateurs WHERE ID=@id", connexion);
            commande.Parameters.AddWithValue("@id", textBoxID.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Connecté)
            {
                
                MySqlCommand commande = new MySqlCommand("SELECT * FORM Utilisateurs", connexion);
                using (MySqlDataReader Lire = cm.ExecuteReader())
                {
                    while (Lire.Read() == true)
                    {
                        string ID = Lire["ID"].ToString();
                        string Nom = Lire["nom"].ToString();
                        string Prenom = Lire["prenom"].ToString();
                        string Adresse = Lire["adresse"].ToString();
                        string Codepostal = Lire["codepostal"].ToString(); 
                        string Ville = Lire["ville"].ToString();

                        listBox1.Items.Add(new ListViewItem(new[] { ID, Nom, Prenom, Adresse, Codepostal, Ville }));
                    }
                }
            }
            else { MessageBox.Show("Vous n'êtes pas connecté à la base de donnée"); }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            {
               
                    string nom2 = listBox1.SelectedItem.ToString();
                    string sql = "DELETE FROM Utilisateurs WHERE Nom = '" + nom2 + "'";


                    initrec(sql);
                    cm.ExecuteNonQuery();
                    cn.Close();
                listBox1.Items.Clear();
                initrec("SELECT * FROM Utilisateurs ");
                dr = cm.ExecuteReader();


                while (dr.Read() == true)
                {
                    listBox1.Items.Add(dr["Nom"]).ToString();

                }


                dr.Close();
                cn.Close();

            }
                MessageBox.Show("Vous avez bien été supprimé de la base de donnée.");
            }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string nom = listBox1.SelectedItem.ToString();
            string sql = "SELECT * FROM Utilisateurs WHERE Nom = '" + nom + "'";


            initrec(sql);
            dr = cm.ExecuteReader();
            dr.Read();
            textBoxID.Text = dr["ID"].ToString();
            textBoxnom2.Text = dr["nom"].ToString();
            textBoxprenom2.Text = dr["prenom"].ToString();
            textBoxadresse2.Text = dr["adresse"].ToString();
            textBoxcodepostal2.Text = dr["codepostal"].ToString();
            textBoxville2.Text = dr["ville"].ToString();

            dr.Close();
            cn.Close();
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }
    

