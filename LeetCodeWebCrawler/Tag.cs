using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeWebCrawler
{
    public class Tag : IEquatable<Tag>
    {
        public string TagName { get; }
        public IList<QuestionNode> RelatedQuestions { get; }

        public Tag(string tag)
        {
            TagName = tag;
            RelatedQuestions = new List<QuestionNode>();
        }

        public void AddQuestionNode(QuestionNode node)
        {
            RelatedQuestions.Add(node);
        }

        public bool Equals(Tag other)
        {
            return other.TagName.Equals(TagName, StringComparison.InvariantCultureIgnoreCase);
        }
        public override int GetHashCode()
        {
            return TagName.ToLower().GetHashCode();
        }

        public override string ToString()
        {
            return TagName;
        }
    }
}
