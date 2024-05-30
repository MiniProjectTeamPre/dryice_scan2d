using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dryice_scan2d {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            try { checkBox1.Checked = Convert.ToBoolean(File.ReadAllText("../../config/dryice_scan2d_checked_1.txt")); } catch { }
            try { checkBox2.Checked = Convert.ToBoolean(File.ReadAllText("../../config/dryice_scan2d_checked_2.txt")); } catch { }
            try { checkBox3.Checked = Convert.ToBoolean(File.ReadAllText("../../config/dryice_scan2d_checked_3.txt")); } catch { }
            try { checkBox4.Checked = Convert.ToBoolean(File.ReadAllText("../../config/dryice_scan2d_checked_4.txt")); } catch { }
            try { digit_sn.Text = File.ReadAllText("../../config/dryice_scan2d_digit_sn.txt"); } catch { }
            File.Delete("dryice_scan2d_sn_header_1.txt");
            File.Delete("dryice_scan2d_sn_header_2.txt");
            File.Delete("dryice_scan2d_sn_header_3.txt");
            File.Delete("dryice_scan2d_sn_header_4.txt");
            File.Delete("dryice_scan2d_clear_sn_1.txt");
            File.Delete("dryice_scan2d_clear_sn_2.txt");
            File.Delete("dryice_scan2d_clear_sn_3.txt");
            File.Delete("dryice_scan2d_clear_sn_4.txt");
            status_1.Text = "";
            status_2.Text = "";
            status_3.Text = "";
            status_4.Text = "";
            backgroundWorker1.RunWorkerAsync();
            Application.Idle += loop;
        }

        private bool flag_focus_text = false;
        private bool flag_focus_text_sup = false;
        private int flag_brink_black = 1;
        private void loop(object sender, EventArgs e) {
            this.Focus();
            if (flag_brink_black != flag_focus) {
                checkBox1.ForeColor = Color.Black;
                checkBox2.ForeColor = Color.Black;
                checkBox3.ForeColor = Color.Black;
                checkBox4.ForeColor = Color.Black;
                flag_brink_black = flag_focus;
            }
            if (flag_focus > 4) flag_focus = 1;
            string s = "";
            try {
                s = File.ReadAllText("dryice_scan2d_clear_sn_1.txt");
                File.Delete("dryice_scan2d_clear_sn_1.txt");
                textBox1.Text = "";
                status_1.Text = "";
                textBox1.Enabled = true;
                flag_focus = 1;
            } catch { }
            try {
                string jj = File.ReadAllText("dryice_scan2d_tested_1.txt");
                File.Delete("dryice_scan2d_tested_1.txt");
                if (jj == "PASS") status_1.ForeColor = Color.Blue;
                else status_1.ForeColor = Color.Red;
                status_1.Text = jj;
            } catch { }
            try {
                s = File.ReadAllText("dryice_scan2d_clear_sn_2.txt");
                File.Delete("dryice_scan2d_clear_sn_2.txt");
                textBox2.Text = "";
                status_2.Text = "";
                textBox2.Enabled = true;
                flag_focus = 2;
            } catch { }
            try {
                string jj = File.ReadAllText("dryice_scan2d_tested_2.txt");
                File.Delete("dryice_scan2d_tested_2.txt");
                if (jj == "PASS") status_2.ForeColor = Color.Blue;
                else status_2.ForeColor = Color.Red;
                status_2.Text = jj;
            } catch { }
            try {
                s = File.ReadAllText("dryice_scan2d_clear_sn_3.txt");
                File.Delete("dryice_scan2d_clear_sn_3.txt");
                textBox3.Text = "";
                status_3.Text = "";
                textBox3.Enabled = true;
                flag_focus = 3;
            } catch { }
            try {
                string jj = File.ReadAllText("dryice_scan2d_tested_3.txt");
                File.Delete("dryice_scan2d_tested_3.txt");
                if (jj == "PASS") status_3.ForeColor = Color.Blue;
                else status_3.ForeColor = Color.Red;
                status_3.Text = jj;
            } catch { }
            try {
                s = File.ReadAllText("dryice_scan2d_clear_sn_4.txt");
                File.Delete("dryice_scan2d_clear_sn_4.txt");
                textBox4.Text = "";
                status_4.Text = "";
                textBox4.Enabled = true;
                flag_focus = 4;
            } catch { }
            try {
                string jj = File.ReadAllText("dryice_scan2d_tested_4.txt");
                File.Delete("dryice_scan2d_tested_4.txt");
                if (jj == "PASS") status_4.ForeColor = Color.Blue;
                else status_4.ForeColor = Color.Red;
                status_4.Text = jj;
            } catch { }
            TextBox t = new TextBox();
            CheckBox c = new CheckBox();
            Label l = new Label();
            switch (flag_focus) {
                case 1: t = textBox1; c = checkBox1; l = status_1; break;
                case 2: t = textBox2; c = checkBox2; l = status_2; break;
                case 3: t = textBox3; c = checkBox3; l = status_3; break;
                case 4: t = textBox4; c = checkBox4; l = status_4; break;
            }
            if (flag_focus_text == true && flag_focus_text_sup == true) { t.Focus(); flag_focus_text = false; flag_focus_text_sup = false; }
            if (!c.Checked) { flag_focus++; flag_focus_text = true; Thread.Sleep(500); return; }
            if (!t.Enabled) { flag_focus++; flag_focus_text = true; Thread.Sleep(500); return; }
            if (t.Text.Count() == Convert.ToInt32(digit_sn.Text)) {
                File.WriteAllText("dryice_scan2d_sn_header_" + flag_focus + ".txt", t.Text);
                Thread.Sleep(2500);
                textBox5.Focus();
                l.Text = "TEST";
                l.ForeColor = Color.Orange;
                t.Enabled = false;
                switch (flag_focus) {
                    case 1: flag_focus = 2; break;
                    case 2: flag_focus = 1; break;
                    case 3: flag_focus = 4; break;
                    case 4: flag_focus = 3; break;
                }
                Thread.Sleep(100);
                t.BackColor = Color.White;
                c.ForeColor = Color.Black;
                flag_focus_text = true;
                return;
            }
            DelaymS(100);
            Thread.Sleep(400);
            flag_focus_text_sup = true;
        }
        private char keyboardThaiToEnglish(char keyThai) {
            char keyEnglish = new char();

            switch (keyThai) {
                case 'ๅ': keyEnglish = '1'; break;
                case '/': keyEnglish = '2'; break;
                case '-': keyEnglish = '3'; break;
                case 'ภ': keyEnglish = '4'; break;
                case 'ถ': keyEnglish = '5'; break;
                case 'ุ': keyEnglish = '6'; break;
                case 'ึ': keyEnglish = '7'; break;
                case 'ค': keyEnglish = '8'; break;
                case 'ต': keyEnglish = '9'; break;
                case 'จ': keyEnglish = '0'; break;

                case 'ข': keyEnglish = '-'; break;
                case 'ช': keyEnglish = '='; break;
                case 'ๆ': keyEnglish = 'q'; break;
                case 'ไ': keyEnglish = 'w'; break;
                case 'ำ': keyEnglish = 'e'; break;
                case 'พ': keyEnglish = 'r'; break;
                case 'ะ': keyEnglish = 't'; break;
                case 'ั': keyEnglish = 'y'; break;
                case 'ี': keyEnglish = 'u'; break;
                case 'ร': keyEnglish = 'i'; break;
                case 'น': keyEnglish = 'o'; break;
                case 'ย': keyEnglish = 'p'; break;
                case 'บ': keyEnglish = '['; break;
                case 'ล': keyEnglish = ']'; break;
                case 'ฃ': keyEnglish = '\\'; break;
                case 'ฟ': keyEnglish = 'a'; break;
                case 'ห': keyEnglish = 's'; break;
                case 'ก': keyEnglish = 'd'; break;
                case 'ด': keyEnglish = 'f'; break;
                case 'เ': keyEnglish = 'g'; break;
                case '้': keyEnglish = 'h'; break;
                case '่': keyEnglish = 'j'; break;
                case 'า': keyEnglish = 'k'; break;
                case 'ส': keyEnglish = 'l'; break;
                case 'ว': keyEnglish = ';'; break;
                case 'ง': keyEnglish = '\''; break;
                case 'ผ': keyEnglish = 'z'; break;
                case 'ป': keyEnglish = 'x'; break;
                case 'แ': keyEnglish = 'c'; break;
                case 'อ': keyEnglish = 'v'; break;
                case 'ิ': keyEnglish = 'b'; break;
                case 'ื': keyEnglish = 'n'; break;
                case 'ท': keyEnglish = 'm'; break;
                case 'ม': keyEnglish = ','; break;
                case 'ใ': keyEnglish = '.'; break;
                case 'ฝ': keyEnglish = '/'; break;

                case '+': keyEnglish = '!'; break;
                case '๑': keyEnglish = '@'; break;
                case '๒': keyEnglish = '#'; break;
                case '๓': keyEnglish = '$'; break;
                case '๔': keyEnglish = '%'; break;
                case 'ู': keyEnglish = '^'; break;
                case '฿': keyEnglish = '&'; break;
                case '๕': keyEnglish = '*'; break;
                case '๖': keyEnglish = '('; break;
                case '๗': keyEnglish = ')'; break;
                case '๘': keyEnglish = '_'; break;
                case '๙': keyEnglish = '+'; break;
                case '๐': keyEnglish = 'Q'; break;
                case '"': keyEnglish = 'W'; break;
                case 'ฎ': keyEnglish = 'E'; break;
                case 'ฑ': keyEnglish = 'R'; break;
                case 'ธ': keyEnglish = 'T'; break;
                case 'ํ': keyEnglish = 'Y'; break;
                case '๊': keyEnglish = 'U'; break;
                case 'ณ': keyEnglish = 'I'; break;
                case 'ฯ': keyEnglish = 'O'; break;
                case 'ญ': keyEnglish = 'P'; break;
                case 'ฐ': keyEnglish = '['; break;
                case ',': keyEnglish = ']'; break;
                case 'ฅ': keyEnglish = '|'; break;
                case 'ฤ': keyEnglish = 'A'; break;
                case 'ฆ': keyEnglish = 'S'; break;
                case 'ฏ': keyEnglish = 'D'; break;
                case 'โ': keyEnglish = 'F'; break;
                case 'ฌ': keyEnglish = 'G'; break;
                case '็': keyEnglish = 'H'; break;
                case '๋': keyEnglish = 'J'; break;
                case 'ษ': keyEnglish = 'K'; break;
                case 'ศ': keyEnglish = 'L'; break;
                case 'ซ': keyEnglish = ':'; break;
                case '.': keyEnglish = '"'; break;
                case '(': keyEnglish = 'Z'; break;
                case ')': keyEnglish = 'X'; break;
                case 'ฉ': keyEnglish = 'C'; break;
                case 'ฮ': keyEnglish = 'V'; break;
                case 'ฺ': keyEnglish = 'B'; break;
                case '์': keyEnglish = 'N'; break;
                case '?': keyEnglish = 'M'; break;
                case 'ฒ': keyEnglish = '<'; break;
                case 'ฬ': keyEnglish = '>'; break;
                case 'ฦ': keyEnglish = '?'; break;
            }

            return keyEnglish;
        }

        public static void DelaymS(int mS) {
            Stopwatch stopwatchDelaymS = new Stopwatch();
            stopwatchDelaymS.Restart();
            while (mS > stopwatchDelaymS.ElapsedMilliseconds) {
                if (!stopwatchDelaymS.IsRunning)
                    stopwatchDelaymS.Start();
                Application.DoEvents();
            }
            stopwatchDelaymS.Stop();
        }

        private int flag_focus = 1;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            TextBox t = new TextBox();
            CheckBox c = new CheckBox();
            while (true) {
                Thread.Sleep(250);
                switch (flag_focus) {
                    case 1: t = textBox1; c = checkBox1; break;
                    case 2: t = textBox2; c = checkBox2; break;
                    case 3: t = textBox3; c = checkBox3; break;
                    case 4: t = textBox4; c = checkBox4; break;
                }
                if (!c.Checked) continue;
                if (!t.Enabled) continue;
                if (t.Text.Count() == Convert.ToInt32(digit_sn.Text)) continue;
                backgroundWorker1.ReportProgress(0);
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            //if (e.ProgressPercentage == 0) 
            CheckBox c = new CheckBox();
            TextBox t = new TextBox();
            switch (flag_focus) {
                case 1: t = textBox1; c = checkBox1; break;
                case 2: t = textBox2; c = checkBox2; break;
                case 3: t = textBox3; c = checkBox3; break;
                case 4: t = textBox4; c = checkBox4; break;
            }
            t.Focus();
            if (t.BackColor == Color.White) t.BackColor = Color.Lime;
            else t.BackColor = Color.White;
            if (c.ForeColor == Color.Black) c.ForeColor = Color.Red;
            else c.ForeColor = Color.Black;
        }

        private void checkBox1_Click(object sender, EventArgs e) {
            File.WriteAllText("../../config/dryice_scan2d_checked_1.txt", checkBox1.Checked.ToString());
        }
        private void checkBox2_Click(object sender, EventArgs e) {
            File.WriteAllText("../../config/dryice_scan2d_checked_2.txt", checkBox2.Checked.ToString());
        }
        private void checkBox3_Click(object sender, EventArgs e) {
            File.WriteAllText("../../config/dryice_scan2d_checked_3.txt", checkBox3.Checked.ToString());
        }
        private void checkBox4_Click(object sender, EventArgs e) {
            File.WriteAllText("../../config/dryice_scan2d_checked_4.txt", checkBox4.Checked.ToString());
        }

        private void digit_sn_Click(object sender, EventArgs e) {
            int asd = 15;
            while (true) {
                string input = Microsoft.VisualBasic.Interaction.InputBox("_ใส่ digit ของ sn", "digit", digit_sn.Text, 500, 300);
                if (input == "") return;
                try {
                    asd = Convert.ToInt32(input);
                } catch (Exception) {
                    MessageBox.Show("not format");
                    continue;
                }
                break;
            }
            digit_sn.Text = asd.ToString();
            File.WriteAllText("../../config/dryice_scan2d_digit_sn.txt", asd.ToString());
        }

        private void textBox1_DoubleClick(object sender, EventArgs e) {
            flag_focus = 1;
        }
        private void textBox2_DoubleClick(object sender, EventArgs e) {
            flag_focus = 2;
        }
        private void textBox3_DoubleClick(object sender, EventArgs e) {
            flag_focus = 3;
        }
        private void textBox4_DoubleClick(object sender, EventArgs e) {
            flag_focus = 4;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (!checkBox_focus.Checked) return;
            this.Activate();
            if (Form.ActiveForm != this) {
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (InputLanguage.CurrentInputLanguage.LayoutName != "US") {
                e.KeyChar = keyboardThaiToEnglish(e.KeyChar);
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) {
            if (InputLanguage.CurrentInputLanguage.LayoutName != "US") {
                e.KeyChar = keyboardThaiToEnglish(e.KeyChar);
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e) {
            if (InputLanguage.CurrentInputLanguage.LayoutName != "US") {
                e.KeyChar = keyboardThaiToEnglish(e.KeyChar);
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e) {
            if (InputLanguage.CurrentInputLanguage.LayoutName != "US") {
                e.KeyChar = keyboardThaiToEnglish(e.KeyChar);
            }
        }
    }
}
