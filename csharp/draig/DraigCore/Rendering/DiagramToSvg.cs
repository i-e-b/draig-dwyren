using System.Text;
using DraigCore.Internal;
using Tag;

namespace DraigCore.Rendering;

/// <summary>
/// Takes a diagram program, and outputs an SVG tag
/// which can be embedded in a HTML document.
/// <p></p>
/// Commands are separated by newlines. Blank lines are ignored.
/// <p></p>
/// Commands: (command names are case insensitive, but everything else is case sensitive)
/// <dl>
///    <dt># [text...]</dt><dd>Comment. Ignores the line.</dd>
///    
///    <dt>Pin name x y</dt><dd>set named pin to a position.
///                     Pins are defined as needed.
///                     If you set the value of a pin more than once, it gets updated.</dd>
///                     
///    <dt>Box pin1 pin2 [text...]</dt><dd>draw a box between two pins, with optional text.
///                                The box will remain in place if the pins are later moved.</dd>
///    
///    <dt>Table cols pin1 pin2 [text...]</dt><dd>Same as 'Box', but present as a table. '|' splits cells in the text.
///                              'cols' is the numeric X dimension of the table. Rows are added to 
///                              fit cells.</dd>
///    
///    <dt>Split name pin1 pin2</dt><dd>create a new pin half-way between two others</dd>
///    
///    <dt>Move x y [pin1..pinN]</dt><dd>move all named pins by the given offset</dd>
///    
///    <dt>MoveTo pin0 [pin1..pinN]</dt><dd>move pins [1..n] as a group so their origin is at pin0</dd>
///    
///    <dt>MoveOver pin0 pin1 [pin2..pinN]</dt><dd>move pins [1..n] as a group so pin1's origin is at pin0</dd>
///    
///    <dt>Centre pin0 [pin1..pinN]</dt><dd>move pins [1..n] as a group so their centre is at pin0</dd>
///    
///    <dt>Fill rgb</dt><dd>set fill colour for boxes. Should be a CSS hex colour (e.g. `eef`, `ff7`)</dd>
///    <dt>Stroke rgb</dt><dd>set colour for lines and arrows. Should be a CSS hex colour (e.g. `eef`, `ff7`)</dd>
///    
///    <dt>ClearFill</dt><dd>reset fill colour to default.</dd>
///    <dt>ClearStroke</dt><dd>reset line colour to default.</dd>
///    
///    <dt>Project name pinA pinB pinX</dt><dd>create a new pin by projecting X onto the line A->B</dd>
///    
///    <dt>Offset name pin1 dx dy</dt><dd>create a new pin offset from an existing one</dd>
///    
///    <dt>Line pin1 pin2 [text...]</dt><dd>draw a line between two pins, with optional text along the line</dd>
///    
///    <dt>FlipLine pin1 pin2 [text...]</dt><dd>Same as Line, but text is upside-down</dd>
///    
///    <dt>Arrow pin1 pin2 [text...]</dt><dd>draw a line between pins,
///                                  with an arrow pointing at 'pin2',
///                                  and optional text along the line.</dd>
///                                  
///    <dt>FlipArrow pin1 pin2 [text...]</dt><dd>same as arrow, but the text is upside-down</dd>
///    
///    <dt>Reset [pin1..pinN]</dt><dd>offset all named pins by the same amount, so the
///                          top left pin ends up at 0,0</dd>
///    
///    <dt>Corner name pin1 pin2</dt><dd>create a new pin at (pin1.x, pin2.y)</dd>
///    
///    <dt>AutoBox name x y width height [text...]</dt><dd>Create a box at the given points, and create 8 new pins
///                                     around the sides (name_tl, name_t, name_tr,
///                                                       name_l,          name_r,
///                                                       name_bl, name_b, name_br)</dd>
///                                                       
///    <dt>AutoTable cols name x y width height [text...]</dt><dd>Same as 'AutoBox', but present as a table. '|' splits cells in the text.
///                              'cols' is the numeric X dimension of the table. Rows are added to 
///                              fit cells. (ALSO, maybe add a pin for the centre of each cell?)</dd>
///                              
///    <dt>Group x y [text...]</dt><dd>same as Translate</dd>
///    <dt>Translate x y [text...]</dt><dd>New pins and boxes are offset by this amount. Doesn't affect already set pins. Use `Translate 0 0` to reset</dd>
/// </dl>
/// </summary>
/// <remarks>
/// This is a C# translation of https://jsfiddle.net/i_e_b/L9v16acs/ .
/// It should be able to run the same programs, and result in the same output.
/// </remarks>
public static class DiagramToSvg
{
    private const double MaxPinValue = (double)int.MaxValue;
    private const double MinPinValue = (double)int.MinValue;

