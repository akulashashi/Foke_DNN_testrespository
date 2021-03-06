// Copyright (c) 2002-2005
// by Jaydeep Bhatt, Vision Consultants. ( http://www.bhattji.com )
//
// Permission is hereby granted, to the person obtaining a copy of this software legaly and associated 
// documentation files (the "Software"), to deal in the Software with restriction, including with limitation 
// the rights to use, copy, modify, merge, PublishDate, distribute, sublicense, and/or sell copies of the Software, and 
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Exceptions;
using bhattji.Modules.SalesReps.Business;

namespace bhattji.Modules.SalesReps
{

    public abstract partial class Settings : DotNetNuke.Entities.Modules.ModuleSettingsBase
	{

		#region " Methods "

		private void SetIconBar()
		{
			//Give the ImageUrl
			imbUpdate.ImageUrl = ModulePath + "img/bhattji_Update.jpg";
			imbCancel.ImageUrl = ModulePath + "img/bhattji_Cancel.jpg";

			//Give the Tooltip
			cmdUpdate.ToolTip = Localization.GetString("cmdUpdate");
			//cmdUpdate.Text
			cmdCancel.ToolTip = Localization.GetString("cmdCancel");
			//cmdCancel.Text
		}
		//SetIconBar()

		private void OptionsToPage(int ModId)
		{
			if (ModId > 0) OptionsToPage(new OptionsInfo(ModId)); 
		}

		private void OptionsToPage(OptionsInfo objOI)
		{
			{
				//Main Options
				try {
					rblItemsScope.SelectedIndex = objOI.ItemsScope;
				}
				catch {
					rblItemsScope.SelectedIndex = 0;
				}
				try {
					rblCategoriesScope.SelectedIndex = objOI.CategoriesScope;
				}
				catch {
					rblCategoriesScope.SelectedIndex = 2;
				}
				chkOnlyHostCanEdit.Checked = objOI.OnlyHostCanEdit;
				try {
					rblViewControl.Items.FindByValue(objOI.ViewControl).Selected = true;
				}
				catch {
					rblViewControl.SelectedIndex = 0;
				}
				chkHideSearch.Checked = objOI.HideSearch;
				txtPagerSize.Text = objOI.PagerSize.ToString();
				txtNoOfPages.Text = objOI.NoOfPages.ToString();
				chkDeleteFromGrid.Checked = objOI.DeleteFromGrid;

				try {
					ddlUpdateRedirection.Items.FindByValue(objOI.UpdateRedirection).Selected = true;
				}
				catch {
				}

				//Option1 Options
				chkAddDescription.Checked = objOI.AddDescription;
				try {
					rblTabCss.Items.FindByValue(objOI.TabCss).Selected = true;
				}
				catch {
					rblTabCss.SelectedIndex = 0;
				}
				try {
					ddlTransition.Items.FindByValue(objOI.Transition).Selected = true;
				}
				catch {
					ddlTransition.Items.FindByValue("Pixelate").Selected = true;
				}
				chkRattleImage.Checked = objOI.RattleImage;
				try {
					ddlAddDate.Items.FindByValue(objOI.AddDate).Selected = true;
				}
				catch {
				}
				try {
					ddlImagePosition.Items.FindByValue(objOI.ImagePosition).Selected = true;
				}
				catch {
					ddlImagePosition.SelectedIndex = 1;
				}
				chkDynamicThumb.Checked = objOI.DynamicThumb;
				chkShowRatings.Checked = objOI.ShowRatings;
				txtThumbWidth.Text = objOI.ThumbWidth;
				txtThumbHeight.Text = objOI.ThumbHeight;
				txtBackColor.Text = objOI.BackColor;
				txtAltColor.Text = objOI.AltColor;
				txtSelectedColor.Text = objOI.SelectedColor;
				txtHeaderBackColor.Text = objOI.HeaderBackColor;
				txtPagerBackColor.Text = objOI.PagerBackColor;
				txtThumbColumns.Text = objOI.ThumbColumns.ToString();
				chkHideTextLink.Checked = objOI.HideTextLink;

				//Scroller Options
				try {
					rblScrollBehavior.Items.FindByValue(objOI.ScrollBehavior).Selected = true;
				}
				catch {
					rblScrollBehavior.SelectedIndex = 0;
				}
				try {
					rblScrollDirection.Items.FindByValue(objOI.ScrollDirection).Selected = true;
				}
				catch {
					rblScrollDirection.SelectedIndex = 0;
				}
				txtScrollAmount.Text = objOI.ScrollAmount;
				txtScrollDelay.Text = objOI.ScrollDelay;
				txtScrollWidth.Text = objOI.ScrollWidth;
				txtScrollHeight.Text = objOI.ScrollHeight;

				//Audit Control
				try {
					//Dim objUC As New UserController
					//Dim objUI As Users.UserInfo = Users.UserController.GetUser(PortalId, .UpdatedByUserId, True)
					//ctlAudit.CreatedByUser = objUI.FirstName & " " & objUI.LastName
					ctlAudit.CreatedByUser = objOI.UpdatedByUser;
					ctlAudit.CreatedDate = objOI.UpdatedDate.ToString();
					DotNetNuke.Entities.Users.UserInfo objHostUser = DotNetNuke.Entities.Users.UserController.GetUser(PortalId, UserId, true);
					rblItemsScope.Enabled = objHostUser.IsSuperUser;
					rblCategoriesScope.Enabled = objHostUser.IsSuperUser;
					chkOnlyHostCanEdit.Enabled = objHostUser.IsSuperUser;
					if (objOI.OnlyHostCanEdit)
					{
						//txtPayPalBusinessID.Enabled = objHostUser.IsSuperUser
					}
				}
				catch {
					ctlAudit.Visible = false;
				}
			}

		}

