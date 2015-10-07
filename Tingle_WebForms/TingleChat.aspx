<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TingleChat.aspx.cs" Inherits="Tingle_WebForms.TingleChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Content/chat.css" />
    <script src="/Scripts/jquery-2.1.3.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.0.2.min.js"></script>
    <script src="/signalr/hubs"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>

        <telerik:RadNotification runat="server" ID="notify" Position="BottomRight" Width="200"
            Animation="Fade" AnimationDuration="300" AutoCloseDelay="5000">
        </telerik:RadNotification>

        <telerik:RadSplitter runat="server" Width="100%" Height="100%" Orientation="Horizontal" BorderSize="0">
            <telerik:RadPane runat="server" Scrolling="None">
                <telerik:RadSplitter runat="server" Width="100%" Height="100%" Orientation="Vertical">
                    <telerik:RadPane runat="server">
                        <asp:Panel runat="server" ID="messages" CssClass="messagePanel" ClientIDMode="Static" Width="100%" Height="100%"></asp:Panel>
                    </telerik:RadPane>
                    <telerik:RadSplitBar runat="server"></telerik:RadSplitBar>
                    <telerik:RadPane runat="server" Width="150" MinWidth="150" MaxWidth="300" CssClass="rightPanel">
                        <telerik:RadListView runat="server" ID="userList" Width="100%" Height="100%" ItemPlaceholderID="currentChatters"
                            SelectMethod="GetChatters" ItemType="Tingle_WebForms.Models.User">
                            <LayoutTemplate>
                                <h3>Current Chatters</h3>
                                <ul id="currentChatters">
                                    <asp:PlaceHolder runat="server" ID="currentChatters"></asp:PlaceHolder>
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li id="chat_<%#: Item.Name %>"><%#: Item.Name %></li>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
            <telerik:RadSplitBar runat="server"></telerik:RadSplitBar>
            <telerik:RadPane runat="server" Height="100" MinHeight="150" MaxHeight="300" Scrolling="None">
                <telerik:RadEditor runat="server" ID="editor" Width="100%" EditModes="Design"
                    OnClientLoad="Editor_OnLoad" AutoResizeHeight="true"
                    OnClientCommandExecuting="Editor_OnClientCommandExecuting">
                    <Tools>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="Bold" />
                            <telerik:EditorTool Name="Italic" />
                            <telerik:EditorTool Name="Underline" />
                            <telerik:EditorTool Name="StrikeThrough" />
                            <telerik:EditorSplitButton Name="Emoticons" Text="Emoticons set inline" ItemsPerRow="5" PopupWidth="170px" PopupHeight="117px">
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/1.gif' />" Value="/content/icons/1.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/2.gif' />" Value="/content/icons/2.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/3.gif' />" Value="/content/icons/3.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/4.gif' />" Value="/content/icons/4.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/5.gif' />" Value="/content/icons/5.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/6.gif' />" Value="/content/icons/6.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/7.gif' />" Value="/content/icons/7.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/8.gif' />" Value="/content/icons/8.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/9.gif' />" Value="/content/icons/9.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/10.gif' />" Value="/content/icons/10.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/11.gif' />" Value="/content/icons/11.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/12.gif' />" Value="/content/icons/12.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/13.gif' />" Value="/content/icons/13.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/14.gif' />" Value="/content/icons/14.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/15.gif' />" Value="/content/icons/15.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/16.gif' />" Value="/content/icons/16.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/17.gif' />" Value="/content/icons/17.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/18.gif' />" Value="/content/icons/18.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/19.gif' />" Value="/content/icons/19.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/20.gif' />" Value="/content/icons/20.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/21.gif' />" Value="/content/icons/21.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/22.gif' />" Value="/content/icons/22.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/23.gif' />" Value="/content/icons/23.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/24.gif' />" Value="/content/icons/24.gif" />
                                <telerik:EditorDropDownItem Name="<img src='/content/icons/25.gif' />" Value="/content/icons/25.gif" />
                            </telerik:EditorSplitButton>
                            <telerik:EditorTool Name="AjaxSpellCheck" ShortCut="F7" />
                        </telerik:EditorToolGroup>
                    </Tools>
                </telerik:RadEditor>
            </telerik:RadPane>
        </telerik:RadSplitter>

        <script type="text/javascript"><!--

    function Editor_OnClientCommandExecuting(editor, args) {
        var name = args.get_name(); //The command name
        var val = args.get_value(); //The tool that initiated the command


        if (name === "Emoticons") {
            //Set the background image to the head of the tool depending on the selected toolstrip item
            var tool = args.get_tool();
            var span = tool.get_element().getElementsByTagName("SPAN")[0];
            span.style.backgroundImage = "url(" + val + ")";

            //Paste the selected in the dropdown emoticon   
            editor.pasteHtml("<img src='" + val + "'>");

            //Cancel the further execution of the command
            args.set_cancel(true);
        }
    }

    function Editor_OnLoad(editor, args) {

        $notify = $find("<%: notify.ClientID %>");

        editor.attachEventHandler("onkeyup", function (e) {

            if (e.keyCode === 13) { // Enter key

                SendMessage(editor.get_html(true));

            }

        });

    }

    var chatHub;
    var $notify;

    function SendMessage(txt) {
        chatHub.server.broadcast(txt);

        var theEditor = $find("editor");
        theEditor.set_html("");
        theEditor.getSelection().GetRange().startOffset = 0

    }

    $(function () {

        var $msg = $("#messages");

        function init() {

        }

        chatHub = $.connection.chatHub;

        chatHub.client.broadcastMessage = function (user, message) {

            console.log("received message");
            $msg.append("<b>" + user + "</b>: " + message);


        }

        chatHub.client.notifyConnection = function (user) {
            // Add user to list of chatters
            console.log("Connecting: " + user);
            if (!$("#chat_" + user).length) {
                $("#currentChatters").append("<li id='chat_" + user + "'>" + user + "</li>");
                $notify.set_text(user + " has joined");
                $notify.show();
            }
        }

        chatHub.client.notifyDisconnection = function (user) {
            // Remove user from list of chatters
            $("#chat_" + user).remove();
        }

        $.connection.hub.logging = true;
        $.connection.hub.start().done(init);



    });

    //--></script>
    </form>
</body>
</html>
