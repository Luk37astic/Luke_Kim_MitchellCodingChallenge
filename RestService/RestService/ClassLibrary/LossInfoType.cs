using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary
{
    [DataContract]
    [XmlRoot("LossInfo")]
    public class LossInfoType
    {
        // Private fields
        private string _causeOfLoss;
        private DateTime _reportedDate;
        private string _lossDescription;

        // Public get/setters
        [DataMember]
        [XmlElement("CauseOfLoss")]
        public string CauseOfLoss { get { return _causeOfLoss; } set { _causeOfLoss = value; } }
        [DataMember]
        [XmlElement("ReportedDate")]
        public DateTime ReportedDate { get { return _reportedDate; } set { _reportedDate = value; } }
        [DataMember]
        [XmlElement("LossDescription")]
        public string LossDescription { get { return _lossDescription; } set { _lossDescription = value; } }

        // Constructors
        public LossInfoType()
        { }
    }
}
