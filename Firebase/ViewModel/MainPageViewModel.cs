using System.Collections.ObjectModel;
using System.Windows.Input;
using Firebase.Model;
using Xamarin.Forms;

namespace Firebase.ViewModel
{
    public class MainPageViewModel : ObservableViewModel
    {
        bool _init;
        #region Properties
        public ICommand AddPersonCommand
        {
            get;
            set;
        }

        public ICommand OnAppearingCommand
        {
            get;
            set;
        }

        public ICommand OnDeletePersonCommand
        {
            get;
            set;
        }

        ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get { return _persons;}
            set { SetProperty(ref _persons, value); }
        }

        #endregion

        public MainPageViewModel()
        {
            Init();
        }

        #region Private Methods
        void Init()
        {
            _init = true;
            AddPersonCommand = new Command(async () =>
            {
                var page = new Pages.AddPersonPage();

                await App.Current.MainPage.Navigation.PushAsync(page);
            });

            OnAppearingCommand = new Command(async () =>
            {
                if (_init)
                {
                    var firebaseclient = DependencyService.Get<DependencyServices.IFirebaseClient>();

                    if (firebaseclient != null)
                    {
                        var items = await firebaseclient.GetAll();

                        if (items != null)
                        {
                            Persons = new ObservableCollection<Person>(items);
                        }

                        _init = false;
                    }
                }
            });

            OnDeletePersonCommand = new Command<Person>(async (obj) =>
            {
                Persons.Remove(obj);

                var firebaseclient = DependencyService.Get<DependencyServices.IFirebaseClient>();
                if (firebaseclient != null)
                {
                    await firebaseclient.Delete(obj);
                }
            });

            MessagingCenter.Subscribe<AddPersonViewModel, Person>(this, "SavePersonResult", (arg1, arg2) => {
                Persons.Add(arg2);
            });

        }
        #endregion
    }
}
