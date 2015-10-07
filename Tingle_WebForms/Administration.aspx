<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="Tingle_WebForms.Administration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ConfirmDelete() {
            var x;
            if (confirm("Are you sure you want to delete this item?") == true) {
                return true;
            } else {
                return false;
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <table>
                <tr class="trheader">
                    <td>
                        <table>
                            <tr>
                                <td class="tdheader" style="width: 25%; text-align: center;">
                                    <div style="float: left; width: 25%">
                                        <asp:DropDownList ID="ddlFormName" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                            ItemType="Tingle_WebForms.Models.TForm" SelectMethod="SelectForms" DataTextField="FormName" DataValueField="FormID"
                                            OnSelectedIndexChanged="ddlFormName_SelectedIndexChanged"
                                            Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                            <asp:ListItem Value="0" Selected="True">Select Form</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 50%">
                                        Form Administration
                                    </div>
                                    <div style="float: left; width: 25%">
                                        <asp:DropDownList ID="ddlTab" runat="server" OnSelectedIndexChanged="ddlTab_SelectedIndexChanged" AutoPostBack="true"
                                            Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                            <asp:ListItem Selected="True" Value="FormSummary">Form Summary</asp:ListItem>
                                            <asp:ListItem Value="EmailList">Email List</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:Panel ID="pnlAdministration" runat="server" Visible="true">
                    <tr>
                        <td class="tdcenter EmailLabels">
                            <table style="border-style: none;">
                                <asp:Panel ID="pnlFormPopup" Visible="false" runat="server">
                                    <tr>
                                        <td colspan="2">
                                            <div style="width: 100%; text-align: center;">
                                                <br />
                                                <table style="border: 2px inset #000; width: 50%; margin-left: auto; margin-right: auto;">
                                                    <tr>
                                                        <td>Form ID:</td>
                                                        <td>
                                                            <asp:Label ID="lblFormId" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Form Name:</td>
                                                        <td>
                                                            <asp:Label ID="lblFormName" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Created By:</td>
                                                        <td>
                                                            <asp:Label ID="lblFormCreator" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Created On:</td>
                                                        <td>
                                                            <asp:Label ID="lblTimestamp" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Notes:</td>
                                                        <td>
                                                            <asp:Label ID="lblNotes" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pnlEmailList" runat="server" Visible="false">
                                    <tr>
                                        <td class="tdcenter" colspan="2">

                                            <asp:GridView ID="gvEmailList" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.EmailAddress"
                                                SelectMethod="gvEmailList_GetData" DataKeyNames="EmailAddressID" AutoGenerateColumns="false"
                                                CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" DeleteMethod="gvEmailList_DeleteItem" UpdateMethod="gvEmailList_UpdateItem"
                                                OnRowUpdating="gvEmailList_RowUpdating" OnRowDataBound="gvEmailList_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmailAddressID" Width="1px" runat="server" Visible="false" Text='<%#: Eval("EmailAddressID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblEmailAddressIDEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("EmailAddressID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNameEdit" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblCompanyEdit" runat="server" Text='<%# Bind("Company") %>' Visible="false"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlCompanyEdit" Style="border-style: inset;">
                                                                <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                                                <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Item.Status == 0 ? "Disabled" : "Enabled" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:RadioButtonList ID="rblStatusEdit" runat="server" RepeatDirection="Horizontal" OnDataBound="rblStatusEdit_DataBound"
                                                                BorderStyle="None">
                                                                <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                                <asp:ListItem Value="0">Disabled</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit"></asp:CommandField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbDeleteEmail" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this Email Address?')" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>Fill out the form below to add the first Email Address.</EmptyDataTemplate>
                                                <FooterStyle BackColor="white" ForeColor="Black" />
                                                <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>
                                            <br />
                                            <br />
                                            <asp:FormView ID="fvEmailInsert" runat="server" ItemType="Tingle_WebForms.Models.EmailAddress"
                                                DataKeyNames="EmailAddressID" AutoGenerateColumns="false" InsertMethod="fvEmailInsert_InsertItem"
                                                DefaultMode="Insert" BorderStyle="None">
                                                <InsertItemTemplate>
                                                    <div style="width: 100%; text-align: center;">
                                                        <table style="border: 2px inset #000;">
                                                            <tr>
                                                                <td>Recipient's Name:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNameInsert" runat="server" Text='<%#: Bind("Name") %>'></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Email Address:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAddressInsert" runat="server" Text='<%#: Bind("Address") %>'></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Company:</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblCompanyInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                                        <asp:ListItem Value="Tingle" Selected="True">Tingle</asp:ListItem>
                                                                        <asp:ListItem Value="Summit">Summit</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Status:</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblStatusInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                                        <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                                        <asp:ListItem Value="0">Disabled</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="btnInsertAddress" runat="server" Text="Add Email" CommandName="Insert" />
                                                        <asp:Button ID="btnClearForm" runat="server" Text="Clear Form" OnClick="btnClearForm_Click" />
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>

                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblEmailMessage" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br /><br /><br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table>
                <tr class="trheader">
                    <td>
                        <table>
                            <tr>
                                <td class="tdheader" style="width: 100%; text-align: center;">Form Permissions</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="1">
                            <tr>
                                <td style="width:40%"></td>
                                <td style="width:15%">User</td>
                                <td style="width:15%">Reports User</td>
                                <td style="width:15%">Reports Admin</td>
                                <td style="width:15%">Super User</td>
                            </tr>
                            <tr>
                                <td style="width:40%">Order Cancellation</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbOrderCancellationUser" Text="" OnCheckedChanged="cbOrderCancellationUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbOrderCancellationReportsUser" Text="" OnCheckedChanged="cbOrderCancellationReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbOrderCancellationReportsAdmin" Text="" OnCheckedChanged="cbOrderCancellationReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbOrderCancellationSuperUser" Text="" OnCheckedChanged="cbOrderCancellationSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Expedited Order</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbExpeditedOrderUser" Text="" OnCheckedChanged="cbExpeditedOrderUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbExpeditedOrderReportsUser" Text="" OnCheckedChanged="cbExpeditedOrderReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbExpeditedOrderReportsAdmin" Text="" OnCheckedChanged="cbExpeditedOrderReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbExpeditedOrderSuperUser" Text="" OnCheckedChanged="cbExpeditedOrderSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Price Change Request</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbPriceChangeRequestUser" Text="" OnCheckedChanged="cbPriceChangeRequestUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbPriceChangeRequestReportsUser" Text="" OnCheckedChanged="cbPriceChangeRequestReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbPriceChangeRequestReportsAdmin" Text="" OnCheckedChanged="cbPriceChangeRequestReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbPriceChangeRequestSuperUser" Text="" OnCheckedChanged="cbPriceChangeRequestSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Hot Rush</td>                
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbHotRushUser" Text="" OnCheckedChanged="cbHotRushUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbHotRushReportsUser" Text="" OnCheckedChanged="cbHotRushReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbHotRushReportsAdmin" Text="" OnCheckedChanged="cbHotRushReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbHotRushSuperUser" Text="" OnCheckedChanged="cbHotRushSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Low Inventory</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbLowInventoryUser" Text="" OnCheckedChanged="cbLowInventoryUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbLowInventoryReportsUser" Text="" OnCheckedChanged="cbLowInventoryReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbLowInventoryReportsAdmin" Text="" OnCheckedChanged="cbLowInventoryReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbLowInventorySuperUser" Text="" OnCheckedChanged="cbLowInventorySuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Sample Request</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbSampleRequestUser" Text="" OnCheckedChanged="cbSampleRequestUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbSampleRequestReportsUser" Text="" OnCheckedChanged="cbSampleRequestReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbSampleRequestReportsAdmin" Text="" OnCheckedChanged="cbSampleRequestReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbSampleRequestSuperUser" Text="" OnCheckedChanged="cbSampleRequestSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Direct Order</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbDirectOrderUser" Text="" OnCheckedChanged="cbDirectOrderUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbDirectOrderReportsUser" Text="" OnCheckedChanged="cbDirectOrderReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbDirectOrderReportsAdmin" Text="" OnCheckedChanged="cbDirectOrderReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbDirectOrderSuperUser" Text="" OnCheckedChanged="cbDirectOrderSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Request For Check</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbRequestForCheckUser" Text="" OnCheckedChanged="cbRequestForCheckUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbRequestForCheckReportsUser" Text="" OnCheckedChanged="cbRequestForCheckReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbRequestForCheckReportsAdmin" Text="" OnCheckedChanged="cbRequestForCheckReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbRequestForCheckSuperUser" Text="" OnCheckedChanged="cbRequestForCheckSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Must Include</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbMustIncludeUser" Text="" OnCheckedChanged="cbMustIncludeUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbMustIncludeReportsUser" Text="" OnCheckedChanged="cbMustIncludeReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbMustIncludeReportsAdmin" Text="" OnCheckedChanged="cbMustIncludeReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbMustIncludeSuperUser" Text="" OnCheckedChanged="cbMustIncludeSuperUser_CheckedChanged" /></td>
                            </tr>
                            <tr>
                                <td style="width:40%">Cannot Wait For Container</td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbCannotWaitForContainerUser" Text="" OnCheckedChanged="cbCannotWaitForContainerUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbCannotWaitForContainerReportsUser" Text="" OnCheckedChanged="cbCannotWaitForContainerReportsUser_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbCannotWaitForContainerReportsAdmin" Text="" OnCheckedChanged="cbCannotWaitForContainerReportsAdmin_CheckedChanged" /></td>
                                <td style="width:15%"><asp:CheckBox runat="server" AutoPostBack="true" ID="cbCannotWaitForContainerSuperUser" Text="" OnCheckedChanged="cbCannotWaitForContainerSuperUser_CheckedChanged" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label runat="server" ID="lblFormPermissions"></asp:Label></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br /><br /><br />
    <asp:UpdatePanel runat="server" ID="up2">
        <ContentTemplate>
            <table>
                <tr class="trheader">
                    <td>
                        <table>
                            <tr>
                                <td class="tdheader" style="width: 100%; text-align: center;">User Administration</td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvUsers" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.SystemUsers"
                            SelectMethod="gvUsers_GetData" DataKeyNames="SystemUserID" AutoGenerateColumns="false"
                            CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC"
                            BorderStyle="None" BorderWidth="1px" DeleteMethod="gvUsers_DeleteItem" UpdateMethod="gvUsers_UpdateItem"
                            OnRowUpdating="gvUsers_RowUpdating" OnRowDataBound="gvUsers_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSystemUserID" Width="1px" runat="server" Visible="false" Text='<%#: Eval("SystemUserID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblSystemUserIDEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("SystemUserID") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblUserNameEdit" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DisplayName" SortExpression="DisplayName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDisplayName" runat="server" Text='<%# Bind("DisplayName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDisplayNameEdit" runat="server" Width="150" MaxLength="50" Text='<%# Bind("DisplayName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" SortExpression="EmailAddress">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEmailAddressEdit" runat="server" Width="150" MaxLength="100" Text='<%# Bind("EmailAddress") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Item.Status == 0 ? "Disabled" : "Enabled" %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:RadioButtonList ID="rblUserStatusEdit" runat="server" RepeatDirection="Vertical" OnDataBound="rblUserStatusEdit_DataBound"
                                            BorderStyle="None">
                                            <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                            <asp:ListItem Value="0">Disabled</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserRole" runat="server" Text='<%# Item.UserRole.RoleName %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlUserRoleEdit" runat="server" SelectMethod="GetUserRoles"
                                            DataTextField="RoleName" DataValueField="UserRoleId" AppendDataBoundItems="true"
                                            Style="border-style: inset;" OnDataBound="ddlUserRoleEdit_DataBound">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory Mgmt.">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbInventoryManagement" Enabled="false" Checked='<%# Eval("InventoryApprovalUser") != null && Convert.ToBoolean(Eval("InventoryApprovalUser")) == true ? true : false %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbInventoryManagementEdit" Checked='<%# Eval("InventoryApprovalUser") != null && Convert.ToBoolean(Eval("InventoryApprovalUser")) == true ? true : false %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit" ItemStyle-VerticalAlign="Middle"></asp:CommandField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbDeleteUser" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this User?')" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>Fill out the form below to add the first System User.</EmptyDataTemplate>
                            <FooterStyle BackColor="white" ForeColor="Black" />
                            <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                        <br />
                        <br />
                        <asp:FormView ID="fvUserInsert" runat="server" ItemType="Tingle_WebForms.Models.SystemUsers"
                            DataKeyNames="SystemUserID" AutoGenerateColumns="false" InsertMethod="fvUserInsert_InsertItem"
                            DefaultMode="Insert" BorderStyle="None">
                            <InsertItemTemplate>
                                <div style="width: 100%; text-align: center;">
                                    <table style="border: 2px inset #000;">
                                        <tr>
                                            <td colspan="2">UserName:</td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtUserNameInsert" runat="server" Text='<%#: Bind("UserName") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">Display Name:</td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtDisplayNameInsert" runat="server" Text='<%#: Bind("DisplayName") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">Email Address:</td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtEmailAddressInsert" runat="server" Text='<%#: Bind("EmailAddress") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">Status:</td>
                                            <td colspan="2">
                                                <asp:RadioButtonList ID="rblUserStatusInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                    <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                    <asp:ListItem Value="0">Disabled</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Role:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlUserRoleInsert" runat="server" SelectMethod="GetUserRoles"
                                                    DataTextField="RoleName" DataValueField="UserRoleId" AppendDataBoundItems="true"
                                                    Style="border-style: inset;">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Inventory Approval:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="cbInventoryApproval" Checked="false" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <asp:Button ID="btnInsertUser" runat="server" Text="Add User" CommandName="Insert" />
                                    <asp:Button ID="btnClearUserForm" runat="server" Text="Clear Form" OnClick="btnClearUserForm_Click" />
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblUserMessage" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br /><br /><br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table>
                <tr class="trheader">
                    <td>
                        <table>
                            <tr>
                                <td class="tdheader" style="width: 100%; text-align: center;">
                                        Email Administration
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvNotificationEmails" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.NotificationEmailAddress"
                            SelectMethod="gvNotificationEmails_GetData" DataKeyNames="RecordId" AutoGenerateColumns="false"
                            CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC"
                            BorderStyle="None" BorderWidth="1px" DeleteMethod="gvNotificationEmails_DeleteItem" UpdateMethod="gvNotificationEmails_UpdateItem"
                            OnRowUpdating="gvNotificationEmails_RowUpdating" OnRowDataBound="gvNotificationEmails_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecorId" Width="1px" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblRecorIdEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNameEdit" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTxtNameEdit" runat="server" ErrorMessage="*" Text="*" ForeColor="Red"  ValidationGroup="NotificationEmailEdit"
                                            ControlToValidate="txtNameEdit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTxtAddressEdit" runat="server" ErrorMessage="*" Text="*" ForeColor="Red"  ValidationGroup="NotificationEmailEdit"
                                            ControlToValidate="txtAddressEdit"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Item.Status == 0 ? "Disabled" : "Enabled" %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:RadioButtonList ID="rblNotificationEmailStatusEdit" runat="server" RepeatDirection="Horizontal" OnDataBound="rblNotificationEmailStatusEdit_DataBound"
                                            BorderStyle="None">
                                            <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                            <asp:ListItem Value="0">Disabled</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" EditText="Edit" ValidationGroup="NotificationEmailEdit" ShowHeader="True" HeaderText="Edit"></asp:CommandField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbDeleteAddress" runat="server" Text="Delete" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this Email Address?')" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>Fill out the form below to add the first Notification Email Address.</EmptyDataTemplate>
                            <FooterStyle BackColor="white" ForeColor="Black" />
                            <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                        <br />
                        <br />
                        <asp:FormView ID="fvNotificationEmails" runat="server" ItemType="Tingle_WebForms.Models.NotificationEmailAddress"
                            DataKeyNames="RecordId" AutoGenerateColumns="false" InsertMethod="fvNotificationEmails_InsertItem"
                            DefaultMode="Insert" BorderStyle="None">
                            <InsertItemTemplate>
                                <div style="width: 100%; text-align: center;">
                                    <table style="border: 2px inset #000;">
                                        <tr>
                                            <td>Name:</td>
                                            <td>
                                                <asp:TextBox ID="txtNameInsert" runat="server" Text='<%#: Bind("Name") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTxtNameInsert" runat="server" ErrorMessage="*" Text="*" ForeColor="Red"  ValidationGroup="NotificationEmailInsert"
                                                    ControlToValidate="txtNameInsert"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Address:</td>
                                            <td>
                                                <asp:TextBox ID="txtAddressInsert" runat="server" Text='<%#: Bind("Address") %>'></asp:TextBox>
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
                                    </table>
                                    <br />
                                    <br />
                                    <asp:Button ID="btnInsertEmail" runat="server" Text="Add Email" CommandName="Insert" ValidationGroup="NotificationEmailInsert" />
                                    <asp:Button ID="btnClearNotificationEmailForm" runat="server" Text="Clear Form" CausesValidation="false" OnClick="btnClearNotificationEmailForm_Click" />
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblNotificationEmailMessage" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="up3">
        <ContentTemplate>
            <table>
                <tr class="trheader">
                    <td>
                        <table>
                            <tr>
                                <td class="tdheader" style="width: 25%; text-align: center;">
                                    <div style="float: left; width: 25%">
                                        <asp:DropDownList runat="server" ID="ddlVariable" AutoPostBack="true" OnSelectedIndexChanged="ddlVariable_SelectedIndexChanged"
                                            Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                            <asp:ListItem Text="Expedite Code" Value="E" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="PO Status" Value="P"></asp:ListItem>
                                            <asp:ListItem Text="Vendors" Value="V"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 50%">
                                        Variable Administration
                                    </div>
                                    <div style="float: left; width: 25%">
                                        &nbsp;
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlExpediteCodes" Visible="true">
                            <asp:GridView ID="gvExpediteCodes" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.ExpediteCode"
                                SelectMethod="gvExpediteCodes_GetData" DataKeyNames="ExpediteCodeID" AutoGenerateColumns="false"
                                CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" DeleteMethod="gvExpediteCodes_DeleteItem" UpdateMethod="gvExpediteCodes_UpdateItem"
                                OnRowUpdating="gvExpediteCodes_RowUpdating" OnRowDataBound="gvExpediteCodes_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpediteCodeID" Width="1px" runat="server" Visible="false" Text='<%#: Eval("ExpediteCodeID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblExpediteCodeIDEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("ExpediteCodeID") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" SortExpression="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCodeEdit" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescriptionEdit" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Item.Status == 0 ? "Disabled" : "Enabled" %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:RadioButtonList ID="rblCodeStatusEdit" runat="server" RepeatDirection="Horizontal" OnDataBound="rblCodeStatusEdit_DataBound"
                                                BorderStyle="None">
                                                <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                <asp:ListItem Value="0">Disabled</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit"></asp:CommandField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDeleteCode" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this Expedite Code?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>Fill out the form below to add the first Expedite Code.</EmptyDataTemplate>
                                <FooterStyle BackColor="white" ForeColor="Black" />
                                <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                            <br />
                            <br />
                            <asp:FormView ID="fvExpediteCodes" runat="server" ItemType="Tingle_WebForms.Models.ExpediteCode"
                                DataKeyNames="ExpediteCodeID" AutoGenerateColumns="false" InsertMethod="fvExpediteCodes_InsertItem"
                                DefaultMode="Insert" BorderStyle="None">
                                <InsertItemTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        <table style="border: 2px inset #000;">
                                            <tr>
                                                <td>Expedite Code:</td>
                                                <td>
                                                    <asp:TextBox ID="txtCodeInsert" runat="server" Text='<%#: Bind("Code") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Description:</td>
                                                <td>
                                                    <asp:TextBox ID="txtDescriptionInsert" runat="server" Text='<%#: Bind("Description") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Status:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblCodeStatusInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                        <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                        <asp:ListItem Value="0">Disabled</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnInsertCode" runat="server" Text="Add Code" CommandName="Insert" />
                                        <asp:Button ID="btnClearCodeForm" runat="server" Text="Clear Form" OnClick="btnClearCodeForm_Click" />
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlPOStatuses" Visible="false">
                            <asp:GridView ID="gvPOStatus" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.PurchaseOrderStatus"
                                SelectMethod="gvPOStatus_GetData" DataKeyNames="RecordId" AutoGenerateColumns="false"
                                CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" DeleteMethod="gvPOStatus_DeleteItem" UpdateMethod="gvPOStatus_UpdateItem"
                                OnRowUpdating="gvPOStatus_RowUpdating" OnRowDataBound="gvPOStatus_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecordId" Width="1px" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRecordIdEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="lblStatusEdit" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="StatusCode" SortExpression="StatusCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusCode" runat="server" Text='<%# Bind("StatusCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="lblStatusCodeEdit" runat="server" Text='<%# Bind("StatusCode") %>' MaxLength="1"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit"></asp:CommandField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDeleteStatus" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this Purchase Order Status?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>Fill out the form below to add the first Purchase Order Status.</EmptyDataTemplate>
                                <FooterStyle BackColor="white" ForeColor="Black" />
                                <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                            <br />
                            <br />
                            <asp:FormView ID="fvPOStatus" runat="server" ItemType="Tingle_WebForms.Models.PurchaseOrderStatus"
                                DataKeyNames="RecordId" AutoGenerateColumns="false" InsertMethod="fvPOStatus_InsertItem"
                                DefaultMode="Insert" BorderStyle="None">
                                <InsertItemTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        <table style="border: 2px inset #000;">
                                            <tr>
                                                <td>Status:</td>
                                                <td>
                                                    <asp:TextBox ID="txtStatusInsert" runat="server" Text='<%#: Bind("Status") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Code:</td>
                                                <td>
                                                    <asp:TextBox ID="txtStatusCodeInsert" runat="server" Text='<%#: Bind("StatusCode") %>' MaxLength="1"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnInsertStatus" runat="server" Text="Add Status" CommandName="Insert" />
                                        <asp:Button ID="btnClearStatusForm" runat="server" Text="Clear Form" OnClick="btnClearStatusForm_Click" />
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlVendors" Visible="false">
                            <asp:GridView ID="gvVendors" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.Vendor"
                                SelectMethod="gvVendors_GetData" DataKeyNames="RecordId" AutoGenerateColumns="false"
                                CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" DeleteMethod="gvVendors_DeleteItem" UpdateMethod="gvVendors_UpdateItem"
                                OnRowUpdating="gvVendors_RowUpdating" OnRowDataBound="gvVendors_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecordId" Width="1px" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRecordIdEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("RecordId") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Name" SortExpression="VendorName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="lblVendorNameEdit" runat="server" Text='<%# Bind("VendorName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Phone" SortExpression="VendorPhone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorPhone" runat="server" Text='<%# Bind("VendorPhone") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="lblVendorPhoneEdit" runat="server" Text='<%# Bind("VendorPhone") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Fax" SortExpression="VendorFax">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorFax" runat="server" Text='<%# Bind("VendorFax") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="lblVendorFaxEdit" runat="server" Text='<%# Bind("VendorFax") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit"></asp:CommandField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDeleteStatus" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this Vendor?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>Fill out the form below to add the first Vendor.</EmptyDataTemplate>
                                <FooterStyle BackColor="white" ForeColor="Black" />
                                <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                            <br />
                            <br />
                            <asp:FormView ID="fvVendors" runat="server" ItemType="Tingle_WebForms.Models.Vendor"
                                DataKeyNames="RecordId" AutoGenerateColumns="false" InsertMethod="fvVendors_InsertItem"
                                DefaultMode="Insert" BorderStyle="None">
                                <InsertItemTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        <table style="border: 2px inset #000;">
                                            <tr>
                                                <td>Vendor Name:</td>
                                                <td>
                                                    <asp:TextBox ID="txtVendorNameInsert" runat="server" Text='<%#: Bind("VendorName") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Vendor Phone:</td>
                                                 <td>
                                                     <asp:TextBox ID="txtVendorPhoneInsert" runat="server" Text='<%#: Bind("VendorPhone") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Vendor Fax:</td>
                                                 <td>
                                                     <asp:TextBox ID="txtVendorFaxInsert" runat="server" Text='<%#: Bind("VendorFax") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnInsertVendor" runat="server" Text="Add Vendor" CommandName="Insert" />
                                        <asp:Button ID="btnClearVendorForm" runat="server" Text="Clear Form" OnClick="btnClearVendorForm_Click" />
                                    </div>
                                </InsertItemTemplate>
                            </asp:FormView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblVariableMessage" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
            <table>
                <tr class="trheader">
                    <td class="tdheader" style="text-align: center;">
                        Inventory Management Administration
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="float:left; width:100%; text-align:center;">
                            <p style="width:100%; text-align:center; font-weight:bold;">Standard Notification List</p>
                            <telerik:RadGrid runat="server" ID="gridInventoryNotificationList" AllowSorting="true" ItemType="Tingle_WebForms.Models.InventoryNotificationEmails" SelectMethod="gridInventoryNotificationList_GetData"
                                AutoGenerateColumns="false" CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="Solid" BorderWidth="1px" OnDeleteCommand="gridInventoryNotificationList_DeleteCommand" OnNeedDataSource="gridInventoryNotificationList_NeedDataSource"
                                OnUpdateCommand="gridInventoryNotificationList_UpdateCommand" OnItemDataBound="gridInventoryNotificationList_ItemDataBound" ShowStatusBar="true" Width="600px"
                                AllowAutomaticDeletes="true" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" InsertMethod="gridInventoryNotificationList_Insert" UpdateMethod="gridInventoryNotificationList_Update"
                                OnInsertCommand="gridInventoryNotificationList_InsertCommand" DeleteMethod="gridInventoryNotificationList_Delete" CssClass="AdminGridSmall">
                                <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" AllowColumnHide="false" AllowExpandCollapse="false"
	                                Resizing-AllowColumnResize="false" AllowGroupExpandCollapse="false" AllowRowsDragDrop="false">
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <MasterTableView SelectMethod="GetInventoryNotificationList" AllowFilteringByColumn="false" AllowPaging="true" PageSize="25" EditMode="InPlace" CommandItemDisplay="TopAndBottom"
	                                DataKeyNames="RecordId">
	                                <EditFormSettings EditFormType="AutoGenerated"></EditFormSettings>
	                                <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" ShowAddNewRecordButton="true" />
	                                <Columns>
		                                <telerik:GridTemplateColumn DataField="RecordId" DataType="System.Int32" Exportable="true" Groupable="False" ReadOnly="True" SortExpression="RecordId"
			                                UniqueName="RecordId" HeaderText="RecordId" Visible="false">
			                                <ItemTemplate>
				                                <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("RecordId") %>'></asp:Label>
			                                </ItemTemplate>
		                                </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Name" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Name"
			                                UniqueName="Name" HeaderText="Name" AllowFiltering="true">
			                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
			                                <ItemStyle HorizontalAlign="Center" />
			                                <ItemTemplate>
				                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
			                                </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox runat="server" ID="txtNameEdit" Text='<%# Eval("Name") %>' Width="150px" MaxLength="100"></telerik:RadTextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <telerik:RadTextBox runat="server" ID="txtNameInsert" Text="" EmptyMessage="Insert Name" Width="150px" MaxLength="100"></telerik:RadTextBox>
                                            </InsertItemTemplate>
		                                </telerik:GridTemplateColumn>
			                            <telerik:GridTemplateColumn DataField="Address" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Address"
			                                UniqueName="Address" HeaderText="Address" AllowFiltering="true">
			                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
			                                <ItemStyle HorizontalAlign="Center" />
			                                <ItemTemplate>
				                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
			                                </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox runat="server" ID="txtAddressEdit" Text='<%# Eval("Address") %>' Width="150px" MaxLength="100"></telerik:RadTextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <telerik:RadTextBox runat="server" ID="txtAddressInsert" Text="" EmptyMessage="Insert Address" Width="150px" MaxLength="100"></telerik:RadTextBox>
                                            </InsertItemTemplate>
		                                </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Status" DataType="System.String" Exportable="true" Groupable="False" SortExpression="Status"
			                                UniqueName="Status" HeaderText="Status" AllowFiltering="true">
			                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
			                                <ItemStyle HorizontalAlign="Center" />
			                                <ItemTemplate>
				                                <asp:Label ID="lblStatus" runat="server" Text='<%# (Int16)Eval("Status") == 0 ? "Disabled" : "Enabled" %>'></asp:Label>
			                                </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:RadioButtonList ID="rblStatusEdit" runat="server" RepeatDirection="Horizontal" OnDataBound="rblStatusEdit_DataBound1"
			                                        BorderStyle="None">
			                                        <asp:ListItem Value="1">Enabled</asp:ListItem>
			                                        <asp:ListItem Value="0">Disabled</asp:ListItem>
		                                        </asp:RadioButtonList>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:RadioButtonList ID="rblStatusInsert" runat="server" RepeatDirection="Vertical" BorderStyle="None">
			                                        <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
			                                        <asp:ListItem Value="0">Disabled</asp:ListItem>
		                                        </asp:RadioButtonList>
                                            </InsertItemTemplate>
		                                </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="LinkButton" CancelText="Cancel" UpdateText="Save" EditText="Edit">
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" ConfirmDialogType="Classic" ConfirmText="Are you sure you want to delete this Email Address?"
                                            ConfirmTitle="Delete Confirmation"></telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <p></p>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
