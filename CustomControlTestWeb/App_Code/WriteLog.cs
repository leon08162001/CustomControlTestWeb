using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Text;

	/// <summary>
	/// WriteLog ���K�n�y�z�C
	/// </summary>
	public class WriteLog
	{
		private Dac db;

		/// <summary>
		/// Initializes a new instance of the <see cref="WriteLog"/> class.
		/// </summary>
		public WriteLog()
		{
			db = new Dac();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="WriteLog"/> class.
		/// </summary>
		/// <param name="ex">The ex.</param>
		public WriteLog(Exception ex)
		{
			string SPName;
			if (ex.GetType().Name=="SqlException")
			{
				SPName=((SqlException)ex).Procedure;
			}
			else{SPName="";}
			string StrSQL;
			StrSQL ="Insert Into [Exp]([ExpType],[SPName],[ErrMsg],[StackTrace],[Createdate]) Values('" + ex.GetType().Name.Replace("'","none")  + "','" + SPName.Replace("'","none") + "','" + ex.Message.Replace("'","none")  + "','" + ex.StackTrace.Replace("'","none")  + "',getdate())";
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["APTemplateDB"]);
			con.Open();
			SqlCommand command = new SqlCommand(StrSQL,con);
			command.ExecuteNonQuery();
			con.Close();
		}
			
		/// <summary>
		/// �O���N�~���p
		/// </summary>
		/// <param name="ex">Exception ����</param>
		public void LogExp(Exception ex)
		{
			StreamWriter sw;
			string LogPath = System.AppDomain.CurrentDomain.BaseDirectory+"log.txt";
			FileInfo LogInfo = new FileInfo(LogPath);
			sw = new StreamWriter(LogPath, true, Encoding.Default);
			sw.WriteLine(ex.Message);
			sw.Close();
		}



		//		public static void LogExp(string FileName, Exception ex)
		//		{
		//			StreamWriter sw=new StreamWriter(FileName,true,System.Text.Encoding.Default);
		//			sw.WriteLine(ex.GetType().Name.Replace("'","none") + "," + ex.Message.Replace("'","none") + "," +  ex.StackTrace.Replace("'","none") + "," +DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
		//			sw.Close();
		//		}

		/// <summary>
		///�O����Ʈw�ާ@
		///�O���Q���檺SQL���O
		/// </summary>
		/// <param name="cmdSQL">The CMD SQL.</param>
		public void LogDB(string cmdSQL)
		{
			string strSQL;
			int Roa;
			strSQL="Insert Into [AccessLog] Values('','" + cmdSQL.Replace("'","none") + "','','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
			db.RunSQL(strSQL, out Roa,"APTemplateDB");
		}


		//�O���ثe�ϥΪ̤�SQL���O
		/// <summary>
		/// Logs the DB.
		/// </summary>
		/// <param name="cmdSQL">The CMD SQL.</param>
		/// <param name="currentUser">The current user.</param>
		public void LogDB(string cmdSQL, string currentUser)
		{
			string strSQL;
			int Roa;
			strSQL="Insert Into [AccessLog] Values('" + currentUser + "','" + cmdSQL.Replace("'","none") + "','','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
			db.RunSQL(strSQL, out Roa,"APTemplateDB");
		}


		//�O���ϥΪ�,���ε{���Ҧb���|��SQL���O
		/// <summary>
		/// Logs the DB.
		/// </summary>
		/// <param name="cmdSQL">The CMD SQL.</param>
		/// <param name="currentUser">The current user.</param>
		/// <param name="AppPath">The app path.</param>
		public void LogDB(string cmdSQL, string currentUser, string AppPath)
		{
			string strSQL;
			int Roa;
			strSQL="Insert Into [AccessLog] Values('" + currentUser + "','" + cmdSQL.Replace("'","none") + "','" + AppPath + "','" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')";
			db.RunSQL(strSQL, out Roa,"APTemplateDB");
		}


		//���o�ҥ~��ƦC��
		/// <summary>
		/// Gets the exp list.
		/// </summary>
		/// <returns></returns>
		public DataTable GetExpList()
		{
			DataTable dt = null;
			string strSQL ="";
			strSQL="select * from [exp] Order By CreateDate";			
			dt = db.RunSQL(strSQL,"ExpList","APTemplateDB");
			return dt;
		}


		//���o��Ʈw�s����ƦC��
		/// <summary>
		/// Gets the DB access list.
		/// </summary>
		/// <returns></returns>
		public DataTable GetDBAccessList()
		{
			DataTable dt = null;
			string strSQL ="";
			strSQL="select * from [AccessLog] Order By CreateDate";
			dt = db.RunSQL(strSQL,"ExpList","APTemplateDB");
			return dt;
		}
	}
