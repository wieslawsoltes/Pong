using System;

namespace ConsolePong
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the game
            int screenWidth = 80;
            int screenHeight = 30;
            int paddleWidth = 5;
            int paddleHeight = 3;
            int ballX = screenWidth / 2;
            int ballY = screenHeight / 2;
            int ballXVelocity = 1;
            int ballYVelocity = 1;
            int player1X = 0;
            int player1Y = screenHeight / 2 - paddleHeight / 2;
            int player2X = screenWidth - paddleWidth;
            int player2Y = screenHeight / 2 - paddleHeight / 2;
            bool player1Up = false;
            bool player1Down = false;
            bool player2Up = false;
            bool player2Down = false;
            int player1Score = 0;
            int player2Score = 0;
            string ballChar = "o";
            string paddleChar = "|";
            string blankChar = " ";

            while (true)
            {
                // Draw the screen
                Console.Clear();
                for (int y = 0; y < screenHeight; y++)
                {
                    for (int x = 0; x < screenWidth; x++)
                    {
                        if (x == ballX && y == ballY)
                        {
                            Console.Write(ballChar);
                        }
                        else if (x == player1X && y >= player1Y && y < player1Y + paddleHeight)
                        {
                            Console.Write(paddleChar);
                        }
                        else if (x == player2X && y >= player2Y && y < player2Y + paddleHeight)
                        {
                            Console.Write(paddleChar);
                        }
                        else
                        {
                            Console.Write(blankChar);
                        }
                    }
                    Console.WriteLine();
                }

                // Update the game state
                ballX += ballXVelocity;
                ballY += ballYVelocity;
                
                ConsoleKeyInfo consoleKey;
                if (Console.KeyAvailable)
                {
                    consoleKey = Console.ReadKey();
                    if (consoleKey.Key == ConsoleKey.W)
                    {
                        player1Up = true;
                    }
                    if (consoleKey.Key == ConsoleKey.S)
                    {
                        player1Down = true;
                    }
                    if (consoleKey.Key == ConsoleKey.UpArrow)
                    {
                        player2Up = true;
                    }
                    if (consoleKey.Key == ConsoleKey.DownArrow)
                    {
                        player2Down = true;
                    }
                }
                else
                {
                    player1Up = false;
                    player1Down = false;
                    player2Up = false;
                    player2Down = false;
                }
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

                // Check for player input
                if (Console.KeyAvailable)
                {
                    consoleKey = Console.ReadKey();
                    if (consoleKey.Key == ConsoleKey.W)
                    {
                        player1Up = true;
                    }
                    if (consoleKey.Key == ConsoleKey.S)
                    {
                        player1Down = true;
                    }
                    if (consoleKey.Key == ConsoleKey.UpArrow)
                    {
                        player2Up = true;
                    }
                    if (consoleKey.Key == ConsoleKey.DownArrow)
                    {
                        player2Down = true;
                    }
                }
                else
                {
                    player1Up = false;
                    player1Down = false;
                    player2Up = false;
                    player2Down = false;
                }

                // Pause for a moment
                Thread.Sleep(100);
            }
        }
    }
}
