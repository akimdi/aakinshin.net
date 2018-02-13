using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace OpenSource
{
    public class ActivityPuller
    {
        public static ActivityPuller Instance = new ActivityPuller();
        private readonly GitHubClient client;

        private ActivityPuller()
        {
            client = new GitHubClient(new ProductHeaderValue("ActivityPuller"));
            var tokenAuth = new Credentials(Environment.GetEnvironmentVariable("GitHubToken"));
            client.Credentials = tokenAuth;
        }

        public async Task Pull()
        {
            var data = new Dictionary<string, HashSet<string>>();

            void Add(Committer committer, string name)
            {
                if (committer?.Name != "Andrey Akinshin")
                    return;

                var date = committer.Date;
                var key = date.ToString("yyyy-MM");
                if (!data.ContainsKey(key))
                    data[key] = new HashSet<string>();
                data[key].Add(name);
            }

            var provider = DataProvider.ReadOpenSource();
            foreach (var repoGroup in provider.En)
            foreach (var repo in repoGroup.Repos)
            {                
                var sp = repo.Url.Split('/');                
                Console.WriteLine("## " + repo.Url);
                var commitRequest = new CommitRequest
                {
                    Author = "AndreyAkinshin"
                };
                var commits = await client.Repository.Commit.GetAll(sp[0], sp[1], commitRequest);

                foreach (var commit in commits)
                {
                    Add(commit?.Commit?.Author, repo.Url);
                    Add(commit?.Commit?.Committer, repo.Url);
                }
            }

            var builder = new StringBuilder();
            builder.AppendLine("activities:");
            foreach (var key in data.Keys.OrderByDescending(it => it))
            {
                builder.AppendLine("  - month: " + key);
                builder.AppendLine("    repos:");
                foreach (var url in data[key].OrderBy(it => it))
                    builder.AppendLine("      - url: " + url);
            }

            File.WriteAllText("activities.yaml", builder.ToString());
        }
    }
}