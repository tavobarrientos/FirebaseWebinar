using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Firebase.ViewModel
{
    public class AddPersonViewModel
    {
        #region Properties
        public ICommand SavePersonCommand
        {
            get;
            set;
        }

        public Model.Person Person
        {
            get;
            set;
        }
        #endregion

        public AddPersonViewModel()
        {
            Init();
        }

        #region Private Methods
        void Init()
        {
            SavePersonCommand = new Command(async () => 
            {
                var firebaseClient = DependencyService.Get<DependencyServices.IFirebaseClient>();

                if(firebaseClient != null)
                {
                    var result = await firebaseClient.Save(Person);
                    MessagingCenter.Send(this, "SavePersonResult", result);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            });

            Person = new Model.Person();
        }
        #endregion
    }
}
