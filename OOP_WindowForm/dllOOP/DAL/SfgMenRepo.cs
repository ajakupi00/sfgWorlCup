﻿using dllOOP.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.DAL
{
    public class SfgMenRepo : ISfg
    {
        private const string NATIONAL_TEAMS_ENDPOINT = @"https://world-cup-json-2018.herokuapp.com/teams/results";
        private const string MATCH_ENDPOINT = @"https://world-cup-json-2018.herokuapp.com/matches/country?fifa_code=";


        public static List<T> DeserializeObject<T>(RestResponse<T> odgovorPodaci)
        {
            return (List<T>)JsonConvert.DeserializeObject(odgovorPodaci.Content, typeof(List<T>), new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public Task<RestResponse<Match>> GetMatches(NationalTeam team)
        {
            var apiKlijent = new RestClient(MATCH_ENDPOINT + team.FifaCode);
            return apiKlijent.ExecuteAsync<Match>(new RestRequest());
        }

        public Task<RestResponse<NationalTeam>> GetNationalTeams()
        {
            var apiKlijent = new RestClient(NATIONAL_TEAMS_ENDPOINT);
            return apiKlijent.ExecuteAsync<NationalTeam>(new RestRequest());
        }

        public async Task<HashSet<Player>> GetPlayers(NationalTeam team)
        {
            RestResponse<Match> response = await GetMatches(team);
            List<Match> matches = SfgMenRepo.DeserializeObject(response);
            HashSet<Player> players = new HashSet<Player>();
            Match match = matches[0];
            if (match.HomeTeamCountry == team.Country)
            {
                for (int i = 0; i < match.HomeTeamStatistics.StartingEleven.Count; i++)
                {
                    players.Add(match.HomeTeamStatistics.StartingEleven[i]);
                }
                for (int i = 0; i < match.HomeTeamStatistics.Substitutes.Count; i++)
                {
                    players.Add(match.HomeTeamStatistics.Substitutes[i]);
                }
            }
            else
            {
                for (int i = 0; i < match.AwayTeamStatistics.StartingEleven.Count; i++)
                {
                    players.Add(match.AwayTeamStatistics.StartingEleven[i]);
                }
                for (int i = 0; i < match.AwayTeamStatistics.Substitutes.Count; i++)
                {
                    players.Add(match.AwayTeamStatistics.Substitutes[i]);
                }
            }

            return players;
        }


    }
}
