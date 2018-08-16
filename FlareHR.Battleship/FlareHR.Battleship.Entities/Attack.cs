namespace FlareHR.Battleship.Entities
{
    public class Attack
    {
        /// <summary>
        /// Gets and sets if the attack is valid
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Column { get; set; }
        public int Line { get; set; }
        public string Message { get; set; }
    }
}
