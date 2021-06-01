using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Text;

using System.IO;
using Common.Utils;
using Common.Settings;
using Common.Exceptions;
using System.ComponentModel;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Drawing.Imaging;

namespace VisitorManagement_V2
{
    public partial class BasePage : Page
    {
        public BasePage()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //   
        }

        public void DeleteFile(string file)
        {
            if (File.Exists(file))
                File.Delete(file);
        }


        public void populateDropDown(DropDownList ddlControlID, Dictionary<int, string> objDictionary)
        {
            ddlControlID.Items.Clear();
            ddlControlID.Items.Insert(0, new ListItem(Constants.SELECT, "0", true));
            foreach (KeyValuePair<int, string> kp in objDictionary)
            {
                ListItem objListItem = new ListItem();
                objListItem.Value = Convert.ToString(kp.Key);
                objListItem.Text = kp.Value;
                ddlControlID.Items.Add(objListItem);
            }
        }

        public void populateRadioButton(RadioButtonList rdlControlID, Dictionary<int, string> objDictionary)
        {
            rdlControlID.Items.Clear();
            foreach (KeyValuePair<int, string> kp in objDictionary)
            {
                ListItem objListItem = new ListItem();
                objListItem.Value = Convert.ToString(kp.Key);
                objListItem.Text = kp.Value;
                rdlControlID.Items.Add(objListItem);
            }
            rdlControlID.SelectedIndex = 0;
        }

        public void populateDropDown(DropDownList ddlControlID, Dictionary<int, string> objDictionary, bool All)
        {
            ddlControlID.Items.Clear();
            if (!All)
            {
                ddlControlID.Items.Insert(0, new ListItem(Constants.All, "0", true));
            }
            else
            {
                ddlControlID.Items.Insert(0, new ListItem(Constants.All, "0", true));
            }
            foreach (KeyValuePair<int, string> kp in objDictionary)
            {
                ListItem objListItem = new ListItem();
                objListItem.Value = Convert.ToString(kp.Key);
                objListItem.Text = kp.Value;
                ddlControlID.Items.Add(objListItem);
            }
        }

        #region  LoadDays Start
        public void LoadDays(DropDownList ddl, int month)
        {
            ddl.Items.Clear();
            int noOfDays = 0;
            switch (month)
            {
                case 1:
                    noOfDays = 31;
                    break;
                case 2:
                    noOfDays = 29;
                    break;
                case 3:
                    noOfDays = 31;
                    break;
                case 4:
                    noOfDays = 30;
                    break;
                case 5:
                    noOfDays = 31;
                    break;
                case 6:
                    noOfDays = 30;
                    break;
                case 7:
                    noOfDays = 31;
                    break;
                case 8:
                    noOfDays = 31;
                    break;
                case 9:
                    noOfDays = 30;
                    break;
                case 10:
                    noOfDays = 31;
                    break;
                case 11:
                    noOfDays = 30;
                    break;
                case 12:
                    noOfDays = 31;
                    break;
            }

            for (int day = 1; day <= noOfDays; day++)
            {
                ListItem li = new ListItem();
                li.Text = day.ToString();
                li.Value = day.ToString();
                ddl.Items.Add(li);
            }
            ddl.Items.Insert(0, new ListItem(Constants.DAY, "0"));
        }
        #endregion  LoadDays End

        #region  Load Months Start
        public void LoadMonths(DropDownList ddl)
        {
            ddl.Items.Clear();

            ddl.Items.Add(new ListItem("Jan", "01"));
            ddl.Items.Add(new ListItem("Feb", "02"));
            ddl.Items.Add(new ListItem("Mar", "03"));
            ddl.Items.Add(new ListItem("Apr", "04"));
            ddl.Items.Add(new ListItem("May", "05"));
            ddl.Items.Add(new ListItem("Jun", "06"));
            ddl.Items.Add(new ListItem("Jul", "07"));
            ddl.Items.Add(new ListItem("Aug", "08"));
            ddl.Items.Add(new ListItem("Sep", "09"));
            ddl.Items.Add(new ListItem("Oct", "10"));
            ddl.Items.Add(new ListItem("Nov", "11"));
            ddl.Items.Add(new ListItem("Dec", "12"));
            ddl.Items.Insert(0, new ListItem(Constants.MONTH, "0"));
        }
        #endregion  Load Months End

        #region  LoadYears() Start
        public void LoadYears(DropDownList ddl)
        {
            ddl.Items.Clear();
            int currentYear = DateTime.Today.Year;
            for (int year = 1920; year <= currentYear; year++)
            {
                ListItem li = new ListItem();
                li.Text = year.ToString();
                li.Value = year.ToString();
                ddl.Items.Add(li);
            }
            ddl.Items.Insert(0, new ListItem(Constants.YEAR, "0"));
        }
        #endregion   LoadYears() End

