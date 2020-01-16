using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;

public class PublicFunc
	{
		public static string GetIniInfo(string iniFile,string iniSection)
		{
			if(! File.Exists(iniFile))
			{
				return "error";
			}
				StreamReader iniRead = new StreamReader(iniFile, System.Text.Encoding.GetEncoding(950));
			try
			{
				string iniStr = iniRead.ReadToEnd();
				int i, cLine, BegLineNum, EndLineNum;
				BegLineNum = 0;
				EndLineNum = 0;
				bool noSec = false;
				string getValue = "";
				string[] cLst = iniStr.Split(Strings.Chr(13));
				cLine = cLst.Length;
				for (i = 0; i < cLine; i++)
				{
					if (cLst[i] == "[" + iniSection + "]" || cLst[i] == "\n[" + iniSection + "]")
					{
						BegLineNum = i + 1;
						continue;
					}
					if (BegLineNum != 0)
					{
						if ((cLst[i].Substring(0, 1) == "[" || cLst[i].Substring(0, 2) == "\n[") && cLst[i].Substring(cLst[i].Length - 1) == "]")
						{
							EndLineNum = i - 1;
							break;
						}
					}
				}
				if (BegLineNum != 0 && EndLineNum == 0)
					EndLineNum = cLst.Length - 1;

				if (BegLineNum != 0)
				{
					for (i = BegLineNum; i < EndLineNum + 1; i++)
					{
						getValue += cLst[i] + Strings.Chr(13);
					}
				}
				return getValue.Substring(1, getValue.Length - 2);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				iniRead.Close();
			}
		}
  }