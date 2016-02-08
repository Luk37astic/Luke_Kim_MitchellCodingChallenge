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
    [XmlRoot("Vehicles")]
    public class Vehicles
    {
        private List<VehicleDetails> _vehicleDetails;

        [DataMember]
        [XmlElement("VehicleDetails")]
        public List<VehicleDetails> VehicleDetails { get { return _vehicleDetails; } set { _vehicleDetails = value; } }
    }
}
