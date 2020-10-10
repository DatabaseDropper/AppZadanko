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
            Console.WriteLine("1 - Show All Teams with Members");
            Console.WriteLine("2 - Add Random Member to all Teams");
            Console.WriteLine("3 - Add Team");
            Console.WriteLine("4 - Exit");
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
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine();
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
