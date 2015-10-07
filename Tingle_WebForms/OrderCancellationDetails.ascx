<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderCancellationDetails.ascx.cs" Inherits="Tingle_WebForms.OrderCancellationDetails" %>

<asp:HiddenField runat="server" ID="hRecordId" Value='<%# Eval("RecordId") %>' />
<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

<table>
    <tr class="trheader">
        <td colspan="4" class="tdheader">Order Cancellation Request ID:
            <asp:Label ID="lblRecordId" runat="server" Text='<%#: DataBinder.Eval(Container, "DataItem.RecordId") %>'></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6" style="text-align:left">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" HeaderText="There are errors in the form:" Font-Size="14px" ForeColor="Red" /><br />
        </td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">Company:</td>
        <td class="tdleft" style="width: 35%">
            <asp:Label runat="server" ID="lblCompanyEdit" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Company") %>'></asp:Label>
            <telerik:RadDropDownList runat="server" ID="ddlCompanyEdit" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="ddlCompanyEdit_SelectedIndexChanged"
                Width="75px" OnDataBinding="ddlCompanyEdit_DataBinding">
                <Items>
                    <telerik:DropDownListItem Text="Tingle" Value="Tingle" />
                    <telerik:DropDownListItem Text="Summit" Value="Summit" />
                </Items>
            </telerik:RadDropDownList>
        </td>
        <td class="tdright" style="width: 15%"></td>
        <td class="tdleft" style="width: 35%"></td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Order # field is required."
                ControlToValidate="txtOrderNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Order #:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtOrderNumberEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderNumber") %>' CssClass="report-textbox-normal"></telerik:RadTextBox><br />

        </td>
        <td class="tdright" style="width: 15%">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Armstrong Reference field is required."
                ControlToValidate="txtArmstrongReferenceEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Armstrong Reference:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtArmstrongReferenceEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ArmstrongReference") %>' CssClass="report-textbox-normal"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Customer field is required."
                ControlToValidate="txtCustomerEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Customer:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtCustomerEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Customer") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
        <td class="tdright" style="width: 15%">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="PO field is required."
                ControlToValidate="txtPOEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>PO:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtPOEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PO") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="SKU field is required."
                ControlToValidate="txtSKUEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>SKU:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtSKUEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SKU") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
        <td class="tdright" style="width: 15%">Status of PO:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadComboBox runat="server" ID="ddlPOStatusEdit" DataTextField="StatusCode" DataValueField="RecordId" Width="175px"
                CheckBoxes="true" DropDownWidth="150px" ItemType="Tingle_WebForms.Models.PurchaseOrderStatus" Skin="Default" SelectMethod="GetPOStatuses"
                EnableCheckAllItemsCheckBox="true" EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true"
                OnClientItemChecked="ClearBox" OnClientBlur="ComboBoxBlurPO" OnClientFocus="ComboBoxFocus" Localization-CheckAllString="Select all Statuses"
                CausesValidation="false" OnDataBound="ddlPOStatusEdit_DataBound" OnClientLoad="ComboBoxBlurPO">
                <ItemTemplate>
                    <%# Eval("StatusCode") %> -- <%# Eval("Status") %>
                </ItemTemplate>
            </telerik:RadComboBox>

        </td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Line field is required."
                ControlToValidate="txtLineEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Line:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtLineEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Line") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
        <td class="tdright" style="width: 15%">Line of PO:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtLineOfPOEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LineOfPO") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">Size:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtSizeEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Size") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
        <td class="tdright" style="width: 15%"></td>
        <td class="tdleft" style="width: 35%"></td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">Date Required:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadDatePicker runat="server" ID="txtDateRequiredEdit" ShowPopupOnFocus="true" DateInput-CssClass="date-picker" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-wrapper"
                DateInput-ClientEvents-OnKeyPress="OnKeyPress" SelectedDate='<%# (DateTime)DataBinder.Eval(Container, "DataItem.DateRequired") %>' DateInput-DisplayText='<%# ((DateTime)DataBinder.Eval(Container, "DataItem.DateRequired")).ToShortDateString() %>'>
            </telerik:RadDatePicker>
        </td>
        <td class="tdright" style="width: 15%">Ship Via:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtShipViaEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ShipVia") %>' CssClass="report-textbox-normal"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="tdright" style="width: 15%">Serial:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtSerialEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Serial") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
        <td class="tdright" style="width: 15%">Truck Route:</td>
        <td class="tdleft" style="width: 35%">
            <telerik:RadTextBox ID="txtTruckRouteEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TruckRoute") %>' CssClass="report-textbox-normal"></telerik:RadTextBox></td>
    </tr>
