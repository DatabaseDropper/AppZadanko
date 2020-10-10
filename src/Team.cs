using System;
using System.Collections.Generic;
using System.Text;

namespace app1
{
    public class Team
    {
        public Team()
        {

        }

        public Team(string name)
        {
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public virtual List<TeamMember> Members { get; } = new List<TeamMember>();
    }
}
