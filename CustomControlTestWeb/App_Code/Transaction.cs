using System;
using System.Data; 
using System.Data.SqlClient; 

	/// <summary>
	///		SQL Transaction�\�स�� 
	/// </summary>
	public class Transaction: IDisposable
	{
		private SqlConnection Conn;
		private SqlCommand Cmd;
		private SqlTransaction Trans;

		/// <summary>
		/// <see cref="Transaction"/> class �غc�l
		/// </summary>
		/// <param name="Connection">The connection.</param>
		public Transaction(string Connection)
		{
			Conn =new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings[Connection]);
		}
		
		/// <summary>
		/// <see cref="Transaction"/> class �غc�l
		/// </summary>
		public Transaction()
		{
			Conn =new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Default"]);
		}


		/// <summary>
		/// �[�J�����檺T-SQL�y�k��transaction
		/// </summary>
		/// <param name="strSQL">T-SQL �y�k</param>
		/// <param name="Commit">�O�_����Transaction</param>
		/// <returns></returns>
		/// <example>
		/// AddTransaction�ϥνd�ҡG
		/// <code>
		/// string strSQL1 = "Insert into region values(2,'en')";
		/// string strSQL2 = "Delete region where regionid=1";
		/// AddTransaction(strSQL1,false);  //�[�J�Ĥ@�q�����檺SQL�y�k��Transaction����
		/// if(AddTransaction(strSQL2,true)) //�[�J�̫�@�q�����檺SQL�y�k�A�ó]�wCommit��True
		/// {	//���榨�\
		/// }
		/// else
		/// {	//���楢��
		/// }
		/// </code>
		/// </example>
		public bool AddTransaction(string strSQL,bool Commit)
		{
			if(Conn.State !=ConnectionState.Open)
			{
				Conn.Close();
				Conn.Open();
				Trans = Conn.BeginTransaction();
			}
			try
			{
				new SqlCommand(strSQL, Conn, Trans)
					.ExecuteNonQuery();
				if(Commit)
				{
					Trans.Commit();
					Conn.Close();
				}
				return true; 
			}
			catch(Exception Ex)
			{
				Trans.Rollback();
				return false;
			}
		}

		#region IDisposable ����

		/// <summary>
		/// ����P���� (Free)�B���� (Release) �έ��] Unmanaged �귽�����p�����ε{���w�q���u�@�C
		/// </summary>
		public void Dispose()
		{
			if(Conn!=null)
			{
				Conn.Close();
				Conn=null;
			}
		}

		#endregion
	}
