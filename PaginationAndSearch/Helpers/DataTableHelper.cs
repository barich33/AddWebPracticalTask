using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PaginationAndSearch.Helpers
{
    public class DataTableHelper
    {
        //Datatable results for pagination
        public class Result<T>
        {
            [JsonProperty("draw")]
            public int Draw { get; set; }
            [JsonProperty("recordsTotal")]
            public int RecordsTotal { get; set; }
            [JsonProperty("recordsFiltered")]
            public int RecordsFiltered { get; set; }
            [JsonProperty("data")]
            public IEnumerable<T> Data { get; set; }
            [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
            public string Error { get; set; }
        }

        public class Parameters
        {
            public int Draw { get; set; }
            public Column[] Columns { get; set; }
            public Order[] Order { get; set; }
            public int Start { get; set; }
            public int Length { get; set; }
            public Search Search { get; set; }
            public string SortOrder => Columns != null && Order != null && Order.Length > 0
                ? (Columns[Order[0].Column].Data +
                   (Order[0].OrderBy == OrderBy.Desc ? " " + Order[0].OrderBy : string.Empty))
                : null;
            public IEnumerable<string> AdditionalValues { get; set; }

            public static implicit operator HttpContent(Parameters v)
            {
                throw new NotImplementedException();
            }
        }

        public class Column
        {
            public string Data { get; set; }
            public string Name { get; set; }
            public bool Searchable { get; set; }
            public bool Orderable { get; set; }
            public Search Search { get; set; }
        }

        public class Order
        {
            public int Column { get; set; }
            public OrderBy OrderBy { get; set; }
        }

        public enum OrderBy
        {
            Asc,
            Desc
        }

        public class Search
        {
            public string Value { get; set; }
            public bool Regex { get; set; }
        }
    }
}