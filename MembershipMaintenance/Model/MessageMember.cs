using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipMaintenance.Model
{
    public class MessageMember : Member
    {
        public string Message { get; private set; }

        public MessageMember(string fName, string lName, string mail, string message) : base(fName, lName, mail)
        {
            Message = message;
        }

    }
}
