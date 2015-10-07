<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Merchants.aspx.cs" Inherits="Tingle_WebForms.Merchants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                $("#menuItemDashboard").removeClass("admin-menu-active");
                $("#menuItemMerchants").addClass("admin-menu-active");
                $("#menuItemPartners").removeClass("admin-menu-active");
                $("#menuItemReports").removeClass("admin-menu-active");
                $("#menuItemProgramAdministration").removeClass("admin-menu-active");
            });

    </script>
    </telerik:RadCodeBlock>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function onPopUpShowing(sender, args) {
                popUp = args.get_popUp();
                var gridWidth = sender.get_element().offsetWidth;
                var gridHeight = sender.get_element().offsetHeight;
                var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
                var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
                popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                popUp.style.top = "105px";
            }

            function onUWPopUpShowing(sender, args) {
                popUp = args.get_popUp();
                var gridWidth = "1000px";
                var popUpWidth = "600px";
                popUp.style.left = "200px";
                popUp.style.top = "105px";
            }

            function onAdvancesPopUpShowing(sender, args) {
                popUp = args.get_popUp();
                var popUpWidth = "1000px";
                var popUpHeight = "600px";
                popUp.style.left = "0px";
                popUp.style.top = "0px";
            }
            

            function onPaymentPopUpShowing(sender, args) {
                popUp = args.get_popUp();
                var gridWidth = "600px";
                var popUpWidth = "400px";
                popUp.style.left = "200px";
                popUp.style.top = "105px";
            }

            function onUserPopUpShowing(sender, args) {
                popUp = args.get_popUp();
                var gridWidth = "400px";
                var popUpWidth = "300px";
                popUp.style.left = "0px";
                popUp.style.top = "0px";
            }


            function OnExitClientClicked(button, args) {
                if (window.confirm("Are you sure you want to exit?  Any unsaved changes will be lost.")) {
                    button.set_autoPostBack(true);
                }
                else {
                    button.set_autoPostBack(false);
                }
            }

            function OnCancelUnderwritingClientClicked(button, args) {
                if (window.confirm("Are you sure you want to cancel?  This Undewriting session will be canceled and the previous underwriting results will be used.")) {
                    button.set_autoPostBack(true);
                }
                else {
                    button.set_autoPostBack(false);
                }
            }

            function OnManualUpdateClientClicked(button, args) {
                if (window.confirm("Are you sure you want to manually change this status?")) {
                    button.set_autoPostBack(true);
                }
                else {
                    button.set_autoPostBack(false);
                }
            }

            function OnManualAdvanceUpdateClientClicked(button, args) {
                if (window.confirm("Are you sure you want to manually change these statuses?")) {
                    button.set_autoPostBack(true);
                }
                else {
                    button.set_autoPostBack(false);
                }
            }

        </script>
    </telerik:RadCodeBlock>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="gridMerchantList" />
        </Triggers>
        <ContentTemplate>
            <div>
                <asp:Literal ID="ltlAdminWelcome" runat="server" Text=""></asp:Literal><br />
            </div>
            <asp:Panel runat="server" ID="pnlMerchantList">
                <div style="width:90%; margin-left:auto; margin-right:auto">
                    <telerik:RadGrid runat="server" ID="gridMerchantList" AllowSorting="True" AllowPaging="True"
                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Height="500px"
                        OnUpdateCommand="gridMerchantList_UpdateCommand" OnNeedDataSource="gridMerchantList_NeedDataSource" UpdateMethod="UpdateMerchant"
                        OnItemDataBound="gridMerchantList_ItemDataBound" OnItemCommand="gridMerchantList_ItemCommand" OnExportCellFormatting="gridMerchantList_ExportCellFormatting"
                        OnItemCreated="gridMerchantList_ItemCreated" AllowAutomaticUpdates="false" AllowAutomaticInserts="false" OnCancelCommand="gridMerchantList_CancelCommand"
                        InsertMethod="InsertMerchant" OnPreRender="gridMerchantList_PreRender">
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowColumnHide="true" AllowExpandCollapse="true"
                            Resizing-AllowColumnResize="true" AllowGroupExpandCollapse="true" AllowRowsDragDrop="true" Resizing-AllowResizeToFit="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
                            Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
                            HideStructureColumns="true" FileName="MerchantList">
                        </ExportSettings>
                        <MasterTableView SelectMethod="GetMerchants" AllowFilteringByColumn="true" AllowPaging="true" PageSize="25" EditMode="PopUp" CommandItemDisplay="Top"
                            DataKeyNames="RecordId" InsertItemDisplay="Top" InsertMethod="InsertMerchant">
                            <EditFormSettings UserControlName="MerchantDetail.ascx" EditFormType="WebUserControl"
                                PopUpSettings-Height="750px" PopUpSettings-Width="1000px" FormStyle-BackColor="#FBFDFF"
                                InsertCaption="Enroll New Merchant">
                                <PopUpSettings Modal="true" />
                            </EditFormSettings>
                            <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" ShowAddNewRecordButton="false" />
                            <Columns>
                                <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
                                    UniqueName="RecordId" HeaderText="Account Number">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="CorpName" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="CorpName"
                                    UniqueName="CorpName" HeaderText="Corporate Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCorpName" runat="server" Text='<%# Eval("CorpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="MerchantPrincipal.Contact.FirstName" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                    UniqueName="MerchantPrincipal.Contact.FirstName" HeaderText="First Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactFirstName" runat="server" Text='<%# Eval("MerchantPrincipal.Contact.FirstName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="MerchantPrincipal.Contact.LastName" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                    UniqueName="MerchantPrincipal.Contact.LastName" HeaderText="Last Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactLastName" runat="server" Text='<%# Eval("MerchantPrincipal.Contact.LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Business.HomePhone" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                    UniqueName="Business.HomePhone" HeaderText="Business Phone">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBusinessPhone" runat="server" Text='<%# Eval("Business.HomePhone") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="UnderwritingStatus.StatusDescription" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                    UniqueName="UnderwritingStatusDescription" HeaderText="Underwriting Status">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUWStatus" runat="server" Text='<%# Eval("UnderwritingStatus.StatusDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="MerchantStatus.StatusDescription" DataType="System.String" Exportable="true" Groupable="False" ReadOnly="True"
                                    UniqueName="StatusDescription" HeaderText="Account Status">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" CssClass="gridMerchantList_StatusCol" Text='<%# Eval("MerchantStatus.StatusDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Exportable="false"
                                    ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" EditText="Details">
                                </telerik:GridEditCommandColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <ClientEvents OnPopUpShowing="onPopUpShowing" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
                <div style="width: 100%; text-align: center; position: absolute; bottom: 50px;">
                    <telerik:RadButton ID="btnAddNewMerchant" runat="server" Text="Add New Merchant" CssClass="blue-flat-button"
                        Width="160px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnAddNewMerchant_Click">
                    </telerik:RadButton>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlNewMerchant" Visible="false">
                <div style="float: left; width: 17%; height: 100%; min-height: 500px;">
                    <telerik:RadTabStrip ID="tabNewMerchantMenu" runat="server" Width="100%" SelectedIndex="0" MultiPageID="multiPage1" Skin="MetroTouch"
                        Font-Size="18px" AutoPostBack="true" Align="Left" CausesValidation="false" Orientation="VerticalLeft">
                        <Tabs>
                            <telerik:RadTab Text="Merchant User" PageViewID="UserView" Selected="true"></telerik:RadTab>
                            <telerik:RadTab Text="Business" PageViewID="BusinessView"></telerik:RadTab>
                            <telerik:RadTab Text="Principal" PageViewID="PrincipalView"></telerik:RadTab>
                            <telerik:RadTab Text="Contact" PageViewID="ContactView"></telerik:RadTab>
                            <telerik:RadTab Text="Merchant Account" PageViewID="MerchantAccountView"></telerik:RadTab>
                            <telerik:RadTab Text="Complete" PageViewID="CompleteView"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                </div>

                <telerik:RadMultiPage ID="multiPage1" runat="server" SelectedIndex="0" RenderSelectedPageOnly="true">
                    <telerik:RadPageView runat="server" ID="UserView">
                        <div style="float: right; width: 80%; height: 100%;">
                            <asp:Panel runat="server" ID="pnlExistingUser" Visible="true">
                                <div style="text-align: center; width: 100%">
                                    <table class="tableAppForm">
                                        <tr>
                                            <td colspan="2" class="editFormNames">Merchant User</td>
                                        </tr>
                                        <tr>
                                            <td class="centerFormValues" style="height:100%; width:50%">
                                                <p class="parenNote">
                                                    Select an existing user to attach to this new merchant.
                                                </p>
                                                <telerik:RadDropDownList runat="server" ID="ddlExistingUser"
                                                        SelectMethod="BindExistingUsers"
                                                        DataTextField="UserName"
                                                        DataValueField="Id"
                                                        DropDownHeight="350px"
                                                        Width="150px"
                                                        DropDownWidth="350px"
                                                        Skin="Windows7"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlExistingUser_SelectedIndexChanged"
                                                        AppendDataBoundItems="true"
                                                        DefaultMessage="Please select a User">
                                                    <ItemTemplate>
                                                        <table style="width:90%; margin-left:auto; margin-right:auto;">
                                                            <tr>
                                                                <td style="width:100%; text-align:center;">
                                                                    <asp:Literal runat="server" ID="ltlUsername" Text='<%# Eval("UserName") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:100%; text-align:center;">
                                                                    <asp:Literal runat="server" ID="ltlName" Text='<%# Eval("FirstName") + " " + Eval("LastName") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:100%; text-align:center;">
                                                                    <asp:Literal runat="server" ID="ltlEmail" Text='<%# Eval("Email") %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:100%" colspan="2"><hr /></td>
                                                            </tr>
                                                        </table>
                                                        
                                                    </ItemTemplate>
                                                </telerik:RadDropDownList>
                                                <asp:Label ID="lblExistingUserRequired" runat="server" Text="Required" ForeColor="Red" Visible="false"></asp:Label>
                                            </td>
                                            <td class="centerFormValues" style="height:100%; width:50%">
                                                <p class="parenNote">
                                                    Create a new user for this merchant.
                                                </p>
                                                <telerik:RadButton ID="btnAddNewUser" runat="server" Text="Add New User" CssClass="blue-flat-button" 
                                                    Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnAddNewUser_Click">
                                                </telerik:RadButton>

                                            </td>
                                        </tr>
                                    </table>
                                    <br /><br />
                                    <table class="tableAppForm">
                                        <tr>
                                            <td colspan="2" class="editFormNames">Selected User Information</td>
                                        </tr>
                                        <tr>
                                            <td class="leftFormValues">
                                                UserName:<br />
                                                <telerik:RadTextBox runat="server" ID="txtSelectedUserName" Text="" Enabled="false"></telerik:RadTextBox>
                                            </td>
                                            <td class="leftFormValues">
                                                Email Address:<br />
                                                <telerik:RadTextBox runat="server" ID="txtSelectedEmail" Text="" Enabled="false"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftFormValues">
                                                First Name:<br />
                                                <telerik:RadTextBox runat="server" ID="txtSelectedFirstName" Text="" Enabled="false"></telerik:RadTextBox>
                                            </td>
                                            <td class="leftFormValues">
                                                Last Name:<br />
                                                <telerik:RadTextBox runat="server" ID="txtSelectedLastName" Text="" Enabled="false"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlRegisterUser" runat="server" Visible="false">
                                <div style="float: left; text-align: center; width: 100%">
                                    <table class="tableAppForm">
                                        <tr>
                                            <td colspan="2" class="editFormNames widthFull">New User Information</td>
                                        </tr>
                                        <tr>
                                            <td class="leftFormValues">Username<br />
                                                <telerik:RadTextBox ID="txtUsername" runat="server" Width="200" MaxLength="100"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* User Name is required." Text="* Required." Font-Size="14px" ForeColor="Red" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                                                <br /><br />
                                            </td>
                                            <td class="leftFormValues">Phone Number<br />
                                                <telerik:RadTextBox ID="txtPhone" runat="server" Width="200" MaxLength="15"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Phone Number is required." Text="* Required." Font-Size="14px" ForeColor="Red" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
                                                <br /><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftFormValues widthHalf">First Name<br />
                                                <telerik:RadTextBox ID="txtFirstName" runat="server" Width="200"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* First Name is required." Text="* Required." Font-Size="14px" ForeColor="Red" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                                            <br /><br />
                                            </td>
                                            <td class="leftFormValues widthHalf">Last Name<br />
                                                <telerik:RadTextBox ID="txtLastName" runat="server" Width="200"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Last Name is required." Text="* Required." Font-Size="14px" ForeColor="Red" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                                            <br /><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftFormValues widthHalf">Email Address<br />
                                                <telerik:RadTextBox ID="txtEmail" runat="server" Width="200"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Email Address is required." Text="* Required." Font-Size="14px" ForeColor="Red" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                                <br />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage=" * Invalid Email Address." Text="* Invalid Email Address" ForeColor="Red" Font-Size="14px" ValidationExpression=".+\@.+\..+" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="leftFormValues widthHalf">Confirm Email Address<br />
                                                <telerik:RadTextBox ID="txtEmailConfirm" runat="server" Width="200"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Confirm Email Address is required." Text="* Required." Font-Size="14px" ForeColor="Red" ControlToValidate="txtEmailConfirm"></asp:RequiredFieldValidator>
                                                <br />
                                                <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                    ErrorMessage=" * Email Addresses must match."
                                                    Text="* Email Addresses must match."
                                                    ControlToValidate="txtEmailConfirm"
                                                    ControlToCompare="txtEmail"
                                                    ForeColor="Red"
                                                    Font-Size="14px">
                                                </asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Label runat="server" ID="lblNewUserMessage" Text="" ForeColor="Red"></asp:Label><br /><br />
                                    <telerik:RadButton ID="btnAddUser" runat="server" Text="Add User" CssClass="blue-flat-button" 
                                        Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnAddUser_Click">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btnCancelAddUser" runat="server" Text="Cancel" CssClass="blue-flat-button" 
                                        Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" CausesValidation="false" OnClick="btnCancelAddUser_Click">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btnContinueAddUser" runat="server" Text="Continue" CssClass="blue-flat-button" 
                                        Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" CausesValidation="false" OnClick="btnContinueAddUser_Click" 
                                        Visible="false">
                                    </telerik:RadButton>
                                </div>
                            </asp:Panel>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="BusinessView">
                        <div style="float: left; width: 80%; height: 100%;">
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
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
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
                                                        DefaultMessage="Please Select">
                                                    </telerik:RadDropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="formValues" style="width: 35%;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="rblMerchantType" runat="server" RepeatDirection="Horizontal" Width="100%">
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
                                            DefaultMessage="Please Select">
                                        </telerik:RadDropDownList>
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
                                        <telerik:RadTextBox ID="txtCorpName" runat="server" Width="250" MaxLength="100" Text=""></telerik:RadTextBox>
                                        <asp:Label ID="lblCorpNameRequired" runat="server" Text="Required" Visible="false" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">Merchant's D/B/A or Trade Name <span class="parenNote">(if different than Legal Name)</span><br />
                                        <telerik:RadTextBox ID="txtDBAName" runat="server" Width="250" MaxLength="100" Text=""></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">Phyiscal Address (no P.O. Box)<br />
                                        <telerik:RadTextBox ID="txtCorpAddress" runat="server" Width="350" MaxLength="100" Text=""></telerik:RadTextBox>
                                    </td>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">City<br />
                                        <telerik:RadTextBox ID="txtCorpCity" runat="server" Width="150" MaxLength="25" Text=""></telerik:RadTextBox>
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
                                            DefaultMessage="Please Select">
                                        </telerik:RadDropDownList>
                                    </td>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">Zip<br />
                                        <telerik:RadTextBox ID="txtCorpZip" runat="server" Width="80" MaxLength="10" Text='<%# Bind("Business.Address.Zip")%>'></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">Business License #<br />
                                        <telerik:RadTextBox ID="txtBusLicNumber" runat="server" Width="100" MaxLength="25" Text='<%# Bind("BusLicNumber")%>'></telerik:RadTextBox>
                                    </td>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">License Type<br />
                                        <telerik:RadTextBox ID="txtBusLicType" runat="server" Width="100" MaxLength="50" Text='<%# Bind("BusLicType")%>'></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">License Issuer<br />
                                        <telerik:RadTextBox ID="txtBusLicIssuer" runat="server" Width="150" MaxLength="25" Text='<%# Bind("BusLicIssuer")%>'></telerik:RadTextBox>
                                    </td>
                                    <td colspan="1" class="leftFormValues" style="width: 25%">License Date<br />
                                        <telerik:RadDatePicker ID="radBusLicDate" runat="server" Width="125" DateInput-ReadOnly="true" DbSelectedDate='<%# Bind("BusLicDate")%>'></telerik:RadDatePicker>
                                    </td>
                                    <td colspan="1" class="leftFormValues" style="width: 25%">Federal Tax ID #<br />
                                        <telerik:RadTextBox ID="txtFedTaxId" runat="server" Width="150" MaxLength="11" Text='<%# Bind("FedTaxId")%>'></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftFormValues" style="width: 50%">Business Email Address<br />
                                        <telerik:RadTextBox ID="txtBusEmail" runat="server" Width="250" MaxLength="100" Text='<%# Bind("Business.Email")%>'></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Business Email, " Display="None" ValidationExpression=".+\@.+\..+" ControlToValidate="txtBusEmail"></asp:RegularExpressionValidator>
                                    </td>
                                    <td colspan="1" class="leftFormValues" style="width: 25%">Business Phone<br />
                                        <telerik:RadTextBox ID="txtBusPhone" runat="server" Width="125" MaxLength="15" Text='<%# Bind("Business.HomePhone")%>'></telerik:RadTextBox>
                                    </td>
                                    <td colspan="1" class="leftFormValues" style="width: 25%">Business Fax<br />
                                        <telerik:RadTextBox ID="txtBusFax" runat="server" Width="125" MaxLength="15" Text='<%# Bind("Business.Fax")%>'></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftFormValues" colspan="4" style="width: 100%">Merchandise / Service Sold by Merchant<br />
                                        <telerik:RadTextBox ID="txtMerchandiseSold" runat="server" TextMode="MultiLine" Width="600" MaxLength="1000" Text='<%# Bind("MerchandiseSold")%>'></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftFormValues" style="width: 50%;">Years In Business:<br />
                                        Years:
                                        <telerik:RadNumericTextBox runat="server" Width="50" ID="txtYearsInBus"
                                            Type="Number" MaxValue="500" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                                        </telerik:RadNumericTextBox>
                                        Months:
                                        <telerik:RadNumericTextBox runat="server" Width="50" ID="txtMonthsInBus"
                                            Type="Number" MaxValue="11" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2" class="leftFormValues" style="width: 50%;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="rblSeasonal" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rblSeasonal_SelectedIndexChanged">
                                                        <asp:ListItem Value="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="False">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlSeasonalMonths" Visible="false" runat="server">
                                                        <span class="parenNote">(Please select high volume months)</span>
                                                        <asp:CheckBoxList ID="cblSeasonal" runat="server" RepeatDirection="Horizontal" RepeatColumns="6">
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
                            <br /><br />
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="PrincipalView">
                        <div style="float: left; width: 80%; height: 100%;">
                            <table class="tableEditAppForm">
                                <tr>
                                    <td colspan="100" class="editFormNames">Prinicpal Information
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="30" class="leftFormValues" style="width: 21%">First Name<br />
                                        <telerik:RadTextBox ID="txtPrincipalFirstName" runat="server" Width="125" MaxLength="30"></telerik:RadTextBox>
                                    </td>
                                    <td colspan="12" class="leftFormValues" style="width: 8%">M.I.<br />
                                        <telerik:RadTextBox ID="txtPrincipalMI" runat="server" Width="30" MaxLength="1"></telerik:RadTextBox>
                                    </td>
                                    <td colspan="30" class="leftFormValues" style="width: 21%">Last Name<br />
                                        <telerik:RadTextBox ID="txtPrincipalLastName" runat="server" Width="125" MaxLength="30"></telerik:RadTextBox>
                                    </td>
                                    <td colspan="28" class="leftFormValues" style="width: 25%">Date of Birth<br />
                                        <telerik:RadDatePicker ID="radPrincipalDoB" runat="server" Width="125" DateInput-ReadOnly="true" MinDate="1900-01-01" Calendar-FastNavigationStep="12"></telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="15" class="leftFormValues" style="width: 15%">% Ownership<br />
                                        <telerik:RadNumericTextBox ID="txtPrincipalPctOwn" runat="server" Width="65" MinValue="0" MaxValue="100"
                                            Type="Percent" NumberFormat-DecimalDigits="2">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="35" class="leftFormValues" style="width: 35%">Title<br />
                                        <telerik:RadTextBox ID="txtPrincipalTitle" runat="server" Width="200" MaxLength="50"></telerik:RadTextBox>
                                    </td>
                                    <td colspan="25" class="leftFormValues" style="width: 25%">Driver's License #<br />
                                        <telerik:RadTextBox ID="txtPrincipalDLNumber" runat="server" Width="150" MaxLength="20">
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
                                            DefaultMessage="Please Select">
                                        </telerik:RadDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="50" class="leftFormValues" style="width: 50%">Home Address<br />
                                        <telerik:RadTextBox ID="txtPrincipalAddress" runat="server" Width="300" MaxLength="100"></telerik:RadTextBox>
                                    </td>
                                    <td colspan="20" class="leftFormValues" style="width: 20%">City<br />
                                        <telerik:RadTextBox ID="txtPrincipalCity" runat="server" Width="150" MaxLength="50"></telerik:RadTextBox>
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
                                            DefaultMessage="Please Select">
                                        </telerik:RadDropDownList>
                                    </td>
                                    <td colspan="15" class="leftFormValues" style="width: 15%">Zip<br />
                                        <telerik:RadMaskedTextBox ID="txtPrincipalZip" runat="server" Width="75" Mask="#####-####" RequireCompleteText="false"></telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="50" class="leftFormValues" style="width: 50%">Home Phone<br />
                                        <telerik:RadMaskedTextBox ID="txtPrincipalHomePhone" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true"></telerik:RadMaskedTextBox>
                                    </td>
                                    <td colspan="50" class="leftFormValues" style="width: 50%">Cell Phone<br />
                                        <telerik:RadMaskedTextBox ID="txtPrincipalCellPhone" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true"></telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="ContactView">
                        <div style="float: left; width: 80%; height: 100%;">
                            <table class="tableEditAppForm">
                                <tr>
                                    <td colspan="5" class="editFormNames">Merchant Contact Information
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftFormValues" style="width: 15%">Contact Name<br />
                                        <telerik:RadTextBox ID="txtContactFirstName" runat="server" Width="100" MaxLength="50"></telerik:RadTextBox>
                                    </td>
                                    <td class="leftFormValues" style="width: 15%">Contact Name<br />
                                        <telerik:RadTextBox ID="txtContactLastName" runat="server" Width="100" MaxLength="50"></telerik:RadTextBox>
                                    </td>
                                    <td class="leftFormValues" style="width: 20%">Contact Email<br />
                                        <telerik:RadTextBox ID="txtContactEmail" runat="server" Width="150" MaxLength="100"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Contact Email," Display="None" ValidationExpression=".+\@.+\..+" ControlToValidate="txtContactEmail"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="leftFormValues" style="width: 20%">Contact Phone<br />
                                        <telerik:RadMaskedTextBox ID="txtContactPhone" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true"></telerik:RadMaskedTextBox>
                                    </td>
                                    <td class="leftFormValues" style="width: 20%">Contact Fax<br />
                                        <telerik:RadMaskedTextBox ID="txtContactFax" runat="server" Width="100" Mask="###-###-####" RequireCompleteText="true"></telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="MerchantAccountView">
                        <div style="float: left; width: 80%; height: 100%;">
                            <table class="tableEditAppForm">
                                <tr>
                                    <td colspan="4" class="editFormNames" style="width: 100%">Merchant Account Information
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftFormValues" style="width: 25%">Swiped %<br />
                                        <telerik:RadNumericTextBox runat="server" Width="150" MaxLength="3" ID="txtSwipedPct"
                                            Type="Percent" MaxValue="100" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td class="leftFormValues" style="width: 25%">Average Monthly Sales Volume<br />
                                        <telerik:RadNumericTextBox runat="server" Width="150" ID="txtAvgMonthlySales"
                                            Type="Currency" MaxValue="999999999" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td class="leftFormValues" style="width: 25%">Highest Monthly Sales Volume<br />
                                        <telerik:RadNumericTextBox runat="server" Width="150" ID="txtHighestMonthlySales"
                                            Type="Currency" MaxValue="999999999" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td class="leftFormValues" style="width: 25%">Average Weekly Sales Volume<br />
                                        <telerik:RadNumericTextBox runat="server" Width="150" ID="txtAvgWeeklySales"
                                            Type="Currency" MaxValue="999999999" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftFormValues" style="width: 50%;">Name of Current Payment Card Processor<br />
                                        <telerik:RadTextBox ID="txtCardProcessor" runat="server" Width="350" MaxLength="30"></telerik:RadTextBox>
                                    </td>
                                    <td colspan="2" class="leftFormValues" style="width: 50%;">Merchant Account ID<br />
                                        <telerik:RadMaskedTextBox ID="txtMerchantId" runat="server" Skin="Windows7" Width="200" Mask="##################" PromptChar="" HideOnBlur="true" RequireCompleteText="false"></telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="centerFormValues" style="width: 100%">Has the merchant or any principal of the merchant ever been included on the Member Alert Control High-Risk Merchants list?<br />
                                        <span class="parenNote">(Formerly known as the Combined Terminated Merchated File)</span><br />
                                        <div style="margin-left: auto; margin-right: auto; width: 40%; text-align: center;">
                                            <asp:RadioButtonList ID="rblHighRisk" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rblHighRisk_SelectedIndexChanged" Width="100%">
                                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="False">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <asp:Panel runat="server" ID="pnlHighRisk" Visible="false">
                                            <div style="margin-left: auto; margin-right: auto; width: 40%; text-align: center;">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>Reported by whom:</td>
                                                        <td>Date:</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtHighRiskWho" runat="server" Style="border: 2px inset #333;" MaxLength="50">
                                                            </telerik:RadTextBox></td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="radHighRiskDate" runat="server" DateInput-ReadOnly="true" Style="border: 2px inset #333;">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="centerFormValues" style="width: 100%">Has the merchant or any principal of the merchant ever filed for Bankruptcy?<br />
                                        <div style="margin-left: auto; margin-right: auto; width: 40%; text-align: center;">
                                            <asp:RadioButtonList ID="rblBankruptcy" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rblBankruptcy_SelectedIndexChanged" Width="100%">
                                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="False">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" SetFocusOnError="true" runat="server" ErrorMessage="Bankruptcy?," Display="None" ForeColor="Red" Font-Size="20px" ControlToValidate="rblBankruptcy"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:Panel runat="server" ID="pnlBankruptcy" Visible="false">
                                            <span class="parenNote" style="color: red;">(Please submit sheet with details.)</span>
                                            Bankruptcy Date:
                                        <telerik:RadDatePicker ID="radBankruptcyDate" runat="server" DateInput-ReadOnly="true" Style="border: 2px inset #333;"
                                            DbSelectedDate='<%# Eval("BankruptcyDate") %>'>
                                        </telerik:RadDatePicker>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="CompleteView">
                        <div style="float: left; width: 80%; height: 100%;">
                            <table class="tableEditAppForm">
                                <tr>
                                    <td colspan="2" class="editFormNames">Complete Merchant Registration</td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="centerFormValues parenNote">To complete the merchant registration process, click Complete below.  If you wish to cancel the process,
                                        click Cancel below.  If you wish to send the selected User an email with instructions to complete enrollment, check the Send Merchant Registration Email checkbox.
                                        If not, please contact the merchant directly to explain the process. 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:50%; text-align:center;"><br /><br />
                                    <telerik:RadButton ID="btnCompleteRegistration" runat="server" Text="Complete" CssClass="blue-flat-button" 
                                        Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnCompleteRegistration_Click">
                                    </telerik:RadButton>
                                    </td>
                                    <td style="width:50%; text-align:center;"><br /><br />
                                    <telerik:RadButton ID="btnCancelRegistration" runat="server" Text="Cancel" CssClass="blue-flat-button" 
                                        Width="140px" Height="25px" Skin="" Font-Bold="true" Font-Size="16px" ForeColor="White" OnClick="btnCancelRegistration_Click">
                                    </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br /><br />
                                        <asp:Label runat="server" ID="lblSubmissionMessage" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
</asp:Content>