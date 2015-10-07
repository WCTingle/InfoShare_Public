<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportOrderCancellation.aspx.cs" EnableEventValidation="false" Inherits="Tingle_WebForms.ReportOrderCancellation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
    <script type="text/javascript">
        function ClearBox(sender, args) {
            sender.clearSelection();
            sender.set_text("");
            sender.setAllItemsVisible(true);
            var input = sender.get_inputDomElement();
            input.focus();
        }

        function OnKeyPress(sender, args) {
            args.set_cancel(true);
        }

        function ComboBoxBlur(sender, args) {
            var box = sender;
            if (box.get_checkedItems().length > 0)
                sender.set_text(box.get_checkedItems().length.toString() + " Email(s) Selected");
        }

        function ComboBoxBlurPO(sender, args) {
            var combo = sender;
            var selectedItemsValues = "";
            var items = combo.get_items();
            var itemsCount = items.get_count();

            for (var itemIndex = 0; itemIndex < itemsCount; itemIndex++) {
                var item = items.getItem(itemIndex);
                var checkbox = getItemCheckBox(item);
                if (checkbox.checked) {
                    selectedItemsValues += item.get_text() + ", ";
                }
            }

            selectedItemsValues = selectedItemsValues.substring(0, selectedItemsValues.length - 2);


            if (selectedItemsValues !== '') {
                sender.set_text(selectedItemsValues);
            }

        }
        function getItemCheckBox(item) {
            var itemDiv = item.get_element();
            var inputs = itemDiv.getElementsByTagName("input");
            for (var inputIndex = 0; inputIndex < inputs.length; inputIndex++) {
                var input = inputs[inputIndex];
                if (input.type == "checkbox") {
                    return input;
                }
            }
        }

        function ComboBoxFocus(sender, args) {
            sender.set_text("");
        }

        function ShowAddNewEmailTd(sender, args) {
            jQuery("#divAddNewEmail").css("display", "block");
        }

        function CancelNewEmail(sender, args) {
            jQuery("#MainContent_fvReport_txtNameInsert").text("");
            jQuery("#MainContent_fvReport_txtAddressInsert").text("");
            jQuery("#divAddNewEmail").css("display", "none");
        }

    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function onPopUpShowing(sender, args) {
                $("html, body").animate({ scrollTop: 0 }, "slow");

            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gvReport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gvReport" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass=""></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" MinDisplayTime="1000" CssClass="loadingDiv">
        <img src="Images/loading.gif" style="position:absolute; top:40%;" />
    </telerik:RadAjaxLoadingPanel>
            <div class="divcenter">
                <asp:Panel ID="pnlFilter" runat="server">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">
                                <div style="float: left; width: 25%">
                                    <telerik:RadDropDownList runat="server" ID="ddlCompany" AutoPostBack="true" CausesValidation="false" Width="110px">
                                        <Items>
                                            <telerik:DropDownListItem Text="Any Company" Value="Any Company" />
                                            <telerik:DropDownListItem Text="Tingle" Value="Tingle" />
                                            <telerik:DropDownListItem Text="Summit" Value="Summit" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                                <div style="float: left; width: 50%">
                                    Order Cancellation Requests
                                </div>
                                <div style="float: left; width: 25%">
                                    <telerik:RadDropDownList runat="server" ID="ddlGlobalStatus" AutoPostBack="true" CausesValidation="false"
                                        Width="75px" Skin="Default">
                                        <Items>
                                            <telerik:DropDownListItem Text="Active" Value="Active" />
                                            <telerik:DropDownListItem Text="Archive" Value="Archive" />
                                            <telerik:DropDownListItem Text="All" Value="All" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:LinkButton runat="server" ID="btnAdvancedSearch" Text="Click for Advanced Search" OnClick="btnAdvancedSearch_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnBasicSearch" Text="Click for Basic Search" Visible="false" OnClick="btnBasicSearch_Click"></asp:LinkButton>
                        </tr>
                        <tr>
                            <td class="tdcenter" colspan="2">Submission Date Range:</td>
                            <td colspan="2" class="tdleft">From:
                                <telerik:RadDatePicker runat="server" ID="txtFromDate" ShowPopupOnFocus="true" DateInput-CssClass="date-picker-small" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-small-wrapper"
                                    DateInput-ClientEvents-OnKeyPress="OnKeyPress" AutoPostBack="true"></telerik:RadDatePicker>
                                To:
                                <telerik:RadDatePicker runat="server" ID="txtToDate" ShowPopupOnFocus="true" DateInput-CssClass="date-picker-small" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-small-wrapper"
                                    DateInput-ClientEvents-OnKeyPress="OnKeyPress" AutoPostBack="true"></telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdright">Requested By:</td>
                            <td class="tdleft">
                                <telerik:RadComboBox runat="server" ID="ddlRequestedBy" DataTextField="EmailAddress" DataValueField="SystemUserId" Height="250px" Width="175px" AutoPostBack="true"
                                    DropDownWidth="300px" ItemType="Tingle_WebForms.Models.SystemUsers" Skin="Default" SelectMethod="GetUsers"
                                    EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true" CausesValidation="false">
                                    <ItemTemplate>
                                        <%# Eval("DisplayName") %> -- <%# Eval("EmailAddress") %>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td class="tdright">Assigned To:</td>
                            <td class="tdleft">
                                <telerik:RadComboBox runat="server" ID="ddlAssignedTo" DataTextField="EmailAddress" DataValueField="SystemUserId" Height="250px" Width="175px" AutoPostBack="true"
                                    DropDownWidth="300px" ItemType="Tingle_WebForms.Models.SystemUsers" Skin="Default" SelectMethod="GetUsers"
                                    EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true" CausesValidation="false">
                                    <ItemTemplate>
                                        <%# Eval("DisplayName") %> -- <%# Eval("EmailAddress") %>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdheader" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdright">Order #:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtOrderNumber" runat="server" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td class="tdright">Armstrong Reference:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtArmstrongReference" runat="server" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdright" colspan="2">Status:</td>
                            <td class="tdleft" colspan="2">
                                <telerik:RadDropDownList ID="ddlStatus" runat="server" SelectMethod="GetStatuses" Skin="Default" AutoPostBack="true"
                                    CausesValidation="false" DataTextField="StatusText" DataValueField="StatusID"
                                    DefaultMessage="Any Status">
                                </telerik:RadDropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="tr1" visible="false">
                            <td class="tdright">Customer:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtCustomer" runat="server" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td class="tdright">PO:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtPO" runat="server" AutoPostBack="true"></asp:TextBox></td>
                        </tr>
                        <tr runat="server" id="tr2" visible="false">
                            <td class="tdright">SKU:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtSKU" runat="server" AutoPostBack="true"></asp:TextBox></td>
                            <td class="tdright">Status of PO:</td>
                            <td class="tdleft">
                                <telerik:RadComboBox runat="server" ID="ddlPOStatus" DataTextField="StatusCode" DataValueField="StatusCode" Width="150px"
                                    CheckBoxes="true" DropDownWidth="150px" ItemType="Tingle_WebForms.Models.PurchaseOrderStatus" Skin="Default" SelectMethod="GetPOStatuses"
                                    EnableCheckAllItemsCheckBox="true" EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true"
                                    OnClientItemChecked="ClearBox" OnClientBlur="ComboBoxBlurPO" OnClientFocus="ComboBoxFocus" Localization-CheckAllString="Select all Statuses"
                                    CausesValidation="false" OnDataBound="ddlPOStatus_DataBound" AutoPostBack="true">
                                    <ItemTemplate>
                                        <%# Eval("StatusCode") %> -- <%# Eval("Status") %>
                                    </ItemTemplate>
                                </telerik:RadComboBox>

                            </td>
                        </tr>
                        <tr runat="server" id="tr3" visible="false">
                            <td class="tdright">Line:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtLine" runat="server" AutoPostBack="true"></asp:TextBox></td>
                            <td class="tdright">Line of PO:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtLineOfPO" runat="server" AutoPostBack="true"></asp:TextBox></td>
                        </tr>
                        <tr runat="server" id="tr4" visible="false">
                            <td class="tdright">Size:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtSize" runat="server" AutoPostBack="true"></asp:TextBox></td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr runat="server" id="tr5" visible="false">
                            <td class="tdsectionheader" colspan="4"></td>
                        </tr>
                        <tr runat="server" id="tr6" visible="false">
                            <td class="tdcenter" colspan="2">Date Required Range:</td>
                            <td colspan="2" class="tdleft">From:
                                <telerik:RadDatePicker runat="server" ID="txtDateRequiredFrom" ShowPopupOnFocus="true" DateInput-CssClass="date-picker-small" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-small-wrapper"
                                    DateInput-ClientEvents-OnKeyPress="OnKeyPress" AutoPostBack="true"></telerik:RadDatePicker>
                                To:
                                <telerik:RadDatePicker runat="server" ID="txtDateRequiredTo" ShowPopupOnFocus="true" DateInput-CssClass="date-picker-small" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-small-wrapper"
                                    DateInput-ClientEvents-OnKeyPress="OnKeyPress" AutoPostBack="true"></telerik:RadDatePicker>
                                </td>
                        </tr>
                        <tr runat="server" id="tr7" visible="false">
                            <td class="tdright">Ship Via:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtShipVia" runat="server" AutoPostBack="true"></asp:TextBox></td>
                            <td class="tdright">Serial:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtSerial" runat="server" AutoPostBack="true"></asp:TextBox></td>
                        </tr>
                        <tr runat="server" id="tr8" visible="false">
                            <td class="tdright">Truck Route:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtTruckRoute" runat="server" AutoPostBack="true"></asp:TextBox></td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr runat="server" id="tr9" visible="false">
                            <td class="tdright">Due By:</td>
                            <td class="tdleft">
                                <table style="border: none;">
                                    <tr>
                                        <td style="width: 25%" class="tdright">From:</td>
                                        <td style="width: 75%" class="tdleft">
                                            <telerik:RadDatePicker runat="server" ID="txtDueByDateFrom" ShowPopupOnFocus="true" DateInput-CssClass="date-picker-small" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-small-wrapper"
                                                DateInput-ClientEvents-OnKeyPress="OnKeyPress" AutoPostBack="true"></telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="tdright">To:</td>
                                        <td style="width: 75%" class="tdleft">
                                            <telerik:RadDatePicker runat="server" ID="txtDueByDateTo" ShowPopupOnFocus="true" DateInput-CssClass="date-picker-small" DatePopupButton-Visible="false" DateInput-WrapperCssClass="date-picker-small-wrapper"
                                                DateInput-ClientEvents-OnKeyPress="OnKeyPress" AutoPostBack="true"></telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>


                            </td>
                            <td class="tdright">Priority:</td>
                            <td class="tdleft">
                                <telerik:RadDropDownList ID="ddlPriority" Skin="Default" Width="100px" runat="server" CausesValidation="false"
                                    DefaultMessage="Any Priority" AutoPostBack="true" SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId">
                                </telerik:RadDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">
                                <asp:LinkButton runat="server" ID="btnApplyFilters" CssClass="normalButton" Text="Apply Filters" AutoPostBack="true"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnResetFilters" CssClass="normalButton" Text="Reset Filters" AutoPostBack="true" OnClick="btnResetFilters_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlReport">
                    <p></p>
                                <telerik:RadGrid runat="server" ID="gvReport" AllowSorting="True" AllowPaging="True" GridLines="Both"
                                        AutoGenerateColumns="False" AutoGenerateEditColumn="False" ShowStatusBar="true" ShowFooter="true"
                                        OnUpdateCommand="gvReport_UpdateCommand" OnNeedDataSource="gvReport_NeedDataSource" 
                                        OnItemDataBound="gvReport_ItemDataBound" OnItemCreated="gvReport_ItemCreated" 
                                        OnPreRender="gvReport_PreRender" UpdateMethod="UpdateReport"
                                        AllowAutomaticUpdates="false" AllowAutomaticInserts="false" OnItemCommand="gvReport_ItemCommand">
                                        <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" AllowColumnHide="false" AllowExpandCollapse="false"
	                                        Resizing-AllowColumnResize="false" AllowGroupExpandCollapse="false" AllowRowsDragDrop="false">
	                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <ExportSettings Excel-Format="Html" ExportOnlyData="true" IgnorePaging="true" Pdf-AllowModify="false" Pdf-AllowPrinting="true" Pdf-AllowCopy="true" OpenInNewWindow="true"
	                                        Csv-ColumnDelimiter="Comma" Csv-FileExtension="csv" Csv-EncloseDataWithQuotes="true" Csv-RowDelimiter="NewLine"
	                                        HideStructureColumns="true" FileName="PendingApprovalInventory">
                                        </ExportSettings>
                                        <MasterTableView SelectMethod="GetReportData" AllowFilteringByColumn="false" AllowSorting="true" ItemType="Tingle_WebForms.Models.OrderCancellationForm"
                                            AllowPaging="true" PageSize="25" EditMode="PopUp" CommandItemDisplay="Bottom"
	                                        DataKeyNames="RecordId" ShowHeadersWhenNoRecords="true" NoDetailRecordsText="No Records to Display" NoMasterRecordsText="No Records to Display">
	                                        <EditFormSettings UserControlName="OrderCancellationDetails.ascx" EditFormType="WebUserControl">
                                                <PopUpSettings Modal="true" />
                                            </EditFormSettings>
	                                        <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" ShowAddNewRecordButton="false" />
	                                        <Columns>
                                                <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
			                                        UniqueName="RecordId" HeaderText="RecordId" Visible="false">
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Timestamp" DataType="System.DateTime" Exportable="true" Groupable="False" SortExpression="Timestamp"
			                                        UniqueName="Timestamp" HeaderText="Submission Date" AllowFiltering="false" ReadOnly="true">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblTimestamp" runat="server" Text='<%# ((DateTime)Eval("Timestamp")).ToString("MM/dd/yyyy") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="RequestedUser.DisplayName" DataType="System.String" Exportable="true" Groupable="False" SortExpression=""
			                                        UniqueName="RequestedUser.DisplayName" HeaderText="Requested By" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblRequestedByUser" runat="server" Text='<%# Eval("RequestedUser.DisplayName") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Customer" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Customer"
			                                        UniqueName="Customer" HeaderText="Customer" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("Customer") %>'></asp:Label>
                                                        <telerik:RadToolTip runat="server" ID="ttCustomer" Position="TopRight" RelativeTo="Element" TargetControlID="lblCustomer" ShowEvent="OnMouseOver"
                                                            Animation="Resize" RenderInPageRoot="false" AnimationDuration="500" HideEvent="LeaveTargetAndToolTip" HideDelay="100" AutoCloseDelay="20000">
                                                        </telerik:RadToolTip>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="OrderNumber" DataType="System.String" Exportable="true" Groupable="False" SortExpression="OrderNumber"
			                                        UniqueName="OrderNumber" HeaderText="Order Number" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblOrderNumber" runat="server" Text='<%# Eval("OrderNumber") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="ArmstrongReference" DataType="System.String" Exportable="true" Groupable="False" SortExpression="ArmstrongReference"
			                                        UniqueName="ArmstrongReference" HeaderText="Armstrong Reference" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemStyle HorizontalAlign="Center" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblArmstrongReference" runat="server" Text='<%# Eval("ArmstrongReference") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="POStatusList" DataType="System.String" Exportable="true" Groupable="False" SortExpression="POStatusList"
			                                        UniqueName="POStatusList" HeaderText="PO Status" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
			                                        <ItemStyle HorizontalAlign="Center" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblPOStatusList" runat="server" Text='<%# Eval("POStatusList") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="" DataType="System.String" Exportable="true" Groupable="False" SortExpression=""
			                                        UniqueName="AssignedUser" HeaderText="Assigned User" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblAssignedUser" runat="server" Text='<%#: Eval("AssignedUser") != null ? Eval("AssignedUser.DisplayName") : "Unassigned" %>' ToolTip='<%#: Eval("AssignedUser") != null ? Eval("AssignedUser.EmailAddress") : "Unassigned" %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Status.StatusText" DataType="System.String" Exportable="true" Groupable="False" SortExpression=""
			                                        UniqueName="Status.StatusText" HeaderText="Status" AllowFiltering="false">
			                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" />
			                                        <ItemTemplate>
				                                        <asp:Label ID="lblStatus" runat="server" Text='<%#: Eval("Status.StatusText") %>'></asp:Label>
			                                        </ItemTemplate>
		                                        </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderText="Details">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
			                                        <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridEditCommandColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="OrderCancellationDetails.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <ClientEvents OnRowDblClick="RowDblClick" OnPopUpShowing="onPopUpShowing" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                              
                </asp:Panel>

                <%--<asp:Panel ID="pnlDetails" runat="server" Visible="false">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" HeaderText="There are errors in the form:" Font-Size="14px" ForeColor="Red" />
                    <asp:FormView ID="fvReport" runat="server" ItemType="Tingle_WebForms.Models.OrderCancellationForm" SelectMethod="GetFormDetails" OnDataBinding="fvReport_DataBinding"
                        BackColor="White" BorderStyle="None" DataKeyNames="RecordId" DefaultMode="Edit" UpdateMethod="UpdateForm" OnDataBound="fvReport_DataBound" OnPreRender="fvReport_PreRender"
                        OnItemUpdating="fvReport_ItemUpdating">
                        <EditItemTemplate>
                            <asp:PlaceHolder runat="server" ID="phFVDetails">
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <table>
                                    <tr class="trheader">
                                        <td colspan="4" class="tdheader">Order Cancellation Request Details</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="tdcenter">Form ID:
                                <asp:Label ID="lblRecordId" runat="server" Text='<%#: Eval("RecordId") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdright">Company:</td>
                                        <td class="tdleft">
                                            <asp:Label runat="server" ID="lblCompanyEdit" Visible="false" Text='<%# Bind("Company") %>'></asp:Label>
                                            <telerik:RadDropDownList runat="server" ID="ddlCompanyEdit" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="ddlCompanyEdit_SelectedIndexChanged"
                                                Width="75px" Skin="Default" OnDataBinding="ddlCompanyEdit_DataBinding">
                                                <Items>
                                                    <telerik:DropDownListItem Text="Tingle" Value="Tingle" />
                                                    <telerik:DropDownListItem Text="Summit" Value="Summit" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td class="tdright"></td>
                                        <td class="tdleft"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdright">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Order # field is required."
                                                ControlToValidate="txtOrderNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Order #:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtOrderNumberEdit" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:TextBox><br />

                                        </td>
                                        <td class="tdright">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Armstrong Reference field is required."
                                                ControlToValidate="txtArmstrongReferenceEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Armstrong Reference:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtArmstrongReferenceEdit" runat="server" Text='<%# Bind("ArmstrongReference") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdright">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Customer field is required."
                                                ControlToValidate="txtCustomerEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Customer:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtCustomerEdit" runat="server" Text='<%# Bind("Customer") %>'></asp:TextBox></td>
                                        <td class="tdright">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="PO field is required."
                                                ControlToValidate="txtPOEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>PO:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtPOEdit" runat="server" Text='<%# Bind("PO") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="tdright">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="SKU field is required."
                                                ControlToValidate="txtSKUEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>SKU:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtSKUEdit" runat="server" Text='<%# Bind("SKU") %>'></asp:TextBox></td>
                                        <td class="tdright">Status of PO:</td>
                                        <td class="tdleft">
                                            <telerik:RadComboBox runat="server" ID="ddlPOStatusEdit" DataTextField="StatusCode" DataValueField="RecordId" Width="150px"
                                                CheckBoxes="true" DropDownWidth="150px" ItemType="Tingle_WebForms.Models.PurchaseOrderStatus" Skin="Default" SelectMethod="GetPOStatuses"
                                                EnableCheckAllItemsCheckBox="true" EmptyMessage="Type to Filter" AllowCustomText="true" Filter="Contains" MarkFirstMatch="true"
                                                OnClientItemChecked="ClearBox" OnClientBlur="ComboBoxBlurPO" OnClientFocus="ComboBoxFocus" Localization-CheckAllString="Select all Statuses"
                                                CausesValidation="false" OnDataBound="ddlStatusOfPOEdit_DataBound">
                                                <ItemTemplate>
                                                    <%# Eval("StatusCode") %> -- <%# Eval("Status") %>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdright">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Line field is required."
                                                ControlToValidate="txtLineEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Line:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtLineEdit" runat="server" Text='<%# Bind("Line") %>'></asp:TextBox></td>
                                        <td class="tdright">Line of PO:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtLineOfPOEdit" runat="server" Text='<%# Bind("LineOfPO") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="tdright">Size:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtSizeEdit" runat="server" Text='<%# Bind("Size") %>'></asp:TextBox></td>
                                        <td class="tdright"></td>
                                        <td class="tdleft"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdright">Date Required:</td>
                                        <td class="tdleft">
                                            <input type="text" id="txtDateRequiredEdit" runat="server" value='<%# ((DateTime)Eval("DateRequired")).ToShortDateString() %>' /></td>
                                        <td class="tdright">Ship Via:</td>
                                        <td class="tdleft">
                                            <asp:TextBox ID="txtShipViaEdit" runat="server" Text='<%# Bind("ShipVia") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    </tr>
                        <tr>
                            <td class="tdright">Serial:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtSerialEdit" runat="server" Text='<%# Bind("Serial") %>'></asp:TextBox></td>
                            <td class="tdright">Truck Route:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtTruckRouteEdit" runat="server" Text='<%# Bind("TruckRoute") %>'></asp:TextBox></td>
                        </tr>
                                    <tr>
                                        <td style="width: 100%;" colspan="4">
                                            <div style="width: 80%; border: 1px solid black; border-radius: 5px; margin: 0 auto; height: 90px;">
                                                <table style="border: none;">
                                                    <tr>
                                                        <td colspan="6">
                                                            <span style="font-weight: bold; color: #bc4445; text-decoration: underline">Assignment and Request Details:</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%; text-align: right"><span class="formRedText">Requested By:</span></td>
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
                                                        <td style="width: 18%; text-align: right"><span class="formRedText">Due By:</span></td>
                                                        <td style="width: 18%; text-align: left">
                                                            <input type="text" id="txtDueByDateEdit" runat="server" style="width: 75px;"
                                                                value='<%# Eval("DueDate") != null ? ((DateTime)Eval("DueDate")).ToString("MM/dd/yyyy") : "" %>' /></td>
                                                        <td style="width: 10%; text-align: right"><span class="formRedText">Status:</span></td>
                                                        <td style="width: 10%; text-align: left">
                                                            <telerik:RadDropDownList ID="ddlStatusEdit" runat="server" SelectMethod="GetStatusesEdit" OnDataBound="ddlStatusEdit_DataBound"
                                                                AutoPostBack="false" DataTextField="StatusText" DataValueField="StatusID" Skin="Default" Width="100px">
                                                            </telerik:RadDropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%; text-align: right"><span class="formRedText">Assigned To:</span></td>
                                                        <td style="width: 25%; text-align: left">
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
                                                        <td style="width: 18%; text-align: right"><span class="formRedText">Date Created:</span></td>
                                                        <td style="width: 18%; text-align: left">
                                                            <asp:Label runat="server" ID="lblDateCreatedEdit" Text='<%# ((DateTime)Eval("Timestamp")).ToShortDateString() %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%; text-align: right"><span class="formRedText">Priority:</span></td>
                                                        <td style="width: 10%; text-align: left">
                                                            <telerik:RadDropDownList ID="ddlPriorityEdit" Skin="Default" Width="100px" runat="server" CausesValidation="false"
                                                                SelectMethod="GetPriorities" DataTextField="PriorityText" DataValueField="RecordId" AutoPostBack="false" OnDataBound="ddlPriorityEdit_DataBound">
                                                            </telerik:RadDropDownList>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="width: 80%; border: 1px solid black; border-radius: 5px; margin: 0 auto; height: 90px;">
                                                <table style="border: none;">
                                                    <tr>
                                                        <td colspan="6">
                                                            <span style="font-weight: bold; color: #bc4445; text-decoration: underline">Notification Details:</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%; text-align: left; vertical-align: middle">
                                                            <asp:CheckBox runat="server" ID="cbNotifyStandard" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyStandard_CheckedChanged" />
                                                            <asp:Label runat="server" ID="lblNotifyStandard" Text="Notify Standard" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td style="width: 28%; text-align: left">
                                                            <asp:Label runat="server" ID="lblNotifyStandardValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 17%; text-align: left">
                                                            <asp:CheckBox runat="server" ID="cbNotifyAssignee" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyAssignee_CheckedChanged" />
                                                            <asp:Label runat="server" ID="lblNotifyAssignee" Text="Notify Assignee" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td style="width: 18%; text-align: left">
                                                            <asp:Label runat="server" ID="lblNotifyAssigneeValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
                                                        </td>
                                                        <td rowspan="2" style="width: 5%; text-align: right">
                                                            <asp:CheckBox runat="server" ID="cbSendComments" />
                                                        </td>
                                                        <td rowspan="2" style="width: 15%; text-align: center; font-size: 12px">Include All Comments in Notification
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%; text-align: left; vertical-align: middle">
                                                            <asp:CheckBox runat="server" ID="cbNotifyOther" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyOther_CheckedChanged" />
                                                            <asp:Label runat="server" ID="lblNotifyOther" Text="Notify Other" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td style="width: 28%; text-align: left; vertical-align: middle">
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
                                                        <td style="width: 17%; text-align: left">
                                                            <asp:CheckBox runat="server" ID="cbNotifyRequester" Text="" Style="vertical-align: middle" AutoPostBack="true" OnCheckedChanged="cbNotifyRequester_CheckedChanged" />
                                                            <asp:Label runat="server" ID="lblNotifyRequester" Text="Notify Requester" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td style="width: 18%; text-align: left">
                                                            <asp:Label runat="server" ID="lblNotifyRequesterValue" Text="" Font-Size="12px" ForeColor="#bb4342" Font-Italic="true"></asp:Label>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;" colspan="4">
                                            <div style="width: 80%; border: 0; margin: 0 auto; display: none; text-align: center" id="divAddNewEmail">
                                                <table style="border: 1px solid #000; border-radius: 5px;">
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
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Back" CssClass="normalButton" OnClick="btnCancel_Click" />
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
                                                <asp:Label runat="server" ID="lblCreatedBy" Style="font-size: 12px; color: black; font-weight: normal" Text='<%# Eval("SubmittedUser.DisplayName") %>'></asp:Label><br />
                                                <br />
                                                <span style="font-weight: bold; color: #bc4445; font-size: 12px">Last Updated By:</span><br />
                                                <asp:Label runat="server" ID="lblLastUpdatedBy" Style="font-size: 12px; color: black; font-weight: normal" Text='<%# Eval("LastModifiedUser.DisplayName") %>'></asp:Label><br />
                                                <br />
                                                <span style="font-weight: bold; color: #bc4445; font-size: 12px">Last Update:</span><br />
                                                <asp:Label runat="server" ID="lblLastUpdated" Style="font-size: 12px; color: black; font-weight: normal" Text='<%# ((DateTime)Eval("LastModifiedTimestamp")).ToShortDateString() %>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 100%; padding-top: 10px">
                                    <telerik:RadTextBox runat="server" ID="txtNewComment" TextMode="MultiLine" Width="81%" Style="text-align: left; margin: 0 auto; padding: 3px; border: 1px solid black"
                                        ValidationGroup="NewComment" MaxLength="300" Height="60px">
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
                            </asp:PlaceHolder>
                        </EditItemTemplate>
                    </asp:FormView>

                </asp:Panel>--%>
            </div>
</asp:Content>
