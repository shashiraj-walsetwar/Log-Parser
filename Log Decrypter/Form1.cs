using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            logEncryptor encryptor = new logEncryptor();
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
                            logDisplay.AppendText(decrypted_line+ "\n");
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

            System.IO.File.WriteAllText($"{location}\\decryptedLog.txt", logDisplay.Text.Replace("\n", Environment.NewLine));
            MessageBox.Show($"Decrypted Logs stored at: {location}");
        }
    }

    // Rijndael's Encryption for .net target framework 2.0
    /*public class AesEncryptor : IDisposable
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
    }*/

    public class logEncryptor : IDisposable
    {

        private Aes _aes;
        private byte[] _key;

        public logEncryptor()
        {
            string encryptionKey = "?;kx7$^Pb;g2ftuIj,4]o8;~S>[G(}/|";
            _aes = Aes.Create();
            _key = Encoding.UTF8.GetBytes(encryptionKey);
            _aes.Key = _key;
        }

        public string EncryptString(string plainText)
        {
            _aes.GenerateIV();

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(_aes.IV, 0, _aes.IV.Length);

                using (CryptoStream cs = new CryptoStream(ms, _aes.CreateEncryptor(), CryptoStreamMode.Write))
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
                _aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(ms, _aes.CreateDecryptor(), CryptoStreamMode.Read))
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
            if (_aes != null)
            {
                _aes.Clear();
                _aes = null;
            }
        }
    }
}
