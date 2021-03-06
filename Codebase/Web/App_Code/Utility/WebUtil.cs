﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using System.Web.Security;
//using App.Core.Base.Model;
using App.Core.Extensions;
using System.Net.Mail;
using App.Data;
//using App.DAL.Utility;
//using App.DAL;

/// <summary>
/// Summary description for WebUtil
/// </summary>
public class WebUtil 
{    
    public static string GetPageTitle(string title)
    {
        return string.Format("OMM Online : {0}", title);
    }
    /// <summary>
    /// Encodes text to show in the web page
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Encode(String text)
    {
        return HttpContext.Current.Server.HtmlEncode(text);
    }
    /// <summary>
    /// Encodes text and replaces newline to show in web pages
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static String FormatText(String text)
    {
        if (text.IsNullOrEmpty())
            return String.Empty;
        return Encode(text).Replace(Environment.NewLine, "<br />").Replace("\n", "<br />");
    }
    /// <summary>
    /// Builds a DateTime Object from a BD Format Date. (24/10/2009)
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime GetDate(String date)
    {
        if (!date.IsNullOrEmpty())
        {           
            return DateTime.ParseExact(date, ConfigReader.CSharpCalendarDateFormat, null);            
        }
        return DateTime.MinValue.SqlDateTimeMinValue();
    }
    /// <summary>
    /// Checks Whether an Uploaded file is a Valid Image File Or Not
    /// </summary>
    /// <param name="httpPostedFile"></param>
    /// <returns></returns>
    public static bool IsValidImageFile(HttpPostedFile httpPostedFile)
    {
        if (httpPostedFile.ContentLength > 0)
        {
            string extension = Path.GetExtension(httpPostedFile.FileName);
            return IsValidImageFileExtension(extension);
        }
        return false;
    }
    /// <summary>
    /// Checks for a valid file extension for an image.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    private static bool IsValidImageFileExtension(string extension)
    {
        if (String.Compare(extension, ".jpg", true) == 0)
            return true;
        else if (String.Compare(extension, ".jpeg", true) == 0)
            return true;
        else if (String.Compare(extension, ".gif", true) == 0)
            return true;
        else if (String.Compare(extension, ".png", true) == 0)
            return true;
        return false;
    }
    /// <summary>
    /// Prepares a DIV box to show as a message box in the UI
    /// </summary>
    /// <param name="divMessage"></param>
    /// <param name="message"></param>
    /// <param name="isErrorMessage"></param>
    public static void ShowMessageBox(HtmlGenericControl divMessage, string message, bool isErrorMessage)
    {
        divMessage.InnerHtml = message;
        divMessage.Attributes["class"] = isErrorMessage ? AppConstants.UI.ERROR_MESSAGE_BOX_CLASS : AppConstants.UI.MESSAGE_BOX_CLASS;
        divMessage.Visible = true;
    }
    /// <summary>
    /// Reads HTML Template from File System
    /// </summary>
    /// <param name="templateFileName"></param>
    /// <returns></returns>
    public static String ReadEmailTemplate(String templateFileName)
    {
        String filePath = HttpContext.Current.Server.MapPath("/EmailTemplates");
        filePath = Path.Combine(filePath, templateFileName);
        if(File.Exists(filePath))
            return File.ReadAllText(filePath);
        throw new FileNotFoundException(String.Format("The Email Template: {0} was not found.", templateFileName));
    }
    /// <summary>
    /// Gets Currently Hosted Domain Address
    /// </summary>
    /// <returns></returns>
    public static String GetDomainAddress()
    {
        return String.Format("http://{0}/", HttpContext.Current.Request.Url.Host);
    }

