using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; 
using System.Web.UI;
using System.Configuration;

	/// <summary>
	/// 資料存取控制元件
	/// </summary>
	public  class Dac : IDisposable
	{
		private SqlConnection  DbCon;
		//private UserInformation userInfo;

		private string strCommandText = "";
		private string strCommandType= "";
		public Transaction transaction =new Transaction();

		/// <summary>
		/// 開啟資料庫
		/// </summary>
		/// <param name="Key">web.config中的appsettings 名稱，取得connection string</param>
		private void Open(string Key)
		{
			if (Key=="") {Key="Default";}
			DbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
			DbCon.Open();
		}

		/// <summary>
		/// <see cref="Dac"/> class建構子
		/// </summary>
		/// <param name="UserInfo">The user info.</param>
		//public Dac(UserInformation UserInfo)
		//{
		//  userInfo=UserInfo;
		//}


		/// <summary>
		/// 關閉資料庫連線
		/// </summary>
		private void CloseDB()
		{
			DbCon.Close();
		}

		/// <summary>
		/// 具體化Command物件,並加入整數型別的Return Value(主要用於執行預儲程序所影響的列數)
		/// </summary>
		/// <example>
		/// 以下為使用BuildIntCommand的範例程式：
		/// <code>
		/// // Create parameter array
		///	SqlParameter[] parameters =
		///	{
		///		new SqlParameter( "@LOGIN_ID", Sqldbkind.NVarChar, 50 ),	// 0
		///		new SqlParameter( "@DOMAIN", Sqldbkind.NVarChar, 50 ),	// 1
		///	};
		///	
		///	// Set parameter values and directions
		///	parameters[ 0 ].Value = LOGINID;
		///	parameters[ 1 ].Value = DOMAIN;
		///
		///	SqlCommand cmd =BuildIntCommand("spCustomProcedure",parameters);
		/// </code>
		/// </example>
		/// <param name="StoredProcName">預存程序名稱</param>
		/// <param name="Parameters">預存程序的參數陣列</param>
		/// <returns></returns>
		private SqlCommand BuildIntCommand(string StoredProcName,IDataParameter[] Parameters)
		{
			SqlCommand command=	BuildQueryCommand(StoredProcName,Parameters);
			SqlParameter Parameter = new SqlParameter();
			Parameter.ParameterName="ReturnValue";
            Parameter.DbType = DbType.Int32;
			Parameter.Size=4;
			Parameter.Direction=ParameterDirection.ReturnValue;
			Parameter.Precision=0;
			Parameter.Scale=0;
			Parameter.SourceColumn=string.Empty;
			Parameter.SourceVersion=DataRowVersion.Default;
			Parameter.Value=null;
			command.Parameters.Add(Parameter);
			return command;
		}


		/// <summary>
		/// 具體化Command物件
		/// </summary>
		/// <example>
		/// 以下為使用BuildQueryCommand的範例程式：
		/// <code>
		/// // Create parameter array
		///	SqlParameter[] parameters =
		///	{
		///		new SqlParameter( "@LOGIN_ID", Sqldbkind.NVarChar, 50 ),	// 0
		///		new SqlParameter( "@DOMAIN", Sqldbkind.NVarChar, 50 ),	// 1
		///	};
		///	
		///	// Set parameter values and directions
		///	parameters[ 0 ].Value = LOGINID;
		///	parameters[ 1 ].Value = DOMAIN;
		///
		///	SqlCommand cmd =BuildIntCommand("spQueryProcedure",parameters);
		/// </code>
		/// </example>
		/// <param name="StoredProcName">預存程序名稱</param>
		/// <param name="Parameters">預存程序參數陣列</param>
		/// <returns></returns>
		private SqlCommand BuildQueryCommand(string StoredProcName,IDataParameter[] Parameters)
		{
			SqlCommand command  = new SqlCommand(StoredProcName,DbCon);
			command.CommandType=CommandType.StoredProcedure;
		
			if (Parameters!=null)
			{
				foreach (SqlParameter Parameter in Parameters) 
				{
					command.Parameters.Add(Parameter);
				}
			}
			return command;
		}


		/// <summary>
		/// 執行預儲程序並傳回影響的列數
		/// </summary>
		/// <example>
		/// 以下為使用RunProcedure的範例程式：
		/// <code>
		/// // Create parameter array
		///	SqlParameter[] parameters =
		///	{
		///		new SqlParameter( "@LOGIN_ID", Sqldbkind.NVarChar, 50 ),	// 0
		///		new SqlParameter( "@DOMAIN", Sqldbkind.NVarChar, 50 ),	// 1
		///	};
		///	
		///	// Set parameter values and directions
		///	parameters[ 0 ].Value = LOGINID;
		///	parameters[ 1 ].Value = DOMAIN;
		///
		///int rowsAffected=0; 
		///BuildIntCommand("spUpdateProcedure",parameters,"Default",rowsAffected);
		///if(rowsAffected==0)
		///		ShowErrorMessage("執行失敗");
		///	else 
		///		ShowErrorMessage("執行成功");
		/// </code>
		/// </example>
		/// <param name="StoredProcName">預存程序名稱</param>
		/// <param name="Parameters">預存程序參數陣列</param>
		/// <param name="Connection">Conncetion名稱</param>
		/// <param name="rowsAffected">影響的列數</param>
		public void RunProcedure(string StoredProcName,IDataParameter[] Parameters,string Connection,out int rowsAffected)
		{
			Open(Connection);
			SqlCommand command =  BuildIntCommand(StoredProcName,Parameters);
			command.ExecuteNonQuery();
			rowsAffected = (int)command.Parameters["ReturnValue"].Value;
			CloseDB();
		}

		/// <summary>
		/// 執行預儲程序並傳回DataReaader物件
		/// </summary>
		/// <example>
		/// 以下為使用RunProcedure的範例程式：
		/// <code>
		/// // Create parameter array
		///	SqlParameter[] parameters =
		///	{
		///		new SqlParameter( "@LOGIN_ID", Sqldbkind.NVarChar, 50 ),	// 0
		///		new SqlParameter( "@DOMAIN", Sqldbkind.NVarChar, 50 ),	// 1
		///	};
		///	
		///	// Set parameter values and directions
		///	parameters[ 0 ].Value = LOGINID;
		///	parameters[ 1 ].Value = DOMAIN;
		///
		///	SqlDataReader cmd =RunProcedure("spQueryProcedure",parameters,"Default");
		/// </code>
		/// </example>
		/// <param name="StoredProcName">預存程序名稱</param>
		/// <param name="Parameters">預存程序參數陣列</param>
		/// <param name="Connection">Connection名稱</param>
		/// <returns></returns>
		 public  SqlDataReader RunProcedure(string StoredProcName,IDataParameter[] Parameters,string Connection)
		{
			Open(Connection);
			SqlCommand command = BuildIntCommand(StoredProcName,Parameters);
			command.CommandType=CommandType.StoredProcedure;
			SqlDataReader sdr=  command.ExecuteReader(CommandBehavior.CloseConnection);
			return sdr;
		}

		/// <summary>
		/// 執行預儲程序並傳回DataTable物件
		/// </summary>
		/// <example>
		/// 以下為使用RunProcedure的範例程式：
		/// <code>
		/// // Create parameter array
		///	SqlParameter[] parameters =
		///	{
		///		new SqlParameter( "@LOGIN_ID", Sqldbkind.NVarChar, 50 ),	// 0
		///		new SqlParameter( "@DOMAIN", Sqldbkind.NVarChar, 50 ),	// 1
		///	};
		///	
		///	// Set parameter values and directions
		///	parameters[ 0 ].Value = LOGINID;
		///	parameters[ 1 ].Value = DOMAIN;
		///
		///	DataTable cmd =RunProcedure("spQueryProcedure",parameters,"Default");
		/// </code>
		/// </example>
		/// <param name="StoredProcName">預存程序名稱</param>
		/// <param name="Parameters">預存程序參數陣列</param>
		/// <param name="TableName">DataTable名稱</param>
		/// <param name="Connection">Connection名稱</param>
		/// <returns></returns>
		public  DataTable RunProcedure(string StoredProcName,IDataParameter[] Parameters,string TableName,string Connection)
		{
				Open(Connection);
				DataTable dt=new DataTable(TableName);
				SqlDataAdapter SqlDa=new SqlDataAdapter();
				SqlDa.SelectCommand=BuildQueryCommand(StoredProcName,Parameters);
				SqlDa.Fill(dt);
				CloseDB();
				return dt;						
		}

		/// <summary>
		/// 執行預儲程序並將結果加入現有的Dataset中
		/// </summary>
		/// <example>
		/// 以下為使用RunProcedure的範例程式：
		/// <code>
		/// // Create parameter array
		///	SqlParameter[] parameters =
		///	{
		///		new SqlParameter( "@LOGIN_ID", Sqldbkind.NVarChar, 50 ),	// 0
		///		new SqlParameter( "@DOMAIN", Sqldbkind.NVarChar, 50 ),	// 1
		///	};
		///	
		///	// Set parameter values and directions
		///	parameters[ 0 ].Value = LOGINID;
		///	parameters[ 1 ].Value = DOMAIN;
		///
		///	DataSet Ds = new DataSet();
		///	RunProcedure("spQueryProcedure",parameters,Ds,"Table1","Default");
		/// </code>
		/// </example>
		/// <param name="StoredProcName">預存程序名稱</param>
		/// <param name="Parameters">預存程序參數陣列</param>
		/// <param name="Ds">DataSet物件</param>
		/// <param name="TableName">DataTable名稱</param>
		/// <param name="Connection">Connection名稱</param>
		public  void RunProcedure(string StoredProcName,IDataParameter[] Parameters,ref DataSet Ds,string TableName,string Connection)
		{
			Open(Connection);
			SqlDataAdapter Da = new SqlDataAdapter();
			Da.SelectCommand = BuildQueryCommand(StoredProcName,Parameters);
			Da.Fill(Ds,TableName);
			CloseDB();
		}

		/// <summary>
		/// 執行查詢，並傳回查詢所傳回的結果集第一個資料列的第一個資料行。會忽略其他的資料行或資料列。 
		/// </summary>
		/// <example>
		///	一般的 RunSQL 查詢可以如下列的範例格式化： 
		/// <code>
		/// string strSQL = "SELECT COUNT(*) FROM dbo.region";
		/// Int32 count = 0 ; //取得資料筆數
		/// RunSQL(strSQL,"Default",count);
		/// </code>
		/// </example>
		/// <param name="strSQL">SQL 語法</param>
		/// <param name="Connection">Connection名稱</param>
		/// <param name="Returnobj">回傳結果物件</param>
		public  void RunSQL(string strSQL,string Connection,out object Returnobj )
		{
			Open(Connection);
			SqlCommand command = new SqlCommand(strSQL,DbCon);
			try
			{
				Returnobj=command.ExecuteScalar();
			}
			catch(SqlException ex){(new WriteLog()).LogExp(ex);Returnobj=null;}
			catch(Exception ex){(new WriteLog()).LogExp(ex);Returnobj=null;}
			CloseDB();				
		}


		/// <summary>
		/// 執行SQL Command並傳回DataTable
		/// </summary>
		///<example>
		///	一般的 RunSQL 的範例：
		///<code>
		///		string strSQL = "SELECT * FROM dbo.region";
		///		DataTable dt = RunSQL(strSQL,"region");
		///</code>
		///</example>
		/// <param name="strSQL">SQL語法</param>
		/// <param name="TableName">DataTable名稱</param>
		/// <returns></returns>
		public DataTable RunSQL(string strSQL,string TableName)
		{
			Open("");
			DataTable dt = new DataTable(TableName);
			SqlDataAdapter Da = new SqlDataAdapter(strSQL,DbCon);
			Da.Fill(dt);
			CloseDB();
			return dt;				
		}

		/// <summary>
		/// 執行SQL Command並傳回DataTable
		/// </summary>
		///<example>
		///	一般的 RunSQL 的範例：
		///<code>
		///		string strSQL = "SELECT * FROM dbo.region";
		///		DataTable dt = RunSQL(strSQL,"region","Default");
		///</code>
		///</example>
		/// <param name="strSQL">SQL語法</param>
		/// <param name="TableName">DataTable名稱</param>
		/// <param name="Connection">Connection名稱</param>
		/// <returns></returns>
		public  DataTable RunSQL(string strSQL,string TableName,string Connection)
		{
			Open(Connection);
			DataTable dt = new DataTable(TableName);
			SqlDataAdapter Da = new SqlDataAdapter(strSQL,DbCon);
			Da.Fill(dt);
			CloseDB();
			return dt;		
		}

		/// <summary>
		/// 執行SQL Command並將DataTable加入到現有的DataSet
		/// </summary>
		///<example>
		///	一般的 RunSQL 的範例：
		///<code>
		///		string strSQL = "SELECT * FROM dbo.region";
		///		DataSet Ds= DataSet();
		///		RunSQL(strSQL,Ds,"region","Default");
		///</code>
		///</example>
		/// <param name="strSQL">SQL語法</param>
		/// <param name="Ds">DataSet物件</param>
		/// <param name="TableName">DataTable名稱</param>
		/// <param name="Connection">Connection名稱</param>
		public  void RunSQL(string strSQL,ref DataSet Ds,string TableName,string Connection)
		{
			Open(Connection);
			SqlDataAdapter Da = new SqlDataAdapter(strSQL,DbCon);
			Da.Fill(Ds,TableName);
			CloseDB();
		}

		/// <summary>
		/// 執行SQL Command並傳回影響的列數
		/// </summary>
		///<example>
		///	一般的 RunSQL 的範例：
		///<code>
		///		string strSQL = "Delete FROM dbo.region where regionid=1";
		///		int RowsAffected=0;
		///		RunSQL(strSQL,RowsAffected,"Default");
		///</code>
		///</example>
		/// <param name="strSQL">T-SQL語法</param>
		/// <param name="RowsAffected">受影響的列數</param>
		/// <param name="Connection">連線字串,預設為""</param>
		public void RunSQL(string strSQL, out int RowsAffected,string Connection)
		{
			try
			{
				Open(Connection);
				SqlCommand command = new SqlCommand(strSQL,DbCon);
				RowsAffected = command.ExecuteNonQuery();
				CloseDB();
				if(RowsAffected==0) RowsAffected=1;
			}
			catch(SqlException ex){(new WriteLog()).LogExp(ex);RowsAffected=0;}
			catch(Exception ex){(new WriteLog()).LogExp(ex);RowsAffected=0;}	
		}


		/// <summary>
		/// 執行SQL Command並傳回成功或失敗
		/// </summary>
		///<example>
		///	一般的 RunSQL 的範例：
		///<code>
		///		string strSQL = "Delete FROM dbo.region where regionid=1";
		///		
		///		if(RunSQL(strSQL))
		///			ShowErrorMessage("執行成功");
		///		else 
		///			ShowErrorMessage("執行失敗");
		///</code>
		///</example>
		/// <param name="strSQL">The STR SQL.</param>
		/// <returns></returns>
		public bool RunSQL(string strSQL)
		{
			int i = 0 ; 
			RunSQL(strSQL,out i,"");
			return (i>0);
		}

		/// <summary>
		/// 執行SQL Command並傳回成功或失敗
		/// </summary>
		///<example>
		///	一般的 RunSQL 的範例：
		///<code>
		///		string strSQL = "Delete FROM dbo.region where regionid=1";
		///		
		///		if(RunSQL(strSQL,"Default"))
		///			ShowErrorMessage("執行成功");
		///		else 
		///			ShowErrorMessage("執行失敗");
		///</code>
		///</example>
		/// <param name="strSQL">The STR SQL.</param>
		/// <param name="strConnectionKey">The STR connection key.</param>
		/// <returns></returns>
		public bool ExecuteSQL(string strSQL, string strConnectionKey)
		{
			int i = 0 ; 
			RunSQL(strSQL,out i,strConnectionKey);
			return (i>0);
		}

		/// <summary>
		/// Generates the condition.
		/// </summary>
		/// <example>
		/// GenerateCondition範例：
		///	<code>
		///		DataTable dt = RunSQL("select * from dbo.region","region");
		///		strSQL = "select * from dbo.employee where " + 
		///		GenerateCondition("regionid",FilterCollection.FilterType.Equal,"en",true); 
		///	</code>
		/// </example>
		/// <param name="FieldName">欄位名稱</param>
		/// <param name="Filter">篩選方式</param>
		/// <param name="KeyWord">值</param>
		/// <param name="AddQuote">值是否使用單引號</param>
		/// <returns></returns>
		public string GenerateCondition(string FieldName, FilterCollection.FilterType Filter, string KeyWord, bool AddQuote)
		{
			string FileType = "=";
			switch(Filter.ToString())
			{
				case "LessEqual": 
					FileType="<=";
					break; 
				case "EqualThan": 
					FileType=">=";
					break; 
				case "NotEqual": 
					FileType="<>";
					break; 
				case "Less": 
					FileType="<";
					break; 
				case "Than": 
					FileType=">";
					break; 
				case "Like": 
					FileType="like";
					break; 
			}

			if(!AddQuote)
				return string.Format("{0} {1} {2}", FieldName, FileType, KeyWord);
			else
			{
				if(FileType=="like")
					return string.Format("{0} {1} '%{2}%'", FieldName, FileType, KeyWord);
				else
					return string.Format("{0} {1} '{2}'", FieldName, FileType, KeyWord);
			}
			
		}

		/// <summary>
		/// 執行 Transact-SQL 交易
		/// </summary>
		/// <example>
		/// Transaction範例：
		///	<code>
		///		if(Transaction("insert into region('1','en')"
		///							,"delete from region where regionid=2"))
		///			ShowErrorMessage("執行成功");
		///		else 
		///			ShowErrorMessage("執行失敗");
		///	</code>
		/// </example>
		/// <param name="strSQLS">The STR SQLS.</param>
		/// <returns></returns>
		public bool Transaction(params string[] strSQLS)
		{
			Open("Default");
			SqlTransaction transaction;
			transaction = DbCon.BeginTransaction();
			try 
			{
				foreach(string strSQL in strSQLS)
				{
					if(strSQL!="" && strSQL!=null)
					{
						new SqlCommand(strSQL, DbCon, transaction)
							.ExecuteNonQuery();
					}
				}
				transaction.Commit();
				return true;
			} 
			catch (SqlException sqlError) 
			{
				transaction.Rollback();
				return false;
			}
			catch(Exception ex)
			{
				transaction.Rollback();
				return false;
			}
			finally
			{
				DbCon.Close();
			}
		}

		/// <summary>
		/// Runs the SQL transaction.
		/// </summary>
		/// <param name="ConnectionKey">The connection key.</param>
		/// <param name="strSQLS">The STR SQLS.</param>
		/// <returns></returns>
		/// <example>
		/// Transaction範例：
		/// <code>
		/// string[] strSQLs = {"insert into region('1','en')","delete from region where regionid=2"};
		/// if(Transaction("Default",strSQLs))
		/// ShowErrorMessage("執行成功");
		/// else
		/// ShowErrorMessage("執行失敗");
		/// </code>
		/// </example>
		public bool Transaction(string ConnectionKey,string[] strSQLS)
		{
			Open(ConnectionKey);
			SqlTransaction transaction;
			transaction = DbCon.BeginTransaction();
			try 
			{
				foreach(string strSQL in strSQLS)
				{
					new SqlCommand(strSQL, DbCon, transaction)
						.ExecuteNonQuery();
				}
				transaction.Commit();
				return true;
			} 
			catch (SqlException sqlError) 
			{
				transaction.Rollback();
				return false;
			}
			catch(Exception ex)
			{
				transaction.Rollback();
				return false;
			}
			finally
			{
				DbCon.Close();
			}
		}

		/// <summary>
		/// Inserts the access log.
		/// </summary>
		/// <param name="strSQL">The STR SQL.</param>
		/// <returns></returns>
		private string InsertAccessLog(string strSQL)
		{
			return "true";
			//return(userInfo.CurrentPath!=null 
			//  && (userInfo.CurrentPath.ToLower()!="default.aspx" 
			//  || userInfo.CurrentPath.ToLower()!="banner.aspx" 
			//  || userInfo.CurrentPath.ToLower()!="middle.aspx" 
			//  || userInfo.CurrentPath.ToLower()!="treeview.aspx"
			//  || userInfo.CurrentPath.ToLower()!="basepage.cs"))?(string.Format(@"INSERT INTO [AccessLog]" + "\r\n" +
			//  @"           ([UserId]" + "\r\n" +
			//  @"           ,[SQLCmd]" + "\r\n" +
			//  @"           ,[AppPath]" + "\r\n" +
			//  @"           ,[LoginId]" + "\r\n" +
			//  @"           ,[Createdate])" + "\r\n" +
			//  @"     VALUES(" + "\r\n" +
			//  @"           '{0}'" + "\r\n" +
			//  @"           ,'{1}'" + "\r\n" +
			//  @"           ,'{2}'" + "\r\n" +
			//  @"           ,'{3}'" + "\r\n" +
			//  @"           ,getdate())",userInfo.UserID,strSQL.Replace("'","none"),userInfo.CurrentPath,userInfo.PageLoginId)):"";
		}
		#region IDisposable 成員

		public void Dispose()
		{
			if ( DbCon != null )
			{
				DbCon.Close();
				DbCon= null;
				DbCon.Dispose();
			}
		}

		#endregion
	}
