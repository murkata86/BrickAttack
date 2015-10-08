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
		static int[,] brickWall = new int[Console.WindowWidth + 1, Console.WindowHeight + 1];
		static bool isSpacePressed = false;
		static bool isWon = false;

		static int currentDirectionX;
		static int currentDirectionY;
		static int index = 3;
		static int result = 0;

		static int padX;
		static int padY = 48;
		static int padLength = 9;

		static void Settings()
		{
			Console.SetWindowSize(69, 49);
			Console.SetBufferSize(70, 50);
			Console.Title = "Brick Attack";
			ballX = Console.WindowWidth / 2 + (padLength / 2);
			ballY = padY - 1;

			horizontalDirection = new int[2] { -1, 1 };
			verticalDirection = new int[2] { -1, 1 };

			currentDirectionX = 0;
			currentDirectionY = 0;

			padX = Console.WindowWidth / 2;
			if (index == 3)
			{
				brickWall = new int[Console.WindowWidth + 1, Console.WindowHeight + 1];
			}
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
			if (ballY + 1 == padY && ballX >= padX && ballX <= padX + padLength)
			{
				ChangeYDirection();

			}
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
			OutOfPad();
			Console.SetCursorPosition(0, 0);
			Console.Write("Lives: ");
			Console.Write(index); //TODO: keep track on remaining lives
			Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 0);
			Console.Write("Result: {0}", result); //TODO: keep track on result
			Console.SetCursorPosition(0, 1);
			var line = string.Concat(Enumerable.Repeat("-", Console.WindowWidth));
			Console.WriteLine(line);
		}

		static void BrickCollision()
		{

			if (brickWall[ballX, ballY - 1] != 0)
			{

				if (brickWall[ballX, ballY - 1] == 1)
				{
					result += 10;
				}
				else
				{
					result += 30;
				}
				brickWall[ballX, ballY - 1] = 0;
				Console.SetCursorPosition(ballX, ballY - 1);
				Console.Write(" ");
				ChangeYDirection();
			}

			if (ballY + 1 <= Console.WindowHeight - 2 && brickWall[ballX, ballY + 1] != 0)
			{
				if (brickWall[ballX, ballY + 1] == 1)
				{
					result += 10;
				}
				else
				{
					result += 30;
				}
				brickWall[ballX, ballY + 1] = 0;
				Console.SetCursorPosition(ballX, ballY + 1);
				Console.Write(" ");
				ChangeYDirection();
			}

			if (ballY - 1 >= 0 && ballX - 1 >= 0 && brickWall[ballX - 1, ballY] != 0)
			{
				if (brickWall[ballX - 1, ballY] == 1)
				{
					result += 10;
				}
				else
				{
					result += 30;
				}
				brickWall[ballX - 1, ballY] = 0;
				Console.SetCursorPosition(ballX - 1, ballY);
				Console.Write(" ");
				ChangeXDirection();
			}

			if (ballX + 1 <= Console.WindowWidth - 2 && brickWall[ballX + 1, ballY] != 0)
			{
				if (brickWall[ballX + 1, ballY] == 1)
				{
					result += 10;
				}
				else
				{
					result += 30;
				}

				brickWall[ballX + 1, ballY] = 0;
				Console.SetCursorPosition(ballX + 1, ballY);
				Console.Write(" ");
				ChangeXDirection();
			}

		}

		static void InitializeBricks()
		{
			for (int i = 5; i < 63; i++)
			{
				for (int j = 5; j < 20; j++)
				{
					if (j % 3 == 0)
					{
						brickWall[i, j] = 3;
					}
					else
					{
						brickWall[i, j] = 1;
					}
				}
			}
		}

		static void DisplayBricks()
		{
			for (int i = 0; i < brickWall.GetLength(0); i++)
			{
				for (int j = 0; j < brickWall.GetLength(1); j++)
				{
					if (brickWall[i, j] != 0)
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

		static void OutOfPad()
		{

			if (index == 0)
			{
				Console.Clear();
				Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
				Console.Write("GAME OVER");
				Thread.Sleep(2000);
				Console.Clear();
				index = 3;
				isSpacePressed = false;
				Settings();
				Engine();

			}

			if (ballY == padY + 1)
			{
				index--;

				Console.Clear();
				isSpacePressed = false;
				Settings();
				Engine();

			}
		}

		static bool SpacePressed()
		{

			ConsoleKeyInfo space = Console.ReadKey();

			if (space.Key == ConsoleKey.Spacebar)
			{
				isSpacePressed = true;
			}

			return isSpacePressed;
		}

		static bool IsWon()
		{

			for (int i = 5; i < 63; i++)
			{
				for (int j = 5; j < 20; j++)
				{
					if (brickWall[i, j] != 0)
					{
						isWon = false;
					}
					else
					{

						isWon = true;
					}
				}
			}

			return isWon;
		}

		static void Main()
		{
			Settings();
			Engine();
		}

		static void Engine()
		{
			if (index == 3)
			{
				InitializeBricks();
			}
			DisplayBricks();
			DisplayPad();
			PrintPlayerData();

			while (!isSpacePressed)
			{
				SpacePressed();
			}

			int speed = 0;
			while (!IsWon())
			{
				if (speed % 2 == 0)
				{

					BallMovement();
					BrickCollision();
					CollisionWithWall();
					CollisionWithPadd();
					PrintPlayerData();
				}

				MovePad();
				Thread.Sleep(40);
				speed++;
			}
		}
	}
}
