using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firebase.DependencyServices
{
    public interface IFirebaseClient
    {
        Task<List<Model.Person>> GetAll();
        Task<Model.Person> Save(Model.Person person);
        Task<bool> Delete(Model.Person person);
    }
}
