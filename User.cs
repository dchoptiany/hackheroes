using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace app
{
	public enum Gender
	{
		Male,
		Female
	}

	public class User
	{
		public static List<User> users;
		public static int currentUserIndex;
		
		///  Body section
		public string name { get; set; }
		public byte age { get; set; }
		public float weight { get; set; }
		public uint height { get; set; }
		public Gender gender { get; set; }

		public float BMI;

		public float activityLevel;
		public bool physicalJob;
		public uint trainingsInWeek;
		public uint dailyMovementLevel;

		///  Macro Section
		public int calories;
		public int protein;
		public int carbohydrates;
		public int fat;

		public User(string _name, byte _age, float _weight, uint _height, Gender _gender)
		{
			name = _name;
			age = _age;
			weight = _weight;
			height = _height;
			gender = _gender;
		}

		public User()
		{
		}

		public static void LoadUsers()
		{
			users = new List<User>();
			currentUserIndex = 0;

			try
			{
				string[] JSON = File.ReadAllLines("..\\..\\users.json");
				List<string> usersJSON = new List<string>();
				string userLine;

				for (int i = 0; i < JSON.Length; i += 7)
				{
					userLine = string.Empty;
					for (int line = 0; line < 7; line++)
                    {
						userLine += JSON[i + line];
					}
					usersJSON.Add(userLine);
				}

				foreach (string line in usersJSON)
				{
					User newUser = JsonSerializer.Deserialize<User>(line);
					User.users.Add(newUser);
				}
			}
			catch (FileNotFoundException exception)
			{
				MessageBox.Show("Wystąpił błąd podczas wczytywania profili.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				User.users.Add(new User("User", 18, 80f, 180, Gender.Male));
			}
		}
	}
}