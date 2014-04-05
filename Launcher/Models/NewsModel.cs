using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;



namespace FoM.Launcher.Models
{
    public class NewsModel
    {
        private const string RSSURL = @"http://forums.faceofmankind.com/external.php?forumids=299&type=rss2";
        private List<NewsItem> _NewsItems;

        public List<NewsItem> NewsItems
        {
            get
            {
                string SummaryText = string.Empty;
                if (this._NewsItems == null)
                {
                    this._NewsItems = new List<NewsItem>();

                    SyndicationFeed NewsFeed = SyndicationFeed.Load(new XmlTextReader(RSSURL));
                    foreach (SyndicationItem NewsFeedItem in NewsFeed.Items)
                    {
                        SummaryText = NewsFeedItem.Summary.Text;
                        if (SummaryText.Contains("\n"))
                            SummaryText = SummaryText.Remove(SummaryText.IndexOf("\n"));
                        this._NewsItems.Add(new NewsItem() {Title = NewsFeedItem.Title.Text, Summary = SummaryText, PublishDate = NewsFeedItem.PublishDate.Date, ArticleURL = NewsFeedItem.Id });
                    }
                }
                return this._NewsItems;
            }
        }
    }
    public class NewsItem
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime PublishDate { get; set; }
        public string ArticleURL { get; set; }
    }
}
