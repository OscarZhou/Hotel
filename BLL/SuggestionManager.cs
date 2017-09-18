using DAL;
using Models;
using System.Collections.Generic;

namespace BLL
{
    public class SuggestionManager
    {
        private SuggestionService objSuggestionService = new SuggestionService();

        public int SubmitSuggestion(Suggestion objSuggestion)
        {
            return objSuggestionService.SubmitSuggestion(objSuggestion);
        }

        public List<Suggestion> GetSuggestionNotAudited(Suggestion objSuggestion)
        {
            return objSuggestionService.GetSuggestionNotAudited(objSuggestion);
        }

        public int ModifySuggestion(Suggestion objSuggestion)
        {
            return objSuggestionService.ModifySuggestion(objSuggestion);
        }
    }
}
