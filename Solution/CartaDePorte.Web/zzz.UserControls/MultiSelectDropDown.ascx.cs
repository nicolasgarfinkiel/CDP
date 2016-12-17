using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;


namespace CartaDePorte.Web.UserControls
{
    public partial class MultiSelectDropDown : System.Web.UI.UserControl
    {
        private string _SelectedText;

        #region  Public properties

        /// <summary>
        /// Get and Set the width of the Dropdown
        /// </summary>
        public double ListWidth
        {
            get { return Panel2.Width.Value; }
            set { Panel2.Width = (Unit)value; }
        }

        /// <summary>
        /// Gets arraylist of  selected values 
        /// </summary>
        public ArrayList SelectedValues
        {
            get
            {
                ArrayList selectedValues = new ArrayList();
                foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
                {
                    if (li.Selected)
                    { selectedValues.Add(li.Value); }
                }
                return selectedValues;
            }
        }

        /// <summary>
        /// Gets arraylist of  selected texts 
        /// </summary>
        public ArrayList SelectedTexts
        {
            get
            {
                ArrayList selectedTexts = new ArrayList();
                foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
                {
                    if (li.Selected)
                    { selectedTexts.Add(li.Text); }
                }
                return selectedTexts;
            }
        }

        /// <summary>
        /// Gets the selected text , the items are separated by comma
        /// </summary>
        public string SelectedText
        {
            get
            {
                string selText = string.Empty;
                foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
                {
                    if (li.Selected)
                    { selText += li.Text + ","; }
                }
                if (selText.Length > 0)
                    selText = selText.Length > 0 ? selText.Substring(0, selText.Length - 1) : selText;
                return selText;
            }
            set
            {
                _SelectedText = value;
                DDLabel.Text = _SelectedText;
                DDLabel.ToolTip = _SelectedText;
            }
        }

        /// <summary>
        /// Gets the selected items of the list
        /// </summary>
        public ArrayList SelectedItems
        {
            get
            {
                ArrayList selectedItems = new ArrayList();
                foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
                {
                    if (li.Selected)
                    { selectedItems.Add(li); }
                }
                return selectedItems;
            }
            set
            {
                ArrayList selectedItems = value;
                string selText = string.Empty;

                // Deselect all the selected items
                foreach (System.Web.UI.WebControls.ListItem li in DDList.Items)
                { li.Selected = false; }

                // Select the items from the list
                foreach (System.Web.UI.WebControls.ListItem selItem in selectedItems)
                {
                    System.Web.UI.WebControls.ListItem li = DDList.Items.FindByText(selItem.Text);
                    if (li != null)
                    { li.Selected = true; selText += li.Text + ","; }
                }
                if (selText.Length > 0)
                    selText = selText.Length > 0 ? selText.Substring(0, selText.Length - 1) : selText;

                SelectedText = selText;
            }
        }
        /// <summary>
        /// Gets the list
        /// </summary>
        public System.Web.UI.WebControls.ListBox List
        {
            get { return DDList; }
            set { DDList = List; }
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Remove all items in the list 
        /// </summary>
        public void Clear()
        {
            DDList.Items.Clear();
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Set the attributes of the controls
        /// </summary>
        public void PageInit()
        {
            string ctlID = this.UniqueID.Replace('$','_') + "_";
            DDList.Attributes.Add("onchange", "SelectedIndexChanged('" + ctlID + "');");
            DDList.Attributes.Add("onmouseout", "CloseListBox('" + ctlID + "');");
            DDLabel.Attributes.Add("onclick", "OpenListBox('" + ctlID + "');");
            colDDImage.Attributes.Add("onclick", "OpenListBox('" + ctlID + "');");
        }


        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PageInit();

                DDList.Height = 0;
                if (DDList.Items.Count > 0)
                    DDLabel.Text = DDList.Items[0].Text;
                else
                    DDLabel.Text = string.Empty;
            }
            else
            {	// set the selected text and tooltip
                DDLabel.Text = SelectedText;
                DDLabel.ToolTip = SelectedText;
            }
        }

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }
}