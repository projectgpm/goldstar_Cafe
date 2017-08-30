<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="QuanLyGio.aspx.cs" Inherits="BanHang.QuanLyGio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID" OnRowDeleting="gridDanhSach_RowDeleting" OnRowInserting="gridDanhSach_RowInserting" OnRowUpdating="gridDanhSach_RowUpdating" OnInitNewRow="gridDanhSach_InitNewRow">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings AutoFilterCondition="Contains" ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton RenderMode="Image">
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
            <NewButton>
                <Image IconID="actions_add_16x16" ToolTip="Thêm">
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
            <EditButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_edit_16x16devav" ToolTip="Sửa">
                </Image>
            </EditButton>
            <DeleteButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin ca bán" Title="DANH SÁCH GIỜ" />
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewColumnLayoutItem ColumnName="Tỷ Lệ (%)" Name="TenCa">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Thời Gian Bắt Đầu" Name="GioBatDau">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Thời Gian Kết Thúc" Name="GioKetThuc" >
                </dx:GridViewColumnLayoutItem>
                <dx:EditModeCommandLayoutItem HorizontalAlign="Right" >
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
         <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="5" Name="iconaction">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Mã" FieldName="MaGio" VisibleIndex="0">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Cập Nhật" FieldName="NgayCapNhat" VisibleIndex="4">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime">
                 </PropertiesDateEdit>
                 <Settings AutoFilterCondition="Contains" />
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataSpinEditColumn Caption="Tỷ Lệ (%)" FieldName="TyLe" VisibleIndex="3">
                 <PropertiesSpinEdit DisplayFormatString="g">
                     <ValidationSettings SetFocusOnError="True">
                         <RequiredField IsRequired="True" />
                     </ValidationSettings>
                 </PropertiesSpinEdit>
                 <Settings AutoFilterCondition="Contains" />
             </dx:GridViewDataSpinEditColumn>
             <dx:GridViewDataTimeEditColumn Caption="Thời Gian Bắt Đầu" FieldName="GioBatDau" VisibleIndex="1">
                 <PropertiesTimeEdit DisplayFormatString="" EditFormat="Custom" EditFormatString="HH:mm:ss">
                     <ValidationSettings SetFocusOnError="True">
                         <RequiredField IsRequired="True" />
                     </ValidationSettings>
                 </PropertiesTimeEdit>
                 <Settings ShowFilterRowMenu="True" />
             </dx:GridViewDataTimeEditColumn>
             <dx:GridViewDataTimeEditColumn Caption="Thời Gian Kết Thúc" FieldName="GioKetThuc" VisibleIndex="2" >
                 <PropertiesTimeEdit DisplayFormatString="" EditFormat="Custom" EditFormatString="HH:mm:ss">
                     <ValidationSettings SetFocusOnError="True">
                         <RequiredField IsRequired="True" />
                     </ValidationSettings>
                 </PropertiesTimeEdit>
             </dx:GridViewDataTimeEditColumn>
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
</asp:Content>
