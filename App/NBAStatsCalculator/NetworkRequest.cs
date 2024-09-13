using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace NBAStatsCalculator
{
    static class NetworkRequest
    {
        public static async Task GetNbaTeams()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://api.balldontlie.io/v1/teams";
                string token = "c7dcb5bd-ebe6-415e-8a23-0926c4490db0";
                client.DefaultRequestHeaders.Add("Authorization", token);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        // Lire le contenu de la réponse en tant que string
                        var responseBody = await response.Content.ReadAsStreamAsync();
                        Debug.WriteLine(responseBody);
                    }
                    else
                    {
                        Debug.WriteLine($"Erreur : {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
