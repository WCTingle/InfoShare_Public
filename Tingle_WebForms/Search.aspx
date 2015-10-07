<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Tingle_WebForms.Search" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            setTimeout(function () {
                if (prm.get_isInAsyncPostBack()) {
                    document.body.style.overflow = 'hidden';
                    jQuery("#loadingDiv").css("display", "block");
                    jQuery("#loadingGif").css("display", "block");
                }
            }, 300);
        }

        function EndRequestHandler(sender, args) {
            document.body.style.overflow = 'scroll';
            jQuery("#loadingDiv").css("display", "none");
            jQuery("#loadingGif").css("display", "none");
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" DefaultButton="btnSearch">
                <div style="text-align: center">
                    <div style="float: left; width: 100%; padding-bottom: 20px; line-break:normal;">
                        <table>
                            <tr class="trheader">
                                <td class="tdheader">Search</td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:TextBox runat="server" ID="txtSearch" Width="300"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnSearch" Text="Go" OnClick="btnSearch_Click" />
                                    <br />
                                    <asp:Label runat="server" ID="lblResults" CssClass="smallText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; max-width:960px;">
                                    <asp:Repeater runat="server" ID="rptrSearchResults">
                                        <ItemTemplate>
                                            <div style="width:100%; float:left; max-width:960px">
                                                <%# Eval("ResultIndex") %>. <asp:LinkButton runat="server" Text='<%# "<b>" + Eval("FormName") + ":</b> " + Eval("FormId") %>' PostBackUrl='<%# Eval("PostBackUrl") %>'></asp:LinkButton>
                                                <br />
                                                <%# Eval("MatchedFieldsHtml") %>
                                            </div>
                                          </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="plcPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="loadingDiv">
        <div id="loadingGif">
            <img src="Images/loading.gif" />
        </div>
    </div>
</asp:Content>