        public void ClearScreen()
        {
            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");

            foreach (Control c in contentPlaceHolder.Controls)
            {
                foreach (Control childc in c.Controls)
                {
                    TextBox tb = childc as TextBox;
                    DropDownList ddl = childc as DropDownList;
                    ListBox lb = childc as ListBox;
                    CheckBox cb = childc as CheckBox;

                    if (tb != null)
                        tb.Text = "";
                    if (ddl != null)
                        ddl.SelectedIndex = 0;
                    if (lb != null)
                        lb.SelectedIndex = 0;
                    if (cb != null)
                        cb.Checked = false;
                }
            }
        }

        public void EnableDisableFields(bool value)
        {
            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");

            foreach (Control c in contentPlaceHolder.Controls)
            {
                foreach (Control childc in c.Controls)
                {
                    TextBox tb = childc as TextBox;
                    DropDownList ddl = childc as DropDownList;
                    ListBox lb = childc as ListBox;
                    CheckBox cb = childc as CheckBox;

                    if (tb != null)
                        tb.Enabled = value;
                    if (ddl != null)
                        ddl.Enabled = value;
                    if (lb != null)
                        lb.Enabled = value;
                    if (cb != null)
                        cb.Enabled = value;
                }
            }
        }

        public bool isValidUser(string userName)
        {
            bool _result = true;

            // code for user access

            return _result;
        }

        public void SetTheFocus(Control control)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\r\n<script language='JavaScript'>\r\n");
            sb.Append("<!--\r\n");
            sb.Append("function SetFocus()\r\n");
            sb.Append("{\r\n");
            sb.Append("\tdocument.");

            Control p = control.Parent;
            while (!(p is System.Web.UI.HtmlControls.HtmlForm))
                p = p.Parent;

            sb.Append(p.ClientID);
            sb.Append("['");
            sb.Append(control.UniqueID);
            sb.Append("'].focus();\r\n");
            sb.Append("}\r\n");
            sb.Append("window.onload = SetFocus;\r\n");
            sb.Append("// -->\r\n");
            sb.Append("</script>");

