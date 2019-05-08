<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="WebPages_Contact" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section>
                <div class="container main-container">
                    <div class="breadcrumb">
                        <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-md-6 ">
                            <div class="borderAll  contact-section">
                                <div class="contact-section-title">
                                    <i class="fa fa-envelope"></i>
                                    <asp:Literal ID="ltrSendLetter" Text="" runat="server" />
                                </div>
                                <div class="contact-form">
                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" ValidationGroup="email" />
                                    <asp:RegularExpressionValidator ID="CheckEmail" runat="server" ValidationGroup="email" ControlToValidate="txtEmail" ErrorMessage="RegularExpressionValidator" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" ID="txtSubject" placeholder="Movzu" CssClass="form-control" />
                                    <asp:TextBox runat="server" ID="txtContent" placeholder="MƏtn" Height="174px" CssClass="form-control" TextMode="MultiLine" />
                                    <asp:Label ID="lblMessage" Text="" runat="server" />
                                    <div style="text-align: right">
                                        <asp:Button ID="btnSend" Text="Send" 
                                            runat="server" 
                                            CssClass="btn btn-primary"
                                            OnClientClick="this.style.display='none';document.getElementById('loading').style.display='block';this.style.display='block';"
                                             OnClick="btnSend_Click" ValidationGroup="email" />
                                        <div id="loading" style="display:none">
                                            <img  src="/images/loading.gif" alt="loading " style="max-height:50px;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 ">
                            <div class="borderAll  contact-section">
                                <div class="contact-section-title">
                                    <i class="fa fa-map-marker"></i>
                                    <asp:Literal ID="ltrMapMarker" Text="" runat="server" />
                                </div>
                                <div>
                                    <iframe
                                        width="100%"
                                        height="300"
                                        frameborder="0" style="border: 0"
                                        src="https://maps.google.com/maps?ie=UTF8&cid=1321900782848881932&q=D%C3%B6vl%C9%99t+Statistika+Komit%C9%99si&gl=AZ&hl=az&hq=D%C3%B6vl%C9%99t+Statistika+Komit%C9%99si&hnear=&t=m&source=embed&ll=40.383224,49.822226&spn=0.006295,0.006295&iwloc=A&output=embed" allowfullscreen></iframe>
                                </div>
                                 <asp:Literal ID="LocationText" Text="" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


