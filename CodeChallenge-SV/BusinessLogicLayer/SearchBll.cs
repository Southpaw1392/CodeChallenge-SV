using CodeChallenge_SV.DataAccessLayer;

namespace CodeChallenge_SV.BusinessLogicLayer
{
    public class SearchBll
    {
        private readonly SearchDal _dataAccessLayer;

        public SearchBll(SearchDal dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }
    }
}
