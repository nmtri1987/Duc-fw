using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biz.Core.Models;
namespace Biz.Core.Messages
{
    public class MessageTokenProvider
    {
        #region Allowed tokens

        private Dictionary<string, IEnumerable<string>> _allowedTokens;
        /// <summary>
        /// Get all available tokens by token groups
        /// </summary>
        protected Dictionary<string, IEnumerable<string>> AllowedTokens
        {
            get
            {
                if (_allowedTokens != null)
                    return _allowedTokens;

                _allowedTokens = new Dictionary<string, IEnumerable<string>>();

                //5 days reminder 
                _allowedTokens.Add(TokenGroupNames.FiveDaysReminder, new[]
                {
                    "%PDM.Employee(s)%",
                    "%PDM.Name%",
                });

                return _allowedTokens;
            }
        }

        #endregion
        #region Utilities
        /// <summary>
        /// Convert a collection to a HTML table
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="vendorId">Vendor identifier (used to limit products by vendor</param>
        /// <returns>HTML table of products</returns>
        protected virtual string ProductListToHtmlTable(int languageId, int vendorId)
        {
            string result;

           // var language = _languageService.GetLanguageById(languageId);

            var sb = new StringBuilder();
            sb.AppendLine("<table border=\"0\" style=\"width:100%;\">");

            //#region Products
            //sb.AppendLine(string.Format("<tr style=\"background-color:{0};text-align:center;\">", _templatesSettings.Color1));
            //sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Name", languageId)));
            //sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Price", languageId)));
            //sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Quantity", languageId)));
            //sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Total", languageId)));
            //sb.AppendLine("</tr>");

            //var table = order.OrderItems.ToList();
            //for (int i = 0; i <= table.Count - 1; i++)
            //{
            //    var orderItem = table[i];
            //    var product = orderItem.Product;
            //    if (product == null)
            //        continue;

            //    if (vendorId > 0 && product.VendorId != vendorId)
            //        continue;

            //    sb.AppendLine(string.Format("<tr style=\"background-color: {0};text-align: center;\">", _templatesSettings.Color2));
            //    //product name
            //    string productName = product.GetLocalized(x => x.Name, languageId);

            //    sb.AppendLine("<td style=\"padding: 0.6em 0.4em;text-align: left;\">" + HttpUtility.HtmlEncode(productName));
            //    //add download link
            //    if (_downloadService.IsDownloadAllowed(orderItem))
            //    {
            //        //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            //        string downloadUrl = string.Format("{0}download/getdownload/{1}", GetStoreUrl(order.StoreId), orderItem.OrderItemGuid);
            //        string downloadLink = string.Format("<a class=\"link\" href=\"{0}\">{1}</a>", downloadUrl, _localizationService.GetResource("Messages.Order.Product(s).Download", languageId));
            //        sb.AppendLine("<br />");
            //        sb.AppendLine(downloadLink);
            //    }
            //    //add download link
            //    if (_downloadService.IsLicenseDownloadAllowed(orderItem))
            //    {
            //        //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            //        string licenseUrl = string.Format("{0}download/getlicense/{1}", GetStoreUrl(order.StoreId), orderItem.OrderItemGuid);
            //        string licenseLink = string.Format("<a class=\"link\" href=\"{0}\">{1}</a>", licenseUrl, _localizationService.GetResource("Messages.Order.Product(s).License", languageId));
            //        sb.AppendLine("<br />");
            //        sb.AppendLine(licenseLink);
            //    }
            //    //attributes
            //    if (!String.IsNullOrEmpty(orderItem.AttributeDescription))
            //    {
            //        sb.AppendLine("<br />");
            //        sb.AppendLine(orderItem.AttributeDescription);
            //    }
            //    //rental info
            //    if (orderItem.Product.IsRental)
            //    {
            //        var rentalStartDate = orderItem.RentalStartDateUtc.HasValue ? orderItem.Product.FormatRentalDate(orderItem.RentalStartDateUtc.Value) : string.Empty;
            //        var rentalEndDate = orderItem.RentalEndDateUtc.HasValue ? orderItem.Product.FormatRentalDate(orderItem.RentalEndDateUtc.Value) : string.Empty;
            //        var rentalInfo = string.Format(_localizationService.GetResource("Order.Rental.FormattedDate"),
            //            rentalStartDate, rentalEndDate);
            //        sb.AppendLine("<br />");
            //        sb.AppendLine(rentalInfo);
            //    }
            //    //sku
            //    if (_catalogSettings.ShowSkuOnProductDetailsPage)
            //    {
            //        var sku = product.FormatSku(orderItem.AttributesXml, _productAttributeParser);
            //        if (!String.IsNullOrEmpty(sku))
            //        {
            //            sb.AppendLine("<br />");
            //            sb.AppendLine(string.Format(_localizationService.GetResource("Messages.Order.Product(s).SKU", languageId), HttpUtility.HtmlEncode(sku)));
            //        }
            //    }
            //    sb.AppendLine("</td>");

            //    string unitPriceStr;
            //    if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
            //    {
            //        //including tax
            //        var unitPriceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceInclTax, order.CurrencyRate);
            //        unitPriceStr = _priceFormatter.FormatPrice(unitPriceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
            //    }
            //    else
            //    {
            //        //excluding tax
            //        var unitPriceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceExclTax, order.CurrencyRate);
            //        unitPriceStr = _priceFormatter.FormatPrice(unitPriceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
            //    }
            //    sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: right;\">{0}</td>", unitPriceStr));

            //    sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: center;\">{0}</td>", orderItem.Quantity));

            //    string priceStr;
            //    if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
            //    {
            //        //including tax
            //        var priceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceInclTax, order.CurrencyRate);
            //        priceStr = _priceFormatter.FormatPrice(priceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
            //    }
            //    else
            //    {
            //        //excluding tax
            //        var priceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceExclTax, order.CurrencyRate);
            //        priceStr = _priceFormatter.FormatPrice(priceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
            //    }
            //    sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: right;\">{0}</td>", priceStr));

            //    sb.AppendLine("</tr>");
            //}
            //#endregion

            //if (vendorId == 0)
            //{
            //    //we render checkout attributes and totals only for store owners (hide for vendors)

            //    #region Checkout Attributes

            //    if (!String.IsNullOrEmpty(order.CheckoutAttributeDescription))
            //    {
            //        sb.AppendLine("<tr><td style=\"text-align:right;\" colspan=\"1\">&nbsp;</td><td colspan=\"3\" style=\"text-align:right\">");
            //        sb.AppendLine(order.CheckoutAttributeDescription);
            //        sb.AppendLine("</td></tr>");
            //    }

            //    #endregion

            //    #region Totals

            //    //subtotal
            //    string cusSubTotal;
            //    bool displaySubTotalDiscount = false;
            //    string cusSubTotalDiscount = string.Empty;
            //    if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax && !_taxSettings.ForceTaxExclusionFromOrderSubtotal)
            //    {
            //        //including tax

            //        //subtotal
            //        var orderSubtotalInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalInclTax, order.CurrencyRate);
            //        cusSubTotal = _priceFormatter.FormatPrice(orderSubtotalInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
            //        //discount (applied to order subtotal)
            //        var orderSubTotalDiscountInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountInclTax, order.CurrencyRate);
            //        if (orderSubTotalDiscountInclTaxInCustomerCurrency > decimal.Zero)
            //        {
            //            cusSubTotalDiscount = _priceFormatter.FormatPrice(-orderSubTotalDiscountInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
            //            displaySubTotalDiscount = true;
            //        }
            //    }
            //    else
            //    {
            //        //exсluding tax

            //        //subtotal
            //        var orderSubtotalExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalExclTax, order.CurrencyRate);
            //        cusSubTotal = _priceFormatter.FormatPrice(orderSubtotalExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
            //        //discount (applied to order subtotal)
            //        var orderSubTotalDiscountExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountExclTax, order.CurrencyRate);
            //        if (orderSubTotalDiscountExclTaxInCustomerCurrency > decimal.Zero)
            //        {
            //            cusSubTotalDiscount = _priceFormatter.FormatPrice(-orderSubTotalDiscountExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
            //            displaySubTotalDiscount = true;
            //        }
            //    }

            //    //shipping, payment method fee
            //    string cusShipTotal;
            //    string cusPaymentMethodAdditionalFee;
            //    var taxRates = new SortedDictionary<decimal, decimal>();
            //    string cusTaxTotal = string.Empty;
            //    string cusDiscount = string.Empty;
            //    string cusTotal;
            //    if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
            //    {
            //        //including tax

            //        //shipping
            //        var orderShippingInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderShippingInclTax, order.CurrencyRate);
            //        cusShipTotal = _priceFormatter.FormatShippingPrice(orderShippingInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
            //        //payment method additional fee
            //        var paymentMethodAdditionalFeeInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeInclTax, order.CurrencyRate);
            //        cusPaymentMethodAdditionalFee = _priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
            //    }
            //    else
            //    {
            //        //excluding tax

            //        //shipping
            //        var orderShippingExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderShippingExclTax, order.CurrencyRate);
            //        cusShipTotal = _priceFormatter.FormatShippingPrice(orderShippingExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
            //        //payment method additional fee
            //        var paymentMethodAdditionalFeeExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeExclTax, order.CurrencyRate);
            //        cusPaymentMethodAdditionalFee = _priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
            //    }

            //    //shipping
            //    bool displayShipping = order.ShippingStatus != ShippingStatus.ShippingNotRequired;

            //    //payment method fee
            //    bool displayPaymentMethodFee = order.PaymentMethodAdditionalFeeExclTax > decimal.Zero;

            //    //tax
            //    bool displayTax = true;
            //    bool displayTaxRates = true;
            //    if (_taxSettings.HideTaxInOrderSummary && order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
            //    {
            //        displayTax = false;
            //        displayTaxRates = false;
            //    }
            //    else
            //    {
            //        if (order.OrderTax == 0 && _taxSettings.HideZeroTax)
            //        {
            //            displayTax = false;
            //            displayTaxRates = false;
            //        }
            //        else
            //        {
            //            taxRates = new SortedDictionary<decimal, decimal>();
            //            foreach (var tr in order.TaxRatesDictionary)
            //                taxRates.Add(tr.Key, _currencyService.ConvertCurrency(tr.Value, order.CurrencyRate));

            //            displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Any();
            //            displayTax = !displayTaxRates;

            //            var orderTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTax, order.CurrencyRate);
            //            string taxStr = _priceFormatter.FormatPrice(orderTaxInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);
            //            cusTaxTotal = taxStr;
            //        }
            //    }

            //    //discount
            //    bool displayDiscount = false;
            //    if (order.OrderDiscount > decimal.Zero)
            //    {
            //        var orderDiscountInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderDiscount, order.CurrencyRate);
            //        cusDiscount = _priceFormatter.FormatPrice(-orderDiscountInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);
            //        displayDiscount = true;
            //    }

            //    //total
            //    var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
            //    cusTotal = _priceFormatter.FormatPrice(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);




            //    //subtotal
            //    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.SubTotal", languageId), cusSubTotal));

            //    //discount (applied to order subtotal)
            //    if (displaySubTotalDiscount)
            //    {
            //        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.SubTotalDiscount", languageId), cusSubTotalDiscount));
            //    }


            //    //shipping
            //    if (displayShipping)
            //    {
            //        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.Shipping", languageId), cusShipTotal));
            //    }

            //    //payment method fee
            //    if (displayPaymentMethodFee)
            //    {
            //        string paymentMethodFeeTitle = _localizationService.GetResource("Messages.Order.PaymentMethodAdditionalFee", languageId);
            //        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, paymentMethodFeeTitle, cusPaymentMethodAdditionalFee));
            //    }

            //    //tax
            //    if (displayTax)
            //    {
            //        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.Tax", languageId), cusTaxTotal));
            //    }
            //    if (displayTaxRates)
            //    {
            //        foreach (var item in taxRates)
            //        {
            //            string taxRate = String.Format(_localizationService.GetResource("Messages.Order.TaxRateLine"), _priceFormatter.FormatTaxRate(item.Key));
            //            string taxValue = _priceFormatter.FormatPrice(item.Value, true, order.CustomerCurrencyCode, false, language);
            //            sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, taxRate, taxValue));
            //        }
            //    }

            //    //discount
            //    if (displayDiscount)
            //    {
            //        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.TotalDiscount", languageId), cusDiscount));
            //    }

            //    //gift cards
            //    var gcuhC = order.GiftCardUsageHistory;
            //    foreach (var gcuh in gcuhC)
            //    {
            //        string giftCardText = String.Format(_localizationService.GetResource("Messages.Order.GiftCardInfo", languageId), HttpUtility.HtmlEncode(gcuh.GiftCard.GiftCardCouponCode));
            //        string giftCardAmount = _priceFormatter.FormatPrice(-(_currencyService.ConvertCurrency(gcuh.UsedValue, order.CurrencyRate)), true, order.CustomerCurrencyCode, false, language);
            //        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, giftCardText, giftCardAmount));
            //    }

            //    //reward points
            //    if (order.RedeemedRewardPointsEntry != null)
            //    {
            //        string rpTitle = string.Format(_localizationService.GetResource("Messages.Order.RewardPoints", languageId), -order.RedeemedRewardPointsEntry.Points);
            //        string rpAmount = _priceFormatter.FormatPrice(-(_currencyService.ConvertCurrency(order.RedeemedRewardPointsEntry.UsedAmount, order.CurrencyRate)), true, order.CustomerCurrencyCode, false, language);
            //        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, rpTitle, rpAmount));
            //    }

            //    //total
            //    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.OrderTotal", languageId), cusTotal));
            //    #endregion

            //}

            sb.AppendLine("</table>");
            result = sb.ToString();
            return result;
        }

        #endregion
        public virtual void Add5DaysNotWorking(IList<Token> tokens)
        {
            tokens.Add(new Token("PM.Employee(s)", ""));
            tokens.Add(new Token("PM.Name", ""));
            //tokens.Add(new Token("Order.Product(s)", ProductListToHtmlTable(order, languageId, vendorId), true));

            ////TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            //tokens.Add(new Token("Order.OrderURLForCustomer", string.Format("{0}orderdetails/{1}", GetStoreUrl(order.StoreId), order.Id), true));

            ////event notification
            //_eventPublisher.EntityTokensAdded(order, tokens);
        }
    }
}
