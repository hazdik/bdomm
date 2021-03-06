
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using App.Core.Extensions;
using App.Data;


public partial class Pages_PersonnelBankDetails_ : BasePage
{
    private int _ContactID = 0;
    private int _ID = 0;
    private bool _IsEditMode = false;
    private bool _IsDeleteMode = false;
    private const int PAGE_SIZE = 15;

    protected void Page_Load(object sender, EventArgs e)
    {
        BindPageInfo();
        if (!IsPostBack)
        {
            CheckAndDeleteData();
            BindDropDownLists();
            BindContactsNotesInfo();
            BindNotesList(1);
            ShowSuccessMessage();
        }
    }
    
    /// <summary>
    /// Bindis the Page Initialization Variables
    /// </summary>
    protected void BindPageInfo()
    {
        _ID = WebUtil.GetQueryStringInInt(AppConstants.QueryString.ID);
        _ContactID = WebUtil.GetQueryStringInInt(AppConstants.QueryString.CONTACT_ID);
        _IsDeleteMode = String.Compare(WebUtil.GetQueryStringInString(AppConstants.QueryString.DELETE), "True", true) == 0 ? true : false;
        if (_ID > 0 && !_IsDeleteMode)        
            _IsEditMode = true;
        
        Page.Title = WebUtil.GetPageTitle("Manage Notes");
    }