		private void PageToOptions(int ModId)
		{
			OptionsInfo objOI = new OptionsInfo();
			{
				//Main Options
				objOI.ItemsScope = rblItemsScope.SelectedIndex;
				objOI.CategoriesScope = rblCategoriesScope.SelectedIndex;
				objOI.OnlyHostCanEdit = chkOnlyHostCanEdit.Checked;
				objOI.ViewControl = rblViewControl.SelectedValue;
				objOI.HideSearch = chkHideSearch.Checked;
				try {
					objOI.PagerSize = int.Parse(txtPagerSize.Text);
				}
				catch {
				}
				try {
					objOI.NoOfPages = int.Parse(txtNoOfPages.Text);
				}
				catch {
				}
				objOI.DeleteFromGrid = chkDeleteFromGrid.Checked;
				objOI.UpdateRedirection = ddlUpdateRedirection.SelectedValue;

				//Option1 Options
				objOI.AddDescription = chkAddDescription.Checked;
				objOI.TabCss = rblTabCss.SelectedValue;
				objOI.Transition = ddlTransition.SelectedValue;
				objOI.RattleImage = chkRattleImage.Checked;
				objOI.AddDate = ddlAddDate.SelectedValue;
				objOI.ImagePosition = ddlImagePosition.SelectedValue;
				objOI.DynamicThumb = chkDynamicThumb.Checked;
				objOI.ShowRatings = chkShowRatings.Checked;
				objOI.ThumbWidth = txtThumbWidth.Text;
				objOI.ThumbHeight = txtThumbHeight.Text;
				objOI.BackColor = txtBackColor.Text;
				objOI.AltColor = txtAltColor.Text;
				objOI.SelectedColor = txtSelectedColor.Text;
				objOI.HeaderBackColor = txtHeaderBackColor.Text;
				objOI.PagerBackColor = txtPagerBackColor.Text;

				try {
					objOI.ThumbColumns = int.Parse(txtThumbColumns.Text);
				}
				catch {
				}
				objOI.HideTextLink = chkHideTextLink.Checked;

				//Scroller Options
				objOI.ScrollBehavior = rblScrollBehavior.SelectedValue;
				objOI.ScrollDirection = rblScrollDirection.SelectedValue;
				objOI.ScrollAmount = txtScrollAmount.Text;
				objOI.ScrollDelay = txtScrollDelay.Text;
				objOI.ScrollWidth = txtScrollWidth.Text;
				objOI.ScrollHeight = txtScrollHeight.Text;

				//Audit Control
				objOI.UpdatedByUserId = UserInfo.UserID;
				objOI.UpdatedByUser = UserInfo.DisplayName;
				objOI.UpdatedDate = DateTime.Now;

				//.Save Options Info Object
				objOI.Update(ModId);
			}
		}

