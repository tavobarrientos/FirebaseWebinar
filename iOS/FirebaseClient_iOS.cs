using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Model;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

[assembly: Dependency(typeof(Firebase.iOS.FirebaseClient_iOS))]
namespace Firebase.iOS
{
    public class FirebaseClient_iOS : DependencyServices.IFirebaseClient
    {
        public FirebaseClient FirebaseClient
        {
            get;
            set;
        }

        public FirebaseClient_iOS()
        {
            FirebaseClient = new FirebaseClient(Constants.FIREBASEDBURL);
        }

        public async Task<bool> Delete(Person person)
        {
            await FirebaseClient.Child(Constants.PERSONCOLLECTION).Child(person.Uid).DeleteAsync();
            return true;
        }

        public async Task<List<Person>> GetAll()
        {
            var all = await FirebaseClient.Child(Constants.PERSONCOLLECTION).OnceAsync<Person>();
            var result = new List<Person>();

            foreach (var item in all)
            {
                var obj = item.Object;
                obj.Uid = item.Key;

                result.Add(obj);
            }

            return result;
        }

        public async Task<Person> Save(Person person)
        {
            var result = await FirebaseClient.Child(Constants.PERSONCOLLECTION).PostAsync(person);
            var item = result.Object;
            item.Uid = result.Key;

            return item;
        }
    }
}
