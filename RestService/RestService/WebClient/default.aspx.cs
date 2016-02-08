using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace WebClient
{
    public partial class _default : Page
    {
        #region VARIABLES
        private List<MitchellClaim> listCollection = new List<MitchellClaim>();
        private Random rand = new Random();
        #endregion

        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Only run one of these two:
                //RunTests();
                UpdateGrid();
            }
        }

        protected void StartDateBtn_Click(object sender, EventArgs e)
        {
            StartDateBtn.Visible = false;
            StartDateCal.Visible = true;
        }
        protected void EndDateBtn_Click(object sender, EventArgs e)
        {
            EndDateBtn.Visible = false;
            EndDateCal.Visible = true;
        }
        protected void StartDateCal_SelectionChanged(object sender, EventArgs e)
        {
            StartDateBtn.Text = StartDateCal.SelectedDate.ToString();
            StartDateCal.Visible = false;
            StartDateBtn.Visible = true;
        }
        protected void EndDateCal_SelectionChanged(object sender, EventArgs e)
        {
            EndDateCal.SelectedDate = EndDateCal.SelectedDate.AddMilliseconds(86399999);
            EndDateBtn.Text = EndDateCal.SelectedDate.ToString();
            EndDateCal.Visible = false;
            EndDateBtn.Visible = true;
        }
        protected void DateRangeBtn_Click(object sender, EventArgs e)
        {
            DateTime start = StartDateCal.SelectedDate;
            DateTime end = EndDateCal.SelectedDate;

            // Validation
            if (start == new DateTime())
            {
                ResTxt.Text = "Please select a start date.";
                return;
            }

            if (end == new DateTime())
            {
                ResTxt.Text = "Please select an end date.";
                return;
            }

            if (end <= start)
            {
                ResTxt.Text = "End date is on or before start date. Please select valid dates.";
                return;
            }

            listCollection.Clear();
            listCollection = ReqGetClaimsInDates(start.ToString("yyyyMMddTHHmmss"), end.ToString("yyyyMMddTHHmmss"));

            XmlGridView.DataSource = listCollection;
            XmlGridView.DataBind();
        }

        protected void GetBtn_Click(object sender, EventArgs e)
        {
            // Validation
            if (ClaNumTxt.Text == "")
            {
                ResTxt.Text = "Please enter a claim number.";
                return;
            }

            listCollection.Clear();
            listCollection.Add(ReqGetClaimByID(ClaNumTxt.Text));

            XmlGridView.DataSource = listCollection;
            XmlGridView.DataBind();

            //try
            //{
            //    // Get the request's response
            //    HttpWebResponse res = GetClaimByID();

            //    // Build response stream
            //    Stream resStream = res.GetResponseStream();
            //    StreamReader sr = new StreamReader(resStream);

            //    // Read the response stream into an XmlDocument
            //    XmlDocument ResponseXmlDocument = new XmlDocument();
            //    ResponseXmlDocument.LoadXml(sr.ReadToEnd());

            //    // Show only the xml representing the response details (inner request)
            //    TextBox1.Text = ResponseXmlDocument.InnerXml;
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
        }
        protected void GetAllBtn_Click(object sender, EventArgs e)
        {
            listCollection = ReqGetAllClaims();

            XmlGridView.DataSource = listCollection;
            XmlGridView.DataBind();
        }

        protected void BtnLossDate_Click(object sender, EventArgs e)
        {
            ((Button)(XmlGridView.FooterRow.FindControl("BtnLossDate"))).Visible = false;
            ((Calendar)(XmlGridView.FooterRow.FindControl("CalLossDate"))).Visible = true;
        }
        protected void CalLossDate_SelectionChanged(object sender, EventArgs e)
        {
            ((Button)(XmlGridView.FooterRow.FindControl("BtnLossDate"))).Text = ((Calendar)(XmlGridView.FooterRow.FindControl("CalLossDate"))).SelectedDate.ToString();
            ((Calendar)(XmlGridView.FooterRow.FindControl("CalLossDate"))).Visible = false;
            ((Button)(XmlGridView.FooterRow.FindControl("BtnLossDate"))).Visible = true;
        }

        protected void BtnRepDate_Click(object sender, EventArgs e)
        {
            ((Button)(XmlGridView.FooterRow.FindControl("BtnRepDate"))).Visible = false;
            ((Calendar)(XmlGridView.FooterRow.FindControl("CalRepDate"))).Visible = true;
        }
        protected void CalRepDate_SelectionChanged(object sender, EventArgs e)
        {
            ((Button)(XmlGridView.FooterRow.FindControl("BtnRepDate"))).Text = ((Calendar)(XmlGridView.FooterRow.FindControl("CalRepDate"))).SelectedDate.ToString();
            ((Calendar)(XmlGridView.FooterRow.FindControl("CalRepDate"))).Visible = false;
            ((Button)(XmlGridView.FooterRow.FindControl("BtnRepDate"))).Visible = true;
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            // Get all fields' values
            string newClaimNumber = ((TextBox)(XmlGridView.FooterRow.FindControl("TxtClaNum"))).Text;
            string newFirstName = ((TextBox)(XmlGridView.FooterRow.FindControl("TxtFName"))).Text;
            string newLastName = ((TextBox)(XmlGridView.FooterRow.FindControl("TxtLName"))).Text;
            string newStatus = ((DropDownList)(XmlGridView.FooterRow.FindControl("DrpStatus"))).SelectedValue;
            DateTime newLossDate = ((Calendar)(XmlGridView.FooterRow.FindControl("CalLossDate"))).SelectedDate;
            string newLossInfoCause = ((DropDownList)(XmlGridView.FooterRow.FindControl("DrpLossInfoCause"))).SelectedValue;
            DateTime newReportedDate = ((Calendar)(XmlGridView.FooterRow.FindControl("CalRepDate"))).SelectedDate;
            string newLossInfoDesc = ((TextBox)(XmlGridView.FooterRow.FindControl("TxtLossInfoDesc"))).Text;

            // Try parsing assigned adjuster ID to check if it's numeric
            string newAssignedAdjStr = ((TextBox)(XmlGridView.FooterRow.FindControl("TxtAssAdjuster"))).Text;
            long newAssignedAdjLong = 0;
            bool assignedAdjIsNum = long.TryParse(newAssignedAdjStr, out newAssignedAdjLong);

            // Validate fields
            if (newClaimNumber == "")
            {
                ResTxt.Text = "Claim number is required. Please enter a claim number.";
                return;
            }

            if (!assignedAdjIsNum ||
                newAssignedAdjStr == "")
            {
                ResTxt.Text = "Assigned adjuster ID must be numeric. Please enter a numeric ID.";
                return;
            }

            // Create Mitchell claim
            MitchellClaim cla = new MitchellClaim()
            {
                ClaimNumber = newClaimNumber,
                ClaimantFirstName = newFirstName,
                ClaimantLastName = newLastName,
                Status = newStatus,
                LossDate = newLossDate,
                LossInfo = new LossInfoType()
                {
                    CauseOfLoss = newLossInfoCause,
                    ReportedDate = newReportedDate,
                    LossDescription = newLossInfoDesc
                },
                AssignedAdjusterID = newAssignedAdjLong,
                Vehicles = new Vehicles()
                {
                    VehicleDetails = new List<VehicleDetails>()
                    {
                        new VehicleDetails()
                    }
                }
            };

            ReqCreateClaim(cla);

            UpdateGrid();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            // Get the button that raised the event
            Button btn = (Button)sender;

            // Get the row that contains this button
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Get the row's claim number
            string claimNumToDel = ((Label)row.Cells[0].FindControl("LblClaNum")).Text;

            //ResTxt.Text = claimToDel;

            // Loop through the bound collection to find that claim
            //MitchellClaim claimToDel = new MitchellClaim();
            //foreach (MitchellClaim c in listCollection)
            //{
            //    if (c.ClaimNumber == claimNumToDel)
            //    {
            //        claimToDel = c;
            //        break;
            //    }
            //}

            ReqDeleteClaim(claimNumToDel);

            UpdateGrid();
        }
        protected void DelAllBtn_Click(object sender, EventArgs e)
        {
            ReqDeleteAllClaims();

            UpdateGrid();
        }
        #endregion

        #region CRUD METHODS
        private MitchellClaim ReqGetClaimByID(string claimNum)
        {
            MitchellClaim ret = new MitchellClaim();

            try
            {
                string url = "http://localhost:35798/RestServiceImpl.svc/claims/" + claimNum; /*22c9c23bac142856018ce14a26b6c299 for George Washington's claim*/

                // Build the GET request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";

                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = sr.ReadToEnd();
                        ResTxt.Text = objText;
                        ret = js.Deserialize<MitchellClaim>(objText);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private List<MitchellClaim> ReqGetAllClaims()
        {
            List<MitchellClaim> ret = new List<MitchellClaim>();

            try
            {
                string url = "http://localhost:35798/RestServiceImpl.svc/claims/all";

                // Build the GET request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";

                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = sr.ReadToEnd();
                        ResTxt.Text = objText;
                        ret = js.Deserialize<List<MitchellClaim>>(objText);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private List<MitchellClaim> ReqGetClaimsInDates(string start, string end)
        {
            //ResTxt.Text = start + "//" + end;
            List<MitchellClaim> ret = new List<MitchellClaim>();

            try
            {
                string url = "http://localhost:35798/RestServiceImpl.svc/claims/range/" + start + "/" + end;

                // Build the GET request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";

                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = sr.ReadToEnd();
                        ResTxt.Text = objText;
                        ret = js.Deserialize<List<MitchellClaim>>(objText);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private void ReqCreateClaim(MitchellClaim cla)
        {
            try
            {
                string url = "http://localhost:35798/RestServiceImpl.svc/claims/create";

                // Build the POST request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/json; charset=utf-8";
                req.Timeout = System.Threading.Timeout.Infinite;
                req.KeepAlive = false;
                req.Headers.Add("SOAPAction", url);

                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(cla);
                    ResTxt.Text = json;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                // Get the request's response
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                ResTxt.Text = "";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ReqDeleteClaim(string claimNum)
        {
            try
            {
                string url = "http://localhost:35798/RestServiceImpl.svc/claims/delete/" + claimNum;

                // Build the POST request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "DELETE";
                req.ContentType = "application/json; charset=utf-8";
                req.Timeout = System.Threading.Timeout.Infinite;
                req.KeepAlive = false;
                req.Headers.Add("SOAPAction", url);

                // Get the request's response
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                ResTxt.Text = "";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ReqDeleteAllClaims()
        {
            try
            {
                string url = "http://localhost:35798/RestServiceImpl.svc/claims/delete/all";

                // Build the POST request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "DELETE";
                req.ContentType = "application/json; charset=utf-8";
                req.Timeout = System.Threading.Timeout.Infinite;
                req.KeepAlive = false;
                req.Headers.Add("SOAPAction", url);

                // Get the request's response
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                ResTxt.Text = "";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        #endregion

        #region UTILITY METHODS
        private void UpdateGrid()
        {
            // Requery for full list of claims
            listCollection = ReqGetAllClaims();

            if (listCollection.Count == 0)
            {
                listCollection.Add(new MitchellClaim()
                {
                    ClaimNumber = "DUMMY",
                    ClaimantFirstName = "EMPTY",
                    ClaimantLastName = "LIST"
                });
            }

            XmlGridView.DataSource = listCollection;
            XmlGridView.DataBind();
        }

        private static byte[] GenerateXMLClaim(MitchellClaim cla)
        {
            // Create the xml document in a memory stream - Recommended
            MemoryStream mStream = new MemoryStream();
            //XmlTextWriter xmlWriter = new XmlTextWriter(@"C:\Employee.xml", Encoding.UTF8);
            XmlTextWriter xmlWriter = new XmlTextWriter(mStream, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;

            //xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("MitchellClaim");

            xmlWriter.WriteStartElement("ClaimNumber");
            xmlWriter.WriteString(cla.ClaimNumber);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("ClaimantFirstName");
            xmlWriter.WriteString(cla.ClaimantFirstName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("ClaimantLastName");
            xmlWriter.WriteString(cla.ClaimantLastName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Status");
            xmlWriter.WriteString(cla.Status);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("LossDate");
            xmlWriter.WriteValue(cla.LossDate);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("LossInfo");
            xmlWriter.WriteStartElement("CauseOfLoss");
            xmlWriter.WriteString(cla.LossInfo.CauseOfLoss);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("ReportedDate");
            xmlWriter.WriteValue(cla.LossInfo.ReportedDate);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("LossDescription");
            xmlWriter.WriteString(cla.LossInfo.LossDescription);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("AssignedAdjusterID");
            xmlWriter.WriteValue(cla.AssignedAdjusterID);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Vehicles");
            foreach (VehicleDetails v in cla.Vehicles.VehicleDetails)
            {
                xmlWriter.WriteStartElement("VehicleDetails");
                xmlWriter.WriteStartElement("ModelYear");
                xmlWriter.WriteValue(v.ModelYear);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("MakeDescription");
                xmlWriter.WriteString(v.MakeDescription);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ModelDescription");
                xmlWriter.WriteString(v.ModelDescription);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("EngineDescription");
                xmlWriter.WriteString(v.EngineDescription);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ExteriorColor");
                xmlWriter.WriteString(v.ExteriorColor);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Vin");
                xmlWriter.WriteString(v.Vin);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("LicPlate");
                xmlWriter.WriteString(v.LicPlate);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("LicPlateState");
                xmlWriter.WriteString(v.LicPlateState);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("LicPlateExpDate");
                xmlWriter.WriteValue(v.LicPlateExpDate);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("DamageDescription");
                xmlWriter.WriteString(v.DamageDescription);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Mileage");
                xmlWriter.WriteValue(v.Mileage);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            //xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
            xmlWriter.Close();
            return mStream.ToArray();
        }
        #endregion

        #region TESTING
        // To run, uncomment in Page_Load
        private bool RunTests()
        {
            string tstMsg = "";

            try
            {
                tstMsg = "[Commencing unit tests.]";

                #region Add 1
                // Clear collection and database
                listCollection.Clear();
                ReqDeleteAllClaims();

                // Add 1
                ReqCreateClaim(CreateArbitrary());
                listCollection = ReqGetAllClaims();
                if (listCollection.Count == 1)
                {
                    tstMsg += "\n[Add 1 passed]";
                }
                else
                {
                    tstMsg += "\n<Failed at [Add 1].>";
                    return false;
                }
                #endregion
                
                #region Add 10 more
                for (int i = 0; i < 10; i++)
                {
                    ReqCreateClaim(CreateArbitrary());
                }
                listCollection = ReqGetAllClaims();
                if (listCollection.Count == 11)
                {
                    tstMsg += "\n[Add 10 more passed]";
                }
                else
                {
                    tstMsg += "\n<Failed at [Add 10 more].>";
                    return false;
                }
                #endregion

                #region Delete all
                ReqDeleteAllClaims();
                listCollection = ReqGetAllClaims();
                if (listCollection.Count == 0)
                {
                    tstMsg += "\n[Delete all passed]";
                }
                else
                {
                    tstMsg += "\n<Failed at [Delete all].>";
                    return false;
                }
                #endregion

                // etc.

                // Success
                tstMsg += "\n[All tests passed!]";
                ResTxt.Text = tstMsg;
                return true;
            }
            catch (Exception ex)
            {
                tstMsg += "\n<Error: " + ex.Message + ">";
                ResTxt.Text = tstMsg;
                // Failed
                return false;
            }
        }

        private MitchellClaim CreateArbitrary()
        {
            // Create arbitrary claim
            MitchellClaim cla = new MitchellClaim()
            {
                ClaimNumber = GenerateRandomString(10),
                ClaimantFirstName = GenerateRandomString(10),
                ClaimantLastName = GenerateRandomString(10),
                Status = "OPEN",
                LossDate = new DateTime(),
                LossInfo = new LossInfoType()
                {
                    CauseOfLoss = "Collision",
                    ReportedDate = new DateTime(),
                    LossDescription = GenerateRandomString(50)
                },
                AssignedAdjusterID = 12345,
                Vehicles = new Vehicles()
                {
                    VehicleDetails = new List<VehicleDetails>()
                        {
                            new VehicleDetails()
                            {
                                ModelYear = 2015,
                                MakeDescription = GenerateRandomString(10),
                                ModelDescription = GenerateRandomString(10),
                                EngineDescription = GenerateRandomString(10),
                                ExteriorColor = GenerateRandomString(10),
                                Vin = GenerateRandomString(10),
                                LicPlate = GenerateRandomString(10),
                                LicPlateState = "CA",
                                LicPlateExpDate = new DateTime(),
                                DamageDescription = GenerateRandomString(50),
                                Mileage = 1000
                            }
                        }
                }
            };

            return cla;
        }

        private MitchellClaim CreateArbitrary(DateTime lossDate)
        {
            // Create arbitrary claim
            MitchellClaim cla = new MitchellClaim()
            {
                ClaimNumber = GenerateRandomString(10),
                ClaimantFirstName = GenerateRandomString(10),
                ClaimantLastName = GenerateRandomString(10),
                Status = "OPEN",
                LossDate = lossDate,
                LossInfo = new LossInfoType()
                {
                    CauseOfLoss = "Collision",
                    ReportedDate = new DateTime(),
                    LossDescription = GenerateRandomString(50)
                },
                AssignedAdjusterID = 12345,
                Vehicles = new Vehicles()
                {
                    VehicleDetails = new List<VehicleDetails>()
                        {
                            new VehicleDetails()
                            {
                                ModelYear = 2015,
                                MakeDescription = GenerateRandomString(10),
                                ModelDescription = GenerateRandomString(10),
                                EngineDescription = GenerateRandomString(10),
                                ExteriorColor = GenerateRandomString(10),
                                Vin = GenerateRandomString(10),
                                LicPlate = GenerateRandomString(10),
                                LicPlateState = "CA",
                                LicPlateExpDate = new DateTime(),
                                DamageDescription = GenerateRandomString(50),
                                Mileage = 1000
                            }
                        }
                }
            };

            return cla;
        }

        private string GenerateRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[rand.Next(chars.Length)];
            }

            return new String(stringChars);
        }
        #endregion
    }
}