    private static string Clean(string msg)
    {
        return msg
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\n", "<xhtml:br/>")
            .Replace("\\n", "<xhtml:br/>");
    }

    private static Vec2d ProjectPoint(Vec2d v1, Vec2d v2, Vec2d p)
    {
        var e1 = new Vec2d { X = v2.X - v1.X, Y = v2.Y - v1.Y };
        var e2 = new Vec2d { X = p.X - v1.X, Y = p.Y - v1.Y };

        var dot = e1.X * e2.X + e1.Y * e2.Y; // get dot product of e1, e2
        var len2 = e1.X * e1.X + e1.Y * e1.Y;
        if (len2 == 0) len2 = 1;

        return new Vec2d
        {
            X = v1.X + dot * e1.X / len2,
            Y = v1.Y + dot * e1.Y / len2
        };
    }

    private static string RenderToSvg(string[] lines)
    {
        var width = 100;
        var height = 100;
        var pins = new Dictionary<string, Vec2d>();
        var content = new StringBuilder();
        var state = new ChartState
        {
            Translate = new Vec2d { X = 0, Y = 0 }
        };

        foreach (var line in lines)
        {
            var result = HandleCommand(line, pins, state);
            if (string.IsNullOrEmpty(result)) continue;

            content.Append(result);
            content.AppendLine();
        }

        // find extents
        foreach (var pin in pins.Values)
        {
            width = (int)Math.Max(width, pin.X);
            height = (int)Math.Max(height, pin.Y);
        }

        // add some margin
        width += 20;
        height += 20;

        var svg = T.g("svg", "xmlns:xhtml", "http://www.w3.org/1999/xhtml", "xmlns:xlink",
            "http://www.w3.org/1999/xlink",
            "xmlns", "http://www.w3.org/2000/svg",
            "viewBox", $"-10 -10 {width} {height}")[
            T.g("defs")[
                T.g("style", "type", "text/css")[$"<![CDATA[{DiagramStyles}]]>"]
            ],
            T.g("defs")[
                T.g("marker", "id", "dot", "viewBox", "-10 -10 20 20", "refX", "0", "refY", "0",
                    "markerUnits", "strokeWidth", "markerWidth", "10", "markerHeight", "10",
                    "orient", "auto", "style", "fill:#333")[
                    T.g("circle", "cx", "0", "cy", "0", "r", "3")
                ],
                T.g("marker", "id", "arrow_l2r", "viewBox", "0 0 10 10", "refX", "10", "refY", "5",
                    "markerUnits", "strokeWidth", "markerWidth", "10", "markerHeight", "10",
                    "orient", "auto", "class", "arrowHead")[
                    T.g("path", "d", "M 0 0 L 10 5 L 0 10 z")
                ],
                T.g("marker", "id", "arrow_r2l", "viewBox", "0 0 10 10", "refX", "0", "refY", "5",
                    "markerUnits", "strokeWidth", "markerWidth", "10", "markerHeight", "10",
                    "orient", "auto", "class", "arrowHead")[
                    T.g("path", "d", "M 10 0 L 0 5 L 10 10 z")
                ]
            ],
            content.ToString()
        ];

        return svg.ToString();
    }

