using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using YamlDotNet.RepresentationModel;

namespace OpenSource
{
    public class Repo
    {
        public string Url { get; set; }
        public string Title { get; set; }

        public string RepoOwner => Url.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries)[0];
        public string RepoName => Url.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries)[1];

        public override string ToString() => Url + ": " + Title;
    }

    public class RepoGroup
    {
        public string Role { get; set; }
        public List<Repo> Repos { get; set; }
    }

    public class ActivityGroup
    {
        public string Month { get; set; }
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
            Title = GetStr(yaml, "title")
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

        public RepoGroup ParseRepoGroup(YamlMappingNode yaml) => new RepoGroup
        {
            Role = GetStr(yaml, "role"),
            Repos = ParseRepos(yaml)
        };

        public ActivityGroup ParseActivityGroup(YamlMappingNode yaml) => new ActivityGroup
        {
            Month = GetStr(yaml, "month"),
            Repos = ParseRepos(yaml)
        };
    }
}