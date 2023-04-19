using DraigCore;
using DraigCore.DataStructures;
using Tag;

namespace TestApp;

internal class Program
{
    public static void Main()
    {
        if (File.Exists(@"C:\temp\example.ddw"))
        {
            Console.WriteLine("Generating SVG...");
            ConvertToSvg(from: @"C:\temp\example.ddw", to: @"C:\temp\example_ddw.svg.html");
        }
        else
        {
            Console.WriteLine("Run again to convert to SVG");
            Console.WriteLine("Generating sample file...");
            GenerateExample(outFile: @"C:\temp\example.ddw");
        }
        Console.WriteLine("...done");
    }

    private static void GenerateExample(string outFile)
    {
        var module = new Module();
        
        // TODO: build out a structure
        
        var storageData = module.Serialise();
        File.WriteAllText(outFile, storageData);
    }

    private static void ConvertToSvg(string from, string to)
    {
        // TODO: move this stuff over to the core
        var module = Module.FromSerial(File.ReadAllText(from));
        
        var svgDoc = SvgHeader(width: 500, height: 600, out var svgRoot);
        
        var backLayer = T.g();
        var midLayer = T.g();
        var foreLayer = T.g();
        

        foreLayer.Add(SimpleLine(80, 80, 120, 120));
        foreLayer.Add(CrookLine(80,80,  100,90,  120,120));
        
        backLayer.Add(PagingControl(10, 10, "page-id", true));
        backLayer.Add(CommitMessage(50, 10, "style-1", "Hello, world"));
        
        midLayer.Add(AnnotationLine(50, 50, 150, "", "Line 1"));
        midLayer.Add(TextBoxCentred(35, 20, 80, 80, 1, "Hello"));
        midLayer.Add(TextBoxCentred(35, 120, 150, 80, 3,
            "H1",   "H2",   "H3",
            "r1c1", "r1c2", "r1c3",
            "r2c1", "r2c2"));
        
        
        svgRoot.Add(backLayer);
        svgRoot.Add(midLayer);
        svgRoot.Add(foreLayer);
        var htmlWrapper = T.g("html")[
            T.g("head")[
                T.g("title")["Test SVG/HTML document"],
                T.g("style")[LoadFile("Styles/PageStyle.css")]
            ],
            T.g("body")[
                T.g("script")[LoadFile("Scripts/PageScript.js")],
                svgDoc
            ]
        ];
        File.WriteAllText(to, htmlWrapper.ToString());
    }

    /// <summary>
    /// Draw a rectangle, with centred text.
    /// If more than one 'cell' is given, the text is arranged into a table.
    /// </summary>
    /// <param name="left">x position of top-left corner</param>
    /// <param name="top">y position of top-left corner</param>
    /// <param name="width">width of outer box. Text is wrapped to fit</param>
    /// <param name="height">height of outer box</param>
    /// <param name="columns">Number of columns in table</param>
    /// <param name="cells">Table cell text content</param>
    /// <returns>SVG fragment</returns>
    private static TagContent TextBoxCentred(int left, int top, int width, int height, int columns, params string[] cells)
    {
        var boxStyle = "fill: #eee";
        var result = T.g()[
            T.g("rect",  "x",$"{left}", "y", $"{top}", "width", $"{width}", "height", $"{height}", "style",boxStyle)
        ];
        
        if (cells.Length < 1) return result; // no text to display
        
        var tableContent = T.g();
        if (columns < 1) columns = (int)Math.Sqrt(cells.Length);
        var remainingCols = columns;
        var row = T.g("xhtml:div", "style","display:table-row");
        var colWidth = width / columns;
        var firstRow = true;

        foreach (var cell in cells)
        {
            // check if we need new row
            if (remainingCols < 1) // new row
            {
                tableContent.Add(row);
                row = T.g("xhtml:div", "style","display:table-row");
                remainingCols = columns;
                firstRow = false;
            }
            remainingCols--;
            
            // style cell borders
            var edge = "";
            if (cells.Length > 1)
            {
                if (remainingCols > 0) edge += "border-right:thin solid rgb(0,0,0, .4); ";
                if (columns > 1) edge += "padding: 0 .3em 0 .3em; ";
                if (firstRow) edge += "border-bottom:thin solid rgb(0,0,0, .4); ";
            }

            // add cell to row
            row.Add(T.g("xhtml:div", "style",$"display: table-cell;{edge} vertical-align: middle;")[
                T.g("xhtml:div", "style",$"color:black; text-align:center; width: {colWidth}px;", "class","boxText")[
                    cell
                ]
            ]);
        }
        tableContent.Add(row);

        result.Add(T.g("foreignObject", "x", $"{left}", "y", $"{top}", "width", $"{width}", "height", $"{height}", "transform", "translate(0,0)")[
            T.g("xhtml:div", "style", $"display: table; height: {height}px; margin: auto; padding: 0 1px 0 1px;")[
                tableContent
            ]
        ]);

        return result;
    }

