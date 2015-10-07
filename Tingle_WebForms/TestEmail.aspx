<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestEmail.aspx.cs" Inherits="Tingle_WebForms.TestEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    From: <br /><asp:TextBox runat="server" ID="txtFrom"></asp:TextBox><br /><br />
    To: <br /><asp:TextBox runat="server" ID="txtTo"></asp:TextBox><br /><br />
    Subject: <br /><asp:TextBox runat="server" ID="txtSubject"></asp:TextBox><br /><br />
    Body Html: <br /><asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine"></asp:TextBox><br /><br /><br />
    
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" /><br /><br /><br /><br />
    Result: <asp:Label runat="server" ID="lblResult"></asp:Label>
</asp:Content>
