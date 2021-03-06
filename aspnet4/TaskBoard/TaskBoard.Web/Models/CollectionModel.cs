﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoard.Web.Models
{
    public class CollectionModel<T>
    {
        public CollectionModel(IEnumerable<T> items, string href)
        {
            Items = items.ToList();
            Href = href;
        }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public IList<T> Items { get; set; }
    }
}
