﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Pages_QuotationDecision : BasePage
{
    protected int _QuotationID = 0;
    protected int _EnquiryID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        BindPageInfo();
        if (!IsPostBack)
        {
            BindQuotationsInfo();
        }
    }
    /// <summary>
    /// Bindis the Page Initialization Variables
    /// </summary>
    protected void BindPageInfo()
    {
        _QuotationID = WebUtil.GetQueryStringInInt(AppConstants.QueryString.ID);

        if (_QuotationID == 0)
        {
            //ltrHeading.Text = "Edit Quotations Wizard";
            //ShowErroMessag("Requested Quotation ");
        }
        Page.Title = WebUtil.GetPageTitle(ltrHeading.Text);
        this.Master.SelectedTab = SelectedTab.Project;
    }
    /// <summary>
    /// Binds Quotations Info Requested through Query Strings
    /// </summary>
    protected void BindQuotationsInfo()
    {
        OMMDataContext dataContext = new OMMDataContext();
        ///Bind Quotation First
        Quotation quotation = dataContext.Quotations.SingleOrDefault(E => E.ID == _QuotationID);
        if (quotation == null)
        {
            ShowErroMessag("Q", String.Empty);
            return;
        }
        else
        {
            ltrHeading.Text = String.Format("Quotation Decision Wizard - Quotation {0} Enquiry {1}", quotation.Number, quotation.Enquiry.Number);
            if (quotation.StatusID != App.CustomModels.QuotationStatus.Submitted)
            {
                ShowErroMessag("V", quotation.Number);
                return;
            }
            _EnquiryID = quotation.EnquiryID;
        }
        Page.Title = ltrHeading.Text;
    }
    /// <summary>
    /// Shows a Message in the UI and Hides the Data Editing Controls
    /// </summary>
    protected void ShowErroMessag(String msgType, String quotationNumber)
    {
        pnlDetails.Visible = false;
        if (msgType == "Q")
            WebUtil.ShowMessageBox(divMessage, "Sorry! requested Quotation was not found.", true);
        else if (msgType == "V")
            WebUtil.ShowMessageBox(divMessage, String.Format("Sorry! You cannot take Decision against this type of Quotation. Quotation Number is {0}", quotationNumber), true);
        else if (msgType == "VC")
            WebUtil.ShowMessageBox(divMessage, String.Format("Sorry! Enquiry {0} has been closed .", quotationNumber), true);
    }
    /// <summary>
    /// Changes the Status of a Quotation
    /// </summary>
    /// <param name="quotationID"></param>
    /// <param name="decision"></param>
    /// <returns></returns>
    [WebMethod]
    public static String SaveDecision(int quotationID, int decision)
    {
        OMMDataContext dataContext = new OMMDataContext();
        var quotation = dataContext.Quotations.SingleOrDefault(Q => Q.ID == quotationID);
        if (quotation != null)
        {
            quotation.StatusID = decision;
            ///If Requote is Requested for this quotation
            if (decision == App.CustomModels.QuotationStatus.ReQquoteRequested)
            {
                quotation.Number = dataContext.GenerateNewQuotationNumber(quotation.EnquiryID, true);
                quotation.StatusID = App.CustomModels.QuotationStatus.NotSubmitted;
            }

            quotation.ChangedByUserID = SessionCache.CurrentUser.ID;
            quotation.ChangedByUsername = SessionCache.CurrentUser.UserName; //SessionCache.CurrentUser.UserNameWeb;
            quotation.ChangedOn = DateTime.Now;
            //if (decision == App.CustomModels.QuotationStatus.Successful || decision == App.CustomModels.QuotationStatus.Unsuccessful)
            //    quotation.Enquiry.StatusID = App.CustomModels.EnquiryStatus.Closed;
            //else if (decision == App.CustomModels.QuotationStatus.ReQquoteRequested)
            //{
            //    //Create a New Quotation for this Enquiry with this objects data

            //}
            //if (decision == App.CustomModels.QuotationStatus.Successful)
            //    quotation.Projects.Add(CreateProjectFromQuotation(quotation, dataContext));

            dataContext.SubmitChanges();
            return String.Format("{0}:{1}", quotation.ID, quotation.Number);
        }
        return String.Empty;
    }

    private static Project CreateProjectFromQuotation(Quotation quotation, OMMDataContext dataContext)
    {
        Project project = new Project();       
        project.Description = quotation.Enquiry.EnguirySubject;
        project.ChangedByUserID = project.CreatedByUserID = SessionCache.CurrentUser.ID;
        project.CreatedByUsername = project.ChangedByUsername = SessionCache.CurrentUser.UserName; //SessionCache.CurrentUser.UserNameWeb;
        project.CreatedOn = project.ChangedOn = DateTime.Now;
        project.StatusID = App.CustomModels.ProjectStatus.InProgress;
        project.StartDate = DateTime.Now;
        project.EndDate = DateTime.Now.AddDays(quotation.ValidityDays);
        project.Number = dataContext.GenerateNewProjectNumber();
        project.Name = quotation.Enquiry.ClientContact.Name;
        return project;
    }
}