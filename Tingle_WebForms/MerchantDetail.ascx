<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MerchantDetail.ascx.cs" Inherits="Tingle_WebForms.MerchantDetail" %>

<asp:HiddenField runat="server" ID="hMerchantRecordId" Value='<%# Eval("RecordId") %>' />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="editAppHeader">
            <table style="width: 100%; height: 60px" runat="server" id="tblEditAppHeader">
                <tr>
                    <td id="tdCorp" colspan="3" style="width: 100%; text-align: center; border-bottom: 1px solid #D4D4D4">
                        <asp:Label ID="lblCorpName" runat="server" Style="font-size: 36px; color: #FFF;" Text='<%# Eval("CorpName") %>'></asp:Label>
                    </td>
                </tr> 
                <tr>
                    <td id="tdContact" style="width: 33%; text-align: center; font-size: 14px; color: #FFF; border-right: 1px solid #D4D4D4;">Principal:
                        <asp:Label ID="lblContactFirstName" runat="server" Style="font-size: 14px; color: #FFF; font-weight: bold;" Text='<%# Eval("MerchantPrincipal.Contact.FirstName") %>'></asp:Label>
                        <asp:Label ID="lblContactLastName" runat="server" Style="font-size: 14px; color: #FFF; font-weight: bold;" Text='<%# Eval("MerchantPrincipal.Contact.LastName") %>'></asp:Label>
                    </td>
                    <td style="width: 33%; color: #FFF; font-size: 14px; text-align: center; border-right: 1px solid #D4D4D4;">Account Number: 
                        <asp:Label ID="lblAccountNumber" runat="server" Style="font-size: 14px; color: #FFF; font-weight: bold;" Text='<%# Eval("RecordId") %>'></asp:Label>
                    </td>
                    <td style="width: 34%; text-align: center; font-size: 14px; color: #FFF;">
                        <div style="width: 150px; height: 20px; margin-left: auto; margin-right: auto; padding-top: 4px;">
                            <asp:Label ID="lblStatusDescription" runat="server" Style="font-size: 18px; color: #FFF; font-weight: bold; letter-spacing: 2px; text-transform: uppercase;" OnPreRender="lblStatusDescription_PreRender" Text='<%# Eval("MerchantStatus.StatusDescription") %>'></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <telerik:RadToolTip runat="server" ID="ttCorp" Position="BottomCenter" RelativeTo="Element" TargetControlID="tdCorp" ShowEvent="OnMouseOver" Animation="Resize" RenderInPageRoot="true"
                AnimationDuration="750" HideEvent="LeaveTargetAndToolTip" HideDelay="100">
                <%# "DBA Name: " + Eval("DbaName") + "<br /> Address: <br />" + Eval("Business.Address.Address") + "<br />" + Eval("Business.Address.City") + ", " +  Eval("Business.Address.State.Name") + " " + Eval("Business.Address.Zip") %>
            </telerik:RadToolTip>
        </div>

        <asp:Panel runat="server" ID="pnlEditMerchant" Visible="true">
            <div style="height: 100%; width: 100%; text-align: Left">
                <telerik:RadTabStrip ID="tabStripMerchantDetail" runat="server" Skin="Windows7" Width="100%" SelectedIndex="0" MultiPageID="multiPage1"
                    Font-Size="18px" AutoPostBack="true" Align="Left" CausesValidation="false" OnTabClick="tabStripMerchantDetail_TabClick">
                    <Tabs>
                        <telerik:RadTab Text="Profile" PageViewID="ProfileView"></telerik:RadTab>
                        <telerik:RadTab Text="Merchant Processing" PageViewID="MerchantProcessingView"></telerik:RadTab>
                        <telerik:RadTab Text="Advances" PageViewID="AdvancesView"></telerik:RadTab>
                        <telerik:RadTab Text="Underwriting" PageViewID="UnderwritingView"></telerik:RadTab>
                        <telerik:RadTab Text="Email Messages" PageViewID="EmailMessagesView"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="multiPage1" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true" BackColor="#FBFDFF">
                    <telerik:RadPageView runat="server" ID="ProfileView">
                        <div style="float: left; width: 15%; text-align: left; padding-top: 20px;">
                            <telerik:RadTabStrip ID="tabStripMerchantProfile" runat="server" SelectedIndex="0" MultiPageID="multiPage2" Width="100px" OnTabClick="tabStripMerchantProfile_TabClick"
                                AutoPostBack="true" Align="Left" Orientation="VerticalLeft" CausesValidation="false" ShowBaseLine="true" Skin="MetroTouch">
                                <Tabs>
                                    <telerik:RadTab Text="Dashboard" PageViewID="DashboardView"></telerik:RadTab>
                                    <telerik:RadTab Text="Status" PageViewID="StatusView"></telerik:RadTab>
                                    <telerik:RadTab Text="Business" PageViewID="BusinessView"></telerik:RadTab>
                                    <telerik:RadTab Text="Principal" PageViewID="PrincipalView"></telerik:RadTab>
                                    <telerik:RadTab Text="Contact" PageViewID="ContactView"></telerik:RadTab>
                                    <telerik:RadTab Text="Banking" PageViewID="BankingView"></telerik:RadTab>
                                    <telerik:RadTab Text="Users" PageViewID="UsersView"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                        </div>
                        <div style="float: right; width: 84%; text-align: left;">
                            <div style="padding-top: 20px; padding-bottom: 20px;">
                                <telerik:RadMultiPage ID="multiPage2" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true">
                                    <telerik:RadPageView runat="server" ID="DashboardView" OnPreRender="DashboardView_PreRender">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="2" class="editFormNames">Merchant Dashboard</td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Status<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDStatus" Width="125px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Underwriting Status<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDUnderwritingStatus" Width="125px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Approved Advance Amount<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDApprovedAmount" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Available Advance Amount<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDAvailableAmount" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Total Outstanding Advance(s)<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDTotalAdvances" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Total Outstanding Advance Fees<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDTotalAdvanceFees" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Total Repaid Amount (For Current Advances)<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDTotalRepaid" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Total Amount Due<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDTotalDue" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Last Payment Date (For Current Advances)<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDLastPaymentDate" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Last Payment Amount (For Current Advances)<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDLastPaymentAmount" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Total Pending Advance(s)<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDPendingAdvances" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Next Payment Amount<br />
                                                    <telerik:RadTextBox runat="server" ID="txtMDNextPaymentAmount" Width="75px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br /><br />
                                        <asp:PlaceHolder runat="server" ID="phSuspension" Visible="false">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="2" class="editFormNames">Merchant Suspension Details</td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Suspension Reason<br />
                                                    <telerik:RadTextBox runat="server" ID="txtSuspensionReason" Width="200px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Suspension Date<br />
                                                    <telerik:RadTextBox runat="server" ID="txtSuspensionTimestamp" Width="200px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">Suspension Notes<br />
                                                    <telerik:RadTextBox runat="server" ID="txtSuspensionNotes" Width="200px" MaxLength="1000" Enabled="false" TextMode="MultiLine">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues">Suspended By<br />
                                                    <telerik:RadTextBox runat="server" ID="txtSuspensionAdmin" Width="200px" Enabled="false">
                                                        <DisabledStyle BackColor="White" ForeColor="Black" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        </asp:PlaceHolder>

                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="StatusView" OnPreRender="StatusView_PreRender">
                                        <asp:PlaceHolder runat="server" ID="phQuickActions">
                                            <table class="tableEditAppForm">
                                                <tr>
                                                    <td class="editFormNames">Quick Actions</td>
                                                </tr>
                                                <tr>
                                                    <td class="centerFormValues">
                                                        <asp:PlaceHolder runat="server" ID="phApproveMerchant">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Approve Merchant >></span></td>
                                                                    <td style="width: 25%; text-align: center">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                            ForeColor="Red" ControlToValidate="txtApprovedAdvanceAmount" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                                                        Approved Amount:<br />
                                                                        <telerik:RadNumericTextBox runat="server" ID="txtApprovedAdvanceAmount" Type="Currency" NumberFormat-DecimalDigits="0" 
                                                                            NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," OnPreRender="txtApprovedAdvanceAmount_PreRender"
                                                                            MinValue="0" Width="60px"></telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td style="width: 35%; text-align: left">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator72a" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                            ForeColor="Red" ControlToValidate="txtMerchantApprovalNotes" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                                                        Notes: 
                                                                        <telerik:RadTextBox runat="server" ID="txtMerchantApprovalNotes" Width="150px" ValidationGroup="Approve" TextMode="MultiLine"></telerik:RadTextBox>
                                                                    </td>
                                                                    <td style="width: 20%; text-align: left">
                                                                        <telerik:RadButton ID="btnApproveMerchant" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                            Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnApproveMerchant_Click"
                                                                            ValidationGroup="Approve">
                                                                        </telerik:RadButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                        </asp:PlaceHolder>
                                                        <asp:PlaceHolder runat="server" ID="phApproveDisallowed">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Approve Merchant >></span></td>
                                                                    <td style="width: 80%; text-align: center" colspan="3">
                                                                        <asp:Label runat="server" ID="lblApprovalDisallowed" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                        </asp:PlaceHolder>

                                                        <asp:PlaceHolder runat="server" ID="phDenyMerchant">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Deny Merchant >></span></td>
                                                                    <td style="width: 25%; text-align: left"></td>
                                                                    <td style="width: 35%; text-align: left">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                            ForeColor="Red" ControlToValidate="txtMerchantDenialNotes" ValidationGroup="Deny"></asp:RequiredFieldValidator>
                                                                        Notes: 
                                                                        <telerik:RadTextBox runat="server" ID="txtMerchantDenialNotes" Width="150px" ValidationGroup="Deny" TextMode="MultiLine"></telerik:RadTextBox>
                                                                    </td>
                                                                    <td style="width: 20%; text-align: left">
                                                                        <telerik:RadButton ID="btnDenyMerchant" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                            Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnDenyMerchant_Click"
                                                                            ValidationGroup="Deny">
                                                                        </telerik:RadButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                        </asp:PlaceHolder>
                                                        <asp:PlaceHolder runat="server" ID="phDenyDisallowed">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Deny Merchant >></span></td>
                                                                    <td style="width: 80%; text-align: center" colspan="3">
                                                                        <asp:Label runat="server" ID="lblDenyDisallowed" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </asp:PlaceHolder>

                                                        <asp:PlaceHolder runat="server" ID="phSuspendMerchant">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Suspend Merchant >></span></td>
                                                                    <td style="width: 25%; text-align: center">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator69" SetFocusOnError="true" runat="server" Text="* " ErrorMessage="* " ForeColor="Red" Font-Size="20px" 
                                                                            ControlToValidate="ddlSuspendReasons" ValidationGroup="Suspend"></asp:RequiredFieldValidator>
                                                                        <telerik:RadDropDownList ID="ddlSuspendReasons" runat="server"
                                                                            SelectMethod="BingSuspendReasons"
                                                                            AppendDataBoundItems="true"
                                                                            DataTextField="ReasonName"
                                                                            DataValueField="RecordId"
                                                                            DropDownHeight="200px"
                                                                            Width="125px"
                                                                            DropDownWidth="250px"
                                                                            DefaultMessage="Select a Reason">
                                                                            <ItemTemplate>
                                                                                <table style="width:90%; margin-left:auto; margin-right:auto;">
                                                                                    <tr>
                                                                                        <td style="width:100%; text-align:center;">
                                                                                            <asp:Label runat="server" ID="lblReasonName" Text='<%# Eval("ReasonName") %>' Font-Bold="true"></asp:Label><br />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width:100%; text-align:center;">
                                                                                            <asp:Label runat="server" ID="lblReasonDescription" Text='<%# Eval("ReasonDescription") %>' Font-Italic="true"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width:100%" colspan="2"><hr /></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </telerik:RadDropDownList>
                                                                        
                                                                    </td>
                                                                    <td style="width: 35%; text-align: left">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                            ForeColor="Red" ControlToValidate="txtMerchantSuspensionNotes" ValidationGroup="Suspend"></asp:RequiredFieldValidator>
                                                                        Notes: 
                                                                        <telerik:RadTextBox runat="server" ID="txtMerchantSuspensionNotes" Width="150px" ValidationGroup="Suspend" TextMode="MultiLine"></telerik:RadTextBox>
                                                                    </td>
                                                                    <td style="width: 20%; text-align: left">
                                                                        <telerik:RadButton ID="btnSuspendMerchant" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                            Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnSuspendMerchant_Click"
                                                                            ValidationGroup="Suspend">
                                                                        </telerik:RadButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                        </asp:PlaceHolder>
                                                        <asp:PlaceHolder runat="server" ID="phSuspendDisallowed">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Suspend Merchant >></span></td>
                                                                    <td style="width: 80%; text-align: center" colspan="3">
                                                                        <asp:Label runat="server" ID="lblSuspendDisallowed" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </asp:PlaceHolder>

                                                        <asp:PlaceHolder runat="server" ID="phReinstateMerchant">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Reinstate Merchant >></span></td>
                                                                    <td style="width: 25%; text-align: left"></td>
                                                                    <td style="width: 35%; text-align: left">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                            ForeColor="Red" ControlToValidate="txtMerchantReinstateNotes" ValidationGroup="Resinstate"></asp:RequiredFieldValidator>
                                                                        Notes: 
                                                                        <telerik:RadTextBox runat="server" ID="txtMerchantReinstateNotes" Width="150px" ValidationGroup="Resinstate" TextMode="MultiLine"></telerik:RadTextBox>
                                                                    </td>
                                                                    <td style="width: 20%; text-align: left">
                                                                        <telerik:RadButton ID="btnReinstateMerchant" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                            Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnReinstateMerchant_Click"
                                                                            ValidationGroup="Resinstate">
                                                                        </telerik:RadButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                        </asp:PlaceHolder>
                                                        <asp:PlaceHolder runat="server" ID="phReinstateDisallowed">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Reinstate Merchant >></span></td>
                                                                    <td style="width: 80%; text-align: center" colspan="3">
                                                                        <asp:Label runat="server" ID="lblReinstateDisallowed" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </asp:PlaceHolder>
                                                        <asp:PlaceHolder runat="server" ID="phCancelMerchant">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Cancel Merchant >></span></td>
                                                                    <td style="width: 25%; text-align: left"></td>
                                                                    <td style="width: 35%; text-align: left">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                            ForeColor="Red" ControlToValidate="txtMerchantCancellationNotes" ValidationGroup="Cancel"></asp:RequiredFieldValidator>
                                                                        Notes: 
                                                                        <telerik:RadTextBox runat="server" ID="txtMerchantCancellationNotes" Width="150px" ValidationGroup="Cancel" TextMode="MultiLine"></telerik:RadTextBox>
                                                                    </td>
                                                                    <td style="width: 20%; text-align: left">
                                                                        <telerik:RadButton ID="btnCancelMerchant" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                            Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnCancelMerchant_Click"
                                                                            ValidationGroup="Cancel">
                                                                        </telerik:RadButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding-top: 5px; padding-bottom: 5px;"></td>
                                                                </tr>
                                                            </table>
                                                        </asp:PlaceHolder>
                                                        <asp:PlaceHolder runat="server" ID="phCancelDisallowed">
                                                            <table style="width: 100%; padding: 0 10px 0 10px;">
                                                                <tr>
                                                                    <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Cancel Merchant >></span></td>
                                                                    <td style="width: 80%; text-align: center" colspan="3">
                                                                        <asp:Label runat="server" ID="lblCancelDisallowed" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="padding-top: 15px; padding-bottom: 15px;"></td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </asp:PlaceHolder>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:PlaceHolder>
                                        <br />
                                        <div style="width:100%; text-align:center;">
                                            <asp:Label runat="server" ID="lblMerchantActionMessage" OnPreRender="lblMerchantActionMessage_PreRender" CssClass="admin-message"></asp:Label>
                                        </div>
                                        <br />
                                        
                                        <table class="tableEditAppForm">
                                                <tr>
                                                    <td class="editFormNames">Status Change Timeline</td>
                                                </tr>
                                                <tr>
                                                    <td class="centerFormValues" style="height:180px">
                                                        <div style="overflow-y:scroll; height:180px" id="divStatusChanges" runat="server">
                                                            <asp:Repeater runat="server" ID="rptrStatusChanges" ItemType="MiqroMoney.Models.StatusChangeModel" SelectMethod="GetStatusChanges">
                                                            <HeaderTemplate>
                                                                <table style="width:100%">
                                                                    <tr>
                                                                        <th>Timestamp</th>
                                                                        <th>Merchant</th>
                                                                        <th>Status / Underwriting Status</th>
                                                                        <th>User</th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Item.Timestamp %></td>
                                                                    <td><%# Item.Merchant.CorpName + "  <span style=\"font-style:italic\">(" + Item.Merchant.RecordId + ")</span>" %></td>
                                                                    <td><%# Item.MerchantStatus.StatusDescription + " / " + Item.UnderwritingStatus.StatusDescription %></td>
                                                                    <td><%# Item.AdminUser.UserName %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater> 
                                                        </div> 
                                                    </td>
                                                </tr>
                                        </table>
                                        
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="BusinessView" OnPreRender="BusinessView_PreRender">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td class="editFormNames" style="width: 50%">Legal Organization</td>
                                                <td class="editFormNames" style="width: 35%">Merchant Type</td>
                                                <td class="editFormNames" style="width: 15%">MCC Code</td>
                                            </tr>
                                            <tr>
                                                <td class="formValues" style="width: 50%;">
                                                    <table class="tableEditAppForm">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlLegalOrgType" runat="server"
                                                                    SelectMethod="BindLegalOrgTypes"
                                                                    AppendDataBoundItems="true"
                                                                    DataTextField="LegalOrgTypeName"
                                                                    DataValueField="RecordId"
                                                                    DropDownHeight="200px"
                                                                    Width="100px"
                                                                    DropDownWidth="150px"
                                                                    SelectedValue='<%# Eval("LegalOrgType.RecordId")%>'
                                                                    DefaultMessage="Please Select">
                                                                </telerik:RadDropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" Text="*" ErrorMessage="Legal Organization Type," ForeColor="Red" Font-Size="20px" ControlToValidate="ddlLegalOrgType"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>State:
                                                    <telerik:RadDropDownList ID="ddlLegalOrgState" runat="server"
                                                        SelectMethod="BindLegalOrgStates"
                                                        AppendDataBoundItems="true"
                                                        DataTextField="Name"
                                                        DataValueField="RecordId"
                                                        DropDownHeight="200px"
                                                        Width="100px"
                                                        DropDownWidth="150px"
                                                        SelectedValue='<%# Eval("LegalOrgState.RecordId")%>'
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" Text="*" ErrorMessage="Legal Organization State," ForeColor="Red" Font-Size="20px" ControlToValidate="ddlLegalOrgState"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="formValues" style="width: 35%;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblMerchantType" runat="server" RepeatDirection="Horizontal" Width="100%"
                                                                    OnDataBinding="rblMerchantType_DataBinding">
                                                                    <asp:ListItem Value="Retail">Retail</asp:ListItem>
                                                                    <asp:ListItem Value="Restaurant">Restaurant</asp:ListItem>
                                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                            <td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="leftFormValues" style="width: 15%;">
                                                    <telerik:RadDropDownList ID="ddlMCC" runat="server"
                                                        SelectMethod="BindMCCs"
                                                        AppendDataBoundItems="true"
                                                        DataTextField="MerchantCategoryCode"
                                                        DataValueField="RecordId"
                                                        DropDownHeight="200px"
                                                        Width="100px"
                                                        DropDownWidth="150px"
                                                        SelectedValue='<%# Eval("Mcc.RecordId")%>'
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" Text="*" runat="server" ErrorMessage="MCC," ForeColor="Red" Font-Size="20px" ControlToValidate="ddlMCC"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>

                                        <br />
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="4" class="editFormNames" style="width: 100%">Merchant Business Information</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Merchant's Corporate/Legal Name<br />
                                                    <telerik:RadTextBox ID="txtCorpName" runat="server" Width="250" MaxLength="100" Text='<%# Eval("CorpName")%>'></telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" Text="*" runat="server" ErrorMessage="Corporation Name," ForeColor="Red" Font-Size="20px" ControlToValidate="txtCorpName"></asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Merchant's D/B/A or Trade Name <span class="parenNote">(if different than Legal Name)</span><br />
                                                    <telerik:RadTextBox ID="txtDBAName" runat="server" Width="250" MaxLength="100" Text='<%# Eval("DbaName")%>'></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Phyiscal Address (no P.O. Box)<br />
                                                    <telerik:RadTextBox ID="txtCorpAddress" runat="server" Width="350" MaxLength="100" Text='<%# Eval("Business.Address.Address")%>'></telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ErrorMessage="Corporation Address," Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtCorpAddress"></asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">City<br />
                                                    <telerik:RadTextBox ID="txtCorpCity" runat="server" Width="150" MaxLength="25" Text='<%# Eval("Business.Address.City")%>'></telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true" runat="server" ErrorMessage="Corporation City," Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtCorpCity"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">State<br />
                                                    <telerik:RadDropDownList ID="ddlCorpState" runat="server"
                                                        SelectMethod="BindLegalOrgStates"
                                                        AppendDataBoundItems="true"
                                                        DataTextField="Name"
                                                        DataValueField="RecordId"
                                                        DropDownHeight="200px"
                                                        Width="100px"
                                                        DropDownWidth="150px"
                                                        SelectedValue='<%# Eval("Business.Address.State.RecordId")%>'
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true" runat="server" ErrorMessage="Corporation State," Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="ddlCorpState"></asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Zip<br />
                                                    <telerik:RadTextBox ID="txtCorpZip" runat="server" Width="80" MaxLength="10" Text='<%# Eval("Business.Address.Zip")%>'></telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true" runat="server" ErrorMessage="Corporation Zip," Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtCorpZip"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Business License #<br />
                                                    <telerik:RadTextBox ID="txtBusLicNumber" runat="server" Width="100" MaxLength="25" Text='<%# Eval("BusLicNumber")%>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">License Type<br />
                                                    <telerik:RadTextBox ID="txtBusLicType" runat="server" Width="100" MaxLength="50" Text='<%# Eval("BusLicType")%>'></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">License Issuer<br />
                                                    <telerik:RadTextBox ID="txtBusLicIssuer" runat="server" Width="150" MaxLength="25" Text='<%# Eval("BusLicIssuer")%>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="1" class="leftFormValues" style="width: 25%">License Date<br />
                                                    <div style="text-align:center">
                                                        <asp:CustomValidator ID="cvLicenseDate" runat="server" ErrorMessage="Business License Date," 
                                                            SetFocusOnError="true" Text="*" ForeColor="Red" Font-Size="20px" OnServerValidate="cvLicenseDate_ServerValidate"></asp:CustomValidator>
                                                        <telerik:RadDropDownList runat="server" ID="ddlLicenseDateYear" CssClass="dropdown"
                                                            SelectMethod="FillDateYears"
                                                            DataTextField="YearText"
                                                            DataValueField="YearValue"
                                                            Width="55px"
                                                            DropDownWidth="100px"
                                                            DefaultMessage="Year"
                                                            DropDownHeight="150px"
                                                            SelectedValue='<%# Eval("BusLicDate") != null ? DateTime.Parse(Eval("BusLicDate").ToString()).Year.ToString() : "" %>'
                                                            >
                                                        </telerik:RadDropDownList>
                                                        <telerik:RadDropDownList runat="server" ID="ddlLicenseDateMonth" CssClass="dropdown"
                                                            SelectMethod="FillDateMonths"
                                                            DataTextField="MonthText"
                                                            DataValueField="MonthValue"
                                                            Width="50px"
                                                            DropDownWidth="75px"
                                                            DefaultMessage="M"
                                                            DropDownHeight="150px"
                                                            SelectedValue='<%# Eval("BusLicDate") != null ? DateTime.Parse(Eval("BusLicDate").ToString()).Month.ToString() : "" %>'
                                                            >
                                                        </telerik:RadDropDownList>
                                                        <telerik:RadDropDownList runat="server" ID="ddlLicenseDateDay" CssClass="dropdown"
                                                            SelectMethod="FillDateDays"
                                                            DataTextField="DayText"
                                                            DataValueField="DayValue"
                                                            Width="45px"
                                                            DropDownWidth="75px"
                                                            DefaultMessage="D"
                                                            DropDownHeight="150px"
                                                            SelectedValue='<%# Eval("BusLicDate") != null ? DateTime.Parse(Eval("BusLicDate").ToString()).Day.ToString() : "" %>'
                                                            >
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                </td>
                                                <td colspan="1" class="leftFormValues" style="width: 25%">Federal Tax ID #<br />
                                                    <telerik:RadTextBox ID="txtFedTaxId" runat="server" Width="150" MaxLength="11" Text='<%# Eval("FedTaxId")%>'></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Business Email Address&nbsp;&nbsp;&nbsp;
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Business Email, " ForeColor="Red" Text="* Invalid Business Email" ValidationExpression=".+\@.+\..+" Font-Size="14px" ControlToValidate="txtBusEmail"></asp:RegularExpressionValidator><br />
                                                    <telerik:RadTextBox ID="txtBusEmail" runat="server" Width="250" MaxLength="100" Text='<%# Eval("Business.Email")%>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="1" class="leftFormValues" style="width: 25%">Business Phone<br />
                                                    <telerik:RadTextBox ID="txtBusPhone" runat="server" Width="125" MaxLength="15" Text='<%# Eval("Business.HomePhone")%>'></telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" SetFocusOnError="true" runat="server" ErrorMessage="Business Phone," Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtBusPhone"></asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="1" class="leftFormValues" style="width: 25%">Business Fax<br />
                                                    <telerik:RadTextBox ID="txtBusFax" runat="server" Width="125" MaxLength="15" Text='<%# Eval("Business.Fax")%>'></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" colspan="4" style="width: 100%">Merchandise / Service Sold by Merchant<br />
                                                    <telerik:RadTextBox ID="txtMerchandiseSold" runat="server" TextMode="MultiLine" Width="675px" MaxLength="1000" Height="30px" Text='<%# Eval("MerchandiseSold")%>'></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%;">Years In Business:<br />
                                                    Years:
                                                    <telerik:RadNumericTextBox runat="server" Width="50" ID="txtYearsInBus"
                                                        Type="Number" MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                                                        Text='<%# Eval("YearsInBusiness")%>'>
                                                    </telerik:RadNumericTextBox>
                                                    Months:
                                                    <telerik:RadNumericTextBox runat="server" Width="50" ID="txtMonthsInBus"
                                                        Type="Number" MaxValue="11" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                                                        Text='<%# Eval("MonthsInBusiness")%>'>
                                                    </telerik:RadNumericTextBox>

                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%;">
                                                    <table>
                                                        <tr>
                                                            <td>Seasonal Sales: </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblSeasonal" runat="server" RepeatDirection="Horizontal"
                                                                    OnDataBinding="rblSeasonal_DataBinding">
                                                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="False">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:Panel ID="pnlSeasonalMonths" Visible="true" runat="server">
                                                                    <asp:CheckBoxList ID="cblSeasonal" runat="server" RepeatDirection="Horizontal"  RepeatColumns="6" 
                                                                        CssClass="cb-seasonal" OnDataBound="cblSeasonal_DataBound">
                                                                        <asp:ListItem Value="Jan"> Jan</asp:ListItem>
                                                                        <asp:ListItem Value="Feb"> Feb</asp:ListItem>
                                                                        <asp:ListItem Value="Mar"> Mar</asp:ListItem>
                                                                        <asp:ListItem Value="Apr"> Apr</asp:ListItem>
                                                                        <asp:ListItem Value="May"> May</asp:ListItem>
                                                                        <asp:ListItem Value="Jun"> Jun</asp:ListItem>
                                                                        <asp:ListItem Value="Jul"> Jul</asp:ListItem>
                                                                        <asp:ListItem Value="Aug"> Aug</asp:ListItem>
                                                                        <asp:ListItem Value="Sep"> Sep</asp:ListItem>
                                                                        <asp:ListItem Value="Oct"> Oct</asp:ListItem>
                                                                        <asp:ListItem Value="Nov"> Nov</asp:ListItem>
                                                                        <asp:ListItem Value="Dec"> Dec</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                        </table>

                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="PrincipalView" OnPreRender="PrincipalView_PreRender">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="100" class="editFormNames">Prinicpal Information</td>
                                            </tr>
                                            <tr>
                                                <td colspan="21" class="leftFormValues" style="width: 21%">First Name<br />
                                                    <telerik:RadTextBox ID="txtPrincipalFirstName" runat="server" Width="125" MaxLength="30" Text='<%# Eval("MerchantPrincipal.Contact.FirstName")%>'></telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" SetFocusOnError="true" runat="server" ErrorMessage="Principal First Name," Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtPrincipalFirstName"></asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="8" class="leftFormValues" style="width: 8%">M.I.<br />
                                                    <telerik:RadTextBox ID="txtPrincipalMI" runat="server" Width="30" MaxLength="1" Text='<%# Eval("MerchantPrincipal.Contact.MiddleInitial")%>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="21" class="leftFormValues" style="width: 21%">Last Name<br />
                                                    <telerik:RadTextBox ID="txtPrincipalLastName" runat="server" Width="125" MaxLength="30" Text='<%# Eval("MerchantPrincipal.Contact.LastName")%>'></telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" SetFocusOnError="true" runat="server" ErrorMessage="Principal Last Name," Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtPrincipalLastName"></asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="25" class="leftFormValues" style="width: 25%">Date of Birth<br />
                                                    <asp:CustomValidator ID="cvDoB" runat="server" ErrorMessage="Date of Birth Invalid," 
                                                        SetFocusOnError="true" Text="*" ForeColor="Red" Font-Size="20px"
                                                        OnServerValidate="cvDoB_ServerValidate"></asp:CustomValidator>
                                                    <telerik:RadDropDownList runat="server" ID="ddlBirthYear" CssClass="dropdown"
                                                            SelectMethod="FillDateYears"
                                                            DataTextField="YearText"
                                                            DataValueField="YearValue"
                                                            Width="55px"
                                                            DropDownWidth="100px"
                                                            DefaultMessage="Year"
                                                            DropDownHeight="150px"
                                                            SelectedValue='<%# Eval("MerchantPrincipal.PrincipalDoB") != null ? DateTime.Parse(Eval("MerchantPrincipal.PrincipalDoB").ToString()).Year.ToString() : "" %>'
                                                        >
                                                    </telerik:RadDropDownList>
                                                    <telerik:RadDropDownList runat="server" ID="ddlBirthMonth" CssClass="dropdown"
                                                            SelectMethod="FillDateMonths"
                                                            DataTextField="MonthText"
                                                            DataValueField="MonthValue"
                                                            Width="50px"
                                                            DropDownWidth="75px"
                                                            DefaultMessage="M"
                                                            DropDownHeight="150px"
                                                            SelectedValue='<%# Eval("MerchantPrincipal.PrincipalDoB") != null ? DateTime.Parse(Eval("MerchantPrincipal.PrincipalDoB").ToString()).Month.ToString() : "" %>'
                                                        >
                                                    </telerik:RadDropDownList>
                                                    <telerik:RadDropDownList runat="server" ID="ddlBirthDay" CssClass="dropdown"
                                                            SelectMethod="FillDateDays"
                                                            DataTextField="DayText"
                                                            DataValueField="DayValue"
                                                            Width="45px"
                                                            DropDownWidth="75px"
                                                            DefaultMessage="D"
                                                            DropDownHeight="150px"
                                                            SelectedValue='<%# Eval("MerchantPrincipal.PrincipalDoB") != null ? DateTime.Parse(Eval("MerchantPrincipal.PrincipalDoB").ToString()).Day.ToString() : "" %>'
                                                        >
                                                    </telerik:RadDropDownList>
                                                </td>
                                                <td colspan="25" class="leftFormValues" style="width: 25%">SSN
                                                    <asp:CustomValidator ID="cvSSN" SetFocusOnError="true" runat="server" ErrorMessage="Invalid SSN" ForeColor="Red" 
                                                        Text="*" Font-Size="20px" OnServerValidate="cvSSN_ServerValidate"></asp:CustomValidator>
                                                    <br />
                                                    <telerik:RadButton runat="server" ID="lbEditSSN" ButtonType="LinkButton" Text="[Click Here to Edit]" OnClick="lbEditSSN_Click"
                                                        BorderWidth="0" CausesValidation="false" Skin="" CssClass="admin-link-button">
                                                    </telerik:RadButton>
                                                    <br />
                                                    <telerik:RadMaskedTextBox runat="server" ID="txtPrincipalSSN" Width="120" SelectionOnFocus="CaretToBeginning" Text='<%# MaskSsn((byte[])Eval("MerchantPrincipal.PrincipalSsn"), (int)Eval("MerchantPrincipal.RecordId")) %>'
                                                        Mask="***-**-####" Enabled="false" RequireCompleteText="true">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="15" class="leftFormValues" style="width: 15%">% Ownership<br />
                                                    <telerik:RadNumericTextBox ID="txtPrincipalPctOwn" runat="server" Width="65" MinValue="0" MaxValue="100"
                                                        Type="Percent" NumberFormat-DecimalDigits="2" Text='<%# Eval("MerchantPrincipal.PrincipalPctOwn")%>'>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="35" class="leftFormValues" style="width: 35%">Title<br />
                                                    <telerik:RadTextBox ID="txtPrincipalTitle" runat="server" Width="200" MaxLength="50" Text='<%# Eval("MerchantPrincipal.Contact.Title")%>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="25" class="leftFormValues" style="width: 25%">Driver's License #<br />
                                                    <telerik:RadButton runat="server" ID="lbEditDL" ButtonType="LinkButton" Text="[Click Here to Edit]" OnClick="lbEditDL_Click" BorderWidth="0"
                                                        CausesValidation="false" Skin="" CssClass="admin-link-button">
                                                    </telerik:RadButton>
                                                    <telerik:RadTextBox ID="txtPrincipalDLNumber" runat="server" Width="150" MaxLength="20" Text='<%# MaskDLNumber((byte[])Eval("MerchantPrincipal.PrincipalDLNumber"), (int)Eval("MerchantPrincipal.RecordId")) %>'
                                                        Enabled="false">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td colspan="25" class="leftFormValues" style="width: 25%">DL State<br />
                                                    <telerik:RadDropDownList ID="ddlPrincipalDLState" runat="server"
                                                        SelectMethod="BindLegalOrgStates"
                                                        AppendDataBoundItems="true"
                                                        DataTextField="Name"
                                                        DataValueField="RecordId"
                                                        DropDownHeight="200px"
                                                        Width="100px"
                                                        DropDownWidth="150px"
                                                        SelectedValue='<%# Eval("MerchantPrincipal.PrincipalDLState.RecordId")%>'
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="50" class="leftFormValues" style="width: 50%">Home Address<br />
                                                    <telerik:RadTextBox ID="txtPrincipalAddress" runat="server" Width="300" MaxLength="100" Text='<%# Eval("MerchantPrincipal.Contact.Address.Address")%>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="20" class="leftFormValues" style="width: 20%">City<br />
                                                    <telerik:RadTextBox ID="txtPrincipalCity" runat="server" Width="150" MaxLength="50" Text='<%# Eval("MerchantPrincipal.Contact.Address.City")%>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="15" class="leftFormValues" style="width: 15%">State<br />
                                                    <telerik:RadDropDownList ID="ddlPrincipalState" runat="server"
                                                        SelectMethod="BindLegalOrgStates"
                                                        AppendDataBoundItems="true"
                                                        DataTextField="Name"
                                                        DataValueField="RecordId"
                                                        DropDownHeight="200px"
                                                        Width="100px"
                                                        DropDownWidth="150px"
                                                        SelectedValue='<%# Eval("MerchantPrincipal.Contact.Address.State.RecordId")%>'
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
                                                </td>
                                                <td colspan="15" class="leftFormValues" style="width: 15%">Zip<br />
                                                    <telerik:RadMaskedTextBox ID="txtPrincipalZip" runat="server" Width="75" Mask="#####-####" RequireCompleteText="false" Text='<%# Eval("MerchantPrincipal.Contact.Address.Zip")%>'></telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="50" class="leftFormValues" style="width: 50%">Home Phone
                                                    <asp:CustomValidator ID="cvHomePhone" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Home Phone" ForeColor="Red" 
                                                        Text="*" Font-Size="20px" OnServerValidate="cvHomePhone_ServerValidate"></asp:CustomValidator><br />
                                                    <telerik:RadMaskedTextBox ID="txtPrincipalHomePhone" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true" Text='<%# Eval("MerchantPrincipal.Contact.HomePhone")%>'></telerik:RadMaskedTextBox>
                                                </td>
                                                <td colspan="50" class="leftFormValues" style="width: 50%">Cell Phone
                                                    <asp:CustomValidator ID="cvCellPhone" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Cell Phone" ForeColor="Red" 
                                                        Text="*" Font-Size="20px" OnServerValidate="cvCellPhone_ServerValidate"></asp:CustomValidator><br />
                                                    <telerik:RadMaskedTextBox ID="txtPrincipalCellPhone" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true" Text='<%# Eval("MerchantPrincipal.Contact.CellPhone")%>'></telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="ContactView" OnPreRender="ContactView_PreRender">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="5"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class="editFormNames">Merchant Contact Information
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width: 15%">Contact First Name<br />
                                                    <telerik:RadTextBox ID="txtContactFirstName" runat="server" Width="100" MaxLength="50" Text='<%# Eval("Contact.FirstName") %>'></telerik:RadTextBox>
                                                </td>
                                                <td class="leftFormValues" style="width: 15%">Contact Last Name<br />
                                                    <telerik:RadTextBox ID="txtContactLastName" runat="server" Width="100" MaxLength="50" Text='<%# Eval("Contact.LastName") %>'></telerik:RadTextBox>
                                                </td>
                                                <td class="leftFormValues" style="width: 20%">Contact Email
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Contact Email," Display="Dynamic" 
                                                        ValidationExpression=".+\@.+\..+" ControlToValidate="txtContactEmail" Font-Size="20px" ForeColor="Red" Text="*"></asp:RegularExpressionValidator><br />
                                                    <telerik:RadTextBox ID="txtContactEmail" runat="server" Width="150" MaxLength="100" Text='<%# Eval("Contact.Email")%>'></telerik:RadTextBox>
                                                    
                                                </td>
                                                <td class="leftFormValues" style="width: 20%">Contact Phone
                                                    <asp:CustomValidator ID="cvContactPhone" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Contact Phone" ForeColor="Red" 
                                                        Text="*" Font-Size="20px" OnServerValidate="cvContactPhone_ServerValidate"></asp:CustomValidator><br />
                                                    <telerik:RadMaskedTextBox ID="txtContactPhone" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true" Text='<%# Eval("Contact.HomePhone")%>'></telerik:RadMaskedTextBox>
                                                </td>
                                                <td class="leftFormValues" style="width: 20%">Contact Fax
                                                    <asp:CustomValidator ID="cvContactFax" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Contact Fax" ForeColor="Red" 
                                                        Text="*" Font-Size="20px" OnServerValidate="cvContactFax_ServerValidate"></asp:CustomValidator><br />
                                                    <telerik:RadMaskedTextBox ID="txtContactFax" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true" Text='<%# Eval("Contact.Fax")%>'></telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="BankingView" OnPreRender="BankingView_PreRender">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="4" class="editFormNames">Banking Information</td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width: 25%">Bank Name
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" SetFocusOnError="true" runat="server" ErrorMessage="Bank Name," Display="Dynamic" ForeColor="Red" 
                                                        Font-Size="20px" ControlToValidate="txtBankName" Text="*"></asp:RequiredFieldValidator><br />
                                                    <telerik:RadTextBox ID="txtBankName" runat="server" Width="175" MaxLength="100" Text='<%# Eval("DebitCard.Bank.BankName")%>'></telerik:RadTextBox>
                                                    
                                                </td>
                                                <td class="leftFormValues" style="width: 25%">Bank City
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" SetFocusOnError="true" runat="server" ErrorMessage="Bank City," Display="Dynamic" ForeColor="Red" 
                                                        Font-Size="20px" ControlToValidate="txtBankCity" Text="*"></asp:RequiredFieldValidator><br />
                                                    <telerik:RadTextBox ID="txtBankCity" runat="server" Width="175" MaxLength="50" Text='<%# Eval("DebitCard.Bank.BankCity")%>'></telerik:RadTextBox>
                                                    
                                                </td>
                                                <td class="leftFormValues" style="width: 25%">Bank State
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" SetFocusOnError="true" runat="server" ErrorMessage="Bank State," Display="Dynamic" ForeColor="Red" 
                                                        Font-Size="20px" ControlToValidate="ddlBankState" Text="*"></asp:RequiredFieldValidator><br />
                                                    <telerik:RadDropDownList ID="ddlBankState" runat="server"
                                                        SelectMethod="BindLegalOrgStates"
                                                        AppendDataBoundItems="true"
                                                        DataTextField="Name"
                                                        DataValueField="RecordId"
                                                        DropDownHeight="200px"
                                                        Width="100px"
                                                        DropDownWidth="150px"
                                                        SelectedValue='<%# Eval("DebitCard.Bank.BankState.RecordId")%>'
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
                                                    
                                                </td>
                                                <td class="leftFormValues" style="width: 25%">Bank Phone
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator51" SetFocusOnError="true" runat="server" ErrorMessage="Bank Phone," Display="Dynamic" ForeColor="Red" 
                                                        Font-Size="20px" ControlToValidate="txtBankPhone" Text="*"></asp:RequiredFieldValidator><br />
                                                    <telerik:RadMaskedTextBox ID="txtBankPhone" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true" Text='<%# Eval("DebitCard.Bank.BankPhone")%>'></telerik:RadMaskedTextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Debit Card Number
                                                    <asp:CustomValidator ID="cvDebitCard" runat="server" ErrorMessage="Invalid Debit Card Number," Display="Dynamic" ForeColor="Red" Text=" * Invalid"
                                                        Font-Size="12px" ControlToValidate="txtDebitCardNumber" OnServerValidate="cvDebitCard_ServerValidate"></asp:CustomValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator52" SetFocusOnError="true" runat="server" ErrorMessage="Debit Card Number," Display="Dynamic" 
                                                        Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtDebitCardNumber"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <telerik:RadButton runat="server" ID="lbEditDebitCard" ButtonType="LinkButton" Text="[Click Here to Edit]" OnClick="lbEditDebitCard_Click"
                                                        Skin="" CssClass="admin-link-button" BorderWidth="0" CausesValidation="false">
                                                    </telerik:RadButton>
                                                    <br />
                                                    <telerik:RadTextBox ID="txtDebitCardNumber" runat="server" Width="200" MaxLength="20" Enabled="false"
                                                        Text='<%# MaskDebitCard((byte[])Eval("DebitCard.DebitCardNumber"), (int)Eval("DebitCard.RecordId")) %>'>
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Bank Account #
                                                    <asp:CustomValidator ID="cvAccountNumber" runat="server" ErrorMessage="Invalid Account Number," Display="Dynamic" ForeColor="Red" Text=" * Invalid"
                                                        Font-Size="12px" ControlToValidate="txtAccountNumber" OnServerValidate="cvAccountNumber_ServerValidate"></asp:CustomValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator53" SetFocusOnError="true" runat="server" ErrorMessage="Account Number," Display="Dynamic" 
                                                        Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtAccountNumber"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <telerik:RadButton runat="server" ID="lbEditBankAccount" ButtonType="LinkButton" Text="[Click Here to Edit]" OnClick="lbEditBankAccount_Click"
                                                        Skin="" CssClass="admin-link-button" BorderWidth="0" CausesValidation="false">
                                                    </telerik:RadButton>
                                                    <br />
                                                    <telerik:RadTextBox ID="txtAccountNumber" runat="server" Width="200" MaxLength="20" Enabled="false"
                                                        Text='<%# MaskAccountNumber((byte[])Eval("BankAccount.AccountNumber"), (int)Eval("BankAccount.RecordId")) %>'>
                                                    </telerik:RadTextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Debit Card Expiration Date
                                                    <asp:CustomValidator ID="cvExpDate" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Expiration Date" ForeColor="Red" Display="Dynamic"
                                                        Text="* Invalid" Font-Size="20px" OnServerValidate="cvExpDate_ServerValidate"></asp:CustomValidator><br />
                                                    <telerik:RadButton runat="server" ID="lbEditExpDate" ButtonType="LinkButton" Text="[Click Here to Edit]"
                                                        OnClick="lbEditExpDate_Click" BorderWidth="0" CausesValidation="false" Skin="" CssClass="admin-link-button">
                                                    </telerik:RadButton>
                                                    <br />
                                                    Month:
                                                    <telerik:RadDropDownList ID="ddlExpMonth" runat="server"
                                                        DefaultMessage="Select"
                                                        Width="75px"
                                                        Enabled="false"
                                                        SelectedValue='<%# Eval("DebitCard.DebitCardExpMonth") %>'>
                                                        <Items>
                                                            <telerik:DropDownListItem Text="01 - Jan" Value="01" />
                                                            <telerik:DropDownListItem Text="02 - Feb" Value="02" />
                                                            <telerik:DropDownListItem Text="03 - Mar" Value="03" />
                                                            <telerik:DropDownListItem Text="04 - Apr" Value="04" />
                                                            <telerik:DropDownListItem Text="05 - May" Value="05" />
                                                            <telerik:DropDownListItem Text="06 - Jun" Value="06" />
                                                            <telerik:DropDownListItem Text="07 - Jul" Value="07" />
                                                            <telerik:DropDownListItem Text="08 - Aug" Value="08" />
                                                            <telerik:DropDownListItem Text="09 - Sep" Value="09" />
                                                            <telerik:DropDownListItem Text="10 - Oct" Value="10" />
                                                            <telerik:DropDownListItem Text="11 - Nov" Value="11" />
                                                            <telerik:DropDownListItem Text="12 - Dec" Value="12" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator67" SetFocusOnError="true" runat="server" ErrorMessage="Expiration Month," ForeColor="Red" Text="*" Font-Size="20px" 
                                                        ControlToValidate="ddlExpMonth" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;&nbsp;
                                                    Year:
                                                    <telerik:RadDropDownList ID="ddlExpYear" runat="server"
                                                        DefaultMessage="Select"
                                                        Width="75px"
                                                        Enabled="false"
                                                        SelectedValue='<%# Eval("DebitCard.DebitCardExpYear") %>'>
                                                        <Items>
                                                            <telerik:DropDownListItem Text="2014" Value="14" />
                                                            <telerik:DropDownListItem Text="2015" Value="15" />
                                                            <telerik:DropDownListItem Text="2016" Value="16" />
                                                            <telerik:DropDownListItem Text="2017" Value="17" />
                                                            <telerik:DropDownListItem Text="2018" Value="18" />
                                                            <telerik:DropDownListItem Text="2019" Value="19" />
                                                            <telerik:DropDownListItem Text="2020" Value="20" />
                                                            <telerik:DropDownListItem Text="2021" Value="21" />
                                                            <telerik:DropDownListItem Text="2022" Value="22" />
                                                            <telerik:DropDownListItem Text="2023" Value="23" />
                                                            <telerik:DropDownListItem Text="2024" Value="24" />
                                                            <telerik:DropDownListItem Text="2025" Value="25" />
                                                            <telerik:DropDownListItem Text="2026" Value="26" />
                                                            <telerik:DropDownListItem Text="2027" Value="27" />
                                                            <telerik:DropDownListItem Text="2028" Value="28" />
                                                            <telerik:DropDownListItem Text="2029" Value="29" />
                                                            <telerik:DropDownListItem Text="2030" Value="30" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    
                                                    <br />
                                                    
                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%">Bank Routing #
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator55" SetFocusOnError="true" runat="server" ErrorMessage="Routing Number," Display="Dynamic" 
                                                        Text="*" ForeColor="Red" Font-Size="20px" ControlToValidate="txtRoutingNumber"></asp:RequiredFieldValidator><br />
                                                    <telerik:RadButton runat="server" ID="lbEditRoutingNumber" ButtonType="LinkButton" Text="[Click Here to Edit]" OnClick="lbEditRoutingNumber_Click"
                                                        Skin="" CssClass="admin-link-button" BorderWidth="0" CausesValidation="false">
                                                    </telerik:RadButton><br />
                                                    <telerik:RadTextBox ID="txtRoutingNumber" runat="server" Width="200" MaxLength="10" Enabled="false" Text='<%# Eval("BankAccount.RoutingNumber")%>'>
                                                    </telerik:RadTextBox>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="UsersView" OnPreRender="UsersView_PreRender">
                                        <asp:UpdatePanel runat="server" ID="upUsersForMerchant">
                                        <ContentTemplate>
                                            <div style="width:100%; margin-left:auto; margin-right:auto; text-align:center">
                                                <asp:Panel runat="server" ID="pnlUserList" Visible="true">
                                                    <telerik:RadGrid runat="server" ID="gridUserList" AllowSorting="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Height="400px"
                                                        OnUpdateCommand="gridUserList_UpdateCommand" OnNeedDataSource="gridUserList_NeedDataSource" UpdateMethod="UpdateUser"
                                                        OnItemDataBound="gridUserList_ItemDataBound" OnItemCommand="gridUserList_ItemCommand" OnExportCellFormatting="gridUserList_ExportCellFormatting"
                                                        OnItemCreated="gridUserList_ItemCreated" AllowAutomaticUpdates="false" AllowAutomaticInserts="false" OnCancelCommand="gridUserList_CancelCommand"
                                                        InsertMethod="InsertUser">
                                                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                                            Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                        </ClientSettings>
                                                        <PagerStyle AlwaysVisible="true" />
                                                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
                                                            Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
                                                            HideStructureColumns="true" FileName="UserList">
                                                        </ExportSettings>
                                                        <MasterTableView SelectMethod="GetUsers" AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" EditMode="PopUp" CommandItemDisplay="Top"
                                                            DataKeyNames="Id" InsertItemDisplay="Top">
                                                            <EditFormSettings UserControlName="UserDetails.ascx" EditFormType="WebUserControl"
                                                                PopUpSettings-Height="750px" PopUpSettings-Width="1000px" FormStyle-BackColor="#FBFDFF">
                                                                <PopUpSettings Modal="true" />
                                                            </EditFormSettings>
                                                            <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" ShowAddNewRecordButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="Id"
                                                                    UniqueName="Id" HeaderText="Id" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="UserName" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="UserName"
                                                                    UniqueName="UserName" HeaderText="User Name">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="FirstName" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                                                    UniqueName="FirstName" HeaderText="First Name">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="LastName" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                                                    UniqueName="LastName" HeaderText="Last Name">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Email" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                                                    UniqueName="Email" HeaderText="Email">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Status.StatusDescription" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                                                    UniqueName="StatusDescription" HeaderText="Status">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true" Width="75px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStatus" runat="server" CssClass="gridMerchantList_StatusCol" Text='<%# Eval("Status.StatusDescription") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Exportable="false"
                                                                    ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" EditText="Details" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                                                                </telerik:GridEditCommandColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings>
                                                            <ClientEvents OnPopUpShowing="onUserPopUpShowing" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                    <br /><br />
                                                    
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlAddUserToMerchant" Visible="false">
                                                        <telerik:RadListView ID="rlvAvailableUsers" runat="server" ItemPlaceholderID="AvailableUserPlaceHolder" AllowMultiItemSelection="true"
                                                            OnSelectedIndexChanged="rlvAvailableUsers_SelectedIndexChanged" SelectMethod="GetAvailableUsers" ItemType="MiqroMoney.Models.ApplicationUser">
                                                            <LayoutTemplate>
                                                                <table class="tableEditAppForm">
                                                                    <tr>
                                                                        <td class="editFormNames">Merchant Contact Information</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="centerFormValues">
                                                                            <span style="parenNote">Please select the users you would like to attach to this merchant.  Click Confirm when you are finished.</span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="centerFormValues">
                                                                            <div class="asLayoutBottom">
                                                                                <asp:Panel ID="AvailableUserPlaceHolder" runat="server"></asp:Panel>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <div class="buttonWrapper">
                                                                    <asp:LinkButton ID="btnUserSelection" Text='<%# Eval("UserName") %>' runat="server" CommandName="Select"
                                                                        CssClass="normalButtons" ToolTip='<%# Eval("FirstName") + " " + Eval("LastName") %>'></asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                            <SelectedItemTemplate>
                                                                <div class="selectButtonWrapper buttonWrapper">
                                                                    <asp:LinkButton ID="btnUserSelection" Text='<%# Eval("UserName") %>' runat="server" CommandName="Select"
                                                                        CssClass="selectedButtons" ToolTip='<%# Eval("FirstName") + " " + Eval("LastName") %>'></asp:LinkButton>
                                                                    <asp:ImageButton ID="DeselectButton" AlternateText="x" runat="server" CssClass="deselectButtons"
                                                                        ImageUrl="~/Admin/Images/remove.png" CommandName="Deselect">
                                                                    </asp:ImageButton>
                                                                </div>
                                                            </SelectedItemTemplate>
                                                        </telerik:RadListView>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upUsersForMerchant">
                                            <ProgressTemplate>
                                                <div id="divLoading" class="ajaxLoader">
                                                    <span runat="server" class="ajaxText">Processing</span>
                                                    <br />
                                                    <br />
                                                    <img src="Images/ajax-loader.gif" alt="Loading" height="256" width="256" style="opacity: .7" /><br />
                                                    <br />
                                                    <span runat="server" class="ajaxTextSmall">Please wait...</span>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </div>
                        </div>

                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="MerchantProcessingView">
                        <div style="float: left; width: 15%; text-align: Left; padding-top: 20px;">
                            <telerik:RadTabStrip ID="tabStripMerchantProcessing" runat="server" SelectedIndex="0" MultiPageID="multiPage3"
                                AutoPostBack="true" Align="Left" Orientation="VerticalLeft" CausesValidation="false" ShowBaseLine="true" Skin="MetroTouch">
                                <Tabs>
                                    <telerik:RadTab Text="Merchant Account" PageViewID="MerchantAccountView"></telerik:RadTab>
                                    <telerik:RadTab Text="Daily Settlements" PageViewID="DailySettlementView"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                        </div>
                        <div style="float: right; width: 84%; text-align: center;">
                            <div style="padding-top: 20px; padding-bottom: 20px;">
                                <telerik:RadMultiPage ID="multiPage3" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true">
                                    <telerik:RadPageView runat="server" ID="MerchantAccountView" OnPreRender="MerchantAccountView_PreRender">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="4" class="editFormNames" style="width: 100%">Merchant Account Information
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width: 25%">Swiped %<br />
                                                    <telerik:RadNumericTextBox runat="server" Width="125" MaxLength="3" ID="txtSwipedPct" Text='<%# Eval("SwipedPct") %>'
                                                        Type="Percent" MaxValue="100" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td class="leftFormValues" style="width: 25%">Average Monthly Sales Volume<br />
                                                    <telerik:RadNumericTextBox runat="server" Width="125" ID="txtAvgMonthlySales" Text='<%# Eval("AvgMonthlySales") %>'
                                                        Type="Currency" MaxValue="999999999" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td class="leftFormValues" style="width: 25%">Highest Monthly Sales Volume<br />
                                                    <telerik:RadNumericTextBox runat="server" Width="125" ID="txtHighestMonthlySales" Text='<%# Eval("HighestMonthlySales") %>'
                                                        Type="Currency" MaxValue="999999999" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td class="leftFormValues" style="width: 25%">Average Weekly Sales Volume<br />
                                                    <telerik:RadNumericTextBox runat="server" Width="125" ID="txtAvgWeeklySales" Text='<%# Eval("AvgWeeklySales") %>'
                                                        Type="Currency" MaxValue="999999999" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="leftFormValues" style="width: 50%;">Name of Current Payment Card Processor<br />
                                                    <telerik:RadTextBox ID="txtCardProcessor" runat="server" Width="200" MaxLength="30" Text='<%# Eval("Processor.ProcessorName") %>'></telerik:RadTextBox>
                                                </td>
                                                <td colspan="2" class="leftFormValues" style="width: 50%;">Merchant Account ID<br />
                                                    <telerik:RadMaskedTextBox ID="txtMerchantId" runat="server" Text='<%# Eval("MerchantId") %>' Width="200" Mask="##################" PromptChar="" 
                                                        HideOnBlur="true" RequireCompleteText="false"></telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="centerFormValues" style="width: 100%">Has the merchant or any principal of the merchant ever been included on the Member Alert Control High-Risk Merchants list?<br />
                                                    <span class="parenNote">(Formerly known as the Combined Terminated Merchated File)</span><br />
                                                    <div style="margin-left: auto; margin-right: auto; width: 60%; text-align: center;">
                                                        <asp:RadioButtonList ID="rblHighRisk" runat="server" RepeatDirection="Horizontal" Width="100%"
                                                            OnDataBinding="rblHighRisk_DataBinding">
                                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div style="margin-left: auto; margin-right: auto; width: 60%; text-align: center;">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>Reported by whom:</td>
                                                                <td>Date:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtHighRiskWho" runat="server" MaxLength="50"
                                                                        Text='<%# Eval("HighRiskWho") %>'>
                                                                    </telerik:RadTextBox>
                                                                    <asp:CustomValidator ID="cvHighRiskWho" runat="server" ErrorMessage="High Risk Reported By Whom Required," 
                                                                        SetFocusOnError="true" Text="*" ForeColor="Red" Font-Size="20px" OnServerValidate="cvHighRiskWho_ServerValidate"></asp:CustomValidator>
                                                                </td>
                                                                <td>
                                                                    <div style="text-align:center">
                                                                        <asp:CustomValidator ID="cvHighRiskDate" runat="server" ErrorMessage="High Risk Reported Date Invalid," 
                                                                        SetFocusOnError="true" Text="*" ForeColor="Red" Font-Size="20px" OnServerValidate="cvHighRiskDate_ServerValidate"></asp:CustomValidator>
                                                                        <telerik:RadDropDownList runat="server" ID="ddlHighRiskYear" CssClass="dropdown"
                                                                            SelectMethod="FillDateYears"
                                                                            DataTextField="YearText"
                                                                            DataValueField="YearValue"
                                                                            Width="55px"
                                                                            DropDownWidth="100px"
                                                                            DefaultMessage="Year"
                                                                            DropDownHeight="150px"
                                                                            SelectedValue='<%# Eval("HighRiskDate") != null ? DateTime.Parse(Eval("HighRiskDate").ToString()).Year.ToString() : "" %>'
                                                                            >
                                                                        </telerik:RadDropDownList>
                                                                        <telerik:RadDropDownList runat="server" ID="ddlHighRiskMonth" CssClass="dropdown"
                                                                            SelectMethod="FillDateMonths"
                                                                            DataTextField="MonthText"
                                                                            DataValueField="MonthValue"
                                                                            Width="50px"
                                                                            DropDownWidth="75px"
                                                                            DefaultMessage="M"
                                                                            DropDownHeight="150px"
                                                                            SelectedValue='<%# Eval("HighRiskDate") != null ? DateTime.Parse(Eval("HighRiskDate").ToString()).Month.ToString() : "" %>'
                                                                            >
                                                                        </telerik:RadDropDownList>
                                                                        <telerik:RadDropDownList runat="server" ID="ddlHighRiskDay" CssClass="dropdown"
                                                                            SelectMethod="FillDateDays"
                                                                            DataTextField="DayText"
                                                                            DataValueField="DayValue"
                                                                            Width="45px"
                                                                            DropDownWidth="75px"
                                                                            DefaultMessage="D"
                                                                            DropDownHeight="150px"
                                                                            SelectedValue='<%# Eval("HighRiskDate") != null ? DateTime.Parse(Eval("HighRiskDate").ToString()).Day.ToString() : "" %>'
                                                                            >
                                                                        </telerik:RadDropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="centerFormValues" style="width: 100%">Has the merchant or any principal of the merchant ever filed for Bankruptcy?<br />
                                                    <div style="margin-left: auto; margin-right: auto; width: 40%; text-align: center;">
                                                        <asp:RadioButtonList ID="rblBankruptcy" runat="server" RepeatDirection="Horizontal" Width="100%"
                                                            OnDataBinding="rblBankruptcy_DataBinding">
                                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                        Bankruptcy Date:<br />
                                                        <div style="text-align:center">
                                                            <asp:CustomValidator ID="cvBankruptcyDate" runat="server" ErrorMessage="Bankruptcy Date Invalid," 
                                                                            SetFocusOnError="true" Text="*" ForeColor="Red" Font-Size="20px"
                                                                            OnServerValidate="cvBankruptcyDate_ServerValidate"></asp:CustomValidator>
                                                            <telerik:RadDropDownList runat="server" ID="ddlBankruptcyYear" CssClass="dropdown"
                                                                SelectMethod="FillDateYears"
                                                                DataTextField="YearText"
                                                                DataValueField="YearValue"
                                                                Width="55px"
                                                                DropDownWidth="100px"
                                                                DefaultMessage="Year"
                                                                DropDownHeight="150px"
                                                                SelectedValue='<%# Eval("BankruptcyDate") != null ? DateTime.Parse(Eval("BankruptcyDate").ToString()).Year.ToString() : "" %>'
                                                                >
                                                            </telerik:RadDropDownList>
                                                            <telerik:RadDropDownList runat="server" ID="ddlBankruptcyMonth" CssClass="dropdown"
                                                                SelectMethod="FillDateMonths"
                                                                DataTextField="MonthText"
                                                                DataValueField="MonthValue"
                                                                Width="50px"
                                                                DropDownWidth="75px"
                                                                DefaultMessage="M"
                                                                DropDownHeight="150px"
                                                                SelectedValue='<%# Eval("BankruptcyDate") != null ? DateTime.Parse(Eval("BankruptcyDate").ToString()).Month.ToString() : "" %>'
                                                                >
                                                            </telerik:RadDropDownList>
                                                            <telerik:RadDropDownList runat="server" ID="ddlBankruptcyDay" CssClass="dropdown"
                                                                SelectMethod="FillDateDays"
                                                                DataTextField="DayText"
                                                                DataValueField="DayValue"
                                                                Width="45px"
                                                                DropDownWidth="75px"
                                                                DefaultMessage="D"
                                                                DropDownHeight="150px"
                                                                SelectedValue='<%# Eval("BankruptcyDate") != null ? DateTime.Parse(Eval("BankruptcyDate").ToString()).Day.ToString() : "" %>'
                                                                >
                                                            </telerik:RadDropDownList>
                                                        </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="DailySettlementView">
                                        <div class="tableEditAppForm">
                                            <telerik:RadGrid runat="server" ID="gridDailySettlements" AllowSorting="True" AllowPaging="True"
                                                AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" OnItemDataBound="gridDailySettlements_ItemDataBound">
                                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                                    Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                                <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true"
                                                    OpenInNewWindow="true" UseItemStyles="true" Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
                                                    HideStructureColumns="true" FileName="DailySettlementList">
                                                </ExportSettings>
                                                <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" CommandItemDisplay="Top"
                                                    DataKeyNames="RecordId" SelectMethod="GetSettlements">
                                                    <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true"
                                                        ShowAddNewRecordButton="false" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="RecordId" UniqueName="RecordId" HeaderText="Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="ActivityDate" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="ActivityDate" UniqueName="ActivityDate" HeaderText="Activity Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTimestamp" runat="server" Text='<%# ((DateTime)Eval("ActivityDate")).ToShortDateString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="ProcessedDate" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="ProcessedDate" UniqueName="ProcessedDate" HeaderText="Payment Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessedDate" runat="server" Text='<%# ParsePaymentDate(Eval("ProcessedDate")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="SalesAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="SalesAmount" UniqueName="SalesAmount" HeaderText="Sales Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSalesAmount" runat="server" Text='<%# "$" + Eval("SalesAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="RefundAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="RefundAmount" UniqueName="RefundAmount" HeaderText="Refund Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefundAmount" runat="server" Text='<%# "$" + Eval("RefundAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="PaymentAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="PaymentAmount" UniqueName="PaymentAmount" HeaderText="Payment Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# "$" + Eval("PaymentAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="ProcessorAdjustmentAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="ProcessorAdjustmentAmount" UniqueName="ProcessorAdjustmentAmount" HeaderText="Processor Adjustment Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessorAdjustmentAmount" runat="server" Text='<%# "$" + Eval("ProcessorAdjustmentAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="DepositAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="DepositAmount" UniqueName="DepositAmount" HeaderText="Net Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepositAmount" runat="server" Text='<%# "$" + Eval("DepositAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn Groupable="False" ReadOnly="True"
                                                            UniqueName="PaymentDetails" Visible="true" AllowFiltering="false" HeaderStyle-Width="50px" ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblPaymentDetails" Text="Details"></asp:Label>
                                                                <telerik:RadToolTip runat="server" ID="ttPaymentDetails" Position="MiddleRight" RelativeTo="Element" TargetControlID="lblPaymentDetails" 
                                                                    ShowEvent="OnMouseOver" Animation="Resize" RenderInPageRoot="true" 
                                                                    AnimationDuration="500" HideEvent="LeaveTargetAndToolTip" AutoCloseDelay="30000" HideDelay="100">
                                                                </telerik:RadToolTip>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </div>
                        </div>

                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="AdvancesView">
                        <div style="float: left; width: 15%; text-align: Left; padding-top: 20px;">
                            <telerik:RadTabStrip ID="tabStripAdvances" runat="server" SelectedIndex="0" MultiPageID="multiPage4"
                                AutoPostBack="true" Align="Left" Orientation="VerticalLeft" CausesValidation="false" ShowBaseLine="true" Skin="MetroTouch">
                                <Tabs>
                                    <telerik:RadTab Text="Active Advances" PageViewID="ActiveAdvancesView"></telerik:RadTab>
                                    <telerik:RadTab Text="Advance History" PageViewID="AdvanceHistoryView"></telerik:RadTab>
                                    <telerik:RadTab Text="Advance Plan" PageViewID="AdvancePlanView"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                        </div>
                        <div style="float: right; width: 84%; text-align: center; padding-top: 20px; padding-bottom: 20px">
                            <div class="tableEditAppForm">
                                <telerik:RadMultiPage ID="multiPage4" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true">
                                    <telerik:RadPageView runat="server" ID="ActiveAdvancesView">
                                        <div style="width: 100%; text-align: center;">
                                            <telerik:RadGrid runat="server" ID="gridActiveAdvances" AllowSorting="True" AllowPaging="True" AllowAutomaticUpdates="false"
                                                AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                                                AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" OnEditCommand="gridActiveAdvances_EditCommand">
                                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                                    Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                                <PagerStyle AlwaysVisible="true" />
                                                <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" CommandItemDisplay="Top" EditMode="PopUp"
                                                    DataKeyNames="RecordId" SelectMethod="GetActiveAdvances" CommandItemSettings-ShowAddNewRecordButton="false">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="RecordId" UniqueName="RecordId" HeaderText="Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Timestamp" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="Timestamp" UniqueName="Timestamp" HeaderText="Request Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTimestamp" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="PrincipalAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="PrincipalAmount" UniqueName="PrincipalAmount" HeaderText="Requested Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrincipalAmount" runat="server" Text='<%# "$" + Eval("PrincipalAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="FeeAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="FeeAmount" UniqueName="FeeAmount" HeaderText="Markup Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeAmount" runat="server" Text='<%# "$" + Eval("FeeAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="PrincipalBalance" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="PrincipalBalance" UniqueName="PrincipalBalance" HeaderText="Principal Balance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrincipalBalance" runat="server" Text='<%# "$" + Eval("PrincipalBalance") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="FeeBalance" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="FeeBalance" UniqueName="FeeBalance" HeaderText="Markup Balance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeBalance" runat="server" Text='<%# "$" + Eval("FeeBalance") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="FundingStatus.StatusDescription" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="FundingStatus.StatusDescription" UniqueName="FundingStatus.StatusDescription" HeaderText="Funding Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFundingStatus" runat="server" Text='<%# Eval("FundingStatus.StatusDescription") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Exportable="false"
                                                            ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" EditText="Details">
                                                        </telerik:GridEditCommandColumn>
                                                    </Columns>
                                                    <EditFormSettings EditFormType="Template"
                                                        PopUpSettings-Height="750px" PopUpSettings-Width="1000px" FormStyle-BackColor="#FBFDFF">
                                                        <PopUpSettings Modal="true" />
                                                        <FormTemplate>
                                                            <div class="editAppHeader">
                                                                <table style="width: 100%; height: 60px" runat="server" id="tblEditAppHeader">
                                                                    <tr>
                                                                        <td id="tdCorp" colspan="2" style="width: 100%; text-align: center; border-bottom: 1px solid #D4D4D4">
                                                                            <asp:Label ID="lblAdvanceId" runat="server" Style="font-size: 36px; color: #FFF;" Text='<%# "Advance ID: " +  Eval("RecordId") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%; text-align: center; font-size: 14px; color: #FFF; border-right: 1px solid #D4D4D4;">
                                                                            <asp:Label ID="lblTimestamp" runat="server" Style="font-size: 14px; color: #FFF; font-weight: bold;" Text='<%# "Requested Timestamp: " + ((DateTime)Eval("Timestamp")).ToLocalTime() %>'></asp:Label>
                                                                        </td>
                                                                        <td style="width: 50%; text-align: center; font-size: 14px; color: #FFF; border-right: 1px solid #D4D4D4;">
                                                                            <asp:Label ID="lblRequestedBy" runat="server" Style="font-size: 14px; color: #FFF; font-weight: bold;" Text='<%# "Requested By: " + Eval("RequestedBy.FirstName") + " " + Eval("RequestedBy.LastName") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div style="height: 100%; width: 100%; text-align: Left">
                                                                <div style="float: left; width: 15%; text-align: left; padding-top: 20px;">
                                                                    <telerik:RadTabStrip ID="tabStripAdvanceDetails" runat="server" SelectedIndex="0" MultiPageID="mpAdvanceDetails" Width="150px"
                                                                        AutoPostBack="true" Align="Left" Orientation="VerticalLeft" CausesValidation="false" ShowBaseLine="true" Skin="MetroTouch">
                                                                        <Tabs>
                                                                            <telerik:RadTab Text="Advance Details" PageViewID="AdvanceView"></telerik:RadTab>
                                                                            <telerik:RadTab Text="Advance Actions" PageViewID="ActionsView"></telerik:RadTab>
                                                                            <telerik:RadTab Text="Repayment Details" PageViewID="RepaymentView"></telerik:RadTab>
                                                                        </Tabs>
                                                                    </telerik:RadTabStrip>
                                                                </div>
                                                                <div style="float: right; width: 84%; text-align: center;">
                                                                    <div>
                                                                        <telerik:RadMultiPage ID="mpAdvanceDetails" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true">
                                                                            <telerik:RadPageView runat="server" ID="AdvanceView">
                                                                                <div style="height: 100%; width: 100%; padding-top: 20px; padding-bottom: 20px;">
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="3" class="editFormNames">Advance Details</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Amount</span><br />
                                                                                                <asp:Label runat="server" ID="lblPrincipalAmount" Text='<%# "$" + Eval("PrincipalAmount") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Markup Amount</span><br />
                                                                                                <asp:Label runat="server" ID="lblMarkupAmount" Text='<%# "$" + Eval("FeeAmount") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Amount</span><br />
                                                                                                <asp:Label runat="server" ID="lblTotalAmount" Text='<%# "$" + ((Decimal)Eval("PrincipalAmount") + (Decimal)Eval("FeeAmount")) %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Balance</span><br />
                                                                                                <asp:Label runat="server" ID="Label1" Text='<%# "$" + Eval("PrincipalBalance") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Markup Balance</span><br />
                                                                                                <asp:Label runat="server" ID="Label2" Text='<%# "$" + Eval("FeeBalance") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Balance</span><br />
                                                                                                <asp:Label runat="server" ID="Label3" Text='<%# "$" + ((Decimal)Eval("PrincipalBalance") + (Decimal)Eval("FeeBalance")) %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="2" class="editFormNames">Status</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Advance Status</span><br />
                                                                                                <asp:Label runat="server" ID="Label4" Text='<%# Eval("Status.StatusDescription") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Funding Status</span><br />
                                                                                                <asp:Label runat="server" ID="Label5" Text='<%# Eval("FundingStatus.StatusDescription") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues"><span style="font-weight: bold">Advance Age (Days since Request)</span><br />
                                                                                                <asp:Label runat="server" ID="Label6" Text='<%# GetRequestAge(Eval("Timestamp"), Eval("CompletedTimestamp"), Eval("Status.StatusDescription")) %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues"><span style="font-weight: bold">Funding Age (Days since Funding)</span><br />
                                                                                                <asp:Label runat="server" ID="Label7" Text='<%# GetFundingAge(Eval("FundedTimestamp"), Eval("CompletedTimestamp"), Eval("FundingStatus.StatusDescription")) %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="4" class="editFormNames">Timestamps</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Last Update Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblLastUpdateTimestamp" Text='<%# Eval("LastUpdateTimestamp") == null ? "N/A" : ((DateTime)Eval("LastUpdateTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Approved Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblApprovedTimestamp" Text='<%# Eval("ApprovedTimestamp") == null ? "N/A" : ((DateTime)Eval("ApprovedTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Funded Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblFundedTimestamp" Text='<%# Eval("FundedTimestamp") == null ? "N/A" : ((DateTime)Eval("FundedTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Completed Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblCompletedTimestamp" Text='<%# Eval("CompletedTimestamp") == null ? "N/A" : ((DateTime)Eval("CompletedTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="2" class="editFormNames">Notes</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Request Notes</span><br />
                                                                                                <asp:Label runat="server" ID="lblRequestNotes" Text='<%# Eval("RequestNotes") == null ? "N/A" : Eval("RequestNotes") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Administrator Notes</span><br />
                                                                                                <asp:Label runat="server" ID="lblAdminNotes" Text='<%# Eval("AdminNotes") == null ? "N/A" : Eval("AdminNotes") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="2" class="editFormNames">Advance Plan</td>
                                                                                        </tr>
                                                                                         <tr>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Plan</span><br />
                                                                                                <asp:Label runat="server" ID="lblPlan" Text='<%# Eval("AdvancePlan") == null ? "N/A" : Eval("AdvancePlan.RecordId") + ": " + Eval("AdvancePlan.PlanName") %>'></asp:Label>
                                                                                            </td>
                                                                                             <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Details</span><br />
                                                                                                <asp:Label runat="server" ID="lblPlanDetails" Text='<%# Eval("AdvancePlan") == null ? "N/A" : "Markup %: " + Eval("AdvancePlan.PlanDiscountPct") 
                                                                                                    + "<br />Payment Count: " + Eval("AdvancePlan.PaymentCount")
                                                                                                    + "<br />Plan Duration: " + Eval("AdvancePlan.StandardPlanDuration") + " Days / " +  Eval("AdvancePlan.ExtendedPlanDuration") + " Days"
                                                                                                    
                                                                                                    %>'>
                                                                                                </asp:Label>
                                                                                             </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </telerik:RadPageView>
                                                                            <telerik:RadPageView runat="server" ID="ActionsView">
                                                                                <div style="height: 100%; width: 100%; padding-top: 20px; padding-bottom: 20px;">
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="2" class="editFormNames">Manually Update Status</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Advance Status</span><br />
                                                                                                <telerik:RadDropDownList ID="ddlAdvanceStatus" runat="server"
                                                                                                    SelectMethod="BindAdvanceStatuses"
                                                                                                    DataTextField="StatusDescription"
                                                                                                    DataValueField="RecordId"
                                                                                                    Width="100px"
                                                                                                    DropDownWidth="100px"
                                                                                                    SelectedValue='<%# Eval("Status.RecordId")%>'>
                                                                                                </telerik:RadDropDownList>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Funding Status</span><br />
                                                                                                <telerik:RadDropDownList ID="ddlFundingStatus" runat="server"
                                                                                                    SelectMethod="BindFundingStatuses"
                                                                                                    DataTextField="StatusDescription"
                                                                                                    DataValueField="RecordId"
                                                                                                    Width="200px"
                                                                                                    DropDownWidth="200px"
                                                                                                    SelectedValue='<%# Eval("FundingStatus.RecordId")%>'>
                                                                                                </telerik:RadDropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2" style="width: 100%" class="centerFormValues">
                                                                                                <telerik:RadButton ID="btnUpdateStatus" runat="server" Text="Update" CssClass="blue-flat-button" 
                                                                                                    Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnUpdateStatus_Click"
                                                                                                    CausesValidation="false" OnClientClicked="OnManualAdvanceUpdateClientClicked">
                                                                                                </telerik:RadButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td class="editFormNames">Quick Actions</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues">
                                                                                                <table style="width: 100%; padding: 10px;">
                                                                                                    <tr>
                                                                                                        <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Fund Advance >></span></td>
                                                                                                        <td style="width: 25%; text-align: left">Amount: <span style="font-weight: bold">$
                                                                                                            <asp:Label runat="server" ID="lblFundedAmount" Text='<%# Eval("PrincipalAmount") %>'></asp:Label></span>
                                                                                                        </td>
                                                                                                        <td style="width: 35%; text-align: left">
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator72" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                                                                ForeColor="Red" ControlToValidate="txtFundNotes" ValidationGroup="Fund"></asp:RequiredFieldValidator>
                                                                                                            Notes: 
                                                                                                            <telerik:RadTextBox runat="server" ID="txtFundNotes" Width="150px" MaxLength="300" ValidationGroup="Approve" TextMode="MultiLine"></telerik:RadTextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%; text-align: left">
                                                                                                            <telerik:RadButton ID="btnFundAdvance" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                                                                Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnFundAdvance_Click"
                                                                                                                ValidationGroup="Fund">
                                                                                                            </telerik:RadButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" style="padding-top: 20px; padding-bottom: 20px;"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Deny Advance >></span></td>
                                                                                                        <td style="width: 25%; text-align: left"></td>
                                                                                                        <td style="width: 35%; text-align: left">
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" ErrorMessage="*" Font-Size="20px" Text="*"
                                                                                                                ForeColor="Red" ControlToValidate="txtDenyNotes" ValidationGroup="Deny"></asp:RequiredFieldValidator>
                                                                                                            Notes: 
                                                                                                            <telerik:RadTextBox runat="server" ID="txtDenyNotes" Width="150px" MaxLength="300" ValidationGroup="Deny" TextMode="MultiLine"></telerik:RadTextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%; text-align: left">
                                                                                                            <telerik:RadButton ID="btnDenyAdvance" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                                                                Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnDenyAdvance_Click"
                                                                                                                ValidationGroup="Deny">
                                                                                                            </telerik:RadButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" style="padding-top: 20px; padding-bottom: 20px;"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 20%; text-align: left"><span style="font-weight: bold;">Retry Funding >></span></td>
                                                                                                        <td style="width: 25%; text-align: left"></td>
                                                                                                        <td style="width: 35%; text-align: left"></td>
                                                                                                        <td style="width: 20%; text-align: left">
                                                                                                            <telerik:RadButton ID="btnRetryFunding" runat="server" Text="Submit" CssClass="blue-flat-button" 
                                                                                                                Width="135px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnRetryFunding_Click" Enabled="false"
                                                                                                                CausesValidation="false">
                                                                                                            </telerik:RadButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:Label runat="server" ID="lblActionMessage" OnPreRender="lblActionMessage_PreRender" CssClass="admin-message"></asp:Label>

                                                                                </div>
                                                                            </telerik:RadPageView>
                                                                            <telerik:RadPageView runat="server" ID="RepaymentView">
                                                                                <div style="height: 100%; width: 100%; padding-top: 20px; padding-bottom: 20px;">
                                                                                    <div class="tableEditAppForm">
                                                                                        <telerik:RadGrid runat="server" ID="gridAdvanceRepayments" AllowSorting="True" AllowPaging="True"
                                                                                            AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Height="550px">
                                                                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                                                                                Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                                                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                                            </ClientSettings>
                                                                                            <PagerStyle AlwaysVisible="true" />
                                                                                            <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" CommandItemDisplay="Top" EditMode="PopUp"
                                                                                                DataKeyNames="RecordId" SelectMethod="GetAdvanceRepayments" CommandItemSettings-ShowAddNewRecordButton="false">
                                                                                                <Columns>
                                                                                                    <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="RecordId" UniqueName="RecordId" HeaderText="Id" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="Timestamp" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="Timestamp" UniqueName="Timestamp" HeaderText="Payment Date" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblTimestamp" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="ExpectedTotalAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="ExpectedTotalAmount" UniqueName="ExpectedTotalAmount" HeaderText="Expected Payment Amount" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblExpectedTotalAmount" runat="server" Text='<%# "$" + Eval("ExpectedTotalAmount") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="ActualTotalAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="ActualTotalAmount" UniqueName="ActualTotalAmount" HeaderText="Actual Payment Amount" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblActualTotalAmount" runat="server" Text='<%# "$" + Eval("ActualTotalAmount") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="Status.StatusDescription" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="Status.StatusDescription" UniqueName="Status.StatusDescription" HeaderText="Payment Status" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblPaymentStatusDescription" runat="server" Text='<%# Eval("Status.StatusDescription") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Exportable="false"
                                                                                                        ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" EditText="Details">
                                                                                                    </telerik:GridEditCommandColumn>
                                                                                                </Columns>
                                                                                                <EditFormSettings EditFormType="Template" PopUpSettings-Height="400px" PopUpSettings-Width="600px" FormStyle-BackColor="#FBFDFF">
                                                                                                    <PopUpSettings Modal="true" />
                                                                                                    <FormTemplate>
                                                                                                        <div style="height: 400px; overflow-y: auto;">
                                                                                                            <table class="tableEditAppForm">
                                                                                                                <tr>
                                                                                                                    <td colspan="2" class="editFormNames">Advance Repayment Details</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Payment Date</span><br />
                                                                                                                        <asp:Label ID="lblPaymentDate" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Payment Status</span><br />
                                                                                                                        <asp:Label ID="lblPaymentStatus" runat="server" Text='<%# Eval("Status.StatusDescription") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <br />
                                                                                                            <table class="tableEditAppForm">
                                                                                                                <tr>
                                                                                                                    <td colspan="3" class="editFormNames">Expected Repayment Details</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Amount</span><br />
                                                                                                                        <asp:Label ID="lblExpectedTotalAmount" runat="server" Text='<%# Eval("ExpectedTotalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Amount</span><br />
                                                                                                                        <asp:Label ID="lblExpectedPrincipalAmount" runat="server" Text='<%# Eval("ExpectedPrincipalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Fee Amount</span><br />
                                                                                                                        <asp:Label ID="lblExpectedFeeAmount" runat="server" Text='<%# Eval("ExpectedFeeAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <br />
                                                                                                            <table class="tableEditAppForm">
                                                                                                                <tr>
                                                                                                                    <td colspan="3" class="editFormNames">Actual Repayment Details</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Amount</span><br />
                                                                                                                        <asp:Label ID="lblActualTotalAmount" runat="server" Text='<%# Eval("ActualTotalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Amount</span><br />
                                                                                                                        <asp:Label ID="lblActualPrincipalAmount" runat="server" Text='<%# Eval("ActualPrincipalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Fee Amount</span><br />
                                                                                                                        <asp:Label ID="lblActualFeeAmount" runat="server" Text='<%# Eval("ActualFeeAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <br />
                                                                                                            <br />
                                                                                                            <br />
                                                                                                            <telerik:RadButton ID="btnCloseAdvancePaymentDetails" runat="server" Text="Close" CssClass="blue-flat-button" 
                                                                                                                Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" CausesValidation="false" CommandName="Cancel">
                                                                                                            </telerik:RadButton>
                                                                                                        </div>
                                                                                                    </FormTemplate>
                                                                                                </EditFormSettings>
                                                                                            </MasterTableView>
                                                                                            <ClientSettings>
                                                                                                <ClientEvents OnPopUpShowing="onPaymentPopUpShowing" />
                                                                                            </ClientSettings>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                </div>
                                                                            </telerik:RadPageView>
                                                                        </telerik:RadMultiPage>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div style="width: 100%; text-align: center; position: absolute; bottom: 10px;">
                                                                <telerik:RadButton ID="btnCloseAdvanceDetails" runat="server" Text="Close" CssClass="blue-flat-button" 
                                                                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" OnClick="btnCloseAdvanceDetails_Click" ForeColor="White" CausesValidation="false" CommandName="Cancel">
                                                                </telerik:RadButton>
                                                            </div>
                                                        </FormTemplate>
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <ClientEvents OnPopUpShowing="onAdvancesPopUpShowing" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>

                                    <telerik:RadPageView runat="server" ID="AdvanceHistoryView">
                                        <div style="width: 100%; text-align: center;">
                                            <telerik:RadGrid runat="server" ID="gridAdvanceHistory" AllowSorting="True" AllowPaging="True"
                                                AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" OnEditCommand="gridAdvanceHistory_EditCommand">
                                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                                    Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                                <PagerStyle AlwaysVisible="true" />
                                                <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" CommandItemDisplay="Top" EditMode="PopUp"
                                                    DataKeyNames="RecordId" SelectMethod="GetAdvanceHistory" CommandItemSettings-ShowAddNewRecordButton="false">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="RecordId" UniqueName="RecordId" HeaderText="Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Timestamp" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="Timestamp" UniqueName="Timestamp" HeaderText="Request Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTimestamp" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="PrincipalAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="PrincipalAmount" UniqueName="PrincipalAmount" HeaderText="Requested Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrincipalAmount" runat="server" Text='<%# "$" + Eval("PrincipalAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="FeeAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="FeeAmount" UniqueName="FeeAmount" HeaderText="Markup Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeAmount" runat="server" Text='<%# "$" + Eval("FeeAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="PrincipalBalance" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="PrincipalBalance" UniqueName="PrincipalBalance" HeaderText="Principal Balance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrincipalBalance" runat="server" Text='<%# "$" + Eval("PrincipalBalance") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="FeeBalance" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="FeeBalance" UniqueName="FeeBalance" HeaderText="Markup Balance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeBalance" runat="server" Text='<%# "$" + Eval("FeeBalance") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="FundingStatus.StatusDescription" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                                            SortExpression="FundingStatus.StatusDescription" UniqueName="FundingStatus.StatusDescription" HeaderText="Funding Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFundingStatus" runat="server" Text='<%# Eval("FundingStatus.StatusDescription") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Exportable="false"
                                                            ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" EditText="Details">
                                                        </telerik:GridEditCommandColumn>
                                                    </Columns>
                                                    <EditFormSettings EditFormType="Template"
                                                        PopUpSettings-Height="750px" PopUpSettings-Width="1000px" FormStyle-BackColor="#FBFDFF">
                                                        <PopUpSettings Modal="true" />
                                                        <FormTemplate>
                                                            <div class="editAppHeader">
                                                                <table style="width: 100%; height: 60px" runat="server" id="tblEditAppHeader">
                                                                    <tr>
                                                                        <td id="tdCorp" colspan="2" style="width: 100%; text-align: center; border-bottom: 1px solid #D4D4D4">
                                                                            <asp:Label ID="lblAdvanceId" runat="server" Style="font-size: 36px; color: #FFF;" Text='<%# "Advance ID: " +  Eval("RecordId") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%; text-align: center; font-size: 14px; color: #FFF; border-right: 1px solid #D4D4D4;">
                                                                            <asp:Label ID="lblTimestamp" runat="server" Style="font-size: 14px; color: #FFF; font-weight: bold;" Text='<%# "Requested Timestamp: " + ((DateTime)Eval("Timestamp")).ToLocalTime() %>'></asp:Label>
                                                                        </td>
                                                                        <td style="width: 50%; text-align: center; font-size: 14px; color: #FFF; border-right: 1px solid #D4D4D4;">
                                                                            <asp:Label ID="lblRequestedBy" runat="server" Style="font-size: 14px; color: #FFF; font-weight: bold;" Text='<%# "Requested By: " + Eval("RequestedBy.FirstName") + " " + Eval("RequestedBy.LastName") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div style="height: 100%; width: 100%; text-align: Left">
                                                                <div style="float: left; width: 15%; text-align: left; padding-top: 20px;">
                                                                    <telerik:RadTabStrip ID="tabStripAdvanceDetails" runat="server" SelectedIndex="0" MultiPageID="mpAdvanceDetails" Width="150px"
                                                                        AutoPostBack="true" Align="Left" Orientation="VerticalLeft" CausesValidation="false" ShowBaseLine="true" Skin="MetroTouch">
                                                                        <Tabs>
                                                                            <telerik:RadTab Text="Advance Details" PageViewID="AdvanceView"></telerik:RadTab>
                                                                            <telerik:RadTab Text="Repayment Details" PageViewID="RepaymentView"></telerik:RadTab>
                                                                        </Tabs>
                                                                    </telerik:RadTabStrip>
                                                                </div>
                                                                <div style="float: right; width: 84%; text-align: center;">
                                                                    <div style="padding-top: 20px; padding-bottom: 20px;">
                                                                        <telerik:RadMultiPage ID="mpAdvanceDetails" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true">
                                                                            <telerik:RadPageView runat="server" ID="AdvanceView">
                                                                                <div style="height: 100%; width: 100%; padding-top: 20px; padding-bottom: 20px;">
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="3" class="editFormNames">Advance Details</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Amount</span><br />
                                                                                                <asp:Label runat="server" ID="lblPrincipalAmount" Text='<%# "$" + Eval("PrincipalAmount") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Markup Amount</span><br />
                                                                                                <asp:Label runat="server" ID="lblMarkupAmount" Text='<%# "$" + Eval("FeeAmount") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Amount</span><br />
                                                                                                <asp:Label runat="server" ID="lblTotalAmount" Text='<%# "$" + ((Decimal)Eval("PrincipalAmount") + (Decimal)Eval("FeeAmount")) %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Balance</span><br />
                                                                                                <asp:Label runat="server" ID="Label1" Text='<%# "$" + Eval("PrincipalBalance") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Markup Balance</span><br />
                                                                                                <asp:Label runat="server" ID="Label2" Text='<%# "$" + Eval("FeeBalance") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Balance</span><br />
                                                                                                <asp:Label runat="server" ID="Label3" Text='<%# "$" + ((Decimal)Eval("PrincipalBalance") + (Decimal)Eval("FeeBalance")) %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="2" class="editFormNames">Status</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Advance Status</span><br />
                                                                                                <asp:Label runat="server" ID="Label4" Text='<%# Eval("Status.StatusDescription") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Funding Status</span><br />
                                                                                                <asp:Label runat="server" ID="Label5" Text='<%# Eval("FundingStatus.StatusDescription") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues"><span style="font-weight: bold">Advance Age (Days since Request)</span><br />
                                                                                                <asp:Label runat="server" ID="Label6" Text='<%# GetRequestAge(Eval("Timestamp"), Eval("CompletedTimestamp"), Eval("Status.StatusDescription")) %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues"><span style="font-weight: bold">Funding Age (Days since Funding)</span><br />
                                                                                                <asp:Label runat="server" ID="Label7" Text='<%# GetFundingAge(Eval("FundedTimestamp"), Eval("CompletedTimestamp"), Eval("FundingStatus.StatusDescription")) %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="4" class="editFormNames">Timestamps</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Last Update Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblLastUpdateTimestamp" Text='<%# Eval("LastUpdateTimestamp") == null ? "N/A" : ((DateTime)Eval("LastUpdateTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Approved Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblApprovedTimestamp" Text='<%# Eval("ApprovedTimestamp") == null ? "N/A" : ((DateTime)Eval("ApprovedTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Funded Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblFundedTimestamp" Text='<%# Eval("FundedTimestamp") == null ? "N/A" : ((DateTime)Eval("FundedTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Completed Timestamp</span><br />
                                                                                                <asp:Label runat="server" ID="lblCompletedTimestamp" Text='<%# Eval("CompletedTimestamp") == null ? "N/A" : ((DateTime)Eval("CompletedTimestamp")).ToLocalTime().ToString() %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table class="tableEditAppForm">
                                                                                        <tr>
                                                                                            <td colspan="2" class="editFormNames">Notes</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Request Notes</span><br />
                                                                                                <asp:Label runat="server" ID="lblRequestNotes" Text='<%# Eval("RequestNotes") == null ? "N/A" : Eval("RequestNotes") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="centerFormValues" style="width: 50%"><span style="font-weight: bold">Administrator Notes</span><br />
                                                                                                <asp:Label runat="server" ID="lblAdminNotes" Text='<%# Eval("AdminNotes") == null ? "N/A" : Eval("AdminNotes") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </telerik:RadPageView>
                                                                            <telerik:RadPageView runat="server" ID="RepaymentView">
                                                                                <div style="height: 100%; width: 100%; padding-top: 20px; padding-bottom: 20px;">
                                                                                    <div class="tableEditAppForm">
                                                                                        <telerik:RadGrid runat="server" ID="gridAdvanceRepayments" AllowSorting="True" AllowPaging="True"
                                                                                            AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Height="550px">
                                                                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                                                                                Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                                                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                                            </ClientSettings>
                                                                                            <PagerStyle AlwaysVisible="true" />
                                                                                            <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" CommandItemDisplay="Top" EditMode="PopUp"
                                                                                                DataKeyNames="RecordId" SelectMethod="GetAdvanceHistoryRepayments" CommandItemSettings-ShowAddNewRecordButton="false">
                                                                                                <Columns>
                                                                                                    <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="RecordId" UniqueName="RecordId" HeaderText="Id" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="Timestamp" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="Timestamp" UniqueName="Timestamp" HeaderText="Payment Date" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblTimestamp" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="ExpectedTotalAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="ExpectedTotalAmount" UniqueName="ExpectedTotalAmount" HeaderText="Expected Payment Amount" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblExpectedTotalAmount" runat="server" Text='<%# "$" + Eval("ExpectedTotalAmount") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="ActualTotalAmount" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="ActualTotalAmount" UniqueName="ActualTotalAmount" HeaderText="Actual Payment Amount" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblActualTotalAmount" runat="server" Text='<%# "$" + Eval("ActualTotalAmount") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridTemplateColumn DataField="Status.StatusDescription" DataType="System.Decimal" Exportable="true" Groupable="False" ReadOnly="True"
                                                                                                        SortExpression="Status.StatusDescription" UniqueName="Status.StatusDescription" HeaderText="Payment Status" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblPaymentStatusDescription" runat="server" Text='<%# Eval("Status.StatusDescription") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Exportable="false"
                                                                                                        ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" EditText="Details">
                                                                                                    </telerik:GridEditCommandColumn>
                                                                                                </Columns>
                                                                                                <EditFormSettings EditFormType="Template" PopUpSettings-Height="400px" PopUpSettings-Width="600px" FormStyle-BackColor="#FBFDFF">
                                                                                                    <PopUpSettings Modal="true" />
                                                                                                    <FormTemplate>
                                                                                                        <div style="height: 400px; overflow-y: auto;">
                                                                                                            <table class="tableEditAppForm">
                                                                                                                <tr>
                                                                                                                    <td colspan="2" class="editFormNames">Advance Repayment Details</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Payment Date</span><br />
                                                                                                                        <asp:Label ID="lblPaymentDate" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 34%"><span style="font-weight: bold">Payment Status</span><br />
                                                                                                                        <asp:Label ID="lblPaymentStatus" runat="server" Text='<%# Eval("Status.StatusDescription") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <br />
                                                                                                            <table class="tableEditAppForm">
                                                                                                                <tr>
                                                                                                                    <td colspan="3" class="editFormNames">Expected Repayment Details</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Amount</span><br />
                                                                                                                        <asp:Label ID="lblExpectedTotalAmount" runat="server" Text='<%# Eval("ExpectedTotalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Amount</span><br />
                                                                                                                        <asp:Label ID="lblExpectedPrincipalAmount" runat="server" Text='<%# Eval("ExpectedPrincipalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Fee Amount</span><br />
                                                                                                                        <asp:Label ID="lblExpectedFeeAmount" runat="server" Text='<%# Eval("ExpectedFeeAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <br />
                                                                                                            <table class="tableEditAppForm">
                                                                                                                <tr>
                                                                                                                    <td colspan="3" class="editFormNames">Actual Repayment Details</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Total Amount</span><br />
                                                                                                                        <asp:Label ID="lblActualTotalAmount" runat="server" Text='<%# Eval("ActualTotalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Principal Amount</span><br />
                                                                                                                        <asp:Label ID="lblActualPrincipalAmount" runat="server" Text='<%# Eval("ActualPrincipalAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="centerFormValues" style="width: 33%"><span style="font-weight: bold">Fee Amount</span><br />
                                                                                                                        <asp:Label ID="lblActualFeeAmount" runat="server" Text='<%# Eval("ActualFeeAmount") %>'></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <br />
                                                                                                            <br />
                                                                                                            <br />
                                                                                                            <telerik:RadButton ID="btnCloseAdvancePaymentDetails" runat="server" Text="Close" CssClass="blue-flat-button" 
                                                                                                                Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" CausesValidation="false" CommandName="Cancel">
                                                                                                            </telerik:RadButton>
                                                                                                        </div>
                                                                                                    </FormTemplate>
                                                                                                </EditFormSettings>
                                                                                            </MasterTableView>
                                                                                            <ClientSettings>
                                                                                                <ClientEvents OnPopUpShowing="onPaymentPopUpShowing" />
                                                                                            </ClientSettings>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                </div>
                                                                            </telerik:RadPageView>
                                                                        </telerik:RadMultiPage>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div style="width: 100%; text-align: center; position: absolute; bottom: 10px;">
                                                                <telerik:RadButton ID="btnRequestAdvance" runat="server" Text="Close" CssClass="blue-flat-button" 
                                                                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" CausesValidation="false" CommandName="Cancel">
                                                                </telerik:RadButton>
                                                            </div>
                                                        </FormTemplate>
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <ClientEvents OnPopUpShowing="onAdvancesPopUpShowing" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="AdvancePlanView" OnPreRender="AdvancePlanView_PreRender">
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td class="editFormNames">Active Advance Plan</td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues">
                                                    <telerik:RadDropDownList ID="ddlActivePlan" runat="server"
                                                        SelectMethod="BindActiveAdvancePlans"
                                                        DataTextField="PlanName"
                                                        DataValueField="RecordId"
                                                        Width="200px"
                                                        DropDownWidth="200px"
                                                        SelectedValue='<%# Eval("AdvancePlan.RecordId")%>'
                                                        OnSelectedIndexChanged="ddlActivePlan_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </telerik:RadDropDownList>
                                                    <br />
                                                    <br />
                                                    <telerik:RadTextBox runat="server" ID="txtAdvancePlanDescription" Width="400px" TextMode="MultiLine" Text='<%# Eval("AdvancePlan.PlanDescription") %>' Enabled="false">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>

                                        </table>
                                        <br />
                                        <br />
                                        <br />
                                        <table class="tableEditAppForm">
                                            <tr>
                                                <td colspan="2" class="editFormNames">Plan Details</td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues" style="width: 50%">Plan Markup %<br />
                                                    <telerik:RadNumericTextBox runat="server" ID="txtPlanMarkupPct" Text='<%# Eval("AdvancePlan.PlanDiscountPct") %>'
                                                        NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Type="Percent" MaxValue="100" MinValue="1" Enabled="false">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td class="centerFormValues" style="width: 50%">Payment Count<br />
                                                    <telerik:RadNumericTextBox runat="server" ID="txtPaymentCount" Text='<%# Eval("AdvancePlan.PaymentCount") %>'
                                                        NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Type="Number" MaxValue="1000" MinValue="1" Enabled="false">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues" style="width: 50%">Standard Plan Duration (Days)<br />
                                                    <telerik:RadNumericTextBox runat="server" ID="txtStandardPlanDuration" Text='<%# Eval("AdvancePlan.StandardPlanDuration") %>'
                                                        NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Type="Number" MaxValue="365" MinValue="1" Enabled="false">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td class="centerFormValues" style="width: 50%">Extended Plan Duration (Days)<br />
                                                    <telerik:RadNumericTextBox runat="server" ID="txtExtendedPlanDuration" Text='<%# Eval("AdvancePlan.ExtendedPlanDuration") %>'
                                                        NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Type="Number" MaxValue="365" MinValue="1" Enabled="false">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues" style="width: 50%">Minimum Advance Amount<br />
                                                    <telerik:RadNumericTextBox runat="server" ID="txtMinAdvanceAmount" Text='<%# Eval("AdvancePlan.MinimumAdvanceAmount") %>'
                                                        NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="," Type="Currency" MaxValue="1000000" MinValue="1" Enabled="false">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td class="centerFormValues" style="width: 50%">Maximum Advance Amount<br />
                                                    <telerik:RadNumericTextBox runat="server" ID="txtMaxAdvanceAmount" Text='<%# Eval("AdvancePlan.MaximumAdvanceAmount") %>'
                                                        NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="," Type="Currency" MaxValue="1000000" MinValue="1" Enabled="false">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="centerFormValues" style="width: 50%">Increment Value<br />
                                                    <telerik:RadTextBox runat="server" ID="txtPlanIncrementValue" Enabled="false"
                                                        Text='<%# Eval("AdvancePlan.IncrementValue.AdvancePlanIncrementName") + " - " + Eval("AdvancePlan.IncrementValue.IncrementAmount") %>'>
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td class="centerFormValues" style="width: 50%">Status<br />
                                                    <telerik:RadTextBox runat="server" ID="txtAdvancePlanStatus" Enabled="false"
                                                        Text='<%# Eval("AdvancePlan.Status.StatusDescription") %>'>
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>

                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="UnderwritingView" OnPreRender="UnderwritingView_PreRender">
                        <asp:UpdatePanel runat="server" ID="upAddUnderwriting">
                        <ContentTemplate>
                        <div style="width: 100%; text-align: center; padding-top: 20px; padding-bottom: 20px;">
                            <div class="tableEditAppForm">
                                <asp:Panel runat="server" ID="pnlUnderwritingHistory" Visible="true">
                                    <telerik:RadGrid runat="server" ID="gridUnderwritingHistory" AllowSorting="True" AllowPaging="True"
                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true"
                                        OnNeedDataSource="gridUnderwritingHistory_NeedDataSource" Height="550px"
                                        OnItemDataBound="gridUnderwritingHistory_ItemDataBound" OnItemCommand="gridUnderwritingHistory_ItemCommand" OnExportCellFormatting="gridUnderwritingHistory_ExportCellFormatting"
                                        OnItemCreated="gridUnderwritingHistory_ItemCreated" AllowAutomaticUpdates="false" AllowAutomaticInserts="false">
                                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                            Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
                                            Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
                                            HideStructureColumns="true" FileName="UnderwritingHistory">
                                        </ExportSettings>
                                        <MasterTableView SelectMethod="GetUnderwritingHistory" AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" EditMode="PopUp" CommandItemDisplay="Top"
                                            DataKeyNames="RecordId">
                                            <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" ShowAddNewRecordButton="false" />
                                            <Columns>
                                                <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="false" Groupable="False" ReadOnly="True"
                                                    UniqueName="RecordId" HeaderText="Account Number" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Active" DataType="System.Boolean" Exportable="true" Groupable="False" ReadOnly="True"
                                                    UniqueName="Active" HeaderText="Active" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ActiveImage" DataType="System.Boolean" Exportable="true" Groupable="False" ReadOnly="True"
                                                    UniqueName="ActiveImage" Visible="true" AllowFiltering="false" HeaderStyle-Width="50px" ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Image runat="server" ID="imgActive" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Timestamp" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="Timestamp"
                                                    UniqueName="Timestamp" HeaderText="Timestamp">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# Eval("Timestamp") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="UnderwritingDecision" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="UnderwritingDecision"
                                                    UniqueName="UnderwritingDecision" HeaderText="Decision">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnderwritingDecision" runat="server" Text='<%# Eval("UnderwritingDecision") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="UnderwriterInitials" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="UnderwriterInitials"
                                                    UniqueName="UnderwriterInitials" HeaderText="Underwriter Initials">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnderwriterInitials" runat="server" Text='<%# Eval("UnderwriterInitials") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="UnderwriterUser.UserName" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="UnderwriterUser.UserName"
                                                    UniqueName="UnderwriterUser.UserName" HeaderText="Underwriter User">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnderwriterUsername" runat="server" Text='<%# Eval("UnderwriterUser.UserName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn ButtonType="LinkButton" UniqueName="EditCommandColumn" EditText="Details" ItemStyle-CssClass="">
                                                </telerik:GridEditCommandColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template" PopUpSettings-Height="500px" PopUpSettings-Width="600px" FormStyle-BackColor="#FBFDFF">
                                                <PopUpSettings Modal="true" />
                                                <FormTemplate>
                                                    <div style="height: 450px; overflow-y: auto;">
                                                        <table class="tableEditAppForm">
                                                            <tr>
                                                                <td class="centerFormValues" style="width: 33%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Corporate Information Verification</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormCorpInfoNotes" Text='<%# Eval("CorpInfoNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgCorpInfoResult" />
                                                                                <asp:Label ID="lblEditFormCorpInfoResult" Visible="false" runat="server" Text='<%# Eval("CorpInfoResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                                <td class="centerFormValues" style="width: 34%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Business License Status Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormBusLicStatusNotes" Text='<%# Eval("BusLicStatusNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgBusLicStatusResult" />
                                                                                <asp:Label ID="lblEditFormBusLicStatusResult" Visible="false" runat="server" Text='<%# Eval("BusLicStatusResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="centerFormValues" style="width: 33%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">EIN Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormEINNotes" Text='<%# Eval("EINNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgEINResult" />
                                                                                <asp:Label ID="lblEditFormEINResult" Visible="false" runat="server" Text='<%# Eval("EINResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="centerFormValues" style="width: 33%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Principal Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormPrincipalNotes" Text='<%# Eval("PrincipalNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgPrincipalResult" />
                                                                                <asp:Label ID="lblEditFormPrincipalResult" Visible="false" runat="server" Text='<%# Eval("PrincipalResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="centerFormValues" style="width: 34%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Card Sales Indicator Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormCardSalesIndicatorNotes" Text='<%# Eval("CardSalesIndicatorNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgCardSalesIndicatorResult" />
                                                                                <asp:Label ID="lblEditFormCardSalesIndicatorResult" Visible="false" runat="server" Text='<%# Eval("CardSalesIndicatorResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="centerFormValues" style="width: 33%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Banking Info Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormBankingInfoNotes" Text='<%# Eval("BankingInfoNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgBankingInfoResult" />
                                                                                <asp:Label ID="lblEditFormBankingInfoResult" Visible="false" runat="server" Text='<%# Eval("BankingInfoResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="centerFormValues" style="width: 33%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">MCC Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormMCCNotes" Text='<%# Eval("MCCNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgMCCResult" />
                                                                                <asp:Label ID="lblEditFormMCCResult" Visible="false" runat="server" Text='<%# Eval("MCCResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="centerFormValues" style="width: 34%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Business Verification Index</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormBVINotes" Text='<%# Eval("BVINotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgBVIResult" />
                                                                                <asp:Label ID="lblEditFormBVIResult" Visible="false" runat="server" Text='<%# Eval("BVIResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="centerFormValues" style="width: 33%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Tax Liens Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormTaxLiensNotes" Text='<%# Eval("TaxLiensNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgTaxLiensResult" />
                                                                                <asp:Label ID="lblEditFormTaxLiensResult" Visible="false" runat="server" Text='<%# Eval("TaxLiensResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="centerFormValues" style="width: 33%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">Risk Indicator Verification</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormRiskIndicatorNotes" Text='<%# Eval("RiskIndicatorNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgRiskIndicatorResult" />
                                                                                <asp:Label ID="lblEditFormRiskIndicatorResult" Visible="false" runat="server" Text='<%# Eval("RiskIndicatorResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="centerFormValues" style="width: 34%">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="2" class="centerMe">OFAC Match Verification</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="centerMe" style="width: 75%">
                                                                                <telerik:RadTextBox runat="server" ID="txtEditFormOFACMatchNotes" Text='<%# Eval("OFACMatchNotes") %>'
                                                                                    TextMode="MultiLine" Height="40px" Width="100%" Enabled="false">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td class="centerMe" style="width: 25%">
                                                                                <asp:Image runat="server" ID="imgOFACMatchResult" />
                                                                                <asp:Label ID="lblEditFormOFACMatchResult" Visible="false" runat="server" Text='<%# Eval("OFACMatchResult") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="centerFormValues" style="width: 33%"></td>
                                                            </tr>
                                                        </table>
                                                            <br /><br />
                                                        <asp:HyperLink runat="server" ID="linkTempXmlFile" Target="_blank" Text="View Temporary XML File"></asp:HyperLink>
                                                        
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <telerik:RadButton ID="btnCloseUnderwritingDetails" runat="server" Text="Close" CssClass="blue-flat-button" CommandName="Cancel"
                                                            Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" CausesValidation="false">
                                                        </telerik:RadButton>
                                                    </div>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <ClientEvents OnPopUpShowing="onUWPopUpShowing" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlNewUnderwriting" Visible="false">

                                            <table class="tableEditAppForm">
                                                <tr>
                                                    <td colspan="3" class="editFormNames" style="width: 100%">Underwriting Criteria
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UnderwritingBegin" DisplayMode="List" ForeColor="Red" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:50%" class="centerFormValues">To begin a new Underwriting process, enter your initials and click the Begin button.  This will enable the Underwriting Form below and archive the previous underwriting result set in Underwriting History.
                                                    </td>
                                                    <td style="width:25%" class="centerFormValues">Underwriter Initials: 
                                                                <telerik:RadTextBox runat="server" ID="txtUnderwriterInitials" MaxLength="3"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ErrorMessage="Please enter Underwriter's Initials before beginning" Text="*"
                                                            ControlToValidate="txtUnderwriterInitials" ValidationGroup="UnderwritingBegin" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width:25%" class="centerFormValues">
                                                        <telerik:RadButton ID="btnBeginUnderwritingUpdate" runat="server" Text="Begin" CssClass="blue-flat-button" 
                                                            Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnBeginUnderwritingUpdate_Click"
                                                            ValidationGroup="UnderwritingBegin">
                                                        </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <asp:Label runat="server" ID="lblUnderwritingMessage" Text="" ForeColor="Red"></asp:Label>
                                            <br />
                                            <br />
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlNewUnderwriting2" Visible="false">
                                    <div style="width: 90%; text-align: center; margin-left: auto; margin-right: auto;">
                                        <asp:Label runat="server" ID="lblUnderwritingMessage2" Text="" ForeColor="Red"></asp:Label><br /><br />
                                    </div>
                                    <div style="width: 90%; text-align: center; height: 500px; overflow-y: scroll; border: 2px solid black; margin-left: auto; margin-right: auto;">
                                        <table style="margin-left: auto; margin-right: auto; width: 100%">
                                            <tr>
                                                <td style="width:40%" class="editFormNames">Underwriting Step</td>
                                                <td style="width:30%" class="editFormNames">Result</td>
                                                <td style="width:30%" class="editFormNames">Notes</td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:30%">Corporate Information Verified:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWCorpInfoVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWCorpInfoVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWCorpInfoVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:30%">OFAC Match Verified:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWOFACMatchVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWOFACMatchVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWOFACMatchVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">Business License Status:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWBusLicStatusResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWBusLicStatusResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWBUsLicStatusVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">EIN Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWEINVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWEINVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWEINVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">Contact/Principal Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWPrincipalVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWPrincipalVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWPrincipalVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">Card Sales Indicators Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWCardSalesIndicatorsVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWCardSalesIndicatorsVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWCardSalesIndicatorsVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">Banking Information Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWBankingInfoVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWBankingInfoVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWBankingInfoVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">MCC Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWMCCVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWMCCVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWMCCVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">Business Verification Index Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWBVIVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWBVIVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWBVIVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">Tax Liens Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWTaxLiensVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWTaxLiensVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <p></p>
                                                    <telerik:RadTextBox runat="server" ID="txtUWTaxLiensVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftFormValues" style="width:40%">Risk Indicator Verification:</td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <asp:RadioButtonList runat="server" ID="rblUWRiskIndicatorVerifiedResult" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" ErrorMessage="" Text="Pass or Fail Required" ForeColor="Red"
                                                        ControlToValidate="rblUWRiskIndicatorVerifiedResult" InitialValue="0" ValidationGroup="UnderwritingUpdate"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="centerFormValues" style="width:30%">
                                                    <telerik:RadTextBox runat="server" ID="txtUWRiskIndicatorVerifiedNotes" Text="" MaxLength="500" TextMode="MultiLine" Rows="2"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upAddUnderwriting">
                        <ProgressTemplate>
                            <div id="divLoading" class="ajaxLoader">
                                <span runat="server" class="ajaxText">Processing</span>
                                <br />
                                <br />
                                <img src="Images/ajax-loader.gif" alt="Loading" height="256" width="256" style="opacity: .7" /><br />
                                <br />
                                <span runat="server" class="ajaxTextSmall">Please wait...</span>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="EmailMessagesView">
                        <div class="tableEditAppForm">
                            <div style="width: 95%; padding-top: 15px; height: 25px; float: left; text-align: center; position: relative">
                                <span class="parenNote">Below you can view all of the Email Messages that have been sent to you from Central Cash.<br />
                                </span>
                            </div>
                            <asp:PlaceHolder runat="server" ID="phGridMessages">
                                <div style="float: left">
                                    <telerik:RadGrid runat="server" ID="gridMessages" AllowSorting="True" AllowPaging="True" Height="500px"
                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Width="99%" OnItemDataBound="gridMessages_ItemDataBound"
                                        BorderWidth="2px">
                                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                                            Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" CommandItemDisplay="Top" EditMode="PopUp"
                                            DataKeyNames="RecordId" SelectMethod="GetEmailMessages" CommandItemSettings-ShowAddNewRecordButton="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True"
                                                    SortExpression="RecordId" UniqueName="RecordId" HeaderText="Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Emailtemplate.EmailSubject" DataType="System.String" Exportable="true" Groupable="False"
                                                    HeaderText="Email Subject" ReadOnly="True" SortExpression="Emailtemplate.EmailSubject" UniqueName="Emailtemplate.EmailSubject">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmailSubject" runat="server" Text='<%# Eval("Emailtemplate.EmailSubject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Timestamp" DataType="System.DateTime" Exportable="true" Groupable="False" ReadOnly="True"
                                                    SortExpression="Timestamp" UniqueName="Timestamp" HeaderText="Timestamp">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToLocalTime() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="User.Email" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                                    SortExpression="User.Email" UniqueName="User.Email" HeaderText="Sent To">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserEmail" runat="server" Text='<%# Eval("User.Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn Groupable="False" ReadOnly="True" UniqueName="ViewEmail" HeaderText="Details" AllowFiltering="false">
                                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <telerik:RadButton ID="btnViewEmail" runat="server" Text="Details" CommandArgument='<%# Eval("RecordId") %>' OnClick="btnViewEmail_Click"
                                                            ToolTip="View Email Details" ButtonType="SkinnedButton" Enabled="true" CssClass="grid-button">
                                                            <Image EnableImageButton="true" ImageUrl="/Admin/Images/info.png" DisabledImageUrl="/Admin/Images/info.png" />
                                                        </telerik:RadButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>

                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="phEmailContent" Visible ="false">
                                <div style="width: 95%; height: 500px; float: left; overflow-y: scroll; border: 6px solid #d1d0d0; margin-bottom: 25px; padding: 15px; position: relative">
                                    <asp:Literal runat="server" ID="ltlEmailContent">
                                        <div style="width:100%; margin-left:auto; margin-right:auto; text-align:center; padding-top:185px;">
                                            <h1>Please click the View Email Details button above for the email you would like to view.</h1>
                                        </div>
                                    </asp:Literal>
                                </div>
                                        <br />
                            </asp:PlaceHolder>
                        </div>

                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlConfirmSave" Visible="false">
            <table class="tableEditAppForm" style="padding-top: 15px;">
                <tr>
                    <td style="font-size: 18px; font-weight: bold; text-align: center;">Confirm Changes
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <br />
                        <asp:Panel runat="server" ID="pnlChangesExist" Visible="true">
                            Please confirm the changes below before updating the merchant account. 
                            <br />
                            If you wish to exclude any changes from the update, simply uncheck the Confirm Checkbox for that value.
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoChangesExist" Visible="false">
                            There are no pending changes for this merchant.<br />
                            Please click Cancel below to return to the merchant details, or Close to return to the Merchant List.
                        </asp:Panel>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 450px; overflow-y: auto;">
                            <telerik:RadPanelBar runat="server" ID="pbMerchantChanges" Width="100%" ExpandMode="SingleExpandedItem">
                                <Items>
                                    <telerik:RadPanelItem Text="Business Changes" Value="Business">
                                        <ContentTemplate>
                                            <div style="padding: 5px;">
                                                <asp:Repeater ID="rptrBusinessChanges" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="tableEditAppForm">
                                                            <tr>
                                                                <td class="editFormNames">Field Name</td>
                                                                <td class="editFormNames">Old Value</td>
                                                                <td class="editFormNames">New Value</td>
                                                                <td class="editFormNames">Confirm</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblFieldName0" Text='<%# DataBinder.Eval(Container.DataItem, "FieldName") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblOldValue0" Text='<%# DataBinder.Eval(Container.DataItem, "OldValue") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblNewValue0" Text='<%# DataBinder.Eval(Container.DataItem, "NewValue") %>'></asp:Label></td>
                                                            <td class="centerMe" style="padding-top: 4px;">
                                                                <asp:CheckBox runat="server" ID="cbConfirmed0" Checked="true" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td class="centerMe">
                                                                <asp:CheckBox runat="server" ID="cbSelectAllBusinessChanges" Checked="true" OnCheckedChanged="cbSelectAllBusinessChanges_CheckedChanged" AutoPostBack="true" /><br />
                                                                <span class="parenNote">(Select All)</span></td>
                                                        </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Principal Changes" Value="Principal">
                                        <ContentTemplate>
                                            <div style="padding: 5px;">
                                                <asp:Repeater ID="rptrPrincipalChanges" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="tableEditAppForm">
                                                            <tr>
                                                                <td class="editFormNames">Field Name</td>
                                                                <td class="editFormNames">Old Value</td>
                                                                <td class="editFormNames">New Value</td>
                                                                <td class="editFormNames">Confirm</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblFieldName1" Text='<%# DataBinder.Eval(Container.DataItem, "FieldName") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblOldValue1" Text='<%# DataBinder.Eval(Container.DataItem, "OldValue") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblNewValue1" Text='<%# DataBinder.Eval(Container.DataItem, "NewValue") %>'></asp:Label></td>
                                                            <td class="centerMe" style="padding-top: 4px;">
                                                                <asp:CheckBox runat="server" ID="cbConfirmed1" Checked="true" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td class="centerMe">
                                                                <asp:CheckBox runat="server" ID="cbSelectAllPrincipalChanges" Checked="true" OnCheckedChanged="cbSelectAllPrincipalChanges_CheckedChanged" AutoPostBack="true" /><br />
                                                                <span class="parenNote">(Select All)</span></td>
                                                        </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Contact Changes" Value="Contact">
                                        <ContentTemplate>
                                            <div style="padding: 5px;">
                                                <asp:Repeater ID="rptrContactChanges" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="tableEditAppForm">
                                                            <tr>
                                                                <td class="editFormNames">Field Name</td>
                                                                <td class="editFormNames">Old Value</td>
                                                                <td class="editFormNames">New Value</td>
                                                                <td class="editFormNames">Confirm</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblFieldName2" Text='<%# DataBinder.Eval(Container.DataItem, "FieldName") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblOldValue2" Text='<%# DataBinder.Eval(Container.DataItem, "OldValue") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblNewValue2" Text='<%# DataBinder.Eval(Container.DataItem, "NewValue") %>'></asp:Label></td>
                                                            <td class="centerMe" style="padding-top: 4px;">
                                                                <asp:CheckBox runat="server" ID="cbConfirmed2" Checked="true" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td class="centerMe">
                                                                <asp:CheckBox runat="server" ID="cbSelectAllContactChanges" Checked="true" OnCheckedChanged="cbSelectAllContactChanges_CheckedChanged" AutoPostBack="true" /><br />
                                                                <span class="parenNote">(Select All)</span></td>
                                                        </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Banking Changes" Value="Banking">
                                        <ContentTemplate>
                                            <div style="padding: 5px;">
                                                <asp:Repeater ID="rptrBankingChanges" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="tableEditAppForm">
                                                            <tr>
                                                                <td class="editFormNames">Field Name</td>
                                                                <td class="editFormNames">Old Value</td>
                                                                <td class="editFormNames">New Value</td>
                                                                <td class="editFormNames">Confirm</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblFieldName3" Text='<%# DataBinder.Eval(Container.DataItem, "FieldName") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblOldValue3" Text='<%# DataBinder.Eval(Container.DataItem, "OldValue") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblNewValue3" Text='<%# DataBinder.Eval(Container.DataItem, "NewValue") %>'></asp:Label></td>
                                                            <td class="centerMe" style="padding-top: 4px;">
                                                                <asp:CheckBox runat="server" ID="cbConfirmed3" Checked="true" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td class="centerMe">
                                                                <asp:CheckBox runat="server" ID="cbSelectAllBankingChanges" Checked="true" OnCheckedChanged="cbSelectAllBankingChanges_CheckedChanged" AutoPostBack="true" /><br />
                                                                <span class="parenNote">(Select All)</span></td>
                                                        </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Merchant Account Changes" Value="MerchantAccount">
                                        <ContentTemplate>
                                            <div style="padding: 5px;">
                                                <asp:Repeater ID="rptrMerchantAccountChanges" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="tableEditAppForm">
                                                            <tr>
                                                                <td class="editFormNames">Field Name</td>
                                                                <td class="editFormNames">Old Value</td>
                                                                <td class="editFormNames">New Value</td>
                                                                <td class="editFormNames">Confirm</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblFieldName4" Text='<%# DataBinder.Eval(Container.DataItem, "FieldName") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblOldValue4" Text='<%# DataBinder.Eval(Container.DataItem, "OldValue") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblNewValue4" Text='<%# DataBinder.Eval(Container.DataItem, "NewValue") %>'></asp:Label></td>
                                                            <td class="centerMe" style="padding-top: 4px;">
                                                                <asp:CheckBox runat="server" ID="cbConfirmed4" Checked="true" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td class="centerMe">
                                                                <asp:CheckBox runat="server" ID="cbSelectAllMerchantAccountChanges" Checked="true" OnCheckedChanged="cbSelectAllMerchantAccountChanges_CheckedChanged" AutoPostBack="true" /><br />
                                                                <span class="parenNote">(Select All)</span></td>
                                                        </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Advance Plan Changes" Value="AdvancePlan">
                                        <ContentTemplate>
                                            <div style="padding: 5px;">
                                                <asp:Repeater ID="rptrAdvancePlanChanges" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="tableEditAppForm">
                                                            <tr>
                                                                <td class="editFormNames">Field Name</td>
                                                                <td class="editFormNames">Old Value</td>
                                                                <td class="editFormNames">New Value</td>
                                                                <td class="editFormNames">Confirm</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblFieldName5" Text='<%# DataBinder.Eval(Container.DataItem, "FieldName") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblOldValue5" Text='<%# DataBinder.Eval(Container.DataItem, "OldValue") %>'></asp:Label></td>
                                                            <td class="centerMe">
                                                                <asp:Label runat="server" ID="lblNewValue5" Text='<%# DataBinder.Eval(Container.DataItem, "NewValue") %>'></asp:Label></td>
                                                            <td class="centerMe" style="padding-top: 4px;">
                                                                <asp:CheckBox runat="server" ID="cbConfirmed5" Checked="true" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td class="centerMe">
                                                                <asp:CheckBox runat="server" ID="cbSelectAllAdvancePlanChanges" Checked="true" OnCheckedChanged="cbSelectAllAdvancePlanChanges_CheckedChanged" AutoPostBack="true" /><br />
                                                                <span class="parenNote">(Select All)</span></td>
                                                        </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>

                                    </telerik:RadPanelItem>

                                </Items>
                            </telerik:RadPanelBar>
                        </div>
                    </td>
                </tr>
            </table>

        </asp:Panel>

        <asp:Panel runat="server" ID="pnlBasicButtons" Visible="false">
            <div style="position: absolute; bottom: 45px; left: 0; width: 100%; height: 20px; text-align: center; padding-bottom: 20px;">
                <telerik:RadButton ID="btnConfirmChanges" runat="server" Text="Confirm Changes" CssClass="blue-flat-button" 
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnConfirmChanges_Click">
                </telerik:RadButton>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAdvancedButtons" Visible="false">
            <div style="position: absolute; bottom: 45px; left: 0; width: 100%; height: 20px; text-align: center; padding-bottom: 20px;">
                <telerik:RadButton ID="btnApplyChanges" runat="server" Text="Confirm Changes" CssClass="blue-flat-button" 
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnApplyChanges_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="btnCancelConfirm" runat="server" Text="Back" CssClass="blue-flat-button" 
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnCancelConfirm_Click">
                </telerik:RadButton>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlUnderwritingButtons" Visible="false">
            <div style="position: absolute; bottom: 45px; left: 0; width: 100%; height: 20px; text-align: center; padding-bottom: 20px;">
                <telerik:RadButton ID="btnAddNewUnderwriting" runat="server" Text="Begin New Underwriting" CssClass="blue-flat-button" 
                    Width="200px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnAddNewUnderwriting_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="btnUnderwritingContinue" runat="server" Text="Continue" CssClass="blue-flat-button"  OnClick="btnUnderwritingContinue_Click"
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" Visible="false" CausesValidation="false">
                </telerik:RadButton>
                <telerik:RadButton ID="btnUnderwritingTryAgain" runat="server" Text="Try Again" CssClass="blue-flat-button"  OnClick="btnUnderwritingTryAgain_Click"
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" Visible="false" CausesValidation="false">
                </telerik:RadButton>
                <telerik:RadButton ID="btnUnderwritingCancel2" runat="server" Text="Cancel" CssClass="blue-flat-button"  CommandName="Cancel" OnClick="btnUnderwitingCancel_Click"
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClientClicked="OnCancelUnderwritingClientClicked" Visible="false" CausesValidation="false">
                </telerik:RadButton>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlUnderwritingAdvancedButtons" Visible="false">
            <div style="position: absolute; bottom: 45px; left: 0; width: 100%; height: 20px; text-align: center; padding-bottom: 20px;">
                <telerik:RadButton ID="btnUnderwritingComplete" runat="server" Text="Complete" CssClass="blue-flat-button" 
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnUnderwritingComplete_Click" Visible="true" ValidationGroup="UnderwritingUpdate">
                </telerik:RadButton>
                <telerik:RadButton ID="btnUnderwritingSave" runat="server" Text="Save" CssClass="blue-flat-button" 
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnUnderwritingSave_Click" Visible="true" ValidationGroup="UnderwritingUpdate"
                    CausesValidation="false">
                </telerik:RadButton>
                <telerik:RadButton ID="btnUnderwritingCancel" runat="server" Text="Cancel" CssClass="blue-flat-button"  CommandName="Cancel" OnClick="btnUnderwitingCancel_Click"
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClientClicked="OnCancelUnderwritingClientClicked" Visible="true" CausesValidation="false">
                </telerik:RadButton>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlUserButtons" Visible="false">
            <div style="position: absolute; bottom: 45px; left: 0; width: 100%; height: 20px; text-align: center; padding-bottom: 20px;">
                <telerik:RadButton ID="btnAddUserToMerchant" runat="server" Text="Add User To Merchant" CssClass="blue-flat-button" 
                    Width="200px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnAddUserToMerchant_Click" Visible="true"
                    CausesValidation="false">
                </telerik:RadButton>
                <telerik:RadButton ID="btnConfirmUsers" runat="server" Text="Confirm" CssClass="blue-flat-button" 
                    Width="200px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnConfirmUsers_Click" Visible="false"
                    CausesValidation="false">
                </telerik:RadButton>
                <telerik:RadButton ID="btnCancelUserChanges" runat="server" Text="Cancel" CssClass="blue-flat-button" 
                    Width="200px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnCancelUserChanges_Click" Visible="false"
                    CausesValidation="false">
                </telerik:RadButton>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlMessagingButtons" Visible="false">
            <div style="position: absolute; bottom: 45px; left: 0; width: 100%; height: 20px; text-align: center; padding-bottom: 20px;">
                <telerik:RadButton ID="btnEmailBack" runat="server" Text="Back" OnClick="btnEmailBack_Click"
                    ToolTip="Back to Email Messages" Enabled="true" CssClass="blue-flat-button" 
                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White">
                </telerik:RadButton>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<div style="position: absolute; bottom: 10px; left: 0; width: 100%; height: 20px; text-align: center; padding-bottom: 15px;">
    <telerik:RadButton ID="btnCancel" runat="server" Text="Close" CssClass="blue-flat-button"  CommandName="Cancel"
        Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClientClicked="OnExitClientClicked" CausesValidation="false">
    </telerik:RadButton>
</div>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <div id="divLoading" class="ajaxLoader">
            <span runat="server" class="ajaxText">Loading</span>
            <br />
            <br />
            <img src="Images/ajax-loader.gif" alt="Loading" height="256" width="256" style="opacity: .7" /><br />
            <br />
            <span runat="server" class="ajaxTextSmall">Please wait...</span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>