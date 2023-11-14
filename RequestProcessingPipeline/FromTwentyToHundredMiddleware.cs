namespace RequestProcessingPipeline
{
    public class FromTwentyToHundredMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredMiddleware(RequestDelegate next)
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
                if(check < 20)
                {
                    await _next.Invoke(context);
                }
                else if (number == 100)
                {
                    await context.Response.WriteAsync("Your number is one hundred");
                }
                else
                {
                    string? result = string.Empty;
                    if (number > 100)
                    {
                        result = context.Session.GetString("number");
                        number = (number % 100) / 10;
                    }
                    else
                        number /= 10;

                    string[] Numbers = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    if (number > 0)
                    {
                        context.Session.SetString("number", result + " " + Numbers[number - 2]);
                        await _next.Invoke(context);
                    }
                    else
                        await _next.Invoke(context);
                }              
            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
