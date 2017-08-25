using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeWebCrawler
{
    public class HtmlParser
    {
        private const string START = "<a";
        private const string END = "</a>";
        private const string DIVEND = "<br>";

        private const string TAGS_TOPICS = "tags-topics";
        private const string TAGS_QUESTION = "tags-question";

        private const string TAG = "href=\"/tag/";
        private const string PROBLEM = "href=\"/problems/";

        private const string PROBLEM_ROOT = "https://leetcode.com";
        //private const string 

        public IEnumerable<QuestionNode> SendRequest(QuestionNode currentNode)
        {
            ParseResponse(currentNode.Url, out IList<string> tagString, out IList<string> questionString);

            foreach (string tagStr in tagString)
            {
                Tag newTage = ParseTag(tagStr);
                TagCollection.Instance.AddTopicToQuestion(currentNode, newTage);
            }

            IList<QuestionNode> nodes = new List<QuestionNode>();
            foreach (string qStr in questionString)
            {
                QuestionNode node = ParseQuestion(qStr, PROBLEM_ROOT);
                currentNode.AddNeighbor(node);
                nodes.Add(node);
            }
            return nodes;
        }

        private void ParseResponse(string url, out IList<string> tagString, out IList<string> questionString)
        {
            tagString = new List<string>();
            questionString = new List<string>();

            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead(url);
                StreamReader reader = new StreamReader(stream);

                bool inTopic = false;
                bool inQuestion = false;
                bool inOneEntry = false;

                StringBuilder sb = null;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    if (line.Contains(TAGS_TOPICS))
                    {
                        inTopic = true;
                        continue;
                    }
                    if (inTopic && line.Contains(DIVEND))
                    {
                        inTopic = false;
                        continue;
                    }
                    if (line.Contains(TAGS_QUESTION))
                    {
                        inQuestion = true;
                        continue;
                    }
                    if (inQuestion && line.Contains(DIVEND))
                    {
                        inQuestion = false;
                        continue;
                    }

                    if (inTopic)
                    {
                        if (inOneEntry)
                        {
                            sb.Append(line.Trim());
                        }

                        if (line.Contains(START))
                        {
                            sb = new StringBuilder();
                            sb.Append(line.Trim());
                            inOneEntry = true;
                        }
                        else if (line.Contains(END))
                        {
                            tagString.Add(sb.ToString());
                            sb = new StringBuilder();
                            inOneEntry = false;
                        }
                    }
                    if (inQuestion)
                    {
                        if (inOneEntry)
                        {
                            sb.Append(line.Trim());
                        }

                        if (line.Contains(START))
                        {
                            sb = new StringBuilder();
                            sb.Append(line.Trim());
                            inOneEntry = true;
                        }
                        else if (line.Contains(END))
                        {
                            questionString.Add(sb.ToString());
                            sb = new StringBuilder();
                            inOneEntry = false;
                        }
                    }
                }
            }
        }

        private Tag ParseTag(string tagString)
        {
            string[] subStrings = tagString.Split('>');
            string[] another = subStrings[1].Split('<');
            return new Tag(another[0]);
        }

        private QuestionNode ParseQuestion(string questionString, string root)
        {
            string[] arr = questionString.Split('"');

            char l = arr[1].ToCharArray().Last();
            DifficultyLevel level = l == 'E' ? DifficultyLevel.Easy :
                (l == 'M' ? DifficultyLevel.Median : DifficultyLevel.Difficult);
            string url = arr[3];
            string name = arr[4].Replace(">", "").Replace(END, "");

            return new QuestionNode(name, root + url, level);
        }

    }
}
