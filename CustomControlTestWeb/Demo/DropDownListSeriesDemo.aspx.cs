using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo_DropDownListSeriesDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList_Multiple.ConnectionKey = "Default";
        DropDownList_Multiple.FirstSelectSQL = "select * from Customers";
        DropDownList_Multiple.FirstDataTextField = "CustomerName";
        DropDownList_Multiple.FirstDataValueField = "CustomerID";
        DropDownList_Multiple.SecondFilterField = "CustomerID";
        DropDownList_Multiple.SecondSelectSQL = "select * from Orders order by OrderDate desc";
        DropDownList_Multiple.SecondDataTextField = "OrderDate";
        DropDownList_Multiple.SecondDataValueField = "OrderID";
        DropDownList_Multiple.ThirdFilterField = "OrderID";
        DropDownList_Multiple.ThirdSelectSQL = "select * from OrderDetails as o inner join Products as p on o.ProductID=p.ProductID order by p.ProductID desc";
        DropDownList_Multiple.ThirdDataTextField = "ProductName";
        DropDownList_Multiple.ThirdDataValueField = "ProductID";
    }
}
