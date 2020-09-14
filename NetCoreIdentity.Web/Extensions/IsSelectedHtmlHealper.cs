using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NetCoreIdentity.Web.Helpers
{
    public static class IsSelectedHtmlHealper
    {
        //public static IHtmlContent IsSelected(this IHtmlHelper html, string controllers = "", string actions = "", string cssClass = "active")
        //{
        //    ViewContext viewContext = html.ViewContext;
        //    bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

        //    if (isChildAction)
        //        viewContext = html.ViewContext.ParentActionViewContext;

        //    RouteValueDictionary routeValues = viewContext.RouteData.Values;
        //    string currentAction = routeValues["action"].ToString();
        //    string currentController = routeValues["controller"].ToString();

        //    if (String.IsNullOrEmpty(actions))
        //        actions = currentAction;

        //    if (String.IsNullOrEmpty(controllers))
        //        controllers = currentController;

        //    string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
        //    string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

        //    return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
        //        cssClass : String.Empty;
        //    //return  acceptedControllers.Contains(currentController) ?
        //    //   cssClass : String.Empty;
        //}
        public static string IsControllerSelected(this IHtmlHelper html, string controllers = "", string cssClass = "active")
        {
            ViewContext viewContext = html.ViewContext;


            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            if (routeValues["controller"] == null)
            {
                return string.Empty;
            }
            string currentController = routeValues["controller"].ToString();



            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            return acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
            //return  acceptedControllers.Contains(currentController) ?
            //   cssClass : String.Empty;
        }

    }
}