namespace RequestProcessingPipeline
{
    public class FromElevenToNineteenMiddleware
    {
        private readonly RequestDelegate _next;

        public FromElevenToNineteenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                int check = number % 100;
                if (check < 11 || check > 19)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    string? result = string.Empty;
                    if (number > 100)
                    {
                        result = context.Session.GetString("number");
                    }
                    string[] Numbers = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    await context.Response.WriteAsync("Your number is " + result + " " + Numbers[check - 11]);
                }
            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
