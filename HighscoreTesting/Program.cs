using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighscoreTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			long currentPoints = long.Parse(Console.ReadLine());

			string[] creatorNames = new string[10] { "Denislav", "Prinov", "Ivan", "Murtov", "Svetoslav", "Iliev", "Nikolay", "Rumenov", "Daniel", "Angelov"};
			int[] creatorPoints = new int[10] { 300, 280, 220, 180, 150, 110, 80, 70, 60, 50 };
			int[] creatorBricks = new int[10] { 35, 20, 26, 13, 15, 11, 8, 70, 60, 5 };
			string[] highscoreFile = new string[10];
			string highscoreFilePath = "highscore.ba";

			//checking if the Highscore file exists
			if (!File.Exists(highscoreFilePath))
			{
				//creating the highscore file and filling it with random data if the file does not exists
				using (var fileCreator = new FileStream(highscoreFilePath, FileMode.Create))
				{
					for (int i = 0; i < 10; i++)
					{
						string line = creatorNames[i] + "|" + creatorPoints[i] + "|" + creatorBricks[i] + "\r\n";
						byte[] buffer = Encoding.UTF8.GetBytes(line);
						fileCreator.Write(buffer, 0, buffer.Length);
					}
				}
			}
			//if the file exists check if the current score is greater than the scores in the file
			else
			{
				using (var highscoreReader = new StreamReader(highscoreFilePath))
				{
					for (int i = 0; i < 10; i++)
					{
						var line = highscoreReader.ReadLine();
						highscoreFile[i] = line.ToString();
					}

				}

				for (int i = 0; i < 10; i++)
				{
					string[] dataFromLine = highscoreFile[i].Split('|').ToArray();
					long points = long.Parse(dataFromLine[1]);

					if (currentPoints > points)
					{
						Console.Write("Please enter your name:");
						string name = Console.ReadLine();
					}
				}	
			}
		}
	}
}
