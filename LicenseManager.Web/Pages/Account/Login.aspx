<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LicenseManager.Web.Pages.Account.Login" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="pageHeader" runat="server"></h1>

    <div class="form-group">
        <asp:Label ID="lblEmail" AssociatedControlID="tbEmail" runat="server"></asp:Label>
        <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Label ID="lblPassword" AssociatedControlID="tbPassword" runat="server"></asp:Label>
        <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <asp:Button ID="btnDoLogin" runat="server" CssClass="btn btn-default" OnClick="btnDoLogin_Click" />
</asp:Content>
