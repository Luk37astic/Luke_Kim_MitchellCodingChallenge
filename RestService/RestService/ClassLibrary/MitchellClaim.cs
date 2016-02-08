using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary
{
    [DataContract(Namespace = "http://www.mitchell.com/examples/claim")]
    [XmlRoot("MitchellClaim")]
    public class MitchellClaim
    {
        // Private fields
        private string _claimNumber;
        private string _claimantFirstName;
        private string _claimantLastName;
        private string _status;
        private DateTime _lossDate;
        private LossInfoType _lossInfo;
        private long _assignedAdjusterID;
        private Vehicles _vehicles;

        // Public get/setters
        [DataMember]
        [XmlElement("ClaimNumber")]
        public string ClaimNumber { get { return _claimNumber; } set { _claimNumber = value; } }
        [DataMember]
        [XmlElement("ClaimantFirstName")]
        public string ClaimantFirstName { get { return _claimantFirstName; } set { _claimantFirstName = value; } }
        [DataMember]
        [XmlElement("ClaimantLastName")]
        public string ClaimantLastName { get { return _claimantLastName; } set { _claimantLastName = value; } }
        [DataMember]
        [XmlElement("Status")]
        public string Status { get { return _status; } set { _status = value; } }
        [DataMember]
        [XmlElement("LossDate")]
        public DateTime LossDate { get { return _lossDate; } set { _lossDate = value; } }
        [DataMember]
        [XmlElement("LossInfo")]
        public LossInfoType LossInfo { get { return _lossInfo; } set { _lossInfo = value; } }
        [DataMember]
        [XmlElement("AssignedAdjusterID")]
        public long AssignedAdjusterID { get { return _assignedAdjusterID; } set { _assignedAdjusterID = value; } }
        [DataMember]
        [XmlElement("Vehicles")]
        public Vehicles Vehicles { get { return _vehicles; } set { _vehicles = value; } }

        // Constructors
        public MitchellClaim()
        { }
    }
}
