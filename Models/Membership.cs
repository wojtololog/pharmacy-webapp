using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Membership
    {
        public List<Section> sections;

        public Membership(double min, double max)
        {
            sections = new List<Section>();
            double length = (max - min) / 4;

            for (int i = 0; i < 5; ++i)
            {
                Section s = new Section();
                s.a = min + i * length - 0.75 * length;
                s.b = min + i * length - 0.25 * length;
                s.c = min + i * length + 0.25 * length;
                s.d = min + i * length + 0.75 * length;
                sections.Add(s);
            }
        }

        public List<double> CalculateSectionsMembership(double x)
        {
            List<double> memberships = new List<double>();

            foreach (Section s in sections)
            {
                if (x <= s.a)
                {
                    memberships.Add(0);
                }
                else if (x > s.a && x <= s.b)
                {
                    memberships.Add((x - s.a) / (s.b - s.a));
                }
                else if (x > s.b && x <= s.c)
                {
                    memberships.Add(1);
                }
                else if (x > s.c && x <= s.d)
                {
                    memberships.Add((s.d - x) / (s.d - s.c));
                }
                else if (x > s.d)
                {
                    memberships.Add(0);
                }
            }

            return memberships;
        }
    }
}