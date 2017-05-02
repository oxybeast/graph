using System.Collections.Generic;

namespace GraphSolver.UnitTests
{
    public class CompareSomething
    {
        public int CompareEdges(Edge edge1, Edge edge2)
        {
            if ((edge1.From < edge2.From) || (edge1.From == edge2.From && edge1.To < edge2.To) ||
                (edge1.From == edge2.From && edge1.To == edge2.To && edge1.Id < edge2.Id))
                return -1;
            if (edge1.From == edge2.From && edge1.To == edge2.To && edge1.Id == edge2.Id)
                return 0;
            return 1;
        }

        public int CompareListEdges(List<Edge> List1, List<Edge> List2)
        {
            for (var i = 0; i < List1.Count && i < List2.Count; ++i)
            {
                var k = CompareEdges(List1[i], List2[i]);
                if (k != 0)
                    return k;
            }
            if (List1.Count == List2.Count)
                return 0;
            if (List1.Count < List2.Count)
                return -1;
            return 1;
        }
        public int ComparePaths(Path path1, Path path2)
        {
            for (var i = 0; i < path1.Length() && i < path2.Length(); ++i)
            {
                var k = CompareEdges(path1.OnePath[i], path2.OnePath[i]);
                if (k != 0)
                    return k;
            }
            if (path1.Length() == path2.Length())
                return 0;
            if (path1.Length() < path2.Length())
                return -1;
            else
                return 1;
        }

        public int CompareListPaths(List<Path> listPaths1, List<Path> listPaths2)
        {           
            for (var i = 0; i < listPaths1.Count && i < listPaths2.Count; ++i)
            {
                var k = ComparePaths(listPaths1[i], listPaths2[i]);
                if (k != 0)
                    return k;
            }
            if (listPaths1.Count == listPaths2.Count)
                return 0;
            if (listPaths1.Count < listPaths2.Count)
                return -1;
            //if (listPaths1.Count > listPaths2.Count)
                return 1;
        }
    }
}
