using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessMemoryReaderLib;

namespace MapleMacro
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        Process[] MyProcess;
        ProcessMemoryReader mem = new ProcessMemoryReader();
        bool attach = false;

        private static DateTime Delay(int MS)

        {

            DateTime ThisMoment = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);

            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)

            {

                System.Windows.Forms.Application.DoEvents();

                ThisMoment = DateTime.Now;

            }

            return DateTime.Now;

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            bool trueOrfalse = true;
            int delay1 = r.Next(500, 530);

            Delay(5000);
            while (trueOrfalse)
            {
                SendKeys.Send("{a}");
                Delay(delay1);
                SendKeys.Send("{a}");
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("종료하시겠습니까?", "종료메시지", MessageBoxButtons.OKCancel);

            if(result == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Abort;
                Application.Exit();
            }
        }

        private void ComboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            MyProcess = Process.GetProcesses();

            for(int i = 0; i < MyProcess.Length; i++)
            {
                String text = MyProcess[i].ProcessName + "-" + MyProcess[i].Id;
                comboBox1.Items.Add(text);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    String selectedItem = comboBox1.SelectedItem.ToString();
                    int pid = int.Parse(selectedItem.Split('-')[selectedItem.Split('-').Length - 1]);
                    Process attachProc = Process.GetProcessById(pid);

                    mem.ReadProcess = attachProc;
                    mem.OpenProcess();
                    attach = true;

                    MessageBox.Show("프로세스 열기 성공! " + attachProc.ProcessName);

                }
            }
            catch(Exception ex)
            {
                attach = false;

                MessageBox.Show("프로세스 열기 실패! " + ex.Message);
            }
        }
    }
}
