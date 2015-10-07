<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryApproval.aspx.cs" Inherits="Tingle_WebForms.InventoryApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function BindEvents() {
            jQuery(document).ready(function () {
                jQuery.noConflict()
                $("#<%= txtEstShipDate.ClientID %>").datepicker({ dateFormat: "mm/dd/yy" });
                $("#<%= txtETA.ClientID %>").datepicker({ dateFormat: "mm/dd/yy" });
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
        function OnKeyPress(sender, args) {
            args.set_cancel(true);
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
    <asp:UpdatePanel runat="server" ID="pnlInventoryApproval" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCompanyGlobal" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlVendorsGlobal" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddNewItem" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnUpdateCurrentStock" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gridPendingApproval" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="gridApproved" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="gridOrdered" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="gridArrived" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="gridInvoiced" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="cbNotifyStandard" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbNotifyOther" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlNotifyOther" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddNewEmail" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSendNotification" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnInsertEmail" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelNewEmail" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPostComment" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
            </script>
            <div style="text-align: center">
                <div style="float: left; width: 100%; padding-bottom: 20px;">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">
                                <div style="float: left; width: 35%; text-align:left">
                                    <telerik:RadDropDownList runat="server" ID="ddlCompanyGlobal" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="ddlCompanyGlobal_SelectedIndexChanged"
                                        Width="75px" Skin="Default">
                                        <Items>
                                                <telerik:DropDownListItem Text="Tingle" Value="Tingle" />
                                                <telerik:DropDownListItem Text="Summit" Value="Summit" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                                <div style="float: left; width: 30%">
                                    Inventory Management
                                </div>
                                <div style="float: left; width: 35%;  text-align:right">
                                    <telerik:RadDropDownList ID="ddlVendorsGlobal" runat="server" SelectMethod="GetVendors" Skin="Default" Width="150px" AutoPostBack="true" 
                                        CausesValidation="false" DataTextField="VendorName" DataValueField="RecordId" OnSelectedIndexChanged="ddlVendorsGlobal_SelectedIndexChanged" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:DropDownListItem Text="All Vendors" Value="All" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <telerik:RadDropDownList runat="server" ID="ddlOverLastGlobal" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlOverLastGlobal_SelectedIndexChanged">
                                        <Items>
                                            <telerik:DropDownListItem Text="All" Value="All" />
                                            <telerik:DropDownListItem Text="30 Days" Value="30" />
                                            <telerik:DropDownListItem Text="60 Days" Value="60" />
                                            <telerik:DropDownListItem Text="90 Days" Value="90" />
                                            <telerik:DropDownListItem Text="180 Days" Value="180" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr class="trheader">
                            <td colspan="4">
                                <div class="iaSummaryDiv" runat="server" id="divPendingApproval">
                                    <div class="iaSummaryDiv_Header">
                                        <div class="iaSummaryDiv_Text">Pending Approval</div>
                                    </div>
                                    <div class="iaSummaryDiv_Bottom">
                                        <div style="width: 65px; float: left; text-align: center; margin-top: 20px;">
                                            <asp:Label runat="server" ID="lblPendingApprovalCount" Text="" Font-Size="50px" OnPreRender="lblPendingApprovalCount_PreRender">3</asp:Label>
                                        </div>
                                        <div style="width: 85px; float: left; text-align: center; margin-top: 10px;">
                                            <div style="width: 85px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px;">Total</div>
                                            <div style="width: 85px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblPendingApprovalTotal" Text="" OnPreRender="lblPendingApprovalTotal_PreRender"></asp:Label>
                                            </div>
                                            <div style="width: 85px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 10px; font-size: 12px;">Oldest</div>
                                            <div style="width: 85px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblPendingApprovalOldest" Text="" OnPreRender="lblPendingApprovalOldest_PreRender"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="iaSummaryDiv" runat="server" id="divApproved">
                                    <div class="iaSummaryDiv_Header">
                                        <div class="iaSummaryDiv_Text">Approved</div>
                                    </div>
                                    <div class="iaSummaryDiv_Bottom">
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 20px;">
                                            <asp:Label runat="server" ID="lblApprovedCount" Text="" Font-Size="60px" OnPreRender="lblApprovedCount_PreRender"></asp:Label>
                                        </div>
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 10px;">
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px;">Total</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblApprovedTotal" Text="" OnPreRender="lblApprovedTotal_PreRender"></asp:Label>
                                            </div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 10px; font-size: 12px;">Oldest</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblApprovedOldest" Text="" OnPreRender="lblApprovedOldest_PreRender"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="iaSummaryDiv" runat="server" id="divOrdered">
                                    <div class="iaSummaryDiv_Header">
                                        <div class="iaSummaryDiv_Text">Ordered</div>
                                    </div>
                                    <div class="iaSummaryDiv_Bottom">
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 20px;">
                                            <asp:Label runat="server" ID="lblOrderedCount" Text="" Font-Size="60px" OnPreRender="lblOrderedCount_PreRender"></asp:Label>
                                        </div>
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 10px;">
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px;">Total</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblOrderedTotal" Text="" OnPreRender="lblOrderedTotal_PreRender">3</asp:Label>
                                            </div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 10px; font-size: 12px;">Oldest</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblOrderedOldest" Text="" OnPreRender="lblOrderedOldest_PreRender"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="iaSummaryDiv" runat="server" id="divArrived">
                                    <div class="iaSummaryDiv_Header">
                                        <div class="iaSummaryDiv_Text">Arrived</div>
                                    </div>
                                    <div class="iaSummaryDiv_Bottom">
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 20px;">
                                            <asp:Label runat="server" ID="lblArrivedCount" Text="" Font-Size="60px" OnPreRender="lblArrivedCount_PreRender"></asp:Label>
                                        </div>
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 10px;">
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px;">Total</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblArrivedTotal" Text="" OnPreRender="lblArrivedTotal_PreRender">3</asp:Label>
                                            </div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 10px; font-size: 12px;">Oldest</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblArrivedOldest" Text="" OnPreRender="lblArrivedOldest_PreRender"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="iaSummaryDiv" runat="server" id="divInvoiced">
                                    <div class="iaSummaryDiv_Header">
                                        <div class="iaSummaryDiv_Text">Invoiced</div>
                                    </div>
                                    <div class="iaSummaryDiv_Bottom">
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 20px;">
                                            <asp:Label runat="server" ID="lblInvoicedCount" Text="" Font-Size="60px" OnPreRender="lblInvoicedCount_PreRender"></asp:Label>
                                        </div>
                                        <div style="width: 75px; float: left; text-align: center; margin-top: 10px;">
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px;">Total</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblInvoicedTotal" Text="" OnPreRender="lblInvoicedTotal_PreRender">3</asp:Label>
                                            </div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 10px; font-size: 12px;">Oldest</div>
                                            <div style="width: 75px; text-align: center; height: 15px; padding-bottom: 2px; padding-top: 2px; font-size: 12px; background-color: #d0604c;">
                                                <asp:Label runat="server" ID="lblInvoicedOldest" Text="" OnPreRender="lblInvoicedOldest_PreRender"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 100%; float: left; height: auto; text-align:left;">
                                    <telerik:RadButton runat="server" ID="btnUpdateCurrentStock" Text="Update Current Stock" ButtonType="LinkButton" OnClick="btnUpdateCurrentStock_Click"></telerik:RadButton>
                                    <asp:Literal runat="server" ID="ltlVendorStock" OnPreRender="ltlVendorStock_PreRender"></asp:Literal>
                                </div>

                                <p></p>
                                <div style="width: 100%; height: auto; padding-top: 25px; padding-bottom: 25px; text-align:center">
                                    <span style="text-align: center; font-weight: bold; text-decoration: underline; padding-right:10px">Add New Inventory for Approval</span>
                                        <telerik:RadButton ID="btnAddNewItem" runat="server" Text="Add New Inventory Approval Item" ButtonType="SkinnedButton" ValidationGroup="NewItem"
                                            CssClass="plusSignImageBtn" CausesValidation="true" ToolTip="Add New Inventory Approval Item" OnClick="btnAddNewItem_Click">
                                            <Image EnableImageButton="true" />
                                        </telerik:RadButton>
                                    <p></p>
                                    <table style="border:none; border-spacing:0;">
                                        <tr>
                                            <th style="width:10%; text-align:center">Company</th>
                                            <th style="width:15%; text-align:center">Vendor</th>
                                            <th style="width:10%; text-align:center">PO #</th>
                                            <th style="width:15%; text-align:center">Material Group</th>
                                            <th style="width:10%; text-align:center">Cost</th>
                                            <th style="width:10%; text-align:center">Container #</th>
                                            <th style="width:10%; text-align:center">Priority</th>
                                            <th style="width:10%; text-align:center">Est. Ship Date</th>
                                            <th style="width:10%; text-align:center">ETA</th>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid black; width:10%; text-align:center">
                                                <telerik:RadDropDownList runat="server" ID="ddlCompany" Skin="Default" Width="75px">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Tingle" Value="Tingle" />
                                                        <telerik:DropDownListItem Text="Summit" Value="Summit" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td style="border: 1px solid black; width:15%; text-align:center">
                                                <telerik:RadDropDownList ID="ddlVendor" runat="server" SelectMethod="GetVendors" Skin="Default" Width="150px" 
                                                    CausesValidation="false" DataTextField="VendorName" DataValueField="RecordId"></telerik:RadDropDownList>
                                            </td>
                                            <td style="border: 1px solid black; width:10%; text-align:center">
                                                <telerik:RadTextBox runat="server" ID="txtPO" Width="85px" MaxLength="20"></telerik:RadTextBox> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="PO # Required" Display="None" ControlToValidate="txtPO" SetFocusOnError="true"
                                                        ValidationGroup="NewItem"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid black; width:15%; text-align:center">
                                                <telerik:RadTextBox runat="server" ID="txtMaterialGroup" Width="120px" TextMode="MultiLine" MaxLength="100"></telerik:RadTextBox> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Material Group Required" Display="None" ControlToValidate="txtMaterialGroup"
                                                        ValidationGroup="NewItem" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid black; width:10%; text-align:center">
                                                <telerik:RadNumericTextBox runat="server" ID="txtCost" MaxValue="1000000000" MinValue="1" Width="85px" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," 
                                                    Type="Currency"></telerik:RadNumericTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Cost Required" Display="None" ControlToValidate="txtCost" 
                                                    ValidationGroup="NewItem" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid black; width:10%; text-align:center">
                                                <telerik:RadTextBox runat="server" ID="txtContainer" Width="85px" MaxLength="20"></telerik:RadTextBox> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Container # Required" Display="None" ControlToValidate="txtContainer" 
                                                    ValidationGroup="NewItem" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid black; width:10%; text-align:center">
                                                <telerik:RadDropDownList ID="ddlPriority" Skin="Default" Width="75px" runat="server" CausesValidation="false"
                                                    SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" OnDataBound="ddlPriority_DataBound">
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td style="border: 1px solid black; width:10%; text-align:center">
                                                <input type="text" id="txtEstShipDate" runat="server" style="width:75px;" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Est. Ship Date Required" Display="None" ControlToValidate="txtEstShipDate" 
                                                    ValidationGroup="NewItem" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid black; width:10%; text-align:center">
                                                <input type="text" id="txtETA" runat="server" style="width:75px;"  />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="ETA Required" Display="None" ControlToValidate="txtETA" 
                                                    ValidationGroup="NewItem" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <p></p>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="NewItem" DisplayMode="List" ShowSummary="true" ForeColor="Red" />
                                </div>

                                <div style="width: 100%; height: auto; padding-top: 25px; padding-bottom: 25px; text-align:center">
                                    <span style="text-align: center; font-weight: bold; text-decoration: underline;">Inventory Pending Approval</span>
                                    <asp:Label runat="server" ID="lblPendingInTheLast" Text="" CssClass="in-the-last" OnPreRender="SetInTheLastText"></asp:Label><p></p>
                                    <span runat="server" id="spanPendingApprovalEmpty" visible="false" style="color:red; font-style:italic;">No Pending Approval Items to Display</span>
                                    <telerik:RadGrid runat="server" ID="gridPendingApproval" AllowSorting="True" AllowPaging="True" GridLines="Both"
                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true"
                                        OnUpdateCommand="gridPendingApproval_UpdateCommand" OnNeedDataSource="gridPendingApproval_NeedDataSource" 
                                        OnItemDataBound="gridPendingApproval_ItemDataBound" 
                                        OnItemCreated="gridPendingApproval_ItemCreated" AllowAutomaticUpdates="true" AllowAutomaticInserts="false"
                                        OnPreRender="gridPendingApproval_PreRender" UpdateMethod="UpdatePendingApproval" >
                                        <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" AllowColumnHide="false" AllowExpandCollapse="false"
	                                        Resizing-AllowColumnResize="false" AllowGroupExpandCollapse="false" AllowRowsDragDrop="false">
	                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
	                                        Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
	                                        HideStructureColumns="true" FileName="PendingApprovalInventory">
                                        </ExportSettings>
                                        <MasterTableView SelectMethod="GetPendingApproval" AllowFilteringByColumn="false" AllowPaging="true" PageSize="25" EditMode="Batch" CommandItemDisplay="Bottom"
	                                        DataKeyNames="RecordId" ShowHeadersWhenNoRecords="true" NoDetailRecordsText="No Records to Display" NoMasterRecordsText="No Records to Display">
	                                        <EditFormSettings EditFormType="AutoGenerated"></EditFormSettings>
	                                        <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" ShowAddNewRecordButton="false" />
	                                        <Columns>
		                                        <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
			                                        UniqueName="RecordId" HeaderText="RecordId" Visible="false">
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
		                                        <telerik:GridTemplateColumn DataField="Vendor.VendorName" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Vendor.VendorName"
			                                        UniqueName="Vendor.VendorName" HeaderText="Vendor" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("Vendor.VendorName") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlVendorEdit" runat="server" SelectMethod="GetVendors" Skin="Default" Width="95%" 
                                                            CausesValidation="false" DataTextField="VendorName" DataValueField="RecordId" Font-Size="10px" DropDownWidth="150px"></telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PurchaseOrderNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="PurchaseOrderNumber"
			                                        UniqueName="PurchaseOrderNumber" HeaderText="PO #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="85px" ID="txtPurchaseOrderNumberEdit" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtPurchaseOrderNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="MaterialGroup" DataType="System.String" Exportable="true" Groupable="False" SortExpression="MaterialGroup"
			                                        UniqueName="MaterialGroup" HeaderText="Material Group"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblMaterialGroup" runat="server" Text='<%# Eval("MaterialGroup") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="120px" ID="txtMaterialGroupEdit" TextMode="MultiLine" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtMaterialGroupEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Cost" DataType="System.Decimal" Exportable="true" Groupable="False" SortExpression="Cost" 
			                                        UniqueName="Cost" HeaderText="Cost"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="9%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="9%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblCost" runat="server" Text='<%# ((Decimal)Eval("Cost")).ToString("c0") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" ID="txtCostEdit" Width="65px" Font-Size="10px" MaxValue="1000000000" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," 
                                                            MinValue="1" Type="Currency"></telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtCostEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ContainerNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ContainerNumber"
			                                        UniqueName="ContainerNumber" HeaderText="Container #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblContainerNumber" runat="server" Text='<%# Eval("ContainerNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" ID="txtContainerNumberEdit" Width="85px" Font-Size="10px" MaxLength="25"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtContainerNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Priority.PriorityText" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Priority.PriorityText"
			                                        UniqueName="Priority.PriorityText" HeaderText="Priority"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPriority" runat="server" Font-Bold="true" Text='<%# Eval("Priority.PriorityText") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlPriorityEdit" Skin="Default" Width="67px" runat="server" CausesValidation="false" Font-Size="10px" 
                                                            SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" OnDataBound="ddlPriority_DataBound">
                                                        </telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Timestamp" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Timestamp"
			                                        UniqueName="Timestamp" HeaderText="Order Entry Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# Convert.ToDateTime(Eval("Timestamp")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="EstimatedShipDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="EstimatedShipDate"
			                                        UniqueName="EstimatedShipDate" HeaderText="Est. Ship Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblEstimatedShipDate" runat="server" Text='<%# Convert.ToDateTime(Eval("EstimatedShipDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtEstShipDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtEstShipDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="EstimatedTimeOfArrival" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="EstimatedTimeOfArrival"
			                                        UniqueName="EstimatedTimeOfArrival" HeaderText="ETA"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblETA" runat="server" Text='<%# Convert.ToDateTime(Eval("EstimatedTimeOfArrival")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtETAEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtETAEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="" DataType="System.String" Groupable="False" SortExpression=""
			                                        UniqueName="Approved" HeaderText="APPROVED" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
			                                        <ItemTemplate>
                                                        <telerik:RadButton runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click" CommandArgument='<%# Eval("RecordId") %>' ButtonType="SkinnedButton"
                                                            CssClass="button-approve-empty" HoveredCssClass="button-approve-checked" PressedCssClass="button-approve-checked">
                                                            <Image EnableImageButton="true" />
                                                        </telerik:RadButton>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
	                                        </Columns>
                                        </MasterTableView>
                                        </telerik:RadGrid>
                                    <div style="width:100%; text-align:right;">
                                        <asp:Label runat="server" ID="lblPendingTotalGrid" Text="" CssClass="Grid-Total-Label"></asp:Label>
                                    </div>
                                    <p style="padding-bottom:10px;"></p>



                                    <span style="text-align: center; font-weight: bold; text-decoration: underline;">Inventory Approved</span>
                                    <asp:Label runat="server" ID="lblApprovedInTheLast" Text="" CssClass="in-the-last" OnPreRender="SetInTheLastText"></asp:Label><p></p>
                                    <span runat="server" id="spanApprovedEmpty" visible="false" style="color:red; font-style:italic;">No Approved Items to Display</span>
                                    <telerik:RadGrid runat="server" ID="gridApproved" AllowSorting="True" AllowPaging="True" GridLines="Both"
	                                    AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true"
	                                    OnUpdateCommand="gridApproved_UpdateCommand" OnNeedDataSource="gridApproved_NeedDataSource" 
	                                    OnItemDataBound="gridApproved_ItemDataBound" Width="940px"
	                                    OnItemCreated="gridApproved_ItemCreated" AllowAutomaticUpdates="true" AllowAutomaticInserts="false"
	                                    OnPreRender="gridApproved_PreRender" UpdateMethod="UpdateApproved">
	                                    <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" AllowColumnHide="false" AllowExpandCollapse="false"
		                                    Resizing-AllowColumnResize="false" AllowGroupExpandCollapse="false" AllowRowsDragDrop="false">
		                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
	                                    </ClientSettings>
	                                    <PagerStyle AlwaysVisible="true" />
	                                    <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
		                                    Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
		                                    HideStructureColumns="true" FileName="PendingApprovalInventory">
	                                    </ExportSettings>
	                                    <MasterTableView SelectMethod="GetApproved" AllowFilteringByColumn="false" AllowPaging="true" PageSize="25" EditMode="Batch" CommandItemDisplay="Bottom"
		                                    DataKeyNames="RecordId" ShowHeadersWhenNoRecords="true" NoDetailRecordsText="No Records to Display" NoMasterRecordsText="No Records to Display">
		                                    <EditFormSettings EditFormType="AutoGenerated"></EditFormSettings>
		                                    <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" ShowAddNewRecordButton="false" />
		                                    <Columns>
			                                    <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
				                                    UniqueName="RecordId" HeaderText="RecordId" Visible="false">
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
				                                    </ItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="Vendor.VendorName" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Vendor.VendorName"
				                                    UniqueName="Vendor.VendorName" HeaderText="Vendor" AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("Vendor.VendorName") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadDropDownList ID="ddlVendorEdit" runat="server" SelectMethod="GetVendors" Skin="Default" Width="95%" 
						                                    CausesValidation="false" DataTextField="VendorName" DataValueField="RecordId" Font-Size="10px" DropDownWidth="150px"></telerik:RadDropDownList>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="PurchaseOrderNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="PurchaseOrderNumber"
				                                    UniqueName="PurchaseOrderNumber" HeaderText="PO #"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadTextBox runat="server" Width="85px" ID="txtPurchaseOrderNumberEdit" Font-Size="10px"></telerik:RadTextBox>
					                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtPurchaseOrderNumberEdit" SetFocusOnError="true"
					                                    ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="MaterialGroup" DataType="System.String" Exportable="true" Groupable="False" SortExpression="MaterialGroup"
				                                    UniqueName="MaterialGroup" HeaderText="Material Group"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblMaterialGroup" runat="server" Text='<%# Eval("MaterialGroup") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadTextBox runat="server" Width="120px" ID="txtMaterialGroupEdit" TextMode="MultiLine" Font-Size="10px"></telerik:RadTextBox>
					                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtMaterialGroupEdit" SetFocusOnError="true"
					                                    ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="Cost" DataType="System.Decimal" Exportable="true" Groupable="False" SortExpression="Cost" 
				                                    UniqueName="Cost" HeaderText="Cost"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="85px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblCost" runat="server" Text='<%# ((Decimal)Eval("Cost")).ToString("c0") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadNumericTextBox runat="server" ID="txtCostEdit" Width="65px" Font-Size="10px" MaxValue="1000000000" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," 
						                                    MinValue="1" Type="Currency"></telerik:RadNumericTextBox>
					                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtCostEdit" SetFocusOnError="true"
					                                    ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="ContainerNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ContainerNumber"
				                                    UniqueName="ContainerNumber" HeaderText="Container #"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblContainerNumber" runat="server" Text='<%# Eval("ContainerNumber") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadTextBox runat="server" ID="txtContainerNumberEdit" Width="85px" Font-Size="10px" MaxLength="25"></telerik:RadTextBox>
					                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtContainerNumberEdit" SetFocusOnError="true"
					                                    ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="Priority.PriorityText" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Priority.PriorityText"
				                                    UniqueName="Priority.PriorityText" HeaderText="Priority"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblPriority" runat="server" Font-Bold="true" Text='<%# Eval("Priority.PriorityText") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadDropDownList ID="ddlPriorityEdit" Skin="Default" Width="67px" runat="server" CausesValidation="false" Font-Size="10px" 
						                                    SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" OnDataBound="ddlPriority_DataBound">
					                                    </telerik:RadDropDownList>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="Timestamp" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Timestamp"
				                                    UniqueName="Timestamp" HeaderText="Order Entry Date"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblTimestamp" runat="server" Text='<%# Convert.ToDateTime(Eval("Timestamp")).ToString("MM/dd/yy") %>'></asp:Label>
				                                    </ItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="EstimatedShipDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="EstimatedShipDate"
				                                    UniqueName="EstimatedShipDate" HeaderText="Est. Ship Date"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblEstimatedShipDate" runat="server" Text='<%# Convert.ToDateTime(Eval("EstimatedShipDate")).ToString("MM/dd/yy") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadDatePicker runat="server" ID="txtEstShipDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
						                                    DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
					                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtEstShipDateEdit" SetFocusOnError="true"
					                                    ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="EstimatedTimeOfArrival" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="EstimatedTimeOfArrival"
				                                    UniqueName="EstimatedTimeOfArrival" HeaderText="ETA"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblETA" runat="server" Text='<%# Convert.ToDateTime(Eval("EstimatedTimeOfArrival")).ToString("MM/dd/yy") %>'></asp:Label>
				                                    </ItemTemplate>
				                                    <EditItemTemplate>
					                                    <telerik:RadDatePicker runat="server" ID="txtETAEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
						                                    DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
					                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtETAEdit" SetFocusOnError="true"
					                                    ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
				                                    </EditItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="Status.StatusDescription" DataType="System.String" Groupable="False" SortExpression="Status.StatusDescription"
				                                    UniqueName="Status.StatusDescription" HeaderText="Status" AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="85px" />
				                                    <ItemTemplate>
					                                    <telerik:RadDropDownList ID="ddlApprovedStatus" Skin="Default" Width="75px" runat="server" CausesValidation="false" Font-Size="10px" DropDownWidth="150px"
						                                    SelectMethod="GetStatuses" DataTextField="StatusDescription" DataValueField="RecordId" OnSelectedIndexChanged="ddlApprovedStatus_SelectedIndexChanged"
						                                    OnDataBinding="ddlApprovedStatus_DataBinding" AutoPostBack="true">
					                                    </telerik:RadDropDownList>
				                                    </ItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="ApprovedDate" ReadOnly="true" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ApprovedDate"
				                                    UniqueName="ApprovedDate" HeaderText="Approved Date"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblApprovedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ApprovedDate")).ToString("MM/dd/yy") %>'></asp:Label>
				                                    </ItemTemplate>
			                                    </telerik:GridTemplateColumn>
			                                    <telerik:GridTemplateColumn DataField="ApprovedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ApprovedBy.DisplayName"
				                                    UniqueName="ApprovedBy.DisplayName" HeaderText="Approved By"  AllowFiltering="false">
				                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
				                                    <ItemTemplate>
					                                    <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("ApprovedBy.DisplayName") %>'></asp:Label>
				                                    </ItemTemplate>
			                                    </telerik:GridTemplateColumn>
		                                    </Columns>
	                                    </MasterTableView>
	                                    </telerik:RadGrid>
                                    <div style="width:100%; text-align:right;">
                                        <asp:Label runat="server" ID="lblApprovedTotalGrid" Text="" CssClass="Grid-Total-Label"></asp:Label>
                                    </div>
                                    <p style="padding-bottom:10px;"></p>
                                    <span style="text-align: center; font-weight: bold; text-decoration: underline;">Inventory Ordered</span>
                                    <asp:Label runat="server" ID="lblOrderedInTheLast" Text="" CssClass="in-the-last" OnPreRender="SetInTheLastText"></asp:Label><p></p>
                                    <span runat="server" id="spanOrderedEmpty" visible="false" style="color:red; font-style:italic;">No Ordered Items to Display</span>
                                    <telerik:RadGrid runat="server" ID="gridOrdered" AllowSorting="True" AllowPaging="True" GridLines="Both"
                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Width="940px"
                                        OnUpdateCommand="gridOrdered_UpdateCommand" OnNeedDataSource="gridOrdered_NeedDataSource" 
                                        OnItemDataBound="gridOrdered_ItemDataBound" 
                                        OnItemCreated="gridOrdered_ItemCreated" AllowAutomaticUpdates="true" AllowAutomaticInserts="false"
                                        OnPreRender="gridOrdered_PreRender" UpdateMethod="UpdateOrdered">
                                        <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" AllowColumnHide="false" AllowExpandCollapse="false"
	                                        Resizing-AllowColumnResize="false" AllowGroupExpandCollapse="false" AllowRowsDragDrop="false">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="5"></Scrolling>
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
	                                        Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
	                                        HideStructureColumns="true" FileName="PendingApprovalInventory">
                                        </ExportSettings>
                                        <MasterTableView SelectMethod="GetOrdered" AllowFilteringByColumn="false" AllowPaging="true" PageSize="25" EditMode="Batch" CommandItemDisplay="Bottom"
	                                        DataKeyNames="RecordId" ShowHeadersWhenNoRecords="true" NoDetailRecordsText="No Records to Display" NoMasterRecordsText="No Records to Display">
	                                        <EditFormSettings EditFormType="AutoGenerated"></EditFormSettings>
	                                        <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" ShowAddNewRecordButton="false" />
	                                        <Columns>
		                                        <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
			                                        UniqueName="RecordId" HeaderText="RecordId" Visible="false">
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
		                                        <telerik:GridTemplateColumn DataField="Vendor.VendorName" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Vendor.VendorName"
			                                        UniqueName="Vendor.VendorName" HeaderText="Vendor" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="140px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("Vendor.VendorName") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlVendorEdit" runat="server" SelectMethod="GetVendors" Skin="Default" Width="95%" 
                                                            CausesValidation="false" DataTextField="VendorName" DataValueField="RecordId" Font-Size="10px" DropDownWidth="150px"></telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PurchaseOrderNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="PurchaseOrderNumber"
			                                        UniqueName="PurchaseOrderNumber" HeaderText="PO #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="85px" ID="txtPurchaseOrderNumberEdit" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtPurchaseOrderNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="MaterialGroup" DataType="System.String" Exportable="true" Groupable="False" SortExpression="MaterialGroup"
			                                        UniqueName="MaterialGroup" HeaderText="Material Group"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblMaterialGroup" runat="server" Text='<%# Eval("MaterialGroup") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="120px" ID="txtMaterialGroupEdit" TextMode="MultiLine" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtMaterialGroupEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Cost" DataType="System.Decimal" Exportable="true" Groupable="False" SortExpression="Cost" 
			                                        UniqueName="Cost" HeaderText="Cost"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="85px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblCost" runat="server" Text='<%# ((Decimal)Eval("Cost")).ToString("c0") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" ID="txtCostEdit" Width="65px" Font-Size="10px" MaxValue="1000000000" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," 
                                                            MinValue="1" Type="Currency"></telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtCostEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ContainerNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ContainerNumber"
			                                        UniqueName="ContainerNumber" HeaderText="Container #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblContainerNumber" runat="server" Text='<%# Eval("ContainerNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" ID="txtContainerNumberEdit" Width="85px" Font-Size="10px" MaxLength="25"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtContainerNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Priority.PriorityText" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Priority.PriorityText"
			                                        UniqueName="Priority.PriorityText" HeaderText="Priority"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPriority" runat="server" Font-Bold="true" Text='<%# Eval("Priority.PriorityText") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlPriorityEdit" Skin="Default" Width="67px" runat="server" CausesValidation="false" Font-Size="10px" 
                                                            SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" OnDataBound="ddlPriority_DataBound">
                                                        </telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Timestamp" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Timestamp"
			                                        UniqueName="Timestamp" HeaderText="Order Entry Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# Convert.ToDateTime(Eval("Timestamp")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ActualShipDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ActualShipDate"
			                                        UniqueName="ActualShipDate" HeaderText="Actual Ship Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblActualShipDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ActualShipDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtActualShipDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtActualShipDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="EstimatedTimeOfArrival" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="EstimatedTimeOfArrival"
			                                        UniqueName="EstimatedTimeOfArrival" HeaderText="ETA"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblETA" runat="server" Text='<%# Convert.ToDateTime(Eval("EstimatedTimeOfArrival")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtETAEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtETAEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Status.StatusDescription" DataType="System.String" Groupable="False" SortExpression="Status.StatusDescription"
			                                        UniqueName="Status.StatusDescription" HeaderText="Status" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="85px" />
			                                        <ItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlOrderedStatus" Skin="Default" Width="75px" runat="server" CausesValidation="false" Font-Size="10px" DropDownWidth="150px"
                                                            SelectMethod="GetStatuses" DataTextField="StatusDescription" DataValueField="RecordId" OnSelectedIndexChanged="ddlOrderedStatus_SelectedIndexChanged"
                                                            OnDataBinding="ddlOrderedStatus_DataBinding" AutoPostBack="true">
                                                        </telerik:RadDropDownList>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="OrderDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="OrderDate"
			                                        UniqueName="OrderDate" HeaderText="Order Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblOrderedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrderDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtOrderedDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtOrderedDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="OrderedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="OrderedBy.DisplayName"
			                                        UniqueName="OrderedBy.DisplayName" HeaderText="Ordered By"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblOrderedBy" runat="server" Text='<%# Eval("OrderedBy.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ApprovedDate" ReadOnly="true" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ApprovedDate"
			                                        UniqueName="ApprovedDate" HeaderText="Approved Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblApprovedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ApprovedDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ApprovedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ApprovedBy.DisplayName"
			                                        UniqueName="ApprovedBy.DisplayName" HeaderText="Approved By"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("ApprovedBy.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
	                                        </Columns>
                                        </MasterTableView>
                                        </telerik:RadGrid>
                                    <div style="width:100%; text-align:right;">
                                        <asp:Label runat="server" ID="lblOrderedTotalGrid" Text="" CssClass="Grid-Total-Label"></asp:Label>
                                    </div>
                                    <p style="padding-bottom:10px;"></p>



                                    <span style="text-align: center; font-weight: bold; text-decoration: underline;">Inventory Arrivals</span>
                                    <asp:Label runat="server" ID="lblArrivedInTheLast" Text="" CssClass="in-the-last" OnPreRender="SetInTheLastText"></asp:Label><p></p>
                                    <span runat="server" id="spanArrivalsEmpty" visible="false" style="color:red; font-style:italic;">No Arrived Items to Display</span>
                                    <telerik:RadGrid runat="server" ID="gridArrived" AllowSorting="True" AllowPaging="True" GridLines="Both"
                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Width="940px"
                                        OnUpdateCommand="gridArrived_UpdateCommand" OnNeedDataSource="gridArrived_NeedDataSource" 
                                        OnItemDataBound="gridArrived_ItemDataBound" 
                                        OnItemCreated="gridArrived_ItemCreated" AllowAutomaticUpdates="true" AllowAutomaticInserts="false"
                                        OnPreRender="gridArrived_PreRender" UpdateMethod="UpdateArrived">
                                        <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" AllowColumnHide="false" AllowExpandCollapse="false"
	                                        Resizing-AllowColumnResize="false" AllowGroupExpandCollapse="false" AllowRowsDragDrop="false">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="5"></Scrolling>
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
	                                        Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
	                                        HideStructureColumns="true" FileName="PendingApprovalInventory">
                                        </ExportSettings>
                                        <MasterTableView SelectMethod="GetArrived" AllowFilteringByColumn="false" AllowPaging="true" PageSize="25" EditMode="Batch" CommandItemDisplay="Bottom"
	                                        DataKeyNames="RecordId" ShowHeadersWhenNoRecords="true" NoDetailRecordsText="No Records to Display" NoMasterRecordsText="No Records to Display">
	                                        <EditFormSettings EditFormType="AutoGenerated"></EditFormSettings>
	                                        <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" ShowAddNewRecordButton="false" />
	                                        <Columns>
		                                        <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
			                                        UniqueName="RecordId" HeaderText="RecordId" Visible="false">
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
		                                        <telerik:GridTemplateColumn DataField="Vendor.VendorName" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Vendor.VendorName"
			                                        UniqueName="Vendor.VendorName" HeaderText="Vendor" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="140px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("Vendor.VendorName") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlVendorEdit" runat="server" SelectMethod="GetVendors" Skin="Default" Width="95%" 
                                                            CausesValidation="false" DataTextField="VendorName" DataValueField="RecordId" Font-Size="10px" DropDownWidth="150px"></telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PurchaseOrderNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="PurchaseOrderNumber"
			                                        UniqueName="PurchaseOrderNumber" HeaderText="PO #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="85px" ID="txtPurchaseOrderNumberEdit" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtPurchaseOrderNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="MaterialGroup" DataType="System.String" Exportable="true" Groupable="False" SortExpression="MaterialGroup"
			                                        UniqueName="MaterialGroup" HeaderText="Material Group"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblMaterialGroup" runat="server" Text='<%# Eval("MaterialGroup") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="120px" ID="txtMaterialGroupEdit" TextMode="MultiLine" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtMaterialGroupEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Cost" DataType="System.Decimal" Exportable="true" Groupable="False" SortExpression="Cost" 
			                                        UniqueName="Cost" HeaderText="Cost"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="85px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblCost" runat="server" Text='<%# ((Decimal)Eval("Cost")).ToString("c0") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" ID="txtCostEdit" Width="65px" Font-Size="10px" MaxValue="1000000000" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," 
                                                            MinValue="1" Type="Currency"></telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtCostEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ContainerNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ContainerNumber"
			                                        UniqueName="ContainerNumber" HeaderText="Container #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblContainerNumber" runat="server" Text='<%# Eval("ContainerNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" ID="txtContainerNumberEdit" Width="85px" Font-Size="10px" MaxLength="25"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtContainerNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Priority.PriorityText" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Priority.PriorityText"
			                                        UniqueName="Priority.PriorityText" HeaderText="Priority"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPriority" runat="server" Font-Bold="true" Text='<%# Eval("Priority.PriorityText") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlPriorityEdit" Skin="Default" Width="67px" runat="server" CausesValidation="false" Font-Size="10px" 
                                                            SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" OnDataBound="ddlPriority_DataBound">
                                                        </telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ActualShipDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ActualShipDate"
			                                        UniqueName="ActualShipDate" HeaderText="Actual Ship Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblActualShipDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ActualShipDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtActualShipDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtActualShipDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ArrivalDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ArrivalDate"
			                                        UniqueName="ArrivalDate" HeaderText="Arrival Date" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblArrivalDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ArrivalDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtArrivalDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtArrivalDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="TimeToArrival" DataType="System.String" Exportable="true" Groupable="False" SortExpression="TimeToArrival"
			                                        UniqueName="TimeToArrival" HeaderText="Time To Arrival" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblTimeToArrival" runat="server" Text='<%# Eval("TimeToArrival") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Status.StatusDescription" DataType="System.String" Groupable="False" SortExpression="Status.StatusDescription"
			                                        UniqueName="Status.StatusDescription" HeaderText="Status" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="85px" />
			                                        <ItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlArrivedStatus" Skin="Default" Width="75px" runat="server" CausesValidation="false" Font-Size="10px" DropDownWidth="150px"
                                                            SelectMethod="GetStatuses" DataTextField="StatusDescription" DataValueField="RecordId" OnSelectedIndexChanged="ddlArrivedStatus_SelectedIndexChanged"
                                                            OnDataBinding="ddlArrivedStatus_DataBinding" AutoPostBack="true">
                                                        </telerik:RadDropDownList>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="OrderDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="OrderDate"
			                                        UniqueName="OrderDate" HeaderText="Order Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblOrderedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrderDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtOrderDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtOrderDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="OrderedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="OrderedBy.DisplayName"
			                                        UniqueName="OrderedBy.DisplayName" HeaderText="Ordered By"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblOrderedBy" runat="server" Text='<%# Eval("OrderedBy.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ApprovedDate" ReadOnly="true" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ApprovedDate"
			                                        UniqueName="ApprovedDate" HeaderText="Approved Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblApprovedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ApprovedDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ApprovedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ApprovedBy.DisplayName"
			                                        UniqueName="ApprovedBy.DisplayName" HeaderText="Approved By"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("ApprovedBy.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Timestamp" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Timestamp"
			                                        UniqueName="Timestamp" HeaderText="Order Entry Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# Convert.ToDateTime(Eval("Timestamp")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
	                                        </Columns>
                                        </MasterTableView>
                                        </telerik:RadGrid>
                                    <div style="width:100%; text-align:right">
                                        <asp:Label runat="server" ID="lblArrivedTotalGrid" Text="" CssClass="Grid-Total-Label"></asp:Label>
                                    </div>
                                    <p style="padding-bottom:10px;"></p>


                                    <span style="text-align: center; font-weight: bold; text-decoration: underline;">Inventory Invoiced</span>
                                    <asp:Label runat="server" ID="lblInvoicedInTheLast" Text="" CssClass="in-the-last" OnPreRender="SetInTheLastText"></asp:Label><p></p>
                                    <span runat="server" id="spanInvoicedEmpty" visible="false" style="color:red; font-style:italic;">No Arrived Items to Display</span>
                                    <telerik:RadGrid runat="server" ID="gridInvoiced" AllowSorting="True" AllowPaging="True" GridLines="Both"
                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" Width="940px"
                                        OnUpdateCommand="gridInvoiced_UpdateCommand" OnNeedDataSource="gridInvoiced_NeedDataSource" 
                                        OnItemDataBound="gridInvoiced_ItemDataBound" 
                                        OnItemCreated="gridInvoiced_ItemCreated" AllowAutomaticUpdates="true" AllowAutomaticInserts="false"
                                        OnPreRender="gridInvoiced_PreRender" UpdateMethod="UpdateInvoiced">
                                        <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" AllowColumnHide="false" AllowExpandCollapse="false"
	                                        Resizing-AllowColumnResize="false" AllowGroupExpandCollapse="false" AllowRowsDragDrop="false">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="5"></Scrolling>
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
	                                        Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
	                                        HideStructureColumns="true" FileName="PendingApprovalInventory">
                                        </ExportSettings>
                                        <MasterTableView SelectMethod="GetInvoiced" AllowFilteringByColumn="false" AllowPaging="true" PageSize="25" EditMode="Batch" CommandItemDisplay="Bottom"
	                                        DataKeyNames="RecordId" ShowHeadersWhenNoRecords="true" NoDetailRecordsText="No Records to Display" NoMasterRecordsText="No Records to Display">
	                                        <EditFormSettings EditFormType="AutoGenerated"></EditFormSettings>
	                                        <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" ShowAddNewRecordButton="false" />
	                                        <Columns>
		                                        <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
			                                        UniqueName="RecordId" HeaderText="RecordId" Visible="false">
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
		                                        <telerik:GridTemplateColumn DataField="Vendor.VendorName" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Vendor.VendorName"
			                                        UniqueName="Vendor.VendorName" HeaderText="Vendor" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="140px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("Vendor.VendorName") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlVendorEdit" runat="server" SelectMethod="GetVendors" Skin="Default" Width="95%" 
                                                            CausesValidation="false" DataTextField="VendorName" DataValueField="RecordId" Font-Size="10px" DropDownWidth="150px"></telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PurchaseOrderNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="PurchaseOrderNumber"
			                                        UniqueName="PurchaseOrderNumber" HeaderText="PO #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="85px" ID="txtPurchaseOrderNumberEdit" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtPurchaseOrderNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="MaterialGroup" DataType="System.String" Exportable="true" Groupable="False" SortExpression="MaterialGroup"
			                                        UniqueName="MaterialGroup" HeaderText="Material Group"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblMaterialGroup" runat="server" Text='<%# Eval("MaterialGroup") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" Width="120px" ID="txtMaterialGroupEdit" TextMode="MultiLine" Font-Size="10px"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtMaterialGroupEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Cost" DataType="System.Decimal" Exportable="true" Groupable="False" SortExpression="Cost" 
			                                        UniqueName="Cost" HeaderText="Cost"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="85px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblCost" runat="server" Text='<%# ((Decimal)Eval("Cost")).ToString("c0") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" ID="txtCostEdit" Width="65px" Font-Size="10px" MaxValue="1000000000" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="," 
                                                            MinValue="1" Type="Currency"></telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtCostEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ContainerNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ContainerNumber"
			                                        UniqueName="ContainerNumber" HeaderText="Container #"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblContainerNumber" runat="server" Text='<%# Eval("ContainerNumber") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" ID="txtContainerNumberEdit" Width="85px" Font-Size="10px" MaxLength="25"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtContainerNumberEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Priority.PriorityText" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Priority.PriorityText"
			                                        UniqueName="Priority.PriorityText" HeaderText="Priority"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPriority" runat="server" Font-Bold="true" Text='<%# Eval("Priority.PriorityText") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlPriorityEdit" Skin="Default" Width="67px" runat="server" CausesValidation="false" Font-Size="10px" 
                                                            SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" OnDataBound="ddlPriority_DataBound">
                                                        </telerik:RadDropDownList>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ActualShipDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ActualShipDate"
			                                        UniqueName="ActualShipDate" HeaderText="Actual Ship Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblActualShipDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ActualShipDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtActualShipDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtActualShipDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ArrivalDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ArrivalDate"
			                                        UniqueName="ArrivalDate" HeaderText="Arrival Date" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblArrivalDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ArrivalDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtArrivalDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtArrivalDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="TimeToArrival" DataType="System.String" Exportable="true" Groupable="False" SortExpression="TimeToArrival"
			                                        UniqueName="TimeToArrival" HeaderText="Time To Arrival" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblTimeToArrival" runat="server" Text='<%# Eval("TimeToArrival") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="InvoiceDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="InvoiceDate"
			                                        UniqueName="InvoiceDate" HeaderText="Invoice Date" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Convert.ToDateTime(Eval("InvoiceDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtInvoiceDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtInvoiceDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="InvoicedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="InvoicedBy.DisplayName"
			                                        UniqueName="InvoicedBy.DisplayName" HeaderText="Invoiced By"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblInvoicedBy" runat="server" Text='<%# Eval("InvoicedBy.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Status.StatusDescription" DataType="System.String" Groupable="False" SortExpression="Status.StatusDescription"
			                                        UniqueName="Status.StatusDescription" HeaderText="Status" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="85px" />
			                                        <ItemTemplate>
                                                        <telerik:RadDropDownList ID="ddlInvoicedStatus" Skin="Default" Width="75px" runat="server" CausesValidation="false" Font-Size="10px" DropDownWidth="150px"
                                                            SelectMethod="GetStatuses" DataTextField="StatusDescription" DataValueField="RecordId" OnSelectedIndexChanged="ddlInvoicedStatus_SelectedIndexChanged"
                                                            OnDataBinding="ddlInvoicedStatus_DataBinding" AutoPostBack="true">
                                                        </telerik:RadDropDownList>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="OrderDate" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="OrderDate"
			                                        UniqueName="OrderDate" HeaderText="Order Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblOrderedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrderDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker runat="server" ID="txtOrderDateEdit" Width="60px" DatePopupButton-Visible="false" ShowPopupOnFocus="true" DateInput-Font-Size="10px" 
                                                            DateInput-ClientEvents-OnKeyPress="OnKeyPress"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="* Required" Display="Dynamic" ControlToValidate="txtOrderDateEdit" SetFocusOnError="true"
                                                        ValidationGroup="EditItem" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="OrderedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="OrderedBy.DisplayName"
			                                        UniqueName="OrderedBy.DisplayName" HeaderText="Ordered By"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblOrderedBy" runat="server" Text='<%# Eval("OrderedBy.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ApprovedDate" ReadOnly="true" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="ApprovedDate"
			                                        UniqueName="ApprovedDate" HeaderText="Approved Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblApprovedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ApprovedDate")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ApprovedBy.DisplayName" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ApprovedBy.DisplayName"
			                                        UniqueName="ApprovedBy.DisplayName" HeaderText="Approved By"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("ApprovedBy.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Timestamp" ReadOnly="true" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Timestamp"
			                                        UniqueName="Timestamp" HeaderText="Order Entry Date"  AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# Convert.ToDateTime(Eval("Timestamp")).ToString("MM/dd/yy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
	                                        </Columns>
                                        </MasterTableView>
                                        </telerik:RadGrid>
                                    <div style="width:100%; text-align:right">
                                        <asp:Label runat="server" ID="lblInvoicedTotalGrid" Text="" CssClass="Grid-Total-Label"></asp:Label>
                                    </div>
                                    <p style="padding-bottom:10px;"></p>
                                </div>
                                <table style="border:none; width:100%">
                                    <tr>
                                        <td style="width:100%;">
                                            <div style="width:90%; border:1px solid black; border-radius:5px; margin: 0 auto; height:90px; padding-bottom:10px;">
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
                                                        <td style="width:12%; text-align:right">
                                                            <asp:Label runat="server" ID="lblNotifyAbout" Text="Notify About" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td style="width:23%; text-align:left">
                                                            <telerik:RadDropDownList runat="server" ID="ddlNotifyAbout" Width="130px">
                                                                <Items>
                                                                    <telerik:DropDownListItem Text="Pending Approval" Value="PendingApproval" />
                                                                    <telerik:DropDownListItem Text="Approved" Value="Approved" />
                                                                    <telerik:DropDownListItem Text="Ordered" Value="Ordered" />
                                                                    <telerik:DropDownListItem Text="Arrived" Value="Arrived" />
                                                                    <telerik:DropDownListItem Text="Invoiced" Value="Invoiced" />
                                                                    <telerik:DropDownListItem Text="All" Value="All" />
                                                                </Items>
                                                            </telerik:RadDropDownList>
                                                        </td>
                                                        <td rowspan="2" style="width:3%; text-align:right">
                                                            <asp:CheckBox runat="server" ID="cbSendComments" />
                                                        </td>
                                                        <td rowspan="2" style="width:17%; text-align:left; font-size:12px">
                                                            Include the last 
                                                            <telerik:RadDropDownList runat="server" ID="ddlCommentCount" Width="45px">
                                                                <Items>
                                                                    <telerik:DropDownListItem Text="1" Value="1" />
                                                                    <telerik:DropDownListItem Text="2" Value="2" />
                                                                    <telerik:DropDownListItem Text="3" Value="3" />
                                                                    <telerik:DropDownListItem Text="4" Value="4" />
                                                                    <telerik:DropDownListItem Text="5" Value="5" />
                                                                    <telerik:DropDownListItem Text="6" Value="6" />
                                                                    <telerik:DropDownListItem Text="7" Value="7" />
                                                                    <telerik:DropDownListItem Text="8" Value="8" />
                                                                    <telerik:DropDownListItem Text="9" Value="9" />
                                                                    <telerik:DropDownListItem Text="10" Value="10" />
                                                                    <telerik:DropDownListItem Text="All" Value="All" />
                                                                </Items>
                                                            </telerik:RadDropDownList> Comments in Notification
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
                                                        <td style="width:12%; text-align:right">
                                                            <asp:Label runat="server" ID="lblNotifyOverLast" Text="Over the last" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td style="width:23%; text-align:left">
                                                            <telerik:RadDropDownList runat="server" ID="ddlNotifyOverLast" Width="50px">
                                                                <Items>
                                                                    <telerik:DropDownListItem Text="30" Value="30" />
                                                                    <telerik:DropDownListItem Text="60" Value="60" />
                                                                    <telerik:DropDownListItem Text="90" Value="90" />
                                                                    <telerik:DropDownListItem Text="180" Value="180" />
                                                                    <telerik:DropDownListItem Text="All" Value="All" />
                                                                </Items>
                                                            </telerik:RadDropDownList>
                                                            Days
                                                        </td>

                                                    </tr>
                                                </table>
                                                
                                            </div>
                                            <p style="border-bottom:15px; text-align:center">
                                                    <asp:Button ID="btnSendNotification" runat="server" Text="Send Notification" OnClick="btnSendNotification_Click" CssClass="normalButton" />
                                                </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:100%;">
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
                                        <td style="width:100%; padding-bottom:15px;">
                                            <p class="smallText" style="font-weight:bold;">Inventory Notifications Sent To:<br /><br />
                                                <asp:Label ID="lblEmailsSentTo" style="color:black; font-style:italic; font-size:12px; font-weight:normal;" runat="server"></asp:Label></p>
                                            <p class="smallText" style="font-weight:bold;">
                                                <asp:Label runat="server" ID="lblNotificationMessage" Text=""></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:100%">
                                            <br /><span class="formRedText">Comments</span><br />
                                            <telerik:RadTextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="90%" MaxLength="300" Height="60px"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:100%">
                                            <div style="width:40%; text-align:left; float:left; margin-left:5%">
                                                <span style="font-size:10px">300 Characters Max</span>
                                                <asp:Label runat="server" ID="lblCommentMessage" Font-Size="10px" Text=""></asp:Label>
                                            </div>
                                            <div style="width:40%; text-align:right; float:right; margin-right:5%">
                                                <asp:Button ID="btnPostComment" runat="server" Text="Post Comment" OnClick="btnPostComment_Click" CssClass="normalButton" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:100%">
                                            <asp:Repeater runat="server" ID="rptrComments" SelectMethod="rptrComments_GetData" ItemType="Tingle_WebForms.Models.InventoryComments">
                                                <ItemTemplate>
                                                    <asp:Literal runat="server" ID="ltlComment" Text='<%# LoadCommentLiteral(Convert.ToBoolean(Eval("SystemComment")), Eval("Note").ToString(), Convert.ToString(Eval("User.DisplayName")), Eval("Timestamp").ToString()) %>'></asp:Literal>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>

                                <div runat="server" id="divUdpateCurrentStock" style="background-color: rgba(216,216,216,.6); height: 100%; width: 100%; position: absolute; top: 0px; left: 0px; z-index:9999" visible="false">
                                    <div style="position: relative; width: 400px; height: 500px; margin: 0 auto; background-color: white; top: 25%; padding: 10px; overflow-x: hidden; overflow-y: auto">
                                        <asp:PlaceHolder runat="server" ID="phCurrentStock">
                                            <div style="position: absolute; bottom: 10px; left: 0px; text-align: center; margin: 0 auto; width: 100%">
                                                <telerik:RadButton runat="server" ID="btnUpdate" Text="Update Stock" ButtonType="StandardButton" OnClick="btnUpdate_Click"></telerik:RadButton>
                                                <telerik:RadButton runat="server" ID="btnCancel" Text="Cancel" ButtonType="StandardButton" OnClick="btnCancel_Click" CausesValidation="false"></telerik:RadButton>
                                            </div>
                                        </asp:PlaceHolder>
                                    </div>
                                </div>

                            </td>
                        </tr>
                    </table>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="loadingDiv">
        <div id="loadingGif">
            <img src="Images/loading.gif" />
        </div>
    </div>
</asp:Content>
