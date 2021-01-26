    using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FiscalPrime.Backend.Shared
{
    public static class ObjectUtils
    {
        public static List<Variance> DetailedCompare<T>(this T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            PropertyInfo[] fi = val1.GetType().GetProperties();
            foreach (PropertyInfo f in fi)
            {
                Variance v = new Variance();
                v.Prop = f.Name;
                v.valA = f.GetValue(val1);
                v.valB = f.GetValue(val2);

                if ((v.valA is null && !(v.valB is null)) || (!(v.valA is null) && v.valB is null))
                    variances.Add(v);
                else if (v.valA is null && v.valB is null)
                    continue;
                else if (!v.valA.Equals(v.valB))
                    variances.Add(v);

            }
            return variances;
        }
    }

    public class Variance
    {
        public string Prop { get; set; }
        public object valA { get; set; }
        public object valB { get; set; }
    }
}
