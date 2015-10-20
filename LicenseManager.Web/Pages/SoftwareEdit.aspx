<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SoftwareEdit.aspx.cs" Inherits="LicenseManager.Web.Pages.SoftwareEdit" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="form-group">
        <asp:Label ID="lblManufacturer" AssociatedControlID="ddlManufacturer" runat="server"></asp:Label>
        <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    
    <div class="form-group">
        <asp:Label ID="lblName" runat="server" AssociatedControlID="tbName"></asp:Label>
        <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    
    <div class="form-group">
        <asp:Label ID="lblGenre" runat="server" AssociatedControlID="ddlGenre"></asp:Label>
        <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
        
    <div class="form-group">
        <asp:Label ID="lblDescription" runat="server" AssociatedControlID="tbDescription"></asp:Label>
        <asp:TextBox ID="tbDescription" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
</asp:Content>
