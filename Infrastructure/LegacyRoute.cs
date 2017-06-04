using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Internal;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute : IRouter
    {
        private string[] urls;
        private IRouter mvcRoute;

        public LegacyRoute(IServiceProvider services, params string[] targetUrls)
        {
            this.urls = targetUrls;
            mvcRoute = services.GetRequiredService<MvcRouteHandler>();
        }

        public async Task RouteAsync(RouteContext context)
        {
            string requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');

            if (urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
            {
                //context.Handler = async ctx =>
                //{
                //    byte[] bytes = Encoding.ASCII.GetBytes($"URL: {requestedUrl}");
                //    HttpResponse response = ctx.Response;
                //    await response.Body.WriteAsync(bytes, 0, bytes.Length);
                //};

                context.RouteData.Values["controller"] = "Legacy";
                context.RouteData.Values["action"] = "GetLegacyUrl";
                context.RouteData.Values["legacyUrl"] = requestedUrl;

                await mvcRoute.RouteAsync(context);
            }
            //return Task.CompletedTask;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            VirtualPathData result = null;
            if (context.Values.ContainsKey("legacyUrl")){
                string url = context.Values["legacyUrl"] as string;
                if (urls.Contains(url)){
                    result = new VirtualPathData(this, url);
                }
            }
            return result;
        }

    }
}