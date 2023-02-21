using System;
using System.IO;
using System.Xml;
namespace TinCan.SVG.Merge{
    class Program
    {
    
        const uint PROGRAM_VER_MAJOR = 1;
        const uint PROGRAM_VER_MINOR = 0;
        const uint COPYRIGHT_YEAR = 2023;

        private static void PrintInfo()
        {
            Console.WriteLine($"tin-can.ca SVG merge (tc-svg-merge) version {PROGRAM_VER_MAJOR}.{PROGRAM_VER_MINOR}");
            Console.WriteLine($"Copyright (c) {COPYRIGHT_YEAR} tomatoes from tin-can.ca");
            Console.WriteLine();
        }
        private static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine($"Usage: tin-can.ca <destination> <original> [[overlay1] [overlay2] ...]");
            Console.WriteLine();
        }
        public static int Main (string[] args)
        {
            if(args.Length < 2)
            {
                PrintInfo();
                Console.WriteLine("Missing required arguments");
                PrintUsage();
                return -2;
            }
            for(int i = 1; i< args.Length; ++i)
            {
                if(!File.Exists(args[i])){
                    PrintInfo();
                    Console.WriteLine($"File \"{args[i]}\"der does not exist");
                    return -1;
                }
            }

            string destFile = args[0];
            string bottomFile = args[1];
            
            try{

                // Load the source file / lowest layer
                XmlDocument document = new XmlDocument();
                document.Load(bottomFile);
                XmlNode? destSvgNode = null;

                // Find the <svg> node
                foreach(XmlNode n in document.ChildNodes){
                    if(n.Name=="svg")
                    {
                        destSvgNode = n;
                        break;
                    }
                }
                if(destSvgNode is null){
                    throw new ApplicationException("Unable to find an <svg> node in the source document.");
                }


                // Loop over remaining documents
                for(int i = 2; i< args.Length; ++i )
                {
                    XmlDocument overlayDoc = new XmlDocument();
                    overlayDoc.Load(args[i]);
                    // Find SVG node
                    foreach(XmlNode onode in overlayDoc.ChildNodes)
                    {
                        if(onode.Name=="svg")
                        {
                            // Copy all the children of the SVG node
                            // over to the document in memory
                            foreach(XmlNode on in onode.ChildNodes)
                            {
                                destSvgNode.AppendChild(document.ImportNode(on,true));
                            }
                        }
                    }
                }

                // Write the new document to a file
                using XmlWriter w = XmlWriter.Create(destFile);
                document.WriteContentTo(w);

            } catch (Exception ex){
                PrintInfo();
                Console.WriteLine("Error: ");
                Console.WriteLine(ex.Message);
                return -1;
            }
            return 0;

        }
    }

}
