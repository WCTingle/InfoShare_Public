<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Tingle_WebForms.Reports.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            // on page ready first init of your accordion
            jQuery("#accordion1").accordion({
                heightStyle: "content"
            });
        });


        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            // after the UpdatePanel finish the render from ajax call
            //  and the DOM is ready, re-initilize the accordion
            $("#accordion1").accordion({
                heightStyle: "content"
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblMe" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCompany" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlGlobalStatus" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div style="text-align: center">
                <div style="float: left; width: 100%; padding-bottom: 20px;">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">Requests Overview
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; padding-top: 0px !important; background-color: #BB4342; color: #FFF">&#8659 Options &#8659
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; padding-top: 0px !important;">
                                <asp:RadioButtonList ID="rblMe" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnPreRender="rblMe_PreRender" OnSelectedIndexChanged="rblMe_SelectedIndexChanged" BorderStyle="None">
                                    <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Assigned To Me" Value="Assigned"></asp:ListItem>
                                    <asp:ListItem Text="Requested By Me" Value="Requested"></asp:ListItem>
                                    <asp:ListItem Text="Created By Me" Value="Created"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 50%;">
                                <telerik:RadDropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" ForeColor="White">
                                    <Items>
                                        <telerik:DropDownListItem Text="Any Company" Value="Any"></telerik:DropDownListItem>
                                        <telerik:DropDownListItem Text="Tingle" Value="Tingle"></telerik:DropDownListItem>
                                        <telerik:DropDownListItem Text="Summit" Value="Summit"></telerik:DropDownListItem>
                                    </Items>
                                </telerik:RadDropDownList>
                            </td>
                            <td colspan="2" style="width: 50%;">
                                <telerik:RadDropDownList runat="server" ID="ddlGlobalStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlGlobalStatus_SelectedIndexChanged" ForeColor="White">
                                    <Items>
                                        <telerik:DropDownListItem Text="Active" Value="Active"></telerik:DropDownListItem>
                                        <telerik:DropDownListItem Text="Archive" Value="Archive"></telerik:DropDownListItem>
                                        <telerik:DropDownListItem Text="All" Value="All"></telerik:DropDownListItem>
                                    </Items>
                                </telerik:RadDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom: 1px solid black">Order Forms</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenterSmall">
                                <div runat="server" id="divExpeditedOrderLink" class="DivReportLinkButton">
                                    Expedited Order:
                                    <asp:LinkButton runat="server" ID="lbExpeditedOrderCount" PostBackUrl="~/ReportExpeditedOrder.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                                <div runat="server" id="divDirectOrderLink" class="DivReportLinkButton">
                                    Direct Order:
                                    <asp:LinkButton runat="server" ID="lbDirectOrderCount" PostBackUrl="~/ReportDirectOrder.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                                <div runat="server" id="divHotRushLink" class="DivReportLinkButton">
                                    Hot Rush:
                                    <asp:LinkButton runat="server" ID="lbHotRushCount" PostBackUrl="~/ReportHotRush.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                                <div runat="server" id="divOrderCancellationLink" class="DivReportLinkButton">
                                    Order Cancellation:
                                    <asp:LinkButton runat="server" ID="lbOrderCancellationCount" PostBackUrl="~/ReportOrderCancellation.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                                <div runat="server" id="divMustIncludeLink" class="DivReportLinkButton">
                                    Must Include:
                                    <asp:LinkButton runat="server" ID="lbMustIncludeCount" PostBackUrl="~/ReportMustInclude.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom: 1px solid black">Inventory Forms</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenterSmall">
                                <div runat="server" id="divSampleRequestLink" class="DivReportLinkButton">
                                    Sample Request:
                                    <asp:LinkButton runat="server" ID="lbSampleRequestCount" PostBackUrl="~/ReportSampleRequest.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                                <div runat="server" id="divLowInventoryLink" class="DivReportLinkButton">
                                    Low Inventory:
                                    <asp:LinkButton runat="server" ID="lbLowInventoryCount" PostBackUrl="~/ReportLowInventory.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                                <div runat="server" id="divCannotWaitForContainerLink" class="DivReportLinkButton">
                                    Cannot Wait For Container:
                                    <asp:LinkButton runat="server" ID="lbCannotWaitForContainerCount" PostBackUrl="~/ReportCannotWaitForContainer.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom: 1px solid black">Price Forms</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenterSmall">
                                <div runat="server" id="divPriceChangeRequestLink" class="DivReportLinkButton">
                                    Price Change Request:
                                    <asp:LinkButton runat="server" ID="lbPriceChangeRequestCount" PostBackUrl="~/ReportPriceChangeRequest.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                                <div runat="server" id="divRequestForCheckLink" class="DivReportLinkButton">
                                    Request For Check:
                                    <asp:LinkButton runat="server" ID="lbRequestForCheckCount" PostBackUrl="~/ReportRequestForCheck.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom: 1px solid black">Setup Forms</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenterSmall"></td>
                        </tr>
                    </table>
                </div>
                <div id="accordion1" style="float: left; width: 100%">
                    <h3>Order Forms</h3>
                    <div>
                        <div class="FormButton" runat="server" id="divExpeditedOrder">
                            <div class="FavFormButtonText">Expedited Order</div>
                            <a href="ReportExpeditedOrder.aspx"></a>
                        </div>
                        <div class="FormButton" runat="server" id="divDirectOrder">
                            <div class="FavFormButtonText">Direct Order</div>
                            <a href="ReportDirectOrder.aspx"></a>
                        </div>
                        <div class="FormButton" runat="server" id="divHotRush">
                            <div class="FavFormButtonText">Hot Rush</div>
                            <a href="ReportHotRush.aspx"></a>
                        </div>
                        <div class="FormButton" runat="server" id="divOrderCancellation">
                            <div class="FavFormButtonText2">
                                Order<br />
                                Cancellation
                            </div>
                            <a href="ReportOrderCancellation.aspx"></a>
                        </div>
                        <div class="FormButton" runat="server" id="divMustInclude">
                            <div class="FavFormButtonText">Must Include</div>
                            <a href="ReportMustInclude.aspx"></a>
                        </div>
                    </div>
                    <h3>Inventory Forms</h3>
                    <div>
                        <div class="FormButton" runat="server" id="divSampleRequest">
                            <div class="FavFormButtonText">Sample Request</div>
                            <a href="ReportSampleRequest.aspx"></a>
                        </div>
                        <div class="FormButton" runat="server" id="divLowInventory">
                            <div class="FavFormButtonText">Low Inventory</div>
                            <a href="ReportLowInventory.aspx"></a>
                        </div>
                        <div class="FormButton" runat="server" id="divCannotWaitForContainer">
                            <div class="FavFormButtonText">Cannot Wait For Container</div>
                            <a href="ReportCannotWaitForContainer.aspx"></a>
                        </div>
                    </div>
                    <h3>Pricing Forms</h3>
                    <div>
                        <div class="FormButton" runat="server" id="divPriceChangeRequest">
                            <div class="FavFormButtonText2">
                                Price Change<br />
                                Request
                            </div>
                            <a href="ReportPriceChangeRequest.aspx"></a>
                        </div>
                        <div class="FormButton" runat="server" id="divRequestForCheck">
                            <div class="FavFormButtonText2">
                                Request For<br />
                                Check
                            </div>
                            <a href="ReportRequestForCheck.aspx"></a>
                        </div>
                    </div>
                    <h3>Setup Forms</h3>
                    <div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