</table>
<p style="padding-bottom: 7px;"></p>
<table style="border: none;">
    <tr class="trheader">
        <td colspan="6" class="tdheader">Assignment and Request Details</td>
    </tr>
    <tr>
        <td colspan="6">
            <p style="padding-bottom: 3px;"></p>
        </td>
    </tr>
    <tr>
        <td style="width: 15%; text-align: right"><span class="formRedText">Requested By:</span></td>
        <td style="width: 25%; text-align: left">
            <telerik:RadComboBox runat="server" ID="ddlRequestedByEdit" DataTextField="EmailAddress" DataValueField="SystemUserId" Height="250px" Width="175px" AutoPostBack="true"
                DropDownWidth="300px" ItemType="Tingle_WebForms.Models.SystemUsers" Skin="Default" SelectMethod="GetUsers"
                EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true" OnDataBound="ddlRequestedByEdit_DataBound"
                OnSelectedIndexChanged="ddlRequestedByEdit_SelectedIndexChanged" CausesValidation="false">
                <ItemTemplate>
                    <%# Eval("DisplayName") %> -- <%# Eval("EmailAddress") %>
                </ItemTemplate>
            </telerik:RadComboBox>
            <asp:CustomValidator ID="cvRequestedBy"
                runat="server"
                ControlToValidate="ddlRequestedByEdit"
                ErrorMessage="Requested By Field contains an invalid selection."
                ValidateEmptyText="true"
                Display="None"
                OnServerValidate="cvRequestedBy_ServerValidate"
                SetFocusOnError="true">
            </asp:CustomValidator>
        </td>
        <td style="width: 15%; text-align: right"><span class="formRedText">Due By:</span></td>
        <td style="width: 15%; text-align: left">
            <telerik:RadDatePicker runat="server" ID="txtDueByDateEdit" ShowPopupOnFocus="true" DateInput-CssClass="date-picker-small" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-small-wrapper"
                DateInput-ClientEvents-OnKeyPress="OnKeyPress" SelectedDate='<%# GetDueByDateSelectedDate(DataBinder.Eval(Container, "DataItem.DueDate")) %>' DateInput-DisplayText='<%# DataBinder.Eval(Container, "DataItem.DueDate") != null ? ((DateTime)DataBinder.Eval(Container, "DataItem.DueDate")).ToString("MM/dd/yyyy") : "" %>'>
            </telerik:RadDatePicker>
            <td style="width: 15%; text-align: right"><span class="formRedText">Status:</span></td>
        <td style="width: 15%; text-align: left">
            <telerik:RadDropDownList ID="ddlStatusEdit" runat="server" SelectMethod="GetStatusesEdit" OnDataBound="ddlStatusEdit_DataBound"
                AutoPostBack="false" DataTextField="StatusText" DataValueField="StatusID" Skin="Default" Width="100px">
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 15%; text-align: right"><span class="formRedText">Assigned To:</span></td>
        <td style="width: 15%; text-align: left">
            <telerik:RadComboBox runat="server" ID="ddlAssignedToEdit" DataTextField="EmailAddress" DataValueField="SystemUserId" Height="250px" Width="175px" AutoPostBack="true"
                DropDownWidth="300px" ItemType="Tingle_WebForms.Models.SystemUsers" Skin="Default" SelectMethod="GetUsers"
                EmptyMessage="Unassigned" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true" OnDataBound="ddlAssignedToEdit_DataBound"
                OnSelectedIndexChanged="ddlAssignedToEdit_SelectedIndexChanged" CausesValidation="false" DefaultMessage="Unassigned">
                <ItemTemplate>
                    <%# Eval("DisplayName") %> -- <%# Eval("EmailAddress") %>
                </ItemTemplate>
            </telerik:RadComboBox>
            <asp:CustomValidator ID="cvAssignedTo"
                runat="server"
                ControlToValidate="ddlAssignedToEdit"
                ErrorMessage="Assigned To Field contains an invalid selection."
                Display="None"
                OnServerValidate="cvAssignedTo_ServerValidate"
                SetFocusOnError="true">
            </asp:CustomValidator>
        </td>
        <td style="width: 15%; text-align: right"><span class="formRedText">Date Created:</span></td>
        <td style="width: 15%; text-align: left">
            <asp:Label runat="server" ID="lblDateCreatedEdit" Text='<%# ((DateTime)DataBinder.Eval(Container, "DataItem.Timestamp")).ToShortDateString() %>'></asp:Label>
        </td>
        <td style="width: 15%; text-align: right"><span class="formRedText">Priority:</span></td>
        <td style="width: 15%; text-align: left">
            <telerik:RadDropDownList ID="ddlPriorityEdit" Skin="Default" Width="100px" runat="server" CausesValidation="false"
                SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" AutoPostBack="false" OnDataBound="ddlPriorityEdit_DataBound">
            </telerik:RadDropDownList>
        </td>

    </tr>
