using System.Collections.Generic;

namespace NikeFrontend.Data
{
    public class CityRootobject
    {
        public List<CityModel> data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }

    public class SingleCityRootobject
    {
        public CityModel data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }

    public class CityModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string createDate { get; set; }
        public bool active { get; set; }
        public List<District> districts { get; set; }
    }

    public class District
    {
        public int id { get; set; }
        public int cityId { get; set; }
        public string name { get; set; }
        public List<Village> villages { get; set; }
    }

    public class Village
    {
        public int id { get; set; }
        public int districtId { get; set; }
        public string name { get; set; }
    }
}