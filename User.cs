using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
