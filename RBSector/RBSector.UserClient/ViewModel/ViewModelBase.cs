using RBSector.UserClient.Mvvm;
using System.Windows.Input;

namespace RBSector.UserClient.ViewModel
{
    internal class ViewModelBase : BindableBase
    {
        protected DelegateCommand editCommand;
        private bool _isInEditMode = false;

        public ViewModelBase()
        {
            if (IsInDesignMode)
            {
                return;
            }

            editCommand = new DelegateCommand(Edit_Executed, Edit_CanExecute);
        }

        public ICommand EditCommand => editCommand;

        public bool IsInDesignMode => Windows.ApplicationModel.DesignMode.DesignModeEnabled;

        public bool IsInEditMode
        {
            get
            {
                return _isInEditMode;
            }

            set
            {
                SetProperty(ref _isInEditMode, value);
                editCommand.RaiseCanExecuteChanged();
            }
        }

        protected virtual bool Edit_CanExecute()
        {
            return !IsInEditMode;
        }

        protected virtual void Edit_Executed()
        {
            IsInEditMode = true;
        }
    }
}
