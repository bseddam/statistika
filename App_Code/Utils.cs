using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
    public enum MenuType
    {
        Header = 1,
        Main = 3,
        Footer = 2,
        Footer2 = 4
    }
    public enum PageType
    {
        News = 1,
        Videos = 2,
        Content = 3,
        Laws = 4,
        SummaryGoals = 5,
        International = 7,
        Konstitusiya = 8,
        KonstitusiyaDefaultPage = 9,
        InternationalLinks = 10,
        MechanismDefaultPage = 11,
        MechanismOtherPages = 12,
        MechanismGundelik = 13,
        TerefdaslarCategory = 14,
        Terefdaslar = 15,
        About = 16,

        Order = 17, //Fermanlar = 17

        Tedqiqatlar,
        Nesrler,
        Slider,
        Maragliterefler,
        Researches = 1000,
        Publications = 2000,
    }
    public enum OrderByColumns
    {
        ID,
        Name,
        Priority
    }


    //select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE' order by TABLE_NAME
    public enum Tables
    {
        menu_type = 1,
        admin_menu = 2,
        Managers = 3,
        goals = 4,
        list_table = 5,
        Logs = 6,
        ManagersGroup = 7,
        managers_permission_menu = 8,
        Menu = 9,
        pages = 10,
        page_type = 11,
        slider = 12,
        static_values = 13,
        v_Managers = 14,
        v_MenuPermission = 15,
        ManagersPermissionTable = 16,
        v_menu = 17,
        sitemap = 18,
        v_sitemap = 19,
        national_reports = 20,
        publications = 21,
        researches = 22,
        users = 23,
        indicator_size = 24

    }

    public enum LogType
    {
        select = 1,
        insert = 2,
        update = 3,
        delete = 4
    }

    public enum MethodType
    {
        Succes = 1,
        Error = 0
    }

    public enum ManagersGroup
    {
        Admin = 1,
        Nezaretci = 2,
        Diplomveren = 3,
        Icrachi = 4
    }



    // program daxilinde isdifade olunacagi ucun bura elave olunub
    /// <summary>
    /// ListEduStatus tableindaki  setirler
    /// </summary>
    public enum ListEduStatus
    {
        Qebul = 1,
        Kochurulme = 2,
        Kochurulme_YerliAliMektebden = 11,
        Kochurulme_XariciAliMektebden = 12,
        Kochurulme_SABAHQrupuna = 13,
        IxtisasDeyisme = 3,
        Kursdaqalma = 14,
        Fasile = 4,
        Fasile_HerbiXidmet = 6,
        Fasile_AkademikMezuniyyet = 7,
        Berpa = 9,
        Berpa_HerbiXidmetden = 15,
        Berpa_AkademikMezuniyyetden = 16,
        Diplomverme = 10,
        Xaricetme = 17,
        Xaricetme_Kochurulme = 25,
        Xaricetme_OzArzusuIle = 18,
        Xaricetme_TehsilHaqqiniOdemiyib = 19,
        Xaricetme_VefatEdib = 8,
        Xaricetme_SABAHQrupuna = 20,
        ShexsiMelumatlarinDeyishdirilmesi = 21,
        ShexsiMelumatlarinDeyishdirilmesi_SoyadıDeyishme = 22,
        ShexsiMelumatlarinDeyishdirilmesi_AdiDeyishme = 23,
        ShexsiMelumatlarinDeyishdirilmesi_AtaAdiniDeyishme = 24,
        Diger = 26
    }

    public enum ListCommandStatus
    {
        DaxilOlunub = 1,
        TesdiqOlunub = 2,
        QismenTesdiqOlunub = 3
    }

    public enum ListDiplomType
    {
        Adi = 1, Ferqlenme = 2
    }
    public enum ListDiplomGivenType
    {
        Etibarli = 1,
        Etibarli_Dublikat = 2,
        Korlanmish = 3,
        Itirilmish = 4
    }
    public enum MainSearchType
    {
        All = 0,
        Student = 1,
        StudentMezun = 2
    }

    public static int rowCount = 15;

    public static List<ListItem> pageSize = new List<ListItem>() 
    { 
        new ListItem("10", "10"),
        new ListItem("20", "20"),
        new ListItem("50", "50"),
        new ListItem("100", "100"),
        new ListItem("500", "500")
    
    };
    public static List<ListItem> pageSize_commands = new List<ListItem>() 
    { 
        new ListItem("10", "10"),
        new ListItem("20", "20"),
        new ListItem("50", "50") ,
        new ListItem("100", "100"),
        new ListItem("250", "250")
    };
    public class PermissionTables
    {
        public bool isSelect { get; set; }
        public bool isInsert { get; set; }
        public bool isUpdate { get; set; }
        public bool isDelete { get; set; }

    }
    /// <summary>
    /// gridde gorunecey setir sayi
    /// </summary>
    public static string cookiePageCount = "pagecount";
    /// <summary>
    /// gridde hansi sehifede oldugu
    /// </summary>
    public static string cookiePage = "page";


}

/// <summary>
/// Command seyfelerine gonderdiymiz deyerler ucun interface
/// </summary>
public interface ICommandParams
{
    int CommandID { get; set; }
    int Command_EduStatusID { get; set; }
    int Command_Year { get; set; }
    string CommandName { get; set; }
    int Command_UniversityID { get; set; }
    string Command_UniversityName { get; set; }
}