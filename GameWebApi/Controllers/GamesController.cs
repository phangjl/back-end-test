using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameDatabase;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data.Entity;

namespace GameWebApi.Controllers
{
    public class GamesController : ApiController
    {
        // GET api/games
        public IEnumerable<Game> Get()
        {
            using(GameApiEntities entities = new GameApiEntities())
            {
                return entities.Games.Include("Statements").Include("GameStatus").Include("Players").Include("Players.PlayerInGameStatus").ToList();
            }
        }

        // GET api/games/<GameID>
        [Authorize]
        public Game Get(int id)
        {
            using (GameApiEntities entities = new GameApiEntities())
            {
                return entities.Games.Include("Statements").Include("GameStatus").Include("Players").Include("Players.PlayerInGameStatus").FirstOrDefault(e=>e.GameID==id);
            }
        }


        // POST api/games
        [HttpPost]
        [AllowAnonymous]
        public object CreateGame(Game game)
        {
            GameApiEntities entities = new GameApiEntities();

            entities.Games.Add(game);
            entities.SaveChanges();

            return Ok(game.GameID);
        }
    }
}