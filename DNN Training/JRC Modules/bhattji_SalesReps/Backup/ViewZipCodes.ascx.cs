// Copyright (c) 2002-2005
// by Jaydeep Bhatt, Vision Consultants. ( http://www.bhattji.com )
//
// Permission is hereby granted, to the person obtaining a copy of this software legaly and associated
// documentation files (the "Software"), to deal in the Software with restriction, including with limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//

using System;
using System.Drawing;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using bhattji.Modules.SalesReps.Business;
using DotNetNuke;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common.Utilities;

namespace bhattji.Modules.SalesReps
{

	/// -----------------------------------------------------------------------------
	/// <summary>
	/// The Links Class provides the UI for displaying the Links
	/// </summary>
	///
	/// <remarks>
	/// </remarks>
	/// <history>
	/// 	[bhattji]	9/23/2004	Moved Links to a separate Project
	///		[bhattji]	10/20/2004	Removed ViewOptions from Action menu
	/// </history>
	/// -----------------------------------------------------------------------------
	///

    public abstract partial class ViewZipCodes : DotNetNuke.Entities.Modules.PortalModuleBase
	{

		#region " Private Members "

		OptionsInfo objOI;

		#endregion

		#region " Public Members "

		public int itemId;
		//Public parentId As String
		public bool Embeded;

		#endregion

		#region " Public Methods "

		public void LoadPage(bool PageIsPostBack)
		{
			objOI = new OptionsInfo(ModuleId);

			InitEveryTime();

			if (!PageIsPostBack)
			{
				InitFirstTime();
			}

		}

		private void InitEveryTime()
		{
			//this needs to execute always, even on Postback, to the client script code is registred in InvokePopupCal
			//hypCalandarFromDate.NavigateUrl = DotNetNuke.Common.Utilities.Calendar.InvokePopupCal(txtFrom)
			//hypCalandarToDate.NavigateUrl = DotNetNuke.Common.Utilities.Calendar.InvokePopupCal(txtTo)

			//Bind the ObjectDataSource everytime, even on Postback
			SetODS();
		}

		private void InitFirstTime()
		{
			SetIconBar();
			//hypClose.Visible = Not Embeded
			//hypImgClose.Visible = hypClose.Visible

			//If hypClose.Visible Then
			//    If (Not Request.Params("LoadID") Is Nothing) OrElse (Request.Params("LoadID") <> "") Then
			//        hypClose.NavigateUrl = EditUrl("LoadID", Request.QueryString("LoadID"))
			//    Else
			//        hypClose.NavigateUrl = NavigateURL()
			//    End If
			//    hypImgClose.NavigateUrl = hypClose.NavigateUrl
			//End If

			SetGridView();

			//BindCategories()
			//BindSearchData()

			ResetViewStates();
			ShowHide();
		}

		private void SetIconBar()
		{
			//Give the ImageUrl
			imbAdd.ImageUrl = ModulePath + "img/bhattji_Add.png";
			imbUpdate.ImageUrl = ModulePath + "img/bhattji_Update.png";
			//imbDelete.ImageUrl = ModulePath & "img/bhattji_Delete.png"
			imbCancel.ImageUrl = ModulePath + "img/bhattji_Cancel.png";

			//Give the Tooltip
			cmdAdd.ToolTip = Localization.GetString("Add");
			//cmdAdd.Text
			cmdUpdate.ToolTip = Localization.GetString("cmdUpdate");
			//cmdUpdate.Text
			//cmdDelete.ToolTip = Localization.GetString("cmdDelete") 'cmdDelete.Text
			cmdCancel.ToolTip = Localization.GetString("cmdCancel");
			//cmdCancel.Text

			//cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & Localization.GetString("DeleteItem") & "');")
			//imbDelete.Attributes.Add("onClick", "javascript:return confirm('" & Localization.GetString("DeleteItem") & "');")

			hypClose.Visible = !Embeded;
			hypImgClose.Visible = hypClose.Visible;
			//divClose.Visible = Not Embeded

			if (hypClose.Visible)
			{
				if (((Request.Params["LoadID"] != null)) || (Request.Params["LoadID"] != ""))
				{
					hypClose.NavigateUrl = EditUrl("LoadID", Request.QueryString["LoadID"]);
				}
				else
				{
					hypClose.NavigateUrl = Globals.NavigateURL();
				}
				hypClose.ToolTip = Localization.GetString("Close");
				//hypClose.Text

				hypImgClose.ImageUrl = ModulePath + "img/bhattji_Close.png";
				hypImgClose.ToolTip = hypClose.ToolTip;
				hypImgClose.NavigateUrl = hypClose.NavigateUrl;
			}
		}
		//SetIconBar()

