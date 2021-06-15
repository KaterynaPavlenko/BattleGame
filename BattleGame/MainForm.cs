using System;
using System.Windows.Forms;
using BattleGame.Map.Controller;

namespace BattleGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            timer1.Interval = 1;
            timer2.Interval = 3;
            timer1.Tick += Update;
            timer2.Tick += UpdateBullet;
            MapController.Init();
            Init();
        }

        public void Init()
        {
            timer1.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            pictureBoxMap.Refresh();
        }

        public void UpdateBullet(object sender, EventArgs e)
        {
            pictureBoxMap.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            MapController.DrawMap(g);
        }

        private void MainForm_KeyDown_1(object sender, KeyEventArgs e)
        {
            CameraController.Move(sender, e);
        }
    }
}