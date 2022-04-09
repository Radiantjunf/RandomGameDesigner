using Microsoft.AspNetCore.Mvc;
using RandomGameDesigner.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RandomGameDesigner.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        // GET: api/<GameController>
        [HttpPost]
        public IEnumerable<Game> RequestGames([FromBody]Request request)
        {

            DataAccess g = new DataAccess();
            var t = g.GetUserGamesIDs(request.Name);

            var random = new Random();
            var intArray = Enumerable.Range(0, t.Count).OrderBy(t => random.Next()).Take(3).ToArray();



            var g1 = g.GetGameInfo(t[intArray[0]].appid);
            var g2 = g.GetGameInfo(t[intArray[1]].appid);
            var g3 = g.GetGameInfo(t[intArray[2]].appid);
            List<Game> te = new List<Game>();
            te.Add(g1);
            te.Add(g2);
            te.Add(g3);



            return te.ToArray();
        }

       








    }
}