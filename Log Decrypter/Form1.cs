﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;
using DataGridViewAutoFilter;

namespace Log_Decrypter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Specify the path to your log file
            string logFilePath = "C:\\Users\\shashiraj.walsetwar\\Documents\\Access_Test.log";
            comboBox1.SelectedText = "--select--";
            filePath_textbox.Text = "Select Log File";

            try
            {
                // Read the contents of the log file
                string logData = File.ReadAllText(logFilePath);

                // Deserialize the JSON data into a DataTable
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(logData);

                // Bind the DataTable to the DataGridView control
                dataGridView1.DataSource = dataTable;

                // Enable auto-filtering for columns
                //dataGridView1.AutoFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Reading Log File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            
            AesEncryptor encryptor = new AesEncryptor();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    using (StreamReader read = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = read.ReadLine()) != null)
                        {
                            //Console.WriteLine(line);
                            string decrypted_line = encryptor.DecryptString(line);
                            //logDisplay.AppendText(decrypted_line+ "\n");
                        }
                    }
                    
                }
            }
            filePath_textbox.Text = filePath;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string location = System.AppDomain.CurrentDomain.BaseDirectory;
            //System.IO.StreamWriter file = new System.IO.StreamWriter($"{location}\\decryptedLogs.txt");

            //System.IO.File.WriteAllText($"{location}\\decryptedLog.txt", logDisplay.Text.Replace("\n", Environment.NewLine));
            MessageBox.Show($"Decrypted Logs stored at: {location}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem as string;


        }
    }

    public class AesEncryptor : IDisposable
    {
        private RijndaelManaged _rijndael;
        private byte[] _key;

        public AesEncryptor()
        {
            string encryptionKey = "?;kx7$^Pb;g2ftuIj,4]o8;~S>[G(}/|";
            _rijndael = new RijndaelManaged();
            _key = Encoding.UTF8.GetBytes(encryptionKey);  // Make sure this key length matches Rijndael's requirements
            _rijndael.Key = _key;
        }

        public string EncryptString(string plainText)
        {
            _rijndael.GenerateIV();

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(_rijndael.IV, 0, _rijndael.IV.Length);

                using (CryptoStream cs = new CryptoStream(ms, _rijndael.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public string DecryptString(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (MemoryStream ms = new MemoryStream(encryptedBytes))
            {
                byte[] iv = new byte[16];
                ms.Read(iv, 0, iv.Length);
                _rijndael.IV = iv;

                using (CryptoStream cs = new CryptoStream(ms, _rijndael.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_rijndael != null)
            {
                _rijndael.Clear();
                _rijndael = null;
            }
        }
    }
}
