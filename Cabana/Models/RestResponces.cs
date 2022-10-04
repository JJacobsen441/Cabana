﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cabana.Models
{
    public class RestResponces
    {
    }
    public class Entry1 { public string name { get; set; } }
    public class Entry2 { public string m_name { get; set; } }

    /*
     * I could have used inheritance also
     * */
    public interface IResult { string Error { get; set; } }

    public class Result1 : IResult
    {
        public List<Entry1> members { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }

    public class Result2 : IResult
    {
        public string name { get; set; }
        public List<Entry2> movies { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }








    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }






    public class Result
    {
        public object poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public object backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
    }

    public class Movies
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }






    public class Genres
    {
        public List<Genre> genres { get; set; }
    }
    





    public class ProductionCompany
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class ProductionCountry
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class SpokenLanguage
    {
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }

    public class Movie
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public object poster_path { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }



}