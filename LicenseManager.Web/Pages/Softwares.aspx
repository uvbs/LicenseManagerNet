<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Softwares.aspx.cs" Inherits="LicenseManager.Web.Pages.Softwares" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Softwares</h1>

    <asp:Table ID="tblSoftwares" runat="server" CssClass="table table-hover" Width="600px">
            <asp:TableRow ID="thrSoftwares" runat="server" TableSection="TableHeader">
                <asp:TableHeaderCell ID="thcId" runat="server"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID="thcManufacturer" runat="server"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID="thcName" runat="server"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID="thcGenre" runat="server"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID="thcButtons" runat="server"></asp:TableHeaderCell>
            </asp:TableRow>
    </asp:Table>
</asp:Content>
