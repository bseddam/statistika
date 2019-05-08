using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for Subscribe
/// </summary>
public class Subscriber
{
    public Subscriber()
    {

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public string Email { get; set; }
    public string UnsubscribeURL { get; set; }
}
public class SubscribeContent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public Utils.PageType Type { get; set; }
    public string URL
    {
        get
        {
            string website = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"];
            string _url = "";
            switch (Type)
            {
                case Utils.PageType.News:
                    _url = string.Format("{0}/az/news/{1}", website, Id);
                    break;
                case Utils.PageType.Videos:
                    _url = string.Format("{0}/az/videos/{1}", website, Id);
                    break;
                case Utils.PageType.Tedqiqatlar:
                    _url = string.Format("{0}/az/research/{1}/{2}", website, Id, Config.Slug(Name));
                    break;
                case Utils.PageType.Nesrler:
                    _url = string.Format("{0}/az/publication/{1}/{2}", website, Id, Config.Slug(Name));
                    break;
                case Utils.PageType.Slider:
                    _url = string.Format("{0}/az/publication/{1}/{2}", website, Id, Config.Slug(Name));
                    break;

                default:
                    _url = website;
                    break;

            }
            return _url;
        }
    }
}
public class SubscribeEmailSender
{
    private List<Subscriber> Subscribers { get; set; }
    private SubscribeContent SubscribeContent { get; set; }

    public SubscribeEmailSender(SubscribeContent content)
    {

        this.SubscribeContent = content;
        GetSubscribersFromDb();
    }

    private void GetSubscribersFromDb()
    {
        Subscribers = new List<Subscriber>();
        DALC _db = new DALC();
        DataTable dt = _db.GetSubscribers();
        if (dt == null)
        {
            return;
        }
        string website = GetValueFromConfig("WebSiteURL");
        foreach (DataRow item in dt.Rows)
        {
            Subscriber person = new Subscriber();
            person.Email = item["email"].ToParseStr();
            person.FatherName = item["patronymic"].ToParseStr();
            person.Id = item["id"].ToParseInt();
            person.Name = item["name"].ToParseStr();
            person.Surname = item["surname"].ToParseStr();
            person.UnsubscribeURL = string.Format("{0}/az/unsubscribe?token={1}",
                                  website,
                                  Cryptography.Encrypt(item["id"].ToParseStr()));
            Subscribers.Add(person);
        }
    }
    public void Send()
    {
        foreach (Subscriber item in Subscribers)
        {
            string template = GetEmailTemplate();

            template = template.Replace("{url}", GetValueFromConfig("WebSiteURL"));
            template = template.Replace("{content}", SubscribeContent.Content);
            template = template.Replace("{title}", SubscribeContent.Name);
            template = template.Replace("{content-url}", SubscribeContent.URL);
            template = template.Replace("{unsubscribe-url}", item.UnsubscribeURL);

            SendMail(item.Email, "Yeni məzmun əlavə olundu", template);
        }
    }
    private string GetEmailTemplate()
    {
        StreamReader rd = new StreamReader(HttpContext.Current.Server.MapPath("~/email-templates/subscribe-content.html"));
        string content = rd.ReadToEnd();
        rd.Close();


        return content;
    }
    private string GetValueFromConfig(string key)
    {
        return System.Configuration.ConfigurationManager.AppSettings[key];
    }


    private int SendMail(string ToEmail, string Title, string Message)
    {
        try
        {
            string fromEmail = GetValueFromConfig("MailSubScribe");
            string passEmail = GetValueFromConfig("MailSubScribePassword");
            string smtp = GetValueFromConfig("MailSubScribeSmtp");


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
        catch (Exception ex)
        {
            (new DALC()).LogInsert(Utils.Tables.users,
                Utils.LogType.insert,
                "SendEmail",
                " ex.Message: " + ex.Message + " - ex.InnerException : " + ex.InnerException,
                "",
                true);
            return -1;
        }
    }
}

