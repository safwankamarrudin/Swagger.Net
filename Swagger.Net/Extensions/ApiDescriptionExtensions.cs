using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using System.Web.Http.OData;

namespace Swagger.Net.Extensions
{
    public static class ApiDescriptionExtensions
    {
        private static readonly Dictionary<string, string> Actions = new Dictionary<string, string>
                                                                         {
                                                                             {"get", "get"},
                                                                             {"post", "post"},
                                                                             {"put", "put"},
                                                                             {"delete", "delete"},
                                                                             {"patch", "patch"},
                                                                             {"merge", "merge"}
                                                                         };

        public static string GetPath(this ApiDescription apiDescription)
        {
            var isOData = apiDescription.ActionDescriptor
                                        .ControllerDescriptor
                                        .GetCustomAttributes<ODataRoutingAttribute>()
                                        .Any();
            if (isOData)
            {
                var oDataPath = apiDescription.RelativePath.Replace("?key={key}", "({key})");
                var actionName = apiDescription.ActionDescriptor.ActionName;
                return !Actions.ContainsKey(actionName.ToLower()) ? string.Format("{0}/{1}", oDataPath, actionName) : oDataPath;
            }

            return apiDescription.RelativePath;
        }
    }
}