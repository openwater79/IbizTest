using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace IbizTest.Storage.Entities
{
    public class ProductEntity : TableEntity
    {
        public ProductEntity() { }

        public ProductEntity(string id, string name)
        {
            PartitionKey = RowKey = Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
