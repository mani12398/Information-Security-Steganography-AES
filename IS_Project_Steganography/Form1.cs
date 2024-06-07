using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IS_Project_Steganography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeScrolling();
            richTextBox1.Focus();
            richTextBox2.TabStop = false;
            richTextBox3.TabStop = false;
            richTextBox4.TabStop = false;
            textBox3.TabStop = false;
            textBox1.TabStop = false;
            textBox4.TabStop = false;
            this.Shown += new EventHandler(Form1_Shown);
        }
        private void InitializeScrolling()
        {
            
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            this.Controls.Add(panel);
            panel.Controls.Add(textBox1);
            panel.Controls.Add(richTextBox1);
            panel.Controls.Add(richTextBox2);
            panel.Controls.Add(label3);
            panel.Controls.Add(label9);
            panel.Controls.Add(button1);
            panel.Controls.Add(button2);
            panel.Controls.Add(button3);
            panel.Controls.Add(button4);
            panel.Controls.Add(button5);
            panel.Controls.Add(button6);
            panel.Controls.Add(pictureBox2);
            panel.Controls.Add(pictureBox3);
            panel.Controls.Add(textBox2);
            panel.Controls.Add(textBox3);
            panel.Controls.Add(textBox4);
            panel.Controls.Add(richTextBox3);
            panel.Controls.Add(richTextBox4);
            panel.Controls.Add(label13);
            panel.Controls.Add(label14);
            panel.Controls.Add(button9);
            panel.Controls.Add(button11);
            panel.Controls.Add(button10);
            panel.Controls.Add(button7);
            panel.Controls.Add(button8);



            int totalHeight = 0;
            foreach (Control control in panel.Controls)
            {
                totalHeight += control.Height;
            }
            panel.ClientSize = new Size(panel.ClientSize.Width, totalHeight + 10);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            
        }

        private string EncryptAES(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            int characterCount = text.Length;
            label3.Text = $"Character Count: {characterCount}";
            label3.ForeColor = Color.Green;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            int characterCount = text.Length;
            label9.Text = $"Character Count: {characterCount}";
            label9.ForeColor = Color.Green;

            if (textBox1.Text == textBox2.Text)
            {
                label15.Text = "Key Matched.";
                label15.ForeColor = Color.Green;
            }
            else
            {
                label15.Text = "Key do not Match.";
                label15.ForeColor = Color.Red;
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                MessageBox.Show("Please generate a Ciphertext first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
                openDialog.InitialDirectory = @"C:\Users\MANI-LAPTOP\source\repos\IS_Project_Steganography\IS_Project_Steganography\IS Project Pics";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    textBox3.Text = openDialog.FileName.ToString();
                    pictureBox2.ImageLocation = textBox3.Text;
                }
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = textBox1.Text;
            string message = richTextBox1.Text;

            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(message))
            {

                if (key.Length == 16 || key.Length == 24 || key.Length == 32)
                {

                    string cipherText = EncryptAES(message, key);
                    richTextBox2.Text = cipherText;
                }
                else
                {
                    MessageBox.Show("Key must be 128, 192, or 256 bits (16, 24, or 32 bytes) for AES encryption.", "Invalid Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter both a key and a message.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetUniqueFilePath(string directory, string fileName)
        {
            string filePath = Path.Combine(directory, fileName);
            if (!File.Exists(filePath))
            {
                return filePath; 
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);
            int counter = 1;

            
            do
            {
                string uniqueFileName = $"{fileNameWithoutExtension}_{counter}{fileExtension}";
                filePath = Path.Combine(directory, uniqueFileName);
                counter++;
            } while (File.Exists(filePath));

            return filePath;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null && string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please select an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                MessageBox.Show("Please generate a Ciphertext first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Bitmap img;
                try
                {
                    img = new Bitmap(textBox3.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Bitmap imgCopy = new Bitmap(img);

                img.Dispose();

                for (int i = 0; i < imgCopy.Width; i++)
                {
                    for (int j = 0; j < imgCopy.Height; j++)
                    {
                        Color pixel = imgCopy.GetPixel(i, j);

                        if (i < 1 && j < richTextBox2.TextLength)
                        {
                            char letter = Convert.ToChar(richTextBox2.Text.Substring(j, 1));
                            int value = Convert.ToInt32(letter);
                            imgCopy.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                        }

                        if (i == imgCopy.Width - 1 && j == imgCopy.Height - 1)
                        {
                            imgCopy.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, richTextBox2.TextLength));
                        }
                    }
                }

                string directory = @"C:\Users\MANI-LAPTOP\source\repos\IS_Project_Steganography\IS_Project_Steganography\IS Project Pics";
                string encodedFileName = "encoded_image.png";

                try
                {
                    string uniqueFilePath = GetUniqueFilePath(directory, encodedFileName);

                    imgCopy.Save(uniqueFilePath);
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.ImageLocation = uniqueFilePath;
                    textBox4.Text = uniqueFilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving encoded image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    imgCopy.Dispose();
                }
            }

            
           


        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image == null && string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please perform steganography first to get stego object image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Bitmap img;
                try
                {
                    img = new Bitmap(textBox4.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading stego object image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string extractedMessage = "";

                Color lastpixel = img.GetPixel(img.Width - 1, img.Height - 1);
                int msgLength = lastpixel.B;

                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color pixel = img.GetPixel(i, j);

                        if (i < 1 && j < msgLength)
                        {
                            int value = pixel.B;
                            char c = Convert.ToChar(value);
                            string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });

                            extractedMessage = extractedMessage + letter;
                        }
                    }
                }

                richTextBox3.Text = extractedMessage;

                string originalCiphertext = richTextBox2.Text;
                if (extractedMessage == originalCiphertext)
                {
                    label13.Text = "Ciphertext Match.";
                    label13.ForeColor = Color.Green; 
                }
                else
                {
                    label13.Text = "Ciphertext do not Match.";
                    label13.ForeColor = Color.Red; 
                }
            }


        }
        private string DecryptAES(string cipherText, string key)
        {
            try
            {

                byte[] keyBytes = Encoding.UTF8.GetBytes(key);


                if (keyBytes.Length != 16 && keyBytes.Length != 24 && keyBytes.Length != 32)
                {
                    MessageBox.Show("Key must be 128, 192, or 256 bits (16, 24, or 32 bytes) for AES encryption.", "Invalid Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }


                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = keyBytes;
                    aesAlg.Mode = CipherMode.ECB;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                    byte[] cipherBytes = Convert.FromBase64String(cipherText);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (CryptographicException)
            {
                MessageBox.Show("Error decrypting the cipher text. Please check the key.", "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (FormatException)
            {
                MessageBox.Show("The input is not a valid Base64 string.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string cipherText = richTextBox3.Text;
            string key = textBox2.Text;

            if (!string.IsNullOrWhiteSpace(cipherText) && !string.IsNullOrWhiteSpace(key))
            {
                string encryptedKey = textBox1.Text;
                if (key.Equals(encryptedKey))
                {

                    string decryptedPlainText = DecryptAES(cipherText, key);


                    richTextBox4.Text = decryptedPlainText;
                    string originalplaintext = richTextBox1.Text;
                    if (decryptedPlainText == originalplaintext)
                    {
                        label14.Text = "Plain Text Match.";
                        label14.ForeColor = Color.Green;

                    }
                    else
                    {
                        label14.Text = "Plain Text do not Match.";
                        label14.ForeColor = Color.Red;
                    }

                }
                else
                {
                    MessageBox.Show("The provided key does not match the key used for encryption.", "Key Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter both the cipher text and the key used for encryption.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            richTextBox2.Clear();
            pictureBox2.Image = null;
            textBox3.Clear();
            pictureBox3.Image = null;
            textBox4.Clear();
            richTextBox3.Clear();
            textBox2.Clear();
            richTextBox4.Clear();
            label3.Text = "Character Count:";
            label9.Text = "Character Count:";
            label3.ForeColor = Color.DarkSlateGray;
            label9.ForeColor = Color.DarkSlateGray;
            label13.Text = "";
            label14.Text = "";
            richTextBox1.Clear();
            Clipboard.Clear();
            label15.Text = "";

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string randomKey = GenerateRandomKey(16);
            textBox1.Text = randomKey;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string randomKey = GenerateRandomKey(24);
            textBox1.Text = randomKey;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string randomKey = GenerateRandomKey(32);
            textBox1.Text = randomKey;
        }

        private string GenerateRandomKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";

            StringBuilder result = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            richTextBox1.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                
                Clipboard.SetText(textBox1.Text);
                
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                textBox2.Text = Clipboard.GetText();
            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Clipboard.Clear();
        }

        private void label15_Click(object sender, EventArgs e)
        {
           
        }
    }
}