    private static string AddPin(Dq<string>? cmdArray, Dictionary<string, Vec2d> pins, ChartState state)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 3) throw new Exception($"Pin command too short '{cmdArray}'");

        var pinName = cmdArray.RemoveFirst();
        var xPos = int.Parse(cmdArray.RemoveFirst());
        var yPos = int.Parse(cmdArray.RemoveFirst());
        pins[pinName] = new Vec2d
        {
            X = xPos + state.Translate.X,
            Y = yPos + state.Translate.Y
        };

        return ""; // no markup
    }

    private static void AddPinInt(string pinName, int xPos, int yPos, Dictionary<string, Vec2d> pins, ChartState state)
    {
        pins[pinName] = new Vec2d
        {
            X = xPos + state.Translate.X,
            Y = yPos + state.Translate.Y
        };
    }

    private static string Translate(Dq<string>? cmdArray, ChartState state)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 2) throw new Exception($"Translate command too short: '{cmdArray}'");
        state.Translate.X = int.Parse(cmdArray.RemoveFirst());
        state.Translate.Y = int.Parse(cmdArray.RemoveFirst());
        return "";
    }

    private static string MovePin(Dq<string>? cmdArray, Dictionary<string, Vec2d> pins)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 3) throw new Exception($"Move command too short: '{cmdArray}'");

        var xOffs = int.Parse(cmdArray.RemoveFirst());
        var yOffs = int.Parse(cmdArray.RemoveFirst());

        foreach (var pinName in cmdArray)
        {
            if (!pins.ContainsKey(pinName)) throw new Exception($"Can't move unknown pin '{pinName}'");
            pins[pinName].X += xOffs;
            pins[pinName].Y += yOffs;
        }

        return ""; // no markup
    }

    private static string ProjectPin(Dq<string>? cmdArray, Dictionary<string, Vec2d> pins)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 4) throw new Exception($"Project command too short: '{cmdArray}'");

        var newPinName = cmdArray.RemoveFirst();

        var pin1 = cmdArray.RemoveFirst();
        var pin2 = cmdArray.RemoveFirst();
        var pinX = cmdArray.RemoveFirst();
        if (!pins.ContainsKey(pin1)) throw new Exception($"Project -- pin not defined '{pin1}'; '{cmdArray}'");
        if (!pins.ContainsKey(pin2)) throw new Exception($"Project -- pin not defined '{pin2}'; '{cmdArray}'");
        if (!pins.ContainsKey(pinX)) throw new Exception($"Project -- pin not defined '{pinX}'; '{cmdArray}'");

        pins[newPinName] = ProjectPoint(pins[pin1], pins[pin2], pins[pinX]);

        return ""; // no markup
    }

    private static string PinAtCorner(Dq<string>? cmdArray, Dictionary<string, Vec2d> pins)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 3) throw new Exception($"Corner command too short: '{cmdArray}'");

        var newPinName = cmdArray.RemoveFirst();
        var pin1 = cmdArray.RemoveFirst();
        var pin2 = cmdArray.RemoveFirst();

        if (!pins.ContainsKey(pin1)) throw new Exception($"Corner -- pin not defined '{pin1}'; '{cmdArray}'");
        if (!pins.ContainsKey(pin2)) throw new Exception($"Corner -- pin not defined '{pin2}'; '{cmdArray}'");
        pins[newPinName] = new Vec2d
        {
            X = pins[pin1].X,
            Y = pins[pin2].Y
        };

        return ""; // no markup
    }

    private static string ResetPinsToOrigin(Dq<string>? cmdArray, Dictionary<string, Vec2d> pins)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 1) throw new Exception($"Pin group reset has no element: '{cmdArray}'");
        var xOffs = MaxPinValue;
        var yOffs = MaxPinValue;

        // find extents
        foreach (var pinName in cmdArray)
        {
            if (!pins.ContainsKey(pinName))
            {
                throw new Exception($"Can't reset unknown pin '{pinName}'");
            }

            xOffs = Math.Min(xOffs, pins[pinName].X);
            yOffs = Math.Min(yOffs, pins[pinName].Y);
        }

        // apply offsets
        foreach (var pinName in cmdArray)
        {
            pins[pinName].X -= xOffs;
            pins[pinName].Y -= yOffs;
        }

        return ""; // no markup
    }

    private static string MoveToPin(bool useFirst, bool centre, Dq<string>? cmdArray, Dictionary<string, Vec2d> pins)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 2) throw new Exception($"Pin group move to has no elements: '{cmdArray}'");
        double xOffs;
        double yOffs;
        var xMin = MaxPinValue;
        var yMin = MaxPinValue;

        var xMax = MinPinValue;
        var yMax = MinPinValue;

        var basePinName = cmdArray.RemoveFirst();
        if (!pins.ContainsKey(basePinName))
            throw new Exception($"MoveTo -- base pin not defined '{basePinName}'; '{cmdArray}'");

        var basePin = pins[basePinName];

        // find extents
        foreach (var pinName in cmdArray)
        {
            if (!pins.ContainsKey(pinName))
            {
                throw new Exception($"Can't reset unknown pin '{pinName}'");
            }

            xMin = Math.Min(xMin, pins[pinName].X);
            yMin = Math.Min(yMin, pins[pinName].Y);
            xMax = Math.Max(xMax, pins[pinName].X);
            yMax = Math.Max(yMax, pins[pinName].Y);
        }

        if (useFirst)
        {
            var relPinName = cmdArray.RemoveFirst();
            if (!pins.ContainsKey(relPinName))
                throw new Exception($"MoveTo -- relative pin not defined '{relPinName}'; '{cmdArray}'");

            xOffs = pins[relPinName].X - basePin.X;
            yOffs = pins[relPinName].Y - basePin.Y;
        }
        else
        {
            xOffs = xMin - basePin.X;
            yOffs = yMin - basePin.Y;
        }

        if (centre)
        {
            xOffs += (xMax - xMin) / 2;
            yOffs += (yMax - yMin) / 2;
        }

        // apply offsets
        foreach (var pinName in cmdArray)
        {
            pins[pinName].X -= xOffs;
            pins[pinName].Y -= yOffs;
        }

        return ""; // no markup
    }

    private static string SplitPin(Dq<string>? cmdArray, Dictionary<string, Vec2d> pins)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 3) throw new Exception($"Offset command too short: '{cmdArray}'");

        var newPinName = cmdArray.RemoveFirst();
        var pin1 = cmdArray.RemoveFirst();
        var pin2 = cmdArray.RemoveFirst();

        if (!pins.ContainsKey(pin1)) throw new Exception($"Split -- pin not defined '{pin1}'; '{cmdArray}'");
        if (!pins.ContainsKey(pin2)) throw new Exception($"Split -- pin not defined '{pin2}'; '{cmdArray}'");
        pins[newPinName] = new Vec2d
        {
            X = (pins[pin1].X + pins[pin2].X) / 2,
            Y = (pins[pin1].Y + pins[pin2].Y) / 2
        };

        return ""; // no markup
    }

    private static string PinByOffset(Dq<string>? cmdArray, Dictionary<string, Vec2d> pins)
    {
        if (cmdArray is null) throw new Exception("Invalid command");
        if (cmdArray.Length() < 4) throw new Exception($"Offset command too short: '{cmdArray}'");

        var newPinName = cmdArray.RemoveFirst();
        var srcPin = cmdArray.RemoveFirst();
        if (!pins.ContainsKey(srcPin)) throw new Exception($"Offset -- pin not defined '{srcPin}'; '{cmdArray}'");
        var xOffs = int.Parse(cmdArray.RemoveFirst());
        var yOffs = int.Parse(cmdArray.RemoveFirst());
        pins[newPinName] = new Vec2d
        {
            X = pins[srcPin].X + xOffs,
            Y = pins[srcPin].Y + yOffs
        };

        return ""; // no markup
    }

    private static string DrawBox(bool splitCells, Dq<string>? cmdArray, Dictionary<string, Vec2d> pins,
        ChartState state, bool small)
    {
        if (cmdArray is null || cmdArray.Length() < 2) throw new Exception($"Box command too short: '{cmdArray}'");

        var cols = 1;
        if (splitCells) cols = int.Parse(cmdArray.RemoveFirst());
        if (cols < 1) throw new Exception($"Table -- table columns not valid: '{cols}'; '{cmdArray}'");

        var pin1 = cmdArray.RemoveFirst();
        var pin2 = cmdArray.RemoveFirst();
        var textSrc = Clean(string.Join(" ", cmdArray));

        var text = new Dq<string>(splitCells ? textSrc.Split('|') : new[] { textSrc });

        if (!pins.ContainsKey(pin1)) throw new Exception($"Box -- pin not defined '{pin1}'; {cmdArray}'");
        if (!pins.ContainsKey(pin2)) throw new Exception($"Box -- pin not defined '{pin2}'; {cmdArray}'");
        var p1 = pins[pin1];
        var p2 = pins[pin2];

        var top = Math.Min(p1.Y, p2.Y);
        var left = Math.Min(p1.X, p2.X);
        var right = Math.Max(p1.X, p2.X);
        var bottom = Math.Max(p1.Y, p2.Y);
        var w = right - left;
        var h = bottom - top;

        var style = state.BoxColor is null ? "" : $" style=\"fill: #{state.BoxColor}\" ";

        var result = $"<rect x=\"{left}\" y=\"{top}\" width=\"{w}\" height=\"{h}\"{style}/>";

        if (text.Length() < 1) return result;

        // This horrendous mess is the only reliable way I found to get
        // centred, wrapping text to look nice
        result +=
            $"<foreignObject x=\"{left}\" y=\"{top}\" width=\"{w}\" height=\"{h}\" transform=\"translate(0,0)\">" +
            $"<xhtml:div style=\"display: table; height: {h}px; margin: auto; padding: 0 1px 0 1px;\">";

        var txtClass = small ? "boxTextSmall" : "boxText";
        var colWidth = w / cols;
        var textLen = text.Length();
        var firstRow = cols > 1;

        // TODO: replace with T.g (see test app)
        while (text.Length() > 0)
        {
            var remCols = cols;
            result += "<xhtml:div style=\"display: table-row\">";
            while (text.Length() > 0 && remCols-- > 0)
            {
                var edge = remCols > 0 ? "border-right:thin solid rgb(0,0,0, .4); " : "";
                if (cols > 1) edge += "padding: 0 .3em 0 .3em; ";
                if (firstRow) edge += "border-bottom:thin solid rgb(0,0,0, .4); ";
                if (textLen < 2) edge = "";
                result += $"<xhtml:div style=\"display: table-cell; {edge}vertical-align: middle;\">" +
                          $"<xhtml:div style=\"color:black; text-align:center; width: {colWidth}px;\" class=\"{txtClass}\">{text.RemoveFirst()}</xhtml:div>" +
                          "</xhtml:div>";
            }

            firstRow = false;
            result += "</xhtml:div>";
        }

        result += "</xhtml:div>" +
                  "</foreignObject>";

        return result;
    }

    private static string DrawAutoBox(bool splitCells, Dq<string>? cmdArray, Dictionary<string, Vec2d> pins,
        ChartState state)
    {
        if (cmdArray is null) throw new Exception("AutoBox invalid commands");
        if (cmdArray.Length() < 5) throw new Exception($"AutoBox command too short: '{cmdArray}'");

        var cols = 1;
        if (splitCells) cols = int.Parse(cmdArray.RemoveFirst());
        if (cols < 1) throw new Exception($"AutoTable -- table columns not valid: '{cols}'; '{cmdArray}'");

        var pinBaseName = cmdArray.RemoveFirst();
        var abX = int.Parse(cmdArray.RemoveFirst());
        var abY = int.Parse(cmdArray.RemoveFirst());
        var width = int.Parse(cmdArray.RemoveFirst());
        var height = int.Parse(cmdArray.RemoveFirst());
        if (width <= 0) throw new Exception($"AutoBox -- invalid width: '{width}'; '{cmdArray}'");
        if (height <= 0) throw new Exception($"AutoBox -- invalid height: '{height}'; '{cmdArray}'");
        var halfWidth = width / 2;
        var halfHeight = height / 2;

        //var text = clean(cmdArray.join(' ')); // leave the text in place
        // create pins, rebuild the command array, then call regular box
        AddPinInt(pinBaseName + "_tl", abX, abY, pins, state);
        AddPinInt(pinBaseName + "_t", abX + halfWidth, abY, pins, state);
        AddPinInt(pinBaseName + "_tr", abX + width, abY, pins, state);

        AddPinInt(pinBaseName + "_l", abX, abY + halfHeight, pins, state);
        AddPinInt(pinBaseName + "_r", abX + width, abY + halfHeight, pins, state);

        AddPinInt(pinBaseName + "_bl", abX, abY + height, pins, state);
        AddPinInt(pinBaseName + "_b", abX + halfWidth, abY + height, pins, state);
        AddPinInt(pinBaseName + "_br", abX + width, abY + height, pins, state);

        // Box pin1 pin2 [text...]
        cmdArray.AddFirst(pinBaseName + "_br");
        cmdArray.AddFirst(pinBaseName + "_tl");
        if (splitCells) cmdArray.AddFirst(cols.ToString());
        return DrawBox(splitCells, cmdArray, pins, state, false);
    }

    private static string DrawLine(bool useArrow, bool flipText, Dq<string>? cmdArray, Dictionary<string, Vec2d> pins,
        ChartState state)
    {
        if (cmdArray is null || cmdArray.Length() < 2) throw new Exception($"Line command too short: '{cmdArray}'");

        var pin1 = cmdArray.RemoveFirst();
        var pin2 = cmdArray.RemoveFirst();
        var text = Clean(string.Join(" ", cmdArray));

        if (!pins.ContainsKey(pin1)) throw new Exception($"Pin not defined '{pin1}' in '{cmdArray}'");
        if (!pins.ContainsKey(pin2)) throw new Exception($"Pin not defined '{pin2}' in '{cmdArray}'");
        var p1 = pins[pin1];
        var p2 = pins[pin2];

        Vec2d t;

        // don't draw lines upside-down
        var rightToLeft = p1.X > p2.X;
        if (rightToLeft)
        {
            t = p1;
            p1 = p2;
            p2 = new Vec2d
            {
                X = t.X,
                Y = t.Y
            };
        }

        var textTransform = "";
        if (flipText)
        {
            // ok, force lines to be upside-down
            rightToLeft = !rightToLeft;
            t = p1;
            p1 = p2;
            p2 = new Vec2d
            {
                X = t.X,
                Y = t.Y
            };
            textTransform = "style=\"transform-origin:center;transform-box:fill-box;transform:rotateZ(180deg);\"";
        }

        var pathStyle = state.LineColor is null ? "" : $" style=\"stroke:#{state.LineColor}\" ";

        var lineId = "curve_" + state.GlobalLineNum;
        state.GlobalLineNum++;
        var dx = "";

        var anchor = "";
        if (useArrow)
        {
            dx = rightToLeft ? "dx=\"5\" " : "dx=\"-5\" ";
            anchor = rightToLeft ? " marker-start=\"url(#arrow_r2l)\" " : " marker-end=\"url(#arrow_l2r)\" ";
        }

        var result = $"<path {anchor}{pathStyle} class=\"line\" id=\"{lineId}\" d=\"M{p1.X},{p1.Y}L{p2.X},{p2.Y}\" />";

        if (text.Length > 0)
        {
            text = text.Replace("_", "&#160;"); // &nbsp; for XML
            result +=
                $"<text dy=\"-2\" {dx}{textTransform} ><textPath xlink:href=\"#{lineId}\" startOffset=\"50%\" class=\"lineText\">{text}</textPath></text>";
        }

        return result;
    }

    private static string SetFillColor(Dq<string>? cmdArray, ChartState state)
    {
        state.BoxColor = cmdArray?.Length() > 0 ? cmdArray[0] : null;
        return "";
    }

    private static string SetLineColor(Dq<string>? cmdArray, ChartState state)
    {
        state.LineColor = cmdArray?.Length() > 0 ? cmdArray[0] : null;
        return "";
    }

    private static string HandleCommand(string cmdStr, Dictionary<string, Vec2d> pins, ChartState state)
    {
        var command = new Dq<string>(cmdStr.Split(' ', '\t'));
        if (command.Length() < 1) return ""; // invalid
        var cmdName = command.RemoveFirst().ToLowerInvariant();

        // ReSharper disable StringLiteralTypo
        switch (cmdName)
        {
            case "#":
                return ""; // comment
            case "pin":
                return AddPin(command, pins, state);
            case "split":
                return SplitPin(command, pins);
            case "offset":
                return PinByOffset(command, pins);
            case "move":
                return MovePin(command, pins);
            case "moveto":
                return MoveToPin(false, false, command, pins);
            case "centre":
            case "center":
                return MoveToPin(false, true, command, pins);
            case "moveover":
                return MoveToPin(true, false, command, pins);
            case "reset":
                return ResetPinsToOrigin(command, pins);
            case "corner":
                return PinAtCorner(command, pins);
            case "autobox":
                return DrawAutoBox(false, command, pins, state);
            case "autotable":
                return DrawAutoBox(true, command, pins, state);
            case "box":
                return DrawBox(false, command, pins, state, false);
            case "smallbox":
                return DrawBox(false, command, pins, state, true);
            case "table":
                return DrawBox(true, command, pins, state, false);
            case "line":
                return DrawLine(false, false, command, pins, state);
            case "arrow":
                return DrawLine(true, false, command, pins, state);
            case "flipline":
                return DrawLine(false, true, command, pins, state);
            case "fliparrow":
                return DrawLine(true, true, command, pins, state);
            case "fill":
                return SetFillColor(command, state);
            case "stroke":
                return SetLineColor(command, state);
            case "clearfill":
                return SetFillColor(null, state);
            case "clearstroke":
                return SetLineColor(null, state);
            case "project":
                return ProjectPin(command, pins);
            case "translate":
            case "group":
                return Translate(command, state);
            default:
                throw new Exception($"Invalid command: '{cmdStr}'");
        }
        // ReSharper restore StringLiteralTypo
    }

    /// <summary>
    /// Take a diagram program as a string, and output an SVG.
    /// <p></p>
    /// Will throw an exception if the input program is malformed
    /// </summary>
    public static string Render(string program)
    {
        return RenderToSvg(
            program.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
    }


    private const string DiagramStyles = ".lineText {font: 0.5em sans-serif;} " +
                                         ".boxText {font: 0.75em sans-serif; display:table-cell;} " +
                                         ".boxTextSmall {font: 0.25em sans-serif; display:table-cell;} " +
                                         "textPath {text-anchor: middle;} " +
                                         ".line {fill: none; stroke-width: 1px; stroke: #888;} " +
                                         ".arrowHead {fill: #888;} " +
                                         "rect {fill: #eef; stroke-width: 1px; stroke: #888;} ";
}

internal class ChartState
{
    public string? BoxColor;
    public string? LineColor;
    public Vec2d Translate = new();
    public int GlobalLineNum = 1;
}

/// <summary>
/// 2D vector of doubles
/// </summary>
internal class Vec2d
{
    public double X { get; set; }
    public double Y { get; set; }
}