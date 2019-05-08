using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    DALC _db = new DALC();
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "Moderno";
    //}
    //string _access_login = Utils.accessLoginSearchPerson;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack) return;

        string _projectName = Config.GetAppSetting("ProjectName");

        ltrFooter.Text = string.Format("{0} &copy; {1}", DateTime.Now.Year, _projectName);
        ImgUser.ImageUrl = string.Format("/uploads/managers/{0}.png", DALC.ManagerInfo.ID);
        LtrUserName.Text = DALC.ManagerInfo.Surname + " " + DALC.ManagerInfo.Name;

        DataTable dt = _db.GetAdminMenuPermission();

        if (dt != null)
        {
            RptMenu.DataSource = dt;
            RptMenu.DataBind();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Config.Cs(dt.Rows[i]["SubCount"]) != "0")
                {
                    Repeater rpt = RptMenu.Items[i].FindControl("rptSubMenu") as Repeater;

                    rpt.DataSource = _db.GetAdminMenu(int.Parse(Config.Cs(dt.Rows[i]["ID"])));
                    rpt.DataBind();
                }
                //GroupName,GroupID
                //if (Config.Cs(dt.Rows[i]["GroupID"]).Length > 0 && _oldGroupID != Config.Cs(dt.Rows[i]["GroupID"]))
                //{
                //    _oldGroupID = Config.Cs(dt.Rows[i]["GroupID"]);
                //    Literal ltrMenuTitle = RptMenu.Items[i].FindControl("LtrMenuTitle") as Literal;
                //    ltrMenuTitle.Text = string.Format("<li class='headingHr' ></li>", dt.Rows[i]["GroupName"]);
                //}
            }
        }





        //EduType.DataSource = _db.GetManagerEduTypeList();
        //EduType.DataBind();
        //EduType.Value = DALC.selectedEduTypeID;
        //EduType_SelectedIndexChanged(null, null);

        Page.Title = _projectName;
        // LtrTitle.Text = string.Format("{0}<br/><span class='edu-year'>{1}</span>", _projectName, Config.GetEduYearStr);
        //LtrTitle.Text = string.Format("<span class='edu-year'>{0}</span>", Config.GetEduYearStr);

        // ltrYear.Text = DALC._selectedUniID;
        //this.form2.Action = Request.RawUrl;
    }

    protected void EduType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["SelectedEduTypeID"] = EduType.Value;

        //string _oldeduTypeID = _db.GetManagerPermissionDefaultEduTypeID;

        //if (EduType.Value.ToParseStr() != _oldeduTypeID)
        //{
        //    Session["SelectedUniID"] = Uni.Value = null;
        //}

        //Uni.DataSource = _db.GetManagerPermissionUniList();
        //Uni.DataBind();
        //if (DALC._selectedUniID.Length > 0)
        //{
        //    Uni.Value = DALC._selectedUniID;
        //}
        //else
        //{
        //    Uni.SelectedIndex = 0;
        //}

        //Session["SelectedUniID"] = Uni.Value;
        //_db.ManagerLastEduType_UniShowUpdate();

        //if (EduType.Value.ToParseStr() != _oldeduTypeID)
        //{
        //    Config.Rd(Request.Url.ToString());
        //}
    }
    protected void Uni_SelectedIndexChanged(object sender, EventArgs e)
    {

        //Session["SelectedUniID"] = Uni.Value;
        //_db.ManagerLastEduType_UniShowUpdate();
        //Config.Rd(Request.Url.ToString());
    }
}
