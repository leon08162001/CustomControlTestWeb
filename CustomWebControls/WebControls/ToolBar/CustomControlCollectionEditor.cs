using System;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Web.UI.Design.WebControls;

namespace APTemplate
{
    class CustomControlCollectionEditor : CollectionEditor
    {
        #region Public Properties & Methods

        /// <summary>
        ///建構函式。
        /// </summary>
        public CustomControlCollectionEditor(Type Type)
            : base(Type)
        {

        }

        #endregion

        #region Protected Properties & Methods

        /// <summary>
        /// 覆寫基底CanSelectMultipleInstances以支援集合編輯器可增加一個以上的item物件型別
        /// </summary>
        protected override bool CanSelectMultipleInstances()
        {
            return true;
        }

        /// <summary>
        /// 覆寫基底CreateNewItemTypes以支援集合編輯器可增加的item物件型別
        /// </summary>
        protected override Type[] CreateNewItemTypes()
        {
            Type[] ItemTypes = new Type[16];
            ItemTypes[0] = typeof(CalendarRange);
            ItemTypes[1] = typeof(DecimalRange);
            ItemTypes[2] = typeof(DropDownList_Date);
            ItemTypes[3] = typeof(DropDownList_Multiple);
            ItemTypes[4] = typeof(TextBox_Normal);
            ItemTypes[5] = typeof(Email);
            ItemTypes[6] = typeof(Identity);
            ItemTypes[7] = typeof(ListBoxToListBox);
            ItemTypes[8] = typeof(Number);
            ItemTypes[9] = typeof(Number_Decimal);
            ItemTypes[10] = typeof(NumberRange);
            ItemTypes[11] = typeof(PopupCalendar);
            ItemTypes[12] = typeof(TextBox_PopupWindow);
            ItemTypes[13] = typeof(Button_ConfirmYesNo);
            ItemTypes[14] = typeof(Button_Normal);
            ItemTypes[15] = typeof(Button_PopupWindow);
            return ItemTypes;
        }

        /// <summary>
        /// 覆寫基底CreateCollectionItemType程序
        /// </summary>
        protected override Type CreateCollectionItemType()
        {
            return base.CreateCollectionItemType();
        }

        #endregion

    }
}