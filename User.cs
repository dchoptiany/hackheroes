using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
	public class User
	{
		enum Gender
		{
			Male,
			Female
		}

		private string name { get; set; }
		private byte age { get; set; }
		private float weight { get; set; }
		private float height { get; set; }
		private Gender gender { get; set; }

		User(string _name, byte _age, float _weight, float _height, Gender _gender)
		{
			name = _name;
			age = _age;
            weight = _weight;
            height = _height;
			gender = _gender;
		}
	}
}