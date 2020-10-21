using System.Collections.Generic;

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
        public static CurrentWeather currentWeather = new CurrentWeather();

        private static Participants latestParticipants;
        private static Weather latestWeather;
        private static EffortLevel latestEffortLevel;

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