        private void AddUpdateSettingsItem()
        {
            try
            {
                //If Page.IsValid Then
                UpdateOptions(Convert.ToInt16(Request.QueryString["SettingsModuleId"]));
                // Redirect back to the portal home page
                Response.Redirect(Globals.NavigateURL(), true);
            }
            //End If
            catch (Exception exc)
            {
                //Module failed to load
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

		#endregion

		#region " Base Method Implementations "

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// LoadSettings loads the settings from the Databas and displays them
		/// </summary>
		/// <remarks>
		/// </remarks>
		/// <history>
		///		[bhattji]	10/20/2004	created
		/// </history>
		/// -----------------------------------------------------------------------------
		public override void LoadSettings()
		{
			LoadOptions(ModuleId);
		}

		public void LoadOptions(int ModId)
		{
			try {
				if (!Page.IsPostBack) OptionsToPage(ModId); 
			}
			catch (Exception exc) {
				//Module failed to load
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// UpdateSettings saves the modified settings to the Database
		/// </summary>
		/// <remarks>
		/// </remarks>
		/// <history>
		///		[bhattji]	10/20/2004	created
		///		[bhattji]	10/25/2004	upated to use TabModuleId rather than TabId/ModuleId
		/// </history>
		/// -----------------------------------------------------------------------------
		public override void UpdateSettings()
		{
			UpdateOptions(ModuleId);
		}

		public void UpdateOptions(int ModId)
		{
			try {
				if (Page.IsValid) PageToOptions(ModId); 
			}
			catch (Exception exc) {
				//Module failed to load
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		#endregion

		#region " Event Methods "
		private void Page_Load(object sender, EventArgs e)
		{
			try {
				//Dim ModId As Integer = ModuleId
				//If Request.QueryString("SettingsModuleId") Is Nothing Then
				//    ModId = Convert.ToInt16(Request.QueryString("SettingsModuleId"))
				//End If

				//Dim objOI As New OptionsInfo(ModId)
				//If objOI.OnlyHostCanEdit Then
				//    Dim objUC As New UserController
				//    Dim objHostUser As UserInfo = objUC.GetUser(PortalId, UserId)
				//    If Not objHostUser.IsSuperUser Then
				//        ' security violation attempt to access item not related to this Module
				//        'DotNetNuke.UI.Skins.Skin.AddModuleMessage(Me, "<a href=""" & NavigateURL() & """ title=""Click to go back"" onmouseover""window.status=this.title; return true"">Requires Host rights</a>", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError)
				//        Response.Redirect(NavigateURL(), True)
				//    End If
				//End If

				if (!IsPostBack)
				{
					//cmdUpdate.Visible = Not Request.QueryString("SettingsModuleId") Is Nothing 'Request.QueryString("SettingsModuleId") <> String.Empty
					//imbUpdate.Visible = cmdUpdate.Visible
					//cmdCancel.Visible = cmdUpdate.Visible
					//imbCancel.Visible = cmdCancel.Visible
					tblButtons.Visible = (Request.QueryString["SettingsModuleId"] != null);
					//Request.QueryString("SettingsModuleId") <> String.Empty
					if (tblButtons.Visible)
					{
						LoadOptions(Convert.ToInt16(Request.QueryString["SettingsModuleId"]));

						SetIconBar();
					}
				}
			}
			catch (Exception exc) {
				//SettingsModule failed to load
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		private void cmdSetViewOrder_Click(object sender, EventArgs e)
		{
			SalesRepsController objSalesRepController = new SalesRepsController();
			objSalesRepController.SetViewOrderSalesReps(ModuleId);
		}

		private void cmdClaimOrphan_Click(object sender, EventArgs e)
		{
			SalesRepsController objSalesRepController = new SalesRepsController();
			objSalesRepController.ClaimOrphanSalesReps(ModuleId);
		}

		private void cmdSetViewOrderCat_Click(object sender, EventArgs e)
		{
			CategoriesController objCategoriesController = new CategoriesController();
			objCategoriesController.SetViewOrderCategories(ModuleId);
		}

		private void cmdClaimOrphanCat_Click(object sender, EventArgs e)
		{
			CategoriesController objCategoriesController = new CategoriesController();
			objCategoriesController.ClaimOrphanCategories(ModuleId);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// cmdUpdate_Click runs when the update button is clicked
		/// </summary>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[bhattji]	9/23/2004	Updated to reflect design changes for Help, 508 support
		///                       and localisation
		/// </history>
		/// -----------------------------------------------------------------------------
		/// 

		private void imbUpdate_Click(object sender, ImageClickEventArgs e)
		{
			EventArgs cmdE = new EventArgs();
			cmdUpdate_Click(sender, cmdE);
		}
		private void cmdUpdate_Click(object sender, EventArgs e)
		{
            AddUpdateSettingsItem();
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// cmdCancel_Click runs when the cancel button is clicked
		/// </summary>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[bhattji]	9/23/2004	Updated to reflect design changes for Help, 508 support
		///                       and localisation
		/// </history>
		/// -----------------------------------------------------------------------------

		private void imbCancel_Click(object sender, ImageClickEventArgs e)
		{
			EventArgs cmdE = new EventArgs();
			cmdCancel_Click(sender, cmdE);
		}
		private void cmdCancel_Click(object sender, EventArgs e)
		{
			try {
				Response.Redirect(Globals.NavigateURL(), true);
			}
			catch (Exception exc) {
				//Module failed to load
				Exceptions.ProcessModuleLoadException(this, exc);
			}
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
            cmdSetViewOrder.Click += new EventHandler(cmdSetViewOrder_Click);
            cmdClaimOrphan.Click += new EventHandler(cmdClaimOrphan_Click);
            cmdSetViewOrderCat.Click += new EventHandler(cmdSetViewOrderCat_Click);
            cmdClaimOrphanCat.Click += new EventHandler(cmdClaimOrphanCat_Click);
            imbUpdate.Click += new ImageClickEventHandler(imbUpdate_Click);
            cmdUpdate.Click += new EventHandler(cmdUpdate_Click);
            imbCancel.Click += new ImageClickEventHandler(imbCancel_Click);
            cmdCancel.Click += new EventHandler(cmdCancel_Click);
            //this.Load += new EventHandler(this.Page_Load);

        }

        #endregion


	}
	//Settings

}
//bhattji.Modules.SalesReps
