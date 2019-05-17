using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SafeAndSecure
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private static Random random = new Random();
        private string server;
        private string database;
        private string uid;
        private string password;
        public static ZipFile zip;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void CreateAccount_Click(object sender, EventArgs e)
        {
            // Username and password returned from prompt box
            var longTuple = Prompt.ShowDialog("Please choose a username.", "Account Creation");
            String username = longTuple.Item1;
            String pass = longTuple.Item2;
            if (username != "" && pass != "")
            {
                string passFile = "passFile.txt";
                String zipPass = RandomString(8);

                string connString = "Server=localhost;Port=3306;Database=sensitivestorage;Uid=root;password=discipline;";
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT username FROM users WHERE username='" + username + "'";

                try
                {
                    conn.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                        MessageBox.Show("Username taken, please choose another.");
                    else
                    {
                        reader.Close();
                        int zipNum = 0;
                        string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        string filter = "Secure*.*";
                        string[] files = Directory.GetFiles(folder, filter);
                        foreach (string file in files)
                        {
                            string strNum = file.Substring(file.LastIndexOf('\\') + 7);
                            strNum = strNum.Substring(0, strNum.IndexOf('.'));
                            int num = int.Parse(strNum);
                            if (num > zipNum)
                                zipNum = num;
                        }
                        ++zipNum;

                        command.CommandText = "INSERT INTO users VALUES ('" + username + "','" + zipPass + "'," + zipNum + ")";
                        command.ExecuteReader();

                        using (zip = new ZipFile())
                        {
                            var myFile = File.Create(passFile);
                            myFile.Close();
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(passFile, true))
                            {
                                file.Write(pass);
                            }

                            zip.Password = zipPass;
                            zip.AddFile("passFile.txt");
                            zip.Save("Secure" + zipNum.ToString() + ".zip");
                            File.Delete(passFile);
                        }

                        MessageBox.Show("Account creation successful!  Please do not delete the generated zip file or its contents.");
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (username == "" && pass != "")
                MessageBox.Show("Please create a username.");
            else if (pass == "" && username != "")
                MessageBox.Show("Please create a password.");
        }

        // Code taken from: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var longTuple = Prompt.ShowDialog("Username", "Login");
            String inputUsername = longTuple.Item1;
            String inputPass = longTuple.Item2;

            string connString = "Server=localhost;Port=3306;Database=sensitivestorage;Uid=root;password=discipline;";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT username FROM users WHERE username='" + inputUsername + "'";

            try
            {
                conn.Open();
                string username=null;
                var reader = command.ExecuteReader();
                if (reader.Read())
                    username = reader.GetString(0);
                else
                    MessageBox.Show("Username not found!");
                reader.Close();
                string zipPass = "";
                string realPass = "";
          
                if (inputUsername == username)
                {
                    command.CommandText = "SELECT zipPass FROM users WHERE username='" + username + "'";
                    reader = command.ExecuteReader();
                    reader.Read();
                    zipPass = reader.GetString(0);
                    reader.Close();
                    command.CommandText = "SELECT zipNum FROM users WHERE username='" + username + "'";
                    reader = command.ExecuteReader();
                    reader.Read();
                    Int16 zipNum = reader.GetInt16(0);
                    using (zip = new ZipFile("Secure" + zipNum + ".zip"))
                    {
                        zip.Password = zipPass;
                        try
                        {
                            zip.ExtractAll(Application.StartupPath);
                        }
                        catch (Ionic.Zip.BadPasswordException)
                        {
                            MessageBox.Show("Incorrect credentials.");
                        }
                        zip.Save();
                    }
                    
                    using (System.IO.StreamReader file = new System.IO.StreamReader("passFile.txt", true))
                    {
                        realPass = file.ReadLine();
                    }
                    if (File.Exists("passFile.txt"))
                        File.Delete("passFile.txt");

                    if (inputPass == realPass)
                        MessageBox.Show("Login successful!");
                    else
                        MessageBox.Show("Password incorrect, try again.");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    // Code adapted from: https://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
    public static class Prompt
    {
        public static Tuple<string,string> ShowDialog(string text, string caption)
        {

            Form prompt = new Form()
            {
                Width = 500,
                Height = 250,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 150, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Label textLabel2 = new Label() { Left = 50, Top = 100, Width = 150, Text = "Password"};
            TextBox textBox2 = new TextBox() { Left = 50, Top = 130, Width = 400 };
            Button confirmation = new Button() { Text = "Submit", Left = 350, Width = 100, Top = 160, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox2);
            prompt.Controls.Add(textLabel2);
            prompt.AcceptButton = confirmation;



            return Tuple.Create(prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "", textBox2.Text);
        }
    }

}