    private static TagContent AnnotationLine(int xLeft, int y, int xRight, string textClass, string text)
    {
        return T.g("g", "class", "tag", "transform", "translate(" + xLeft + "," + y + ")")[
            T.g("text", "x", "-40", "y", "3", "text-anchor", "end", "class", textClass)[text],
            T.g("path", "marker-end", "url(#dot)", "d", "M-35,0L" + xRight + ",0", "style", "opacity:1;")
        ];
    }
    
    private static TagContent SimpleLine(int x1, int y1, int x2, int y2) {
        return T.g("g", "class","link")[
            T.g("path", "d",
                "M"+x1+","+y1+
                "L"+x2+","+y2)
        ];
    }

    private static TagContent CrookLine(int x1, int y1, int x2, int y2, int x3, int y3) {
        return T.g("g", "class","link")[
            T.g("path", "d",
                "M"+x1+","+y1+
                "L"+x2+","+y2+
                "L"+x3+","+y3)
        ];
    }
    
    private static TagContent CommitMessage(int x, int y, string styleClass, string text)
    {
        return T.g("g", "class", "tag " + styleClass, "transform", "translate(" + x + "," + y + ")")[
            T.g("text", "x", "20", "y", "3", "text-anchor", "start")[text]
        ];
    }
    
    private static TagContent PagingControl(int x, int y, string id, bool down)
    {
        string transform;
        if (down) transform = "translate(" + (x - 6) + "," + (y + 6) + ") scale(1,-1)";
        else transform = "translate(" + (x - 6) + "," + (y - 6) + ")";

        return T.g("g", "class", "paging", "transform", transform)[
            T.g("circle", "id", id,
                "style","fill:#fff;fill-opacity:1;stroke:none;",
                "cx", "6", "cy", "6", "r", "7")[
                T.g("title")["Page Up (more recent)"]
            ],
            T.g("path/", 
                "id", id,
                "style", "fill:#000;fill-opacity:1;stroke:none;",
                "d", "M 6,0 A 6,6 0 0 0 0,6 6,6 0 0 0 6,12 6,6 0 0 0 12,6 6,6 0 0 0 6,0 Z M 6,1.25 11,5 H 8 V 11 H 4 V 5 H 1.25 Z")
        ];
    }
    
    private static TagContent SvgHeader(int width, int height, out TagContent rootElement){
        rootElement = T.g("g", "transform","translate(20,20)")[
            T.g("defs")[
                T.g("style")[LoadFile("Styles/SvgStyle.css")]
            ],
            T.g("defs")[
                T.g("marker", "id","dot", "viewBox","-10 -10 20 20", "refX","0", "refY","0",
                    "markerUnits","strokeWidth", "markerWidth","10", "markerHeight","10",
                    "orient","auto", "style","fill:#333")[
                    T.g("circle", "cx","0", "cy","0", "r","3")
                ],
                T.g("marker", "id", "arrow_l2r", "viewBox", "0 0 10 10", "refX", "10", "refY", "5", 
                    "markerUnits", "strokeWidth", "markerWidth", "10", "markerHeight", "10",
                    "orient", "auto", "class", "arrowHead")[
                    T.g("path", "d","M 0 0 L 10 5 L 0 10 z")
                ],
                T.g("marker", "id", "arrow_r2l", "viewBox", "0 0 10 10", "refX", "0", "refY", "5", 
                    "markerUnits", "strokeWidth", "markerWidth", "10", "markerHeight", "10",
                    "orient", "auto", "class", "arrowHead")[
                    T.g("path", "d","M 10 0 L 0 5 L 10 10 z")
                ],
                T.g("script")[LoadFile("Scripts/SvgEmbeddedScript.js")]
            ]
        ];

        return T.g("svg", "id", "svgroot", "width", width + "px", "height", height + "px",  "onload","inject()")[rootElement];
    }
    
    private static string LoadFile(string path) => File.ReadAllText(path);
}