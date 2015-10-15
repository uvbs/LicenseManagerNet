<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Softwares.aspx.cs" Inherits="LicenseManager.Web.Pages.Softwares" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblTest" runat="server" />
    <asp:Table ID="tblSoftwares" runat="server" CssClass="table table-hover">
            <asp:TableRow ID="thrSoftwares" runat="server" TableSection="TableHeader">
                <asp:TableHeaderCell ID="thcId" runat="server"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID="thcManufacturer" runat="server"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID="thcName" runat="server"></asp:TableHeaderCell>
            </asp:TableRow>
    </asp:Table>
</asp:Content>
