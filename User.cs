namespace app
{
	public enum Gender
	{
		Male,
		Female
	}

	public class User
	{
		///  Body section
		public string name { get; set; }
		public byte age { get; set; }
		public float weight { get; set; }
		public uint height { get; set; }
		public Gender gender { get; set; }

		public float BMI;

		public float activityLevel { get; set; }
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
	}
}