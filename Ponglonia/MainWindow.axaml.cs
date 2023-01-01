using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;

namespace Ponglonia
{
    public partial class MainWindow : Window
    {
        private int screenWidth;
        private int screenHeight;
        private int paddleWidth;
        private int paddleHeight;
        private int ballX;
        private int ballY;
        private int ballXVelocity;
        private int ballYVelocity;
        private int player1X;
        private int player1Y;
        private int player2X;
        private int player2Y;
        private bool player1Up;
        private bool player1Down;
        private bool player2Up;
        private bool player2Down;
        private int player1Score;
        private int player2Score;
        private string ballChar;
        private string paddleChar;
        private string blankChar;
        private int ballWidth;
        private int ballHeight;
        private bool isSecondPlayerCPU;
        
        public MainWindow()
        {
            InitializeComponent();
            
            screenWidth = 80;
            screenHeight = 60;
            paddleWidth = 3;
            paddleHeight = 6;
            ballX = screenWidth / 2;
            ballY = screenHeight / 2;
            ballXVelocity = 1;
            ballYVelocity = 1;
            player1X = 0;
            player1Y = screenHeight / 2 - paddleHeight / 2;
            player2X = screenWidth - paddleWidth;
            player2Y = screenHeight / 2 - paddleHeight / 2;
            player1Up = false;
            player1Down = false;
            player2Up = false;
            player2Down = false;
            player1Score = 0;
            player2Score = 0;
            ballChar = "o";
            paddleChar = "|";
            blankChar = " ";
            ballWidth = 1;
            ballHeight = 1;
            isSecondPlayerCPU = true;
            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += (sender, e) =>
            {
                // Update the game state
                ballX += ballXVelocity;
                ballY += ballYVelocity;

                if (player1Up)
                {
                    player1Y--;
                }
                if (player1Down)
                {
                    player1Y++;
                }
                if (player2Up)
                {
                    player2Y--;
                }
                if (player2Down)
                {
                    player2Y++;
                }
                if (ballX <= 0)
                {
                    player2Score++;
                    ballX = screenWidth / 2;
                    ballY = screenHeight / 2;
                    ballXVelocity = 1;
                }
                if (ballX >= screenWidth - 1)
                {
                    player1Score++;
                    ballX = screenWidth / 2;
                    ballY = screenHeight / 2;
                    ballXVelocity = -1;
                }
                if (ballY <= 0 || ballY >= screenHeight - 1)
                {
                    ballYVelocity *= -1;
                }
                if (ballX == player1X + paddleWidth && ballY >= player1Y && ballY < player1Y + paddleHeight)
                {
                    ballXVelocity *= -1;
                }
                if (ballX == player2X - 1 && ballY >= player2Y && ballY < player2Y + paddleHeight)
                {
                    ballXVelocity *= -1;
                }

                if (isSecondPlayerCPU)
                {
                    /*
                    // Calculate the distance between the ball and the center of the AI paddle
                    int paddleCenterY = player2Y + paddleHeight / 2;
                    int distanceToPaddleCenter = ballY - paddleCenterY;

                    // Set the AI's speed based on the distance to the ball
                    int speed = 0;
                    if (distanceToPaddleCenter < -5)
                    {
                        speed = -1;
                    }
                    else if (distanceToPaddleCenter > 5)
                    {
                        speed = 1;
                    }

                    // Update the AI's paddle position
                    player2Y += speed;
                    */
                    
                    // Predict the trajectory of the ball based on its current velocity and position
                    int predictedX = ballX + ballXVelocity;
                    int predictedY = ballY + ballYVelocity;
    
                    // Move the second player's paddle to intercept the ball at the predicted point of impact
                    player2Y = predictedY - paddleHeight / 2;

                    /*
                    int ballCenterY = ballY + ballHeight / 2;
                    int paddleCenterY = player2Y + paddleHeight / 2;

                    if (ballCenterY > paddleCenterY)
                    {
                        player2Y++;
                    }
                    else if (ballCenterY < paddleCenterY)
                    {
                        player2Y--;
                    }
                    */
                }

                // Redraw the game on the canvas
                DrawGame(canvas);
            };
            timer.Start();

            this.KeyDown += (sender, e) =>
            {
                if (e.Key == Key.W)
                {
                    player1Up = true;
                }
                if (e.Key == Key.S)
                {
                    player1Down = true;
                }
                if (e.Key == Key.Up)
                {
                    player2Up = true;
                }
                if (e.Key == Key.Down)
                {
                    player2Down = true;
                }
            };

            this.KeyUp += (sender, e) =>
            {
                if (e.Key == Key.W)
                {
                    player1Up = false;
                }
                if (e.Key == Key.S)
                {
                    player1Down = false;
                }
                if (e.Key == Key.Up)
                {
                    player2Up = false;
                }
                if (e.Key == Key.Down)
                {
                    player2Down = false;
                }
            };
        }
        
        private void DrawGame(Canvas canvas)
        {
            canvas.Children.Clear();

            // Draw the ball
            Ellipse ball = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.White
            };
            Canvas.SetLeft(ball, ballX * 10);
            Canvas.SetTop(ball, ballY * 10);
            canvas.Children.Add(ball);

            // Draw the paddles
            Rectangle player1Paddle = new Rectangle
            {
                Width = 30,
                Height = 60,
                Fill = Brushes.White
            };
            Canvas.SetLeft(player1Paddle, player1X * 10);
            Canvas.SetTop(player1Paddle, player1Y * 10);
            canvas.Children.Add(player1Paddle);

            Rectangle player2Paddle = new Rectangle
            {
                Width = 30,
                Height = 60,
                Fill = Brushes.White
            };
            Canvas.SetLeft(player2Paddle, player2X * 10);
            Canvas.SetTop(player2Paddle, player2Y * 10);
            canvas.Children.Add(player2Paddle);
        }
    }
}