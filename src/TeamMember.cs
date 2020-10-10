using System;

namespace app1
{
    public class TeamMember
    {
        public TeamMember()
        {

        }

        public TeamMember(string firstName, string lastName, RolaEnum rola)
        {
            FirstName = firstName;
            LastName = lastName;
            Rola = rola;
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RolaEnum Rola { get; set; }
    }
}
