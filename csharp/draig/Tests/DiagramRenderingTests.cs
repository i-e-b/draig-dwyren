using DraigCore.Rendering;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class DiagramRenderingTests
{
    [Test]
    public void can_render_a_QnD_program_into_an_SVG()
    {
        var svg = DiagramToSvg.Render(Diagram1);
        
        Assert.That(svg, Is.Not.Empty);
        
        File.WriteAllText(@"C:\temp\QaD_1.svg", svg);
        
        Assert.That(Reduce(svg), Is.EqualTo(Reduce(ExpectedResult1)));
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
}