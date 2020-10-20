using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace app
{
    public enum Participants
    {
        Any,
        One,
        Two,
        More
    }

    public enum Weather
    {
        Any,
        Good,
        Bad
    }

    public enum EffortLevel
    {
        Any,
        Low,
        Medium,
        High
    }

    public class Sport
    {
        public string name { get; set; }
        public Participants participants { get; set; }
        public Weather weather { get; set; }
        public EffortLevel effortLevel { get; set; }

        public Sport(string _name, Participants _participants, Weather _weather, EffortLevel _effortLevel)
        {
            name = _name;
            participants = _participants;
            weather = _weather;
            effortLevel = _effortLevel;
        }

        public Sport()
        {
        }
    }

    class ActivityMatcher
    {
        public static List<Sport> sports;
        public static List<Sport> approvedSports;

        private static Participants latestParticipants;
        private static Weather latestWeather;
        private static EffortLevel latestEffortLevel;

        public static bool LoadSports()
        {
            try
            {
                sports = new List<Sport>();
                approvedSports = new List<Sport>();

                string[] JSON = File.ReadAllLines("..\\..\\Resources\\Sports.json");
                List<string> sportsJSON = new List<string>();

                for (int i = 0; i < JSON.Length; i += 6)
                {
                    sportsJSON.Add(JSON[i] + JSON[i + 1] + JSON[i + 2] + JSON[i + 3] + JSON[i + 4] + JSON[i + 5]);
                }

                foreach (string line in sportsJSON)
                {
                    sports.Add(JsonSerializer.Deserialize<Sport>(line));
                }

                return true;
            }
            catch(FileNotFoundException exception)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania aktywności. Wyszukiwarka aktywności nie będzie dostępna.", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public static string Search(Participants participants, Weather weather, EffortLevel effortLevel)
        {
            latestParticipants = participants;
            latestWeather = weather;
            latestEffortLevel = effortLevel;

            if (approvedSports.Count < 1)
            {
                foreach (Sport sport in sports)
                {
                    if ((sport.participants == Participants.Any || participants == Participants.Any || participants == sport.participants)
                        && (sport.weather == Weather.Any || weather == Weather.Any || weather == sport.weather) 
                        && (sport.effortLevel == EffortLevel.Any || effortLevel == EffortLevel.Any || effortLevel == sport.effortLevel))
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
        public static string Search()
        {
            if (approvedSports.Count < 1)
            {
                foreach (Sport sport in sports)
                {
                    if ((sport.participants == Participants.Any || latestParticipants == Participants.Any || latestParticipants == sport.participants)
                        && (sport.weather == Weather.Any || latestWeather == Weather.Any || latestWeather == sport.weather)
                        && (sport.effortLevel == EffortLevel.Any || latestEffortLevel == EffortLevel.Any || latestEffortLevel == sport.effortLevel))
                    {
                        approvedSports.Add(sport);
                    }
                }
            }
            if (approvedSports.Count >= 1)
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