using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZQY.Models;
using PagedList;
namespace ZQY.ViewModels
{
    public class EveryIndexViewModel
    {

        //public IQueryable<Every> Everies { get; set; }
        public IPagedList<Every> Everies { get; set; }
        public string Search { get; set; }
        public IEnumerable<SeasonWithCount> SeasWithCount { get; set; }
        public string Season { get; set; }

       
        public IEnumerable<SelectListItem> SeaFilterItems
        {
            get
            {
                var allSeas = SeasWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.SeasonName,
                    Text = cc.SeaNameWithCount
                });
                return allSeas;
            }
        }
        public string SortBy { get; set; }
       public Dictionary<string, string> Sorts { get; set; }




    }
    public class SeasonWithCount
    {
        public int EveryCount { get; set; }
        public string SeasonName { get; set; }
        public string SeaNameWithCount
        {
            get
            {
                return SeasonName + " (" + EveryCount.ToString() + ")";
            }
        }
    }
}
    

