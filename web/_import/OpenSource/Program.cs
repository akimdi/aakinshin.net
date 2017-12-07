using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utils;
using YamlDotNet.RepresentationModel;

namespace OpenSource
{
    public class Repo
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }

        public override string ToString() => Url + ": " + Title;                
    }

    public class RepoGroup
    {
        public string Role { get; set; }
        public List<Repo> Repos { get; set; }
    }

    public class Parser : YamlParser
    {
        public static readonly Parser En = new Parser("en");
        public static readonly Parser Ru = new Parser("ru");

        private Parser(string lang) : base(lang)
        {
        }

        private Repo ParseRepo(YamlMappingNode yaml) => new Repo
        {
            Url = GetStr(yaml, "url"),
            Title = GetStr(yaml, "title"),
            Alias = GetStr(yaml, "alias")
        };

        private List<Repo> ParseRepos(YamlMappingNode yaml)
        {
            var yamlList = Get(yaml, "repos") as YamlSequenceNode;
            if (yamlList == null)
                return new List<Repo>();
            return yamlList.Children
                .Select(repo => ParseRepo((YamlMappingNode) repo))
                .ToList();
        }

        public RepoGroup ParseRepoGroup(YamlMappingNode yaml) => new RepoGroup()
        {
            Role = GetStr(yaml, "role"),
            Repos = ParseRepos(yaml)
        };
    }

    public class Formatter
    {
        public static readonly Formatter Instance = new Formatter();

        public string ToHtml(List<RepoGroup> groups)
        {
            var builder = new StringBuilder();
            foreach (var repoGroup in groups)
            {
                builder.AppendLine($"<h5>{repoGroup.Role}</h5>");
                builder.AppendLine("<ul>");
                foreach (var repo in repoGroup.Repos)
                {
                    var href = "https://" + repo.Url;
                    var caption = GetHtmlCaption(repo);
                    var hrefCommit = href + "/commits?author=AndreyAkinshin";
                    builder.Append($"  <li><a href=\"{href}\">{caption}</a>");
                    if (repoGroup.Role != "Gists")
                        builder.Append($" (<a href=\"{hrefCommit}\">commits</a>)");
                    builder.Append("<br />");
                    builder.AppendLine($" {repo.Title}</li>");
                }

                builder.AppendLine("</ul>");
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static string GetHtmlCaption(Repo repo)
        {
            if (repo.Alias != string.Empty)
                return $"<b>{repo.Alias}</b>";
            var parts = repo.Url
                .Replace("github.com/", "")
                .Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            return string.Join('/', parts.Take(parts.Length - 1)) + "/<b>" + parts.Last() + "</b>";
        }
    }

    public class Program
    {
        public static void Main()
        {
            var yaml = new YamlStream();
            yaml.Load(new StreamReader("opensource.yaml"));
            var yamlRoot = (YamlMappingNode) yaml.Documents[0].RootNode;
            var githubYamlList = ((YamlSequenceNode) yamlRoot.Children.Values.First())
                .Children.Cast<YamlMappingNode>().ToList();

            var repoGroupsEn = githubYamlList.Select(it => Parser.En.ParseRepoGroup(it)).ToList();
            var repoGroupsRU = githubYamlList.Select(it => Parser.Ru.ParseRepoGroup(it)).ToList();

            Console.WriteLine(Formatter.Instance.ToHtml(repoGroupsEn));
            
            var htmlEn = Formatter.Instance.ToHtml(repoGroupsEn);
            var htmlRu = Formatter.Instance.ToHtml(repoGroupsRU);

            if (!Directory.Exists("_generated"))
                Directory.CreateDirectory("_generated");

            File.WriteAllText(Path.Combine("_generated", "opensource.html"), htmlEn);
            File.WriteAllText(Path.Combine("_generated", "opensource-ru.html"), htmlRu);
        }
    }
}