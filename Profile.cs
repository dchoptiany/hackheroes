using System;

public class Profile
{
	public Profile()
	{
		enum Gender
		{
			Male,
			Female
		}
		
		public string name {get; set;}
	    public uint age {get; set;}
		public float weight {get; set;}
		public float height {get; set;}
		public Gender gender {get; set;}

		Profile(string name, unsigned age, float weight, float height, Gender gender) 
				:name(name), age(age), weight(weight), height(height), gender(gender) {}
	}
}