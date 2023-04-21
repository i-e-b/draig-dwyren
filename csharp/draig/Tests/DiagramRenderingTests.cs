using DraigCore.Rendering;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class DiagramRenderingTests
{
    [Test]
    public void can_render_a_QnD_program_into_an_SVG_1()
    {
        var svg = DiagramToSvg.Render(Diagram1);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_1.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult1)));
    }
    
    [Test]
    public void can_render_a_QnD_program_into_an_SVG_2()
    {
        var svg = DiagramToSvg.Render(Diagram2);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_2.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult2)));
    }
    
    [Test]
    public void can_render_a_QnD_program_into_an_SVG_3()
    {
        var svg = DiagramToSvg.Render(Diagram3);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_3.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult3)));
    }
    
    [Test]
    public void can_render_a_QnD_program_into_an_SVG_4()
    {
        var svg = DiagramToSvg.Render(Diagram4);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_4.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult4)));
    }
    
    [Test]
    public void can_render_a_QnD_program_into_an_SVG_5()
    {
        var svg = DiagramToSvg.Render(Diagram5);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_5.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult5)));
    }

    /// <summary>
    /// Remove spaces and newlines from a string
    /// </summary>
    private static string Reduce(string src) => src.Replace("\r","").Replace("\n", "").Replace(" ", ""); // TODO: improve


    private const string Diagram1 = @"
fill ffa

Pin a1 0 0
offset a2 a1 100 100
Box a1 a2 hello,\nworld!
split a3 a1 a2
split x1 a3 a2
corner x1 a2 x1

clearfill

Pin b1 200 0
offset b2 b1 100 100
Box b1 b2
offset b3 b1 0 100
split x2 b1 b3

