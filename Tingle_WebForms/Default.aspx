<%@ Page Title="WCTingle - Web Forms" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tingle_WebForms._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
            <div style="text-align:center">
                <span style="font-size:16px; font-weight:bold; text-decoration:underline">Favorite Forms</span><br />
                <asp:Label runat="server" ID="lblUserMessage" CssClass="smallText" Visible="false"><br /></asp:Label>
                <div style="width:100%; text-align:left; float:left">
                    <asp:UpdatePanel runat="server" ID="upFavorites" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="phNoFavorites" runat="server" Visible="false">
                                <div style="width:100%; text-align:center"><p class="smallText"><br />You have no Favorite Forms selected.  <br />To select a Form as a Favorite, click the checkbox in the upper right hand of the chosen button below.</p></div>
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
                                    <div class="FavFormButtonText2">Order<br />Cancellation</div>
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
                                    <div class="FavFormButtonText">Cannot Wait<br />For Container</div>
                                    <a href="CannotWaitForContainerForm.aspx"></a>
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phFavoritePriceChangeRequest" runat="server" Visible="false">
                                <div class="FormButton">
                                    <div class="FavFormButtonText2">Price Change<br />Request</div>
                                    <a href="PriceChangeRequestForm.aspx"></a>
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phFavoriteRequestForCheck" runat="server" Visible="false">
                                <div class="FormButton">
                                    <div class="FavFormButtonText2">Request For<br />Check</div>
                                    <a href="RequestForCheckForm.aspx"></a>
                                </div>
                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br /><br />
                <div id="accordion" style="float:left; width:100%">
                  <h3>Order Forms</h3>
                  <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <div class="FormButton" runat="server" id="divExpeditedOrder">
                                <div class="formButtonCBDiv">
                                  <asp:CheckBox id="cbFavoriteExpeditedOrder" runat="server" OnCheckedChanged="cbFavoriteExpeditedOrder_CheckedChanged" AutoPostBack="true" />
                                </div>
                              <div class="FormButtonText">Expedited Order</div>
                              <a href="ExpeditedOrderForm.aspx"></a>
                            </div>
                            <div class="FormButton" runat="server" id="divDirectOrder">
                                <div class="formButtonCBDiv">
                                  <asp:CheckBox id="cbFavoriteDirectOrder" runat="server" OnCheckedChanged="cbFavoriteDirectOrder_CheckedChanged" AutoPostBack="true" />
                                </div>
                              <div class="FormButtonText">Direct Order</div>
                              <a href="DirectOrderForm.aspx"></a>
                            </div>
                            <div class="FormButton" runat="server" id="divHotRush">
                                <div class="formButtonCBDiv">
                                  <asp:CheckBox id="cbFavoriteHotRush" runat="server" OnCheckedChanged="cbFavoriteHotRush_CheckedChanged" AutoPostBack="true" />
                                </div>
                              <div class="FormButtonText">Hot Rush</div>
                              <a href="HotRushForm.aspx"></a>
                            </div>
                            <div class="FormButton" runat="server" id="divOrderCancellation">
                                <div class="formButtonCBDiv">
                                  <asp:CheckBox id="cbFavoriteOrderCancellation" runat="server" OnCheckedChanged="cbFavoriteOrderCancellation_CheckedChanged" AutoPostBack="true" />
                                </div>
                              <div class="FormButtonText2">Order<br />Cancellation</div>
                              <a href="OrderCancellationForm.aspx"></a>
                            </div>
                            <div class="FormButton" runat="server" id="divMustInclude">
                                <div class="formButtonCBDiv">
                                  <asp:CheckBox id="cbFavoriteMustInclude" runat="server" OnCheckedChanged="cbFavoriteMustInclude_CheckedChanged" AutoPostBack="true" />
                                </div>
                              <div class="FormButtonText">Must Include</div>
                              <a href="MustIncludeForm.aspx"></a>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  </div>
                  <h3>Inventory Forms</h3>
                  <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate>
                            <div class="FormButton" runat="server" id="divSampleRequest">
                                <div class="formButtonCBDiv">
                                    <asp:CheckBox id="cbFavoriteSampleRequest" runat="server" OnCheckedChanged="cbFavoriteSampleRequest_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="FormButtonText">Sample Request</div>
                                <a href="SampleRequestForm.aspx"></a>
                            </div>
                            <div class="FormButton" runat="server" id="divLowInventory">
                                <div class="formButtonCBDiv">
                                    <asp:CheckBox id="cbFavoriteLowInventory" runat="server" OnCheckedChanged="cbFavoriteLowInventory_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="FormButtonText">Low Inventory</div>
                                <a href="LowInventoryForm.aspx"></a>
                            </div>
                            <div class="FormButton" runat="server" id="divCannotWaitForContainer">
                                <div class="formButtonCBDiv">
                                    <asp:CheckBox id="cbFavoriteCannotWaitForContainer" runat="server" OnCheckedChanged="cbFavoriteCannotWaitForContainer_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="FormButtonText">Cannot Wait<br />For Container</div>
                                <a href="CannotWaitForContainerForm.aspx"></a>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  </div>
                  <h3>Pricing Forms</h3>
                  <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <div class="FormButton" runat="server" id="divPriceChangeRequest">
                                <div class="formButtonCBDiv">
                                    <asp:CheckBox id="cbFavoritePriceChangeRequest" runat="server" OnCheckedChanged="cbFavoritePriceChangeRequest_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="FormButtonText2">Price Change<br />Request</div>
                                <a href="PriceChangeRequestForm.aspx"></a>
                            </div>
                            <div class="FormButton" runat="server" id="divRequestForCheck">
                                <div class="formButtonCBDiv">
                                  <asp:CheckBox id="cbFavoriteRequestForCheck" runat="server" OnCheckedChanged="cbFavoriteRequestForCheck_CheckedChanged" AutoPostBack="true" />
                                </div>
                              <div class="FormButtonText2">Request For<br />Check</div>
                              <a href="RequestForCheckForm.aspx"></a>
                          </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  </div>

                  <h3>Setup Forms</h3>
                  <div></div>
                </div>
            </div>
<script type="text/javascript">
    jQuery(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        // on page ready first init of your accordion
        jQuery("#accordion").accordion({
            heightStyle: "content"
        });
    });


    function InitializeRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        // after the UpdatePanel finish the render from ajax call
        //  and the DOM is ready, re-initilize the accordion
        jQuery("#accordion").accordion({
            heightStyle: "content"
        });
    }

</script>
</asp:Content>
