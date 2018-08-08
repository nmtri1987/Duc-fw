using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Biz.Core.ComponentModel;
using System.Linq;
using System.Web.Hosting;
using System.IO;
using System.Net;
using Biz.Core.Attribute;
using Biz.Core.Configuration;
using Biz.Core.Infrastructure;
using Biz.Core.Models;
using Biz.Core.Services;
using System.Data;
using Biz.Core.Converts;
namespace Biz.Core
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class CommonHelper
    {

        public static string GetNTId()
        {
            string sCurrentUserName = HttpContext.Current.User.Identity.Name.ToString();
            if (sCurrentUserName != null && sCurrentUserName.Trim().Length > 0)
            {
                string[] sSplittedName = sCurrentUserName.Split('\\');
                string sDomain = sSplittedName[0].ToString();
                return sSplittedName[1].ToString().Trim().ToUpper();
            }
            else
            {
                throw new Exception("Unable to get NT-Id. Please check the contact administrator.");
            }
        }
        public static DNHUsers CurrentUser
        {
            get
            {
                return Biz.Core.Security.CustomerAuthorize.CurrentUser;
            }

        }
        public static DNHSitemapAction CheckActionPermission(DNHSiteMap objSiteMap, string ActionName)
        {
            //get NodeID role 
            //acc Role permission
            DNHSitemapAction objItem = null;
            
            if (objSiteMap.Access != 0)
            {
                DNHSitemapActionCollection myCol = DNHSitemapActionManager.GetById(objSiteMap.Access, objSiteMap.CompanyID);
                bool isMatch = false;
                if (myCol.Count > 0)
                {

                    foreach (DNHSitemapAction myitem in myCol)
                    {
                        if (myitem.ActionName.ToLower() == ActionName.ToLower())
                        {
                            isMatch = true;
                            objItem = myitem;
                            break;
                        }
                    }
                }
                if (!isMatch)
                {
                    objItem = AddNewDefaultAction(objSiteMap, ActionName);
                }

            }
            else
            {
                objItem = AddNewDefaultAction(objSiteMap, ActionName);
                if (objItem!=null)
                {
                    //objSiteMap.Access = objItem.ID;
                    DNHRoleSitemap RoleSitemap = DNHRoleSitemapManager.GetbyID(objSiteMap.NodeID.ToString(), objSiteMap.CompanyID, objSiteMap.RoleName);
                    if (RoleSitemap.NodeID != Guid.Empty)
                    {
                        RoleSitemap.Access = objItem.ID;
                        RoleSitemap.CreateDate = SystemConfig.CurrentDate;
                        RoleSitemap = DNHRoleSitemapManager.Update(RoleSitemap);
                        if (RoleSitemap.NodeID != Guid.Empty)
                        {
                            DNHSiteMapManager.RemoveCache(objSiteMap);
                        }
                    }
                }
            }
            return objItem;

        }
        public static DNHSitemapAction AddNewDefaultAction(DNHSiteMap objSiteMap, string ActionName)
        {
            DNHSitemapAction myAction = new DNHSitemapAction()
            {
                CompanyID = objSiteMap.CompanyID,
                ID = objSiteMap.Access,
                RoleName = objSiteMap.RoleName,
                NodeID = objSiteMap.NodeID,
                ActionName = ActionName,
                Allow = true,
                Edit = false,
                CreatedUser = CurrentUser.UserName,
                CreatedDate = SystemConfig.CurrentDate,
                ScreenID = ""
            };
            return DNHSitemapActionManager.Add(myAction);
        }
        /// <summary>
        /// Ensures the subscriber email or throw.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new IFindException("Email is not valid.");
            }

            return output;
        }

        public static string EncyptURLString(string Text)
        {
            return HttpUtility.UrlEncode(HttpUtility.HtmlEncode(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes((Text)))));
        }

        public static string DecyptURLString(string Text)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.HtmlDecode(HttpUtility.UrlDecode(Text))));
        }
        public static string EncodeString(string Text)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Text));
        }

        public static AppConfig myConfig
        {
            get
            {
                return EngineContext.Current.ContainerManager.Resolve<AppConfig>("", EngineContext.Current.ContainerManager.Scope());
            }
        }

        public static HeaderItemCollection UserSearchColumn<T>(DNHUsers objUser, string Page)
        {
            HeaderItemCollection ColumnName = new HeaderItemCollection();
            if (objUser == null) return ColumnName;
            //check the UserFile has exist or not
            string FileName = IOHelper.GetDirectory(myConfig.UserDataFolder + objUser.CompanyID + "\\Header\\" + objUser.UserName + "\\" + Page) + "\\headerFilter.bin";
            if (IOHelper.hasFile(FileName))
            {
                //get form the file
                ColumnName = IOHelper.ReadFromBinaryFile<HeaderItemCollection>(FileName);
            }
            else
            {
                //var nopConfig = 

                ColumnName = CommonHelper.JsonHeaderSearchList<T>(objUser);
                IOHelper.WriteToBinaryFile<HeaderItemCollection>(FileName, ColumnName);
            }



            return ColumnName;
        }
        public static HeaderItemCollection JsonHeaderSearchList<T>(DNHUsers ObjUser)
        {
            HeaderItemCollection myCols = new HeaderItemCollection();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            PropertyInfo propertyInfo;
            ColumnAttribute myCustom;
            HeaderItem item;
            bool isSearch = true;
            for (int i = 0; i < pia.Length - 5; i++)
            {
                item = new HeaderItem();
                propertyInfo = pia[i];
                if (propertyInfo == null) continue;
                item.name = propertyInfo.Name;
                item.value = L(propertyInfo.Name);
                item.type = JsonTypes(propertyInfo);
                isSearch = true;
                int CustomLength = propertyInfo.CustomAttributes.Count<CustomAttributeData>();

                if (CustomLength > 0 && i != pia.Length - 6)
                {
                    var attributes = propertyInfo.GetCustomAttributes(false);
                    foreach (var attribute in attributes)
                    {
                        //check if the object has attribute and set the data
                        if (attribute.GetType() == typeof(ColumnAttribute))
                        {

                            myCustom = (ColumnAttribute)attribute;

                            if (myCustom.NotAllowSearch)
                            {

                                isSearch = false;
                                //   coumnvalue = "{\"name\":\"" + propertyInfo.Name + "\",\"type\":\"" + myCustom.DataType + "\",\"value\":\"" + L(propertyInfo.Name) + "\",\"alink\":\"" + myCustom.ActionLink + "\"}";
                            }

                        }

                    }

                }
                if (i != pia.Length - 6 && isSearch)
                {
                    myCols.Add(item);
                }

            }
            return myCols;
        }

        /// <summary>
        /// return User File
        /// </summary>
        /// <param name="objUser"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        public static HeaderItemCollection UserConfigPageFolder<T>(DNHUsers objUser, string Page)
        {
            HeaderItemCollection ColumnName = new HeaderItemCollection();
            if (objUser == null) return ColumnName;
            //check the UserFile has exist or not
            string FileName = IOHelper.GetDirectory(myConfig.UserDataFolder + objUser.CompanyID + "\\Header\\" + objUser.UserName + "\\" + Page) + "\\header.bin";
            if (IOHelper.HasFile(FileName))
            {
                //get form the file
                ColumnName = IOHelper.ReadFromBinaryFile<HeaderItemCollection>(FileName);
            }
            else
            {
                //var nopConfig = 

                ColumnName = CommonHelper.JsonHeaderList<T>(objUser);
                IOHelper.WriteToBinaryFile<HeaderItemCollection>(FileName, ColumnName);
            }



            return ColumnName;
        }

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// Verifies that string is an valid IP-Address
        /// </summary>
        /// <param name="ipAddress">IPAddress to verify</param>
        /// <returns>true if the string is a valid IpAddress and false if it's not</returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            IPAddress ip;
            return IPAddress.TryParse(ipAddress, out ip);
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var pLen = postfix == null ? 0 : postfix.Length;

                var result = str.Substring(0, maxLength - pLen);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }

            return str;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : new string(str.Where(p => char.IsDigit(p)).ToArray());
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            return stringsToValidate.Any(p => string.IsNullOrEmpty(p));
        }

        /// <summary>
        /// Compare two arrasy
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="a1">Array 1</param>
        /// <param name="a2">Array 2</param>
        /// <returns>Result</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            //also see Enumerable.SequenceEqual(a1, a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        private static AspNetHostingPermissionLevel? _trustLevel;
        /// <summary>
        /// Finds the trust level of the running application (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
        /// </summary>
        /// <returns>The current trust level.</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //set minimum
                _trustLevel = AspNetHostingPermissionLevel.None;

                //determine maximum
                foreach (AspNetHostingPermissionLevel trustLevel in new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break; //we've set the highest permission we can
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }

        /// <summary>
        /// Sets a property on an object to a valuae.
        /// </summary>
        /// <param name="instance">The object whose property to set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            Type instanceType = instance.GetType();
            PropertyInfo pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new IFindException("No property '{0}' found on the instance of type '{1}'.", propertyName, instanceType);
            if (!pi.CanWrite)
                throw new IFindException("The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName, instanceType);
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        public static TypeConverter GetNopCustomTypeConverter(Type type)
        {
            //we can't use the following code in order to register our custom type descriptors
            //TypeDescriptor.AddAttributes(typeof(List<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            //so we do it manually here

            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();
            //if (type == typeof(ShippingOption))
            //    return new ShippingOptionTypeConverter();
            //if (type == typeof(List<ShippingOption>) || type == typeof(IList<ShippingOption>))
            //    return new ShippingOptionListTypeConverter();
            //if (type == typeof(PickupPoint))
            //    return new PickupPointTypeConverter();
            if (type == typeof(Dictionary<int, int>))
                return new GenericDictionaryTypeConverter<int, int>();

            return TypeDescriptor.GetConverter(type);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetNopCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetNopCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            string result = string.Empty;
            foreach (var c in str)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }

        /// <summary>
        /// Set Telerik (Kendo UI) culture
        /// </summary>
        public static void SetTelerikCulture()
        {
            //little hack here
            //always set culture to 'en-US' (Kendo UI has a bug related to editing decimal values in other cultures). Like currently it's done for admin area in Global.asax.cs

            var culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// Get difference in years
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            //source: http://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c
            //this assumes you are looking for the western idea of age and not using East Asian reckoning.
            int age = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-age))
                age--;
            return age;
        }

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }

            //not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }
        public static string JsonColumnType<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            PropertyInfo propertyInfo;
            string ColumnName = "[";
            string coumnvalue = "";
            ColumnAttribute myCustom;
            bool isHide = false;
            for (int i = 0; i < pia.Length - 5; i++)
            {
                propertyInfo = pia[i];
                coumnvalue = "";
                if (propertyInfo == null) continue;
                isHide = false;
                int CustomLength = propertyInfo.CustomAttributes.Count<CustomAttributeData>();

                if (CustomLength > 0)
                {
                    var attributes = propertyInfo.GetCustomAttributes(false);
                    foreach (var attribute in attributes)
                    {
                        if (attribute.GetType() == typeof(ColumnAttribute))
                        {

                            myCustom = (ColumnAttribute)attribute;
                            if (!myCustom.Hide)
                            {
                                coumnvalue = "{\"name\":\"" + propertyInfo.Name + "\",\"type\":\"" + myCustom.DataType + "\",\"value\":\"" + L(propertyInfo.Name) + "\",\"alink\":\"" + myCustom.ActionLink + "\",\"classN\":\""+ myCustom.ClassName+ "\"}";

                            }
                            else
                            {
                                isHide = true;
                            }

                        }
                        else
                        {
                            coumnvalue = JsonType(propertyInfo);
                        }
                    }

                }
                else
                {
                    coumnvalue = JsonType(propertyInfo);
                }

                if (i != pia.Length - 6 && !isHide)
                {
                    coumnvalue += ",";
                }

                ColumnName += coumnvalue;
            }
            ColumnName += "]";
            //foreach (PropertyInfo prop in props)
            //{
            //    //object propValue = prop.GetValue(myObject, null);

            //    // Do something with propValue
            //}
            return ColumnName;
        }

        public static HeaderItemCollection JsonHeaderList<T>(DNHUsers ObjUser)
        {
            HeaderItemCollection myCols = new HeaderItemCollection();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            PropertyInfo propertyInfo;
            ColumnAttribute myCustom;
            HeaderItem item;
            bool isHide = false;
            for (int i = 0; i < pia.Length - 5; i++)
            {
                item = new HeaderItem();
                propertyInfo = pia[i];
                if (propertyInfo == null) continue;
                item.name = propertyInfo.Name;
                item.value = L(propertyInfo.Name);
                item.type = JsonTypes(propertyInfo);
                isHide = false;
                int CustomLength = propertyInfo.CustomAttributes.Count<CustomAttributeData>();
                if (CustomLength > 0)
                {
                    var attributes = propertyInfo.GetCustomAttributes(false);

                    foreach (var attribute in attributes)
                    {
                        //check if the object has attribute and set the data
                        if (attribute.GetType() == typeof(ColumnAttribute))
                        {
                            myCustom = (ColumnAttribute)attribute;
                            isHide = myCustom.Hide;
                            if (!myCustom.Hide)
                            {
                                item.alink = myCustom.ActionLink;
                                item.type = myCustom.DataType.ToLower();
                                //   coumnvalue = "{\"name\":\"" + propertyInfo.Name + "\",\"type\":\"" + myCustom.DataType + "\",\"value\":\"" + L(propertyInfo.Name) + "\",\"alink\":\"" + myCustom.ActionLink + "\"}";

                            }
                        }
                    }
                }

                if (i != pia.Length - 6 && !isHide)
                {
                    myCols.Add(item);
                }
            }
            return myCols;
        }
        public static HeaderItemCollection HideColumn<T>(DNHUsers objUser, string Page)
        {
            HeaderItemCollection ColumnName = new HeaderItemCollection();
            if (objUser == null) return ColumnName;
            //check the UserFile has exist or not
            string FileName = IOHelper.GetDirectory(myConfig.UserDataFolder + objUser.CompanyID + "\\Header\\" + objUser.UserName + "\\" + Page) + "\\Hideheader.bin";
            if (IOHelper.hasFile(FileName))
            {
                //get form the file
                ColumnName = IOHelper.ReadFromBinaryFile<HeaderItemCollection>(FileName);
            }
            else
            {
                //var nopConfig = 

                ColumnName = CommonHelper.JsonHideHeaderList<T>(objUser);
                IOHelper.WriteToBinaryFile<HeaderItemCollection>(FileName, ColumnName);
            }



            return ColumnName;
        }
        public static HeaderItemCollection JsonHideHeaderList<T>(DNHUsers ObjUser)
        {
            HeaderItemCollection myCols = new HeaderItemCollection();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            PropertyInfo propertyInfo;
            ColumnAttribute myCustom;
            HeaderItem item;
            bool isHide = false;
            for (int i = 0; i < pia.Length; i++)
            {
                item = new HeaderItem();
                propertyInfo = pia[i];
                if (propertyInfo == null) continue;
                item.name = propertyInfo.Name;
                item.value = L(propertyInfo.Name);
                item.type = JsonTypes(propertyInfo);
                isHide = false;
                int CustomLength = propertyInfo.CustomAttributes.Count<CustomAttributeData>();

                if (CustomLength > 0)
                {
                    var attributes = propertyInfo.GetCustomAttributes(false);
                    foreach (var attribute in attributes)
                    {
                        //check if the object has attribute and set the data
                        if (attribute.GetType() == typeof(ColumnAttribute))
                        {
                            myCustom = (ColumnAttribute)attribute;
                            isHide = myCustom.Hide;
                            if (!myCustom.Hide)
                            {
                                item.alink = myCustom.ActionLink;
                                item.type = myCustom.DataType.ToLower();
                                //   coumnvalue = "{\"name\":\"" + propertyInfo.Name + "\",\"type\":\"" + myCustom.DataType + "\",\"value\":\"" + L(propertyInfo.Name) + "\",\"alink\":\"" + myCustom.ActionLink + "\"}";
                            }

                        }

                    }

                }


                if (i >= pia.Length - 5 || isHide)
                {
                    myCols.Add(item);
                }


            }
            return myCols;
        }
        /// <summary>
        /// change Language of system
        /// </summary>
        /// <param name="value">code</param>
        /// <returns></returns>
        public static string L(string value)
        {
            return Biz.Core.Services.DNHLocaleStringResourceManager.LResource(value, CurrentUser.CompanyID, CurrentUser.UserLanguageID).ResourceValue; 
        }
        public static string L(string value, int CompanyID, int LanguageID)
        {
            return Biz.Core.Services.DNHLocaleStringResourceManager.LResource(value, CompanyID, LanguageID).ResourceValue;
        }
        private static string JsonType(PropertyInfo propertyInfo)
        {
            string val = "";
            switch (Type.GetTypeCode(propertyInfo.PropertyType))
            {
                case TypeCode.DateTime:
                    val = "{\"name\":\"" + propertyInfo.Name + "\",\"type\":\"date\",\"value\":\"" + L(propertyInfo.Name) + "\"}";

                    // It's an int
                    break;

                case TypeCode.Decimal:
                    val = "{\"name\":\"" + propertyInfo.Name + "\",\"type\":\"number\",\"value\":\"" + L(propertyInfo.Name) + "\"}";
                    // It's a string
                    break;
                case TypeCode.Boolean:
                    val = "{\"name\":\"" + propertyInfo.Name + "\",\"type\":\"bool\",\"value\":\"" + L(propertyInfo.Name) + "\"}";
                    break;

                // Other type code cases here...

                default:
                    val = "{\"name\":\"" + propertyInfo.Name + "\",\"value\":\"" + L(propertyInfo.Name) + "\"}";
                    break;
            }
            return val;
        }
        private static string JsonTypes(PropertyInfo propertyInfo)
        {
            string val = "string";
            switch (Type.GetTypeCode(propertyInfo.PropertyType))
            {
                case TypeCode.DateTime:
                    val = "date";
                    // It's an int
                    break;
                case TypeCode.Decimal:
                    val = "number";
                    // It's a string
                    break;
                case TypeCode.Boolean:
                    val = "bool";
                    // It's a string
                    break;
                // Other type code cases here...
                default:
                    val = "string";
                    break;
            }
            return val;
        }
        public static bool DelteSiteMap(int CompanyID)
        {
            try
            {
                string Folder = UserSitemapFolder(CompanyID);
                Directory.Delete(Folder, true);
                return true;
            }
            catch (Exception objEx)
            {
                return false;
            }
        }
        public static string UserSitemapFolder(int CompanyID)
        {
            return IOHelper.GetDirectory(string.Format(myConfig.UserSiteMapFolder, CompanyID));
        }

        /// <summary>
        /// <UserFileImportFolder Folder="~/App_Data/Config/{0}/FileImport/{1}/{2}/" />
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserName"></param>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static string UserFileImportFolder(int CompanyID, string UserName, string screen)
        {
            return IOHelper.GetDirectory(string.Format(myConfig.UserFileImportFolder, CompanyID, UserName, screen));
        }
        public static void SaveImportFile<T>(DNHUsers objUser, string Screen, string ExFile, IEnumerable<T> objSMaps)
        {
            string FileName = UserFileImportFolder(objUser.CompanyID, objUser.UserName, Screen) + ExFile + "_" + DateTime.Today.ToFileTimeUtc() + ".bin";
            IOHelper.WriteToBinaryFile<IEnumerable<T>>(FileName, objSMaps);
        }
        public static void SaveImportErrorFile<T>(DNHUsers objUser, string Screen, IEnumerable<T> objSMaps)
        {
            string FileName = UserFileImportFolder(objUser.CompanyID, objUser.UserName, Screen) + "Exc.bin";
            IOHelper.WriteToBinaryFile<IEnumerable<T>>(FileName, objSMaps);
        }
        public static System.Data.DataTable GetImportErrorFile<T>(DNHUsers objUser, string Screen)
        {
            DataTable dt = new DataTable();
            string FileName = UserFileImportFolder(objUser.CompanyID, objUser.UserName, Screen) + "Exc.bin";
            if (IOHelper.HasFile(FileName))
            {
                //get form the file
                dt = IOHelper.ReadFromBinaryFile<IEnumerable<T>>(FileName).ToDataTable<T>();
            }
            return dt;
        }
        public static DNHSiteMapCollection ReadWriteSiteMap(DNHUsers objUser)
        {
            DNHSiteMapCollection objSMaps = new DNHSiteMapCollection();
            if (objUser != null)
            {
                //DNHSiteMapManager.GetAllByUser(objUser.DomainID, objUser.CompanyID, null);

                //save file 

                string FileName = IOHelper.GetDirectory(string.Format(myConfig.UserSiteMapFolder, objUser.CompanyID) + objUser.UserName) + "\\sitemap.bin";
                if (IOHelper.HasFile(FileName))
                {
                    //get form the file
                    objSMaps = IOHelper.ReadFromBinaryFile<DNHSiteMapCollection>(FileName);
                }
                else
                {
                    objSMaps = DNHSiteMapManager.GetAllByUser(objUser.UserName, objUser.CompanyID, null);
                    //var nopConfig = 
                    if (objSMaps.Count > 0)
                    {

                        IOHelper.WriteToBinaryFile<IList<DNHSiteMap>>(FileName, objSMaps);
                    }
                }
                objUser.UserSiteMaps = objSMaps;
                // Contract objContract = ContractManager.GetById(objEmp.EmployeeCode);
                //add UserSession
                HttpContext.Current.Session[SystemConfig.loginKey] = objUser;
            }
            return objSMaps;
        }
    }
}