    public static String GetContentDetailsUrl(int contentId)
    {
        return String.Format("{0}?{1}={2}", AppConstants.Pages.SHOW_CONTENT, AppConstants.QueryString.ID, contentId);
    }
    /// <summary>
    /// Gets Completed External Absolute URL
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string GetCompleteUrl(String url)
    {
        return url.ToLower().StartsWith("http://") ? url : String.Format("http://{0}", url);
    }
    /// <summary>
    /// Gets the IP Address of the Requested Client
    /// </summary>
    /// <returns></returns>
    public static string GetRemoteIPAddress()
    {
        return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    }
    /// <summary>
    /// Signouts the user.
    /// </summary>
    public static void SignoutUser()
    {
        SessionCache.ClearSession();
        FormsAuthentication.SignOut();
        HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl);
    }

    /// <summary>
    /// Gets the request param value in long.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static long GetQueryStringInLong(String key)
    {
        long paramValue = 0;
        long.TryParse(HttpContext.Current.Request[key], out paramValue);

        return paramValue;
    }

    /// <summary>
    /// Gets the request param value in byte.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static byte GetQueryStringInByte(String key)
    {
        byte paramValue = 0;
        byte.TryParse(HttpContext.Current.Request[key], out paramValue);

        return paramValue;
    }

    /// <summary>
    /// Gets the request param value in int.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static int GetQueryStringInInt(String key)
    {
        int paramValue = 0;
        int.TryParse(HttpContext.Current.Request[key], out paramValue);

        return paramValue;
    }

    /// <summary>
    /// Gets the request param value in string.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static string GetQueryStringInString(String key)
    {
        string paramValue = HttpContext.Current.Request[key];
        return paramValue;
    }

    /// <summary>
    /// Logins the user.
    /// </summary>
    public static void LoginUser()
    {        
        if (HttpContext.Current.User.Identity.IsAuthenticated && SessionCache.CurrentUser == null)
        {
            String userName = HttpContext.Current.User.Identity.Name;

            OMMDataContext dataContext = new OMMDataContext();
            //var user = dataContext.Users.SingleOrDefault(U => U.UserNameWeb == userName);///For Forms Authentication
            var user = dataContext.Users.SingleOrDefault(U => String.Compare(U.UserName, userName, true) == 0); ///For Windows Authentication            
            if (user != null)
            {
                SessionCache.CurrentUser = user;
            }
        }
    }

    public static int[] GetIntArray(string receiPientsList)
    {
        if (!receiPientsList.IsNullOrEmpty())
        {
            String[] ids = receiPientsList.Split(',');
            IList<int> intIds = new List<int>();            
            foreach (String id in ids)
            {
                int value = 0;
                int.TryParse(id, out value);
                if (value > 0)
                    intIds.Add(value);
            }
            return intIds.ToArray();
        }
        return null;
    }
    public static String GetFormattedFileName(String fileName)
    {
        if (!fileName.IsNullOrEmpty())
        {
            if (fileName.IndexOf('_') > -1)
            {
                String[] nameParts = fileName.Split('_');
                StringBuilder sb = new StringBuilder(10);
                for (int i = 0; i < nameParts.Length; i++)
                {
                    if (i > 0)
                        sb.Append(nameParts[i]);
                }
                return sb.ToString();
            }
            return fileName;
        }
        return String.Empty;
    }
    /// <summary>
    /// Binds a Dropdownlist Controls to a Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ddl"></param>
    /// <param name="entities"></param>
    /// <param name="displayField"></param>
    /// <param name="valueField"></param>
    //public static void BindDropDownList<T>(DropDownList ddl, IList<T> entities, String displayField, String valueField)       
    //    where T : BaseEntity
    //{
    //    if (entities == null || entities.Count == 0)
    //    {
    //        if (ddl.Items.Count > 0)
    //            ddl.Items.Clear();
    //        ddl.DataSource = null;
    //        ddl.DataBind();            
    //    }        
    //    else
    //    {
    //        ddl.DataSource = entities;
    //        ddl.DataTextField = displayField;
    //        ddl.DataValueField = valueField;
    //        ddl.DataBind();
    //    }
    //    ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
    //}
    /// <summary>
    /// Binds a Dropdownlist to a DataTable
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="dt"></param>
    /// <param name="displayField"></param>
    /// <param name="valueField"></param>
    //public static void BindDropDownList(DropDownList ddl, DataTable dt, String displayField, String valueField)
    //{
       
    //    if (dt == null || dt.Rows.Count <= 0)
    //    {
    //        if (ddl.Items.Count > 0)
    //            ddl.Items.Clear();
    //        ddl.DataSource = null;
    //        ddl.DataBind();
    //    }
    //    else
    //    {
    //        ddl.DataSource = dt.DefaultView;
    //        ddl.DataTextField = displayField;
    //        ddl.DataValueField = valueField;
    //        ddl.DataBind();
    //    }
    //    ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));        
    //}
    /// <summary>
    /// Adds Client Side Confirmation Text to a Server Side Button Control according to the Validation Group
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="message"></param>
    /// <param name="isSaveButton"></param>
    //public static void AddCorfirmation(Button btn, String message, bool isSaveButton)
    //{
    //    if (isSaveButton)
    //    {
    //        if (!btn.ValidationGroup.IsNullOrEmpty())
    //            btn.Attributes.Add("ValidationGroupName", btn.ValidationGroup);

    //        btn.Attributes.Add(AppConstants.CONFIRMATION_MSG_CUSTOM_ATTR, message);
    //        btn.Attributes["onclick"] = "return ShowSaveConfirmation(this);";            
    //    }
    //    else
    //        btn.Attributes["onclick"] = String.Format("return confirm('{0}');", message);
    //}
    /// <summary>
    /// Creates a Uniques UserID from Organization Code and UserID 
    /// </summary>
    /// <param name="adminSystemUser"></param>
    /// <returns></returns>
    //public static string GetUserID(App.Models.Admin.AdminSystemUser adminSystemUser)
    //{
    //    return String.Format("{0}{1}", adminSystemUser.OrgCode, adminSystemUser.UserID);
    //}

    //public static String GetDeleteConfirmation()
    //{
    //    return String.Format("onclick=\"return confirm('Sure to Delete?')\"");
    //}

    //public static String GetEditPermissionDeniedMessage()
    //{
    //    return String.Format("javascript:alert('{0}');javascript:void(0);", AppConstants.Message.EDIT_PERMISSION_DENIED);
    //}
    //public static String GetDeletePermissionDeniedMessage()
    //{
    //    return String.Format("javascript:alert('{0}');javascript:void(0);", AppConstants.Message.DELETE_PERMISSION_DENIED);
    //}

    /// <summary>
    /// Returns Formatted Data Returned from a Data Reader or a Data Table Cell
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static String FormatData(object data)
    {
        if (data.GetType() == typeof(String))
            return WebUtil.FormatText(NullHandler.GetString(data));
        else if (data.GetType() == typeof(DateTime))
            return NullHandler.GetDateTime(data).ToString(AppConstants.ValueOf.DATE_FROMAT_DISPLAY);
        else if (data.GetType() == typeof(decimal))
            return String.Format(AppConstants.ValueOf.DECIMAL_FORMAT, NullHandler.GetDecimal(data));
        else if (data.GetType() == typeof(bool))
            return NullHandler.GetBoolean(data) ? "Yes" : "No";
        else
            return NullHandler.GetString(data);
    }

    #region Email Helper
    public static bool SendMail(string mailTo, string mailCc, string mailBcc, string mailFrom, string mailSubject, string mailBody)
    {
        try
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(mailFrom);

                //Spliting the to addresses by ','
                string[] emailAddesses = mailTo.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string to in emailAddesses)
                {
                    mailMessage.To.Add(new MailAddress(to.Trim()));
                }

                //Spliting the cc Adresses by ','
                string[] ccAddresses = mailCc.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string cc in ccAddresses)
                {
                    mailMessage.CC.Add(new MailAddress(cc.Trim()));
                }

                //determining the BCC of the mail.
                string[] bccAddresses = mailBcc.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string bcc in bccAddresses)
                {
                    mailMessage.Bcc.Add(new MailAddress(bcc.Trim()));
                }

                mailMessage.Subject = mailSubject;
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;

                //sending the mail.
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = ConfigReader.SmtpHost;//SmtpHost; //smtpHost;
                smtpClient.Port = ConfigReader.SmtpPort; //smtpPort;
                smtpClient.Send(mailMessage);
                return true;
            }
        }
        catch //(Exception ex)
        {
            //Exception excToUse = ex.InnerException ?? ex;
            //throw new CommunicationException(excToUse.Message, excToUse);
        }
        return false;
    }

    public static bool SendEmailThroughGmail(String toEmail, String subject, String body)
    {
        var fromAddress = new MailAddress("btcrealestate@gmail.com"
                                        , "OMM");
        var toAddress = new MailAddress(toEmail, String.Empty);
        String fromPassword = "NoPassword";
        //String subject = "Subject";
        //const string body = "Body";

        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
        };
        using (var message = new MailMessage(fromAddress, toAddress)
        {
            IsBodyHtml = true,
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
        return true;
    }
    #endregion Email helper
}
