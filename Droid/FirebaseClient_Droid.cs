using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Droid;
using Firebase.Model;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

[assembly:Dependency(typeof(FirebaseClient_Droid))]
namespace Firebase.Droid
{
    public class FirebaseClient_Droid : DependencyServices.IFirebaseClient
    {
        public FirebaseClient FirebaseClient
        {
            get;
            set;
        }

        public FirebaseClient_Droid()
        {
            FirebaseClient = new FirebaseClient(Constants.FIREBASEDBURL);
        }

        public async Task<List<Person>> GetAll()
        {
            var result = new List<Person>();
            var items = await FirebaseClient.Child(Constants.PERSONCOLLECTION).OnceAsync<Person>();

            foreach (var item in items)
            {
                var i = item.Object;
                i.Uid = item.Key;

                result.Add(i);
            }

            return result;
        }

        public async Task<Person> Save(Person person)
        {
            var item = await FirebaseClient.Child(Constants.PERSONCOLLECTION).PostAsync(person);

            return item.Object;
        }

        public async Task<bool> Delete(Model.Person person)
        {
            await FirebaseClient.Child(Constants.PERSONCOLLECTION).Child(person.Uid).DeleteAsync();
            return true;
        }

    }
}
