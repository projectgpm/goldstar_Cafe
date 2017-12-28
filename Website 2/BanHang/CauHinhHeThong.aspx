<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="CauHinhHeThong.aspx.cs" Inherits="BanHang.CauHinhHeThong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowUpdating="gridDanhSach_RowUpdating">
                                    <SettingsPager Visible="False">
                                    </SettingsPager>
                                    <SettingsEditing Mode="Batch">
                                    </SettingsEditing>
                                    <Settings AutoFilterCondition="Contains" />
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsCommandButton>
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
                                    <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin đơn vị tính" Title="DANH SÁCH ĐƠN VỊ TÍNH" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
                                    <EditFormLayoutProperties>
                                        <Items>
                                            <dx:GridViewColumnLayoutItem ColumnName="Mã ĐVT">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Tên Đơn Vị Tính" Name="TenDonViTinh">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                            </dx:EditModeCommandLayoutItem>
                                        </Items>
                                    </EditFormLayoutProperties>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Công Ty" FieldName="CongTy" VisibleIndex="0">
                                            <PropertiesTextEdit>
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Địa Chỉ" FieldName="DiaChi" VisibleIndex="1">
                                            <PropertiesTextEdit>
                    
                                                <ValidationSettings SetFocusOnError="True">
                        
                                                    <RequiredField IsRequired="True" />
                        
                                                </ValidationSettings>
                    
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Điện Thoại" FieldName="SDT" VisibleIndex="2">
                                            <PropertiesTextEdit>
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Tiền Qui Đổi Điểm" FieldName="SoTienTichLuy" VisibleIndex="3">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="N0" NumberFormat="Custom">
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Điểm Qui Đổi Tiền" FieldName="SoTienQuyDoi" VisibleIndex="5">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="N0" NumberFormat="Custom">
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
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
</asp:Content>
