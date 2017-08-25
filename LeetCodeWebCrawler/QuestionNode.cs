using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeWebCrawler
{
    public class QuestionNode : IEquatable<QuestionNode>
    {
        public QuestionNode(string name, string url, DifficultyLevel level)
        {
            Name = name;
            Url = url;
            Difficulty = level;

            tags = new List<Tag>();
            neighbors = new List<QuestionNode>();
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        private IList<Tag> tags;
        public IEnumerable<Tag> Tags => tags;

        private IList<QuestionNode> neighbors;
        public IEnumerable<QuestionNode> Neighbors => neighbors;

        public void AddTag(Tag tag)
        {
            tags.Add(tag);
        }

        public void AddNeighbor(QuestionNode another)
        {
            neighbors.Add(another);
        }

        public bool Equals(QuestionNode other)
        {
            //return Url == other.Url;
            return Url.Equals(other.Url, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return Url;
        }
    }

    public enum DifficultyLevel
    {
        Easy,
        Median,
        Difficult
    }


}
