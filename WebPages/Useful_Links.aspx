<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Useful_Links.aspx.cs" Inherits="WebPages_UsefulLinks" %>


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
                        <div class="col-md-12">
                            <div class="borderAll paddingLR10">
                                <!-- Nav tabs -->
                                <h2>Faydalı keçidlər</h2>
                                <hr>
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item active">
                                        <a class="nav-link active" data-toggle="tab" href="http://etsim.az/db/sdg/useful-links.php#home" aria-expanded="true">Məlumat bazaları</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="http://etsim.az/db/sdg/useful-links.php#menu1" aria-expanded="false">Bilik platformaları</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="http://etsim.az/db/sdg/useful-links.php#menu2">Hesabatlıq platformaları</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="http://etsim.az/db/sdg/useful-links.php#menu3">Digər keçidlər</a>
                                    </li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div id="home" class="container tab-pane active in">
                                        <br>
                                        <h3>Məlumat bazaları</h3>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                            <div class="col-md-6">
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="menu1" class="container tab-pane fade">
                                        <br>
                                        <h3>Bilik platformaları</h3>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                            <div class="col-md-6">
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="menu2" class="container tab-pane fade">
                                        <br>
                                        <h3>Hesabatlıq platformaları</h3>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <h6>Qlobal hesabatlıq platformaları</h6>
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                            <div class="col-md-6">
                                                <h6>Milli hesabatlıq platformaları</h6>
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="menu3" class="container tab-pane fade">
                                        <br>
                                        <h3>Digər keçidlər</h3>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                            <div class="col-md-6">
                                                <ul class="list-unstyled">
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 1</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 2</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 3</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 4</a></li>
                                                    <li><a href="http://etsim.az/db/sdg/useful-links.php#">Link 5</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

