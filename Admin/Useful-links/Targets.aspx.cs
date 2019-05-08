﻿using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Useful_links_Default : System.Web.UI.Page
{
    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.static_values;
    const string _defaultDropdownRowText = "--Bütün məlumatlar--";

    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isLogin(Page);

        _loadGridFromDb();
        if (IsPostBack) return;

        lblFilter.Text = "Axtarış";
        ltrPageTitle.Text = "Hədəflər üzrə cavabdehlik";

        _loadDropDowns();
        Goals_SelectedIndexChanged(null, null);
        //_dataPermission();

        //_loadGridFromDb();

        lnkSearch.Visible = false;
        PnlSearch.Visible = true;
    }

    private void _loadDropDowns()
    {
        Goals.DataSource = _db.GetGoals();
        Goals.DataBind();
        Goals.SelectedIndex = 0;
    }

    #region Methods

    void _loadGridFromDb()
    {
        DataTable dtGoals = _db.GetGoals();

        GridViewDataComboBoxColumn column = Grid.Columns["goal_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dtGoals.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem("Məqsəd " + item["priority"].ToParseStr() + ". " + item["name_short_az"].ToParseStr(), item["Id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText, ""));

        DataTable dt = _db.Get_useful_links_targets(rdNational.SelectedValue.ToParseInt(),Goals.Value.ToParseInt());
        Grid.DataSource = dt;
        Grid.DataBind();
        ViewState["Grid"] = Grid.DataSource;
    }

    void _loadGridFromViewState()
    {
        if (ViewState["Grid"] != null)
        {
            Grid.DataSource = ViewState["Grid"] as System.Data.DataTable;
            Grid.DataBind();
        }
    }

    void _export(string value)
    {
        gridExporter.FileName = DateTime.Now.ToString("ddMMyyyhhmm");
        switch (value)
        {
            case "ex_xls": gridExporter.WriteXlsToResponse(); break;
            case "ex_xlsx": gridExporter.WriteXlsxToResponse(); break;
            //case "ex_csv": gridExporter.WriteCsvToResponse(); break;
            case "ex_pdf": gridExporter.WritePdfToResponse(); break;
            case "ex_rtf": gridExporter.WriteRtfToResponse(); break;
        }
    }

    void _dataPermission()
    {
        //Utils.PermissionTables _data = _db.GetPermissionTable(_pageTable);
        //btnAdd.Visible = _data.isInsert;

        //if (_data.isUpdate)
        //{
        //    Grid.ClientSideEvents.RowDblClick = "function(s, e) {" + Page.ClientScript.GetPostBackClientHyperlink(btnEdit, "") + " ; }";
        //}
        //else
        //{
        //    Grid.Columns[10].Visible = false;
        //}
        //if (!_data.isDelete)
        //{
        //    Grid.Columns[11].Visible = false;
        //}
    }

    #endregion




    protected void LnkPnlMenu_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "add":
                Config.Rd("/diplomblanks/blankitem.aspx");
                break;
            //case "search":
            //    PnlSearch.Visible = true;
            //    break;
            case "export":
                _loadGridFromViewState();
                _export(btn.CommandName);
                break;

        }
    }

    protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        Utils.MethodType val = _db.useful_links_targets_Update(
                                     id: e.Keys["id"].ToParseInt(),
                                     name_az: e.NewValues["name_az"].ToParseStr(),
                                     name_en: e.NewValues["name_en"].ToParseStr(),
                                     link: e.NewValues["link"].ToParseStr(),
                                     isnational: rdNational.SelectedValue.ToParseInt(),
                                     goal_id: e.NewValues["goal_id"].ToParseInt()
                                 );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void rdNational_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
        PnlSearch.Style["display"] = "block";
        _loadGridFromDb();
    }
    protected void Grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        Utils.MethodType val = _db.useful_links_targets_Insert(
                                     name_az: e.NewValues["name_az"].ToParseStr(),
                                     name_en: e.NewValues["name_en"].ToParseStr(),
                                     link: e.NewValues["link"].ToParseStr(),
                                     isnational: rdNational.SelectedValue.ToParseInt(),
                                     goal_id: e.NewValues["goal_id"].ToParseInt()
                                 );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Utils.MethodType val = _db.useful_links_targets_Delete(
                                     id: e.Keys["id"].ToParseInt()
                                 );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }

    protected void Goals_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
        PnlSearch.Style["display"] = "block";
        _loadGridFromDb();
    }
}