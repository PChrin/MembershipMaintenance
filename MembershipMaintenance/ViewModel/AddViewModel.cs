using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MembershipMaintenance.Model;
using System.Windows.Input;
using System.Windows;

namespace MembershipMaintenance.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        private string enteredFName;
        private string enteredLName;
        private string enteredEmail;
        public ICommand SaveCommand { get;  private set; }
        public RelayCommand<IClosable> CloseWindowCommand { get; private set; }
        public bool test { get; set; }


        // Constructor
        public AddViewModel()
        {
            SaveCommand = new RelayCommand<IClosable>(SaveMethod);
            this.CloseWindowCommand = new RelayCommand<IClosable>(this.CloseWindowMethod);
        }

        public string EnteredFName
        {
            get
            {
                return enteredFName;
            }
            set
            {
                enteredFName = value;
                RaisePropertyChanged("EnteredFName");
            }
        }
        public string EnteredLName
        {
            get
            {
                return enteredLName;
            }
            set
            {
                enteredLName = value;
                RaisePropertyChanged("EnteredLName");
            }
        }
        public string EnteredEmail
        {
            get
            {
                return enteredEmail;
            }
            set
            {
                enteredEmail = value;
                RaisePropertyChanged("EnteredEmail");
            }
        }

        public void SaveMethod(IClosable window)
        {
            if (window != null)
            {
                if (Validator.isPresent(EnteredFName) && Validator.isPresent(EnteredLName)
                    && Validator.isPresent(EnteredEmail) && Validator.IsValidEmail(EnteredEmail))
                {
                    Messenger.Default.Send(new MessageMember(enteredFName, enteredLName, enteredEmail, "Add"));
                    EnteredFName = null;
                    EnteredLName = null;
                    EnteredEmail = null;
                    window.Close();
                    MessageBox.Show("Customer added.", "Add Membership");

                }
            }
        }

        private void CloseWindowMethod(IClosable window)
        {
            if (window != null)
            {
                EnteredFName = null;
                EnteredLName = null;
                EnteredEmail = null;
                window.Close();
            }
        }
    }

}
