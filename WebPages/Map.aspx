<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Map.aspx.cs" Inherits="WebPages_Map" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/example-map/ammap/ammap.css" type="text/css" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script src="/example-map/ammap/ammap.js" type="text/javascript"></script>
    <!-- map file should be included after ammap.js -->
    <script src="/example-map/ammap/maps/js/azerbaijanLow.js" type="text/javascript"></script>
    <script>
        AmCharts.makeChart("mapdiv", {
            type: "map",


            colorSteps: 10,

            dataProvider: {
                map: "azerbaijanLow",
                areas: [{ id: "AM-GR" }, { id: "AZ-ABS" }, { id: "AZ-AGA" }, { id: "AZ-AGC" }, { id: "AZ-AGM" }, { id: "AZ-AGS" }, { id: "AZ-AGU" }, { id: "AZ-AST" }, { id: "AZ-BA" }, { id: "AZ-BAB" }, { id: "AZ-BAL" }, { id: "AZ-BAR" }, { id: "AZ-BEY" }, { id: "AZ-BIL" }, { id: "AZ-CAB" }, { id: "AZ-CAL" }, { id: "AZ-CUL" }, { id: "AZ-DAS" }, { id: "AZ-FUZ" }, { id: "AZ-GA" }, { id: "AZ-GAD" }, { id: "AZ-GOR" }, { id: "AZ-GOY" }, { id: "AZ-GYG" }, { id: "AZ-HAC" }, { id: "AZ-IMI" }, { id: "AZ-ISM" }, { id: "AZ-KAL" }, { id: "AZ-KAN" }, { id: "AZ-KUR" }, { id: "AZ-LA" }, { id: "AZ-LAC" }, { id: "AZ-LAN" }, { id: "AZ-LER" }, { id: "AZ-MAS" }, { id: "AZ-MI" }, { id: "AZ-NA" }, { id: "AZ-NEF" }, { id: "AZ-NV" }, { id: "AZ-OGU" }, { id: "AZ-ORD" }, { id: "AZ-QAB" }, { id: "AZ-QAX" }, { id: "AZ-QAZ" }, { id: "AZ-QBA" }, { id: "AZ-QBI" }, { id: "AZ-QOB" }, { id: "AZ-QUS" }, { id: "AZ-SA" }, { id: "AZ-SAB" }, { id: "AZ-SAD" }, { id: "AZ-SAH" }, { id: "AZ-SAK" }, { id: "AZ-SAL" }, { id: "AZ-SAR" }, { id: "AZ-SAT" }, { id: "AZ-SBN" }, { id: "AZ-SIY" }, { id: "AZ-SKR" }, { id: "AZ-SM" }, { id: "AZ-SMI" }, { id: "AZ-SMX" }, { id: "AZ-SR" }, { id: "AZ-SUS" }, { id: "AZ-TAR" }, { id: "AZ-TOV" }, { id: "AZ-UCA" }, { id: "AZ-XAC" }, { id: "AZ-XCI" }, { id: "AZ-XIZ" }, { id: "AZ-XVD" }, { id: "AZ-YAR" }, { id: "AZ-YE" }, { id: "AZ-YEV" }, { id: "AZ-ZAN" }, { id: "AZ-ZAQ" }, { id: "AZ-ZAR" }]
            },

            areasSettings: {
                autoZoom: false,
                rollOverColor: '#152e47',
                rollOverOutlineColor: '#ffffff',
                color: '#336699',
                allowMultipleDescriptionWindows: true
            }


        });
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="container main-container">
        <div id="mapdiv" style="width: 100%; background-color: #fff; height: 500px;"></div>
    </div>
</asp:Content>


