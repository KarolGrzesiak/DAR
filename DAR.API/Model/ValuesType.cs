using System.Collections.Generic;
using DAR.API.Model.Enums;

namespace DAR.API.Model
{
    public class ValuesType
    {
        public IEnumerable<Constraint> Constraints { get; set; }
        public ValuesTypeName Name { get; set; }

    }
}