using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default4 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
    //if (!Page.IsPostBack)
    //{
      DataTable DT = GetDataTable();

      //DT.Rows.Clear();
      GridView1.DataSource = DT;
      GridView1.DataBind();

      GridView2.DataSource = DT;
      GridView2.DataBind();
    //}
	}
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection oldCell = e.Row.Cells;
			oldCell.Clear();
			//產生多重表列的第一列

			Color col = Color.FromArgb(System.Convert.ToInt32("95B3DE", 16));

			GridViewRow headRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
			//第一欄
			TableCell head2Cell = new TableCell();
			head2Cell.Text = "編輯";
			head2Cell.RowSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第二欄
			head2Cell = new TableCell();
			head2Cell.Text = "級距";
			head2Cell.RowSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第三欄
			head2Cell = new TableCell();
			head2Cell.Text = "帳列手續費";
			head2Cell.ColumnSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第四欄
			head2Cell = new TableCell();
			head2Cell.Text = "折讓金額";
			head2Cell.ColumnSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第五欄
			head2Cell = new TableCell();
			head2Cell.Text = "實收金額";
			head2Cell.ColumnSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			//將自製的資料列新增上去
			GridView1.Controls[0].Controls.Add(headRow);
			//產生多重表列的第二列
			headRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

			#region 帳列手續費
			head2Cell = new TableCell();
			head2Cell.Text = "人工單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			head2Cell = new TableCell();
			head2Cell.Text = "電子單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			#endregion
			#region 折讓金額
			head2Cell = new TableCell();
			head2Cell.Text = "人工單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			head2Cell = new TableCell();
			head2Cell.Text = "電子單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			#endregion
			#region 實收金額
			head2Cell = new TableCell();
			head2Cell.Text = "人工單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			head2Cell = new TableCell();
			head2Cell.Text = "電子單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			#endregion
			GridView1.Controls[0].Controls.Add(headRow);
		}
	}
	protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection oldCell = e.Row.Cells;
			oldCell.Clear();
			//產生多重表列的第一列

			Color col = Color.FromArgb(System.Convert.ToInt32("95B3DE", 16));

			GridViewRow headRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
			//第一欄
			TableCell head2Cell = new TableCell();
			head2Cell.Text = "編輯";
			head2Cell.RowSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第二欄
			head2Cell = new TableCell();
			head2Cell.Text = "級距";
			head2Cell.RowSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第三欄
			head2Cell = new TableCell();
			head2Cell.Text = "帳列手續費";
			head2Cell.ColumnSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第四欄
			head2Cell = new TableCell();
			head2Cell.Text = "折讓金額";
			head2Cell.ColumnSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);

			//第五欄
			head2Cell = new TableCell();
			head2Cell.Text = "實收金額";
			head2Cell.ColumnSpan = 2;
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			//將自製的資料列新增上去
			GridView2.Controls[0].Controls.Add(headRow);
			//產生多重表列的第二列
			headRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

			#region 帳列手續費
			head2Cell = new TableCell();
			head2Cell.Text = "人工單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			head2Cell = new TableCell();
			head2Cell.Text = "電子單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			#endregion
			#region 折讓金額
			head2Cell = new TableCell();
			head2Cell.Text = "人工單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			head2Cell = new TableCell();
			head2Cell.Text = "電子單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			#endregion
			#region 實收金額
			head2Cell = new TableCell();
			head2Cell.Text = "人工單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			head2Cell = new TableCell();
			head2Cell.Text = "電子單";
			head2Cell.HorizontalAlign = HorizontalAlign.Center;
			head2Cell.BackColor = col;
			headRow.Cells.Add(head2Cell);
			#endregion
			GridView2.Controls[0].Controls.Add(headRow);
		}
	}

  protected void Button_Normal1_Click(object sender, EventArgs e)
  {
    PageMaker1.RePaging(1);
  }

  private DataTable GetDataTable()
  {
    DataTable DT = new DataTable();
    DT.Columns.Add(new DataColumn("級距", typeof(System.Int32)));
    DT.Columns.Add(new DataColumn("帳列人工單", typeof(System.Int32)));
    DT.Columns.Add(new DataColumn("帳列電子單", typeof(System.Int32)));
    DT.Columns.Add(new DataColumn("折讓人工單", typeof(System.Int32)));
    DT.Columns.Add(new DataColumn("折讓電子單", typeof(System.Int32)));
    DT.Columns.Add(new DataColumn("實收人工單", typeof(System.Int32)));
    DT.Columns.Add(new DataColumn("實收電子單", typeof(System.Int32)));

    for (int i = 1; i <= 500; i++)
    {
      DataRow Dr1 = DT.NewRow();
      Dr1["級距"] = 1 + (i - 1) * 4;
      Dr1["帳列人工單"] = i*10;
      Dr1["帳列電子單"] = (i + 1) * 10;
      Dr1["折讓人工單"] = (i + 2) * 10;
      Dr1["折讓電子單"] = (i + 3) * 10;
      Dr1["實收人工單"] = (i + 4) * 10;
      Dr1["實收電子單"] = (i + 5) * 10;
      DT.Rows.Add(Dr1);

      DataRow Dr2 = DT.NewRow();
      Dr2["級距"] = 2 + (i - 1) * 4;
      Dr2["帳列人工單"] = (i + 2) * 10;
      Dr2["帳列電子單"] = (i + 3) * 10;
      Dr2["折讓人工單"] = (i + 4) * 10;
      Dr2["折讓電子單"] = (i + 5) * 10;
      Dr2["實收人工單"] = (i + 6) * 10;
      Dr2["實收電子單"] = (i + 7) * 10;
      DT.Rows.Add(Dr2);

      DataRow Dr3 = DT.NewRow();
      Dr3["級距"] = 3 + (i - 1) * 4;
      Dr3["帳列人工單"] = (i + 3) * 10;
      Dr3["帳列電子單"] = (i + 4) * 10;
      Dr3["折讓人工單"] = (i + 5) * 10;
      Dr3["折讓電子單"] = (i + 6) * 10;
      Dr3["實收人工單"] = (i + 7) * 10;
      Dr3["實收電子單"] = (i + 8) * 10;
      DT.Rows.Add(Dr3);

      DataRow Dr4 = DT.NewRow();
      Dr4["級距"] = 4 + (i - 1) * 4;
      Dr4["帳列人工單"] = (i + 4) * 10;
      Dr4["帳列電子單"] = (i + 5) * 10;
      Dr4["折讓人工單"] = (i + 6) * 10;
      Dr4["折讓電子單"] = (i + 7) * 10;
      Dr4["實收人工單"] = (i + 8) * 10;
      Dr4["實收電子單"] = (i + 9) * 10;
      DT.Rows.Add(Dr4);
    }

    return DT;
  }
}
