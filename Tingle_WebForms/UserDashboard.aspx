<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="Tingle_WebForms.UserDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center">
        <div style="width: 50%; text-align: center; float: left; padding-bottom:30px;">
            <span style="font-size: 16px; font-weight: bold; text-decoration: underline">Open Assigned Forms</span><br />
            <div runat="server" id="divExpeditedOrderLink" class="DivReportLinkDashboard">
                Expedited Order:
                <asp:LinkButton runat="server" ID="lbExpeditedOrderCount" OnClick="lbExpeditedOrderCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divDirectOrderLink" class="DivReportLinkDashboard">
                Direct Order:
                <asp:LinkButton runat="server" ID="lbDirectOrderCount"  OnClick="lbDirectOrderCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divHotRushLink" class="DivReportLinkDashboard">
                Hot Rush:
                <asp:LinkButton runat="server" ID="lbHotRushCount" OnClick="lbHotRushCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divOrderCancellationLink" class="DivReportLinkDashboard">
                Order Cancellation:
                <asp:LinkButton runat="server" ID="lbOrderCancellationCount" OnClick="lbOrderCancellationCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divMustIncludeLink" class="DivReportLinkDashboard">
                Must Include:
                <asp:LinkButton runat="server" ID="lbMustIncludeCount" OnClick="lbMustIncludeCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divSampleRequestLink" class="DivReportLinkDashboard">
                Sample Request:
                <asp:LinkButton runat="server" ID="lbSampleRequestCount" OnClick="lbSampleRequestCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divLowInventoryLink" class="DivReportLinkDashboard">
                Low Inventory:
                <asp:LinkButton runat="server" ID="lbLowInventoryCount" OnClick="lbLowInventoryCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divPriceChangeRequestLink" class="DivReportLinkDashboard">
                Price Change Request:
                <asp:LinkButton runat="server" ID="lbPriceChangeRequestCount" OnClick="lbPriceChangeRequestCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divRequestForCheckLink" class="DivReportLinkDashboard">
                Request For Check:
                <asp:LinkButton runat="server" ID="lbRequestForCheckCount" OnClick="lbRequestForCheckCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divCannotWaitForContainerLink" class="DivReportLinkDashboard">
                Cannot Wait For Container:
                <asp:LinkButton runat="server" ID="lbCannotWaitForContainerCount" OnClick="lbCannotWaitForContainerCount_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
        </div>
        <div style="width: 50%; text-align: center; float: left">
            <span style="font-size: 16px; font-weight: bold; text-decoration: underline">Open Requested Forms</span><br />
            <div runat="server" id="divExpeditedOrderLinkReq" class="DivReportLinkDashboard">
                Expedited Order:
                <asp:LinkButton runat="server" ID="lbExpeditedOrderCountReq" OnClick="lbExpeditedOrderCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divDirectOrderLinkReq" class="DivReportLinkDashboard">
                Direct Order:
                <asp:LinkButton runat="server" ID="lbDirectOrderCountReq" OnClick="lbDirectOrderCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divHotRushLinkReq" class="DivReportLinkDashboard">
                Hot Rush:
                <asp:LinkButton runat="server" ID="lbHotRushCountReq" OnClick="lbHotRushCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divOrderCancellationLinkReq" class="DivReportLinkDashboard">
                Order Cancellation:
                <asp:LinkButton runat="server" ID="lbOrderCancellationCountReq" OnClick="lbOrderCancellationCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divMustIncludeLinkReq" class="DivReportLinkDashboard">
                Must Include:
                <asp:LinkButton runat="server" ID="lbMustIncludeCountReq" OnClick="lbMustIncludeCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divSampleRequestLinkReq" class="DivReportLinkDashboard">
                Sample Request:
                <asp:LinkButton runat="server" ID="lbSampleRequestCountReq" OnClick="lbSampleRequestCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divLowInventoryLinkReq" class="DivReportLinkDashboard">
                Low Inventory:
                <asp:LinkButton runat="server" ID="lbLowInventoryCountReq" OnClick="lbLowInventoryCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divPriceChangeRequestLinkReq" class="DivReportLinkDashboard">
                Price Change Request:
                <asp:LinkButton runat="server" ID="lbPriceChangeRequestCountReq" OnClick="lbPriceChangeRequestCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divRequestForCheckLinkReq" class="DivReportLinkDashboard">
                Request For Check:
                <asp:LinkButton runat="server" ID="lbRequestForCheckCountReq" OnClick="lbRequestForCheckCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
            <div runat="server" id="divCannotWaitForContainerLinkReq" class="DivReportLinkDashboard">
                Cannot Wait For Container:
                <asp:LinkButton runat="server" ID="lbCannotWaitForContainerCountReq" OnClick="lbCannotWaitForContainerCountReq_Click" CssClass="ReportLinkButton"></asp:LinkButton>
            </div>
        </div>
        <div style="width: 40%; text-align: left; float: left">
            
        </div>
        <div style="width: 100%; text-align: center; float: left; padding-bottom:30px;">
        <span style="font-size: 16px; font-weight: bold; text-decoration: underline">Favorite Request Forms</span><br />
        <asp:Label runat="server" ID="lblUserMessage" CssClass="smallText" Visible="false"><br /></asp:Label>
            <asp:PlaceHolder ID="phNoFavorites" runat="server" Visible="false">
                <div style="width: 100%; text-align: center">
                    <p class="smallText">
                        <br />
                        You have no Favorite Forms selected. 
                            <br />
                        To select a Form as a Favorite, click the checkbox in the upper right hand of the chosen button below.
                    </p>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteExpeditedOrder" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText">Expedited Order</div>
                    <a href="ExpeditedOrderForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteDirectOrder" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText">Direct Order</div>
                    <a href="DirectOrderForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteHotRush" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText">Hot Rush</div>
                    <a href="HotRushForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteOrderCancellation" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText2">
                        Order<br />
                        Cancellation
                    </div>
                    <a href="OrderCancellationForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteMustInclude" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText">Must Include</div>
                    <a href="MustIncludeForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteSampleRequest" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText">Sample Request</div>
                    <a href="SampleRequestForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteLowInventory" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText">Low Inventory</div>
                    <a href="LowInventoryForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteCannotWaitForContainer" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText2">
                        Cannot Wait<br />For Container
                    </div>
                    <a href="CannotWaitForContainerForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoritePriceChangeRequest" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText2">
                        Price Change<br />
                        Request
                    </div>
                    <a href="PriceChangeRequestForm.aspx"></a>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFavoriteRequestForCheck" runat="server" Visible="false">
                <div class="FormButton">
                    <div class="FavFormButtonText2">
                        Request For<br />
                        Check
                    </div>
                    <a href="RequestForCheckForm.aspx"></a>
                </div>
            </asp:PlaceHolder>

        </div>
    </div>

</asp:Content>
