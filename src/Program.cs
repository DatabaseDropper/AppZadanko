using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace app1
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                await DisplayMenu();
            }
        }

        private static async Task DisplayMenu()
        {
            Console.WriteLine("1 - Show All Teams and Members");
            Console.WriteLine("2 - Add Random Member to all Teams");
            Console.WriteLine("3 - Add Team");
            Console.WriteLine("4 - Show All Members and their Teams");
            Console.WriteLine("5 - Remove team of some member");
            Console.WriteLine("6 - Exit");
            Console.WriteLine();

            var choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    await ShowAllLazily();
                    break;
                case "2":
                    await AddRandomMember();
                    break;
                case "3":
                    await AddTeam();
                    break;
                case "4":
                    await ShowAllMembers();
                    break;
                case "5":
                    await RemoveTeamOfMember();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine();
        }

        private static async Task RemoveTeamOfMember()
        {
            using (var ctx = new Context())
            {
                var teams = await ctx.Teams.Include(x => x.Members).ToListAsync();

                var randomTeam = teams[Randomness.Random.Next(teams.Count)];

                randomTeam.Members.RemoveAt(0);

                await ctx.SaveChangesAsync();
            }
        }

        private static async Task ShowAllMembers()
        {
            using (var ctx = new Context())
            {
                var members = await ctx.TeamMembers.Include(x => x.Team).ToListAsync();

                foreach (var member in members)
                {
                    Console.WriteLine($"FirstName: {member.FirstName}; Team: {member.Team?.Name ?? "NULL"}");
                }
            }
        }

        private static async Task AddTeam()
        {
            Console.WriteLine("Type team name");
            var name = Console.ReadLine();

            using (var ctx = new Context())
            {
                var team = new Team(name);
                await ctx.AddAsync(team);
                await ctx.SaveChangesAsync();
            }
        }

        private static async Task AddRandomMember()
        {
            var random1stNames = new[] { "Tomek", "Damian", "Basia" };
            var randomLastNames = new[] { "Kowalski", "Miodek", "Nowak" };
            var roles = Enum.GetValues(typeof(RolaEnum));

            using (var ctx = new Context())
            {
                var teams = await ctx.Teams.Include(x => x.Members).ToListAsync();

                foreach (var team in teams)
                {
                    var firstName = random1stNames[Randomness.Random.Next(random1stNames.Length)];
                    var lastName = randomLastNames[Randomness.Random.Next(randomLastNames.Length)];
                    var role = (RolaEnum)roles.GetValue(Randomness.Random.Next(roles.Length));

                    var newMember = new TeamMember(firstName, lastName, role);
                    team.Members.Add(newMember);
                }

                await ctx.SaveChangesAsync();
            }
        }

        private static async Task ShowAllLazily()
        {
            using (var ctx = new Context())
            {
                var teams = await ctx.Teams.ToListAsync();

                foreach (var team in teams)
                {
                    Console.WriteLine(team.Name);

                    foreach (var member in team.Members)
                        Console.WriteLine($"\t{member.FirstName} {member.LastName}");
                }
            }
        }
    }
}
