using dllOOOP.Models;
using dllOOP.Models;
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
        public List<Match> GetMatches(NationalTeam team)
        {
            throw new NotImplementedException();
        }


        public List<Player> GetPlayers(NationalTeam team)
        {
            throw new NotImplementedException();
        }

        public static List<T> NewMethod<T>(RestResponse<T> odgovorPodaci)
        {
            return (List<T>)JsonConvert.DeserializeObject(odgovorPodaci.Content, typeof(List<T>));
        }

        public Task<RestResponse<NationalTeam>> GetNationalTeams()
        {
            var apiKlijent = new RestClient(NATIONAL_TEAMS_ENDPOINT);
            return apiKlijent.ExecuteAsync<NationalTeam>(new RestRequest());
        }
    }
}
