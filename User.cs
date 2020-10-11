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
		public string name { get; set; }
		public byte age { get; set; }
		public float weight { get; set; }
		public uint height { get; set; }
		public Gender gender { get; set; }

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