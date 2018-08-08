using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/js/stack/MyApp.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.unobtrusive.bootstrap.js"

                        ));
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //          "~/Scripts/jquery-ui.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
                      "~/Scripts/jquery.unobtrusive-ajax*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/js/stack").Include(
                    "~/Scripts/js/stack/Jquery2.js",
                    "~/Scripts/js/stack/Jquery.js",
                    "~/Scripts/js/stack/Headroom.js",
                    "~/Scripts/js/stack/App_menu.js",
                    "~/Scripts/js/stack/App.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      //"~/Scripts/js/plugins/metisMenu/jquery.metisMenu.js",
                      "~/Scripts/js/plugins/slimscroll/jquery.slimscroll.min.js",
                      "~/Scripts/respond.js"));

            //<!-- iCheck -->
            bundles.Add(new ScriptBundle("~/js/iCheck").Include(
                      "~/Scripts/js/plugins/iCheck/icheck.min.js"));

            //Date Picker
            bundles.Add(new ScriptBundle("~/js/datapicker").Include(
                    "~/Scripts/js/plugins/datapicker/Moment.js",
                    "~/Scripts/js/plugins/datapicker/bootstrap-datetimepicker.js",
                    "~/Scripts/js/plugins/datapicker/bootstrap-datepicker.js",
                    "~/Scripts/js/plugins/datapicker/picker.js",
                    "~/Scripts/js/plugins/datapicker/picker.time.js"
                    ));
            bundles.Add(new ScriptBundle("~/Scripts/jsForm").Include(
"~/Scripts/js/JsForm/jquery.jsForm.controls.js",
"~/Scripts/js/JsForm/jquery.jsForm.js"
               ));
            //bundles.Add(new ScriptBundle("~/js/Times").Include(
            //        "~/Scripts/js/Jquery/jquery.timeAutocomplete.js",
            //        "~/Scripts/js/Jquery/formatters/ampm.js",
            //        "~/Scripts/js/Jquery/formatters/24hr.js"
            //        ));

            //Data table//
            bundles.Add(new ScriptBundle("~/js/Dtable").Include(
                      "~/Scripts/js/plugins/dataTables/jquery.dataTables.js",
                      "~/Scripts/js/plugins/dataTables/datatable-basic.js",
                      "~/Scripts/js/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/js/plugins/dataTables/dataTables.responsive.js",
                      "~/Scripts/js/plugins/dataTables/dataTables.fixedHeader.js",
                      "~/Scripts/js/plugins/dataTables/FixedColumn.js",
                      "~/Scripts/js/plugins/dataTables/dataTables.tableTools.min.js",
                      "~/Scripts/js/plugins/dataTables/fnReloadAjax.js",
                      "~/Scripts/js/plugins/dataTables/Datatablepaging.js",
                        "~/Scripts/js/plugins/jeditable/jquery.jeditable.js",

                        "~/Scripts/js/plugins/slimscroll/jquery.slimscroll.min.js",
                      "~/Scripts/js/ifind.js",
                      "~/Scripts/js/print.js",
                      //"~/Scripts/js/inspinia.js",
                      "~/Scripts/js/RBVHFrm.js",
                      "~/Scripts/js/DNHTable.js",
                      "~/Scripts/js/DNHSearchTable.js",
                      "~/Scripts/js/FormLoad.js",
                      "~/Scripts/js/Download.js",
                       "~/Scripts/js/Search/handlebars.js",
                       "~/Scripts/js/Search/typeahead.bundle.js",
                      //"~/Scripts/js/plugins/dataTables/ifind_table.js",
                      //"~/Scripts/Model/Ifind_Table.js",
                      "~/Scripts/js/Custom.js",
                      "~/Scripts/Model/BindFrm.js",
                      "~/Scripts/js/select2.js"
                      //   "~/Content/js/plugins/dataTables/select.js"
                      //     "~/Content/js/plugins/flot/jquery.flot.time.js"
                      ));

            //message Box
            bundles.Add(new ScriptBundle("~/js/Lobibox").Include(
                   "~/Scripts/js/plugins/Lobibox/js/lobibox.js"));

            //<!-- swichery -->
            bundles.Add(new ScriptBundle("~/js/swichery").Include(
                      "~/Scripts/js/plugins/switchery/switchery.js"));
            bundles.Add(new ScriptBundle("~/js/maskMoney").Include(
                      "~/Scripts/js/plugins/maskMoney/jquery.maskMoney.min.js"));

            ////<!-- Ifinds -->
            //bundles.Add(new ScriptBundle("~/js/Ifind").Include(
            //      //"~/Scripts/js/jquery.mcautocomplete.js",
            //      //"~/Scripts/js/select2.js",
            //      //"~/Scripts/model/Ifind_Table.js",
            //  "~/Script/js/ifind.js"));

            //app
            //bundles.Add(new ScriptBundle("~/js/app").Include(
            //        "~/Scripts/js/inspinia.js"));

            bundles.Add(new ScriptBundle("~/js/masks").Include("~/Scripts/js/plugins/input-mask/jquery.inputmask.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Scripts/css/Botstrap/bootstrap.css",
                "~/Scripts/css/Botstrap/bootstrap-extended.css",
                "~/Scripts/css/bootstrap.ext.css",
                "~/Scripts/css/Botstrap/V1/vertical-overlay-menu.css",
                "~/Scripts/css/Botstrap/V1/vertical-menu.css",

                "~/Scripts/css/Botstrap/style.css",
                "~/Scripts/css/Botstrap/style_002.css",
                "~/Scripts/css/Botstrap/style_003.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Scripts/css/bootstrap.min.css",
                       //"~/Scripts/css/bootstrap.ext.css",
                       "~/Scripts/css/bootstrap-theme.css",
                       "~/Scripts/css/site.css",
                       //"~/Scripts/css/style.css",
                       "~/Scripts/css/jquery-ui.css",
                       "~/Scripts/css/flag-icon.css",
                       "~/Scripts/font-awesome/css/font-awesome.css",
                       "~/Scripts/css/animate.css",
                       "~/Scripts/css/select2.css",
                       "~/Scripts/css/dnhmenu.css",
                       "~/Scripts/css/dnhSeacrh.css"
                      ));

            //Datatable style
            bundles.Add(new StyleBundle("~/Scripts/css/Table").Include(
                    "~/Scripts/css/plugins/dataTables/dataTables.bootstrap.css",
                    "~/Scripts/css/plugins/dataTables/dataTables.responsive.css",
                    "~/Scripts/css/plugins/dataTables/fixedHeader.bootstrap.css",
                    "~/Scripts/css/plugins/dataTables/FixCol.css",
                     "~/Scripts/css/plugins/dataTables/dataTables.tableTools.min.css",
                     "~/Scripts/css/Botstrap/app.css")
                    );

            bundles.Add(new StyleBundle("~/Content/datapicker").Include(
                          "~/Scripts/css/plugins/datapicker/datepicker3.css",
                          "~/Scripts/css/plugins/datapicker/pickadate.css"
                          )
                          );

            //iCheck style
            bundles.Add(new StyleBundle("~/Content/iCheck").Include(
                    "~/Scripts/css/plugins/iCheck/custom.css"));

            //message box
            bundles.Add(new StyleBundle("~/Content/Lobibox").Include(
                          "~/Scripts/js/plugins/Lobibox/css/lobibox.css")
                          );

            //Jquery UI
            bundles.Add(new StyleBundle("~/Content/JUI").Include(
                   "~/Scripts/css/jquery-ui.css"));

        }
    }
}
