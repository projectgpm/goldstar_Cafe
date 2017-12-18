<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietBangGia.aspx.cs" Inherits="BanHang.ChiTietBangGia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
             <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridChiTietPhieuKiemKho" KeyFieldName="ID" OnRowUpdating="gridChiTietPhieuKiemKho_RowUpdating">
                 <SettingsPager Mode="ShowAllRecords">
                 </SettingsPager>
        <SettingsEditing Mode="Batch">
        </SettingsEditing>
<Settings ShowTitlePanel="True"></Settings>

        <SettingsBehavior ConfirmDelete="True" />

        <SettingsCommandButton>
        <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

        <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
            <NewButton>
                <Image IconID="actions_add_16x16" ToolTip="Thêm mới">
                </Image>
            </NewButton>
            <UpdateButton ButtonType="Image" RenderMode="Image">
                <Image IconID="save_save_32x32office2013" ToolTip="Lưu">
                </Image>
            </UpdateButton>
            <CancelButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_close_32x32" ToolTip="Hủy thao tác">
                </Image>
            </CancelButton>
            <EditButton>
                <Image IconID="actions_edit_16x16devav" ToolTip="Sửa">
                </Image>
            </EditButton>
            <DeleteButton>
                <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>

        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>

<SettingsText Title="THÔNG TIN CHI TIẾT BẢNG GIÁ" CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa"></SettingsText>
        <EditFormLayoutProperties ColCount="2">
            <Items>
                <dx:GridViewColumnLayoutItem ColumnName="Mã Hàng">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Hàng Hóa">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="ĐVT">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Tồn Kho">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Thực Tế">
                </dx:GridViewColumnLayoutItem>
                <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
<Columns>
    <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" VisibleIndex="1" ReadOnly="true" Name="TenHangHoa" FieldName="TenHangHoa">
    </dx:GridViewDataTextColumn>
    <dx:GridViewDataTextColumn Caption="Mã Hàng" VisibleIndex="0" Name="MaHangHoa" ReadOnly="True" FieldName="MaHangHoa">
    </dx:GridViewDataTextColumn>
    
    <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" VisibleIndex="2" ReadOnly="true" Name="TenDonViTinh" FieldName="TenDonViTinh">
    </dx:GridViewDataTextColumn>
    
    <dx:GridViewDataSpinEditColumn Caption="Giáp Áp Dụng" VisibleIndex="4" Name="GiaMoi" FieldName="GiaMoi">
        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
        </PropertiesSpinEdit>
    </dx:GridViewDataSpinEditColumn>
    
    <dx:GridViewDataSpinEditColumn Caption="Giá Hệ Thống" Name="GiaCu" ReadOnly="True" VisibleIndex="3" FieldName="GiaCu">
        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
        </PropertiesSpinEdit>
    </dx:GridViewDataSpinEditColumn>
    
</Columns>

<Styles>
<Header HorizontalAlign="Center" Font-Bold="True"></Header>

<AlternatingRow Enabled="True"></AlternatingRow>

<TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
</Styles>
  
</dx:ASPxGridView>
    </div>
    </form>
</body>
</html>