fliparrow x2 x1 flopped
move 0 20 x1 x2
fliparrow x1 x2 flipped
move 0 -30 x1 x2
arrow x1 x2 forwards
move 0 -20 x1 x2
arrow x2 x1 backwards
";
    private const string ExpectedResult1 = @"
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 320 120""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs>
<defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker>
<marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker>
<marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""100"" style=""fill: #ffa"" /><foreignObject x=""0"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 100px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">hello,<xhtml:br/>world!</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""200"" y=""0"" width=""100"" height=""100""/><foreignObject x=""200"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 100px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText""></xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path  marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_1"" d=""M200,50L100,75"" /><text dy=""-2"" dx=""-5"" style=""transform-origin:center;transform-box:fill-box;transform:rotateZ(180deg);"" ><textPath xlink:href=""#curve_1"" startOffset=""50%"" class=""lineText"">flopped</textPath></text>
<path  marker-start=""url(#arrow_r2l)""  class=""line"" id=""curve_2"" d=""M200,70L100,95"" /><text dy=""-2"" dx=""5"" style=""transform-origin:center;transform-box:fill-box;transform:rotateZ(180deg);"" ><textPath xlink:href=""#curve_2"" startOffset=""50%"" class=""lineText"">flipped</textPath></text>
<path  marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_3"" d=""M100,65L200,40"" /><text dy=""-2"" dx=""-5""  ><textPath xlink:href=""#curve_3"" startOffset=""50%"" class=""lineText"">forwards</textPath></text>
<path  marker-start=""url(#arrow_r2l)""  class=""line"" id=""curve_4"" d=""M100,45L200,20"" /><text dy=""-2"" dx=""5""  ><textPath xlink:href=""#curve_4"" startOffset=""50%"" class=""lineText"">backwards</textPath></text>
</svg>
";
    
        private const string Diagram2 = @"
 # Any element with the marker class
 # and an id should be rendered
 
 Pin tl 0 0
 Offset tr tl 100 0
 Offset br tr 0 100
 Box tl br Start
 Split ax tr br
 
 Pin b 200 0
 Offset r b 100 100
 Box b r End
 Corner bx b r
 Split bx b bx
 
 Stroke 0f0
 Arrow ax bx Our Process
 ClearStroke
 
 # Using 'project' to split up a line
 Project cb br bx ax
 Project ct ax b bx
 Line cb ct Not_______Us
 Line br cb
 Arrow ct b
";
    private const string ExpectedResult2 = @"
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 320 120""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""100""/><foreignObject x=""0"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 100px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">Start</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""200"" y=""0"" width=""100"" height=""100""/><foreignObject x=""200"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 100px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">End</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path marker-end=""url(#arrow_l2r)""  style=""stroke:#0f0""  class=""line"" id=""curve_1"" d=""M100,50L200,50"" /><text dy=""-2"" dx=""-5""  ><textPath xlink:href=""#curve_1"" startOffset=""50%"" class=""lineText"">Our Process</textPath></text>
<path class=""line"" id=""curve_2"" d=""M120,90L180,10"" /><text dy=""-2""  ><textPath xlink:href=""#curve_2"" startOffset=""50%"" class=""lineText"">Not&#160;&#160;&#160;&#160;&#160;&#160;&#160;Us</textPath></text>
<path class=""line"" id=""curve_3"" d=""M100,100L120,90"" />
<path marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_4"" d=""M180,10L200,0"" />
</svg>
";
    
        private const string Diagram3 = @"
 # Example of moving a template around
 
 # Define the box
 pin a_tl 0 0
 pin a_cl 0 25
 pin a_br 100 50
 pin a_cr 100 25
 
 # Move it around
 move 0 0 a_tl a_br a_cl a_cr
 box a_tl a_br Steal underpants
 offset x1 a_cr 0 0
 
 move 150 0 a_tl a_br a_cl a_cr
 box a_tl a_br ?
 offset x2 a_cl 0 0
 offset x3 a_cr 0 0
 
 move 150 0 a_tl a_br a_cl a_cr
 box a_tl a_br Profit!
 offset x4 a_cl 0 0
 offset x5 a_cr 0 0
 corner x6 a_tl a_br
 split x6 x6 a_br
 
 # Set our template back to 0,0 and move again
 reset a_tl a_br a_cl a_cr
 move 0 80 a_tl a_br a_cl a_cr
 smallbox a_tl a_br Metasystem parallel to project
 
 arrow x1 x2
 arrow x3 x4
 
 # Fancy arrow
 corner x7 x6 a_cr
 flipline a_cr x7 (back leakage)
 arrow x7 x6
 
 # Move a box over a known point
 pin mt1 0 0
 pin mt2 10 20
 centre x7 mt1 mt2
 box mt1 mt2 $
";
    private const string ExpectedResult3 = @"
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 420 150""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""50""/><foreignObject x=""0"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">Steal underpants</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""150"" y=""0"" width=""100"" height=""50""/><foreignObject x=""150"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">?</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""300"" y=""0"" width=""100"" height=""50""/><foreignObject x=""300"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">Profit!</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""0"" y=""80"" width=""100"" height=""50""/><foreignObject x=""0"" y=""80"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxTextSmall"">Metasystem parallel to project</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_1"" d=""M100,25L150,25"" />
<path marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_2"" d=""M250,25L300,25"" />
<path class=""line"" id=""curve_3"" d=""M350,105L100,105"" /><text dy=""-2"" style=""transform-origin:center;transform-box:fill-box;transform:rotateZ(180deg);"" ><textPath xlink:href=""#curve_3"" startOffset=""50%"" class=""lineText"">(back leakage)</textPath></text>
<path marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_4"" d=""M350,105L350,50"" />
<rect x=""345"" y=""95"" width=""10"" height=""20""/><foreignObject x=""345"" y=""95"" width=""10"" height=""20"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 20px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 10px;"" class=""boxText"">$</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
</svg>
";
    
    
        private const string Diagram4 = @"
 # Same as above, but using AutoBox
 
 # Draw boxes
 autobox pants 0 0 100 50 Steal underpants
 autobox quest 150 0 100 50 ?
 autobox prof 300 0 100 50 Profit!
 fill eee
 autobox crit 0 80 75 50 Many critical components
 
 # Draw lines
 corner c_crit quest_b crit_r
 arrow pants_r quest_l
 arrow quest_r prof_l
 line crit_r c_crit Note: look into this
 arrow c_crit quest_b
 
 # auto-table
 fill ffa
 Translate 250 80
 AutoTable 3 tabl 0 0 150 80 H1|H2|H3 | R1C1|R1C2|R1C3 | R2C1|R2C2|R2C3 | R3D2
";
    private const string ExpectedResult4 = @"
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 420 180""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""50""/><foreignObject x=""0"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">Steal underpants</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""150"" y=""0"" width=""100"" height=""50""/><foreignObject x=""150"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">?</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""300"" y=""0"" width=""100"" height=""50""/><foreignObject x=""300"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">Profit!</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""0"" y=""80"" width=""75"" height=""50"" style=""fill: #eee"" /><foreignObject x=""0"" y=""80"" width=""75"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 50px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 75px;"" class=""boxText"">Many critical components</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path  marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_1"" d=""M100,25L150,25"" />
<path  marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_2"" d=""M250,25L300,25"" />
<path  class=""line"" id=""curve_3"" d=""M75,105L200,105"" /><text dy=""-2""  ><textPath xlink:href=""#curve_3"" startOffset=""50%"" class=""lineText"">Note: look into this</textPath></text>
<path  marker-end=""url(#arrow_l2r)""  class=""line"" id=""curve_4"" d=""M200,105L200,50"" />
<rect x=""250"" y=""80"" width=""150"" height=""80"" style=""fill: #ffa"" /><foreignObject x=""250"" y=""80"" width=""150"" height=""80"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 80px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4); vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4); vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4); vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R1C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R2C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R3D2</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
</svg>
";
    
        private const string Diagram5 = @"
  Group 0 0 -- Overlapping boxes --
  Pin tl 0 0
  Pin br 100 70
  Split m tl br
  
  Box tl br One
  MoveOver br m tl br
  Box tl br Two
  
  Group 0 110 -- Box table --
  Pin tl 0 0
  Pin br 150 110
  Fill aea
  # Note that the table doesn't have to be complete
  Table 3 tl br H1|H2|H3 | R1C1|R1C2|R1C3 | R2C1|R2C2|R2C3 | R3D2
";
    private const string ExpectedResult5 = @"
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 170 240""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""70""/><foreignObject x=""0"" y=""0"" width=""100"" height=""70"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 70px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">One</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""50"" y=""35"" width=""100"" height=""70""/><foreignObject x=""50"" y=""35"" width=""100"" height=""70"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 70px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 100px;"" class=""boxText"">Two</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""0"" y=""110"" width=""150"" height=""110"" style=""fill: #aea"" /><foreignObject x=""0"" y=""110"" width=""150"" height=""110"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 110px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4); vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4); vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4); vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R1C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R2C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell; padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display: table-row""><xhtml:div style=""display: table-cell; border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R3D2</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
</svg>
";
}