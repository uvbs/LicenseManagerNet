<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="LicenseManager.Web.Pages.Account.Register" Async="true" %>
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
    <div class="form-group">
        <asp:Label ID="lblConfirmPassword" AssociatedControlID="tbPassword" runat="server"></asp:Label>
        <asp:TextBox ID="tbConfirmPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-default" OnClick="btnRegister_Click" />
</asp:Content>
