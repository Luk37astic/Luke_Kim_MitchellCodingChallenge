<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebClient._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="GetBtn" runat="server" onclick="GetBtn_Click" Text="Get Claim Number: "/>
                            <asp:TextBox ID="ClaNumTxt" runat="server"/>
                            <asp:Button ID="GetAllBtn" runat="server" onclick="GetAllBtn_Click" Text="Get All Claims"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="StartDateLbl" runat="server" Text="Start Date: "/>
                            <asp:Button ID="StartDateBtn" runat="server" Text="Open Calendar" OnClick="StartDateBtn_Click"/>
                            <asp:Calendar ID="StartDateCal" runat="server" Visible="false" OnSelectionChanged="StartDateCal_SelectionChanged"/>
                            <asp:Label ID="EndDateLbl" runat="server" Text="End Date: "/>
                            <asp:Button ID="EndDateBtn" runat="server" Text="Open Calendar" OnClick="EndDateBtn_Click"/>
                            <asp:Calendar ID="EndDateCal" runat="server" Visible="false" OnSelectionChanged="EndDateCal_SelectionChanged"/>
                            <asp:Button ID="DateRangeBtn" runat="server" Text="Get claims in date range" OnClick="DateRangeBtn_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="DelAllBtn" runat="server" Text="Delete All Claims" OnClick="DelAllBtn_Click"/>
                        </td>
                    </tr>
                </table>

                <asp:GridView ID="XmlGridView" runat="server" AutoGenerateColumns="false"
                    Height="247px" Width="795px" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    GridLines="Vertical" ShowFooter="true"
                    ShowHeaderWhenEmpty="true">
                    <%--OnRowCancelingEdit="XmlGridView_RowCancelingEdit" OnRowDeleting="XmlGridView_RowDeleting"
                    OnRowEditing="XmlGridView_RowEditing" OnRowUpdating="XmlGridView_RowUpdating"--%>

                    <AlternatingRowStyle BackColor="#DCDCDC"/>

                    <Columns>

                        <asp:TemplateField HeaderText="Claim Number">
                            <ItemTemplate>
                                <asp:Label ID="LblClaNum" runat="server" Text='<%# Bind("ClaimNumber")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtClaNum" runat="server" MaxLength="50"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Claimant First Name">
                            <ItemTemplate>
                                <asp:Label ID="LblFName" runat="server" Text='<%# Bind("ClaimantFirstName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditFName" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtFName" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Claimant Last Name">
                            <ItemTemplate>
                                <asp:Label ID="LblLName" runat="server" Text='<%# Bind("ClaimantLastName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditLName" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtLName" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="LblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditStatus" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="DrpStatus" runat="server">
                                    <asp:ListItem Value="OPEN" Selected="true"/>
                                    <asp:ListItem Value="CLOSED"/>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Loss Date">
                            <ItemTemplate>
                                <asp:Label ID="LblLossDate" runat="server" Text='<%# Bind("LossDate") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditLossDate" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="BtnLossDate" runat="server" Text="Open Calendar" OnClick="BtnLossDate_Click"/>
                                <asp:Calendar ID="CalLossDate" runat="server" Visible="false" OnSelectionChanged="CalLossDate_SelectionChanged"/>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Loss Info: Cause of Loss">
                            <ItemTemplate>
                                <asp:Label ID="LblLossInfoCause" runat="server" Text='<%# Bind("LossInfo.CauseOfLoss") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditLossInfoCause" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="DrpLossInfoCause" runat="server">
                                    <asp:ListItem Value="Collision" Selected="true"/>
                                    <asp:ListItem Value="Explosion"/>
                                    <asp:ListItem Value="Fire"/>
                                    <asp:ListItem Value="Hail"/>
                                    <asp:ListItem Value="Mechanical Breakdown"/>
                                    <asp:ListItem Value="Other"/>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Loss Info: Reported Date">
                            <ItemTemplate>
                                <asp:Label ID="LblLossInfoDate" runat="server" Text='<%# Bind("LossInfo.ReportedDate") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditLossInfoDate" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="BtnRepDate" runat="server" Text="Open Calendar" OnClick="BtnRepDate_Click"/>
                                <asp:Calendar ID="CalRepDate" runat="server" Visible="false" OnSelectionChanged="CalRepDate_SelectionChanged"/>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Loss Info: Loss Description">
                            <ItemTemplate>
                                <asp:Label ID="LblLossInfoDesc" runat="server" Text='<%# Bind("LossInfo.LossDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditLossInfoDesc" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtLossInfoDesc" runat="server" MaxLength="500"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assigned Adjuster ID">
                            <ItemTemplate>
                                <asp:Label ID="LblAssAdjuster" runat="server" Text='<%# Bind("AssignedAdjusterID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEditAssAdjuster" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtAssAdjuster" runat="server"/>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Operations">
                            <ItemTemplate>
                                <asp:Button ID="BtnEdit" runat="server" Text="Edit" />
                                <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_Click"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="BthUpdate" runat="server" Text="Update" />
                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="BtnInsert" runat="server" Text="Insert" OnClick="BtnInsert_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />

                </asp:GridView>

                <table>
                    <tr>
                        <td valign="top">Ouput:</td>
                        <td>
                            <asp:TextBox ID="ResTxt" runat="server" Height="134px" TextMode="MultiLine" 
                                Width="773px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </body>
</html>
