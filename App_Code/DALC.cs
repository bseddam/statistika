using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Net.Mail;
using System.Net;
using MySql.Data.MySqlClient;
using System.Web.UI;

/// <summary>
/// Summary description for DALC_Student
/// </summary>
public class DALC
{
    public static MySqlConnection SqlConn
    {
        get { return new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString); }
    }

    public string GetSingleValue(Utils.Tables Table, string Columns, string Where, string Value)
    {
        MySqlCommand com = new MySqlCommand("select " + Columns + " from " + Table + " Where " + Where + "=@Values", SqlConn);
        try
        {
            com.Connection.Open();
            com.Parameters.AddWithValue("@Values", Value);
            return Config.Cs(com.ExecuteScalar());
        }
        catch (Exception ex)
        {
            LogInsert(Table, Utils.LogType.select, "GetSingleValue()", ex.Message, "", true);
            return "-1";
        }
        finally
        {
            com.Connection.Close();
        }
    }

    public DataTable GetListQebulDt()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));

        DataRow dr;
        for (int i = 0; i < 10; i++)
        {
            dr = dt.NewRow();
            dr["ID"] = DateTime.Now.AddYears(-i).Year;
            dr["Name"] = (DateTime.Now.AddYears(-i).Year).ToString();
            dt.Rows.Add(dr);
        }
        return dt;
    }

    /// <summary>
    /// table ve ya view-daki melumat sayi
    /// </summary>
    /// <param name="_table"></param>
    /// <returns></returns>
    public string GetCount(Utils.Tables _table)
    {
        DataTable dt = new DataTable();
        MySqlCommand cmd = new MySqlCommand("SELECT Count(ID) FROM  " + _table.ToString(), SqlConn);
        try
        {
            cmd.Connection.Open();
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            LogInsert(_table, Utils.LogType.select, String.Format("GetCount (_table {0}) ", _table.ToString()), ex.Message, "", false);
            return "0";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #region Manager

    public class StrukturManagerInfo
    {
        public int ID { get; set; }
        public bool isLogin { get; set; }
        public int GroupID { get; set; }

        public string Login { get; set; }

        public int Number { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", Surname, Name, Patronymic);
            }
        }
        //public int EduTypeID { get; set; }

        public Utils.ManagersGroup Group { get; set; }

    }

    public static StrukturManagerInfo ManagerInfo
    {
        get
        {
            StrukturManagerInfo data = new StrukturManagerInfo();
            data.isLogin = false;
            data.ID = -1;

            if (HttpContext.Current.Session["ManagerInfoStruktur"] != null)
            {
                return (StrukturManagerInfo)HttpContext.Current.Session["ManagerInfoStruktur"];
            }



            string ManagerID = Convert.ToString(HttpContext.Current.Session["ManagerID"]);

            DataTable dt = new DataTable();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT   id 
                                , group_id                            
                                , name 
                                , surname 
                                , patronymic 
                                , number 
                                , pin 
                                , login
      
                            FROM v_Managers  where id=@id", SqlConn);

                da.SelectCommand.Parameters.AddWithValue("@id", ManagerID);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                new DALC().LogInsert(Utils.Tables.Managers, Utils.LogType.select, String.Format("ManagerInfo"), ex.Message, true);
                dt = null;
            }

            if (dt == null || dt.Rows.Count < 1) return data;

            DataRow dr = dt.Rows[0];

            if (dr == null) return data;

            data.isLogin = true;
            data.ID = int.Parse(Convert.ToString(dr["id"]));

            data.GroupID = int.Parse(Convert.ToString(dr["group_id"]));

            data.Group = (Utils.ManagersGroup)Enum.Parse(typeof(Utils.ManagersGroup), data.GroupID.ToString());

            data.Login = Convert.ToString(dr["login"]);
            data.Name = Convert.ToString(dr["name"]);
            data.Surname = Convert.ToString(dr["surname"]);
            data.Patronymic = Convert.ToString(dr["patronymic"]);

            //data.Number = int.Parse(Convert.ToString(dr["number"]));
            //data.Pin = Convert.ToString(dr["pin"]);


            HttpContext.Current.Session["ManagerInfoStruktur"] = data;

            return data;
        }
    }

    public string ChechkManager(string PassPin)
    {

        MySqlCommand cmd = new MySqlCommand("SELECT count(id)  FROM Managers where Pin=@Pin ", SqlConn);
        cmd.Parameters.AddWithValue("@Pin", PassPin);

        try
        {
            cmd.Connection.Open();
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.select,
                String.Format("ChechkManager ( PassPin: {0} ) ",
                PassPin),
                ex.Message, "", true);
            return "-1";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public string ChechkManagerLogin(string Login)
    {

        MySqlCommand cmd = new MySqlCommand("SELECT count(id)  FROM Managers where Login=@Login ", SqlConn);
        cmd.Parameters.AddWithValue("@Login", Login);

        try
        {
            cmd.Connection.Open();
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.select,
                String.Format("ChechkManagerLogin ( Login: {0} ) ",
                Login),
                ex.Message, "", true);
            return "-1";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    /// <summary>
    /// login ve parola uygun melumat
    /// </summary>
    /// <param name="Login"></param>
    /// <param name="Pass"></param>
    /// <returns></returns>
    public string GetManager(string Login, string Pass)
    {
        MySqlCommand cmd = new MySqlCommand("SELECT  ID    FROM managers where login=@login and password=@password and is_active=1", SqlConn);
        cmd.Parameters.AddWithValue("@Login", Login);
        cmd.Parameters.AddWithValue("@password", Config.Sha1(Pass));
        try
        {
            cmd.Connection.Open();
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.select, String.Format("GetManager ( Login: {0} ) ", Login), ex.Message, "", true);
            return "-1";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    /// <summary>
    /// managerid -e uygun melumatlari getirir
    /// </summary>
    /// <param name="managerID"></param>
    /// <returns></returns>
    public DataTable GetManager(int managerID)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Managers WHERE ID=@ManagerID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@ManagerID", managerID);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_Managers, Utils.LogType.select,
                String.Format("GetManager(managerID:{0})", managerID), ex.Message, "", true);
            return null;
        }
    }

    public string GetManager(string Pin)
    {
        MySqlCommand cmd = new MySqlCommand("SELECT  ID  FROM Managers where Pin=@Pin and IsActive=1", SqlConn);
        cmd.Parameters.AddWithValue("@Pin", Pin);

        try
        {
            cmd.Connection.Open();
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.select, String.Format("GetManager ( Pin: {0} ) ", Pin), ex.Message, "", true);
            return "-1";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public DataTable GetManagers()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  ROW_NUMBER() over (order by LastLoginDt desc) as No,ID , GroupName ,  Login , Surname +' '+Name+' '+ Patronymic  as SAA 
                                                        , Mail, Tel , Description,ActiveText  ,case when LastLoginDt>DATEADD(minute,-3,GETDATE()) then 'on.gif' else 'off.png' end  as Online  ,LastLoginDt
                                                      FROM v_Managers", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_Managers, Utils.LogType.select,
                String.Format("GetManagers()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetListManagers()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT ID
                                                      ,GroupName
                                                      ,EduName
                                                      ,Login     
                                                      ,Surname+' '+ Name+' '+ Patronymic as SAA     
                                                  FROM v_Managers order by GroupName,Login", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_Managers, Utils.LogType.select,
                String.Format("GetManagers()"), ex.Message, "", true);
            return null;
        }
    }

    //    public DataTable GetManagerPermissionUniList()
    //    {
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT ID ,Name
    //                                                    FROM v_ManagerPermissionUniList
    //                                                    WHERE ManagerID=@ManagerID and  EduTypeID=@EduTypeID
    //                                                    ORDER BY Code", SqlConn);
    //            da.SelectCommand.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);
    //            //da.SelectCommand.Parameters.AddWithValue("@EduTypeID", selectedEduTypeID);

    //            da.Fill(dt);
    //            return dt;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogInsert(Utils.Tables.ManagersPermissionUni, Utils.LogType.select,
    //                String.Format("GetManagerPermissionUniList()"), ex.Message, "", true);
    //            return null;
    //        }
    //    }


    public String GetManagerPermissionDefaultUniID
    {
        get
        {
            MySqlCommand cmd = new MySqlCommand(@"declare  @uniID int 

                                    select @uniID= isnull([LastShowUniID],0) from Managers where id=@ManagerID

                                    if @uniID =0
	                                    SELECT  top (1) u.ID 
	                                    FROM ListUniversity as u
	                                    INNER JOIN ManagersPermissionUni as m on m.UniID= u.ID
	                                    WHERE m.ManagerID=@ManagerID and u.isdeleted=0
	                                    ORDER BY u.Code

	                                    else 
	                                    select @uniID ", SqlConn);
            cmd.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                LogInsert(Utils.Tables.v_MenuPermission, Utils.LogType.select, String.Format("GetMenuDefaultPageUrl () "), ex.Message, "", true);
                return null;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }
    }
    public String GetManagerPermissionDefaultEduTypeID
    {
        get
        {
            MySqlCommand cmd = new MySqlCommand(@"declare  @eduTypeID int 

                                    select @eduTypeID= isnull(LastEduTypeID,0) from Managers where id=@ManagerID

                                    if @eduTypeID =0
	                                    select top (1) ID from ListEduType
										where ID in (select cast(item as int) from dbo.SplitString((select EduTypeID from Managers where ID=@ManagerID),',')) 
										order by Priority

	                                    else 
	                                    select @eduTypeID", SqlConn);
            cmd.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                LogInsert(Utils.Tables.v_MenuPermission, Utils.LogType.select, String.Format("GetMenuDefaultPageUrl () "), ex.Message, "", true);
                return null;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }
    }




    public Utils.MethodType ManagerPassUpdate(string Pass,string Login)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE Managers   SET login=@login, Password =@Password    WHERE id=@ID", SqlConn);
        cmd.Parameters.AddWithValue("@Password", Config.Sha1(Pass));
        cmd.Parameters.AddWithValue("@login", Login);
        cmd.Parameters.AddWithValue("@ID", DALC.ManagerInfo.ID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.update, String.Format("ManagerPassUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType ManagerLastDtUpdate()
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE Managers   SET Last_Login_Dt =@date   WHERE id=@ID", SqlConn);
        cmd.Parameters.AddWithValue("@date", DateTime.Now);
        cmd.Parameters.AddWithValue("@ID", DALC.ManagerInfo.ID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.update, String.Format("ManagerLastDtUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public Utils.MethodType ManagerInsert(string GroupID, string UniIDs, string Login, string Password,
        string Name, string Surname, string Patronymic, string Number, string Pin, string BirthDt,
        string BirthCity, string IamasAddress, string Address, string IamasSex, string Mail, string Tel,
        string Description, string MenuIDs, string MySqlPermTable, string EduTypeID)
    {

        MySqlCommand cmd = new MySqlCommand("ManagerInsert", SqlConn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);
        cmd.Parameters.AddWithValue("@GroupID", GroupID);

        cmd.Parameters.AddWithValue("@Login", Login);
        cmd.Parameters.AddWithValue("@Password", Config.Sha1(Password));
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Surname", Surname);
        cmd.Parameters.AddWithValue("@Patronymic", Patronymic);
        cmd.Parameters.AddWithValue("@Number", Number);
        cmd.Parameters.AddWithValue("@Pin", Pin);
        cmd.Parameters.AddWithValue("@BirthDt", BirthDt);
        cmd.Parameters.AddWithValue("@BirthCity", BirthCity);
        cmd.Parameters.AddWithValue("@IamasAddress", IamasAddress);
        cmd.Parameters.AddWithValue("@Address", Address);
        cmd.Parameters.AddWithValue("@IamasSex", IamasSex);
        cmd.Parameters.AddWithValue("@Mail", Mail);
        cmd.Parameters.AddWithValue("@Add_Ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@Tel", Tel);
        cmd.Parameters.AddWithValue("@Description", Description);
        cmd.Parameters.AddWithValue("@MenuIDs", MenuIDs);


        cmd.Parameters.AddWithValue("@EduTypeID", EduTypeID);
        cmd.Parameters.AddWithValue("@MySqlPermTable", MySqlPermTable);
        cmd.Parameters.AddWithValue("@UniIDs", UniIDs);



        try
        {

            cmd.Connection.Open();

            //MySql prosedurun icinde catch blokuna dushse 0 qaytarir 
            //her sey niormal gederse insert olunan meumatin id-sin getirir

            string value = Config.Cs(cmd.ExecuteScalar());

            if (value != "0")
            {
                LogInsert(Utils.Tables.Managers, Utils.LogType.insert, String.Format("ManagerInsert ( PassNum: {0}  )", Number), "",
                       String.Format("Yeni istifadəçi əlavə olundu ( ID: {0} )", value), false);

                return Utils.MethodType.Succes;
            }

            return Utils.MethodType.Error;

        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.insert, String.Format("ManagerInsert ( PassNum: {0}  )", Number), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public Utils.MethodType ManagerUpdate(string GroupID, string UniIDs, string Password,
       string Name, string Surname, string Patronymic,
       string BirthCity, string IamasAddress, string Address, string Mail, string Tel,
       string Description, string MySqlPermMenu, string EduTypeID, string DataID, string Number, string Pin, string BirthDt)
    {

        MySqlCommand cmd = new MySqlCommand("ManagerUpdate", SqlConn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Number", Number);
        cmd.Parameters.AddWithValue("@Pin", Pin);
        cmd.Parameters.AddWithValue("@BirthDt", BirthDt);


        cmd.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);
        cmd.Parameters.AddWithValue("@DataID", DataID);

        cmd.Parameters.AddWithValue("@GroupID", GroupID);
        cmd.Parameters.AddWithValue("@Password", Password);
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Surname", Surname);
        cmd.Parameters.AddWithValue("@Patronymic", Patronymic);


        cmd.Parameters.AddWithValue("@BirthCity", BirthCity);
        cmd.Parameters.AddWithValue("@IamasAddress", IamasAddress);
        cmd.Parameters.AddWithValue("@Address", Address);

        cmd.Parameters.AddWithValue("@Mail", Mail);
        cmd.Parameters.AddWithValue("@Update_Ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@Tel", Tel);
        cmd.Parameters.AddWithValue("@Description", Description);



        cmd.Parameters.AddWithValue("@EduTypeID", EduTypeID);
        cmd.Parameters.AddWithValue("@MySqlPermMenu", MySqlPermMenu);
        cmd.Parameters.AddWithValue("@UniIDs", UniIDs);



        try
        {

            cmd.Connection.Open();

            //MySql prosedurun icinde catch blokuna dushse 0 qaytarir 
            //her sey niormal gederse insert olunan meumatin id-sin getirir

            string value = Config.Cs(cmd.ExecuteScalar());

            if (value != "0")
            {
                LogInsert(Utils.Tables.Managers, Utils.LogType.update, String.Format("ManagerUpdate ( ID: {0}  )", DataID), "",
                       String.Format("Yeni istifadəçi əlavə olundu ( ID: {0} )", value), false);

                return Utils.MethodType.Succes;
            }

            return Utils.MethodType.Error;

        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.update, String.Format("ManagerUpdate ( ID: {0}  )", DataID), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }



    public Utils.MethodType ManagerActivity(int ManagerID)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE Managers   SET IsActive=(case when IsActive = 0 then 1 else 0 end)   WHERE id=@ID", SqlConn);

        cmd.Parameters.AddWithValue("@ID", ManagerID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Managers, Utils.LogType.update, String.Format("ManagerActivity ({0}) ", ManagerID), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    #endregion

    #region ManagerGroup

    public DataTable GetListmanagerGroup()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT ID,case  when  len(Description)>1 then Name  +' ( '+Description+' )' else Name end  as Name  FROM ManagersGroup", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.ManagersGroup, Utils.LogType.select, String.Format("GetListmanagerGroup()"), ex.Message, "", true);
            return null;
        }
    }


    #endregion

    #region Logs
    public DataTable GetLogs()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  row_number() over (order by ID Desc) as No, ID 
                                                              , TableName 
                                                              , TypeName 
                                                              , MethodName 
                                                              , Text 
                                                              , TextShowing 
                                                              , isError 
                                                              , AddDt 
                                                              , Ip 
                                                              , ManagerID 
                                                              , LogTableID 
                                                              , LogTypeID 
                                                              , ManagerLogin 
                                                              , ManagerSAA 
                                                          FROM v_Logs
                                                          where  isError =0", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Logs, Utils.LogType.select, String.Format("GetLogs()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetLogs(string DateBegin, string DateEnd, int ManagerID = 0)
    {
        string _where = "";

        if (ManagerID != 0)
        {
            _where += " and ManagerID=@ManagerID ";
        }



        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT   row_number() over (order by ID Desc) as No, ID 
                                                              , TableName 
                                                              , TypeName 
                                                              , MethodName 
                                                              , Text 
                                                              , TextShowing 
                                                              , isError 
                                                              , AddDt 
                                                              , Ip 
                                                              , ManagerID 
                                                              , LogTableID 
                                                              , LogTypeID 
                                                              , ManagerLogin 
                                                              , ManagerSAA 
                                                          FROM v_Logs
                                                          where  isError=0 
                                and  AddDt between isnull(@DateBegin,'2000-01-01') and  isnull(@DateEnd,GETDATE()+1) " + _where, SqlConn);

            da.SelectCommand.Parameters.AddWithValue("DateBegin", DateBegin);
            da.SelectCommand.Parameters.AddWithValue("DateEnd", DateEnd);
            da.SelectCommand.Parameters.AddWithValue("ManagerID", ManagerID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Logs, Utils.LogType.select, String.Format("GetLogs()"), ex.Message, "", true);
            return null;
        }
    }

    public int LogInsert(Utils.Tables LogTable, Utils.LogType logType, string MethodName, string CatchError, string TextShowing, bool isError, bool SendEmail = false)
    {
        MySqlCommand cmd = new MySqlCommand(@"INSERT INTO  Logs 
                                              ( isError , ManagerID , LogTableID , LogTypeID , MethodName , Text , TextShowing , AddDt , Ip )
                                         VALUES
			                                 ( @isError , @ManagerID , @LogTableID , @LogTypeID , @MethodName , @Text , @TextShowing , @date, @Ip )"
                                         , SqlConn);
        cmd.Parameters.AddWithValue("@isError", isError);
        cmd.Parameters.AddWithValue("@ManagerID", DALC.ManagerInfo.ID);
        cmd.Parameters.AddWithValue("@LogTableID", (int)LogTable);
        cmd.Parameters.AddWithValue("@LogTypeID", (int)logType);
        cmd.Parameters.AddWithValue("@MethodName", MethodName);
        cmd.Parameters.AddWithValue("@Text", CatchError);
        cmd.Parameters.AddWithValue("@TextShowing", TextShowing);
        cmd.Parameters.AddWithValue("@date", DateTime.Now);

        cmd.Parameters.AddWithValue("@Ip", HttpContext.Current.Request.UserHostAddress);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return 1;
        }
        catch (Exception ex)
        {
            //if (SendEmail)
            //{
            //    Config.SendMailAdmin(string.Format("Diplom - metod : {0},catch : {1} ", MethodName, ex.Message));
            //}
            return 0;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    /// <summary>
    /// eyer manager id yoxdusa bununla bazaya log gondermey olar
    /// </summary>
    /// <param name="LogTable"></param>
    /// <param name="logType"></param>
    /// <param name="MethodName"></param>
    /// <param name="CatchError"></param>
    /// <param name="isError"></param>
    /// <returns></returns>
    public int LogInsert(Utils.Tables LogTable, Utils.LogType logType, string MethodName, string CatchError, bool isError)
    {
        MySqlCommand cmd = new MySqlCommand(@"INSERT INTO  Logs 
                                              ( isError , ManagerID , LogTableID , LogTypeID , MethodName , Text  , AddDt , Ip )
                                         VALUES
			                                 ( @isError , @ManagerID , @LogTableID , @LogTypeID , @MethodName , @Text  ,@date, @Ip )"
                                         , SqlConn);
        cmd.Parameters.AddWithValue("@isError", isError);
        cmd.Parameters.AddWithValue("@ManagerID", -1);
        cmd.Parameters.AddWithValue("@LogTableID", (int)LogTable);
        cmd.Parameters.AddWithValue("@LogTypeID", (int)logType);
        cmd.Parameters.AddWithValue("@MethodName", MethodName);
        cmd.Parameters.AddWithValue("@Text", CatchError);
        cmd.Parameters.AddWithValue("@date", DateTime.Now);

        cmd.Parameters.AddWithValue("@Ip", HttpContext.Current.Request.UserHostAddress);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return 1;
        }
        catch (Exception ex)
        {
            Config.SendMailAdmin("Diplom - " + ex.Message);
            return 0;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region List

    public DataTable GetList(string TableName)
    {

        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT ID ,Name  FROM " + TableName, SqlConn);

            da.Fill(dt);


            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GetList(Utils.Tables TableName)
    {
        string _cacheName = TableName.ToString();
        if (HttpRuntime.Cache[_cacheName] != null)
        {
            return HttpRuntime.Cache[_cacheName] as DataTable;
        }

        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT ID ,Name  FROM " + TableName.ToString() + " order by Priority", SqlConn);

            da.Fill(dt);

            HttpRuntime.Cache[_cacheName] = dt;

            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(TableName, Utils.LogType.select, "GetList(TableName :" + TableName.ToString() + ")", ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetList(Utils.Tables TableName, Utils.OrderByColumns _column)
    {
        string _cacheName = string.Format("{0}_{1}", TableName, _column);

        if (HttpRuntime.Cache[_cacheName] != null)
        {
            return HttpRuntime.Cache[_cacheName] as DataTable;
        }
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT ID ,Name  FROM  " + TableName.ToString() + " order by " + _column.ToString(), SqlConn);

            da.Fill(dt);
            HttpRuntime.Cache[_cacheName] = dt;
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(TableName, Utils.LogType.select, "GetList(TableName :" + TableName.ToString() + ")", ex.Message, "", true);
            return null;
        }
    }




    #endregion

    #region Menu client

    public DataTable GetMenuClient(Utils.MenuType menuType, string lang, int parentId = 0)
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  Id,name_" + lang + " as Name,URL,SubCount FROM v_menu as m  where m.typeid=@typeid and parentId=@parentId order by priority ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeid", (int)menuType);
            da.SelectCommand.Parameters.AddWithValue("parentId", parentId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMenuClient  "), ex.Message, "", true);
            return null;
        }
    }


    public DataTable GetMenuClient()
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * FROM v_menu as m  order by typeid,priority ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMenuClient  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType MenuClientUpdate(int id, int priority, int parentId)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE menu SET parentId=@parentId,updateip=@ip,updatedt=@dt,priority=@priority WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@parentId", parentId);
        cmd.Parameters.AddWithValue("@priority", priority);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MenuClientUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MenuClientUpdate(int id, string url, int typeid, int parentId, string name_az, string name_en, int priority)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE menu SET Url=@url,
                                                            typeId=@typeid,
                                                            parentId=@parentId,
                                                            priority=@priority,
                                                            name_az=@name_az,
                                                            name_en=@name_en,
                                                            updateip=@ip,
                                                            updatedt=@dt WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@url", url);
        cmd.Parameters.AddWithValue("@typeid", typeid);
        cmd.Parameters.AddWithValue("@parentId", parentId);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@priority", priority);
        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MenuClientUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MenuClientDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE menu SET isActive=0,updatedt=@dt,updateip=@ip WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MenuClientUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType MenuClientInsert(int typeid, int parentid, string Name_az, string Name_en, string URL, int Priority)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into menu (typeid,parentid,Name_az,Name_en,URl,AddDt,AddIp,isActive,Priority) 
                            values(@typeid,@parentid,@Name_az,@Name_en,@URl,@AddDt,@AddIp,1,@Priority)", SqlConn);

        cmd.Parameters.AddWithValue("@typeid", typeid);
        cmd.Parameters.AddWithValue("@parentid", parentid);
        cmd.Parameters.AddWithValue("@Name_az", Name_az);
        cmd.Parameters.AddWithValue("@Name_en", Name_en);
        cmd.Parameters.AddWithValue("@URl", URL);
        cmd.Parameters.AddWithValue("@Priority", Priority);
        cmd.Parameters.AddWithValue("@AddIp", HttpContext.Current.Request.UserHostAddress);

        cmd.Parameters.AddWithValue("@AddDt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MenuClientUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region AdminMenu

    public DataTable GetListAdminMenu(int SubID)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  ID,Name,TableIDs FROM Menu  where isActive=1 and SubID=@SubID order by priority ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@SubID", SubID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.select, String.Format("GetListMenu ()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetAdminMenu(int SubID)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  ID
                                                        ,Sub_ID
                                                        ,Name
                                                        ,Icon
                                                        ,Url
                                                        ,Priority
                                                        ,is_New
                                                        ,(select COUNT(ID) from Menu where Sub_ID=m.ID and is_Active=1 ) as SubCount
                                                    FROM admin_menu as m
                                                    where is_Active=1 and Sub_ID=@SubID order by priority ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@SubID", SubID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.select, String.Format("GetMenu ( SubID: {0} ) ", SubID), ex.Message, "", true);
            return null;
        }
    }
    public String GetAdminMenuName(string url)
    {
        MySqlCommand cmd = new MySqlCommand("SELECT top (1) Name  FROM admin_Menu  where Url like @url ", SqlConn);
        cmd.Parameters.AddWithValue("@url", "%" + url + "%");

        try
        {
            cmd.Connection.Open();
            string name = Config.Cs(cmd.ExecuteScalar());
            return name;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.select, String.Format("GetAdminMenuName ( url: {0} ) ", url), ex.Message, "", true);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetAdminMenuPermission()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  ID ,  Sub_ID , Name , Icon , Url , is_New ,SubCount 
                                            FROM v_Menu_Permission where Manager_ID=@ManagerID  
                                            order by priority", SqlConn);

            da.SelectCommand.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_MenuPermission, Utils.LogType.select, String.Format("GetMenuPermission () "), ex.Message, "", true);
            return null;
        }
    }
    public String GetAdminMenuDefaultPageUrl
    {
        get
        {
            MySqlCommand cmd = new MySqlCommand(@"SELECT  top 1 case Url when '#' then (select top 1 url from Menu where SubID=m.ID ) else Url end as Url
                                            FROM v_MenuPermission as m where ManagerID=@ManagerID  
                                            order by priority ", SqlConn);
            cmd.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                LogInsert(Utils.Tables.v_MenuPermission, Utils.LogType.select, String.Format("GetMenuDefaultPageUrl () "), ex.Message, "", true);
                return null;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }
    }
    public DataTable GetMenuPermissionbyManager(string ManagerID)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT MenuID,TbInsert,TbUpdate,TbDelete
                                                      FROM ManagersPermissionMenu
                                                      where ManagerID=@ManagerID", SqlConn);

            da.SelectCommand.Parameters.AddWithValue("@ManagerID", ManagerID);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_MenuPermission, Utils.LogType.select, String.Format("GetMenuPermission () "), ex.Message, "", true);
            return null;
        }
    }

    #endregion

    #region Permission



    public Utils.PermissionTables GetPermissionTable(Utils.Tables _table)
    {

        string _val = string.Format("PermissionTable{0}_{1}", ManagerInfo.ID, _table.ToString());

        //if (HttpRuntime.Cache[_val] != null)
        //{
        //    return (Utils.PermissionTables)HttpRuntime.Cache[_val];
        //}


        Utils.PermissionTables _data = new Utils.PermissionTables();

        _data.isInsert = false;
        _data.isUpdate = false;
        _data.isDelete = false;
        _data.isSelect = false;

        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT ID,TbInsert,TbUpdate,TbDelete
                                                      FROM v_MenuPermission                                                       
                                                    WHERE ManagerID=@ManagerID and ','+TableIDs+',' like @TableID", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@ManagerID", ManagerInfo.ID);
            da.SelectCommand.Parameters.AddWithValue("@TableID", "%," + (int)_table + ",%");
            da.Fill(dt);

            if (dt == null || dt.Rows.Count == 0)
            {
                // HttpRuntime.Cache[_val] = _data;
                return _data;
            }


            _data.isInsert = Config.Cs(dt.Rows[0]["TbInsert"]) == "True" ? true : false;
            _data.isUpdate = Config.Cs(dt.Rows[0]["TbUpdate"]) == "True" ? true : false;
            _data.isDelete = Config.Cs(dt.Rows[0]["TbDelete"]) == "True" ? true : false;
            //eyer bura kimi gelibse dimeli hemin table-da duzeliw etmete imkan verilib
            _data.isSelect = true;

            // HttpRuntime.Cache[_val] = _data;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.ManagersPermissionTable, Utils.LogType.select, String.Format("GetPermissionTable (_table : {0})",
                _table.ToString()), ex.Message, "", true);
        }

        return _data;
    }



    #endregion

    #region Data Counts

    public string GetCountManager
    {
        get
        {
            MySqlCommand cmd = new MySqlCommand(@"SELECT Count(ID) FROM Managers where isactive=1 ", SqlConn);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                LogInsert(Utils.Tables.Managers, Utils.LogType.select, "GetCountManager () ", ex.Message, "", true);
                return "0";
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }
    }

    #endregion

    #region Static vlaues

    public DataTable GetStaticPages()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * FROM static_pages  order by `order`", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.static_values, Utils.LogType.select, String.Format("GetStaticValues  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetStaticValues()
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * FROM static_values  as m   order by m.`key`", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.static_values, Utils.LogType.select, String.Format("GetStaticValues  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetStaticValues(int pageId)
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * FROM static_values as m where page_id=@pageId    order by m.`key`", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("pageId", pageId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.static_values, Utils.LogType.select, String.Format("GetStaticValues  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType StaticValueUpdate(int id, string name_az, string name_en)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE static_values SET name_az=@name_az,name_en=@name_en WHERE Id=@id", SqlConn);

        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.static_values, Utils.LogType.update, String.Format("StaticValueUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public static string GetStaticValue(string key)
    {

        MySqlCommand cmd = new MySqlCommand(string.Format("SELECT name_{0} FROM static_values where `key`=@key", Config.getLangSession), SqlConn);

        try
        {
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("key", key);
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            (new DALC()).LogInsert(Utils.Tables.static_values, Utils.LogType.select, "GetStaticValue () ", ex.Message, "", true);
            return "";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }

    }
    

    public static string GetStaticValue_Url(string key)
    {

        MySqlCommand cmd = new MySqlCommand("SELECT name_az FROM static_values where `key`=@key", SqlConn);

        try
        {
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("key", key);
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            (new DALC()).LogInsert(Utils.Tables.static_values, Utils.LogType.select, "GetStaticValue () ", ex.Message, "", true);
            return "";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }

    }


    #endregion

    #region SiteMap

    public DataTable GetSiteMap(int parentId, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  Id,Name_" + lang + " as Name,URL,SuBCount FROM v_sitemap as m where parentId=@parentId  order by priority ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("parentId", parentId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_sitemap, Utils.LogType.select, String.Format("GetSiteMap  "), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetSiteMap()
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * FROM v_sitemap as m  order by priority ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_sitemap, Utils.LogType.select, String.Format("GetSiteMap  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType SiteMapUpdate(int id, int priority, int parentId)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE sitemap SET parentId=@parentId,updateip=@ip,updatedt=@dt,priority=@priority WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@parentId", parentId);
        cmd.Parameters.AddWithValue("@priority", priority);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.sitemap, Utils.LogType.update, String.Format("SiteMapUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType SiteMapUpdate(int id, string url, int parentId, string name_az, string name_en, int priority)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE sitemap SET Url=@url,
                                                                parentId=@parentId,
                                                                name_az=@name_az,
                                                                name_en=@name_en,
                                                                priority=@priority,
                                                                updateip=@ip,
                                                                updatedt=@dt
                                                                WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@url", url);
        cmd.Parameters.AddWithValue("@parentId", parentId);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@priority", priority);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.sitemap, Utils.LogType.update, String.Format("SiteMapUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType SitemapDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE sitemap SET isActive=0,updatedt=@dt,updateip=@ip WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.sitemap, Utils.LogType.update, String.Format("SitemapDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType SitemapInsert(int parentid, string Name_az, string Name_en, string URL, int Priority)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into sitemap (parentid,Name_az,Name_en,URl,AddDt,AddIp,isActive,Priority) 
                            values(@parentid,@Name_az,@Name_en,@URl,@AddDt,@AddIp,1,@Priority)", SqlConn);

        cmd.Parameters.AddWithValue("@parentid", parentid);
        cmd.Parameters.AddWithValue("@Name_az", Name_az);
        cmd.Parameters.AddWithValue("@Name_en", Name_en);
        cmd.Parameters.AddWithValue("@URl", URL);
        cmd.Parameters.AddWithValue("@Priority", Priority);
        cmd.Parameters.AddWithValue("@AddIp", HttpContext.Current.Request.UserHostAddress);

        cmd.Parameters.AddWithValue("@AddDt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.sitemap, Utils.LogType.insert, String.Format("SitemapInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Pages
    public DataTable GetPages_All(Utils.PageType PageType, int pageNum, int rowCount)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and page_id=0 order by page_dt desc limit @pageNum,@rowCount ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);
            da.SelectCommand.Parameters.AddWithValue("pageNum", pageNum * rowCount);
            da.SelectCommand.Parameters.AddWithValue("rowCount", rowCount);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPages_All  "), ex.Message, "", true);
            return null;
        }
    }
    public int GetPages_AllCount(Utils.PageType PageType)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  count(*) FROM pages where type_id=@typeId and page_id=0 order by page_dt  ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);

            da.Fill(dt);
            return dt.Rows[0][0].ToParseInt();
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPages_AllCount  "), ex.Message, "", true);
            return 0;
        }
    }
    public string getLastPageId
    {
        get
        {

            MySqlCommand cmd = new MySqlCommand(string.Format("SELECT id FROM pages order by id desc limit 1", Config.getLangSession), SqlConn);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                (new DALC()).LogInsert(Utils.Tables.pages, Utils.LogType.select, "getLastPageId () ", ex.Message, "", true);
                return "";
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

    }
    public DataTable GetTerefdaslarById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select p.*,pc.Name as Filename from pages as p
                    left join pages_content as pc on pc.PageId=p.id
                    where p.id=@id", SqlConn);

            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetTerefdaslarById  "), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetTerefdaslarByCat(int categoryId, int pageNum, int rowCount)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select p.*,pc.Name as Filename from pages as p
                    left join pages_content as pc on pc.PageId=p.id
                    where page_id=@page_id
                    order by id desc 
                    limit @pageNum,@rowCount ", SqlConn);

            da.SelectCommand.Parameters.AddWithValue("page_id", categoryId);
            da.SelectCommand.Parameters.AddWithValue("pageNum", pageNum * rowCount);
            da.SelectCommand.Parameters.AddWithValue("rowCount", rowCount);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPage  "), ex.Message, "", true);
            return null;
        }
    }
    public int GetTerefdaslarByCatCount(int CategoryID)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select count(*) as count from pages as p
                    where page_id=@pageID", SqlConn);

            da.SelectCommand.Parameters.AddWithValue("pageID", CategoryID);
            da.Fill(dt);
            return dt.Rows[0][0].ToParseInt();

        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPage  "), ex.Message, "", true);
            return 0;
        }
    }

    public DataTable GetTerefdaslarAll(Utils.PageType type_id, int pageNum, int rowCount)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select p.*,pc.Name as Filename from pages as p
                    left join pages_content as pc on pc.PageId=p.id
                    where page_id in (select id from pages where type_id=@type_id)
                    order by id desc 
                    limit @pageNum,@rowCount ", SqlConn);

            da.SelectCommand.Parameters.AddWithValue("type_id", (int)type_id);
            da.SelectCommand.Parameters.AddWithValue("pageNum", pageNum * rowCount);
            da.SelectCommand.Parameters.AddWithValue("rowCount", rowCount);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPage  "), ex.Message, "", true);
            return null;
        }
    }
    public int GetTerefdaslarAllCount(Utils.PageType type_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select count(*) as count from pages as p
                    where page_id in (select id from pages where type_id=@type_id)", SqlConn);

            da.SelectCommand.Parameters.AddWithValue("type_id", (int)type_id);
            da.Fill(dt);
            return dt.Rows[0][0].ToParseInt();

        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPage  "), ex.Message, "", true);
            return 0;
        }
    }

    public DataTable GetPage(int id, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  title_az,title_en,content_az,content_en,type_id,Id,title_" + lang + " as title,content_" + lang + " as content,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt,video_url FROM pages as m  where id=@id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPage  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPage(Utils.PageType type, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  title_az,title_en,content_az,content_en,type_id,Id,title_" + lang + " as title,content_" + lang + " as content,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt,video_url FROM pages as m  where type_id=@type_id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("type_id", (int)type);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPage  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPage1(int id)
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * FROM pages as m  where id=@id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPage  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPagesTerefdaslarCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and page_id=0 order by orderBy   ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)Utils.PageType.TerefdaslarCategory);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPages  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPages(Utils.PageType PageType)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and page_id=0 order by page_dt desc  ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPages  "), ex.Message, "", true);
            return null;
        }
    }


    public DataTable GetPagesOrderBy(Utils.PageType PageType)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and page_id=0 order by orderby  ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPages  "), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetPagesOther(Utils.PageType PageType, int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and page_id=0   and id<>@id order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPagesOther  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType PageUpdate(int id, int typeid, string title_az, string slug_az, string title_en, string slug_en,
        string content_az, string content_en, DateTime page_dt, string videoURl, int goalId, int parentId, string more_url, int orderBy)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE pages SET goal_id=@goal_id,orderBy=@orderBy,video_url=@videoURL,
