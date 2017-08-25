using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeWebCrawler
{
    public class Crawler
    {
        public static readonly QuestionNode startPoint 
            = new QuestionNode("Maximum Length of Pair Chain", 
                "https://leetcode.com/problems/maximum-length-of-pair-chain", 
                DifficultyLevel.Median);
       

        public void Start(QuestionNode orig)
        {
            HtmlParser parser = new HtmlParser();

            Queue<QuestionNode> queue = new Queue<QuestionNode>();
            HashSet<QuestionNode> set = new HashSet<QuestionNode>();
            queue.Enqueue(orig);
            set.Add(orig);

            while(queue.Any())
            {
                QuestionNode currentNode = queue.Dequeue();
                IEnumerable<QuestionNode> neigbors = parser.SendRequest(currentNode);
                foreach(QuestionNode nei in neigbors)
                {
                    if (set.Contains(nei)) continue;
                    queue.Enqueue(nei);
                    set.Add(nei);
                }
            }
        }


    }
}
