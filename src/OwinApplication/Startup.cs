using Owin;

namespace OwinApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                context.Response.WriteAsync("<p>Start. Middleware 1</p>");

                return next().ContinueWith(task =>
                {
                    context.Response.WriteAsync("<p>Done. Middleware 1</p>");
                });
            });

            app.Use((context, next) =>
            {
                context.Response.WriteAsync("<p>Start. 2nd MW</p>");

                return next().ContinueWith(task =>
                {
                    context.Response.WriteAsync("<p>Done. Middleware 2</p>");
                });
            });

            app.Run(context =>
            {
                context.Response.WriteAsync("<p>3rd MW</p>");

                return context.Response.WriteAsync(string.Format("<p>Hello from OWIN middleware.</p> {0}", context.Request.Path));
            });
        }
    } 
}