using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace FlashCards
{
    public class QuestionDB
    {
        readonly SQLiteAsyncConnection database;

        public QuestionDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Question>().Wait();
        }

        public Task<List<Question>> GetItemsAsync()
        {
            return database.Table<Question>().ToListAsync();
        }

        public Task<Question> GetItemAsync(int id)
        {
            return database.Table<Question>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Question item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Question item)
        {
            return database.DeleteAsync(item);
        }

        public Task<List<Question>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Question>("SELECT * FROM [Question]");
        }

        public void ClearQuestions()
        {
            database.QueryAsync<Question>("DROP TABLE [Question]");
        }
    }
}
