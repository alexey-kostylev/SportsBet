using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBet.Core;

/// <summary>
/// Specifies player positions for football, including both offensive and defensive roles.
/// </summary>
/// <remarks>This enumeration includes positions commonly used in American football (such as Quarterback, Wide Receiver, and Kicker). Use these values to represent or
/// filter player roles in sports-related applications.
/// </remarks>
public partial struct Position
{
    /// <summary>
    /// Quarterback
    /// </summary>
    public static readonly Position QB = new("qb"); // Quarterback

    ///// <summary>
    ///// wide receiver
    ///// </summary>
    public static readonly Position WR = new("wr"); // Wide Receiver

    ///// <summary>
    ///// running back
    ///// </summary>
    public static readonly Position RB = new("rb"); // Running Back

    ///// <summary>
    ///// tight end
    ///// </summary>
    public static readonly Position TE = new("te"); // Tight End

    ///// <summary>
    ///// kicker
    ///// </summary>
    public static readonly Position K = new("k"); // Kicker

    ///// <summary>
    ///// punter
    ///// </summary>
    public static readonly Position P = new("p"); // Punter

    ///// <summary>
    ///// kicker returner
    ///// </summary>
    public static readonly Position KR = new("kr"); // Kicker Returner

    ///// <summary>
    ///// punt returner
    ///// </summary>
    public static readonly Position PR = new("pr"); // Punt Returner
}
