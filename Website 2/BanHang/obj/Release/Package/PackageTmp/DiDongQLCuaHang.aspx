<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiDongQLCuaHang.aspx.cs" Inherits="BanHang.DiDongQLCuaHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
            <Items>
                <dx:LayoutGroup Caption="Thời gian" ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="Từ ngày" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxDateEdit ID="txtNgayBD" runat="server" DisplayFormatString="dd/MM/yyyy">
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Đến ngày:" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxDateEdit ID="txtNgayKT" runat="server" DisplayFormatString="dd/MM/yyyy">
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="ASPxFormLayout1_E1" runat="server" OnClick="ASPxFormLayout1_E1_Click" Text="Hôm nay">
                                        <Image IconID="actions_reset2_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnXem" runat="server" OnClick="btnXem_Click" Text="Lịch sử doanh thu">
                                        <Image IconID="data_database_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="">
                    <Items>
                        <dx:LayoutItem Caption="" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridView ID="gridNoiDung" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsCommandButton>
                                            <ShowAdaptiveDetailButton ButtonType="Image">
                                            </ShowAdaptiveDetailButton>
                                            <HideAdaptiveDetailButton ButtonType="Image">
                                            </HideAdaptiveDetailButton>
                                        </SettingsCommandButton>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Thống kê" FieldName="ThongKe" ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nội dung" FieldName="NoiDung" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    
    </div>
    </form>
</body>
</html>
