<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="SoftwareDetails.aspx.cs" Inherits="LicenseManager.Web.Pages.SoftwareDetails" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="lblSoftwareTitle" runat="server"></asp:Label></h1>

    <asp:Panel ID="panButtons" runat="server">
        <asp:Button ID="btnEdit" CssClass="btn btn-warning" runat="server" />
        <asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" />
    </asp:Panel>
    <br />
    <asp:Table ID="tblSoftwareDetails" runat="server" CssClass="table" Visible="false">
        <asp:TableRow>
            <asp:TableCell ID="tcManufacturer" runat="server"></asp:TableCell>
            <asp:TableCell ID="tcManufacturerValue" runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ID="tcName" runat="server"></asp:TableCell>
            <asp:TableCell ID="tcNameValue" runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ID="tcGenre" runat="server"></asp:TableCell>
            <asp:TableCell ID="tcGenreValue" runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ID="tcDescription" runat="server"></asp:TableCell>
            <asp:TableCell ID="tcDescriptionValue" runat="server"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table runat="server" ID="tblLicenses" CssClass="table table-hover" Visible="false">
        <asp:TableRow ID="thrLicenses" runat="server" TableSection="TableHeader">
            <asp:TableHeaderCell ID="thcLicensesId" runat="server" />
            <asp:TableHeaderCell ID="thcLicensesEdition" runat="server" />
            <asp:TableHeaderCell ID="thcLicensesKey" runat="server" />
            <asp:TableHeaderCell ID="thcLicensesVolume" runat="server" />
        </asp:TableRow>
    </asp:Table>
</asp:Content>
