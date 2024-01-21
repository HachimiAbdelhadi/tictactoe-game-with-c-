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
using TicTacToeGame.Properties;

namespace TicTacToeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string Player1 = "Player1";
        string Player2 = "Player2";
        byte[,] resultPlayer1 = new byte [3, 3];
        byte[,] resultPlayer2 = new byte [3, 3];
        byte PlayCounter = 0;
        bool GameOver = false;
        enum GameChoice
        {
            Player1=1,
            player2=2,
        }
        bool CheckIfWin(byte[,] ResultGame )
        {
            byte result=0 ;
            byte scoreWiner1 = Convert.ToByte(GameChoice.Player1);
            scoreWiner1 = (byte)(scoreWiner1 * 3);
            byte scoreWiner2 = Convert.ToByte(GameChoice.player2);
            scoreWiner2 = (byte)(scoreWiner2 * 3);

            for (byte i=0;i<3;i++)
            {
                for(byte j=0;j<3;j++)
                {
                    result += ResultGame[i, j];
                }
                if(result == (scoreWiner1) || result == (scoreWiner2))
                {
                    return true;
                }
                result = 0;

            }
            for (byte i = 0; i < 3; i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    result += ResultGame[j, i];
                }
                if (result == (scoreWiner1) || result == (scoreWiner2))
                {
                    return true;
                }
                result = 0;

            }
            for (byte i = 0; i < 3; i++)
            {
                result += ResultGame[i, i];
            }
            if (result == (scoreWiner1) || result == (scoreWiner2))
            {
                return true;
            }
            result = 0;
            byte r = 2;
            for (byte i = 0; i < 3; i++)
            {               
                    result += ResultGame[i, r];
                r--;
            }
            if (result == (scoreWiner1) || result == (scoreWiner2))
            {
                return true;
            }

            return false;
        }


        void LockedPictures(bool UnLocked)
        {

            pct1.Enabled = UnLocked;
            pct2.Enabled = UnLocked;
            pct3.Enabled = UnLocked;
            pct4.Enabled = UnLocked;
            pct5.Enabled = UnLocked;
            pct6.Enabled = UnLocked;
            pct7.Enabled = UnLocked;
            pct8.Enabled = UnLocked;
            pct9.Enabled = UnLocked;
        }
        void UpdateResultWiner(object sender)
        {
            if(PlayCounter<10)
            {
                string[] koko = ((PictureBox)sender).Tag.ToString().Split(',');
                byte x = (Convert.ToByte(koko[0]));
                byte y = (Convert.ToByte(koko[1]));

                if (lblPlay.Text == Player1)
                {
                    resultPlayer1[x, y] = Convert.ToByte(GameChoice.Player1);
                }
                else
                {
                    resultPlayer2[x, y] = Convert.ToByte(GameChoice.player2);
                }
                PlayCounter++;
            }
         

        }

        void UpdatePicture(object sender)
        {
            if(PlayCounter <10)
            {
                if (lblPlay.Text == Player1)
                {
                    ((PictureBox)sender).Image = Resources.X;
                }
                else if (lblPlay.Text == Player2)
                {
                    ((PictureBox)sender).Image = Resources.O;

                }
                else
                    ((PictureBox)sender).Image = Resources.question_mark_96;


                
            }
        }
        void CheckWinner()
        {

            if (lblPlay.Text == Player1)
            {
                if (CheckIfWin(resultPlayer1))
                {
                    lblFinalWiner.Text = Player1;
                    LockedPictures(true);
                    MessageBox.Show("Game Over", "Game Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GameOver = true;

                }
            }
            else
            {
                if (CheckIfWin(resultPlayer2))
                {
                    lblFinalWiner.Text = Player2;
                    LockedPictures(true);
                    MessageBox.Show("Game Over", "Game Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GameOver = true;
                }
            }
            

            
        }
        void UpdateRound(object sender)
        {
            if(!GameOver)
            {
                UpdatePicture(sender);
                UpdateResultWiner(sender);
                if (PlayCounter > 4)
                {
                    CheckWinner();
                }

                if (lblPlay.Text == Player1)
                {
                    lblPlay.Text = Player2;
                }
                else
                {
                    lblPlay.Text = Player1;
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Choice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LockedPictures(false);
            }
        }

        void RestPictures()
        {
            pct1.Image = Resources.question_mark_96;
            pct2.Image = Resources.question_mark_96;
            pct3.Image = Resources.question_mark_96;
            pct4.Image = Resources.question_mark_96;
            pct5.Image = Resources.question_mark_96;
            pct6.Image = Resources.question_mark_96;
            pct7.Image = Resources.question_mark_96;
            pct8.Image = Resources.question_mark_96;
            pct9.Image = Resources.question_mark_96;


        }

        private void ButtonCommon(object sender, EventArgs e)
        {
            UpdateRound(sender);
        }

        void CleanArray(byte[,] bytes)
        {
            for(byte i=0;i<3;i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    bytes[i, j] = 0;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lblPlay.Text = Player1;
            lblFinalWiner.Text = "Winner";
            RestPictures();
            LockedPictures(true);
            GameOver = false;
            CleanArray(resultPlayer1);
            CleanArray(resultPlayer2);
            PlayCounter = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255, 255);
            Pen pen = new Pen(Color.White);
            pen.Width = 13;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 585, 65, 585, 650);
            e.Graphics.DrawLine(pen, 820, 65, 820, 650);

            e.Graphics.DrawLine(pen, 300, 240, 1100, 240);
            e.Graphics.DrawLine(pen, 300, 435, 1100, 435);

        }

    }
}
