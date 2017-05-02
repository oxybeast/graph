using System.Collections.Generic;

namespace GraphSolver
{
    public class VertexOfTable
    {
        private string _name;
        private List<string> _columns;

        public VertexOfTable(string name, List<string> columns)
        {
            _name = name;
            _columns = new List<string>(columns);
        }

        public string GetName()
        {
            return _name;
        }
        public List<string> GetColumns()
        {
            return new List<string>(_columns);
        }
    }
    
}
