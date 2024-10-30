// Controllers/ChatBotController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace OnlineLearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBotController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> GetResponse([FromBody] ChatBotRequest request)
        {
            string response = GetPredefinedResponse(request.Input);
            return response;
        }

        private string GetPredefinedResponse(string input)
        {
            // Define your predefined responses here
            Dictionary<string, string> predefinedResponses = new Dictionary<string, string>
            {
                { "hello", "Hello! How can I assist you?" },
                { "help", "Sure! I can help you with that." },
                { "goodbye", "Goodbye! Have a great day!" }
            };

            // Check if the input matches any predefined response
            if (predefinedResponses.ContainsKey(input.ToLower()))
            {
                return predefinedResponses[input.ToLower()];
            }

            // If no predefined response matches, return a default response
            return "I'm sorry, I didn't understand. Can you please rephrase your question?";
        }
    }

    public class ChatBotRequest
    {
        public string Input { get; set; }
    }
}
