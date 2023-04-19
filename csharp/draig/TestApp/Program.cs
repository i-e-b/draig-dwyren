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
        
        var content = T.g();
        content.Add(PagingControl(10, 10, "page-id", true));
        content.Add(CommitMessage(50, 10, "style-1", "Hello, world"));
        
        
        svgRoot.Add(content);
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
                T.g("marker", "id","dot", "viewbox","-10 -10 20 20", "refX","0", "refY","0", "markerUnits","strokeWidth", "markerWidth","10", "markerHeight","10", "orient","auto", "style","fill:#333")[
                    T.g("circle", "cx","0", "cy","0", "r","3")
                ],
                T.g("script")[LoadFile("Scripts/SvgEmbeddedScript.js")]
            ]
        ];

        return T.g("svg", "id", "svgroot", "width", width + "px", "height", height + "px",  "onload","inject()")[rootElement];
    }
    
    private static string LoadFile(string path) => File.ReadAllText(path);
}