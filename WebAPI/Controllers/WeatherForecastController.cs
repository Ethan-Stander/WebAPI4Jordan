using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //API 

        //HttpPost

        //This POSTS information to the controller. You're essentially sending info to your API 

        //HttpGet 

        //The client REQUESTS INFORMATION and the controller sends it 

        //HttpDelete

        //You're deleting a server resource 

        //HttpPUT 
        //Editing a resource 



        [HttpGet("GetWords")]

        public List<string> GetWord()
            {
            //sql processing method 
            List<string> words = new List<string>();
            words = SQLModel.GetWords();   
            return words;
            }
        [HttpGet("GetWordsSorted")]

        public List<string> GetWordsSorted()
        {
            List<string> words = new List<string>();
            words = SQLModel.GetWords(); 
            words.Sort();
            return words;
        }
        [HttpGet("GetOneWord")]
        public string GetOneWord()
            {
            return SQLModel.GetWords()[0];
            }

        [HttpPost("PostMethod")]
        public void Post()
        {
            SQLModel.GetUsers(); 
        }



    }
}