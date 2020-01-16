using System;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Web.UI.Design.WebControls;

namespace APTemplate
{
    class FileFilterItemCollectionEditor : CollectionEditor
    {
        #region Public Properties & Methods

        /// <summary>
        ///建構函式。
        /// </summary>
        public FileFilterItemCollectionEditor(Type Type)
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
            Type[] ItemTypes = new Type[1];
            ItemTypes[0] = typeof(FileFilterItem);
            return ItemTypes;
        }

        #endregion

    }
}