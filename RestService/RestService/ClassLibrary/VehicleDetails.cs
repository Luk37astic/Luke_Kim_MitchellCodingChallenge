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
    [XmlRoot("VehicleDetails")]
    public class VehicleDetails
    {
        // Private fields
        private int _modelYear;
        private string _makeDescription;
        private string _modelDescription;
        private string _engineDescription;
        private string _exteriorColor;
        private string _vin;
        private string _licPlate;
        private string _licPlateState;
        private DateTime _licPlateExpDate;
        private string _damageDescription;
        private int _mileage;

        // Public get/setters
        [DataMember]
        [XmlElement("ModelYear")]
        public int ModelYear { get { return _modelYear; } set { _modelYear = value; } }
        [DataMember]
        [XmlElement("MakeDescription")]
        public string MakeDescription { get { return _makeDescription; } set { _makeDescription = value; } }
        [DataMember]
        [XmlElement("ModelDescription")]
        public string ModelDescription { get { return _modelDescription; } set { _modelDescription = value; } }
        [DataMember]
        [XmlElement("EngineDescription")]
        public string EngineDescription { get { return _engineDescription; } set { _engineDescription = value; } }
        [DataMember]
        [XmlElement("ExteriorColor")]
        public string ExteriorColor { get { return _exteriorColor; } set { _exteriorColor = value; } }
        [DataMember]
        [XmlElement("Vin")]
        public string Vin { get { return _vin; } set { _vin = value; } }
        [DataMember]
        [XmlElement("LicPlate")]
        public string LicPlate { get { return _licPlate; } set { _licPlate = value; } }
        [DataMember]
        [XmlElement("LicPlateState")]
        public string LicPlateState { get { return _licPlateState; } set { _licPlateState = value; } }
        [DataMember]
        [XmlElement("LicPlateExpDate")]
        public DateTime LicPlateExpDate { get { return _licPlateExpDate; } set { _licPlateExpDate = value; } }
        [DataMember]
        [XmlElement("DamageDescription")]
        public string DamageDescription { get { return _damageDescription; } set { _damageDescription = value; } }
        [DataMember]
        [XmlElement("Mileage")]
        public int Mileage { get { return _mileage; } set { _mileage = value; } }

        // Constructors
        public VehicleDetails()
        { }
    }
}
