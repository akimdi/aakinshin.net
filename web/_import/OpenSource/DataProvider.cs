using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace OpenSource
{
    public class OpenSourceData
    {
        public List<RepoGroup> Ru { get; }
        public List<RepoGroup> En { get; }

        public OpenSourceData(List<RepoGroup> ru, List<RepoGroup> en)
        {
            Ru = ru;
            En = en;
        }
    }

    public class ActivitiesData
    {
        public List<ActivityGroup> Ru { get; }
        public List<ActivityGroup> En { get; }

        public ActivitiesData(List<ActivityGroup> ru, List<ActivityGroup> en)
        {
            Ru = ru;
            En = en;
        }
    }

    public class DataProvider
    {
        public static OpenSourceData ReadOpenSource()
        {
            var yaml = new YamlStream();
            yaml.Load(new StreamReader("opensource.yaml"));
            var yamlRoot = (YamlMappingNode) yaml.Documents[0].RootNode;
            var githubYamlList = ((YamlSequenceNode) yamlRoot.Children.Values.First())
                .Children.Cast<YamlMappingNode>().ToList();
            var ru = githubYamlList.Select(it => Parser.Ru.ParseRepoGroup(it)).ToList();
            var en = githubYamlList.Select(it => Parser.En.ParseRepoGroup(it)).ToList();
            return new OpenSourceData(ru, en);
        }

        public static ActivitiesData ReadActivities()
        {
            var yaml = new YamlStream();
            yaml.Load(new StreamReader("activities.yaml"));
            var yamlRoot = (YamlMappingNode) yaml.Documents[0].RootNode;
            var githubYamlList = ((YamlSequenceNode) yamlRoot.Children.Values.First())
                .Children.Cast<YamlMappingNode>().ToList();
            var ru = githubYamlList.Select(it => Parser.Ru.ParseActivityGroup(it)).ToList();
            var en = githubYamlList.Select(it => Parser.En.ParseActivityGroup(it)).ToList();
            return new ActivitiesData(ru, en);
        }
    }
}