using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        //Drawing player 1 player 2 and ball assests
        Rectangle player1 = new Rectangle(10, 170, 10, 60); //left
        Rectangle player2 = new Rectangle(430, 170, 10, 60); //right
        Rectangle ball = new Rectangle(295, 195, 10, 10); //ball

        //setting the player scores to start at 0
        int player1Score = 0;
        int player2Score = 0;

        //setting the player and ball properties
        int playerSpeed = 4;
        int ballXSpeed = -6;
        int ballYSpeed = -6;

        //bool to check key pressed
        bool wPressed = false;
        bool sPressed = false;
        bool upPressed = false;
        bool downPressed = false;

        //setting up the brush
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        public Form1()
        {
            InitializeComponent();
        }

        //when key is pressed
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
            }
        }

        //when key is 
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X = ball.X + ballXSpeed;
            ball.Y = ball.Y + ballYSpeed; 

            //check if the ball hits the top or bottom
            if(ball.Y <= 0 || ball.Y >= this.Height - 10)
            {
                ballYSpeed = - ballYSpeed;
            }

            //move player 1 left side player
            if(wPressed == true && player1.Y > 0)
            {
                player1.Y = player1.Y - playerSpeed;
            }
            if(sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y = player1.Y + playerSpeed;   
            }

            //move player 2 right side
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y = player2.Y - playerSpeed;
            }
            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y = player2.Y + playerSpeed;
            }

            //check if the ball hit the player1
            if (player1.IntersectsWith(ball))
            {
                ballXSpeed = - ballXSpeed;
                ball.X = player1.X + player1.Width;
            }

            //check if the ball hit the player2
            if (player2.IntersectsWith(ball))
            {
                ballXSpeed = -ballXSpeed;
                ball.X = player2.X - player2.Width;
            }

            //check if the ball goes of the left side
            if (ball.X <= 0)
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                //reset ball position
                ball.X = 295;
                ball.Y = 195;

                //reset player position
                player1.X = 10;
                player1.Y = 170;
                player2.X = 430;
                player2.Y = 170;

                Random randGen = new Random();
                int randValue = randGen.Next(1, 3);

                if (randValue == 1)
                {
                    ballYSpeed = -6;
                }
                else
                {
                    ballYSpeed = 6;
                }
            }

            //check if the ball goes of the right side
            if (ball.X >= this.Width)
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                //reset ball position
                ball.X = 295;
                ball.Y = 195;

                //reset player position
                player1.X = 10;
                player1.Y = 170;
                player2.X = 430;
                player2.Y = 170;

                Random randGen = new Random();
                int randValue = randGen.Next(1, 3);

                //random start for the ball
                if (randValue == 1)
                {
                    ballYSpeed = -6;
                }
                else
                {
                    ballYSpeed = 6;
                }
            }

            //check for a winner
            if (player1Score == 3)
            {
                winLabel.Text = "Player 1 wins";
                gameTimer.Stop();
            }

            if (player2Score == 3)
            {
                winLabel.Text = "Player 2 wins";
                gameTimer.Stop();
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
        }
    }
}
