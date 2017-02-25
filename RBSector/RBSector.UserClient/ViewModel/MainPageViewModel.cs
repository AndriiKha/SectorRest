using RBSector.UserClient.DataAccessLayer;
using RBSector.UserClient.Models;
using RBSector.UserClient.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;


namespace RBSector.UserClient.ViewModel
{
    internal class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand _cancelCommand;
        private DelegateCommand _createCommand;
        private DelegateCommand _deleteCommand;
        private bool _hasSelection;
        private DelegateCommand _newCommand;
        private ObservableCollection<UserViewModel> _users = new ObservableCollection<UserViewModel>();
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        private UserViewModel _selectedUser;
        private bool _isDatabaseCreated;
        public MainPageViewModel()
        {
            if (IsInDesignMode)
            {
                return;
            }

            _createCommand = new DelegateCommand(Create_Executed);
            _selectCommand = new DelegateCommand(Select_Executed);
            _newCommand = new DelegateCommand(New_Executed, New_CanExecute);
            _deleteCommand = new DelegateCommand(Delete_Executed, Edit_CanExecute);
            _saveCommand = new DelegateCommand(Save_Executed, Save_CanExecute);
            _cancelCommand = new DelegateCommand(Cancel_Executed, Save_CanExecute);
        }

        public ICommand CancelCommand => _cancelCommand;

        public ICommand CreateCommand => _createCommand;

        public ICommand DeleteCommand => _deleteCommand;

        public bool HasSelection
        {
            get { return _hasSelection; }
            private set { SetProperty(ref _hasSelection, value); }
        }

        public ICommand NewCommand => _newCommand;

        public ObservableCollection<UserViewModel> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        public ICommand SaveCommand => _saveCommand;

        public ICommand SelectCommand => _selectCommand;

        public UserViewModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                SetProperty(ref _selectedUser, value);
                HasSelection = _selectedUser != null;
                _deleteCommand.RaiseCanExecuteChanged();
                editCommand.RaiseCanExecuteChanged();
            }
        }
        protected override bool Edit_CanExecute()
        {
            return _selectedUser != null && base.Edit_CanExecute();
        }

        protected override void Edit_Executed()
        {
            base.Edit_Executed();
            _selectedUser.IsInEditMode = true;
            _saveCommand.RaiseCanExecuteChanged();
            _cancelCommand.RaiseCanExecuteChanged();
        }

        private void Cancel_Executed()
        {
            if (_selectedUser.Id == 0)
            {
                _users.Remove(_selectedUser);
                SelectedUser = null;

                // Select last User. 
                if (_users.Count > 0)
                {
                    SelectedUser = Users.Last();
                }
            }
            else
            {
                // Get old one back from db
                _selectedUser.Model = Dal.GetByPrimaryKey(nameof(User), _selectedUser.Id) as User;
                _selectedUser.IsInEditMode = false;
            }

            IsInEditMode = false;
        }

        private async void Create_Executed()
        {
            await Dal.CreateDatabase();
        }

        private void Delete_Executed()
        {
            // Remove from db
            Dal.Delete(_selectedUser.Model);

            // Remove from list
            Users.Remove(_selectedUser);
            SelectedUser = null;
        }

        private bool New_CanExecute()
        {
            return !IsInEditMode && _isDatabaseCreated;
        }

        private void New_Executed()
        {
            Users.Add(new UserViewModel(new User()));
            SelectedUser = _users.Last();
            editCommand.Execute(null);
        }

        private bool Save_CanExecute()
        {
            return IsInEditMode;
        }

        private void Save_Executed()
        {
            if (_selectedUser.Model.UserRole == null)
                _selectedUser.Model.UserRole = Dal.GetAll(nameof(Role)).FirstOrDefault() as Role;
            // Store new one in db
            Dal.InsertOrUpdate(_selectedUser.Model);

            // Force a property change notification on the ViewModel:
            _selectedUser.Model = _selectedUser.Model;

            // Leave edit mode
            IsInEditMode = false;
            _selectedUser.IsInEditMode = false;
        }

        private void Select_Executed()
        {
            List<User> users = Dal.GetAll(nameof(User))
                .Select(user => user as User)
                .ToList();


            _users.Clear();
            foreach (var m in users)
            {
                _users.Add(new UserViewModel(m));
            }

            _isDatabaseCreated = true;
            _newCommand.RaiseCanExecuteChanged();
        }
    }
}
