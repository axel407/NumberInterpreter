namespace RequestProcessingPipeline
{
    public class FromHundredToThousandMiddleware
    {
        private readonly RequestDelegate _next;
        
        public FromHundredToThousandMiddleware(RequestDelegate next)
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
               
                if(number == 1000)
                {
                    await context.Response.WriteAsync("Your number is one thousand");
                }
                else
                {
                    string? result = string.Empty;
                    if (number > 100)
                    {
                        result = context.Session.GetString("number");
                        number = (number % 1000) / 100;
                    }
                    else
                        number /= 100;
                    
                    string[] Numbers = { "one hundred", "two hundred", "three hundred", "four hundred", "five hundred", "six hundred", "seven hundred", "eight hundred", "nine hundred" };
                    if (number > 0)
                    {
                        context.Session.SetString("number", result + " " + Numbers[number - 1]);
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
