using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.IO;

namespace MembershipMaintenance.Model
{
    public class Member : ObservableObject
    {
        // Private properties.
        private string firstName;
        private string lastName;
        private string email;
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static StreamWriter outputFile;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                Set<string>(() => this.FirstName, ref firstName, value);
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                Set<string>(() => this.LastName, ref lastName, value);
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                Set<string>(() => this.Email, ref email, value);
            }
        }

        // Constructors.
        public Member() { }
        public Member(string firstName, string lastName, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }

        public override string ToString()
        {
            return firstName + " " + lastName + ", " + email;
        }

        // Write the list of members in observablecollection to a text file name WriteLine.txt.
        public void SaveMemberships(ObservableCollection<Member> obj)
        {
            using (outputFile = new StreamWriter(path + @"\WriteLines.txt", false))
            {
                foreach (Member member in obj)
                {
                    outputFile.Write(member.FirstName + "|");
                    outputFile.Write(member.LastName + "|");
                    outputFile.WriteLine(member.Email);

                }
                //write the end of the document
                outputFile.Close();
            }
        }

        // Read the list of members from the textfile, WriteLines.txt to an observablecollection object.
        public ObservableCollection<Member> GetMemberships()
        {
            ObservableCollection<Member> list = new ObservableCollection<Member>();
            string line;
            using (StreamReader file = new StreamReader(path + @"\WriteLines.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {

                    char[] delimiters = new char[] { '|' };
                    string[] parts = line.Split(delimiters);

                    Member objMember = new Member();
                    objMember.FirstName = parts[0];
                    objMember.LastName = parts[1];
                    objMember.Email = parts[2];
                    list.Add(objMember);
                }
                file.Close();
            }
            return list;
        }
    }
}
