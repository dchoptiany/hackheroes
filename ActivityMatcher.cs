using System;
using System.Collections.Generic;
using System.IO;

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
        public string name;
        public Participants participants;
        public Weather weather;
        public EffortLevel effortLevel;

        public Sport(string _name, Participants _participants, Weather _weather, EffortLevel _effortLevel)
        {
            name = _name;
            participants = _participants;
            weather = _weather;
            effortLevel = _effortLevel;
        }
    }

    static class ActivityMatcher
    {
        public readonly static List<Sport> sports = LoadSports();
        public static List<Sport> approvedSports;

        public static List<Sport> LoadSports()
        {
            List<Sport> sportsList = new List<Sport>();

            using (StreamReader loading = new StreamReader("..\\..\\Resources\\Sports"))
            {
                approvedSports = new List<Sport>();

                string name;
                Participants participants = 0;
                Weather weather = 0;
                EffortLevel effortLevel = 0;

                string line;
                string[] arr = new string[4];

                while (!loading.EndOfStream)
                {
                    name = loading.ReadLine();

                    line = loading.ReadLine();
                    arr = line.Split(' ');

                    switch(arr[0])
                    {
                        case "0":
                            participants = Participants.Any;
                            break;
                        case "1":
                            participants = Participants.One;
                            break;
                        case "2":
                            participants = Participants.Two;
                            break;
                        case "3":
                            participants = Participants.More;
                            break;
                    }
                 
                    switch(arr[1])
                    {
                        case "0":
                            weather = Weather.Any;
                            break;
                        case "1":
                            weather = Weather.Good;
                            break;
                    }

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

                    sportsList.Add(new Sport(name, participants, weather, effortLevel));
                    
                    ///// DEBUG ONLY //////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("nowy sport: " + name + " " + participants + " " + weather + " " + effortLevel);
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
            return sportsList;
        }

        public static string Search(Participants participants, Weather weather, EffortLevel effortLevel)
        {
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
    }
}