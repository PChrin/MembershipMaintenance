using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MembershipMaintenance.Model;
using MembershipMaintenance.View;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace MembershipMaintenance.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // Properties.
        private Member selectedMember;
        private Member dataBase = new Member();
        private ObservableCollection<Member> members;
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public RelayCommand<IClosable> CloseWindowCommand { get; private set; }

        // Constructor.
        public MainViewModel()
        {
            members = new ObservableCollection<Member>();
            members = dataBase.GetMemberships();
            Messenger.Default.Register<MessageMember>(this, ReceiveMember);
            AddCommand = new RelayCommand(AddCommandMethod);
            ChangeCommand = new RelayCommand(ChangeMethod);
            DeleteCommand = new RelayCommand(DeleteMethod);
            this.CloseWindowCommand = new RelayCommand<IClosable>(this.CloseWindowMethod);
        }

        public Member SelectedMember
        {
            get
            {
                return selectedMember;
            }
            set
            {
                selectedMember = value;
                RaisePropertyChanged("SelectedMember");
            }
        }

        public ObservableCollection<Member> MemberList
        {
            get
            {
                return members;
            }
        }

        public void AddCommandMethod()
        {
            AddView obj = new AddView();
            obj.Show();
            
        }

        public void ReceiveMember(MessageMember m)
        {
            if(m.Message == "Update")
            {
                int index = members.IndexOf(SelectedMember);
                members.RemoveAt(index);
                members.Insert(index,m);
                dataBase.SaveMemberships(members);
            }
            else if(m.Message == "Add")
            {
                members.Add(m);
                dataBase.SaveMemberships(members);
            }
        }

        public void ChangeMethod()
        {
            if (SelectedMember != null)
            {
                UpdateView changeView = new UpdateView();
                Messenger.Default.Send(SelectedMember);
                changeView.Show();
            }
        }

        public void DeleteMethod()
        {
            if (SelectedMember != null)
            {
                members.Remove(SelectedMember);
                dataBase.SaveMemberships(members);
                MessageBox.Show("Customer deleted.", "Membership Maintenance");
            }
        }

        private void CloseWindowMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}