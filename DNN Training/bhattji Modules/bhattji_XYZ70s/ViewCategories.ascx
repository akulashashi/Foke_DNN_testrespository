<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCategories.ascx.cs" Inherits="bhattji.Modules.XYZ70s.ViewCategories" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnLeft">
    <div id="divManageCategories" runat="server">
        <asp:GridView ID="gdvCategories" DataSourceID="odsCategories" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ItemId,ModuleId" GridLines="None" EmptyDataText="<center class='NormalRed'>No XYZ70 Category Defined</center>" CssClass="dnnGrid">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:ImageButton ID="cmdAdd" runat="server" ImageUrl="~/images/add.gif" ResourceKey="Add" CommandName="Add" CausesValidation="false" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/images/edit.gif" ResourceKey="Edit" CommandName="Edit" CausesValidation="false" ID="cmdEdit" />
                        <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/images/delete.gif" ResourceKey="cmdDelete" CommandName="Delete" CausesValidation="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="ItemId" HeaderText="Id" SortExpression="ItemId" ReadOnly="True">
                    <ItemStyle CssClass="NormalBold" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Category" SortExpression="Category">
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" CssClass="Normal" Text='<%# Eval("Category") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View Order" SortExpression="ViewOrder">
                    <ItemTemplate>
                        <asp:Label ID="lblViewOrder" runat="server" CssClass="Normal" Text='<%# Eval("ViewOrder") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <p class="NormalRed" align="center">No XYZ70 Category Defined<br />
                    <asp:ImageButton ID="imbAdd" runat="server" CausesValidation="false" CommandName="Add" ImageUrl="~/images/add.gif" ResourceKey="Add" Visible='<%# IsEditable %>' /><asp:LinkButton ID="lnbAdd" runat="server" CausesValidation="false" CommandName="Add" ResourceKey="Add" Visible='<%# IsEditable %>' /></p>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="dnnGridHeader" HorizontalAlign="Left" Font-Bold="True" />
            <RowStyle CssClass="dnnGridItem" VerticalAlign="Top" />
            <AlternatingRowStyle CssClass="dnnGridAltItem" VerticalAlign="Top" />
            <SelectedRowStyle CssClass="dnnFormError" />
            <EditRowStyle CssClass="dnnFormInput" />
            <PagerStyle CssClass="dnnGridPager" Font-Bold="True" HorizontalAlign="Center" />
            <PagerSettings Mode="NumericFirstLast" />
            <FooterStyle CssClass="dnnGridFooter" />
        </asp:GridView>
        <asp:ObjectDataSource ID="odsCategories" runat="server" />
        <div id="divClose" style="text-align: center" runat="server">
            <asp:HyperLink ID="hypImgClose" ImageUrl="~/images/lt.gif" ResourceKey="Close" CssClass="CommandButton" BorderStyle="none" runat="server" />
            <asp:HyperLink ID="hypClose" ResourceKey="Close" CssClass="CommandButton" BorderStyle="none" runat="server" />
        </div>
    </div>
    <div id="divAddCategory" runat="server">
        <div class="dnnForm" id="frmAddCategory">
            <fieldset>
                <div class="dnnFormItem">
                    <dnn:Label ID="plItemId" ControlName="lblItemId" Suffix=":" Text="ItemId" runat="server" />
                    <asp:Label ID="lblItemId" runat="server" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plCategory" ControlName="txtCategory" Suffix=":" runat="server" />
                    <asp:TextBox ID="txtCategory" runat="server" />
                    <asp:RequiredFieldValidator ID="valCategory" ControlToValidate="txtCategory" ErrorMessage="You Must Enter A Category For The XYZ70" ResourceKey="Category.ErrorMessage" CssClass="dnnFormMessage dnnFormError" Display="Dynamic" runat="server" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plViewOrder" ControlName="txtViewOrder" Suffix=":" runat="server" />
                    <asp:TextBox ID="txtViewOrder" runat="server" />
                </div>
            </fieldset>
            <ul class="dnnActions dnnClear">
                <li id="liAdd" runat="server">
                    <asp:LinkButton ID="cmdAdd" CommandName="Insert" ResourceKey="Add" CssClass="dnnPrimaryAction" runat="server" /></li>
                <li id="liUpdate" runat="server">
                    <asp:LinkButton ID="cmdUpdate" CommandName="Update" ResourceKey="cmdUpdate" CssClass="dnnPrimaryAction" runat="server" /></li>
                <li>
                    <asp:LinkButton ID="cmdCancel" CommandName="Cancel" ResourceKey="cmdCancel" CssClass="dnnSecondaryAction" CausesValidation="False" runat="server" /></li>
                <li id="liDelete" runat="server">
                    <asp:LinkButton ID="cmdDelete" ResourceKey="cmdDelete" CssClass="dnnSecondaryAction" CausesValidation="False" runat="server" /></li>
            </ul>
        </div>
    </div>
</div>