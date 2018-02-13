using System.IO;
using System.Linq;

namespace OpenSource
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.FirstOrDefault() == "pull")
            {
                ActivityPuller.Instance.Pull().Wait();
                return;
            }

            var openSource = DataProvider.ReadOpenSource();
            var activities = DataProvider.ReadActivities();

            var htmlEn = Formatter.Instance.ToHtml(openSource.En, activities.En);
            var htmlRu = Formatter.Instance.ToHtml(openSource.Ru, activities.Ru);

            if (!Directory.Exists("_generated"))
                Directory.CreateDirectory("_generated");

            File.WriteAllText(Path.Combine("_generated", "opensource.html"), htmlEn);
            File.WriteAllText(Path.Combine("_generated", "opensource-ru.html"), htmlRu);
        }
    }
}