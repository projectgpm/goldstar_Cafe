<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="QuanLyNguyenLieu.aspx.cs" Inherits="BanHang.QuanLyNguyenLieu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
         <Items>
             <dx:LayoutGroup Caption="Nguyên liệu chế biến">
                 <Items>
                     <dx:LayoutItem Caption="">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer runat="server">
                                 <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridDanhSach_RowDeleting" OnRowInserting="gridDanhSach_RowInserting" OnRowUpdating="gridDanhSach_RowUpdating" OnInitNewRow="gridDanhSach_InitNewRow">
                                    <SettingsEditing Mode="PopupEditForm">
                                    </SettingsEditing>
                                    <Settings AutoFilterCondition="Contains" ShowFilterRow="True" />
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
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin nguyên liệu" Title="DANH SÁCH NGUYÊN LIỆU CHẾ BIẾN" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
                                    <EditFormLayoutProperties>
                                        <Items>
                                            <dx:GridViewColumnLayoutItem ColumnName="Mã Nguyên Liệu">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Tên Nguyên Liệu" Name="TenDonViTinh">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="ĐVT">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Giá Mua">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Giá Bán">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Loại Nguyên Liệu">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Nhà Cung Cấp">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Ghi Chú">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                            </dx:EditModeCommandLayoutItem>
                                        </Items>
                                    </EditFormLayoutProperties>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="9" Name="iconaction">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã Nguyên Liệu" FieldName="MaNguyenLieu" VisibleIndex="0" ReadOnly="True">
                                            <PropertiesTextEdit>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên Nguyên Liệu" FieldName="TenNguyenLieu" VisibleIndex="1">
                                            <PropertiesTextEdit>
                    
                                                <ValidationSettings SetFocusOnError="True">
                        
                                                    <RequiredField IsRequired="True" />
                        
                                                </ValidationSettings>
                    
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Ngày Cập Nhật" FieldName="NgayCapNhat" VisibleIndex="8">
                                            <propertiesdateedit displayformatstring="dd/MM/yyyy"></propertiesdateedit>
                                            <settings autofiltercondition="Contains" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhà Cung Cấp" FieldName="NhaCungCap" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" VisibleIndex="5">
                                            <PropertiesComboBox DataSourceID="SqlDVT" TextField="TenDonViTinh" ValueField="ID">
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Giá Mua" FieldName="GiaMua" VisibleIndex="2">
                                            <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom" DisplayFormatInEditMode="True">
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Loại Nguyên Liệu" FieldName="TrangThai" ShowInCustomizationForm="True" VisibleIndex="7">
                                            <PropertiesComboBox>
                                                <Items>
                                                    <dx:ListEditItem Text="Tự chọn" Value="1" />
                                                    <dx:ListEditItem Text="Không tự chọn" Value="0" />
                                                </Items>
                                                <ValidationSettings SetFocusOnError="True">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Giá Bán" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="3">
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
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                 </Items>
             </dx:LayoutGroup>
         </Items>
     </dx:ASPxFormLayout>
     
     <asp:SqlDataSource ID="SqlDVT" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [CF_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
</asp:Content>
