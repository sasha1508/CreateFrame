using CreateFrame.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreateFrame.Model;

namespace CreateFrame.AddInWorkers
{
    public partial class Form_EnterSize : Form
    {
        public Form_EnterSize()
        {
            InitializeComponent();
        }

        private void Form_EnterSize_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (Convert.ToDouble(textBox1.Text) < 160 || Convert.ToDouble(textBox2.Text) < 160 || Convert.ToDouble(textBox3.Text) < 160)
                {
                    MessageBox.Show("Габаритные размеры не могут быть меньше 160 мм", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Создаём пространственную раму:
                    Create3D.CreateFrame(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text));

                    Close();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Введите размеры в правильном формате", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);       
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
