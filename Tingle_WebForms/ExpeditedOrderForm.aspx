<%@ Page Title="WCTingle - Expedited Order Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpeditedOrderForm.aspx.cs" Inherits="Tingle_WebForms.Forms.ExpeditedOrderForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function BindEvents() {
            jQuery(document).ready(function () {
                jQuery.noConflict()
                $("#<%= txtInstallDate.ClientID %>").datepicker({ dateFormat: "mm/dd/yy" });
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

        function ShowAddNewSkuQuantity(sender, args) {
            jQuery("#divAddNewSkuQuantity").css("display", "block");
        }

        function CancelNewEmail(sender, args) {
            jQuery("#txtNameInsert").text("");
            jQuery("#txtAddressInsert").text("");
            jQuery("#divAddNewEmail").css("display", "none");
        }

        function CancelNewSku(sender, args) {
            jQuery("#txtMaterialSku").text("");
            jQuery("#txtQuantityOrdered").text("");
            jQuery("#divAddNewSkuQuantity").css("display", "none");
        }

        function Validate_Sku() {
            if (jQuery("<%# gvSkus.ClientID %>").rows.length > 0) {
                return true;
            }
            else {
                return false;
            }
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
                <asp:Label runat="server" ID="lblRandomIdentifier" Text="" Visible="false"></asp:Label>
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
                                Expedited Order Request
                            </div>
                            <div style="float: left; width: 25%">&nbsp;
                            </div>
                                
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer field is required." ForeColor="Black" Font-Size="16px"
                                ControlToValidate="txtCustomer" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Customer:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox><br />
                        </td>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Expedite Code field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="ddlExpediteCode" InitialValue="0" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Expedite Code:</td>
                        <td class="tdleft">
                            <asp:DropDownList ID="ddlExpediteCode" runat="server" SelectMethod="GetExpediteCodes"
                                DataTextField="Code" DataValueField="ExpediteCodeID" AppendDataBoundItems="true"
                                Style="border-style: inset;">
                                <asp:ListItem Text="Select Code" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Contact Name field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtContactName" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Contact Name:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox></td>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Phone Number field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtPhoneNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Phone Number:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdcenter">Add New Material SKU# and Quantity
                            <telerik:RadButton ID="btnShowNewSKUDiv" runat="server" Text="Add New Material SKU And Quantity" ButtonType="SkinnedButton" 
                                CssClass="plusSignImageBtn" CausesValidation="false" AutoPostBack="false" OnClientClicked="ShowAddNewSkuQuantity" ToolTip="Add New Material SKU And Quantity">
                                <Image EnableImageButton="true" />
                            </telerik:RadButton>
                            <asp:CustomValidator ID="cvSKU" runat="server" ErrorMessage="Must add at least one SKU#" Text="*" ForeColor="Red" Display="None"
                                ValidateEmptyText="true" OnServerValidate="cvSKU_ServerValidate"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td colspan="4">
                            <div style="width:80%; border:0; margin: 0 auto; display:none; text-align:center" id="divAddNewSkuQuantity">
                                <table style="border: 1px solid #000; border-radius:5px;">
                                    <tr>
                                        <td class="tdcenter">Material SKU#:</td>
                                        <td>
                                            <telerik:RadTextBox ID="txtMaterialSku" runat="server" Text="" Width="150px"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="SkuQuantityInsert"
                                                ControlToValidate="txtMaterialSku"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdcenter">Quantity:</td>
                                        <td>
                                            <telerik:RadTextBox ID="txtQuantityOrdered" runat="server" Text="" Width="150px"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="SkuQuantityInsert"
                                                ControlToValidate="txtQuantityOrdered"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadButton ID="btnAddSkuQuantity" runat="server" Text="Add SKU" OnClick="btnAddSkuQuantity_Click" ValidationGroup="SkuQuantityInsert"></telerik:RadButton>
                                            <telerik:RadButton runat="server" ID="btnCancelSkuQuantity" Text="Cancel" OnClientClicked="CancelNewSku" CausesValidation="false"></telerik:RadButton>
                                            <br />
                                            <asp:Label runat="server" ID="lblAddSkuMessage" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvSkus" runat="server" AllowSorting="True" AutoGenerateColumns="False" Width="70%" Style="margin: 0 auto !important"
                                ItemType="Tingle_WebForms.Models.SkuQuantity" SelectMethod="GetSkuQuantities" CellPadding="4" ForeColor="#000000" Font-Bold="false" GridLines="Horizontal"
                                AllowPaging="True" DataKeyNames="RecordId" BackColor="White" BorderColor="#000000" BorderStyle="solid" BorderWidth="1px" 
                                DeleteMethod="gvSkus_DeleteItem" UpdateMethod="gvSkus_UpdateItem" OnRowUpdating="gvSkus_RowUpdating" >
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom"  />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecordId" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRecordIdEdit" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MaterialSku">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialSku" runat="server" Text='<%#: Eval("MaterialSku") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtMaterialSkuEdit" Text='<%# Eval("MaterialSku") %>' MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTxtMaterialSkuEdit" runat="server" ErrorMessage="*" Text="*" ControlToValidate="txtMaterialSkuEdit"
                                                ForeColor="Red" Font-Size="16px" ValidationGroup="AddNewSku"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#: Eval("Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtQuantityEdit" Text='<%# Eval("Quantity") %>' MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTxtQuantityEdit" runat="server" ErrorMessage="*" Text="*" ControlToValidate="txtQuantityEdit"
                                                ForeColor="Red" Font-Size="16px" ValidationGroup="AddNewSku"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Completed">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="cbCompleted" Text="" Checked='<%# (Boolean)Eval("Completed") %>' BorderStyle="None" Enabled="false" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox runat="server" ID="cbCompletedEdit" Text="" Checked='<%# (Boolean)Eval("Completed") %>' BorderStyle="None" CausesValidation="false" />
                                        </EditItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TempId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTempId" runat="server" Text='<%#: Eval("TempId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblTempIdEdit" runat="server" Text='<%#: Eval("TempId") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit" ValidationGroup="AddNewSku"></asp:CommandField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDeleteCode" runat="server" Text="Delete" CommandName="Delete"  CausesValidation="false" ForeColor="Black" Font-Bold="false"
                                                OnClientClick="return confirm('Are you sure you want to delete this Material SKU?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>Please add at least one SKU# and Quantity.</EmptyDataTemplate>
                                <EmptyDataRowStyle CssClass="standardTd" />
                                <FooterStyle BackColor="white" ForeColor="Black" />
                                <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                <RowStyle HorizontalAlign="Center" />
                            </asp:GridView><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Account Number must be a numeric value between 0 and 999999."
                                ControlToValidate="txtAccountNumber" MinimumValue="0" MaximumValue="999999" Font-Size="12px"
                                SetFocusOnError="true" Display="None"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Account Number field is required." 
                                Text="*" ForeColor="Red" ControlToValidate="txtAccountNumber"></asp:RequiredFieldValidator>Account Number:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtAccountNumber" runat="server" MaxLength="6"></asp:TextBox></td>
                        <td class="tdright">Purchase Order #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtPurchaseOrderNumber" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdright">OOW Order #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtOowOrderNumber" runat="server"></asp:TextBox></td>
                        <td class="tdright">S/M:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtSM" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdright">Install Date:</td>
                        <td class="tdleft">
                            <input type="text" id="txtInstallDate" runat="server" /></td>
                        <td class="tdright"></td>
                        <td class="tdleft"></td>
                    </tr>
                    <tr>
                        <td class="tdsectionheader" colspan="4">
                            <div style="float: left; width: 25%">&nbsp;</div>
                            <div style="float: left; width: 50%; text-align: center">Ship To (If different than default)</div>
                            <div style="float: left; width: 25%; text-align: right">
                                <asp:CheckBox runat="server" ID="cbShipToSection" AutoPostBack="true" OnCheckedChanged="cbShipToSection_CheckedChanged" Checked="false" />
                            </div>
                        </td>
                    </tr>
                    <tr runat="server" id="tr1" visible="false">
                        <td class="tdright">Name:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToName" runat="server"></asp:TextBox></td>
                        <td class="tdright">Street Address:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToAddress" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="tr2" visible="false">
                        <td class="tdright">City:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToCity" runat="server"></asp:TextBox></td>
                        <td class="tdright">State:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToState" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="tr3" visible="false">
                        <td class="tdright">Zip:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToZip" runat="server"></asp:TextBox></td>
                        <td class="tdright">Phone:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToPhone" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:100%;" colspan="4">
                            <div style="width:80%; border:1px solid black; border-radius:5px; margin: 0 auto; height:90px;">
                                <table style="border:none;">
                                    <tr>
                                        <td colspan="6" class="standardTd">
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
                                        <td colspan="6" class="standardTd">
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
                        <td colspan="4" class="standardTd">
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
