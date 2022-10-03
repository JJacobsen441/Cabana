using Cabana.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Cabana.Statics
{
    public class RestHelper
    {
        public static Movie MovieGET(long id)
        {
            HttpResponseMessage res = Send(
                    HttpMethod.Get,
                    "",
                    "https://api.themoviedb.org",
                    "3/movie/" + id,
                    "",
                    "",//accept
                    "application/json;charset=utf-8",//contenttype
                    "",//api_key
                    "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NTdiYWI5ZGE2NjY1MmVkYjgyZGEzNDc1NGE2OGY2NyIsInN1YiI6IjYzMzJhMjQyY2VkZTY5MDA3ZWFmNWUwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.8p1gCBT8d0UpiKZIWARdosOzTRKxH3tDiAM7UclbSkw"//token
                    );

            string s_res = res.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrWhiteSpace(s_res))
            {
                Movie _res = JsonConvert.DeserializeObject<Movie>(s_res);
                return _res;
            }
            else
            {
                throw new Exception();
            }
        }

        public static Movies MoviesGET(string _q)
        {
            if (_q.IsNull())
                throw new Exception();

            _q = HttpUtility.UrlEncode(_q);

            HttpResponseMessage res = Send(
                    HttpMethod.Get,
                    "",
                    "https://api.themoviedb.org",
                    "3/search/movie",
                    "query=" + _q,
                    "",//accept
                    "application/json;charset=utf-8",//contenttype
                    "",//api_key
                    "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NTdiYWI5ZGE2NjY1MmVkYjgyZGEzNDc1NGE2OGY2NyIsInN1YiI6IjYzMzJhMjQyY2VkZTY5MDA3ZWFmNWUwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.8p1gCBT8d0UpiKZIWARdosOzTRKxH3tDiAM7UclbSkw"//token
                    );

            string s_res = res.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrWhiteSpace(s_res))
            {
                Movies _res = JsonConvert.DeserializeObject<Movies>(s_res);
                return _res;
            }
            else
            {
                throw new Exception();
            }
        }

        public static Genres GenresGET()
        {
            HttpResponseMessage res = Send(
                    HttpMethod.Get,
                    "",
                    "https://api.themoviedb.org",
                    "3/genre/movie/list",
                    "",
                    "",//accept
                    "application/json;charset=utf-8",//contenttype
                    "",//api_key
                    "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NTdiYWI5ZGE2NjY1MmVkYjgyZGEzNDc1NGE2OGY2NyIsInN1YiI6IjYzMzJhMjQyY2VkZTY5MDA3ZWFmNWUwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.8p1gCBT8d0UpiKZIWARdosOzTRKxH3tDiAM7UclbSkw"//token
                    );

            string s_res = res.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrWhiteSpace(s_res))
            {
                Genres _res = JsonConvert.DeserializeObject<Genres>(s_res);
                return _res;
            }
            else
            {
                throw new Exception();
            }
        }

        public static Movies MoviesGET()
        {
            HttpResponseMessage res = Send(
                    HttpMethod.Get,
                    "",
                    "https://api.themoviedb.org",
                    "3/discover/movie",
                    "primary_release_date.gte=2014-09-15&primary_release_date.lte=2014-10-22",
                    "",//accept
                    "application/json;charset=utf-8",//contenttype
                    "",//api_key
                    "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NTdiYWI5ZGE2NjY1MmVkYjgyZGEzNDc1NGE2OGY2NyIsInN1YiI6IjYzMzJhMjQyY2VkZTY5MDA3ZWFmNWUwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.8p1gCBT8d0UpiKZIWARdosOzTRKxH3tDiAM7UclbSkw"//token
                    );

            string s_res = res.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrWhiteSpace(s_res))
            {
                Movies _res = JsonConvert.DeserializeObject<Movies>(s_res);
                return _res;
            }
            else
            {
                throw new Exception();
            }
        }

        /*public static Movies ___MoviesGET()
        {
            HttpResponseMessage res = Send(
                    HttpMethod.Get,
                    "",
                    "https://api.themoviedb.org",
                    "3/movie/76341",
                    "",
                    "",//accept
                    "application/json;charset=utf-8",//contenttype
                    "",//api_key
                    "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NTdiYWI5ZGE2NjY1MmVkYjgyZGEzNDc1NGE2OGY2NyIsInN1YiI6IjYzMzJhMjQyY2VkZTY5MDA3ZWFmNWUwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.8p1gCBT8d0UpiKZIWARdosOzTRKxH3tDiAM7UclbSkw"//token
                    );
            return null;
        }/**/
              
        /*public static Movies GetMoviePOST(string secr, string resp)
        {
            using (HttpClient hc = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {
                        "secret",
                        secr
                    },
                    {
                        "response",
                        resp
                    }
                };
                FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = hc.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
                string s_response = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(s_response))
                {
                    Movies _res = JsonConvert.DeserializeObject<Movies>(s_response);
                    return _res;
                }
                else
                {
                    return null;
                }
            }
        }/**/

        public static HttpResponseMessage Send(HttpMethod method,
            string _json,
            string _base,
            string _path,
            string _params,
            string _accept,
            string _contenttype,
            string _apikey,
            string _token
                          
            )
        {
            using (var client = new HttpClient())
            {
                _params = _params != "" ? "?" + _params : "";

                _apikey = _params == "" && _apikey != "" ? "?api_key=" + _apikey :
                          _params != "" && _apikey != "" ? "&api_key=" + _apikey : "";

                client.BaseAddress = new Uri(_base);
                HttpRequestMessage req = new HttpRequestMessage(method, "/" + _path + _params + _apikey);
                    
                if (_json != "")
                    req.Content = new StringContent(_json, Encoding.UTF8, _contenttype);
                                        
                if (_accept != "")
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_accept));//ACCEPT header application/x-www-form-urlencoded

                if (_token != "")
                    req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                //if (content_type != "")
                //    req.Content.Headers.ContentType = new MediaTypeHeaderValue(content_type);

                HttpResponseMessage response = client.SendAsync(req).Result;
                
                return response;
            }
        }
    }
}