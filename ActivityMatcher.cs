using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace app
{
    public enum Condition
    {
        Yes,
        No,
        Both
}

    public enum EffortLevel
    {
        Undefined,
        Low,
        Medium,
        High
    }

    public class Sport
    {
        public string name;
        public bool teamSport;
        public bool weatherDependent;
        public EffortLevel effortLevel;

        public Sport(string _name, bool _teamSport, bool _weatherDependent, EffortLevel _effortLevel)
        {
            name = _name;
            teamSport = _teamSport;
            weatherDependent = _weatherDependent;
            effortLevel = _effortLevel;
        }
    }

    static class ActivityMatcher
    {
        public readonly static List<Sport> sports = LoadSports();
        public static List<Sport> approvedSports;

        private static bool Compare(Condition lhs, bool rhs)
        {
            if (lhs == Condition.No && rhs || (lhs == Condition.Yes && !rhs))
            {
                return false;
            }
            return true;
        }

        public static List<Sport> LoadSports()
        {
            List<Sport> sportsList = new List<Sport>();

            using (StreamReader loading = new StreamReader("..\\..\\Resources\\Sports"))
            {
                approvedSports = new List<Sport>();

                string name;
                bool teamSport;
                bool weatherDependent;
                EffortLevel effortLevel = 0;

                string line;
                string[] arr = new string[4];

                while (!loading.EndOfStream)
                {
                    name = loading.ReadLine();

                    line = loading.ReadLine();
                    arr = line.Split(' ');

                    teamSport = Convert.ToBoolean(arr[0]);
                    weatherDependent = Convert.ToBoolean(arr[1]);
                    switch(arr[2])
                    {
                        case "0":
                            effortLevel = EffortLevel.Low;
                            break;
                        case "1":
                            effortLevel = EffortLevel.Medium;
                            break;
                        case "2":
                            effortLevel = EffortLevel.High;
                            break;
                    }

                    sportsList.Add(new Sport(name, teamSport, weatherDependent, effortLevel));
                    
                    ///// DEBUG ONLY //////////
                    Console.WriteLine("nowy sport: " + name + " " + teamSport + " " + weatherDependent + " " + effortLevel);
                    /////////////////////////////
                }
            }
            return sportsList;
        }

        public static string Search(Condition teamSport, Condition weatherConditions, EffortLevel effortLevel)
        {
            if (approvedSports.Count < 1)
            {
                foreach (Sport sport in sports)
                {
                    if (Compare(teamSport, sport.teamSport) && Compare(weatherConditions, sport.weatherDependent) && (effortLevel == EffortLevel.Undefined || effortLevel == sport.effortLevel)) 
                    {
                        approvedSports.Add(sport);
                    }
                }
            }
            if(approvedSports.Count >= 1)
            {
                string result = approvedSports[0].name;
                approvedSports.RemoveAt(0);
                return result;
            }
            else
            {
                return "";
            }
        }
    }
}