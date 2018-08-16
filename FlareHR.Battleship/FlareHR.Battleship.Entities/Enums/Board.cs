using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlareHR.Battleship.Entities.Enums
{
    public enum PositionState
    {
        [Display(Name = "Empty")]
        Empty = 0,
        [Display(Name = "Ship Sunk")]
        Sunk = 1,
        [Display(Name = "Attacked")]
        Attacked = 2,
        [Display(Name = "Ship Floating")]
        Floating = 3
    }

}