        private void AddUpdateZipCode()
        {
            Business.ZipCodeInfo objZipCode = new Business.ZipCodeInfo();
            //Initialise the ojbZipCode object
            objZipCode = (Business.ZipCodeInfo)CBO.InitializeObject(objZipCode, typeof(Business.ZipCodeInfo));

            //bind text values to object
            {
                objZipCode.ItemId = Null.NullInteger;
                try
                {
                    if (lblItemId.Text != "") objZipCode.ItemId = Convert.ToInt16(lblItemId.Text);
                    //itemId
                }
                catch
                {
                }
                //.parentId = parentId

                objZipCode.ZipCode = txtZipCode.Text;
                objZipCode.Area = txtArea.Text;
                objZipCode.City = txtCity.Text;
                objZipCode.StateCode = txtStateCode.Text;
                objZipCode.State = txtState.Text;
            }

            //objZipCode


            Business.ZipCodesController objZipCodesController = new Business.ZipCodesController();
            objZipCodesController.AddUpdateZipCode(objZipCode);

            //tdMsg.InnerHtml = "ItemId=" & objZipCode.ItemId.ToString()
            ResetViewStates();
            ShowHide();
        }
        public void ShowHide()
        {
            ShowHide(false);
        }
        public void ShowHide(bool InsertMode)
        {
            trAddZipCode.Visible = InsertMode;
            trManageZipCodes.Visible = !InsertMode;
            //lnkAddZipCode.Visible = Not InsertMode 

            tdUpdate.Visible = (itemId > 0);
            tdAdd.Visible = !tdUpdate.Visible;
            //cmdUpdate.Visible = (itemId > 0) 
            //imbUpdate.Visible = cmdUpdate.Visible 
            //cmdAdd.Visible = Not cmdUpdate.Visible 
            //imbAdd.Visible = cmdAdd.Visible 
        }
        //ShowHide(Optional ByVal InsertMode As Boolean = False) 

		public void InitAddPanel()
		{
			txtZipCode.ReadOnly = (itemId > 0);
			if (itemId > 0)
			{
				ZipCodeInfo objZipCode = (new ZipCodesController()).GetZipCode(itemId);
				{
					lblItemId.Text = itemId.ToString();
					txtZipCode.Text = objZipCode.ZipCode;
					txtArea.Text = objZipCode.Area;
					txtCity.Text = objZipCode.City;
					txtStateCode.Text = objZipCode.StateCode;
					txtState.Text = objZipCode.State;
				}
			}
			else
			{
				lblItemId.Text = string.Empty;
				txtZipCode.Text = "";
				txtArea.Text = "";
				txtCity.Text = "";
				txtStateCode.Text = "";
				txtState.Text = "";
			}
		}

		private void SetODS()
		{
			{
				odsZipCodes.TypeName = "bhattji.Modules.SalesReps.Business.ZipCodesController";
				odsZipCodes.DataObjectTypeName = "bhattji.Modules.SalesReps.Business.ZipCodeInfo";
				odsZipCodes.SelectMethod = "GetZipCodes";
				//.InsertMethod = "AddZipCode"
				//.UpdateMethod = "UpdateZipCode"
				odsZipCodes.DeleteMethod = "DeleteZipCode";
			}
			//odsShops
		}
		//SetODS

		public void SetGridView()
		{
			{
				//.PageSize = objOI.PagerSize
				//.AllowPaging = objOI.PagerSize > 0
				gdvZipCodes.Columns[0].Visible = IsEditable;
				//Remove the First column if the User is not Admin

				if (objOI.BackColor != string.Empty)
				{
					try {
                        gdvZipCodes.RowStyle.BackColor = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(objOI.BackColor);
					}
					catch {
					}
				}
				if (objOI.AltColor != string.Empty)
				{
					try {
						gdvZipCodes.AlternatingRowStyle.BackColor = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(objOI.AltColor);
					}
					catch {
					}
				}

				if (objOI.HeaderBackColor != string.Empty)
				{
					try {
						gdvZipCodes.HeaderStyle.BackColor = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(objOI.HeaderBackColor);
						gdvZipCodes.FooterStyle.BackColor = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(objOI.HeaderBackColor);
					}
					catch {
					}
				}
				if (objOI.PagerBackColor != string.Empty)
				{
					try {
						gdvZipCodes.PagerStyle.BackColor = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(objOI.PagerBackColor);
					}
					catch {
					}
				}
			}
			//gdvZipCodes

		}

