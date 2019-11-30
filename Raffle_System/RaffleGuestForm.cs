﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raffle_System
{
    public partial class RaffleGuestForm : Form
    {
        public List<string> eventRow; // Getting the rows for Event
        List<List<string>> data; //data on who's in event

        DatabaseConnect connect; // for class database

        public RaffleGuestForm()
        {
            InitializeComponent();
        }
        Boolean running = false;// Timer
        System.Media.SoundPlayer player;// Music
        private void btnRaffleStart_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                //Music plays
                player = new System.Media.SoundPlayer("gg1.wav");
                player.Play();

                timeRandom.Start();
                running = true;
                btnRaffleStart.BackColor = Color.FromArgb(226, 67, 83);
                btnRaffleStart.Text = "STOP";
            }
            else if (running == true)
            {
                player.Stop();
                var completeName = data[index][2] + ", " + data[index][1];
                lblRandomGuest.Text = completeName;
                timeRandom.Stop();
                running = false;
                btnRaffleStart.BackColor = Color.FromArgb(36, 116, 116);
                btnRaffleStart.Text = "GO";
                data.RemoveAt(index);
            }
        }

        private void RaffleGuestForm_Load(object sender, EventArgs e)
        {
            connect = new DatabaseConnect();
            data = connect.GuestAttendanceData(eventRow[5].ToString());
        }

        Random rand;
        int index;
        private void timeRandom_Tick(object sender, EventArgs e)
        {
            rand = new Random();
            index = rand.Next(data.Count);
            var num = data[index][2] + ", " + data[index][1];
            lblRandomGuest.Text = num;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mfInitialize = new MainForm();
            mfInitialize.Show();
            this.Close();
        }
    }
}
