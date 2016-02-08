using ClassLibrary;
using DALayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace RestService
{
    public class RestServiceImpl : IRestServiceImpl
    {
        #region VARIABLES
        private DAL dal;
        private string connString = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=dc5ao522;Database=postgres;";
        #endregion

        #region CRUD METHODS
        public MitchellClaim GetClaimByID(string id)
        {
            dal = new DAL(connString);

            // For string return type
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //return serializer.Serialize(dal.GetClaim(id));

            return dal.DBGetClaimByID(id);
        }

        public List<MitchellClaim> GetAllClaims()
        {
            dal = new DAL(connString);

            return dal.DBGetAllClaims();
        }

        public List<MitchellClaim> GetClaimsInDates(string start, string end)
        {
            dal = new DAL(connString);

            DateTime startDate = DateTime.ParseExact(start, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(end, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture);

            return dal.DBGetClaimsInDates(startDate, endDate);
        }

        public void CreateClaim(MitchellClaim cla)
        {
            dal = new DAL(connString);

            dal.DBCreateClaim(cla);
        }

        public void DeleteClaim(string id)
        {
            dal = new DAL(connString);

            dal.DBDeleteClaim(id);
        }

        public void DeleteAllClaims()
        {
            dal = new DAL(connString);

            dal.DBDeleteAllClaims();
        }
        #endregion

        #region UTILITY METHODS
        private string Serialize(MitchellClaim cla)
        {
            string XmlizedString = null;

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(MitchellClaim));
                //create an instance of the MemoryStream class since we intend to keep the XML string 
                //in memory instead of saving it to a file.
                MemoryStream memoryStream = new MemoryStream();
                //XmlTextWriter - fast, non-cached, forward-only way of generating streams or files 
                //containing XML data
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                //Serialize emp in the xmlTextWriter
                xs.Serialize(xmlTextWriter, cla);
                //Get the BaseStream of the xmlTextWriter in the Memory Stream
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                //Convert to array
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {

            }

            return XmlizedString;
        }

        private MitchellClaim Deserialize(byte[] xmlByteData)
        {
            MitchellClaim cla = new MitchellClaim();

            try
            {
                XmlSerializer ds = new XmlSerializer(typeof(MitchellClaim));
                MemoryStream memoryStream = new MemoryStream(xmlByteData);
                cla = (MitchellClaim)ds.Deserialize(memoryStream);
            }
            catch (Exception ex)
            {

            }

            return cla;
        }

        private string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(characters);
        }
        #endregion
    }
}