using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Juliett.Properties;

namespace Juliett
{
    public partial class Form1 : Form
    {
        public bool programEnabled;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint GetCurrentThreadId();
        [DllImport("user32")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, int Msg, char wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        globalKeyboardHook gkh1 = new globalKeyboardHook();
        globalKeyboardHook gkh2 = new globalKeyboardHook();


        private void EnableProgramm()
        {
            programEnabled = true;
            gkh1.hook();
            gkh2.unhook();
            startToolStripMenuItem.Checked = true;
            stopToolStripMenuItem.Checked = false;
            pictureBox1.Image = Resources.V;
            notifyIcon1.Icon = Resources.V1;
        }
        private void DisableProgramm()
        {
            programEnabled = false;
            gkh1.unhook();
            gkh2.hook();
            stopToolStripMenuItem.Checked = true;
            startToolStripMenuItem.Checked = false;
            pictureBox1.Image = Resources.X;
            notifyIcon1.Icon = Resources.X1;
        }

        private void functionlOne(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    foreach (char ch in textBox1.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F2:
                    foreach (char ch in textBox2.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F3:
                    foreach (char ch in textBox3.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F4:
                    foreach (char ch in textBox4.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F5:
                    foreach (char ch in textBox5.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F6:
                    foreach (char ch in textBox6.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F7:
                    foreach (char ch in textBox7.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F8:
                    foreach (char ch in textBox8.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F9:
                    foreach (char ch in textBox9.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F10:
                    foreach (char ch in textBox10.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F12:
                    DisableProgramm();
                    break;
                default:
                    break;
            }

            
            e.Handled = true;//запрет получения клавиш другими приложениями
        }

        private void functionlTwo(object sender, KeyEventArgs e)
        {
            EnableProgramm();
            e.Handled = true;//запрет получения клавиш другими приложениями
        }




        private IntPtr GetFocusedControl()
        {
            IntPtr hFocus;
            IntPtr hFore;
            uint id = 0;
            //узнаем в каком окне находится фокус ввода
            hFore = GetForegroundWindow();
            //подключаемся к процессу
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), true);
            //получаем хэндл фокуса
            hFocus = GetFocus();
            //отключаемся от процесса
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), false);
            return hFocus;
        }

        
        public Form1()
        {
            InitializeComponent();
            ShowTheForm(this);
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            #region HotKeys
            gkh2.HookedKeys.Add(Keys.F11);
            gkh1.HookedKeys.Add(Keys.F1);
            gkh1.HookedKeys.Add(Keys.F2);
            gkh1.HookedKeys.Add(Keys.F3);
            gkh1.HookedKeys.Add(Keys.F4);
            gkh1.HookedKeys.Add(Keys.F5);
            gkh1.HookedKeys.Add(Keys.F6);
            gkh1.HookedKeys.Add(Keys.F7);
            gkh1.HookedKeys.Add(Keys.F8);
            gkh1.HookedKeys.Add(Keys.F9);
            gkh1.HookedKeys.Add(Keys.F10);
            gkh1.HookedKeys.Add(Keys.F12);
            #endregion
            gkh1.KeyDown += new KeyEventHandler(functionlOne);
            gkh2.KeyDown += new KeyEventHandler(functionlTwo);
            EnableProgramm();
            StartupSettings();
            
        }


        private void StartupSettings()
        {
            textBox1.Text = Settings.Default["textBox1Text"].ToString();
            textBox2.Text = Settings.Default["textBox2Text"].ToString();
            textBox3.Text = Settings.Default["textBox3Text"].ToString();
            textBox4.Text = Settings.Default["textBox4Text"].ToString();
            textBox5.Text = Settings.Default["textBox5Text"].ToString();
            textBox6.Text = Settings.Default["textBox6Text"].ToString();
            textBox7.Text = Settings.Default["textBox7Text"].ToString();
            textBox8.Text = Settings.Default["textBox8Text"].ToString();
            textBox9.Text = Settings.Default["textBox9Text"].ToString();
            textBox10.Text = Settings.Default["textBox10Text"].ToString();
        }

        

        private void CloseupSettings()
        {
            Settings.Default["textBox1Text"] = textBox1.Text;
            Settings.Default["textBox2Text"] = textBox2.Text;
            Settings.Default["textBox3Text"] = textBox3.Text;
            Settings.Default["textBox4Text"] = textBox4.Text;
            Settings.Default["textBox5Text"] = textBox5.Text;
            Settings.Default["textBox6Text"] = textBox6.Text;
            Settings.Default["textBox7Text"] = textBox7.Text;
            Settings.Default["textBox8Text"] = textBox8.Text;
            Settings.Default["textBox9Text"] = textBox9.Text;
            Settings.Default["textBox10Text"] = textBox10.Text;
            Settings.Default.Save();
        }


        #region SmoothWindows
        private async void ShowTheForm(Form form)
        {
            while (form.Opacity < 0.9)
            {
                form.Opacity += 0.04;
                await Task.Delay(10);
            }

            form.Opacity = 1;
        }


        private void UnShowTheForm(Form form)
        {
            while (form.Opacity > 0.1)
            {
                form.Opacity -= 0.04;
                Thread.Sleep(10);
            }

            form.Opacity = 0;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            UnShowTheForm(this);
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseupSettings();
            UnShowTheForm(this);
        }
        #endregion

        
        private void AboutButton(object sender, EventArgs e)
        {
            bool form2Moving = false;
            int oldMousePos2X = 0, oldMousePos2Y = 0;
            Form form2 = new Form();
            if (form2 == null)
            {
                form2 = new Form();
            }
            Label labFor2 = new Label();
            Label labFor22 = new Label();
            Button butFor2 = new Button();
            
            form2.BackColor = System.Drawing.Color.Black;
            form2.BackgroundImage = Resources.N;
            form2.BackgroundImageLayout = ImageLayout.Stretch;
            form2.ClientSize = new System.Drawing.Size(150, 150);
            form2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form2.Opacity = 0D;
            form2.Controls.Add(butFor2);
            form2.Controls.Add(labFor2);
            form2.Controls.Add(labFor22);
            labFor2.Location = new Point(5, 5);
            labFor2.Height = 25;
            labFor2.Width = 130;
            labFor2.Text = "Only for Nimfea Noctis";
            labFor2.ForeColor = Color.White;
            labFor22.Location = new Point(5, form2.Height - 25);
            labFor22.Height = 25;
            labFor22.Width = 130;
            labFor22.Text = "Only for Nimfea Noctis";
            labFor22.ForeColor = Color.White;
            form2.Show();
            butFor2.BackColor = System.Drawing.Color.Black;
            butFor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            butFor2.ForeColor = System.Drawing.Color.White;
            butFor2.Location = new System.Drawing.Point(124, 2);
            butFor2.Name = "button1";
            butFor2.Size = new System.Drawing.Size(24, 23);
            butFor2.TabIndex = 0;
            butFor2.Text = "V";
            butFor2.UseVisualStyleBackColor = false;
            butFor2.Click += (s, swe) => { UnShowTheForm(form2); form2.Close(); };
            form2.MouseDown += (s, swe) =>
            {
                form2Moving = true;
                oldMousePos2X = swe.X;
                oldMousePos2Y = swe.Y;
            };
            form2.MouseMove += (s, swe) => { if (form2Moving) form2.Location = new Point(form2.Location.X + swe.X - oldMousePos2X, form2.Location.Y + swe.Y - oldMousePos2Y);};
            form2.MouseUp += (s, swe) => { form2Moving = false;};
            ShowTheForm(form2);
        }


        private void ClosingTheProgram(object sender, EventArgs e)
        {
            DisableProgramm();
            gkh2.unhook();
            Close();
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowTheForm(this);
            ShowInTaskbar = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutButton(this, null);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableProgramm();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableProgramm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseupSettings();
            ClosingTheProgram(this, null);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (programEnabled)
            {
                DisableProgramm();
            }
            else
            {
                EnableProgramm();
            }
        }

        private static readonly int minChB = 26;
        private static readonly int maxChB = 111;
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Height = maxChB;
            textBox1.BringToFront();
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Height = maxChB;
            textBox2.BringToFront();
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Height = maxChB;
            textBox3.BringToFront();
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Height = maxChB;
            textBox4.BringToFront();
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.Height = maxChB;
            textBox5.BringToFront();
        }
        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.Height = maxChB;
            textBox6.BringToFront();
        }
        private void textBox7_Enter(object sender, EventArgs e)
        {
            textBox7.Height = maxChB;
            textBox7.BringToFront();
        }
        private void textBox8_Enter(object sender, EventArgs e)
        {
            textBox8.Height = maxChB;
            textBox8.BringToFront();
            textBox8.Location = new Point(textBox8.Location.X,textBox8.Location.Y- maxChB+minChB);
        }
        private void textBox9_Enter(object sender, EventArgs e)
        {
            textBox9.Height = maxChB;
            textBox9.BringToFront();
            textBox9.Location = new Point(textBox9.Location.X, textBox9.Location.Y - maxChB + minChB);
        }
        private void textBox10_Enter(object sender, EventArgs e)
        {
            textBox10.Height = maxChB;
            textBox10.BringToFront();
            textBox10.Location = new Point(textBox10.Location.X, textBox10.Location.Y - maxChB + minChB);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Height = minChB;
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.Height = minChB;
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.Height = minChB;
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            textBox4.Height = minChB;
        }
        private void textBox5_Leave(object sender, EventArgs e)
        {
            textBox5.Height = minChB;
        }
        private void textBox6_Leave(object sender, EventArgs e)
        {
            textBox6.Height = minChB;
        }
        private void textBox7_Leave(object sender, EventArgs e)
        {
            textBox7.Height = minChB;
        }
        private void textBox8_Leave(object sender, EventArgs e)
        {
            textBox8.Height = minChB;
            textBox8.Location = new Point(textBox8.Location.X, textBox8.Location.Y + maxChB - minChB);
        }
        private void textBox9_Leave(object sender, EventArgs e)
        {
            textBox9.Height = minChB;
            textBox9.Location = new Point(textBox9.Location.X, textBox9.Location.Y + maxChB - minChB);
        }
        private void textBox10_Leave(object sender, EventArgs e)
        {
            textBox10.Height = minChB;
            textBox10.Location = new Point(textBox10.Location.X, textBox10.Location.Y + maxChB - minChB);
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Lines.Length > 8) { textBox1.ScrollBars = ScrollBars.Vertical; }else { textBox1.ScrollBars = ScrollBars.None; }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Lines.Length > 8) { textBox2.ScrollBars = ScrollBars.Vertical; } else { textBox2.ScrollBars = ScrollBars.None; }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Lines.Length > 8) { textBox3.ScrollBars = ScrollBars.Vertical; } else { textBox3.ScrollBars = ScrollBars.None; }

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Lines.Length > 8) { textBox4.ScrollBars = ScrollBars.Vertical; } else { textBox4.ScrollBars = ScrollBars.None; }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Lines.Length > 8) { textBox5.ScrollBars = ScrollBars.Vertical; } else { textBox5.ScrollBars = ScrollBars.None; }
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Lines.Length > 8) { textBox6.ScrollBars = ScrollBars.Vertical; } else { textBox6.ScrollBars = ScrollBars.None; }
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Lines.Length > 8) { textBox7.ScrollBars = ScrollBars.Vertical; } else { textBox7.ScrollBars = ScrollBars.None; }
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Lines.Length > 8) { textBox8.ScrollBars = ScrollBars.Vertical; } else { textBox8.ScrollBars = ScrollBars.None; }
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Lines.Length > 8) { textBox9.ScrollBars = ScrollBars.Vertical; } else { textBox9.ScrollBars = ScrollBars.None; }
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Lines.Length > 8) { textBox10.ScrollBars = ScrollBars.Vertical; } else { textBox10.ScrollBars = ScrollBars.None; }
        }

        private int oldMousePosX, oldMousePosY;
        private bool formMoving = false;



        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            oldMousePosX = e.X;
            oldMousePosY = e.Y;
            formMoving = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            formMoving = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formMoving)
            {
                Location = new Point(Location.X+e.X - oldMousePosX,Location.Y+e.Y - oldMousePosY);
            }
        }

    }
}
