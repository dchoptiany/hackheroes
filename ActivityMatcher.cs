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
        public string imagePath { get; set; }
        public Sport(string _name, Participants _participants, Weather _weather, EffortLevel _effortLevel, string _imagePath)
        {
            name = _name;
            participants = _participants;
            weather = _weather;
            effortLevel = _effortLevel;
            imagePath = _imagePath;
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

        public static Sport currentSport;
        private static Participants latestParticipants;
        private static Weather latestWeather;
        private static EffortLevel latestEffortLevel;

        private static bool ParticipantsCondition(Participants sportValue, Participants userPreference)
        {
            return (sportValue == Participants.Any || userPreference == Participants.Any || userPreference == sportValue);
        }

        private static bool WeatherCondition(Weather sportValue, Weather userPreference)
        {
            return (sportValue == Weather.Any || userPreference == Weather.Any || userPreference == sportValue);
        }

        private static bool EffortLevelCondition(EffortLevel sportValue, EffortLevel userPreference)
        {
            return (sportValue == EffortLevel.Any || userPreference == EffortLevel.Any || userPreference == sportValue);
        }

        public static void Search(Participants participants, Weather weather, EffortLevel effortLevel)
        {
            latestParticipants = participants;
            latestWeather = weather;
            latestEffortLevel = effortLevel;

            if (approvedSports.Count < 1)
            {
                foreach (Sport sport in sports)
                {
                    if (ParticipantsCondition(sport.participants, participants) && WeatherCondition(sport.weather, weather) && EffortLevelCondition(sport.effortLevel, effortLevel))
                    {
                        approvedSports.Add(sport);
                    }
                }
            }
            if (approvedSports.Count >= 1)
            {
                currentSport = approvedSports[0];
                approvedSports.RemoveAt(0);
            }
            else
            {
                currentSport = null;
            }
        }
        public static void Search()
        {
            if (approvedSports.Count < 1)
            {
                foreach (Sport sport in sports)
                {
                    if (ParticipantsCondition(sport.participants, latestParticipants) && WeatherCondition(sport.weather, latestWeather) && EffortLevelCondition(sport.effortLevel, latestEffortLevel))
                    {
                        approvedSports.Add(sport);
                    }
                }
            }
            if (approvedSports.Count >= 1)
            {
                currentSport = approvedSports[0];
                approvedSports.RemoveAt(0);
            }
            else
            {
                currentSport = null;
            }
        }
    }
}