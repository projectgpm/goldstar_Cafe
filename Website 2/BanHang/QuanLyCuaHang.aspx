<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="QuanLyCuaHang.aspx.cs" Inherits="BanHang.QuanLyCuaHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Thống kê">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxGridView ID="gridDanhSachBan" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
                                    <SettingsPager Visible="False">
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm">
                                    </SettingsEditing>
       
       
                            <SettingsCommandButton>
                            <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

                            <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
                            </SettingsCommandButton>

       
                                    <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin danh mục thuế" Title="DANH SÁCH LỊCH SỬ TRUY CẬP HỆ THỐNG" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Bàn Sử Dụng" FieldName="SoBanSuDung" VisibleIndex="0">
                                            <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:#,#} bàn">
                                            </PropertiesTextEdit>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Tổng Tiền" FieldName="TongTien" ShowInCustomizationForm="True" VisibleIndex="1">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:N0} VNĐ" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Lợi Nhuận" FieldName="LoiNhuan" ShowInCustomizationForm="True" VisibleIndex="5">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:N0} VNĐ" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Giảm Giá" FieldName="GiamGia" ShowInCustomizationForm="True" VisibleIndex="2">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:N0} VNĐ" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Phụ Thu" FieldName="PhuThu" ShowInCustomizationForm="True" VisibleIndex="4">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:N0} VNĐ" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                    </Columns>
                                    <Styles>
                                        <Header Font-Bold="True" HorizontalAlign="Center">
                                        </Header>
                                        <AlternatingRow Enabled="True">
                                        </AlternatingRow>
                                        <TitlePanel Font-Bold="True" HorizontalAlign="Left">
                                        </TitlePanel>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
