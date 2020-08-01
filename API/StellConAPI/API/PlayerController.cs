using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StellConAPI.Models;

namespace StellConAPI.API
{
    public class PlayerController : ApiController
    {
        // GET: api/Player
        public Players Get()
        {
            List<Player> _playerList = new List<Player>();

            _playerList.Add(new Player("abcdmku", 8710, 154, 87, 25, 81, 3, 8, 4, 8));
            _playerList.Add(new Player("storm58", 2548, 148, 31, 53, 8, 4, 7, 2, 4));

            Players _players = new Players(_playerList.ToArray());
            return _players;
        }

        // GET: api/Player/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Player
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Player/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
        }
    }
}
