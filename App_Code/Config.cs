using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Config
{
    public static int SendMail(string ToEmail, string Title, string Message)
    {
        try
        {
            string fromEmail = System.Configuration.ConfigurationManager.AppSettings["Mail"];
            string passEmail = System.Configuration.ConfigurationManager.AppSettings["MailPassword"];
            string smtp = System.Configuration.ConfigurationManager.AppSettings["MailSmtp"];


            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtp);

            mail.From = new MailAddress(fromEmail);
            mail.To.Add(ToEmail);
            mail.Subject = Title;
            mail.Body = Message;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(fromEmail, passEmail);
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                   delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                   {
                       return true;
                   };
            SmtpServer.Send(mail);

            return 1;
        }
        catch
        {
            // DALC.LogInsert("SendEmail - Email Message : " + Message + "ex.Message: " + ex.Message + " - ex.InnerException : " + ex.InnerException);
            return -1;
        }
    }
    public static string ClearIndicatorCode(string code)
    {
        try
        {
            string result = "";
            string[] arr = code.Trim().Split('.');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "10")
                {
                    if (arr[i].Length == 3)
                    {
                        arr[i] = arr[i].Substring(2, 1);
                    }
                    else
                    {
                        arr[i] = arr[i].Replace("0", "");
                    }
                }


                if (arr[i].Length > 0)
                {
                    result += arr[i] + ".";
                }
            }

            return result.Trim('.');
        }
        catch
        {
            return "";
        }
    }
    public static string getYoutubeIframeURL(string link)
    {
        try
        {
            string videoId = link.Split(new string[] { "v=" }, StringSplitOptions.None)[1];
            string url = "https://www.youtube.com/embed/" + videoId.Split('&')[0];
            return url;
        }
        catch (Exception)
        {
            return "";
        }

    }
    public static Utils.PageType getPageType
    {

        get
        {
            Utils.PageType _type = Utils.PageType.Content;

            switch (HttpContext.Current.Request.QueryString["typeid"])
            {
                case "news": _type = Utils.PageType.News; break;
                case "content": _type = Utils.PageType.Content; break;
                case "videos": _type = Utils.PageType.Videos; break;
                case "law": _type = Utils.PageType.Laws; break;
                case "summarygoals": _type = Utils.PageType.SummaryGoals; break;
                case "international": _type = Utils.PageType.International; break;
                case "constitution": _type = Utils.PageType.Konstitusiya; break;
                case "cons_default_page": _type = Utils.PageType.KonstitusiyaDefaultPage; break;
                case "mechanism": _type = Utils.PageType.MechanismDefaultPage; break;
                case "mechanism_progress": _type = Utils.PageType.MechanismGundelik; break;
                case "mechanism_other_page": _type = Utils.PageType.MechanismOtherPages; break;
                case "sides": _type = Utils.PageType.Terefdaslar; break;
                case "about": _type = Utils.PageType.About; break;
                case "order": _type = Utils.PageType.Order; break;
            }
            return _type;
        }
    }
    public static string getLang(Page p)
    {
        string lang = p.RouteData.Values["lang"].ToParseStr();
        HttpContext.Current.Session["lang"] = lang;
        return lang.Length == 0 ? "az" : lang;
    }
    public static string getLangSession
    {
        get
        {
            string lang = HttpContext.Current.Session["lang"].ToParseStr();
            return lang.Length == 0 ? "az" : lang;
        }
    }

    //Bunun lazım olan bütün səhifələrin ilk Page_Load -na qoyuruq. 
    //Lazım olan bütün setting əmməliyyatlarını bura əlavə et.
    public static void PageSettings()
    {
        HttpContext.Current.Response.Cache.SetNoStore(); //Templəri bağlayırıq...
        HttpContext.Current.Session.Timeout = 10080;
        HttpContext.Current.Server.ScriptTimeout = 9999; //Əgər yüklənmə gecikərsə maksimum gözləmə saniyəsi.
        System.Threading.Thread.Sleep(0);
    }

    //Get WebConfig.config App Key
    public static string GetAppSetting(string KeyName)
    {
        return Cs(ConfigurationManager.AppSettings[KeyName]);
    }


    //Fileupload-da gələn şəkilin ölçüsünü kəsir (100x??px).
    public static Unit PicturesSizeSplit(string s)
    {
        try
        {
            int i = Convert.ToInt16(s.Substring(0, 3).Trim().Trim('x'));
            if (i < 220)
                return Unit.Pixel(i);
            else return Unit.Pixel(220);
        }
        catch { return Unit.Pixel(220); }
    }

    //Açar yaradaq.
    public static string Key(int say)
    {
        Random ran = new Random();
        string Bind = "aqwertyuipasdfghjkzxcvbnmQAZWSXEDCRFVTGBYHNUJMKP23456789";
        string Key = "";
        for (int i = 1; i <= say; i++)
        {
            Key += Bind.Substring(ran.Next(Bind.Length - 1), 1);
        }
        return Key.Trim();
    }

    public static string Slug(string x)
    {
        x = HtmlRemoval.StripTagsRegex(x);
        x = x.ToLower().Replace("<", "");
        x = x.Replace(">", "");
        x = x.Replace("(", "");
        x = x.Replace(")", "");
        x = x.Replace("`", "");
        x = x.Replace("'", "");
        x = x.Replace("\"", "");
        x = x.Replace("?", "");
        x = x.Replace("!", "");
        x = x.Replace("/", "");
        x = x.Replace("\\", "");
        x = x.Replace("&", "");
        x = x.Replace("#", "");
        x = x.Replace("~", "");
        x = x.Replace("%", "");

        x = x.Replace(" ", "-");
        x = x.Replace("&nbsp;", "-");

        x = x.Replace("ə", "e");
        x = x.Replace("ü", "u");
        x = x.Replace("ç", "ch");
        x = x.Replace("ö", "o");
        x = x.Replace("ğ", "g");
        x = x.Replace("ş", "s");
        x = x.Replace("ı", "i");
        x = x.Replace(",", "");
        x = x.Replace(".", "");
        x = x.Replace(":", "");
        x = x.Replace("”", "");
        x = x.Replace("“", "");

        x = x.Trim('.');
        x = x.Trim();

        int length = x.Length;

        if (length > 100)
        {
            length = 100;
        }
        return x.Substring(0, length);
    }

    //Cümlə çox uzun olanda üç nöqtə qoyaq.
    public static string SizeLimit(string t, int l)
    {
        if (t.Length > l) t = t.Substring(0, l) + " ...";
        return t;
    }

    //ConvertString.
    public static string Cs(object s)
    {
        if (s == null) s = "";
        return Convert.ToString(s);
    }

    //Numaric testi.
    public static bool IsNumeric(string s)
    {
        int num1;
        return int.TryParse(s, out num1);
    }
    public static bool IsNumeric_double(string s)
    {
        double num1;
        return double.TryParse(s, out num1);
    }

    //Messages Box.
    public static void MsgBox(string MsgTxt, Page P)
    {
        P.ClientScript.RegisterClientScriptBlock(P.GetType(), "PopupScript", "window.focus(); alert('" + MsgTxt + "');", true);
    }
    public static void ScriptAdd(string Script, Page P)
    {
        ScriptManager.RegisterClientScriptBlock(P, P.GetType(), "script", Script, true);
    }
    //Sha1  - özəl
    public static string Sha1(string Password)
    {
        byte[] result;
        System.Security.Cryptography.SHA1 ShaEncrp = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        Password = String.Format("{0}{1}{0}", "CSAASADM", Password);
        byte[] buffer = new byte[Password.Length];
        buffer = System.Text.Encoding.UTF8.GetBytes(Password);
        result = ShaEncrp.ComputeHash(buffer);
        return Convert.ToBase64String(result);
    }

    //Səhifəni yönləndirək:
    public static void Rd(string GetUrl)
    {
        HttpContext.Current.Response.Redirect(GetUrl, true);
    }
    /// <summary>
    /// Admine email gondermey
    /// </summary>
    /// <param name="Messages"></param>
    public static Utils.MethodType SendMailAdmin(string Messages)
    {
        try
        {
            //Mail gonder
            MailMessage MailSrv = new MailMessage(
                                                 Config.Cs(System.Configuration.ConfigurationManager.AppSettings["Mail"]),
                                                 Config.Cs(System.Configuration.ConfigurationManager.AppSettings["AdminMail"]),
                                                 "Tims Ali",
                                                 "Messages: " + Messages + " <br/> Ip: " + HttpContext.Current.Request.UserHostAddress + " Url: " + HttpContext.Current.Request.Url.ToString() + " Datetime: " + DateTime.Now.ToShortDateString()
                                                 );

            SmtpClient SmtpMail = new SmtpClient(Config.Cs(System.Configuration.ConfigurationManager.AppSettings["MailSmtp"]));
            SmtpMail.Credentials = new NetworkCredential(Config.Cs(System.Configuration.ConfigurationManager.AppSettings["Mail"]), Config.Cs(System.Configuration.ConfigurationManager.AppSettings["MailPassword"]));
            MailSrv.BodyEncoding = System.Text.Encoding.UTF8;
            MailSrv.Priority = System.Net.Mail.MailPriority.Normal;
            MailSrv.IsBodyHtml = true;

            SmtpMail.Send(MailSrv);
            return Utils.MethodType.Succes;
        }
        catch
        {
            return Utils.MethodType.Error;
        }
    }
    public static Utils.MethodType SendMailFeedBack(string Messages, System.IO.Stream AttachFile = null, string AttachFileName = "")
    {
        try
        {
            //Mail gonder
            MailMessage MailSrv = new MailMessage(
                                                 Config.Cs(System.Configuration.ConfigurationManager.AppSettings["Mail"]),
                                                 Config.Cs(System.Configuration.ConfigurationManager.AppSettings["FeedBackMail"]),
                                                 "Tims Ali - FeedBack ",
                                                 Messages + " <br/> Ip: " + HttpContext.Current.Request.UserHostAddress + " Url: " + HttpContext.Current.Request.Url.ToString() + " Datetime: " + DateTime.Now.ToShortDateString()
                                                );


            if (AttachFile != null)
            {
                MailSrv.Attachments.Add(new Attachment(AttachFile, "Attachment 1 - " + AttachFileName));
            }


            SmtpClient SmtpMail = new SmtpClient(Config.Cs(System.Configuration.ConfigurationManager.AppSettings["MailSmtp"]));
            SmtpMail.Credentials = new NetworkCredential(Config.Cs(System.Configuration.ConfigurationManager.AppSettings["Mail"]), Config.Cs(System.Configuration.ConfigurationManager.AppSettings["MailPassword"]));
            MailSrv.BodyEncoding = System.Text.Encoding.UTF8;
            MailSrv.Priority = System.Net.Mail.MailPriority.Normal;
            MailSrv.IsBodyHtml = true;

            SmtpMail.Send(MailSrv);
            return Utils.MethodType.Succes;
        }
        catch
        {
            return Utils.MethodType.Error;
        }
    }
    //public static string SendMail(string Subject, string Body, string EmailTo)
    //{
    //    try
    //    {
    //        //Mail gonder
    //        MailMessage MailSrv = new MailMessage(
    //                                             Config.Cs(System.Configuration.ConfigurationManager.AppSettings["Mail"]),
    //                                             EmailTo,
    //                                             Subject,
    //                                            Body
    //                                            );

    //        SmtpClient SmtpMail = new SmtpClient(Config.Cs(System.Configuration.ConfigurationManager.AppSettings["MailSmtp"]));
    //        SmtpMail.Credentials = new NetworkCredential(Config.Cs(System.Configuration.ConfigurationManager.AppSettings["Mail"]), Config.Cs(System.Configuration.ConfigurationManager.AppSettings["MailPassword"]));
    //        MailSrv.BodyEncoding = System.Text.Encoding.UTF8;
    //        MailSrv.Priority = System.Net.Mail.MailPriority.Normal;
    //        MailSrv.IsBodyHtml = true;

    //        SmtpMail.Send(MailSrv);
    //        return "1";
    //    }
    //    catch (Exception ex)
    //    {
    //        string fileName = DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
    //        string Error = string.Format("{0}-{1}-{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), HttpContext.Current.Request.UserHostAddress, ex.Message);
    //        string path = HttpContext.Current.Server.MapPath("~/Logs/" + fileName);
    //        System.IO.File.AppendAllText(path, Error + "\r\n");
    //        return "0";
    //    }
    //}

    //public static void isAdminLogin(Page p)
    //{
    //    if (!DALC.ManagerInfo.isLogin)
    //    {
    //        //HttpContext.Current.Response.Redirect("/", true);
    //        string TARGET_URL = "/adminn/?url=" + HttpContext.Current.Request.Url.ToString();
    //        if (p.IsCallback)
    //            DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(TARGET_URL);
    //        else
    //            HttpContext.Current.Response.Redirect(TARGET_URL);
    //    }
    //}
    public static void isUserLogin(Page p)
    {
        //if (!DALC.ManagerInfo.isLogin)
        //{
        //    HttpContext.Current.Response.Redirect("/?return=" + HttpContext.Current.Request.Url, true);
        //}
        // System.Threading.Thread.Sleep(10000);
        if (!DALC.UserInfo.isLogin)
        {
            //  HttpContext.Current.Response.Redirect("/", true);
            string TARGET_URL = "/user/default.aspx?return=" + HttpContext.Current.Request.Url.ToString();
            if (p.IsCallback)
                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(TARGET_URL);
            else
                HttpContext.Current.Response.Redirect(TARGET_URL);
        }
    }
    public static void isLogin(Page p)
    {
        //if (!DALC.ManagerInfo.isLogin)
        //{
        //    HttpContext.Current.Response.Redirect("/?return=" + HttpContext.Current.Request.Url, true);
        //}
        // System.Threading.Thread.Sleep(10000);
        if (!DALC.ManagerInfo.isLogin)
        {
            //  HttpContext.Current.Response.Redirect("/", true);
            string TARGET_URL = "/admin/default.aspx?return=" + HttpContext.Current.Request.Url.ToString();
            if (p.IsCallback)
                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(TARGET_URL);
            else
                HttpContext.Current.Response.Redirect(TARGET_URL);
        }
        else
        {

            DALC _db = new DALC();
            _db.ManagerLastDtUpdate();
        }
    }
    /// <summary>
    /// eyer verilen seyfede gosderilen huquqa (insert,delete ,...) malik olmazsa /access seyfesine yonlendirelecey
    /// </summary>
    /// <param name="_table"></param>
    /// <param name="_type"></param>
    public static void checkPermission(Utils.Tables _table, Utils.LogType _type)
    {
        string acces_link = "/admin/access";
        DALC _db = new DALC();
        Utils.PermissionTables _data = _db.GetPermissionTable(_table);
        switch (_type)
        {
            case Utils.LogType.select: if (!_data.isSelect) Rd(acces_link); break;

            case Utils.LogType.insert: if (!_data.isInsert) Rd(acces_link); break;
            case Utils.LogType.update: if (!_data.isUpdate) Rd(acces_link); break;
            case Utils.LogType.delete: if (!_data.isDelete) Rd(acces_link); break;
            //case Utils.LogType.insert:
            //case Utils.LogType.update:
            //case Utils.LogType.delete:
            //    {
            //        System.Threading.Thread.Sleep(10000);
            //        Rd("/error");
            //    }
            //    break;


            default:
                break;
        }


    }
    public static string ReplaceAnwers(string AnswerText)
    {
        string src = "";
        string returnVal = "";
        string[] array = AnswerText.Split(new char[] { '{', '}' });

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].IndexOf("img:[") > -1)
            {
                src = array[i].Split(new char[] { '[', ']' })[1];
                break;
            }
        }

        if (src.Length > 1)
        {
            returnVal = AnswerText.Replace("{img:[" + src + "]}", "<img src='" + src + "'>");
        }
        else
        {
            returnVal = AnswerText;
        }
        return returnVal;
    }
    public static string _EncryptString(string text)
    {

        //key;userid ; dateime ticks ;key
        string _key = Cryptography.Encrypt(string.Format("{0};{1};{0}", DateTime.Now.Ticks, text));

        return _key;
    }

    public static string GetPageName()
    {
        //aspx -in adini verir
        //string sPath = HttpContext.Current.Request.Url.AbsolutePath;
        //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        //string sRet = oInfo.Name;
        //return sRet;

        return HttpContext.Current.Request.Url.AbsolutePath;

    }

    public static object DateFormatClear(string Date)
    {
        //Clear
        Date = Date.Trim();
        Date = Date.Replace(",", ".");
        Date = Date.Replace("+", ".");
        Date = Date.Replace("/", ".");
        Date = Date.Replace("-", ".");
        Date = Date.Replace("*", ".");
        Date = Date.Replace("\\", ".");
        Date = Date.Replace(" ", ".");

        if (!IsNumeric(Date.Replace(".", "")))
            return null;

        string[] DtSplit = Date.Split('.');

        if (DtSplit.Length != 3)
            return null;

        //İli 2 simvol olsa yanına 20 artıq. 3 minici ilə qədər gedər :)
        if (Config.Cs(DtSplit[2]).Length == 2)
            DtSplit[2] = "20" + Config.Cs(DtSplit[2]);

        if (Config.Cs(DtSplit[2]).Length == 1)
            DtSplit[2] = "200" + Config.Cs(DtSplit[2]);

        try
        {
            DateTime Dt = new DateTime(Convert.ToInt16(DtSplit[2]),
                Convert.ToInt16(DtSplit[1]),
                Convert.ToInt16(DtSplit[0])
                );

            return Dt;
        }
        catch
        {
            return null;
        }
    }

    public static int nullStringToInt(object value)
    {
        if (Cs(value) == "")
        {
            return 0;
        }
        return int.Parse(value.ToString());
    }
    public static string GetPassSeriaAze
    {
        get
        {
            return "AZE";
        }

    }
    /// <summary>
    /// 2014/2015 formatda geri qaytarir , sentyabr ayinda yeni novbeti ile kecir
    /// </summary>
    public static string GetEduYearStr
    {
        get
        {
            string _eduYear = "";
            //eyer sentyabr ayidisa 
            if (DateTime.Now.Month >= 9)
            {
                _eduYear = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Year + 1);
            }
            else
            {
                _eduYear = string.Format("{0}/{1}", DateTime.Now.Year - 1, DateTime.Now.Year);
            }
            return _eduYear;
        }
    }
    /// <summary>
    /// cari ders ilini (misal: 2015)  geri qaytarir , sentyabr ayinda yeni novbeti ile kecir
    /// </summary>
    public static int GetEduYear
    {
        get
        {
            //eyer sentyabr ayidisa 
            if (DateTime.Now.Month >= 9)
            {
                return DateTime.Now.Year + 1;
            }
            else
            {
                return DateTime.Now.Year;
            }
        }
    }
    /// <summary>
    /// Methods to remove HTML from strings.
    /// </summary>
    public static class HtmlRemoval
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
