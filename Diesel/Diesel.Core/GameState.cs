using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class GameState
    {
        public int Id { get; set; }
        public ActiveGame Game { get; set; }
        public BoardState Player1BoardState { get; set; }
        public BoardState Player2BoardState { get; set; }
        public CommandList Player1LastCommandList { get; set; }
        public CommandList Player2LastcommandList { get; set; }
        public string RandomNumber { get; set; }
    }
}
