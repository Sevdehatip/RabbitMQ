<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="RabbitMQProject.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<label runat="server" id="lblNotConnect" visible="false">No connection. Please press the "Connect" button.</label>
    <asp:Button runat="server" ID="btnConnect" OnClick="btnConnect_Click" Text="Connect"/>
    <div>
        <label>Declare Exchange</label><br />
        Name: <asp:textbox runat="server" ID="tbExchangeName"></asp:textbox>
        Type: <asp:DropDownList CssClass="form-control" ID="ddlExchangeType" runat="server">
                                        <asp:ListItem Value="direct">Direct</asp:ListItem>
                                        <asp:ListItem Value="topic">Topic</asp:ListItem>
                                        <asp:ListItem Value="fanout">Fanout</asp:ListItem>
                                        <asp:ListItem Value="headers">Headers</asp:ListItem>
                                    </asp:DropDownList>
        <asp:Button runat="server" ID="btnDeclareExchange" OnClick="btnDeclareExchange_Click" Text="Declare"/>
    </div>
</asp:Content>
