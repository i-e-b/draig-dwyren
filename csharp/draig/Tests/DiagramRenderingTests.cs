using System.Diagnostics;
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
    
    [Test]
    public void can_render_a_QnD_program_into_an_SVG_6()
    {
        var svg = DiagramToSvg.Render(Diagram6);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_6.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult6)));
    }
    
    [Test]
    public void can_render_a_QnD_program_into_an_SVG_7()
    {
        var svg = DiagramToSvg.Render(Diagram7);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_7.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult7)));
    }

    [Test]
    public void speed_test()
    {
        var sw = new Stopwatch();
        sw.Start();
        var count = 250;
        for (int i = 0; i < count; i++)
        {
            DiagramToSvg.Render(Diagram4);
        }
        sw.Stop();
        var time = sw.Elapsed.TotalMilliseconds / count;
        Console.WriteLine($"Render took average of {time}ms");
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
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 320 120""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""100"" style=""fill: #ffa""/><foreignObject x=""0"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:100px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">hello,<xhtml:br/>world!</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""200"" y=""0"" width=""100"" height=""100""/><foreignObject x=""200"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:100px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText""></xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_1"" d=""M200,50L100,75""/><text dy=""-2"" dx=""-5"" style=""transform-origin:center;transform-box:fill-box;transform:rotateZ(180deg);""><textPath xlink:href=""#curve_1"" startOffset=""50%"" class=""lineText"">flopped</textPath></text>
<path marker-start=""url(#arrow_r2l)"" class=""line"" id=""curve_2"" d=""M200,70L100,95""/><text dy=""-2"" dx=""5"" style=""transform-origin:center;transform-box:fill-box;transform:rotateZ(180deg);""><textPath xlink:href=""#curve_2"" startOffset=""50%"" class=""lineText"">flipped</textPath></text>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_3"" d=""M100,65L200,40""/><text dy=""-2"" dx=""-5""><textPath xlink:href=""#curve_3"" startOffset=""50%"" class=""lineText"">forwards</textPath></text>
<path marker-start=""url(#arrow_r2l)"" class=""line"" id=""curve_4"" d=""M100,45L200,20""/><text dy=""-2"" dx=""5""><textPath xlink:href=""#curve_4"" startOffset=""50%"" class=""lineText"">backwards</textPath></text>
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
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 320 120""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""100""/><foreignObject x=""0"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:100px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Start</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""200"" y=""0"" width=""100"" height=""100""/><foreignObject x=""200"" y=""0"" width=""100"" height=""100"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:100px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">End</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path marker-end=""url(#arrow_l2r)"" style=""stroke:#0f0"" class=""line"" id=""curve_1"" d=""M100,50L200,50""/><text dy=""-2"" dx=""-5""><textPath xlink:href=""#curve_1"" startOffset=""50%"" class=""lineText"">Our Process</textPath></text>
<path class=""line"" id=""curve_2"" d=""M120,90L180,10""/><text dy=""-2""><textPath xlink:href=""#curve_2"" startOffset=""50%"" class=""lineText"">Not&#160;&#160;&#160;&#160;&#160;&#160;&#160;Us</textPath></text>
<path class=""line"" id=""curve_3"" d=""M100,100L120,90""/>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_4"" d=""M180,10L200,0""/>
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
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 420 150""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""50""/><foreignObject x=""0"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Steal underpants</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""150"" y=""0"" width=""100"" height=""50""/><foreignObject x=""150"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">?</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""300"" y=""0"" width=""100"" height=""50""/><foreignObject x=""300"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Profit!</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""0"" y=""80"" width=""100"" height=""50""/><foreignObject x=""0"" y=""80"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxTextSmall"">Metasystem parallel to project</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_1"" d=""M100,25L150,25""/>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_2"" d=""M250,25L300,25""/>
<path class=""line"" id=""curve_3"" d=""M350,105L100,105""/><text dy=""-2"" style=""transform-origin:center;transform-box:fill-box;transform:rotateZ(180deg);""><textPath xlink:href=""#curve_3"" startOffset=""50%"" class=""lineText"">(back leakage)</textPath></text>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_4"" d=""M350,105L350,50""/>
<rect x=""345"" y=""95"" width=""10"" height=""20""/><foreignObject x=""345"" y=""95"" width=""10"" height=""20"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:20px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">$</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
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
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 420 180""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""50""/><foreignObject x=""0"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Steal underpants</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""150"" y=""0"" width=""100"" height=""50""/><foreignObject x=""150"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">?</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""300"" y=""0"" width=""100"" height=""50""/><foreignObject x=""300"" y=""0"" width=""100"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Profit!</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""0"" y=""80"" width=""75"" height=""50"" style=""fill: #eee""/><foreignObject x=""0"" y=""80"" width=""75"" height=""50"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:50px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Many critical components</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_1"" d=""M100,25L150,25""/>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_2"" d=""M250,25L300,25""/>
<path class=""line"" id=""curve_3"" d=""M75,105L200,105""/><text dy=""-2""><textPath xlink:href=""#curve_3"" startOffset=""50%"" class=""lineText"">Note: look into this</textPath></text>
<path marker-end=""url(#arrow_l2r)"" class=""line"" id=""curve_4"" d=""M200,105L200,50""/>
<rect x=""250"" y=""80"" width=""150"" height=""80"" style=""fill: #ffa""/><foreignObject x=""250"" y=""80"" width=""150"" height=""80"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 80px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4);  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4);  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4);  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R1C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R2C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R3D2</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
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
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 170 240""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""70""/><foreignObject x=""0"" y=""0"" width=""100"" height=""70"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:70px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">One</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""50"" y=""35"" width=""100"" height=""70""/><foreignObject x=""50"" y=""35"" width=""100"" height=""70"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:70px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Two</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<rect x=""0"" y=""110"" width=""150"" height=""110"" style=""fill: #aea""/><foreignObject x=""0"" y=""110"" width=""150"" height=""110"" transform=""translate(0,0)""><xhtml:div style=""display: table; height: 110px; margin: auto; padding: 0 1px 0 1px;""><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4);  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4);  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;padding: 0 .3em 0 .3em; border-bottom:thin solid rgb(0,0,0, .4);  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">H3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R1C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R1C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R2C1</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C2</xhtml:div></xhtml:div><xhtml:div style=""display: table-cell;padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText"">R2C3 </xhtml:div></xhtml:div></xhtml:div><xhtml:div style=""display:table-row""><xhtml:div style=""display: table-cell;border-right:thin solid rgb(0,0,0, .4); padding: 0 .3em 0 .3em;  vertical-align: middle;""><xhtml:div style=""color:black; text-align:center; width: 50px;"" class=""boxText""> R3D2</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
</svg>
";
    
    private const string Diagram6 = @"
  # Box shapes
  pin far 280 0
  pin tl 0 0
  pin br 100 40
  Box tl br Plain box
  
  move 170 0 tl br
  Pill tl br Box with\nround sides
  
  move -170 70 tl br
  Hex tl br Hex box\n(question)
  
  move 170 0 tl br
  BoxOut down tl br Box\narrow down
  
  move -170 70 tl br
  BoxOut up tl br Box\narrow up
  
  move 170 0 tl br
  BoxOut left tl br Box arrow left
  
  move -170 70 tl br
  BoxOut right tl br Box\narrow right
  
  move 170 0 tl br
  BoxIn left tl br Arrow in left
  
  move -170 70 tl br
  BoxIn right tl br Arrow in right
  
  move 170 0 tl br
  BoxIn bottom tl br Arrow in bottom
  
  move -170 70 tl br
  BoxIn top tl br Arrow in top
  
  move 170 0 tl br
  TiltBox 8 tl br Tilted box +ve
  
  move -170 70 tl br
  TiltBox -4 tl br Tilted box -ve
  
  move 170 0 tl br
  TrapBox 8 -4 tl br Trapezoid\nwide top
  
  move -170 70 tl br
  TrapBox -5 5 tl br Trapezoid\nnarrow top
";
    private const string ExpectedResult6 = @"
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 300 550""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""100"" height=""40""/><foreignObject x=""0"" y=""0"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Plain box</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 190 0 l 60 0 a 20 20 0 0 1 0 40 l -60 0 a 20 20 0 0 1 0 -40 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""0"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Box with<xhtml:br/>round sides</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M20 70 l60 0 l20 20 l-20 20 l-60 0 l -20 -20 l 20 -20 Z"" stroke=""#888"" fill=""#eef""></path><foreignObject x=""0"" y=""70"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Hex box<xhtml:br/>(question)</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 170 70 l 100 0 l 0 40 l -43.333333333333336 0 l 0 4 l 13.333333333333332 0 l -20 20 l -20 -20 l 13.333333333333332 0 l 0 -4 l -43.333333333333336 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""70"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Box<xhtml:br/>arrow down</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 0 140 l 43.333333333333336 0 l 0 -4 l -13.333333333333332 0 l 20 -20 l 20 20 l -13.333333333333332 0 l 0 4 l 43.333333333333336 0 l 0 40 l -100 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""0"" y=""140"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Box<xhtml:br/>arrow up</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 170 140 l 100 0 l 0 40 l -100 0 l 0 -13.333333333333332 l -4 0 l 0 13.333333333333332 l -20 -20 l 20 -20 l 0 13.333333333333332 l 4 0 l 0 -13.333333333333332 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""140"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Box arrow left</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 0 210 l 100 0 l 0 13.333333333333332 l 4 0 l 0 -13.333333333333332 l 20 20 l -20 20 l 0 -13.333333333333332 l -4 0 l 0 13.333333333333332 l -100 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""0"" y=""210"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Box<xhtml:br/>arrow right</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 170 210 l 100 0 l 0 40 l -100 0 l 0 -16 l -16 16 l 0 -16 l -5 0 l 0 -4 l 0 -4 l 5 0 l 0 -16 l 16 16 l 0 -16 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""210"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Arrow in left</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 0 280 l 100 0 l 0 16 l 16 -16 l 0 16 l 5 0 l 0 4 l 0 4 l -5 0 l 0 16 l -16 -16 l 0 16 l -100 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""0"" y=""280"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Arrow in right</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 170 280 l 100 0 l 0 40 l -46 0 l 16 16 l -16 0 l 0 5 l -4 0 l -4 0 l 0 -5 l -16 0 l 16 -16 l -46 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""280"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Arrow in bottom</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 0 350 l 46 0 l -16 -16 l 16 0 l 0 -5 l 4 0 l 4 0 l 0 5 l 16 0 l -16 16 l 46 0 l 0 40 l -100 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""0"" y=""350"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Arrow in top</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M178 350 l100 0 l -16 40 l -100 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""350"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Tilted box +ve</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M-4 420 l100 0 l 8 40 l -100 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""0"" y=""420"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Tilted box -ve</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M162 420 l 116 0 l -12 40 l -92 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""420"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Trapezoid<xhtml:br/>wide top</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M5 490 l 90 0 l 10 40 l -110 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""0"" y=""490"" width=""100"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Trapezoid<xhtml:br/>narrow top</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
</svg>
";
    
    private const string Diagram7 = @"
  # Box auto shapes
  AutoBox plain 0 0 120 40 Plain auto-box
  
  AutoPill pill1 170 0 120 40 Auto-box with\nround sides
  
  Line plain_r pill1_l
  
  AutoHex hex1 0 80 120 40 Hex auto-box\n(question)
  Split hex1ret plain_b hex1_t
  Offset hex1yes hex1_r 20 0
  Corner hex1retc hex1yes hex1ret
  
  Line plain_b hex1_t
  Line hex1_r hex1yes Yes
  Line hex1yes hex1retc
  Arrow hex1retc hex1ret Try again
  
  AutoTrapBox 8 -8 trap1 0 150 120 40 Trap box top
  Line hex1_b trap1_t No
  
  AutoTiltBox 8 tilt1 170 80 120 40 Tilted box +ve
  
  Corner c tilt1_b trap1_r
  Line tilt1_b c
  Line trap1_r c
  
  
  AutoBoxOut right arr1 80 220 120 40 ABO-r
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
  
  
  AutoBoxOut left arr1 80 290 120 40 ABO-l
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
  
  
  AutoBoxOut top arr1 80 390 120 40 ABO-t
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
  
  
  AutoBoxOut down arr1 80 470 120 40 ABO-b
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
  
  AutoBoxIn left arr1 80 560 120 40 ABI-l
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
  
  
  AutoBoxIn right arr1 80 640 120 40 ABI-r
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
  
  
  AutoBoxIn up arr1 80 730 120 40 ABI-u
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
  
  
  AutoBoxIn down arr1 80 820 120 40 ABI-d
  Offset t arr1_r 10 0
  Line arr1_r t
  Offset t arr1_l -10 0
  Line arr1_l t
  Offset t arr1_tl -7 -7
  Line arr1_tl t
  Offset t arr1_bl -7 7
  Line arr1_bl t
  Offset t arr1_tr 7 -7
  Line arr1_tr t
  Offset t arr1_br 7 7
  Line arr1_br t
  Offset t arr1_t 0 -10
  Line arr1_t t
  Offset t arr1_b 0 10
  Line arr1_b t
";
    private const string ExpectedResult7 = @"
<svg xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""-10 -10 310 911""><defs><style type=""text/css""><![CDATA[.lineText {font: 0.5em sans-serif;} .boxText {font: 0.75em sans-serif; display:table-cell;} .boxTextSmall {font: 0.25em sans-serif; display:table-cell;} textPath {text-anchor: middle;} .line {fill: none; stroke-width: 1px; stroke: #888;} .arrowHead {fill: #888;} rect {fill: #eef; stroke-width: 1px; stroke: #888;} ]]></style></defs><defs><marker id=""dot"" viewBox=""-10 -10 20 20"" refX=""0"" refY=""0"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" style=""fill:#333""><circle cx=""0"" cy=""0"" r=""3""></circle></marker><marker id=""arrow_l2r"" viewBox=""0 0 10 10"" refX=""10"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 0 0 L 10 5 L 0 10 z""></path></marker><marker id=""arrow_r2l"" viewBox=""0 0 10 10"" refX=""0"" refY=""5"" markerUnits=""strokeWidth"" markerWidth=""10"" markerHeight=""10"" orient=""auto"" class=""arrowHead""><path d=""M 10 0 L 0 5 L 10 10 z""></path></marker></defs><rect x=""0"" y=""0"" width=""120"" height=""40""/><foreignObject x=""0"" y=""0"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Plain auto-box</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path d=""M 190 0 l 80 0 a 20 20 0 0 1 0 40 l -80 0 a 20 20 0 0 1 0 -40 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""0"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Auto-box with<xhtml:br/>round sides</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_1"" d=""M120,20L170,20""/>
<path d=""M20 80 l80 0 l20 20 l-20 20 l-80 0 l -20 -20 l 20 -20 Z"" stroke=""#888"" fill=""#eef""></path><foreignObject x=""0"" y=""80"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Hex auto-box<xhtml:br/>(question)</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_2"" d=""M60,40L60,80""/>
<path class=""line"" id=""curve_3"" d=""M120,100L140,100""/><text dy=""-2""><textPath xlink:href=""#curve_3"" startOffset=""50%"" class=""lineText"">Yes</textPath></text>
<path class=""line"" id=""curve_4"" d=""M140,100L140,60""/>
<path marker-start=""url(#arrow_r2l)"" class=""line"" id=""curve_5"" d=""M60,60L140,60""/><text dy=""-2"" dx=""5""><textPath xlink:href=""#curve_5"" startOffset=""50%"" class=""lineText"">Try again</textPath></text>
<path d=""M-8 150 l 136 0 l -16 40 l -104 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""0"" y=""150"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Trap box top</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_6"" d=""M60,120L60,150""/><text dy=""-2""><textPath xlink:href=""#curve_6"" startOffset=""50%"" class=""lineText"">No</textPath></text>
<path d=""M178 80 l120 0 l -16 40 l -120 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""170"" y=""80"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">Tilted box +ve</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_7"" d=""M230,120L230,170""/>
<path class=""line"" id=""curve_8"" d=""M120,170L230,170""/>
<path d=""M 80 220 l 120 0 l 0 13.333333333333332 l 4 0 l 0 -13.333333333333332 l 20 20 l -20 20 l 0 -13.333333333333332 l -4 0 l 0 13.333333333333332 l -120 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""220"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABO-r</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_9"" d=""M224,240L234,240""/>
<path class=""line"" id=""curve_10"" d=""M70,240L80,240""/>
<path class=""line"" id=""curve_11"" d=""M73,213L80,220""/>
<path class=""line"" id=""curve_12"" d=""M73,267L80,260""/>
<path class=""line"" id=""curve_13"" d=""M200,220L207,213""/>
<path class=""line"" id=""curve_14"" d=""M200,260L207,267""/>
<path class=""line"" id=""curve_15"" d=""M140,220L140,210""/>
<path class=""line"" id=""curve_16"" d=""M140,260L140,270""/>
<path d=""M 80 290 l 120 0 l 0 40 l -120 0 l 0 -13.333333333333332 l -4 0 l 0 13.333333333333332 l -20 -20 l 20 -20 l 0 13.333333333333332 l 4 0 l 0 -13.333333333333332 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""290"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABO-l</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_17"" d=""M200,310L210,310""/>
<path class=""line"" id=""curve_18"" d=""M46,310L56,310""/>
<path class=""line"" id=""curve_19"" d=""M73,283L80,290""/>
<path class=""line"" id=""curve_20"" d=""M73,337L80,330""/>
<path class=""line"" id=""curve_21"" d=""M200,290L207,283""/>
<path class=""line"" id=""curve_22"" d=""M200,330L207,337""/>
<path class=""line"" id=""curve_23"" d=""M140,290L140,280""/>
<path class=""line"" id=""curve_24"" d=""M140,330L140,340""/>
<path d=""M 80 390 l 53.333333333333336 0 l 0 -4 l -13.333333333333332 0 l 20 -20 l 20 20 l -13.333333333333332 0 l 0 4 l 53.333333333333336 0 l 0 40 l -120 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""390"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABO-t</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_25"" d=""M200,410L210,410""/>
<path class=""line"" id=""curve_26"" d=""M70,410L80,410""/>
<path class=""line"" id=""curve_27"" d=""M73,383L80,390""/>
<path class=""line"" id=""curve_28"" d=""M73,437L80,430""/>
<path class=""line"" id=""curve_29"" d=""M200,390L207,383""/>
<path class=""line"" id=""curve_30"" d=""M200,430L207,437""/>
<path class=""line"" id=""curve_31"" d=""M140,366L140,356""/>
<path class=""line"" id=""curve_32"" d=""M140,430L140,440""/>
<path d=""M 80 470 l 120 0 l 0 40 l -53.333333333333336 0 l 0 4 l 13.333333333333332 0 l -20 20 l -20 -20 l 13.333333333333332 0 l 0 -4 l -53.333333333333336 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""470"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABO-b</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_33"" d=""M200,490L210,490""/>
<path class=""line"" id=""curve_34"" d=""M70,490L80,490""/>
<path class=""line"" id=""curve_35"" d=""M73,463L80,470""/>
<path class=""line"" id=""curve_36"" d=""M73,517L80,510""/>
<path class=""line"" id=""curve_37"" d=""M200,470L207,463""/>
<path class=""line"" id=""curve_38"" d=""M200,510L207,517""/>
<path class=""line"" id=""curve_39"" d=""M140,470L140,460""/>
<path class=""line"" id=""curve_40"" d=""M140,534L140,544""/>
<path d=""M 80 560 l 120 0 l 0 40 l -120 0 l 0 -16 l -16 16 l 0 -16 l -5 0 l 0 -4 l 0 -4 l 5 0 l 0 -16 l 16 16 l 0 -16 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""560"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABI-l</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_41"" d=""M200,580L210,580""/>
<path class=""line"" id=""curve_42"" d=""M49,580L59,580""/>
<path class=""line"" id=""curve_43"" d=""M73,553L80,560""/>
<path class=""line"" id=""curve_44"" d=""M73,607L80,600""/>
<path class=""line"" id=""curve_45"" d=""M200,560L207,553""/>
<path class=""line"" id=""curve_46"" d=""M200,600L207,607""/>
<path class=""line"" id=""curve_47"" d=""M140,560L140,550""/>
<path class=""line"" id=""curve_48"" d=""M140,600L140,610""/>
<path d=""M 80 640 l 120 0 l 0 16 l 16 -16 l 0 16 l 5 0 l 0 4 l 0 4 l -5 0 l 0 16 l -16 -16 l 0 16 l -120 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""640"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABI-r</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_49"" d=""M221,660L231,660""/>
<path class=""line"" id=""curve_50"" d=""M70,660L80,660""/>
<path class=""line"" id=""curve_51"" d=""M73,633L80,640""/>
<path class=""line"" id=""curve_52"" d=""M73,687L80,680""/>
<path class=""line"" id=""curve_53"" d=""M200,640L207,633""/>
<path class=""line"" id=""curve_54"" d=""M200,680L207,687""/>
<path class=""line"" id=""curve_55"" d=""M140,640L140,630""/>
<path class=""line"" id=""curve_56"" d=""M140,680L140,690""/>
<path d=""M 80 730 l 56 0 l -16 -16 l 16 0 l 0 -5 l 4 0 l 4 0 l 0 5 l 16 0 l -16 16 l 56 0 l 0 40 l -120 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""730"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABI-u</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_57"" d=""M200,750L210,750""/>
<path class=""line"" id=""curve_58"" d=""M70,750L80,750""/>
<path class=""line"" id=""curve_59"" d=""M73,723L80,730""/>
<path class=""line"" id=""curve_60"" d=""M73,777L80,770""/>
<path class=""line"" id=""curve_61"" d=""M200,730L207,723""/>
<path class=""line"" id=""curve_62"" d=""M200,770L207,777""/>
<path class=""line"" id=""curve_63"" d=""M140,709L140,699""/>
<path class=""line"" id=""curve_64"" d=""M140,770L140,780""/>
<path d=""M 80 820 l 120 0 l 0 40 l -56 0 l 16 16 l -16 0 l 0 5 l -4 0 l -4 0 l 0 -5 l -16 0 l 16 -16 l -56 0 Z"" stroke=""#888"" fill=""#eef""/><foreignObject x=""80"" y=""820"" width=""120"" height=""40"" transform=""translate(0,0)""><xhtml:div style=""display:table;height:40px;margin:auto;padding:0 1px 0 1px""><xhtml:div style=""display:table-row""><xhtml:div style=""display:table-cell;vertical-align:middle""><xhtml:div style=""color:black; text-align:center;width:100%"" class=""boxText"">ABI-d</xhtml:div></xhtml:div></xhtml:div></xhtml:div></foreignObject>
<path class=""line"" id=""curve_65"" d=""M200,840L210,840""/>
<path class=""line"" id=""curve_66"" d=""M70,840L80,840""/>
<path class=""line"" id=""curve_67"" d=""M73,813L80,820""/>
<path class=""line"" id=""curve_68"" d=""M73,867L80,860""/>
<path class=""line"" id=""curve_69"" d=""M200,820L207,813""/>
<path class=""line"" id=""curve_70"" d=""M200,860L207,867""/>
<path class=""line"" id=""curve_71"" d=""M140,820L140,810""/>
<path class=""line"" id=""curve_72"" d=""M140,881L140,891""/>
</svg>
";
}