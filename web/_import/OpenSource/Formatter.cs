using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSource
{
    public class Formatter
    {
        public static readonly Formatter Instance = new Formatter();

        public string ToHtml(List<RepoGroup> groups, List<ActivityGroup> activityGroups)
        {
            var builder = new StringBuilder();
            foreach (var repoGroup in groups)
            {
                builder.AppendLine($"<h5>{repoGroup.Role}</h5>");
                builder.AppendLine("<ul>");
                foreach (var repo in repoGroup.Repos)
                {
                    var href = "https://github.com/" + repo.Url;
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

            builder.AppendLine("<h5>Timeline</h5>");
            builder.AppendLine("<ul>");
            foreach (var activityGroup in activityGroups)
            {
                builder.Append("  <li>");
                builder.Append("<b>");
                builder.Append(activityGroup.Month);
                builder.Append(":</b> ");
                var first = true;
                foreach (var repo in activityGroup.Repos)
                {
                    var href = "https://github.com/" + repo.Url;
                    var caption = repo.RepoName;
                    if (first)
                        first = false;
                    else
                        builder.Append(", ");
                    builder.Append($"<a href=\"{href}\">{caption}</a>");
                }

                builder.AppendLine("</li>");
            }

            builder.AppendLine("</ul>");

            return builder.ToString();
        }

        private static string GetHtmlCaption(Repo repo)
        {
            var parts = repo.Url.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            return parts[0] + "/<b>" + parts[1] + "</b>";
        }
    }
}