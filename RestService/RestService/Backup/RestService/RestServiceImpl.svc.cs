namespace RestService
{
    public class RestServiceImpl : IRestServiceImpl
    {
        #region IRestServiceImpl Members

        public string XMLData(string id)
        {
            return "You requested product " + id;
        }

        public string JSONData(string id)
        {
            return "You requested product " + id;
        }

        public ResponseData Auth(RequestData rData)
        {
            // Call BLL here
            var data = rData.details.Split('|');
            var response = new ResponseData
                               {
                                   Name = data[0],
                                   Age = data[1],
                                   Exp = data[2],
                                   Technology = data[3]
                               };

            return response;
        }

        #endregion



       
    }
}