using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeWebCrawler
{
    public class TagCollection
    {
        private TagCollection()
        {
            allTags = new HashSet<Tag>();
        }
        private readonly static TagCollection instance = new TagCollection();

        public static TagCollection Instance
        {
            get
            {
                return instance;
            }
        }

        private ISet<Tag> allTags;

        public void AddTopicToQuestion(QuestionNode node, Tag tag)
        {
            tag.AddQuestionNode(node);
            node.AddTag(tag);

            allTags.Add(tag);
        }
    }

}