</table>
<p style="padding-bottom: 7px;"></p>
<table style="border: none;">
    <tr class="trheader">
        <td colspan="6" class="tdheader">Notification Details</td>
    </tr>
    <tr>
        <td colspan="6">
            <p style="padding-bottom: 3px;"></p>
        </td>
    </tr>
    <tr>
        <td style="width: 15%; text-align: left; padding-left: 10px; vertical-align: middle">
            <asp:CheckBox runat="server" ID="cbNotifyStandard" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyStandard_CheckedChanged" />
            <asp:Label runat="server" ID="lblNotifyStandard" Text="Notify Standard" Font-Size="12px"></asp:Label>
        </td>
        <td style="width: 30%; text-align: left">
            <asp:Label runat="server" ID="lblNotifyStandardValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
        </td>
        <td style="width: 15%; text-align: left; padding-left: 10px;">
            <asp:CheckBox runat="server" ID="cbNotifyAssignee" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyAssignee_CheckedChanged" />
            <asp:Label runat="server" ID="lblNotifyAssignee" Text="Notify Assignee" Font-Size="12px"></asp:Label>
        </td>
        <td style="width: 20%; text-align: left">
            <asp:Label runat="server" ID="lblNotifyAssigneeValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
        </td>
        <td rowspan="2" style="width: 5%; text-align: right">
            <asp:CheckBox runat="server" ID="cbSendComments" />
        </td>
        <td rowspan="2" style="width: 15%; text-align: center; font-size: 12px; padding: 10px">Include All Comments in Notification
        </td>
    </tr>
    <tr>
        <td style="width: 15%; text-align: left; vertical-align: middle; padding-left: 10px;">
            <asp:CheckBox runat="server" ID="cbNotifyOther" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyOther_CheckedChanged" />
            <asp:Label runat="server" ID="lblNotifyOther" Text="Notify Other" Font-Size="12px"></asp:Label>
        </td>
        <td style="width: 30%; text-align: left; vertical-align: middle">
            <telerik:RadComboBox runat="server" ID="ddlNotifyOther" DataTextField="Address" DataValueField="Name" Height="250px" Width="175px" AutoPostBack="true"
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
        <td style="width: 15%; text-align: left; padding-left: 10px;">
            <asp:CheckBox runat="server" ID="cbNotifyRequester" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyRequester_CheckedChanged" />
            <asp:Label runat="server" ID="lblNotifyRequester" Text="Notify Requester" Font-Size="12px"></asp:Label>
        </td>
        <td style="width: 20%; text-align: left">
            <asp:Label runat="server" ID="lblNotifyRequesterValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
        </td>

    </tr>
