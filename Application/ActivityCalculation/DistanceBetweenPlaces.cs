using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ActivityCalculation
{
    public static class ActicityDisp
    {
        public static double DistanceBetweenPlaces(string lon1, string lat1, string lon2, string lat2)
        {
            double R = 6371; // km

            double sLat1 = Math.Sin(Radians(Convert.ToDouble(lat1)));
            double sLat2 = Math.Sin(Radians(Convert.ToDouble(lat2)));
            double cLat1 = Math.Cos(Radians(Convert.ToDouble(lat1)));
            double cLat2 = Math.Cos(Radians(Convert.ToDouble(lat2)));
            double cLon = Math.Cos(Radians(Convert.ToDouble(lon1)) - Radians(Convert.ToDouble(lon2)));

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = R * d;

            return dist;
        }
        public static double Radians(double degree)
        {
            return degree * Math.PI / 180;
        }

    }
}