            control.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "SetFocus", sb.ToString());
        }

        protected override void OnError(EventArgs e)
        {
            System.Exception _exception = Server.GetLastError();

            if (_exception.GetBaseException() is System.Web.HttpRequestValidationException)
            {
                Response.Write(GetMessage((int)SystemMessage.InvalidHttpRequest));
                Response.StatusCode = 200;
                Response.End();
            }
        }

        protected string GetMessage(int intErrorCode)
        {
            return ErrorSettings.GetProperty(intErrorCode);
        }

        public void BindReportWithReportViewer(DataTable dt, ReportViewer rv, string reportPath, string Datasource)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtCopy = new DataTable();
                dtCopy = dt.Copy();
                ds.Tables.Add(dtCopy);
                rv.Reset();
                rv.LocalReport.ReportPath = reportPath;
                rv.LocalReport.Refresh();
                rv.LocalReport.DataSources.Add(new ReportDataSource(Datasource, ds.Tables[0]));
            }
            catch (Exception ex)
            {
                AppException.HandleException(ex);
            }
        }

        /********** Convert Generic List into DataTable **************/
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public void RecordsOnLabel(Label lblCurrentPage, int pagesize, int pageindex, int Totalcount)
        {
            int p, q = 0;
            p = (pageindex + 1) * pagesize;
            q = Totalcount == 0 ? 0 : p - (pagesize - 1);

            if (Totalcount > 0)
            {
                if ((pageindex + 1) * pagesize < Totalcount)
                {
                    lblCurrentPage.Text = "Displaying " + Convert.ToString(q) + " - " + Convert.ToString(p)  /*+ pds.PageCount.ToString() */+ " Out Of " + Totalcount.ToString() + " Records ";
                }
                else
                {
                    lblCurrentPage.Text = "Displaying " + Convert.ToString(q) + " - " + Totalcount.ToString()  /*+ pds.PageCount.ToString() */+ " Out Of " + Totalcount.ToString() + " Records ";
                }
            }
            else
            {
                lblCurrentPage.Text = string.Empty;
            }
        }

        public void navigationDisplay(LinkButton btnNext, LinkButton btnPrevious, int Totalcount, int pageindex, int pagesize)
        {
            try
            {

                //int pagesize = Convert.ToInt32(ConfigSettings.GetProperty("RowSizeInRepeater"));
                //Totalcount = Convert.ToInt32(hdfTotalCount.Value);
                if (pageindex != 0)
                {
                    btnPrevious.Visible = true;
                    btnPrevious.Enabled = true;
                }
                else
                {
                    btnPrevious.Visible = false;
                    btnPrevious.Enabled = false;

                }

                if ((pageindex + 1) * pagesize < Totalcount)
                {
                    btnNext.Visible = true;
                    btnNext.Enabled = true;
                }
                else
                {
                    btnNext.Visible = false;
                    btnNext.Enabled = false;
                }

                //string message = ErrorSettings.GetProperty((int)GeneralMessage.AllEmployee);
                // message = message.Replace(Constants.REPLACE_CHARACTER, Convert.ToString(_totalCount));
                //ltrEmployeeCount.Text = message;
            }
            catch
            {

                throw;
            }

        }

        //Redirect to Home Page. Depending upon UserType redirect to Home page accordingly
        public void RedirectToHomePage(Int16? UserTypeID = null)
        {
            if (UserTypeID == null || UserTypeID == 0)
            {
                UserTypeID=Convert.ToInt16((Session[Constants.USER_TYPEID]));
            }

            try
            {
                if (UserTypeID == (int)UserType.Admin)
                {
                    HttpContext.Current.Response.Redirect("../Users/UserDetails.aspx", false);
                }
                else if (UserTypeID == (int)UserType.Requester)
                {
                    HttpContext.Current.Response.Redirect("../Registration/CreateVisitRequest.aspx", false);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("../Dashboard/DefaultDashboard.aspx", false);
                }
            }
            catch
            {
                throw;

            }
        }

        public string SaveFileFromUploadControl(FileUpload fileupload,string saveDirectoryPath,string uniqkey)
        {
            try
            {
                string fileName =Path.GetFileName(fileupload.PostedFile.FileName);
                if (!string.IsNullOrEmpty(fileName))
                {
                    Directory.CreateDirectory(saveDirectoryPath);
                    string newFileName = Path.GetFileNameWithoutExtension(fileName) + uniqkey + Path.GetExtension(fileName);
                    string savePath = Path.Combine(saveDirectoryPath, newFileName);                    
                   // fileupload.PostedFile.SaveAs(savePath);                    
                    VaryQualityLevel(fileupload.PostedFile.InputStream, savePath);
                    return newFileName;
                }
                else
                    return null;
            }
            catch
            {                
                throw;
            }
        }
        //public string SaveFileFromImageControl(string filename, string saveDirectoryPath, string uniqkey)
        //{
        //    try
        //    {
        //        string fileName = Path.GetFileName(filename);
        //        if (!string.IsNullOrEmpty(fileName))
        //        {
        //            Directory.CreateDirectory(saveDirectoryPath);
        //            string newFileName = Path.GetFileNameWithoutExtension(fileName) + uniqkey + Path.GetExtension(fileName);
        //            string savePath = Path.Combine(saveDirectoryPath, newFileName);
        //            // fileupload.PostedFile.SaveAs(savePath);                    
        //          //  VaryQualityLevel(fileupload.PostedFile.InputStream, savePath);
        //            return newFileName;
        //        }
        //        else
        //            return null;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private void VaryQualityLevel(Stream imageInputStream,string savePath)
        {
            try
            {
                // Get a bitmap.
                Bitmap bmp1 = new Bitmap(imageInputStream);
                ImageCodecInfo jpgEncoder = GetEncoder(bmp1.RawFormat);

                // Create an Encoder object based on the GUID
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.
                // An EncoderParameters object has an array of EncoderParameter
                // objects. In this case, there is only one
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                // Save the bitmap as a JPG file with 50L(50%) quality level compression.
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(savePath, jpgEncoder, myEncoderParameters);
            }
            catch
            {                
                throw;
            }                        
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            try
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == format.Guid)
                    {
                        return codec;
                    }
                }
                return null;
            }
            catch
            {                
                throw;
            }
        }

        public string getVisitorPhotoURL(string filename)
        {
            string FileURL = string.Empty;
            try
            {
                FileURL = ConfigSettings.GetProperty(Constants.PhotoURLPath) + filename;                
            }
            catch
            {                
                throw;
            }
            return FileURL;
        }

        /********* To Open Rdlc Report in PDF Format by Default **********/
        public void CreatePDF(ReportViewer rptViewer)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                //string fileName = rptViewer.LocalReport.DataSources[0].Name;
                string fileName=string.Empty;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = "Default";
                }

                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;

                rptViewer.ProcessingMode = ProcessingMode.Local;

                byte[] bytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "filename=" + fileName + "." + extension);
                Response.BinaryWrite(bytes); // create the file        
                Response.Flush(); // send it to the client to download 
            }
            catch (Exception ex)
            {
                AppException.HandleException(ex);
            }
        }
    }
}



