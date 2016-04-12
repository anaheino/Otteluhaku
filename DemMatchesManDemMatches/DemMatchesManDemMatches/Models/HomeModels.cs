using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Varsinainen dataluokka. 
/// </summary>
namespace DemMatchesManDemMatches.Models

{
    public class MatchModels
    {
        List<Match> matches = new List<Match>();
        //matchgetteri
        public List<Match> getMatches { get; set; }

        //hakee toivotut ottelut. Jos käyttäjä on syöttänyt vain toisen ottelun päivämäärän, haetaan sen perusteella.
        // jos ei ole syöttänyt mitään korrektia arvoa, ladataan kaikki ottelut.
        public List<Match> searchMatch(String date1, String date2)
        {
            try
            {
                DateTime beginDate = Convert.ToDateTime(date1);
                DateTime endDate = Convert.ToDateTime(date2);
                List<Match> result = new List<Match>();
                List<Match> compared = getMatches;
                foreach (Match r in compared)
                {
                    if (r.MatchDate >= beginDate && r.MatchDate <= endDate) result.Add(r);
                }
                return result;
            }
            catch(Exception ){ }
            try
            {
                DateTime beginDate = Convert.ToDateTime(date1);
                List<Match> result = new List<Match>();
                List<Match> compared = getMatches;
                foreach (Match r in compared)
                {
                    if (r.MatchDate >= beginDate) result.Add(r);
                }
                return result;
            }
            catch(Exception) { }
            try
            {
                DateTime endDate = Convert.ToDateTime(date2);
                List<Match> result = new List<Match>();
                List<Match> compared = getMatches;
                foreach (Match r in compared)
                {
                    if (r.MatchDate <= endDate) result.Add(r);
                }
                return result;
            }
            catch (Exception) { }
            return getMatches;   
        }

        // kauhea spesifinen hirviöratkaisu joka pitäisi korjata paremmalla ajalla. Json.net rikkoo dictin sisäiset
        // dictit, joten vedin gettoratkaisun hätäfixiksi. Pitänee paremmalla ajalla tutustua siihen, kuinka tämä ongelma ratkaistaisiin.
        public String seekName(String a)
        {
            string[] ab = a.Split(':');
            string[] s = ab[2].Split(',');
            a = s[0];
            a = a.Remove(0, 2);
            a = a.Remove(a.Length - 1, 1);
            return a;
        }

        //luodaan ottelulista käymällä läpi  yksittäiset ottelutiedot parametri-dictistä ja lisätään ne listaan.
        public void addMatches(List<Dictionary<String, dynamic>> matsit)
        {   
            foreach (var m in matsit)
            {
                Match match = new Match();
                match.Id = (int)m["Id"];
                match.Round = m["Round"];
                match.RoundNumber = (int)m["RoundNumber"];
                match.MatchDate = Convert.ToDateTime(m["MatchDate"]);
                match.HomeTeam = seekName(m["HomeTeam"].ToString());
                match.AwayTeam = seekName(m["AwayTeam"].ToString());
                match.HomeGoals = (int)m["HomeGoals"];
                match.AwayGoals = (int)m["AwayGoals"];
                match.Status = (int)m["Status"];
                match.PlayedMinutes = (int)m["PlayedMinutes"];
                match.SecondHalfStarted = m["SecondHalfStarted"];
                match.GameStarted = Convert.ToDateTime(m["GameStarted"]);
                match.OnlyResultsAvailable = m["OnlyResultAvailable"];
                match.Season = (int)m["Season"];
                match.League = m["League"];
                matches.Add(match);
            }
            getMatches = matches;
        }
    }
        // eli luokka yksittäistä ottelua varten, omaa tarvittavat kentät ja vähän ylimääräisiä.
        public class Match
        {
            public int Id { get; set; }
            public String Round { get; set; }
            public int RoundNumber { get; set; }
            public DateTime MatchDate { get; set; }
            public string HomeTeam { get; set; }
            public string AwayTeam { get; set; }
            public int HomeGoals { get; set; }
            public int AwayGoals { get; set; }
            public int Status { get; set; }
            public int PlayedMinutes { get; set; }
            public String SecondHalfStarted { get; set; }
            public DateTime GameStarted { get; set; }
            public bool OnlyResultsAvailable { get; set; }
            public int Season { get; set; }
            public String Country { get; set; }
            public String League { get; set; }
        }
    }