</table>
<table>
    <tr>
        <td style="width: 100%;" colspan="4">
            <div style="width: 40%; border: 0; margin: 0 auto; display: none; text-align: center" id="divAddNewEmail">
                <table style="border: 1px solid #000 !important; border-radius: 5px; !important; padding: 5px;">
                    <tr>
                        <td>Name:</td>
                        <td>
                            <telerik:RadTextBox ID="txtNameInsert" runat="server" Text="" EmptyMessage="Enter Name Here" Width="150px"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="rfvTxtNameInsert" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="NotificationEmailInsert"
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
                            <telerik:RadButton ID="btnInsertEmail" runat="server" Text="Add Email" OnClick="btnInsertEmail_Click" ValidationGroup="NotificationEmailInsert" />
                            <telerik:RadButton runat="server" ID="btnCancelNewEmail" Text="Cancel" OnClientClicked="CancelNewEmail" AutoPostBack="false" CausesValidation="false"></telerik:RadButton>
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
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="4" class="tdcenter">
            <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Request" CssClass="normalButton" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Back" CssClass="normalButton" />
        </td>
    </tr>
    <tr>
        <td colspan="4" class="tdcenter">
            <div style="width: 14%; float: left">
                <br />
            </div>
            <div style="width: 70%; float: left">
                <p class="smallText" style="font-weight: bold;">
                    Request Notifications Sent To:<br />
                    <br />
                    <asp:Label ID="lblEmailsSentTo" Style="color: black; font-style: italic; font-size: 12px; font-weight: normal;" runat="server"></asp:Label>
                </p>
            </div>
            <div style="width: 15%; float: right">
                <span style="font-weight: bold; color: #bc4445; font-size: 12px">Created By:</span><br />
                <asp:Label runat="server" ID="lblCreatedBy" Style="font-size: 12px; color: black; font-weight: normal" Text='<%# DataBinder.Eval(Container, "DataItem.SubmittedUser.DisplayName") %>'></asp:Label><br />
                <br />
                <span style="font-weight: bold; color: #bc4445; font-size: 12px">Last Updated By:</span><br />
                <asp:Label runat="server" ID="lblLastUpdatedBy" Style="font-size: 12px; color: black; font-weight: normal" Text='<%# DataBinder.Eval(Container, "DataItem.LastModifiedUser.DisplayName") %>'></asp:Label><br />
                <br />
                <span style="font-weight: bold; color: #bc4445; font-size: 12px">Last Update:</span><br />
                <asp:Label runat="server" ID="lblLastUpdated" Style="font-size: 12px; color: black; font-weight: normal" Text='<%# ((DateTime)DataBinder.Eval(Container, "DataItem.LastModifiedTimestamp")).ToShortDateString() %>'></asp:Label>
            </div>
        </td>
    </tr>
</table>
<div style="width: 100%; padding-top: 10px">
    <telerik:RadTextBox runat="server" ID="txtNewComment" TextMode="MultiLine" ValidationGroup="NewComment" MaxLength="300" CssClass="new-comment" WrapperCssClass="new-comment-wrapper">
    </telerik:RadTextBox>
    <asp:RequiredFieldValidator ID="rfvTxtNewComment" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="NewComment"
        ControlToValidate="txtNewComment"></asp:RequiredFieldValidator>
    <br />
</div>
<div style="width: 81%; color: #bc4445; margin: 0 auto; text-align: left; padding-bottom: 15px; height: 25px">
    <div style="float: left; width: 45%">
        <span style="font-size: 10px">300 Characters Max</span>
    </div>
    <div style="float: right; text-align: right; width: 45%; padding-right: 5px; padding-top: 5px;">
        <asp:Button ID="btnAddComment" runat="server" CausesValidation="True" Text="Post Comment" CssClass="normalButton"
            OnClick="btnAddComment_Click" ValidationGroup="NewComment" />
    </div>
</div>
<div style="width: 100%; padding-top: 10px">
    <div style="width: 80%; max-width: 800px; background-color: #FFF; margin: 0 auto; text-align: left; font-size: 12px; color: #bc4445; overflow: hidden; word-wrap: break-word;">
        <asp:CheckBox runat="server" ID="cbShowSystemComments" Text="Show System Comments" AutoPostBack="true"
            OnCheckedChanged="cbShowSystemComments_CheckedChanged" />
    </div>
    <asp:Repeater runat="server" ID="rptrComments" SelectMethod="rptrComments_GetData" ItemType="Tingle_WebForms.Models.Comments">
        <ItemTemplate>
            <asp:Literal runat="server" ID="ltlComment" Text='<%# LoadCommentLiteral(Convert.ToBoolean(Eval("SystemComment")), Eval("Note").ToString(), Convert.ToString(Eval("User.DisplayName")), Eval("Timestamp").ToString()) %>'></asp:Literal>
        </ItemTemplate>
    </asp:Repeater>
</div>
