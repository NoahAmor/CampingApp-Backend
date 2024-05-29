using LiteDB;

namespace CampingAPI.Data
{
    public class LiteDbContext
    {
        public LiteDatabase Database;

        public LiteDbContext()
        {
            Database = new LiteDatabase(@"Filename=CampingDB.db;Connection=shared");
        }
    }
}
