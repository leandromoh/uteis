using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Student : TableEntity
    {
        public Student() { }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public bool? IsActive { get; set; }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            var ConnectionString = "adasd";

            var manager = new TableStorageManager<Student>("teste", ConnectionString);

            var st = new Student();

            st.PartitionKey = "Student";
            st.Name = "name";
            st.RowKey = "asd";

            manager.InsertEntity(st).Wait();
            
            var itens = manager.RetrieveEntity().Result;

            Console.ReadLine();
        }
    }

    public class TableStorageManager<T> : ITableStorageManager<T> where T : TableEntity, new()
    {
        private CloudTable _table;

        public TableStorageManager(IOptions<AzureStorageOptions> storageOptions)
            : this(storageOptions.Value.OrderTableName, storageOptions.Value.ConnectionString)
        {

        }

        public TableStorageManager(string CloudTableName, string ConnectionString)
        {
            if (string.IsNullOrEmpty(CloudTableName))
                throw new ArgumentNullException(nameof(CloudTableName), "Table name can not be empty");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference(CloudTableName);
            var tableWasCreated = _table.CreateIfNotExistsAsync().Result;
        }

        public async Task<TableResult> InsertEntityAsync(T entity)
        {
            var insertOperation = TableOperation.Insert(entity);
            return await _table.ExecuteAsync(insertOperation);
        }

        public async Task<TableResult> UpdateEntityAsync(T entity)
        {
            var insertOrMergeOperation = TableOperation.Replace(entity);
            return await _table.ExecuteAsync(insertOrMergeOperation);
        }

        public async Task<TableResult> DeleteEntityAsync(T entity)
        {
            var DeleteOperation = TableOperation.Delete(entity);
            return await _table.ExecuteAsync(DeleteOperation);
        }

        public async Task<IList<TableResult>> DeleteEntitiesOlderThanAsync(DateTime date)
        {
            string filter = TableQuery.GenerateFilterConditionForDate(
                "Timestamp",
                QueryComparisons.LessThan,
                date);

            var elements = await RetrieveEntityAsync(filter);

            var deleteResults = await DeleteEntityInBatchAsync(elements);

            return deleteResults;
        }

        public async Task<IList<TableResult>> DeleteEntityInBatchAsync(List<T> elements)
        {
            // Split into chunks of 100 for batching
            List<List<T>> rowsChunked = elements
                .Where(x => x != null)
                .Select((x, index) => new { Index = index, Value = x })
                .GroupBy(x => x.Index / 100)
                .Select(group => group.Select(v => v.Value).ToList())
                .ToList();

            var tableResults = new List<TableResult>();

            // Delete each chunk of 100 in a batch
            foreach (List<T> rows in rowsChunked)
            {
                TableBatchOperation tableBatchOperation = new TableBatchOperation();
                rows.ForEach(x => tableBatchOperation.Add(TableOperation.Delete(x)));

                var batchResult = await _table.ExecuteBatchAsync(tableBatchOperation);
                tableResults.AddRange(batchResult);
            }

            return tableResults;
        }

        public async Task<List<T>> RetrieveEntityAsync(string query = null, CancellationToken ct = default)
        {
            var DataTableQuery = new TableQuery<T>();

            if (!string.IsNullOrEmpty(query))
                DataTableQuery = new TableQuery<T>().Where(query);

            var items = new List<T>();
            TableContinuationToken token = null;

            do
            {
                TableQuerySegment<T> seg = await _table.ExecuteQuerySegmentedAsync<T>(DataTableQuery, token);
                token = seg.ContinuationToken;
                items.AddRange(seg);
            }
            while (token != null && !ct.IsCancellationRequested);

            return items;
        }
    }
}