    /// <summary>
    /// Binds Dropdownlists for the initial request.
    /// </summary>
    protected void BindDropDownLists()
    {
        //BindDropdownList.Contactses(ddlContactID);
        //BindDropdownList.Userses(ddlChangedByUserID);
    }
    protected void ShowSuccessMessage()
    {
        if (String.Compare(WebUtil.GetQueryStringInString(AppConstants.QueryString.SUCCESS_MSG), "True", false) == 0)
            WebUtil.ShowMessageBox(divMessage, "Bank Details Saved Successfully.", false);
    }
    protected void CheckAndDeleteData()
    {
        if (_IsDeleteMode)
        {
            OMMDataContext context = new OMMDataContext();
            var bankDetails = context.BankDetails.FirstOrDefault(P => P.ID == _ID && P.ContactID == _ContactID);
            if (bankDetails == null)
                WebUtil.ShowMessageBox(divMessage, "Sorry! requested Bank Details not found for delete. Delete Failed.", true);
            else
            {
                context.BankDetails.DeleteOnSubmit(bankDetails);
                try
                {
                    context.SubmitChanges();
                    WebUtil.ShowMessageBox(divMessage, "Bank Details deleted successfully.", false);
                }
                catch
                {
                    WebUtil.ShowMessageBox(divMessage, "Sorry! this note contains related information. Delete failed.", true);
                }
            }
        }
    }
    protected void BindNotesList(int pageNumber)
    {
        UtilityDAO dao = new UtilityDAO();
        DbParameter[] parameters = new[] { new DbParameter("@ContactID", _ContactID) };
        int totalRecord = 0;
        //DataSet ds = dao.GetPagedData(AppSQL.GET_BANK_DETAILS_BY_CONTACT, parameters, pageNumber, PAGE_SIZE, out totalRecord);
        DataSet ds = dao.GetDataSet(AppSQL.GET_BANK_DETAILS_BY_CONTACT, parameters, false);
        
        //Bind the List Control
        ucNoteList.DataSource = ds.Tables[0];
        ucNoteList.EditLink = Request.Url.AbsolutePath + "?" + AppConstants.QueryString.CONTACT_ID + "={0}&" + AppConstants.QueryString.ID + "={1}";
        ucNoteList.DeleteLink = Request.Url.AbsolutePath + "?" + AppConstants.QueryString.CONTACT_ID + "={0}&" + AppConstants.QueryString.ID + "={1}&" + AppConstants.QueryString.DELETE + "=True";
        ucNoteList.DataBind();

        ///Bind the Pager Control
        ucNoteListPager.TotalRecord = totalRecord;
        ucNoteListPager.PageNo = pageNumber;
        ucNoteListPager.PageSize = PAGE_SIZE;
        ucNoteListPager.DataBind();
    }
    /// <summary>
    /// Binds ContactsNotes Info Requested through Query Strings
    /// </summary>
    protected void BindContactsNotesInfo()
    {
        OMMDataContext context = new OMMDataContext();
        if (context.BankDetails.FirstOrDefault(P => P.ID == _ContactID) == null)
            ShowNotFoundMessage();
        else
        {
            if (_IsEditMode)
            {                
                BankDetail entity = context.BankDetails.FirstOrDefault(P => P.ID == _ID && P.ContactID == _ContactID);//dao.GetByID(_ID);
                if (entity == null)
                    ShowNotFoundMessage();
                else
                {
                    //ddlContactID.SetSelectedItem(entity.ContactID.ToString());
                    //txtNotes.Text = entity.Notes;
                    //ddlChangedByUserID.SetSelectedItem(entity.ChangedByUserID.ToString());
                    //txtChangedOn.Text = entity.ChangedOn.ToString(ConfigReader.CSharpCalendarDateFormat);
                    //txtVersion.Text = entity.Version;
                    //txtCreatedByUsername.Text = entity.CreatedByUsername;
                    //txtChangedByUsername.Text = entity.ChangedByUsername;

                    tbxBankName.Text = entity.BankName;
                    tbxBranchName.Text = entity.BranchName;
                    tbxBranchAddress.Text = entity.BranchAddress;
                    tbxSortCode.Text = entity.SortCode;
                    tbxAccNumber.Text = entity.AccountNumber;
                    tbxAccName.Text = entity.AccountName;
                    tbxBicCode.Text = entity.BicCode;
                    tbxAbaCode.Text = entity.AbaCode;
                }
            }
        }
    }
    /// <summary>
    /// Shows a Message in the UI and Hides the Data Editing Controls
    /// </summary>
    protected void ShowNotFoundMessage()
    {
        pnlFormContainer.Visible = false;
        WebUtil.ShowMessageBox(divMessage, "Requested Bank Details was not found.", true);
    }
    protected void SaveContactsNotes()
    {
        OMMDataContext context = new OMMDataContext();
        BankDetail entity = null;

        if (_IsEditMode)
            entity = context.BankDetails.FirstOrDefault(P => P.ID == _ID && P.ContactID == _ContactID); //dao.GetByID(_ID);        
        else
        {
            entity = new BankDetail();
            entity.ContactID = _ContactID;
            context.BankDetails.InsertOnSubmit(entity);
        }

        //ddlContactID.SelectedValue.ToInt();
        entity.BankName = tbxBankName.Text;
        entity.BranchName = tbxBranchName.Text;
        entity.BranchAddress = tbxBranchAddress.Text;
        entity.SortCode = tbxSortCode.Text;
        entity.AccountNumber = tbxAccNumber.Text;
        entity.AccountName = tbxAccName.Text;
        entity.BicCode = tbxBicCode.Text;
        entity.AbaCode = tbxAbaCode.Text;

        entity.ChangedByUserId = SessionCache.CurrentUser.ID;
        entity.ChangedOn = DateTime.Now;        
        //entity = entity.ChangedByUsername = SessionCache.CurrentUser.UserName;

        context.SubmitChanges();
        String url = String.Format("{0}?{1}={2}&{3}=True"
            , Request.Url.AbsolutePath
            , AppConstants.QueryString.CONTACT_ID
            , _ContactID
            , AppConstants.QueryString.SUCCESS_MSG);
        Response.Redirect(url);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SaveContactsNotes();
            //Response.Redirect(AppConstants.Pages.CONTACTSNOTES_LIST);
            return;
        }
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        //Response.Redirect(AppConstants.Pages.CONTACTSNOTES_LIST);
        //return;
    }
    protected void ucNoteListPager_PageIndexChanged(object sender, PagerEventArgs e)
    {
        BindNotesList(e.PageIndex);
    }
}














