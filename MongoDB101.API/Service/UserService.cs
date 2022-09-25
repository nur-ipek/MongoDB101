using MongoDB.Driver;
using MongoDB101.API.Model;
using MongoDB101.API.Settings;
using System.Collections.Generic;
using System.Xaml.Permissions;

namespace MongoDB101.API.Service
{
    public class UserService
    {
        private IDbSettings _dbSettings;
        private IMongoCollection<User> _user;

        public UserService(IDbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            MongoClient client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.Database);
            _user = db.GetCollection<User>(dbSettings.Collection);
        }

        public List<User> GetAll()
        {
            return _user.Find(a => true).ToList();

        }

        public User GetSingle(string id)
        {
            return _user.Find(u => u.Id == id).FirstOrDefault();
        }

        public User Add(User user)
        {
            _user.InsertOne(user);
           return user;
        }

        public long Update(User currentUser)
        //Kullancı Güncelleme (ReplaceOne() ->id alanı dışında bir verinin tüm içeriğini değiştirmek için kullanılır.)
        //Update() --> Önce hangi kaydı güncellemek istediğimiz belirtilir. Ardından ise hangi alanı güncellemek istediğimiz belirtilir.
        //FindOneUpdate --> Koleksiyon içerisindeki ilk veriyi yakalar ve onu günceller.
        {
            return _user.ReplaceOne(a => a.Id == currentUser.Id,currentUser).ModifiedCount;
        }

        public long Delete(string id)
        {
            return _user.DeleteOne(a => a.Id == id).DeletedCount;
        }

    }
}