		public void ResetViewStates()
		{
			ViewState.Remove("odsZipCode");
			ViewState.Remove("gdvZipCodes");
			odsZipCodes.DataBind();
			gdvZipCodes.DataBind();
		}

		public void DataBindZipCodes(string SearchText,  // ERROR: Unsupported modifier : In, Optional
string SearchOn)
		{
			{
				ddlZipCodes.DataValueField = "ItemId";
				ddlZipCodes.DataTextField = "Display";
				ddlZipCodes.DataSource = (new ZipCodesController()).GetSearchedZipCodes(SearchText, SearchOn);

				ddlZipCodes.DataBind();

				if (ddlZipCodes.Items.Count > 0) ddlZipCodes_SelectedIndexChanged(new object(), new EventArgs()); 
			}
		}
		#endregion

		#region " Event Handlers "

		private void Page_Load(object sender, EventArgs e)
		{
			try {
				//LoadPage(Page.IsPostBack)
				objOI = new OptionsInfo(ModuleId);

				InitEveryTime();

				if (!Page.IsPostBack)
				{
					InitFirstTime();
				}
			}
			catch (Exception exc) {
				//Module failed to load
                Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		private void gdvZipCodes_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try {
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");

					//        .PUName = (CType(e.Item.Cells(1).FindControl("txt_PUName"), TextBox)).Text
					//(CType(gdvZipCodes.FindControl("txt_PUName"), TextBox)).Text
					try {
						HyperLink cmdCalandar_PUDate = ((HyperLink)e.Row.FindControl("cmdCalandar_PUDate"));
						TextBox txt_PUDate = ((TextBox)e.Row.FindControl("txt_PUDate"));
						cmdCalandar_PUDate.NavigateUrl = DotNetNuke.Common.Utilities.Calendar.InvokePopupCal(txt_PUDate);
					}

					catch {
					}

					if ((imbDelete != null))
					{
						{
							imbDelete.Visible = IsEditable;
							if (imbDelete.Visible)
							{
								imbDelete.ToolTip = "Delete";
								//.OnClientClick = "javascript:return confirm('" & Localization.GetString("DeleteItem") & "');"
								imbDelete.Attributes.Add("onmouseover", "window.status=this.title; return true");
								imbDelete.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
							}
						}
						//imbDelete
					}

				}
			}
			catch (Exception exc) {
				//Module failed to RowDataBound
				Exceptions.ProcessModuleLoadException(this, exc);
			}

		}

		private void gdvZipCodes_RowEditing(object sender, GridViewEditEventArgs e)
		{
			itemId = (int)gdvZipCodes.DataKeys[e.NewEditIndex].Value;
			InitAddPanel();
			ShowHide(true);
		}

		private void gdvZipCodes_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Add")
			{
				itemId = Null.NullInteger;
				InitAddPanel();
				ShowHide(true);
			}
		}

		//Private Sub odsZipCodes_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsZipCodes.Inserting

		//End Sub

		//Private Sub odsZipCodes_Deleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsZipCodes.Deleting
		//    e.InputParameters["AltId") = gdvZipCodes.DataKeys(0).Value
		//End Sub

		//Private Sub odsZipCodes_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles odsZipCodes.Selecting
		//    e.InputParameters["LoadID") = loadId.ToString()
		//End Sub

		//Private Sub imbSearchZipCode_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbSearchZipCode.Click, imbSearchCity.Click, imbSearchArea.Click
		//    Select Case TryCast(sender, ImageButton).ID 'CType(sender, ImageButton).ID
		//        Case "imbSearchArea"
		//            DataBindZipCodes(txtArea.Text, "Area")
		//        Case "imbSearchCity"
		//            .DataSource = (New ZipCodesController).GetSearchedZipCodes(txtCity.Text, "City")
		//            'Case "imbSearchState"
		//            '    .DataSource = (New ZipCodesController).GetSearchedZipCodes(txtState.Text, "State")
		//        Case Else '"imbSearchZipCode"
		//            .DataSource = (New ZipCodesController).GetSearchedZipCodes(txtZipCode.Text, "ZipCode")
		//    End Select
		//End Sub

