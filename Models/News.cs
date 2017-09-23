using System;

namespace Models
{
    [Serializable]
    public class News
    {
        public int NewsId { get; set; }

        public string NewsTitle { get; set; }

        public string NewsContents { get; set; }

        public DateTime PublishTime { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

    }
}
