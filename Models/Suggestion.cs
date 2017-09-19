using System;

namespace Models
{
    [Serializable ]
    public class Suggestion
    {
        public int SuggestionId { get; set; }

        public string CustomerName { get; set; }

        public string ConsumeDesc { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string SuggestionDesc { get; set; }

        public int StatusId { get; set; }

        public DateTime SuggestionTime { get; set; }

    }
}