type_id=@typeid,title_az=@title_az,slug_az=@slug_az,title_en=@title_en,slug_en=@slug_en,content_az=@content_az,
content_en=@content_en,page_dt=@page_dt,page_id=@page_id,more_url=@more_url WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@videoURL", videoURl);
        cmd.Parameters.AddWithValue("@page_id", parentId);
        cmd.Parameters.AddWithValue("@more_url", more_url);
        cmd.Parameters.AddWithValue("@typeid", typeid);
        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@slug_az", slug_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@slug_en", slug_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);
        cmd.Parameters.AddWithValue("@orderBy", orderBy);
        cmd.Parameters.AddWithValue("@page_dt", page_dt);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);

        cmd.Parameters.AddWithValue("@goal_id", goalId);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PageUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType PageUpdate(int id, int typeid, string title_az, string title_en, string more_url, int orderBy)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE pages SET orderBy=@orderBy,type_id=@typeid,title_az=@title_az,title_en=@title_en,more_url=@more_url WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@more_url", more_url);
        cmd.Parameters.AddWithValue("@typeid", typeid);
        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@orderBy", orderBy);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PageUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType PageDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"delete from pages where id=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("PageDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    //   
    public Utils.MethodType PageInsert(int typeid, string title_az, string slug_az, string title_en, string slug_en,
        string content_az, string content_en, DateTime page_dt, string videoURl, int goalId, int parentId, string more_url,
        int orderBy)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into pages (orderBy,video_url,type_id,title_az,slug_az,title_en,slug_en,content_az,content_en,page_dt,add_manager_id,add_dt,add_ip,page_id,more_url,goal_id) 
                            values(@orderBy,@videoURL,@typeid,@title_az,@slug_az,@title_en,@slug_en,@content_az,@content_en,@page_dt,@add_manager_id,@add_dt,@add_ip,@page_id,@more_url,@goal_id) ", SqlConn);
        cmd.Parameters.AddWithValue("@page_id", parentId);
        cmd.Parameters.AddWithValue("@goal_id", goalId);
        cmd.Parameters.AddWithValue("@more_url", more_url);
        cmd.Parameters.AddWithValue("@orderBy", orderBy);
        cmd.Parameters.AddWithValue("@videoURL", videoURl);
        cmd.Parameters.AddWithValue("@typeid", typeid);
        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@slug_az", slug_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@slug_en", slug_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);

        cmd.Parameters.AddWithValue("@page_dt", page_dt);

        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@add_manager_id", ManagerInfo.ID);

        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PageInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType PageInsert1(int typeid, string title_az, string title_en, string more_url = "")
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into pages (type_id,title_az,title_en,add_manager_id,add_dt,add_ip,more_url) 
                            values (@type_id,@title_az,@title_en,@add_manager_id,@add_dt,@add_ip,@more_url)  ", SqlConn);
        cmd.Parameters.AddWithValue("@more_url", more_url);

        cmd.Parameters.AddWithValue("@typeid", typeid);
        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);


        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@add_manager_id", ManagerInfo.ID);

        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PageInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType PageContentInsert(int pageId, string name)
    {
        MySqlCommand cmd = new MySqlCommand(@" insert into pages_content (PageId,name) values(@PageId,@name) ", SqlConn);

        cmd.Parameters.AddWithValue("@PageId", pageId);
        cmd.Parameters.AddWithValue("@name", name);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PageContentInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType PageContentDelete(int pageId)
    {
        MySqlCommand cmd = new MySqlCommand(@" delete from pages_content where pageid=@pageId ", SqlConn);

        cmd.Parameters.AddWithValue("@pageId", pageId);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PageContentDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetPages(Utils.PageType PageType, int topCount)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and page_id=0  order by page_dt desc limit @topCount ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);
            da.SelectCommand.Parameters.AddWithValue("topCount", (int)topCount);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPages  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPagesByID(int PageId)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where  id=@PageId order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("PageId", PageId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPagesByPageID  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPagesByPageID(Utils.PageType PageType, int PageId)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and page_id=@PageId order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);
            da.SelectCommand.Parameters.AddWithValue("PageId", PageId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPagesByPageID  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPagesByGoalId(Utils.PageType PageType, int GoalId)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and goal_id=@GoalId order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);
            da.SelectCommand.Parameters.AddWithValue("GoalId", GoalId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPagesByGoalId  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPagesByGoalIdOrderby(Utils.PageType PageType, int GoalId)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(page_dt, \"%d.%m.%Y\") as pageDt FROM pages where type_id=@typeId and goal_id=@GoalId order by OrderBy  ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("typeId", (int)PageType);
            da.SelectCommand.Parameters.AddWithValue("GoalId", GoalId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPagesByGoalId  "), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetPagesContent(int parentId)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * FROM pages_content where pageid=@page_id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("page_id", parentId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.select, String.Format("GetPagesContent  "), ex.Message, "", true);
            return null;
        }
    }

    #endregion

    #region Goals

    public DataTable GetGoals()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,concat('Məqsəd ',priority,' . ',name_short_az) as name  from goals order by priority ", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetGoals()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetGoalById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from goals where id=@id order by priority ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetGoals()"), ex.Message, "", true);
            return null;
        }
    }


    public Utils.MethodType GoalUpdate(int id, string name_az, string name_en, string name_short_az, string name_short_en,
        int priority, string facts_en, string desc_en, string facts_az, string desc_az,
        string priority_desc_en, string priority_desc_az
        )
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE goals SET name_az=@name_az,
                                                        name_en=@name_en,
                                                        name_short_az=@name_short_az,
                                                        name_short_en=@name_short_en,
                                                        priority=@priority,
                                                        facts_az=@facts_az,
                                                        description_az=@desc_az,
                                                        facts_en=@facts_en,
                                                        description_en=@desc_en,
                                                        priority_desc_az=@priority_desc_az,
                                                        priority_desc_en=@priority_desc_en 
                                                        WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_short_az", name_short_az);
        cmd.Parameters.AddWithValue("@name_short_en", name_short_en);
        cmd.Parameters.AddWithValue("@priority", priority);
        cmd.Parameters.AddWithValue("@facts_az", facts_az);
        cmd.Parameters.AddWithValue("@desc_az", desc_az);
        cmd.Parameters.AddWithValue("@facts_en", facts_en);
        cmd.Parameters.AddWithValue("@desc_en", desc_en);

        cmd.Parameters.AddWithValue("@priority_desc_az", priority_desc_az);
        cmd.Parameters.AddWithValue("@priority_desc_en", priority_desc_en);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("GoalUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType GoalInsert(string name_az, string name_en, string name_short_az, string name_short_en, int priority,
        string facts_en, string desc_en, string facts_az, string desc_az)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into goals (name_az,name_en,name_short_az,name_short_en,priority,facts_az,description_az,facts_en,description_en) 
                            values (@name_az,@name_en,@name_short_az,@name_short_en,@priority,@facts_az,@desc_az,@facts_en,@desc_en)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_short_az", name_short_az);
        cmd.Parameters.AddWithValue("@name_short_en", name_short_en);
        cmd.Parameters.AddWithValue("@priority", priority);
        cmd.Parameters.AddWithValue("@facts_az", facts_az);
        cmd.Parameters.AddWithValue("@desc_az", desc_az);
        cmd.Parameters.AddWithValue("@facts_en", facts_en);
        cmd.Parameters.AddWithValue("@desc_en", desc_en);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("GoalInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Slider
    public string getSliderId
    {
        get
        {

            MySqlCommand cmd = new MySqlCommand(string.Format("SELECT id FROM slider order by id desc limit 1", Config.getLangSession), SqlConn);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                (new DALC()).LogInsert(Utils.Tables.pages, Utils.LogType.select, "getSliderId () ", ex.Message, "", true);
                return "";
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

    }
    public DataTable GetSlidersGoalList(int goal_id, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select g.priority,g.name_short_" + lang + @" as GoalName,g.id as GoalId,
(select s.image_url_" + lang + @" from slider as sg where sg.goal_id=g.id and sg.show_home_slider=1 limit 1) as image_url from slider s
inner join goals g on g.id=s.goal_id
where g.id<>@goal_id
group by GoalName , GoalId order by g.priority", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSlider()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetSlidersByGoal(int goal_id, Page p)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select g.priority, g.name_" + Config.getLang(p) +
                @" as GoalName, s.* from slider as s
                    inner join goals g on g.id=s.goal_id 
                    where  g.id=@id order by indicatorCode(title_az)", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", goal_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSlider()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetSliders(int show_home_slider, Page p)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select g.priority,g.name_" + Config.getLang(p) + @" as GoalName,g.color, s.* from slider as s
                    inner join goals g on g.id=s.goal_id 
                    where s.show_home_slider=@show_home_slider
                    order by g.priority", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("show_home_slider", show_home_slider);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSlider()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetSlider()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * from slider order by goal_id,indicatorCode(title_az) ", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSlider()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetSlider(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * from slider where id=@id order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSlider()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType SliderUpdate(int id, int goal_id, string title_az, string title_en, string content_az, string content_en, string image_url_az, string image_url_en, bool show_home_slider)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE slider SET goal_id=@goal_id,title_az=@title_az,title_en=@title_en,content_az=@content_az,content_en=@content_en,image_url_az=@image_url_az,image_url_en=@image_url_en,show_home_slider=@show_home_slider,update_dt=@dt,update_ip=@ip WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);

        cmd.Parameters.AddWithValue("@image_url_az", image_url_az);
        cmd.Parameters.AddWithValue("@image_url_en", image_url_en);
        cmd.Parameters.AddWithValue("@show_home_slider", show_home_slider);

        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PageUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType SliderDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("delete from slider where id=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.delete, String.Format("SliderDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType SliderInsert(int goal_id, string title_az, string title_en, string content_az, string content_en, string image_url_az, string image_url_en, bool show_home_slider)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into slider (goal_id,title_az,title_en,content_az,content_en,image_url_az,image_url_en,show_home_slider,add_dt,add_ip) 
                            values (@goal_id,@title_az,@title_en,@content_az,@content_en,@image_url_az,@image_url_en,@show_home_slider,@add_dt,@add_ip) ", SqlConn);

        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);

        cmd.Parameters.AddWithValue("@image_url_az", image_url_az);
        cmd.Parameters.AddWithValue("@image_url_en", image_url_en);
        cmd.Parameters.AddWithValue("@show_home_slider", show_home_slider);


        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);

        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("SliderInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    #endregion

    #region Indicators
    public DataTable GetIndicatorRepeatedById_2(int Id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select tekrarlanan_gosderici_kod from indicators where id=@Id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("Id", Id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorRepeatedById_2()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorByCodes(string codes)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from indicators where code in ("+codes+")", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorByCodes()"+codes), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorRepeatedById(int Id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from indicators where code in (select tekrarlanan_gosderici_kod from indicators where id=@Id)", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("Id", Id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorRepeatedById()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsByOrg_Year(int orgId, int year)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  iss.name_az as SizeName,
 case (select count(*) from hesabat where indicator_id=i.id and year=@year and value<>0)
when 0 then 0
else 1 end
 as FillCount
,i.*  from indicators as i
inner join indicator_size as iss on iss.id=i.size_id
where i.qurum_id=@orgId and i.isActive=1 ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("orgId", orgId);
            da.SelectCommand.Parameters.AddWithValue("year", year);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsByQurum()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsByQurum(int orgId)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from indicators where qurum_id=@orgId and isActive=1 ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("orgId", orgId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsByQurum()"), ex.Message, "", true);
            return null;
        }
    }
    public int indiqatoryoxla(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            int a = 0;
            MySqlDataAdapter da = new MySqlDataAdapter(@"
select * from (select *,concat(SUBSTRING(i.code, 1, 9),SUBSTRING(i.code, 11, 8)) kodu from indicators i WHERE 
i.id=@id and i.isActive=1 and i.parent_id=0 ) as indi 
inner join 
(
   SELECT count(*) say,kod from (
     SELECT concat(SUBSTRING(i.code, 1, 9),SUBSTRING(i.code, 11, 8)) kod,id
     from indicators as i
     where  i.isActive=1 and i.parent_id=0 
   ) c group by kod HAVING count(*)>1
) iki
on indi.kodu=iki.kod where indi.isActive=1", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);


            DataTable dt1 = new DataTable();
            MySqlDataAdapter da1 = new MySqlDataAdapter(@"SELECT * FROM `hesabat` WHERE indicator_id=@id 
and is_active=1 and (`value` is not null or footnote_id is not null)", SqlConn);
            da1.SelectCommand.Parameters.AddWithValue("id", id);
            da1.Fill(dt1);


            if (dt.Rows.Count > 0 && dt1.Rows.Count == 0)
            {
                DataTable dt2 = new DataTable();
                MySqlDataAdapter da2 = new MySqlDataAdapter(@"select *  from indicators i where 
 i.isActive=1 and i.parent_id=0 and type_id=2 and concat(SUBSTRING(i.code, 1, 9),SUBSTRING(i.code, 11, 8))=@kodu" , SqlConn);
                da2.SelectCommand.Parameters.AddWithValue("kodu", dt.Rows[0]["kodu"].ToParseStr());
                da2.Fill(dt2);
                if (dt2.Rows.Count!=0)
                {
                    a = dt2.Rows[0]["id"].ToParseInt();
                }
                else
                    a = id;
            }
            else
                a = id;
            return a;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("indiqatoryoxla()"), ex.Message, "", true);
            return 0;
        }
    }



    public DataTable GetIndicatorById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select  i.*,it.code as type_code,
i_s.code as size_code,i_s.name_az as size_name_az,i_s.name_en as size_name_en  from indicators as i
left join indicators_type as it on it.id=i.type_id
left join indicator_size as i_s on i_s.id=i.size_id
 where i.id=@id and i.isActive=1 ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorById()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicators_withDesc(int goal_id, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  (select mg.name_" + lang + @" from metadata as mg
inner join metadata_list as mgl on mg.list_id=mgl.id
where mg.indicator_id=i.id and mgl.code='DATA_DESCR'
) as descr,i_s.code as size_code,i.*  from indicators as i 
inner join hesabat as h on h.indicator_id=i.id 
left join indicator_size as i_s on i_s.id=i.size_id 
where i.goal_id=@goal_id and i.isActive=1 and h.is_active=1 and length(h.value)>0 group by i.code order by i.code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicators_withDesc2(int goal_id, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  (select mg.name_" + lang + @" from metadata as mg
inner join metadata_list as mgl on mg.list_id=mgl.id
where mg.indicator_id=i.id and mgl.code='DATA_DESCR'
) as descr,i_s.code as size_code,i.*  from indicators as i 
inner join hesabat as h on h.indicator_id=i.id 
left join indicator_size as i_s on i_s.id=i.size_id 
where i.goal_id=@goal_id and i.isActive=1 and h.is_active=1 and length(h.value)>0 and i_s.code !=35 group by i.code order by i.code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicators(int goal_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select  *  from indicators where goal_id=@goal_id and 
isActive=1 order by indicatorCode(code) ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsByOrgId(int org_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from indicators where  qurum_id=@qurum_id and parent_id=0 and isActive=1 order by code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("qurum_id", org_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicators(int goal_id, int parentId)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from indicators where goal_id=@goal_id and isActive=1 and parent_id=@parent_id order by code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            da.SelectCommand.Parameters.AddWithValue("parent_id", parentId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicators_1(int goal_id, int indicatorId, string code)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  * from indicators 
where code like @code and goal_id=@goal_id and id<>@indicatorId and isActive=1 order by orderBy", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            da.SelectCommand.Parameters.AddWithValue("code", "%" + code + "%");
            da.SelectCommand.Parameters.AddWithValue("indicatorId", indicatorId);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }


    public DataTable GetIndicatorsReportingStatus()
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select i1.name_az movcudname_az,i2.name_az planname_az,i3.name_az arasdirilirname_az,
i1.name_en movcudname_en,i2.name_en planname_en,i3.name_en arasdirilirname_en,i.*,i1.movcuddur,i2.plan,i3.arasdirilir,
cast(i1.movcuddur*100/i.cemisay as decimal(6,1)) faizmovcud,cast(i2.plan*100/i.cemisay as decimal(6,1)) faizplan,
cast(i3.arasdirilir*100/i.cemisay as decimal(6,1))  faizarasdirilir

from (SELECT i.goal_id,g.name_short_az,g.name_short_en,count(*) cemisay FROM `indicators` i 
inner join goals g on i.goal_id=g.id 
where i.type_id=1 and i.parent_id=0 and i.isActive=1 
group by i.goal_id,g.name_short_az,g.name_short_en) as i 

inner join (SELECT i.goal_id,s.name_az,s.name_en,count(*) movcuddur FROM `indicators` as i 
inner join indicators_status as s on i.status_id=s.id
where i.type_id=1 and i.parent_id=0 and i.isActive=1 and 
i.status_id=3 group by i.goal_id,s.name_az,s.name_en) as i1 
on i.goal_id=i1.goal_id

inner join (SELECT i.goal_id,s.name_az,s.name_en,count(*) plan FROM `indicators` as i 
inner join indicators_status as s on i.status_id=s.id
where i.type_id=1 and i.parent_id=0 and i.isActive=1 and 
i.status_id=2 group by i.goal_id,s.name_az,s.name_en) as i2 
on i.goal_id=i2.goal_id

inner join (SELECT i.goal_id,s.name_az,s.name_en,count(*) arasdirilir FROM `indicators` as i 
inner join indicators_status as s on i.status_id=s.id
where i.type_id=1 and i.parent_id=0 and i.isActive=1 and 
i.status_id=1 group by i.goal_id,s.name_az,s.name_en) as i3 
on i.goal_id=i3.goal_id", SqlConn);
            //da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            //da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);
            //da.SelectCommand.Parameters.AddWithValue("typeid", typeid);



            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsReportingStatus()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsReportingStatusSum()
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT count(*) say FROM `indicators` where type_id=1 and 
parent_id=0 and isActive=1", SqlConn);
            //da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            //da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);
            //da.SelectCommand.Parameters.AddWithValue("typeid", typeid);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsReportingStatusSum()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsReportingStatusSumMovcud()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT count(*) say,s.name_az,s.name_en,
cast(count(*)*100/(SELECT count(*) say FROM `indicators` where type_id=1 and 
parent_id=0 and isActive=1) as decimal(6,1)) faiz


FROM `indicators` i
inner join indicators_status as s on i.status_id=s.id
where i.type_id=1 and i.parent_id=0 and i.isActive=1 and i.status_id=3 group by s.name_az,s.name_en", SqlConn);
            //da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            //da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);
            //da.SelectCommand.Parameters.AddWithValue("typeid", typeid);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsReportingStatusSumMovcud()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsReportingStatusSumPlan()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT count(*) say,s.name_az,s.name_en,
cast(count(*)*100/(SELECT count(*) say FROM `indicators` where type_id=1 and 
parent_id=0 and isActive=1) as decimal(6,1)) faiz

FROM `indicators` i
inner join indicators_status as s on i.status_id=s.id
where i.type_id=1 and i.parent_id=0 and i.isActive=1 and i.status_id=2 group by s.name_az,s.name_en", SqlConn);
            //da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            //da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);
            //da.SelectCommand.Parameters.AddWithValue("typeid", typeid);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsReportingStatusSumPlan()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsReportingStatusSumArasdirilir()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT count(*) say,s.name_az,s.name_en,
cast(count(*)*100/(SELECT count(*) say FROM `indicators` where type_id=1 and 
parent_id=0 and isActive=1) as decimal(6,1)) faiz

FROM `indicators` i
inner join indicators_status as s on i.status_id=s.id
where i.type_id=1 and i.parent_id=0 and i.isActive=1 and i.status_id=1 group by s.name_az,s.name_en", SqlConn);
            //da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            //da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);
            //da.SelectCommand.Parameters.AddWithValue("typeid", typeid);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsReportingStatusSumArasdirilir()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetIndicatorsByParentId(int goal_id, int parent_id, int typeid = 0)
    {
        try
        {
            string where = "";
            if (typeid != 0)
            {
                where += " and i.type_id=@typeid ";
            }
            //if (milli_priotet != -1)
            //{
            //    where += " and i.milli_priotet=@milli_priotet";
            //}
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  i.*,s.name_az as status_name_az,s.name_en as status_name_en,s.`key`  from indicators as i
left join indicators_status as s on i.status_id=s.id where i.goal_id=@goal_id and i.parent_id=@parent_id   " + where + " and i.isActive=1 order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);
            da.SelectCommand.Parameters.AddWithValue("typeid", typeid);



            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsByParentId_2(int parent_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"
SELECT  i.*,s.name_az as status_name_az,s.name_en as status_name_en,s.`key`  from indicators as i
left join indicators_status as s on i.status_id=s.id 
inner join hesabat as h on h.indicator_id=i.id
where (i.parent_id=@parent_id or i.id=@parent_id) and i.isActive=1 and length(h.value)>0 and h.is_active=1
group by i.id
order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsByParentId_3(int parent_id)
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"
SELECT  i.*,s.name_az as status_name_az,s.name_en as status_name_en,s.`key`  from indicators as i
left join indicators_status as s on i.status_id=s.id 
inner join hesabat as h on h.indicator_id=i.id
where i.parent_id=@parent_id  and i.isActive=1 and length(h.value)>0 and h.is_active=1
group by i.id
order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("parent_id", parent_id);



            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsByParentId_3()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetIndicators_1(int goal_id, int codeLength, int milli_priotet = -1, int typeid = 0)
    {
        try
        {
            string where = "";
            if (typeid != 0)
            {
                where += " and i.type_id=@typeid ";
            }
            //if (milli_priotet != -1)
            //{
            //    where += " and i.milli_priotet=@milli_priotet";
            //}
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  i.*,s.name_az as status_name_az,s.name_en as status_name_en,s.`key`  from indicators as i
left join indicators_status as s on i.status_id=s.id where i.goal_id=@goal_id  and length(trim(i.code))=@codeLength " + where + " and i.isActive=1 order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            //da.SelectCommand.Parameters.AddWithValue("milli_priotet", milli_priotet);
            da.SelectCommand.Parameters.AddWithValue("codeLength", codeLength);
            da.SelectCommand.Parameters.AddWithValue("typeid", typeid);



            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetIndicatorsForGoalInfo(int goal_id, int codeLength, int typeId)
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from indicators where goal_id=@goal_id  and length(trim(code))=@codeLength and type_id=@typeId and isActive=1 order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);
            da.SelectCommand.Parameters.AddWithValue("codeLength", codeLength);
            da.SelectCommand.Parameters.AddWithValue("typeId", typeId);


            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsForNationaPriority(int goal_id)
    {
        try
        {

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT  *  from indicators where goal_id=@goal_id  
            and parent_id=0 and uygunluq_id=1  and isActive=1 order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);




            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicators()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorsType()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from indicators_type order by priority ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorsType()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType IndicatorsUpdate(int id, int qurum_id, int size_id, int goal_id, int type_id, int uygunluq_id, string code,
                                            string tekrarlanan_gosderici_kod, string name_az, string name_en, string info_az, string info_en, int seviye, int teqdim_olunma_bas_gun, int teqdim_olunma_bas_ay,
                                            int teqdim_olunma_son_gun, int teqdim_olunma_son_ay,
        object status_id, string note_az, string note_en, string source_az, string source_en, int elave_gun, int parent_id)
    {




        MySqlCommand cmd = new MySqlCommand(@"UPDATE indicators SET 
                                       qurum_id=@qurum_id,
                                       size_id=@size_id,
                                       status_id=@status_id,
                                       goal_id=@goal_id,
                                       type_id=@type_id,
                                       uygunluq_id=@uygunluq_id,
                                       code=@code,
                                       tekrarlanan_gosderici_kod=@tekrarlanan_gosderici_kod,
                                       name_az=@name_az,
                                       name_en=@name_en,
                                       info_az=@info_az,
                                       info_en=@info_en,
                                       seviye=@seviye,
                                       teqdim_olunma_bas_gun=@teqdim_olunma_bas_gun,
                                       teqdim_olunma_bas_ay=@teqdim_olunma_bas_ay,
                                       teqdim_olunma_son_gun=@teqdim_olunma_son_gun,
                                       teqdim_olunma_son_ay=@teqdim_olunma_son_ay,
                                       parent_id=@parent_id,
                                       elave_gun=@elave_gun,
                                       note_az=@note_az,
                                       source_az=@source_az,
                                       note_en=@note_en,
                                       source_en=@source_en
                                WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@qurum_id", qurum_id);
        cmd.Parameters.AddWithValue("@size_id", size_id);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@status_id", status_id.ToParseInt() == 0 ? DBNull.Value : status_id);
        cmd.Parameters.AddWithValue("@type_id", type_id);
        cmd.Parameters.AddWithValue("@uygunluq_id", uygunluq_id);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@tekrarlanan_gosderici_kod", tekrarlanan_gosderici_kod);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@info_az", info_az);
        cmd.Parameters.AddWithValue("@info_en", info_en);
        cmd.Parameters.AddWithValue("@seviye", seviye);
        cmd.Parameters.AddWithValue("@teqdim_olunma_bas_gun", teqdim_olunma_bas_gun);
        cmd.Parameters.AddWithValue("@teqdim_olunma_bas_ay", teqdim_olunma_bas_ay);
        cmd.Parameters.AddWithValue("@teqdim_olunma_son_gun", teqdim_olunma_son_gun);
        cmd.Parameters.AddWithValue("@teqdim_olunma_son_ay", teqdim_olunma_son_ay);
        cmd.Parameters.AddWithValue("@note_az", note_az);
        cmd.Parameters.AddWithValue("@source_az", source_az);
        cmd.Parameters.AddWithValue("@note_en", note_en);
        cmd.Parameters.AddWithValue("@source_en", source_en);
        cmd.Parameters.AddWithValue("@elave_gun", elave_gun);
        cmd.Parameters.AddWithValue("@parent_id", parent_id);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("IndicatorsUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType IndicatorsDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE indicators SET isActive=0 WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorsDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType IndicatorInsert(int qurum_id, int size_id, int goal_id, int type_id, int uygunluq_id, string code,
                                            string tekrarlanan_gosderici_kod, string name_az, string name_en, string info_az, string info_en, int seviye, int teqdim_olunma_bas_gun, int teqdim_olunma_bas_ay,
                                            int teqdim_olunma_son_gun, int teqdim_olunma_son_ay,
        int status_id, string note_az, string note_en, string source_az, string source_en, int elave_gun, int parent_id)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into indicators (qurum_id,size_id,goal_id,type_id,uygunluq_id,code,
                                    tekrarlanan_gosderici_kod,name_az,name_en,info_az,info_en,seviye,teqdim_olunma_bas_gun,teqdim_olunma_bas_ay,
                                    teqdim_olunma_son_gun,teqdim_olunma_son_ay,isActive,status_id,note_az,source_az,note_en,source_en,elave_gun,parent_id) 
                                values (@qurum_id,
                                        @size_id,
                                        @goal_id,
                                        @type_id,
                                        @uygunluq_id,
                                        @code,
                                        @tekrarlanan_gosderici_kod,
                                        @name_az,@name_en,
                                        @info_az,@info_en,
                                        @seviye,
                                        @teqdim_olunma_bas_gun,
                                        @teqdim_olunma_bas_ay,
                                        @teqdim_olunma_son_gun,
                                        @teqdim_olunma_son_ay,1,@status_id,@note_az,@source_az,@note_en,@source_en,@elave_gun,@parent_id)  ", SqlConn);

        cmd.Parameters.AddWithValue("@qurum_id", qurum_id);
        cmd.Parameters.AddWithValue("@size_id", size_id);
        cmd.Parameters.AddWithValue("@type_id", type_id);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@uygunluq_id", uygunluq_id);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@tekrarlanan_gosderici_kod", tekrarlanan_gosderici_kod);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@info_az", info_az);
        cmd.Parameters.AddWithValue("@info_en", info_en);
        cmd.Parameters.AddWithValue("@seviye", seviye);
        cmd.Parameters.AddWithValue("@teqdim_olunma_bas_gun", teqdim_olunma_bas_gun);
        cmd.Parameters.AddWithValue("@teqdim_olunma_bas_ay", teqdim_olunma_bas_ay);
        cmd.Parameters.AddWithValue("@teqdim_olunma_son_gun", teqdim_olunma_son_gun);
        cmd.Parameters.AddWithValue("@teqdim_olunma_son_ay", teqdim_olunma_son_ay);
        cmd.Parameters.AddWithValue("@status_id", status_id);
        cmd.Parameters.AddWithValue("@note_az", note_az);
        cmd.Parameters.AddWithValue("@source_az", source_az);
        cmd.Parameters.AddWithValue("@note_en", note_en);
        cmd.Parameters.AddWithValue("@source_en", source_en);
        cmd.Parameters.AddWithValue("@elave_gun", elave_gun);

        cmd.Parameters.AddWithValue("@parent_id", parent_id);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("IndicatorInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Targets
    public DataTable GetTargetsByGoalId(int goal_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from targets where goal_id=@goalId and is_active=1  order by code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goalId", goal_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetTargets()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetTargets(int goal_id, bool milliPriotet = false)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from targets where goal_id=@goalId and is_active=1 and milli_Priotet=@milliPriotet order by code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goalId", goal_id);
            da.SelectCommand.Parameters.AddWithValue("milliPriotet", milliPriotet);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetTargets()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetTargets_1(int goal_id, bool milliPriotet = false)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from targetsnational where goal_id=@goalId and is_active=1 order by cast(code as unsigned) ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goalId", goal_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetTargets()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetTargetsFull(int goal_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from targets where goal_id=@goalId and is_active=1 order by code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goalId", goal_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetTargets()"), ex.Message, "", true);
            return null;
        }
    }









    public DataTable GetTargetsFull1(int goal_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from targetsnational where goal_id=@goalId and is_active=1 order by code ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("goalId", goal_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetTargets1()"), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType TargetsUpdate1(int id, string name_az, string name_en, string code, int goal_id, object milli_priotet)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE targetsnational SET name_az=@name_az,name_en=@name_en,code=@code,goal_id=@goal_id WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@id", id);
  

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("IndicatorSizeUpdate1 () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType TargetsDelete1(int id)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE targetsnational SET is_Active=0 WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("TargetsDelete1 () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType TargetsInsert1(string name_az, string name_en, string code, int goal_id, object milli_priotet)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into targetsnational (name_az,name_en,code,goal_id) 
                            values (@name_az,@name_en,@code,@goal_id)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("IndicatorSizeInsert1 () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }










    public Utils.MethodType TargetsUpdate(int id, string name_az, string name_en, string code, int goal_id, object milli_priotet)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE targets SET name_az=@name_az,name_en=@name_en,code=@code,goal_id=@goal_id,milli_priotet=@milli_priotet WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@milli_priotet", milli_priotet);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("IndicatorSizeUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType TargetsDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE targets SET is_Active=0 WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("TargetsDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType TargetsInsert(string name_az, string name_en, string code, int goal_id, object milli_priotet)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into targets (name_az,name_en,code,goal_id,milli_priotet) 
                            values (@name_az,@name_en,@code,@goal_id,@milli_priotet)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@milli_priotet", milli_priotet);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("IndicatorSizeInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region IndicatorSize
    public DataTable GetIndicatorSizeById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from indicator_size where id=@id order by priority ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorSize()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetIndicatorSize()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from indicator_size where is_active=1 order by code ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetIndicatorSize()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType IndicatorSizeUpdate(int id, string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE indicator_size SET name_az=@name_az,name_en=@name_en,code=@code WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);

        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("IndicatorSizeUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType IndicatorSizeDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE indicator_size SET is_Active=0 WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("IndicatorSizeDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType IndicatorSizeInsert(string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into indicator_size (name_az,name_en,code) 
                            values (@name_az,@name_en,@code)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("IndicatorSizeInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Qurumlar

    public DataTable GetQurum()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from qurumlar where is_active=1 order by priority ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetQurum()"), ex.Message, "", true);
            return null;
        }
    }
    public string GetQurumByCode(string code)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select q.id from qurumlar as q  where q.code=@code and q.is_active=1", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("code", code);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToParseStr();
            }

        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetQurumByCode()"), ex.Message, "", true);
            return "-1";
        }
    }

    public Utils.MethodType QurumUpdate(int id, string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE qurumlar SET name_az=@name_az,name_en=@name_en,code=@code WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);

        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("QurumUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType QurumDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE qurumlar SET is_Active=0 WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("QurumDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType QurumInsert(string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into qurumlar (name_az,name_en,code) 
                            values (@name_az,@name_en,@code)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("QurumInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Regionlar

    public DataTable GetRegion()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from regions where is_active=1 order by priority ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetRegion()"), ex.Message, "", true);
            return null;
        }
    }
    public string GetRegionByCode(string code)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select count(q.id) from regions as q  where q.code=@code and q.is_active=1", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("code", code);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToParseStr();
            }

        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetRegionByCode()"), ex.Message, "", true);
            return "-1";
        }
    }

    public Utils.MethodType RegionUpdate(int id, int sub_id, string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE regions SET sub_id=@sub_id,name_az=@name_az,name_en=@name_en,code=@code WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@sub_id", sub_id);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("RegionUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType RegionDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE regions SET is_Active=0 WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("RegionDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType RegionInsert(int sub_id, string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into regions (sub_id,name_az,name_en,code) 
                            values (@sub_id,@name_az,@name_en,@code)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@sub_id", sub_id);



        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("RegionInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Files
    public DataTable GetFile(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from Files where id=@id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetFiles()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetFiles()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from Files order by id desc ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetFiles()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType FileUpdate(int id, string name, string filename)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE files SET name=@name,filename=@filename WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@filename", filename);

        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("FileUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType FileInsert(string name, string filename)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into files (name,filename) 
                            values (@name,@filename)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@filename", filename);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("FileInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region NationalReports
    public DataTable GetNationalReportsAll(int pageNum, int rowCount)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from national_reports  order by id desc limit @pageNum,@rowCount", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("pageNum", pageNum * rowCount);
            da.SelectCommand.Parameters.AddWithValue("rowCount", rowCount);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.select, String.Format("GetNationalReportsAll()"), ex.Message, "", true);
            return null;
        }
    }
    public int GetNationalReportsAllCount()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  count(*)  from national_reports  ", SqlConn);

            da.Fill(dt);
            return dt.Rows[0][0].ToParseInt();
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.select, String.Format("GetNationalReportsAllCount()"), ex.Message, "", true);
            return 0;
        }
    }
    public DataTable GetNationalReports()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from national_reports  order by id desc", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.select, String.Format("GetNationalReports()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetNationalReportsById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from national_reports where id=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.select, String.Format("GetNationalReportsById()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType NationalReportsUpdate(int id, string title_az, string title_en, string content_az, string content_en,
        DateTime date, string full_material, string short_material, string image_filename_az
        , string image_filename_en
        , string content_short_az
        , string content_short_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE national_reports SET 
                                                title_az=@title_az,
                                                title_en=@title_en,
                                                content_az=@content_az,
                                                content_en=@content_en,
                                                content_short_az=@content_short_az,
                                                content_short_en=@content_short_en,
                                                date=@date,
                                                image_filename_az=@image_filename_az,
                                                image_filename_en=@image_filename_en,   
                                                full_material=@full_material,
                                                short_material=@short_material
                                              WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);

        cmd.Parameters.AddWithValue("@content_short_az", content_short_az);
        cmd.Parameters.AddWithValue("@content_short_en", content_short_en);

        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@full_material", full_material);
        cmd.Parameters.AddWithValue("@image_filename_az", image_filename_az);
        cmd.Parameters.AddWithValue("@image_filename_en", image_filename_en);
        cmd.Parameters.AddWithValue("@short_material", short_material);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.update, String.Format("NationalReportsUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType NationalReportDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("delete from national_reports WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.delete, String.Format("ReportDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType NationalReportInsert(string title_az, string title_en, string content_az, string content_en,
        DateTime date, string full_material, string short_material, string image_filename_az
        , string image_filename_en
        , string content_short_az
        , string content_short_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into national_reports (content_short_az,content_short_en,image_filename_az,image_filename_en,title_az,title_en,content_az,content_en,date,full_material,short_material,add_dt) 
                            values (@content_short_az,@content_short_en,@image_filename_az,@image_filename_en,@title_az,@title_en,@content_az,@content_en,@date,@full_material,@short_material,@add_dt)   ", SqlConn);

        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);
        cmd.Parameters.AddWithValue("@content_short_az", content_short_az);
        cmd.Parameters.AddWithValue("@content_short_en", content_short_en);
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@full_material", full_material);
        cmd.Parameters.AddWithValue("@short_material", short_material);
        cmd.Parameters.AddWithValue("@image_filename_az", image_filename_az);
        cmd.Parameters.AddWithValue("@image_filename_en", image_filename_en);

        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.insert, String.Format("ReportInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Researches
    public DataTable GetResearchAll(int pageNum, int rowCount)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from researches  order by id desc limit @pageNum,@rowCount", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("pageNum", pageNum * rowCount);
            da.SelectCommand.Parameters.AddWithValue("rowCount", rowCount);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.researches, Utils.LogType.select, String.Format("GetResearchAll()"), ex.Message, "", true);
            return null;
        }
    }
    public int GetResearchesAllCount()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  count(*)  from researches  ", SqlConn);

            da.Fill(dt);
            return dt.Rows[0][0].ToParseInt();
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.researches, Utils.LogType.select, String.Format("GetResearchesAllCount()"), ex.Message, "", true);
            return 0;
        }
    }
    public DataTable GetResearches()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from researches  order by id desc", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.researches, Utils.LogType.select, String.Format("GetResearches()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetResearchesById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from researches where id=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.select, String.Format("GetResearchesById()"), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType ResearchDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("delete from researches WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.researches, Utils.LogType.delete, String.Format("researchesDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType ResearchUpdate(int id, string title_az, string title_en, string content_az, string content_en,
     DateTime date, string full_material, string short_material)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE researches SET 
                                                title_az=@title_az,
                                                title_en=@title_en,
                                                content_az=@content_az,
                                                content_en=@content_en,
                                                date=@date,
                                                full_material=@full_material,
                                                short_material=@short_material
                                              WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@full_material", full_material);
        cmd.Parameters.AddWithValue("@short_material", short_material);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);

        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.update, String.Format("NationalReportsUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public Utils.MethodType ResearchInsert(string title_az, string title_en, string content_az, string content_en,
        DateTime date, string full_material, string short_material)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into researches (title_az,title_en,content_az,content_en,date,full_material,short_material,add_dt) 
                            values (@title_az,@title_en,@content_az,@content_en,@date,@full_material,@short_material,@add_dt)   ", SqlConn);

        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@full_material", full_material);
        cmd.Parameters.AddWithValue("@short_material", short_material);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.insert, String.Format("publicationsInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public string getLastResearchId
    {
        get
        {

            MySqlCommand cmd = new MySqlCommand(string.Format("SELECT id FROM researches order by id desc limit 1", Config.getLangSession), SqlConn);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                (new DALC()).LogInsert(Utils.Tables.pages, Utils.LogType.select, "getLastResearchId () ", ex.Message, "", true);
                return "";
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

    }
    #endregion

    #region publications
    public DataTable GetPublicationsAll(int pageNum, int rowCount)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from publications  order by id desc limit @pageNum,@rowCount", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("pageNum", pageNum * rowCount);
            da.SelectCommand.Parameters.AddWithValue("rowCount", rowCount);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.select, String.Format("GetpublicationsAll()"), ex.Message, "", true);
            return null;
        }
    }
    public int GetPublicationsAllCount()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  count(*)  from publications  ", SqlConn);

            da.Fill(dt);
            return dt.Rows[0][0].ToParseInt();
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.select, String.Format("GetpublicationsAllCount()"), ex.Message, "", true);
            return 0;
        }
    }

    public DataTable GetPublicationsAll(int pageNum, int rowCount, int category_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from publications where category_id=@category_id  order by id desc limit @pageNum,@rowCount", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("pageNum", pageNum * rowCount);
            da.SelectCommand.Parameters.AddWithValue("rowCount", rowCount);
            da.SelectCommand.Parameters.AddWithValue("category_id", category_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.select, String.Format("GetpublicationsAll()"), ex.Message, "", true);
            return null;
        }
    }
    public int GetPublicationsAllCount(int category_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  count(*)  from publications where category_id=@category_id  ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("category_id", category_id);
            da.Fill(dt);
            return dt.Rows[0][0].ToParseInt();
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.select, String.Format("GetpublicationsAllCount()"), ex.Message, "", true);
            return 0;
        }
    }

    public DataTable GetPublications()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from publications  order by id desc", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.select, String.Format("Getpublications()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPublicationsById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *,DATE_FORMAT(date, \"%d.%m.%Y\") as pageDt  from publications where id=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.select, String.Format("GetpublicationsById()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType PublicationDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("delete from publications WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.delete, String.Format("publicationsDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType PublicationUpdate(int id, string title_az, string title_en, string content_az, string content_en,
        DateTime date, string full_material, string short_material, string image_filename, int category_id)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE publications SET 
                                                title_az=@title_az,
                                                title_en=@title_en,
                                                content_az=@content_az,
                                                content_en=@content_en,
                                                date=@date,
                                                image_filename=@image_filename,
                                                full_material=@full_material,
                                                short_material=@short_material,
                                                category_id=@category_id
                                              WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@full_material", full_material);
        cmd.Parameters.AddWithValue("@image_filename", image_filename);
        cmd.Parameters.AddWithValue("@short_material", short_material);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        cmd.Parameters.AddWithValue("@category_id", category_id);

        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.update, String.Format("NationalReportsUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public string getLastPublicationId
    {
        get
        {

            MySqlCommand cmd = new MySqlCommand(string.Format("SELECT id FROM publications order by id desc limit 1", Config.getLangSession), SqlConn);

            try
            {
                cmd.Connection.Open();
                return Config.Cs(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                (new DALC()).LogInsert(Utils.Tables.pages, Utils.LogType.select, "getLastPublicationId () ", ex.Message, "", true);
                return "";
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

    }
    public Utils.MethodType PublicationInsert(string title_az, string title_en, string content_az, string content_en,
        DateTime date, string full_material, string short_material, string image_filename, int category_id)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into publications (image_filename,title_az,title_en,content_az,content_en,date,full_material,short_material,add_dt,category_id) 
                            values (@image_filename,@title_az,@title_en,@content_az,@content_en,@date,@full_material,@short_material,@add_dt,@category_id)   ", SqlConn);

        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@content_az", content_az);
        cmd.Parameters.AddWithValue("@content_en", content_en);
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@full_material", full_material);
        cmd.Parameters.AddWithValue("@short_material", short_material);
        cmd.Parameters.AddWithValue("@image_filename", image_filename);
        cmd.Parameters.AddWithValue("@category_id", category_id);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);


        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.publications, Utils.LogType.insert, String.Format("publicationsInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region useful_links_targets
    public DataTable Get_useful_links_targets(int isNational, int goal_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from useful_links_targets where isNational=@isNational and goal_id=@goal_id order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("isNational", isNational);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("Get_useful_links_targets()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable Get_useful_links_targets(int isNational)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from useful_links_targets where isNational=@isNational order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("isNational", isNational);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("Get_useful_links_targets()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType useful_links_targets_Update(int id, string name_az, string name_en, string link, int goal_id, int isnational)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE useful_links_targets SET goal_id=@goal_id,name_az=@name_az,name_en=@name_en,link=@link,isnational=@isnational WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@link", link);
        cmd.Parameters.AddWithValue("@isnational", isnational);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("useful_links_targets_Update () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType useful_links_targets_Delete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("delete from useful_links_targets WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("useful_links_targets () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType useful_links_targets_Insert(string name_az, string name_en, string link, int goal_id, int isnational)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into useful_links_targets (name_az,name_en,link,isnational,goal_id) 
                            values (@name_az,@name_en,@link,@isnational,@goal_id)  ", SqlConn);

        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@link", link);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@isnational", isnational);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("useful_links_targets_Insert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region useful_links_indicators
    public DataTable Get_useful_links_indicators(int isNational, int goal_id, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select  
 group_concat( concat('<a target=_blank href=""',u.partner_link,'"">',u.partner_name_" + lang + @",'</a>'),'') as concat_partner,
 group_concat(u.qurum_name_" + lang + @") as concat_qurum,
 u.*,
 i.code as IndicatorCode,
 i.name_az as IndicatorName 
 from useful_links_indicators as u
inner join indicators as i on u.indicator_id=i.id
where u.goal_id=@goal_id and u.isNational=@isNational
group by indicator_id
order by i.code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("isNational", isNational);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("Get_useful_links_indicators()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable Get_useful_links_indicators(int isNational, int goal_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from useful_links_indicators where isNational=@isNational and goal_id=@goal_id order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("isNational", isNational);
            da.SelectCommand.Parameters.AddWithValue("goal_id", goal_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("Get_useful_links_indicators()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType useful_links_indicators_Update(int id, string qurum_name_az, string qurum_name_en, string qurum_link,
         string partner_name_az, string partner_name_en, string partner_link,
        int goal_id, int indicator_id, int isnational)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE useful_links_indicators SET 
                                                    goal_id=@goal_id,
                                                    indicator_id=@indicator_id,
                                                    qurum_name_az=@qurum_name_az,
                                                    qurum_name_en=@qurum_name_en,
                                                    qurum_link=@qurum_link,

                                                    partner_name_az=@partner_name_az,
                                                    partner_name_en=@partner_name_en,
                                                    partner_link=@partner_link,

                                                    isnational=@isnational
                                                    WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@qurum_name_en", qurum_name_en);
        cmd.Parameters.AddWithValue("@qurum_name_az", qurum_name_az);
        cmd.Parameters.AddWithValue("@qurum_link", qurum_link);

        cmd.Parameters.AddWithValue("@partner_name_en", partner_name_en);
        cmd.Parameters.AddWithValue("@partner_name_az", partner_name_az);
        cmd.Parameters.AddWithValue("@partner_link", partner_link);

        cmd.Parameters.AddWithValue("@isnational", isnational);
        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("useful_links_indicators_Update () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType useful_links_indicators_Delete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("delete from useful_links_indicators WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("useful_links_indicatorsdelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType useful_links_indicators_Insert(string qurum_name_az, string qurum_name_en, string qurum_link,
                                                            string partner_name_az, string partner_name_en, string partner_link,
                                                            int goal_id, int indicator_id, int isnational)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into useful_links_indicators (qurum_name_az,qurum_name_en,qurum_link,partner_name_az,partner_name_en,partner_link,goal_id,indicator_id,isnational) 
                                                                           values (@qurum_name_az,@qurum_name_en,@qurum_link,@partner_name_az,@partner_name_en,@partner_link,@goal_id,@indicator_id,@isnational)  ", SqlConn);

        cmd.Parameters.AddWithValue("@qurum_name_en", qurum_name_en);
        cmd.Parameters.AddWithValue("@qurum_name_az", qurum_name_az);
        cmd.Parameters.AddWithValue("@qurum_link", qurum_link);

        cmd.Parameters.AddWithValue("@partner_name_en", partner_name_en);
        cmd.Parameters.AddWithValue("@partner_name_az", partner_name_az);
        cmd.Parameters.AddWithValue("@partner_link", partner_link);

        cmd.Parameters.AddWithValue("@isnational", isnational);
        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@goal_id", goal_id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("useful_links_indicators_insert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Users

    public DataTable GetUsers(int isActuve)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from users where is_active=@active ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("active", isActuve);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetUsers()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetUserByOrgCode(string orgCode)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select u.id,u.email,u.is_active from users as u
inner join qurumlar as q on u.org_id=q.id
where q.code=@orgCode
", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("orgCode", orgCode);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetUserByOrgCode()"), ex.Message, "", true);
            return null;
        }
    }
    public string CheckUser(string orgCode)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select count(id) from users as u where u.org_id=@orgCode", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("orgCode", orgCode);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToParseStr();
            }
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetUserByOrgId()"), ex.Message, "", true);
            return "-1";
        }
    }

    public Utils.MethodType UserPassUpdate(int id, string password)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE Users SET password=@password WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("UserPassUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType UserUpdate(int id, int activeStatus)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE Users SET is_Active=@activeStatus WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@activeStatus", activeStatus);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("UserUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType UserUpdate_full_access(int id, int full_access)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE Users SET full_access=@full_access WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@full_access", full_access);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("UserUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType UserInsert(int org_id, string name, string surname, string patronymic, string position, string phone_mob, string phone_work, string email, string pass)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into users (org_id,name,surname,patronymic,position,phone_mob,phone_work,email,password,add_dt,add_ip,is_active) 
                            values (@org_id,@name,@surname,@patronymic,@position,@phone_mob,@phone_work,@email,@password,@add_dt,@add_ip,@is_active)  ", SqlConn);
        cmd.Parameters.AddWithValue("@org_id", org_id);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@surname", surname);
        cmd.Parameters.AddWithValue("@patronymic", patronymic);
        cmd.Parameters.AddWithValue("@position", position);
        cmd.Parameters.AddWithValue("@phone_mob", phone_mob);
        cmd.Parameters.AddWithValue("@phone_work", phone_work);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", pass);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@is_active", "0");

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("UserInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType UserUpdate(int id, int org_id, string name, string surname, string patronymic, string position, string phone_mob, string phone_work, string email, string pass)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE `users` SET 
`org_id`=@org_id,
`name`=@name,
`surname`=@surname,
`patronymic`=@patronymic,
`position`=@position,
`phone_mob`=@phone_mob,
`phone_work`=@phone_work,
`email`=@email,
`password`=@password
 WHERE id=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@org_id", org_id);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@surname", surname);
        cmd.Parameters.AddWithValue("@patronymic", patronymic);
        cmd.Parameters.AddWithValue("@position", position);
        cmd.Parameters.AddWithValue("@phone_mob", phone_mob);
        cmd.Parameters.AddWithValue("@phone_work", phone_work);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", pass);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("UserInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType UserDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"Update users SET is_Active=0 WHERE Id=@id;", SqlConn);


        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.delete, String.Format("UserDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    #region Manager

    public Utils.MethodType UserInfoUpdate(string name, string position, string phone_work, string phone_mob)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE users SET name=@name,position=@position ,phone_work=@phone_work ,phone_mob=@phone_mob  WHERE id=@ID", SqlConn);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@position", position);
        cmd.Parameters.AddWithValue("@phone_work", phone_work);
        cmd.Parameters.AddWithValue("@phone_mob", phone_mob);
        cmd.Parameters.AddWithValue("@ID", DALC.UserInfo.ID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.users, Utils.LogType.update, String.Format("UserInfoUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType UserPassUpdate(string Pass)
    {
        MySqlCommand cmd = new MySqlCommand("UPDATE users SET Password =@Password  WHERE id=@ID", SqlConn);
        cmd.Parameters.AddWithValue("@Password", (Pass));
        cmd.Parameters.AddWithValue("@ID", DALC.UserInfo.ID);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.users, Utils.LogType.update, String.Format("UserPassUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public class StrukturUserInfo
    {
        public int ID { get; set; }
        public bool isLogin { get; set; }
        public int OrgId { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneMob { get; set; }
        public string Email { get; set; }
        public bool FullAccess { get; set; }
    }

    public static StrukturUserInfo UserInfo
    {
        get
        {
            StrukturUserInfo data = new StrukturUserInfo();
            data.isLogin = false;
            data.ID = -1;
            data.FullAccess = false;

            if (HttpContext.Current.Session["UserInfoStruktur"] != null)
            {
                return (StrukturUserInfo)HttpContext.Current.Session["UserInfoStruktur"];
            }



            string USerId = Convert.ToString(HttpContext.Current.Session["UserID"]);

            DataTable dt = new DataTable();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT   id 
                                , org_id                            
                                , name 
                                , surname
                                , position 
                                , phone_work 
                                , phone_mob 
                                , email 
                                , full_access
                            FROM users  where id=@id and is_active=1", SqlConn);

                da.SelectCommand.Parameters.AddWithValue("@id", USerId);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                new DALC().LogInsert(Utils.Tables.users, Utils.LogType.select, String.Format("UserInfo"), ex.Message, true);
                dt = null;
            }

            if (dt == null || dt.Rows.Count < 1) return data;

            DataRow dr = dt.Rows[0];

            if (dr == null) return data;

            data.isLogin = true;
            data.ID = int.Parse(Convert.ToString(dr["id"]));

            data.OrgId = int.Parse(Convert.ToString(dr["org_id"]));

            data.Email = Convert.ToString(dr["email"]);
            data.Name = Convert.ToString(dr["name"]);
            data.Surname = Convert.ToString(dr["surname"]);
            data.Position = Convert.ToString(dr["position"]);
            data.PhoneMob = Convert.ToString(dr["phone_mob"]);
            data.PhoneWork = Convert.ToString(dr["phone_work"]);
            data.FullAccess = dr["full_access"].ToParseStr() == "1" ? true : false;

            HttpContext.Current.Session["UserInfoStruktur"] = data;

            return data;
        }
    }


    /// <summary>
    /// login ve parola uygun melumat
    /// </summary>
    /// <param name="Login"></param>
    /// <param name="Pass"></param>
    /// <returns></returns>
    public string GetUser(string Login, string Pass)
    {
        MySqlCommand cmd = new MySqlCommand(@"select u.*,q.code from users as u 
                                                inner join qurumlar as q on q.id=u.org_id
                                                where q.code=@Login and u.password=@password ", SqlConn);
        cmd.Parameters.AddWithValue("@Login", Login);
        cmd.Parameters.AddWithValue("@password", (Pass));

        try
        {
            cmd.Connection.Open();
            return Config.Cs(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.users, Utils.LogType.select, String.Format("GetUser ( Login: {0} ) ", Login), ex.Message, "", true);
            return "-1";
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    /// <summary>
    /// managerid -e uygun melumatlari getirir
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DataTable GetUser(int Id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM users WHERE ID=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("@id", Id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.users, Utils.LogType.select,
                String.Format("GetUser(id:{0})", Id), ex.Message, "", true);
            return null;
        }
    }



    #endregion

    #endregion

    #region Hesabat
    public DataTable GetHesabat1(int indicator_id, int year)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from hesabat where indicator_id=@indicator_id and year=@year ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.SelectCommand.Parameters.AddWithValue("year", year);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat1()"), ex.Message, "", true);
            return null;
        }
    }
    public int CheckHesabatCount(int indicator_id, int year)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  count(*)  from hesabat where indicator_id=@indicator_id and year=@year ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.SelectCommand.Parameters.AddWithValue("year", year);
            da.Fill(dt);


            if (dt == null)
            {
                return 0;
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToParseInt();
            }
            return 0;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("CheckHesabatCount()"), ex.Message, "", true);
            return 0;
        }
    }
    public Utils.MethodType HesabatNullInsert(int user_id, int indicator_id, int year)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into hesabat (user_id,indicator_id,region_id,year,add_dt,add_ip,value,is_active)
select @user_id,@indicator_id,id,@year,@add_dt,@add_ip,'',0 from regions where is_active=1   ", SqlConn);

        cmd.Parameters.AddWithValue("@user_id", user_id);
        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("HesabatInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public DataTable GetHesabatforChart_Years(int indicator_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  year from hesabat as h where indicator_id=@indicator_id group by year", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabatforChart_Years()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetHesabatforChart_Indicators_1(int indicator_id, int[] years)
    {
        try
        {
            string year = "";
            for (int i = 0; i < years.Length; i++)
            {
                if (i == 0)
                {
                    year = years[i].ToParseStr();
                }
                else
                {
                    year = year + "," + years[i].ToParseStr();
                }
            }
            //year = "2015,2016";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT indicator_id from hesabat as h " +
                "where indicator_id=@indicator_id and length(value)>0 and value not in ('-','...','x') " +
                " and value is not null and year in (" + year + ") and is_active=1 group by indicator_id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabatforChart_Indicators_1()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetHesabatforChart_Years_1(int indicator_id, int[] years)
    {
        try
        {
            string year = "";
            for (int i = 0; i < years.Length; i++)
            {
                if (i == 0)
                {
                    year = years[i].ToParseStr();
                }
                else
                {
                    year = year + "," + years[i].ToParseStr();
                }
            }
            //year = "2015,2016";

           DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  year from hesabat as h " +
                "where indicator_id=@indicator_id and length(value)>0 and value not in ('-','...','x') " +
                " and value is not null and year in (" + year+ ") and is_active=1 group by year", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabatforChart_Years_1()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetHesabat_Indicators(List<int> indicators, int[] years)
    {
        try
        {
            string _indicators = "0";
            foreach (int item in indicators)
            {
                _indicators += item + ",";
            }
            _indicators = _indicators.Trim(',');
            string year = "";
            for (int i = 0; i < years.Length; i++)
            {
                if (i == 0)
                {
                    year = years[i].ToParseStr();
                }
                else
                {
                    year = year + "," + years[i].ToParseStr();
                }
            }
            DataTable dt = new DataTable();

            MySqlDataAdapter da = new MySqlDataAdapter("SELECT indicator_id from hesabat as h " +
                "where indicator_id in (" + _indicators + ") " +
                " and length(value)> 0 and value not in ('-', '...', 'x') and value is not null and year in ("
                + year + ") and is_active=1 group by indicator_id", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat_Years()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetHesabat_Years(List<int> indicators,int[] years)
    {
        try
        {
            string _indicators = "0";
            foreach (int item in indicators)
            {
                _indicators += item + ",";
            }
            _indicators = _indicators.Trim(',');
            string year = "";
            for (int i = 0; i < years.Length; i++)
            {
                if (i == 0)
                {
                    year = years[i].ToParseStr();
                }
                else
                {
                    year = year + "," + years[i].ToParseStr();
                }
            }
            DataTable dt = new DataTable();
              
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT year from hesabat as h " +
                "where indicator_id in ("+_indicators + ") " +
                " and length(value)> 0 and value not in ('-', '...', 'x') and value is not null and year in ("
                + year + ") and is_active=1 group by year", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat_Years()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetHesabat_Years()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT year from hesabat as h where is_active=1 group by year", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat_Years()"), ex.Message, "", true);
            return null;
        }
    }

    public string GetHesabatforChart_Value(int indicator_id, int year)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  IFNULL(value,'-') from hesabat as h " +
                "where indicator_id=@indicator_id and  h.is_active = 1 and h.value is not null and " +
                "length(h.value)> 0 and h.value not in ('-', '...', 'x')  and year=@year limit 1", SqlConn);
            
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.SelectCommand.Parameters.AddWithValue("year", year);

            da.Fill(dt);

            if (dt == null)
            {
                return "";
            }

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToParseStr();
            }

            return "";
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabatforChart()"), ex.Message, "", true);
            return "";
        }
    }

    public DataTable GetHesabat(int user_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from hesabat where user_id=@user_id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("user_id", user_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetHesabatById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from hesabat where id=@Id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("Id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabatById()"), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType HesabatUpdate1(int indicator_id, int region_id, int year, object value, object footnote_id)
    {
        MySqlCommand cmd = new MySqlCommand(@"update hesabat set value=@value,footnote_id=@footnote_id,is_active=1
                            where indicator_id=@indicator_id and year=@year and region_id=@region_id;", SqlConn);

        cmd.Parameters.AddWithValue("@footnote_id", footnote_id);
        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@region_id", region_id);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@value", value);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("HesabatUpdate1 () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType HesabatUpdate(int id, int user_id, int indicator_id, int region_id, int year, object value)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE hesabat SET 
                                                        indicator_id=@indicator_id,
                                                        region_id=@region_id,
                                                        year=@year,is_active=1
                                                        value=@value
                                                  WHERE Id=@id and user_id=@user_id ;", SqlConn);

        cmd.Parameters.AddWithValue("@user_id", user_id);
        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@region_id", region_id);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@value", value);

        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("HesabatUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType HesabatInsert(int user_id, int indicator_id, int region_id, int year, object value)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into hesabat (user_id,indicator_id,region_id,year,value,add_dt,add_ip,is_active) 
                            values (@user_id,@indicator_id,@region_id,@year,@value,@add_dt,@add_ip,@is_active)   ", SqlConn);

        cmd.Parameters.AddWithValue("@user_id", user_id);
        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@region_id", region_id);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@value", value);
        cmd.Parameters.AddWithValue("@is_active", "0");
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("HesabatInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region MedataList

    public DataTable GetMetaDataList()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM metadata_list as m where is_active=1 order by cast(no as unsigned)", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMetaDataList  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType MetaDataListUpdate(int id, int sub_id, string name_az, string name_en, string no, string code, string description)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE metadata_list SET sub_id=@sub_id,
                                                                        name_az=@name_az,
                                                                        name_en=@name_en,
                                                                        no=@no,
                                                                        description=@description,
                                                                        code=@code
                                                                        WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@sub_id", sub_id);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@no", no);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@description", description);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataListDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("update metadata_list set is_active=0 WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataListInsert(int sub_id, string name_az, string name_en, string no, string code, string description)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into metadata_list (sub_id,Name_az,Name_en,is_Active,no,code,description) 
                            values(@sub_id,@Name_az,@Name_en,1,@no,@code,@description)", SqlConn);

        cmd.Parameters.AddWithValue("@sub_id", sub_id);
        cmd.Parameters.AddWithValue("@Name_az", name_az);
        cmd.Parameters.AddWithValue("@Name_en", name_en);
        cmd.Parameters.AddWithValue("@no", no);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@description", description);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion


    #region MedataList_global

    public DataTable GetMetaDataList_global()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM metadata_global_list as m where is_active=1 order by cast(code as unsigned)", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMenuClient  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType MetaDataListUpdate_global(int id, string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE metadata_global_list SET name_az=@name_az,name_en=@name_en,code=@code WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListUpdate_global () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataListDelete_global(int id)
    {
        MySqlCommand cmd = new MySqlCommand("update metadata_global_list set is_active=0 WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataListInsert_global(string name_az, string name_en, string code)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into metadata_global_list (Name_az,Name_en,is_Active,code) 
                            values(@Name_az,@Name_en,1,@code)", SqlConn);

        cmd.Parameters.AddWithValue("@Name_az", name_az);
        cmd.Parameters.AddWithValue("@Name_en", name_en);
        cmd.Parameters.AddWithValue("@code", code);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Medata
    public DataTable GetMetaDataClient2(int indicator_id, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  ml.sub_id,m.*,ml.name_" + lang + @" as l_name from metadata  as m 
                inner join metadata_list as ml on m.list_id=ml.id 
                where  m.indicator_id=@indicator_id
                order by list_id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMetaDataClient  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetMetaDataClient(int indicator_id, int parentId, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  ml.sub_id,m.*,ml.name_" + lang + @" as l_name from metadata  as m 
                inner join metadata_list as ml on m.list_id=ml.id 
                where ml.sub_id=@sub_id and m.indicator_id=@indicator_id and m.is_active=1 and ml.is_active=1
                order by list_id ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.SelectCommand.Parameters.AddWithValue("sub_id", parentId);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMetaDataClient  "), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType MetadataUpdateAll(int indicator_id)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into metadata (indicator_id,list_id,add_dt,add_ip,is_active)
                        select @indicator_id,id,@add_dt,@add_ip,@is_active from metadata_list 
                        where is_active=1 and id not in 
                        (select list_id from metadata where indicator_id=@indicator_id)", SqlConn);

        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@is_active", true);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public DataTable GetMetaData(int indicator_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT m.*,ml.sub_id,ml.no,ml.code,ml.name_az as ml_name_az,ml.name_en as ml_name_en FROM metadata as m
                                    inner join metadata_list as ml on m.list_id=ml.id
                                    where m.indicator_id=@indicator_id and m.is_active=1 order by cast(no as unsigned)", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMetaData  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType MetaDataUpdate(int indicator_id, int list_id, string name_az, string name_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE metadata SET 
                                                name_az=@name_az,
                                                name_en=@name_en 
                                                WHERE  indicator_id=@indicator_id and list_id=@list_id", SqlConn);

        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@list_id", list_id);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        // cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("update metadata set is_active=0 WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataInsert(int indicator_id, int list_id, string name_az, string name_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into metadata (indicator_id,list_id,Name_az,Name_en,is_Active,add_dt,add_ip) 
                            values(@indicator_id,@list_id,@Name_az,@Name_en,1,@addDt,@ip)", SqlConn);

        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@list_id", list_id);

        cmd.Parameters.AddWithValue("@Name_az", name_az);
        cmd.Parameters.AddWithValue("@Name_en", name_en);

        cmd.Parameters.AddWithValue("@addDt", DateTime.Now);
        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion

    #region Medata_global
    public DataTable GetMetaDataClient_global(int indicator_id, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  m.*,ml.name_" + lang + @" as l_name from metadata_global  as m 
                inner join metadata_global_list as ml on m.list_id=ml.id 
                where  m.indicator_id=@indicator_id and ml.is_active=1 and m.is_active=1
                order by cast( ml.code as unsigned ) ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMetaDataClient  "), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType MetadataUpdateAll_global(int indicator_id)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into metadata_global (indicator_id,list_id,add_dt,add_ip,is_active)
                        select @indicator_id,id,@add_dt,@add_ip,@is_active from metadata_global_list 
                        where is_active=1 and id not in 
                        (select list_id from metadata_global where indicator_id=@indicator_id)", SqlConn);

        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@is_active", true);
        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public DataTable GetMetaData_global(int indicator_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT m.*,ml.code FROM metadata_global as m 
inner join metadata_global_list as ml on m.list_id=ml.id where m.indicator_id=@indicator_id and m.is_active=1 
and ml.is_active=1 

order by cast( ml.code as unsigned ) ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetMetaData  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType MetaDataUpdate_global(int id, int indicator_id, int list_id, string name_az, string name_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE metadata_global SET 
                                                indicator_id=@indicator_id,
                                                list_id=@list_id,
                                                name_az=@name_az,
                                                name_en=@name_en 
                                                WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@list_id", list_id);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataDelete_global(int id)
    {
        MySqlCommand cmd = new MySqlCommand("update metadata_global set is_active=0 WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType MetaDataInsert_global(int indicator_id, int list_id, string name_az, string name_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into metadata_global (indicator_id,list_id,Name_az,Name_en,is_Active,add_dt,add_ip) 
                            values(@indicator_id,@list_id,@Name_az,@Name_en,1,@addDt,@ip)", SqlConn);

        cmd.Parameters.AddWithValue("@indicator_id", indicator_id);
        cmd.Parameters.AddWithValue("@list_id", list_id);

        cmd.Parameters.AddWithValue("@Name_az", name_az);
        cmd.Parameters.AddWithValue("@Name_en", name_en);

        cmd.Parameters.AddWithValue("@addDt", DateTime.Now);
        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion


    #region Hesabat1
    public DataTable GetHesabat2(string indicator_ids, string years, string region_ids, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(string.Format(@"select h.id,h.indicator_id,h.year,h.value,h.region_id,
i.code as IndicatorCode,i.name_{0}  as IndicatorName ,
g.name_short_{0} as GoalName,
isx.name_{0} as IndicatorSize,
r.name_{0} as RegionName 
from hesabat  as h
inner join indicators as i on i.id=h.indicator_id
inner join goals as g on g.id=i.goal_id
inner join indicator_size as isx on isx.id=i.size_id
inner join regions as r on r.id=h.region_id
where h.is_active=1 and h.indicator_id in (" + indicator_ids + ") and h.year in (" + years +
") and h.region_id in (" + region_ids + ") group by i.name_az,r.name_az", lang), SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat2()"), ex.Message, "", true);
            return null;
        }
    }
    public string GetHesabat2_value(int indicator_id, int year, int region_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select h.value
from hesabat as h
where h.indicator_id =@indicator_id and h.year=@year and h.region_id=@region_id and h.is_active=1", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.SelectCommand.Parameters.AddWithValue("year", year);
            da.SelectCommand.Parameters.AddWithValue("region_id", region_id);
            da.Fill(dt);

            if (dt == null)
            {
                return "0";
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToParseStr();
            }
            return "0";
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat2_value()"), ex.Message, "", true);
            return "0";
        }
    }
    public DataTable GetHesabat21(string indicator_ids, string years, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(string.Format(@"select h.id,h.indicator_id,h.year,h.value,
i.code as IndicatorCode,i.name_{0}  as IndicatorName ,
g.name_short_{0} as GoalName,
isx.name_{0} as IndicatorSize
from hesabat  as h
inner join indicators as i on i.id=h.indicator_id
inner join goals as g on g.id=i.goal_id
inner join indicator_size as isx on isx.id=i.size_id
where h.is_active=1  and h.indicator_id in (" + indicator_ids + ") and h.year in (" + years + ") group by i.name_az order by i.code", lang), SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat21()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetHesabat2(string indicator_ids, string years, string lang)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(string.Format(@"select h.id,h.indicator_id,h.year,h.value,
i.code as IndicatorCode,i.name_{0}  as IndicatorName ,
g.name_short_{0} as GoalName,
isx.name_{0} as IndicatorSize
from hesabat  as h
inner join indicators as i on i.id=h.indicator_id
inner join goals as g on g.id=i.goal_id
inner join indicator_size as isx on isx.id=i.size_id
where h.is_active=1 and h.value is not null and length(h.value)> 0 and h.value not in ('-', '...', 'x')  and h.indicator_id in (" + indicator_ids + 
") and h.year in (" + years + ") group by i.name_az order by i.code", lang), SqlConn);
              
               
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat2_2inci()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetHesabat2_value(int indicator_id, int year)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"select h.value,h.footnote_id from hesabat as h
                    where h.indicator_id=@indicator_id and h.year=@year and h.is_active=1", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("indicator_id", indicator_id);
            da.SelectCommand.Parameters.AddWithValue("year", year);
            da.Fill(dt);
            string a = "";
            if (dt.Rows.Count == 0)
            {
                a = "error";
                DataRow dr = dt.NewRow();
                dr["value"] = "" ;
                dr["footnote_id"] = DBNull.Value;
                dt.Rows.Add(dr);
            }
            else
            {
                a = dt.Rows[0]["value"].ToParseStr();
            }
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.goals, Utils.LogType.select, String.Format("GetHesabat2_value_2ci()"), ex.Message, "", true);
            return null;
        }
    }

    #endregion




    public DataTable GetSubscribers()
    {

        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT *  FROM subscribers where subscribe=1", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public int CheckSubscribe(int id)
    {

        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM subscribers where subscribe=1 and id=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt.Rows.Count;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
    public Utils.MethodType SubscribeInsert(string name, string surname, string patronymic, string email)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into subscribers (name,surname,patronymic,email) 
                            values (@name,@surname,@patronymic,@email)  ", SqlConn);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@surname", surname);
        cmd.Parameters.AddWithValue("@patronymic", patronymic);
        cmd.Parameters.AddWithValue("@email", email);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("SubscribeInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType SubscribeUpdate(int id)
    {
        MySqlCommand cmd = new MySqlCommand(@"Update  subscribers set subscribe=0 where id=@id", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("SubscribeUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public DataTable GetIndicators_status()
    {

        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT *  FROM indicators_status", SqlConn);

            da.Fill(dt);


            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    #region PublicationCategory

    public DataTable GetPublicationCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM publication_category as m where is_active=1  order by orderBy", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetPublicationCategory  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType PublicationCategoryUpdate(int id, int orderBy, string name_az, string name_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE publication_category SET orderBy=@orderBy,name_az=@name_az,name_en=@name_en WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@orderBy", orderBy);
        cmd.Parameters.AddWithValue("@name_az", name_az);
        cmd.Parameters.AddWithValue("@name_en", name_en);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("PublicationCategoryUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType PublicationCategoryDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("update publication_category set is_active=0 WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("PublicationCategoryDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType PublicationCategoryInsert(int orderBy, string name_az, string name_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into publication_category (orderBy,Name_az,Name_en,is_active) 
                            values(@orderBy,@Name_az,@Name_en,1)", SqlConn);

        cmd.Parameters.AddWithValue("@orderBy", orderBy);
        cmd.Parameters.AddWithValue("@Name_az", name_az);
        cmd.Parameters.AddWithValue("@Name_en", name_en);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("PublicationCategoryInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public DataTable GetPublicationCategroyById(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  *  from publication_category where id=@id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.national_reports, Utils.LogType.select, String.Format("GetPublicationCategroyById()"), ex.Message, "", true);
            return null;
        }
    }

    #endregion


    #region Footnotes

    public DataTable GetFootnotes(int org_id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from footnotes where org_id=@org_id and is_active=1 order by id desc", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("org_id", org_id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetFootnotes  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetFootnotes()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from footnotes where is_active=1 order by id desc", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetFootnotes  "), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetFootnotesOrderById()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from footnotes where is_active=1 order by id ", SqlConn);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.v_menu, Utils.LogType.select, String.Format("GetFootnotes  "), ex.Message, "", true);
            return null;
        }
    }
    public Utils.MethodType FootnoteUpdate(int id, string name, string desc_az, string desc_en)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE footnotes SET 
                                                name=@name,
                                                desc_az=@desc_az,
                                                desc_en=@desc_en
                                                WHERE Id=@id;", SqlConn);

        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@desc_az", desc_az);
        cmd.Parameters.AddWithValue("@desc_en", desc_en);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    public Utils.MethodType FootnoteInsert(string name, string desc_az, string desc_en, int user_id, int org_id)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into footnotes (name,desc_az,desc_en,user_id,org_id,is_active) 
                            values (@name,@desc_az,@desc_en,@user_id,@org_id,@is_active) ", SqlConn);

        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@desc_az", desc_az);
        cmd.Parameters.AddWithValue("@desc_en", desc_en);
        cmd.Parameters.AddWithValue("@user_id", user_id);
        cmd.Parameters.AddWithValue("@org_id", org_id);
        cmd.Parameters.AddWithValue("@is_active", 1);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("MetaDataListInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType FootnoteDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("update footnotes set is_active=0 WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.Menu, Utils.LogType.update, String.Format("FootnoteDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    #endregion


    #region Partnyors



    public DataTable GetPartners()
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * from partners order by id desc ", SqlConn);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetPartners()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetPartner(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT  * from partners where id=@id order by id desc ", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetPartner()"), ex.Message, "", true);
            return null;
        }
    }

    public Utils.MethodType PartnerUpdate(int id, string title_az, string title_en, string image_url, string url)
    {
        MySqlCommand cmd = new MySqlCommand(@"UPDATE partners SET 
                                                    title_az=@title_az,
                                                    title_en=@title_en,
                                                    image_url=@image_url,
                                                    url=@url,
                                                    update_dt=@dt,
                                                    update_ip=@ip
                                                    WHERE Id=@id;", SqlConn);
        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@image_url", image_url);
        cmd.Parameters.AddWithValue("@url", url);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.pages, Utils.LogType.update, String.Format("PartnerUpdate () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }
    public Utils.MethodType PartnerDelete(int id)
    {
        MySqlCommand cmd = new MySqlCommand("delete from partners where id=@id ", SqlConn);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.delete, String.Format("PartnerDelete () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }

    public Utils.MethodType PartnerInsert(string title_az, string title_en, string image_url, string url)
    {
        MySqlCommand cmd = new MySqlCommand(@"insert into partners (title_az,title_en,image_url,add_dt,add_ip,url) 
                            values (@title_az,@title_en,@image_url,@add_dt,@add_ip,@url) ", SqlConn);

        cmd.Parameters.AddWithValue("@title_az", title_az);
        cmd.Parameters.AddWithValue("@title_en", title_en);
        cmd.Parameters.AddWithValue("@image_url", image_url);
        cmd.Parameters.AddWithValue("@url", url);
        cmd.Parameters.AddWithValue("@add_ip", HttpContext.Current.Request.UserHostAddress);

        cmd.Parameters.AddWithValue("@add_dt", DateTime.Now);
        try
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            return Utils.MethodType.Succes;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.insert, String.Format("PartnerInsert () "), ex.Message, "", true);
            return Utils.MethodType.Error;
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Dispose();
        }
    }


    #endregion

    #region Search



    public DataTable GetSearchGeneral_Groups(string searchText)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"
select 
ptg.id as group_id,
ptg.name_az as group_name_az,
ptg.name_en as group_name_en,
ptg.orderby
 from pages as p 
inner join pages_type as pt on p.type_id=pt.id
inner join pages_type_group as ptg on ptg.id=pt.group_id
where (title_az like @searchText or title_en like @searchText or content_az like @searchText or content_en like @searchText)
and pt.searchable=1
group by ptg.id
order by ptg.orderby
", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("searchText", "%" + searchText + "%");
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSearchGeneral_Groups()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetSearchGeneral_Items(int grpId, string searchText)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"
select 

p.id,
p.page_id,
p.goal_id,
p.type_id,
p.title_az,
p.title_en,
p.content_az,
p.content_en

 from pages as p 
inner join pages_type as pt on p.type_id=pt.id
inner join pages_type_group as ptg on ptg.id=pt.group_id
where (title_az like @searchText or title_en like @searchText or content_az like @searchText or content_en like @searchText)
and pt.searchable=1 and ptg.id=@grpId 

order by ptg.orderby
", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("grpId", grpId);
            da.SelectCommand.Parameters.AddWithValue("searchText", "%" + searchText + "%");
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSearchGeneral_Items()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetSearchGeneral_Pub_Res(string searchText)
    {
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"
select 1000 as type_id,0 as page_id,0 as goal_id,id,title_az,title_en,content_az,content_en from researches as r 
where (title_az like @searchText or title_en like @searchText or content_az like @searchText or content_en like @searchText)
union
select 2000 as type_id,0 as page_id,0 as goal_id,id,title_az,title_en,content_az,content_en from publications as p 
where (title_az like @searchText or title_en like @searchText or content_az like @searchText or content_en like @searchText)

", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("searchText", "%" + searchText + "%");
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSearchGeneral_Pub_Res()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetSearch_Goal(string id, string name)
    {
        try
        {
            string where = "";
            if (id.Length > 0)
            {
                where += "where id like @id ";
            }
            if (name.Length > 0)
            {
                if (where.Length > 0)
                {
                    where = where + " or ";
                }
                else
                {
                    where = " where " + where;
                }
                where += "name_az like @name or name_en like @name";
            }

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(@"Select * from goals " + where + " order by id", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("id", "%" + id + "%");
            da.SelectCommand.Parameters.AddWithValue("name", "%" + name + "%");

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSearch_Goal()"), ex.Message, "", true);
            return null;
        }
    }

    public DataTable GetSearch_Target(string code, string name, string milli_priotet)
    {
        try
        {
            string where = "";
            if (code.Length > 0)
            {
                where += " code like @code ";
            }
            if (name.Length > 0)
            {
                if (where.Length > 0)
                {
                    where = where + " or ";
                }
                else
                {
                    where = " where " + where;
                }
                where += "name_az like @name or name_en like @name";
            }
            if (milli_priotet.Length > 0)
            {
                if (where.Length > 0)
                {
                    where = where + " or ";
                }
                else
                {
                    where = " where " + where;
                }
                where += " milli_priotet=@milli_priotet ";
            }
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from targets where is_active=1 and ( " + where + " ) order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("code", "%" + code + "%");
            da.SelectCommand.Parameters.AddWithValue("name", "%" + name + "%");
            da.SelectCommand.Parameters.AddWithValue("milli_priotet", milli_priotet);

            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSearch_Target()"), ex.Message, "", true);
            return null;
        }
    }
    public DataTable GetSearch_Indicator(string code, string name, string milli_priotet, string qurum_id, string status)
    {
        try
        {
            string where = "";
            if (code.Length > 0)
            {
                where += " indicatorCode(code) like @code ";
            }

            if (name.Length > 0)
            {
                if (where.Length > 0)
                {
                    where = where + " or ";
                }

                where += " name_az like @name or name_en like @name ";
            }

            if (milli_priotet.Length > 0)
            {
                if (where.Length > 0)
                {
                    where = where + " or ";
                }
                where += " uygunluq_id=@uygunluq_id ";
            }
            if (qurum_id.Length > 0)
            {
                if (where.Length > 0)
                {
                    where = where + " or ";
                }
                where += " qurum_id=@qurum_id ";
            }
            if (status.Length > 0)
            {
                if (where.Length > 0)
                {
                    where = where + " or ";
                }
                where += " status_id=@status_id ";
            }

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from indicators where isactive=1 and ( " + where + " ) order by code", SqlConn);
            da.SelectCommand.Parameters.AddWithValue("code", "%" + code + "%");
            da.SelectCommand.Parameters.AddWithValue("name", "%" + name + "%");
            da.SelectCommand.Parameters.AddWithValue("uygunluq_id", milli_priotet);
            da.SelectCommand.Parameters.AddWithValue("qurum_id", qurum_id);
            da.SelectCommand.Parameters.AddWithValue("status_id", status);


            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            LogInsert(Utils.Tables.slider, Utils.LogType.select, String.Format("GetSearch_Indicator()"), ex.Message, "", true);
            return null;
        }
    }
    #endregion

}