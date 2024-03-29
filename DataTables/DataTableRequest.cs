﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MushroomWebsite.DataTables
{
    public class Order
    {
        public int Column { get; set; }

        public string Dir { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }

        public bool IsRegex { get; set; }
    }
    public class Column
    {
        public string Data { get; set; }

        public string Name { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public Search Search { get; set; }
    }

    public class DataTableRequest
    {
        public int Draw { get; set; }

        public IEnumerable<Column> Columns { get; set; }

        public IEnumerable<Order> Order { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public Search Search { get; set; }
    }
}
