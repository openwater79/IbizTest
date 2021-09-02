using IbizTest.Models;
using IbizTest.Storage.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbizTest.Storage
{
    public class ProductRepository
    {
        CloudTable _table;

        public ProductRepository()
        {
            var storageconn = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            var storageAcc = CloudStorageAccount.Parse(storageconn);
            var tblclient = storageAcc.CreateCloudTableClient(new TableClientConfiguration());
            _table = tblclient.GetTableReference("Product");
        }

        public async Task AddAsync(Product product)
        {
            var entity = new ProductEntity(product.Id, product.Name);

            var insertOperation = TableOperation.InsertOrMerge(entity);
            await _table.ExecuteAsync(insertOperation);
        }

        public IEnumerable<Product> Get()
        {
            var query = new TableQuery<ProductEntity>();
            var products = _table.ExecuteQuery(query);

            return products.Select(x => new Product() { Id = x.Id, Name = x.Name });
        }
    }
}
