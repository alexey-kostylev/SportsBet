using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBet.Core;

/// <summary>
/// Specifies player positions for baseball and football, including both offensive and defensive roles.
/// </summary>
/// <remarks>This enumeration includes positions commonly used in baseball (such as Starting Pitcher, Catcher, and
/// Shortstop) and American football (such as Quarterback, Wide Receiver, and Kicker). Use these values to represent or
/// filter player roles in sports-related applications. The set of positions may not be exhaustive for all leagues or
/// formats. The reason of using a class is to support extensibility for future new sports</remarks>
public partial struct Position
{
    /// <summary>
    /// Starting Pitcher
    /// </summary>
    public static readonly Position SP = new("sp"); //Starting Pitcher

    ///// <summary>
    ///// Relief Pitcher
    ///// </summary>
    public static readonly Position RP = new("rp"); // Relief Pitcher

    ///// <summary>
    ///// Catcher
    ///// </summary>
    public static readonly Position C = new("c"); // Catcher

    ///// <summary>
    ///// First Base (1B)
    ///// </summary>
    public static readonly Position FirstBase = new("1b"); // First Base
    ///// <summary>
    ///// Second Base (2B
    ///// </summary>
    public static readonly Position SecondBase = new("2b"); // Second Base
    ///// <summary>
    ///// Third Base (3B)
    ///// </summary>
    public static readonly Position ThirdBase = new("3b"); // Third Base
    ///// <summary>
    ///// Shortstop
    ///// </summary>
    public static readonly Position SS = new("ss"); // Shortstop
    ///// <summary>
    ///// Left Field
    ///// </summary>
    public static readonly Position LF = new("lf"); // Left Field
    ///// <summary>
    ///// Center Field
    ///// </summary>
    public static readonly Position CF = new("cf"); // Center Field
    ///// <summary>
    ///// Right Field
    ///// </summary>
    public static readonly Position RF = new("rf"); // Right Field
    ///// <summary>
    ///// Designated Hitter
    ///// </summary>
    public static readonly Position DH = new("dh"); // Designated Hitter
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
