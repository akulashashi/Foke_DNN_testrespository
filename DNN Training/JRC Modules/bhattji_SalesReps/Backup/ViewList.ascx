<%@ Control Language="c#" Inherits="bhattji.Modules.SalesReps.ViewList" Codebehind="ViewList.ascx.cs" AutoEventWireup="true" Explicit="True" %>
<%@ Register TagPrefix="act" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>

<table id="tblSearch" width="100%" runat="server">
    <tr>
        <td nowrap colspan="2">
            <asp:RadioButtonList ID="rblSearchOn" CssClass="NormalTextBox" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Title" Text="Title" resourcekey="Title" />
                <asp:ListItem Value="Description" Text="Description" resourcekey="Description" />
                <asp:ListItem Value="Details" Text="Details" resourcekey="Details" />
                <asp:ListItem Value="ANY" Text="Any" resourcekey="Any" Selected="True" />
            </asp:RadioButtonList></td>
        <td nowrap align="right">
            <asp:RadioButtonList ID="rblSearchType" CssClass="NormalTextBox" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="S" Text="StartsWith" resourcekey="StartsWith" Selected="True" />
                <asp:ListItem Value="A" Text="Contains" resourcekey="Contains" />
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td nowrap>
            <asp:Label ID="plFromDate" Text="From" CssClass="SubHead" runat="server" />
            <asp:TextBox ID="txtFrom" CssClass="NormalTextBox" runat="server" Columns="10" /><asp:Image runat="server" ID="imgFrom" ImageUrl="~/images/calendar.png" style="cursor:hand"/>
            <act:CalendarExtender ID="calFrom" TargetControlID="txtFrom" PopupButtonID="imgFrom" runat="server" Format="d" />&nbsp;
            <asp:Label ID="plToDate" Text="To" CssClass="SubHead" runat="server" />
            <asp:TextBox ID="txtTo" CssClass="NormalTextBox" runat="server" Columns="10" /><asp:Image runat="server" ID="imgTo" ImageUrl="~/images/calendar.png" style="cursor:hand"/>
            <act:CalendarExtender ID="calTo" TargetControlID="txtTo" PopupButtonID="imgTo" runat="server" Format="d" /></td>
        <td nowrap>
            <asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="true" CssClass="NormalTextBox" /><asp:HyperLink ID="hypEditCategory" runat="server" ImageUrl="~/images/action_settings.gif" resourcekey="Edit" /></td>
        <td nowrap align="right">
            <asp:TextBox ID="txtSearch" CssClass="NormalTextBox" runat="server" /><asp:Button ID="btnSearch" runat="server" resourcekey="Search" /></td>
    </tr>
</table>
<%= GetMarquee() %>
<asp:GridView ID="gdvViewList" DataSourceID="odsSalesReps" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ItemId" GridLines="None" Width="100%" EmptyDataText="<center class='NormalRed'>No SalesRep Defined</center>">
    <Columns>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
            <HeaderTemplate>
                <asp:HyperLink ID="hypAddItem" ImageUrl="~/images/Add.gif" resourcekey="Add" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:HyperLink ID="hypEditItem" ImageUrl="~/images/edit.gif" resourcekey="Edit" runat="server" />
                <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/images/delete.gif" resourcekey="cmdDelete" CommandName="Delete" CausesValidation="false" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Title">
            <ItemTemplate>
                <asp:HyperLink ID="hypThumb" ImageUrl="~/images/icon_profile_16px.gif" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" SortExpression="Title">
            <ItemTemplate>
                <asp:HyperLink ID="hypTitle" CssClass="SubHead" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="RatingAverage">
            <ItemTemplate>
                <asp:HyperLink ID="hypRatings" CssClass="SubHead" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="Title">
            <ItemTemplate>
                <asp:HyperLink ID="hypPrint" ImageUrl="~/images/print.gif" resourcekey="Print" CssClass="SubHead" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" SortExpression="Title">
            <ItemTemplate>
                <asp:HyperLink ID="hypMoreLink" resourcekey="ReadMore" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <p class="NormalRed" align="center">No SalesRep Defined<br />
            <asp:HyperLink ImageUrl="~/images/add.gif" NavigateUrl='<%# EditUrl() %>' resourcekey="Add" Visible='<%# IsEditable %>' runat="server" /><asp:HyperLink NavigateUrl='<%# EditUrl() %>' resourcekey="Add" Visible='<%# IsEditable %>' runat="server" /></p>
    </EmptyDataTemplate>
    <HeaderStyle CssClass="NormalBold" HorizontalAlign="Left" Font-Bold="True" />
    <RowStyle CssClass="Normal" VerticalAlign="Top" />
    <AlternatingRowStyle CssClass="Normal" VerticalAlign="Top" />
    <PagerStyle CssClass="NormalBold" Font-Bold="True" HorizontalAlign="Center" />
    <PagerSettings Mode="NumericFirstLast" />
</asp:GridView>
<%= GetMarqueeEnd() %>
<asp:ObjectDataSource ID="odsSalesReps" runat="server" />
