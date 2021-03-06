#region dashCommerce License
/*
dashCommerce® is Copyright © 2008-2012 Mettle Systems LLC. All Rights Reserved.


dashCommerce, and the dashCommerce logo are registered trademarks of Mettle Systems LLC. Mettle Systems LLC logos and trademarks may not be used without prior written consent.

dashCommerce is licensed under the following license. If you do not accept the terms, please discontinue the use of dashCommerce and uninstall dashCommerce. 

Your license to the dashCommerce source and/or binaries is governed by the Reciprocal Public License 1.5 (RPL1.5) license as described here: 

http://www.opensource.org/licenses/rpl1.5.txt 

If you do not wish to release the source of software you build using dashCommerce, you may purchase a site license, which will allow you to deploy dashCommerce for use in 1 web store defined as using 1 URL. You may purchase a site license here: 

http://www.dashcommerce.org/license.html 
*/
#endregion
using System;
using System.Globalization;
using System.Web.UI;
using MettleSystems.dashCommerce.Core;
using MettleSystems.dashCommerce.Core.Globalization;
using MettleSystems.dashCommerce.Localization;
using MettleSystems.dashCommerce.Store;

namespace MettleSystems.dashCommerce.Web.admin.controls.sitesettings {
  public partial class globalization : SiteSettingsControl {

    #region Page Events

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    protected override void Page_Load(object sender, EventArgs e) {
      try {
        base.Page_Load(sender, e);
        if (view.Equals("gl")) {
          SetGlobalizationSettingsProperties();
          if (!Page.IsPostBack) {
            SetLanguageSelections();
            SetCurrencySelections();
            ddlLanguage.SelectedValue = SiteSettings.Language;
            ddlCurrency.SelectedValue = SiteSettings.CurrencyCode;
          }
        }
      }
      catch (Exception ex) {
        Logger.Error(typeof(globalization).Name + ".Page_Load", ex);
        base.MasterPage.MessageCenter.DisplayCriticalMessage(ex.Message);
      }
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e) {
      try {
        SiteSettings.Language = ddlLanguage.SelectedValue;
        SiteSettings.CurrencyCode = ddlCurrency.SelectedValue;
        base.Save(SiteSettings);
      }
      catch (Exception ex) {
        Logger.Error(typeof(browsinglog).Name + ".btnSave_Click", ex);
        base.MasterPage.MessageCenter.DisplayCriticalMessage(ex.Message);
      }
    }

    #endregion

    #region Methods

    #region Private

    /// <summary>
    /// Sets the language selections.
    /// </summary>
    private void SetLanguageSelections() {
      CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
      Array.Sort(cultures, new CultureComparer()) ;
      ddlLanguage.DataSource = cultures;
      ddlLanguage.DataValueField = "Name";
      ddlLanguage.DataTextField = "DisplayName";
      ddlLanguage.DataBind();
    }

    /// <summary>
    /// Sets the currency selections.
    /// </summary>
    private void SetCurrencySelections() {
      CurrencyCollection currencyCollection = new CurrencyController().FetchAll().OrderByAsc("Description");
      ddlCurrency.DataSource = currencyCollection;
      ddlCurrency.DataValueField = "Code";
      ddlCurrency.DataTextField = "Description";
      ddlCurrency.DataBind();
    }

    /// <summary>
    /// Sets the globalization settings properties.
    /// </summary>
    private void SetGlobalizationSettingsProperties() {
      this.Page.Title = LocalizationUtility.GetText("titleSiteSettingsGlobalization");
    }

    #endregion

    #endregion

  }
}