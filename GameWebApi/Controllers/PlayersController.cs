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

namespace GameWebApi.Controllers
{
    public class PlayersController : ApiController
    {
        // GET api/players
        // fetch all players
        public IEnumerable<Player> Get()
        {
            using (GameApiEntities entities = new GameApiEntities())
            {
                return entities.Players.Include("PlayerInGameStatus").ToList();
            }
        }

        // GET api/players/<PlayerId>
        // find player's score by player id and game id
        [HttpGet]
        public IQueryable<Object> GetScoreByGameIDPlayerID(int pid, int gid)
        {
            GameApiEntities entities = new GameApiEntities();

            var query = from plyr in entities.Players
                   join gm in entities.Games on plyr.GameID equals gm.GameID
                        join plystat in entities.PlayerInGameStatus on plyr.PlayerId equals plystat.PlayerId
                        where plyr.PlayerId == pid && gm.GameID == gid
                   select new
                   {
                       name = plyr.PlayerName,
                       score = plystat.PlayerScore
                   };
            return query.AsQueryable();
        }

        // GET api/players/<GameId>
        // find players' score by game id
        [Authorize]
        [HttpGet]
        public IEnumerable<Player> GetScoreByGameID(int gid)
        {
            GameApiEntities entities = new GameApiEntities();

            return entities.Players.Include("PlayerInGameStatus").Where(e => e.GameID == gid).ToList();
        }

        // POST api/players
        [HttpPost]
        public IHttpActionResult JoinGame(Player joinGame)
        {
            GameApiEntities entities = new GameApiEntities();

            entities.Players.Add(joinGame);
            entities.SaveChanges();

            string key = "this is a difficult to guess secret key";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt_token);
        }
    }
}