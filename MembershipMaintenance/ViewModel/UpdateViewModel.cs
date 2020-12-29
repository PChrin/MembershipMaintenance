using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MembershipMaintenance.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace MembershipMaintenance.ViewModel
{
    public class UpdateViewModel : ViewModelBase
    {
        private string enteredFName;
        private string enteredLName;
        private string enteredEmail;
        public ICommand UpdateCommand { get; private set; }
        public RelayCommand<IClosable> CloseWindowCommand { get; private set; }
        
        public UpdateViewModel()
        {
            Messenger.Default.Register<Member>(this, GetSelectedMember);
            this.CloseWindowCommand = new RelayCommand<IClosable>(CloseWindowMethod);
            UpdateCommand = new RelayCommand<IClosable>(UpdateMethod);
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

        public void GetSelectedMember(Member m)
        {
            EnteredFName = m.FirstName;
            EnteredLName = m.LastName;
            EnteredEmail = m.Email;
        }

        public void UpdateMethod(IClosable window)
        {
            if (window != null)
            {
                if (Validator.isPresent(EnteredFName) && Validator.isPresent(EnteredLName)
                    && Validator.isPresent(EnteredEmail) && Validator.IsValidEmail(EnteredEmail))
                {
                    Messenger.Default.Send(new MessageMember(EnteredFName, EnteredLName, EnteredEmail, "Update"));
                    EnteredFName = null;
                    EnteredLName = null;
                    EnteredEmail = null;
                    window.Close();
                    MessageBox.Show("Customer's information updated.", "Update Membership");
                }
            }
        }

        public void CloseWindowMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }

}