		private void imbSearchArea_Click(object sender, ImageClickEventArgs e)
		{
			DataBindZipCodes(txtArea.Text, "Area");
		}

		private void imbSearchCity_Click(object sender, ImageClickEventArgs e)
		{
			DataBindZipCodes(txtCity.Text, "City");
		}

		private void imbSearchZipCode_Click(object sender, ImageClickEventArgs e)
		{
			DataBindZipCodes(txtZipCode.Text, "ZipCode");
		}

		private void ddlZipCodes_SelectedIndexChanged(object sender, EventArgs e)
		{
			try {
				ZipCodeInfo objZip = (new ZipCodesController()).GetZipCode(int.Parse(ddlZipCodes.SelectedValue));
				txtArea.Text = objZip.Area;
				txtCity.Text = objZip.City;
				txtStateCode.Text = objZip.StateCode;
				txtZipCode.Text = objZip.ZipCode;
			}
			catch {
			}
		}

		//Private Sub imbAdd_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbAdd.Click
		//    cmdAdd_Click(sender, New System.EventArgs)
		//End Sub
		//Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
		//    Dim objZipCode As New Business.ZipCodeInfo
		//    'Initialise the ojbZipCode object
		//    objZipCode = CType(CBO.InitializeObject(objZipCode, GetType(Business.ZipCodeInfo)), Business.ZipCodeInfo)

		//    'bind text values to object
		//    With objZipCode
		//        '.ItemId = ItemId
		//        '.parentId = parentId

		//        .ZipCode = txtZipCode.Text
		//        .Area = txtArea.Text
		//        .City = txtCity.Text
		//        .StateCode = txtStateCode.Text
		//        .State = txtState.Text

		//    End With 'objZipCode


		//    Dim objZipCodesController As New Business.ZipCodesController
		//    objZipCodesController.AddZipCode(objZipCode)

		//    ResetViewStates()
		//    ShowHide()
		//End Sub

		private void imbUpdate_Click(object sender, ImageClickEventArgs e)
		{
			cmdUpdate_Click(sender, new EventArgs());
		}
		private void cmdUpdate_Click(object sender, EventArgs e)
		{
            AddUpdateZipCode();
		}

		private void imbCancel_Click(object sender, ImageClickEventArgs e)
		{
			cmdCancel_Click(sender, new EventArgs());
		}
		private void cmdCancel_Click(object sender, EventArgs e)
		{
			ShowHide();
		}

		#endregion

        #region " Web Form Designer generated code "
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        /// 


        private void InitializeComponent()
        {   
            gdvZipCodes.RowDataBound += new GridViewRowEventHandler(gdvZipCodes_RowDataBound);
            gdvZipCodes.RowEditing += new GridViewEditEventHandler(gdvZipCodes_RowEditing);
            gdvZipCodes.RowCommand += new GridViewCommandEventHandler(gdvZipCodes_RowCommand);
            imbSearchArea.Click += new ImageClickEventHandler(imbSearchArea_Click);
            imbSearchCity.Click += new ImageClickEventHandler(imbSearchCity_Click);
            imbSearchZipCode.Click += new ImageClickEventHandler(imbSearchZipCode_Click);
            ddlZipCodes.SelectedIndexChanged += new EventHandler(ddlZipCodes_SelectedIndexChanged);
            imbAdd.Click += new ImageClickEventHandler(imbUpdate_Click);
            cmdAdd.Click += new EventHandler(cmdUpdate_Click);
            imbUpdate.Click += new ImageClickEventHandler(imbUpdate_Click);
            cmdUpdate.Click += new EventHandler(cmdUpdate_Click);
            imbCancel.Click += new ImageClickEventHandler(imbCancel_Click);
            cmdCancel.Click += new EventHandler(cmdCancel_Click);
           //this.Load += new EventHandler(this.Page_Load);

        }

        #endregion

	}
	//ViewAlts

}
//bhattji.Modules.Products