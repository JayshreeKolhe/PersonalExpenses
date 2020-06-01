using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenses.classes
{
    public class graph
    {
        public ObservableCollection<TotalAmount> graphdData{ get; set; }
        
        public graph()
        {
            graphdData = App._DatesTotalAmount;
        }
    }
}
