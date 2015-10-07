<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LowInventoryForm.aspx.cs" Inherits="Tingle_WebForms.LowInventoryForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function BindEvents() {
            jQuery(document).ready(function () {
                jQuery.noConflict()
                $("#<%= txtQuantity.ClientID %>").width(35)
                $("#<%= txtQuantity.ClientID %>").spinner();
                $("#<%= txtDueByDate.ClientID %>").datepicker({ dateFormat: "mm/dd/yy" });
            });
        }
    </script>
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
    <script type="text/javascript">
        function ClearBox(sender, args) {
            sender.clearSelection();
            sender.set_text("");
            sender.setAllItemsVisible(true);
            var input = sender.get_inputDomElement();
            input.focus();
        }

        function ComboBoxBlur(sender, args) {
            var box = $find('<%= ddlNotifyOther.ClientID %>');
            if (box.get_checkedItems().length > 0)
                sender.set_text(box.get_checkedItems().length.toString() + " Email(s) Selected");
        }

        function ComboBoxFocus(sender, args) {
            sender.set_text("");
        }

        function ShowAddNewEmailTd(sender, args) {
            jQuery("#divAddNewEmail").css("display", "block");
        }

        function CancelNewEmail(sender, args) {
            jQuery("#txtNameInsert").text("");
            jQuery("#txtAddressInsert").text("");
            jQuery("#divAddNewEmail").css("display", "none");
        }

    </script>
    <asp:Panel ID="pnlCompleted" runat="server" Visible="false">
        <div style="text-align: center; width: 100%">
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="22px"></asp:Label><br />
            <br />
            <asp:LinkButton ID="lbStartOver" runat="server" Text="Click Here to Submit Another Request" OnClick="lbStartOver_Click" Font-Size="22px"></asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlForm" runat="server" Visible="true">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(BindEvents);
                </script>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" HeaderText="There are errors in the form:" Font-Size="14px" ForeColor="Red" />
                <br />
                <br />
                <table>
                    <tr class="trheader">
                        <td colspan="4" class="tdheader">
                            <div style="float: left; width: 25%">
                                <telerik:RadDropDownList runat="server" ID="ddlCompany" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                    Width="75px" Skin="Default">
                                    <Items>
                                            <telerik:DropDownListItem Text="Tingle" Value="Tingle" />
                                            <telerik:DropDownListItem Text="Summit" Value="Summit" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </div>
                            <div style="float: left; width: 50%">
                                Low Inventory Request
                            </div>
                            <div style="float: left; width: 25%">&nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Order # field is required." ForeColor="Black" Font-Size="16px"
                                ControlToValidate="txtOrderNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Order #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox><br />
                        </td>
                        <td class="tdright">Plant:</td>
                        <td class="tdleft">
                            <telerik:RadDropDownList ID="ddlPlants" runat="server" SelectMethod="GetPlants" Skin="Default" Width="150px" AutoPostBack="false" 
                                CausesValidation="false" DataTextField="PlantText" DataValueField="RecordId"></telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Line field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtLine" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Line:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtLine" runat="server"></asp:TextBox></td>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Quantity field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtQuantity" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            Quantity:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="SKU field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtSKU" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>SKU:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtSKU" runat="server"></asp:TextBox></td>
                        <td class="tdright"></td>
                        <td class="tdleft"></td>
                    </tr>
                    <tr>
                        <td style="width:100%;" colspan="4">
                            <div style="width:80%; border:1px solid black; border-radius:5px; margin: 0 auto; height:90px;">
                                <table style="border:none;">
                                    <tr>
                                        <td colspan="6">
                                            <span style="font-weight:bold; color:#bc4445; text-decoration:underline">Assignment and Request Details:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%; text-align:right"><span class="formRedText">Requested By:</span></td>
                                        <td style="width:25%; text-align:left">
                                            <telerik:RadComboBox runat="server" ID="ddlRequestedBy" DataTextField="EmailAddress" DataValueField="SystemUserId" Height="250px" Width="175px" AutoPostBack="true"
                                                DropDownWidth="300px" ItemType="Tingle_WebForms.Models.SystemUsers" Skin="Default" SelectMethod="GetUsers"
                                                EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true" OnDataBound="ddlRequestedBy_DataBound"
                                                OnSelectedIndexChanged="ddlRequestedBy_SelectedIndexChanged" CausesValidation="false">
                                                <ItemTemplate>
                                                    <%# Eval("DisplayName") %> -- <%# Eval("EmailAddress") %>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                            <asp:CustomValidator  ID="cvRequestedBy" 
                                                runat="server"
                                                ControlToValidate="ddlRequestedBy"
                                                ErrorMessage="Requested By Field contains an invalid selection."
                                                ValidateEmptyText="true"
                                                Display="None"
                                                OnServerValidate="cvRequestedBy_ServerValidate"
                                                SetFocusOnError="true">
                                            </asp:CustomValidator>
                                        </td>
                                        <td style="width:18%; text-align:right"><span class="formRedText">Due By:</span></td>
                                        <td style="width:18%; text-align:left"><input type="text" id="txtDueByDate" runat="server" style="width:75px;" /></td>
                                        <td style="width:10%; text-align:right"><span class="formRedText">Status:</span></td>
                                        <td style="width:10%; text-align:left">
                                            <telerik:RadDropDownList ID="ddlStatus" runat="server" SelectMethod="GetStatuses"
                                                DataTextField="StatusText" DataValueField="StatusID" Skin="Default" Width="100px"></telerik:RadDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%; text-align:right"><span class="formRedText">Assigned To:</span></td>
                                        <td style="width:25%; text-align:left">
                                            <telerik:RadComboBox runat="server" ID="ddlAssignedTo" DataTextField="EmailAddress" DataValueField="SystemUserId" Height="250px" Width="175px" AutoPostBack="true"
                                                DropDownWidth="300px" ItemType="Tingle_WebForms.Models.SystemUsers" Skin="Default" SelectMethod="GetUsers"
                                                EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true" OnDataBound="ddlRequestedBy_DataBound"
                                                OnSelectedIndexChanged="ddlAssignedTo_SelectedIndexChanged" CausesValidation="false" DefaultMessage="Unassigned">
                                                <ItemTemplate>
                                                    <%# Eval("DisplayName") %> -- <%# Eval("EmailAddress") %>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                            <asp:CustomValidator  ID="cvAssignedTo" 
                                                runat="server"
                                                ControlToValidate="ddlAssignedTo"
                                                ErrorMessage="Assigned To Field contains an invalid selection."
                                                Display="None"
                                                OnServerValidate="cvAssignedTo_ServerValidate"
                                                SetFocusOnError="true">
                                            </asp:CustomValidator>
                                        </td>
                                        <td style="width:18%; text-align:right"><span class="formRedText">Date Created:</span></td>
                                        <td style="width:18%; text-align:left">
                                            <%= DateTime.Now.ToShortDateString() %>
                                        </td>
                                        <td style="width:10%; text-align:right"><span class="formRedText">Priority:</span></td>
                                        <td style="width:10%; text-align:left">
                                            <telerik:RadDropDownList ID="ddlPriority" Skin="Default" Width="100px" runat="server" CausesValidation="false"
                                                SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" OnDataBound="ddlPriority_DataBound">
                                            </telerik:RadDropDownList>
                                        </td>

                                    </tr>
                                </table>
                            </div>
                            <div style="width:80%; border:1px solid black; border-radius:5px; margin: 0 auto; height:90px;">
                                <table style="border:none;">
                                    <tr>
                                        <td colspan="6">
                                            <span style="font-weight:bold; color:#bc4445; text-decoration:underline">Notification Details:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:17%; text-align:left; vertical-align:middle">
                                            <asp:CheckBox runat="server" ID="cbNotifyStandard" Text="" Style="vertical-align:middle" AutoPostBack="true" OnCheckedChanged="cbNotifyStandard_CheckedChanged" />
                                            <asp:Label runat="server" ID="lblNotifyStandard" Text="Notify Standard" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td style="width:28%; text-align:left">
                                            <asp:Label runat="server" ID="lblNotifyStandardValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
                                        </td>
                                        <td style="width:17%; text-align:left">
                                            <asp:CheckBox runat="server" ID="cbNotifyAssignee" Text="" Style="vertical-align:middle" AutoPostBack="true" OnCheckedChanged="cbNotifyAssignee_CheckedChanged" />
                                            <asp:Label runat="server" ID="lblNotifyAssignee" Text="Notify Assignee" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td style="width:18%; text-align:left">
                                            <asp:Label runat="server" ID="lblNotifyAssigneeValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
                                        </td>
                                        <td rowspan="2" style="width:5%; text-align:right">
                                            <asp:CheckBox runat="server" ID="cbSendComments" />
                                        </td>
                                        <td rowspan="2" style="width:15%; text-align:center; font-size:12px">
                                            Include All Comments in Notification
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:17%; text-align:left; vertical-align:middle">
                                            <asp:CheckBox runat="server" ID="cbNotifyOther" Text="" Style="vertical-align:middle" AutoPostBack="true" OnCheckedChanged="cbNotifyOther_CheckedChanged" />
                                            <asp:Label runat="server" ID="lblNotifyOther" Text="Notify Other" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td style="width:28%; text-align:left; vertical-align:middle">
                                            <telerik:RadComboBox runat="server" ID="ddlNotifyOther" DataTextField="Address" DataValueField="Name" Height="250px" Width="150px" AutoPostBack="true"
                                                CheckBoxes="true" DropDownWidth="300px" ItemType="Tingle_WebForms.Models.NotifyOtherList" Skin="Default" SelectMethod="GetOtherEmails"
                                                EnableCheckAllItemsCheckBox="true" EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true"
                                                OnClientItemChecked="ClearBox" OnClientBlur="ComboBoxBlur" OnClientFocus="ComboBoxFocus" Localization-CheckAllString="Select all Emails"
                                                OnSelectedIndexChanged="ddlNotifyOther_SelectedIndexChanged" CausesValidation="false" OnItemChecked="ddlNotifyOther_ItemChecked" OnCheckAllCheck="ddlNotifyOther_CheckAllCheck">
                                                <ItemTemplate>
                                                    <%# Eval("Name") %> -- <%# Eval("Address") %>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                            <telerik:RadButton ID="btnAddNewEmail" runat="server" Text="Add New Notification Email" ButtonType="SkinnedButton" 
                                                CssClass="plusSignImageBtn" CausesValidation="false" AutoPostBack="false" OnClientClicked="ShowAddNewEmailTd" ToolTip="Add New Notification Email">
                                                <Image EnableImageButton="true" />
                                            </telerik:RadButton>
                                        </td>
                                        <td style="width:17%; text-align:left">
                                            <asp:CheckBox runat="server" ID="cbNotifyRequester" Text="" Style="vertical-align:middle" AutoPostBack="true" OnCheckedChanged="cbNotifyRequester_CheckedChanged" />
                                            <asp:Label runat="server" ID="lblNotifyRequester" Text="Notify Requester" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td style="width:18%; text-align:left">
                                            <asp:Label runat="server" ID="lblNotifyRequesterValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
                                        </td>

                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%;" colspan="4">
                            <div style="width:80%; border:0; margin: 0 auto; display:none; text-align:center" id="divAddNewEmail">
                                <table style="border: 1px solid #000; border-radius:5px;">
                                    <tr>
                                        <td>Name:</td>
                                        <td>
                                            <telerik:RadTextBox ID="txtNameInsert" runat="server" Text="" EmptyMessage="Enter Name Here" Width="150px"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="rfvTxtNameInsert" runat="server" ErrorMessage="*" Text="*" ForeColor="Red"  ValidationGroup="NotificationEmailInsert"
                                                ControlToValidate="txtNameInsert"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Address:</td>
                                        <td>
                                            <telerik:RadTextBox ID="txtAddressInsert" runat="server" Text="" EmptyMessage="Enter Email Address Here" Width="150px"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="rfvTxtAddressInsert" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="NotificationEmailInsert"
                                                ControlToValidate="txtAddressInsert"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Status:</td>
                                        <td>
                                            <asp:RadioButtonList ID="rblNotificationEmailStatusInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                <asp:ListItem Value="0">Disabled</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnInsertEmail" runat="server" Text="Add Email" OnClick="btnInsertEmail_Click" ValidationGroup="NotificationEmailInsert" />
                                            <telerik:RadButton runat="server" ID="btnCancelNewEmail" Text="Cancel" OnClientClicked="CancelNewEmail" CausesValidation="false"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" Text="" ID="lblInsertEmailMessage" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                
                            </div>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="4">
                            <br /><span class="formRedText">Comments</span><br />
                            <telerik:RadTextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="80%" MaxLength="300" Height="60px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:left;">
                            <div style="width:80%; text-align:left; margin: 0 auto;">
                                <span style="font-size:10px">300 Characters Max</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdcenter">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit Request" OnClick="btnSubmit_Click" CssClass="normalButton" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <br />
                            <p class="smallText" style="font-weight:bold;">Request Notifications Sent To:<br /><br />
                                <asp:Label ID="lblEmailsSentTo" style="color:black; font-style:italic; font-size:12px; font-weight:normal;" runat="server"></asp:Label></p>
                            <br />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

    </asp:Panel>
    <div id="loadingDiv">
        <div id="loadingGif">
            <img src="Images/loading.gif" />
        </div>
    </div>
</asp:Content>
