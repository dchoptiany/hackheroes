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
		public string name;
		public byte age;
		public float weight;
		public uint height;
		public Gender gender;

		public float BMI;

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

		public string getData()
        {
			return age + " " + weight + " " + height + " " + gender + " " + calories + " " + protein + " " + carbohydrates + " " + fat;
		}
	}
}