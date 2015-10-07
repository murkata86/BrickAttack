using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAttack
{
	class BrickAttack
	{
		static int ballX;
		static int ballY;

		static int[] horizontalDirection;
		static int[] verticalDirection;
		static int[,] brickWall;

		static int currentDirectionX;
		static int currentDirectionY;

		static int padX;
		static int padY = Console.WindowHeight - 1;
		static int padLength = 9;

		static void Settings()
		{
			Console.SetWindowSize(69, 49);
			Console.SetBufferSize(70, 50);
			Console.Title = "Brick Attack";

			ballX = Console.WindowWidth / 2;
			ballY = Console.WindowHeight / 2;

			horizontalDirection = new int[2] { -1, 1 };
			verticalDirection = new int[2] { -1, 1 };

			currentDirectionX = 0;
			currentDirectionY = 0;

			padX = Console.WindowWidth - 20;
			brickWall = new int[Console.WindowWidth, Console.WindowHeight];
		}
		static void BallMovement()
		{
			Console.SetCursorPosition(ballX, ballY);
			Console.Write(" ");
			ballX += horizontalDirection[currentDirectionX];
			ballY += verticalDirection[currentDirectionY];
			Console.SetCursorPosition(ballX, ballY);
			Console.Write("@");

		}
		static void CollisionWithWall()
		{
			if (ballX <= 1)
			{
				ChangeXDirection();
			}
			if (ballX >= Console.WindowWidth - 2)
			{
				ChangeXDirection();
			}
			if (ballY <= 2)
			{
				ChangeYDirection();
			}
			if (ballY >= Console.WindowHeight)
			{
				ChangeYDirection();
			}
		}
		static void CollisionWithPadd()
		{
			//if (ballY + 1 == padY && ballX >=pa)
			//{

			//}
		}
		static void ChangeXDirection()
		{
			if (currentDirectionX == 0)
			{
				currentDirectionX = 1;
			}
			else
			{
				currentDirectionX = 0;
			}
		}
		static void ChangeYDirection()
		{
			if (currentDirectionY == 0)
			{
				currentDirectionY = 1;
			}
			else
			{
				currentDirectionY = 0;
			}
		}
		static void DisplayPad()
		{
			for (int i = padX; i < padX + padLength; i++)
			{
				Console.SetCursorPosition(i, padY);
				Console.Write("#");
			}
		}
		static void MovePad()
		{
			while (Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey();

				while (Console.KeyAvailable)
				{
					Console.ReadKey();
				}

				if (padX > 0 && padX + padLength < Console.WindowWidth)
				{
					ExecuteKey(key);
				}
			}
		}
		static void ExecuteKey(ConsoleKeyInfo key)
		{
			if (key.Key == ConsoleKey.LeftArrow)
			{
				if (padX > 1)
				{
					padX--;
					Console.SetCursorPosition(padX + padLength, padY);
					Console.Write(" ");
					DisplayPad();
				}
			}
			if (key.Key == ConsoleKey.RightArrow)
			{
				if (padX + padLength < Console.WindowWidth - 1)
				{
					padX++;
					Console.SetCursorPosition(padX - 1, padY);
					Console.Write(" ");
					DisplayPad();
				}
			}
		}
		static void PrintPlayerData()
		{
			Console.SetCursorPosition(0, 0);
			Console.Write("Lives: ");
			Console.Write("= = ="); //TODO: keep track on remaining lives
			Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 0);
			Console.Write("Result: {0}", 5); //TODO: keep track on result
			Console.SetCursorPosition(0, 1);
			var line = string.Concat(Enumerable.Repeat("-", Console.WindowWidth));
			Console.WriteLine(line);
		}
		static void InitializeBricks()
		{
			for (int i = 5; i < 64; i++)
			{
				for (int j = 5; j < 20; j++)
				{
					brickWall[i, j] = 1;
				}
			}
		}

		static void DisplayBricks()
		{
			for (int i = 0; i < brickWall.GetLength(0); i++)
			{
				for (int j = 0; j < brickWall.GetLength(1); j++)
				{
					if (brickWall[i,j] != 0)
					{
						Console.SetCursorPosition(i, j);
						if (j % 3 == 0)
						{
							Console.Write("=");
						}
						else
						{
							Console.Write("*");
						}						
					}
				}
			}
		}

		static void Main()
		{
			Settings();
			Engine();
		}

		static void Engine()
		{
			InitializeBricks();
			DisplayBricks();
			DisplayPad();
			int speed = 0;
			while (true)
			{
				if (speed % 2 == 0)
				{
					CollisionWithWall();
					BallMovement();
				}
				PrintPlayerData();
				MovePad();
				Thread.Sleep(50);
				speed++;
			}
		}
	}
}
