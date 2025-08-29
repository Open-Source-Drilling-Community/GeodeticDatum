using OSDC.DotnetLibraries.General.Common;
using SMath = System.Math;
using NMath = OSDC.DotnetLibraries.General.Math;
using OSDC.DotnetLibraries.General.Octree;

namespace NORCE.Drilling.GeodeticDatum.Model
{
    public class GeodeticCoordinate
    {
        /// <summary>
        /// the Latitude in the WGS84 datum
        /// </summary>
        public double? LatitudeWGS84 { get; set; }
        /// <summary>
        /// the Longitude in the WGS84 datum
        /// </summary>
        public double? LongitudeWGS84 { get; set; }
        /// <summary>
        /// the vertical depth in the WGS84 datum
        /// </summary>
        public double? VerticalDepthWGS84 { get; set; }
        /// <summary>
        /// the Latitude in the reference datum
        /// </summary>
        public double? LatitudeDatum { get; set; }
        /// <summary>
        /// the Longitude in the reference datum
        /// </summary>
        public double? LongitudeDatum { get; set; }
        /// <summary>
        /// the vertical depth in the reference datum
        /// </summary>
        public double? VerticalDepthDatum { get; set; }
        /// <summary>
        /// the octree depth level
        /// </summary>
        public byte OctreeDepth { get; set; }
        /// <summary>
        /// the octree code
        /// </summary>
        public OctreeCodeLong? OctreeCode { get; set; }

        /// <summary>
        /// default constructor required for JSON serialization
        /// </summary>
        public GeodeticCoordinate() : base()
        {
        }

        #region computations on geodetic datums
        /// <summary>
        /// Computes the geodetic coordinates relative to the global geodetic datum (WGS84), from the geodetic coordinates expressed in the given datum
        /// WARNING: elevation and TVD are opposite quantities to eachother
        /// <param name="datum">the geodetic datum the geodetic coordinates to convert from refer to</param>
        /// <result>the geodetic coordinates relative to the global geodetic datum (WGS84)</result>
        /// </summary>
        public void ToFundamental(GeodeticDatum? datum)
        {
            LatitudeWGS84 = null;
            LongitudeWGS84 = null;
            VerticalDepthWGS84 = null;

            if (datum != null && datum.Spheroid != null && LatitudeDatum != null && LongitudeDatum != null && VerticalDepthDatum != null)
            {
                // Global geodetic datum (WGS84) - deltaX=0, deltaY=0, deltaZ=0, rotationX=0, rotationY=0, rotationZ=0, scaleFactor=1
                if (datum.Spheroid.SemiMajorAxis != null &&
                    datum.Spheroid.SemiMinorAxis != null &&
                    Numeric.EQ(datum.Spheroid.SemiMajorAxis.ScalarValue, Spheroid.WGS84!.SemiMajorAxis!.ScalarValue) && 
                    Numeric.EQ(datum.Spheroid.SemiMinorAxis.ScalarValue, Spheroid.WGS84!.SemiMinorAxis!.ScalarValue) &&
                    datum.DeltaX != null && datum.DeltaY != null && datum.DeltaZ != null && datum.RotationX != null && datum.RotationY != null && datum.RotationZ != null && datum.ScaleFactor != null &&
                    Numeric.EQ(datum.DeltaX.ScalarValue, 0) && Numeric.EQ(datum.DeltaY.ScalarValue, 0) && Numeric.EQ(datum.DeltaZ.ScalarValue, 0) &&
                    Numeric.EQ(datum.RotationX.ScalarValue, 0) && Numeric.EQ(datum.RotationY.ScalarValue, 0) && Numeric.EQ(datum.RotationZ.ScalarValue, 0) && Numeric.EQ(datum.ScaleFactor.ScalarValue, 1))
                {
                    LatitudeWGS84 = LatitudeDatum;
                    LongitudeWGS84 = LongitudeDatum;
                    VerticalDepthWGS84 = VerticalDepthDatum;
                }
                // Using Molodensky transformation - deltaX, deltaY, deltaZ are defined, one of the remaining is not defined
                else if (datum.DeltaX != null && datum.DeltaY != null && datum.DeltaZ != null &&
                         Numeric.IsDefined(datum.DeltaX.ScalarValue) && Numeric.IsDefined(datum.DeltaY.ScalarValue) && Numeric.IsDefined(datum.DeltaZ.ScalarValue) &&
                         (datum.RotationX == null || datum.RotationY == null || datum.RotationZ == null || datum.ScaleFactor == null ||
                          Numeric.IsUndefined(datum.RotationX.ScalarValue) ||
                          Numeric.IsUndefined(datum.RotationY.ScalarValue) ||
                          Numeric.IsUndefined(datum.RotationZ.ScalarValue) ||
                          Numeric.IsUndefined(datum.ScaleFactor.ScalarValue)))
                {
                    MolodenskyShift(datum.Spheroid, Spheroid.WGS84, (double)LatitudeDatum, (double)LongitudeDatum, -(double)VerticalDepthDatum, datum.DeltaX.ScalarValue!.Value, datum.DeltaY.ScalarValue!.Value, datum.DeltaZ.ScalarValue!.Value, out double latitudeOut, out double longitudeOut, out double elevationOut);
                    LatitudeWGS84 = latitudeOut;
                    LongitudeWGS84 = longitudeOut;
                    VerticalDepthWGS84 = -elevationOut;
                }
                // Using Helmer transformation - deltaX, deltaY, deltaZ, rotationX, rotationY, rotationZ, scaleFactor are all defined
                else if (datum.DeltaX != null && datum.DeltaY != null && datum.DeltaZ != null && datum.RotationX != null && datum.RotationY != null && datum.RotationZ != null && datum.ScaleFactor != null &&
                         Numeric.IsDefined(datum.DeltaX.ScalarValue) && Numeric.IsDefined(datum.DeltaY.ScalarValue) && Numeric.IsDefined(datum.DeltaZ.ScalarValue) &&
                         Numeric.IsDefined(datum.RotationX.ScalarValue) && Numeric.IsDefined(datum.RotationY.ScalarValue) && Numeric.IsDefined(datum.RotationZ.ScalarValue) &&
                         Numeric.IsDefined(datum.ScaleFactor.ScalarValue))
                {
                    ToCartesian(datum.Spheroid, (double)LatitudeDatum, (double)LongitudeDatum, -(double)VerticalDepthDatum, out double x, out double y, out double z);
                    double dx = 0;
                    double dy = 0;
                    double dz = 0;
                    bool solution;
                    do
                    {
                        double dx1 = dx;
                        double dy1 = dy;
                        double dz1 = dz;
                        double tmpX = x - dx1;
                        double tmpY = y - dy1;
                        double tmpZ = z - dz1;
                        double scaleFactor = datum.ScaleFactor.ScalarValue!.Value;
                        double rotX = datum.RotationX.ScalarValue!.Value;
                        double rotY = datum.RotationY.ScalarValue!.Value;
                        double rotZ = datum.RotationZ.ScalarValue!.Value;
                        dx = scaleFactor * tmpX - rotZ * tmpY + rotY * tmpZ;
                        dy = rotZ * tmpX + scaleFactor * tmpY - rotX * tmpZ;
                        dz = rotX * tmpY - rotY * tmpX + scaleFactor * tmpZ;
                        solution = Numeric.EQ(dx, dx1, 0.001) && Numeric.EQ(dy, dy1, 0.001) && Numeric.EQ(dz, dz1, 0.001);
                    } while (!solution);
                    x -= dx;
                    y -= dy;
                    z -= dz;
                    FromCartesian(Spheroid.WGS84, x, y, z, out double latitudeOut, out double longitudeOut, out double elevationOut);
                    LatitudeWGS84 = latitudeOut;
                    LongitudeWGS84 = longitudeOut;
                    VerticalDepthWGS84 = -elevationOut;
                }
            }
        }

        /// <summary>
        /// Computes the geodetic coordinates relative to the reference geodetic datum , from the given geodetic coordinates relative to the global geodetic datum (WGS84)
        /// WARNING: elevation and TVD are opposite quantities to eachother
        /// </summary>
        /// <param name="datum">the geodetic datum the geodetic coordinates to convert to refer to</param>
        /// <param name="latitudeWGS84">the input geodetic latitude relative to the global geodetic datum (WGS84)</param>
        /// <param name="longitudeWGS84">the input geodetic longitude relative to the global geodetic datum (WGS84)</param>
        /// <param name="elevationWGS84">the input geodetic elevation relative to the global geodetic datum (WGS84)</param>
        /// <param name="latitudeDatum">the output geodetic latitude relative to the reference geodetic datum </param>
        /// <param name="longitudeDatum">the output geodetic longitude relative to the reference geodetic datum </param>
        /// <param name="elevationDatum">the output geodetic elevation relative to the reference geodetic datum </param>
        public static void FromFundamental(GeodeticDatum? datum, double latitudeWGS84, double longitudeWGS84, double elevationWGS84,
                                  out double latitudeDatum, out double longitudeDatum, out double elevationDatum)
        {
            latitudeDatum = Numeric.UNDEF_DOUBLE;
            longitudeDatum = Numeric.UNDEF_DOUBLE;
            elevationDatum = Numeric.UNDEF_DOUBLE;
            if (datum != null && datum.Spheroid != null)
            {
                // Global geodetic datum (WGS84) - deltaX=0, deltaY=0, deltaZ=0, rotationX=0, rotationY=0, rotationZ=0, scaleFactor=1
                if (datum.Spheroid.SemiMajorAxis != null &&
                    datum.Spheroid.SemiMinorAxis != null &&
                    Numeric.EQ(datum.Spheroid.SemiMajorAxis.ScalarValue, Spheroid.WGS84!.SemiMajorAxis!.ScalarValue) && 
                    Numeric.EQ(datum.Spheroid.SemiMinorAxis.ScalarValue, Spheroid.WGS84!.SemiMinorAxis!.ScalarValue) &&
                    datum.DeltaX != null && datum.DeltaY != null && datum.DeltaZ != null && datum.RotationX != null && datum.RotationY != null && datum.RotationZ != null && datum.ScaleFactor != null &&
                    Numeric.EQ(datum.DeltaX.ScalarValue, 0) && Numeric.EQ(datum.DeltaY.ScalarValue, 0) && Numeric.EQ(datum.DeltaZ.ScalarValue, 0) &&
                    Numeric.EQ(datum.RotationX.ScalarValue, 0) && Numeric.EQ(datum.RotationY.ScalarValue, 0) && Numeric.EQ(datum.RotationZ.ScalarValue, 0) && Numeric.EQ(datum.ScaleFactor.ScalarValue, 1))
                {
                    latitudeDatum = latitudeWGS84;
                    longitudeDatum = longitudeWGS84;
                    elevationDatum = elevationWGS84;
                }
                // Using Molodensky transformation - deltaX, deltaY, deltaZ are defined, one of the remaining is not defined
                else if (datum.DeltaX != null && datum.DeltaY != null && datum.DeltaZ != null && 
                         Numeric.IsDefined(datum.DeltaX.ScalarValue) && Numeric.IsDefined(datum.DeltaY.ScalarValue) && Numeric.IsDefined(datum.DeltaZ.ScalarValue) &&
                         (datum.RotationX == null || datum.RotationY == null || datum.RotationZ == null || datum.ScaleFactor == null ||
                          Numeric.IsUndefined(datum.RotationX.ScalarValue) || Numeric.IsUndefined(datum.RotationY.ScalarValue) || Numeric.IsUndefined(datum.RotationZ.ScalarValue) ||
                          Numeric.IsUndefined(datum.ScaleFactor.ScalarValue)))
                {
                    MolodenskyShift(Spheroid.WGS84, datum.Spheroid,
                                    latitudeWGS84, longitudeWGS84, elevationWGS84,
                                    -datum.DeltaX.ScalarValue!.Value, -datum.DeltaY.ScalarValue!.Value, -datum.DeltaZ.ScalarValue!.Value,
                                    out latitudeDatum, out longitudeDatum, out elevationDatum);
                }
                // Using Helmer transformation - deltaX, deltaY, deltaZ, rotationX, rotationY, rotationZ, scaleFactor are all defined
                else if (datum.DeltaX != null && datum.DeltaY != null && datum.DeltaZ != null && datum.RotationX != null && datum.RotationY != null && datum.RotationZ != null && datum.ScaleFactor != null && 
                         Numeric.IsDefined(datum.DeltaX.ScalarValue) && Numeric.IsDefined(datum.DeltaY.ScalarValue) && Numeric.IsDefined(datum.DeltaZ.ScalarValue) &&
                         Numeric.IsDefined(datum.RotationX.ScalarValue) && Numeric.IsDefined(datum.RotationY.ScalarValue) && Numeric.IsDefined(datum.RotationZ.ScalarValue) &&
                         Numeric.IsDefined(datum.ScaleFactor.ScalarValue))
                {
                    ToCartesian(Spheroid.WGS84, latitudeWGS84, longitudeWGS84, elevationWGS84, out double x, out double y, out double z);
                    double deltaX = datum.DeltaX.ScalarValue!.Value;
                    double deltaY = datum.DeltaY.ScalarValue!.Value;
                    double deltaZ = datum.DeltaZ.ScalarValue!.Value;
                    double scaleFactor = datum.ScaleFactor.ScalarValue!.Value;
                    double rotX = datum.RotationX.ScalarValue!.Value;
                    double rotY = datum.RotationY.ScalarValue!.Value;
                    double rotZ = datum.RotationZ.ScalarValue!.Value;
                    double dx = deltaX + scaleFactor * x - rotZ * y + rotY * z;
                    double dy = deltaY + rotZ * x + scaleFactor * y - rotX * z;
                    double dz = deltaZ - rotY * x + rotX * y + scaleFactor * z;
                    x += dx;
                    y += dy;
                    z += dz;
                    FromCartesian(datum.Spheroid, x, y, z, out latitudeDatum, out longitudeDatum, out elevationDatum);
                }
            }
        }

        /// <summary>
        /// Converts the geodetic coordinates relative to the reference geodetic datum , into geocentric coordinates relative to the same datum
        /// </summary>
        /// <param name="datum">the geodetic datum the geodetic coordinates and geocentric coordinates refer to</param>
        /// <param name="geodeticLatitude">the input geodetic latitude relative to the reference geodetic datum </param>
        /// <param name="geodeticLongitude">the input geodetic longitude relative to the reference geodetic datum </param>
        /// <param name="geodeticElevation">the input geodetic elevation relative to the reference geodetic datum </param>
        /// <param name="geocentricLatitude">the output geocentric latitude relative to the reference geodetic datum </param>
        /// <param name="geocentricLongitude">the output geocentric longitude relative to the reference geodetic datum </param>
        /// <param name="geocentricRadius">the output geocentric radius relative to the reference geodetic datum </param>
        public void ToGeocentric(GeodeticDatum? datum, double geodeticLatitude, double geodeticLongitude, double geodeticElevation,
                                  out double geocentricLatitude, out double geocentricLongitude, out double geocentricRadius)
        {
            VerticalDepthDatum = -geodeticElevation;
            ToFundamental(datum);
            geodeticElevation = -(double)VerticalDepthWGS84!;
            geocentricLongitude = geodeticLongitude;
            double a2 = Spheroid.WGS84!.SemiMajorAxis!.ScalarValue!.Value * Spheroid.WGS84!.SemiMajorAxis!.ScalarValue!.Value;
            double b2 = Spheroid.WGS84!.SemiMinorAxis!.ScalarValue!.Value * Spheroid.WGS84!.SemiMinorAxis!.ScalarValue!.Value;
            double clat = SMath.Cos(geodeticLatitude);
            double slat = SMath.Sin(geodeticLatitude);
            double aa = a2 * clat * clat;
            double bb = b2 * slat * slat;
            double cc = aa + bb;
            double dd = SMath.Sqrt(cc);
            geocentricRadius = SMath.Sqrt(geodeticElevation * (geodeticElevation + 2.0 * dd) + (a2 * aa + b2 * bb) / cc);
            double cd = (geodeticElevation + dd) / geocentricRadius;
            double sd = (a2 - b2) / dd * slat * clat / geocentricRadius;
            aa = slat * cd - clat * sd;
            bb = clat * cd + slat * sd;
            geocentricLatitude = SMath.Atan2(aa, bb);
        }

        /// <summary>
        /// Converts the geocentric coordinates relative to the reference geodetic datum , into gedetic coordinates relative to the same datum
        /// </summary>
        /// <param name="datum">the geodetic datum the geodetic coordinates and geocentric coordinates refer to</param>
        /// <param name="geocentricLatitude">the input geodetic latitude relative to the reference geodetic datum </param>
        /// <param name="geocentricLongitude">the input geodetic longitude relative to the reference geodetic datum </param>
        /// <param name="geocentricRadius">the output geocentric radius relative to the reference geodetic datum </param>
        /// <param name="geodeticLatitude">the output geocentric latitude relative to the reference geodetic datum </param>
        /// <param name="geodeticLongitude">the output geocentric longitude relative to the reference geodetic datum </param>
        /// <param name="geodeticElevation">the input geodetic elevation relative to the reference geodetic datum </param>
        public static void FromGeocentric(GeodeticDatum? datum, double geocentricLatitude, double geocentricLongitude, double geocentricRadius,
                                  out double geodeticLatitude, out double geodeticLongitude, out double geodeticElevation)
        {
            geodeticLatitude = Numeric.UNDEF_DOUBLE;
            geodeticLongitude = Numeric.UNDEF_DOUBLE;
            geodeticElevation = Numeric.UNDEF_DOUBLE;
            double sinLat = SMath.Sin(geocentricLatitude);
            double cosLat = SMath.Cos(geocentricLatitude);
            double sinLon = SMath.Sin(geocentricLongitude);
            double cosLon = SMath.Cos(geocentricLongitude);
            double x0 = geocentricRadius * sinLon * cosLat;
            double y0 = geocentricRadius * cosLon * cosLat;
            double z0 = geocentricRadius * sinLat;
            double a = Spheroid.WGS84!.SemiMajorAxis!.ScalarValue!.Value;
            double b = Spheroid.WGS84!.SemiMinorAxis!.ScalarValue!.Value;
            NMath.QuarticPolynom eq = new();
            eq[4] = 1.0 / (a * a * b * b);
            eq[3] = -2.0 * (1.0 / (a * a) + 1.0 / (b * b));
            eq[2] = 4.0 + (a * a) / (b * b) + (x0 * x0) / (b * b) - (y0 * y0) / (b * b) - (z0 * z0) / (a * a);
            eq[1] = 2.0 * (x0 * x0 + y0 * y0 + z0 * z0 - a * a - b * b);
            eq[0] = a * a * b * b - x0 * x0 * b * b - y0 * y0 * b * b - z0 * z0 * a * a;
            NMath.Complex r1 = new();
            NMath.Complex r2 = new();
            NMath.Complex r3 = new();
            NMath.Complex r4 = new();
            _ = eq.FindRoots(ref r1, ref r2, ref r3, ref r4);
            double d2 = Numeric.MAX_DOUBLE;
            double z2 = Numeric.UNDEF_DOUBLE;
            double alpha = Numeric.UNDEF_DOUBLE;
            if (!r1.IsUndefined() && Numeric.EQ(r1.Imaginary, 0))
            {
                double x = x0 * a * a / (a * a - r1.Real);
                double y = y0 * a * a / (a * a - r1.Real);
                double z = z0 * b * b / (b * b - r1.Real);
                double v2 = (x - x0) * (x - x0) + (y - y0) * (y - y0) + (z - z0) * (z - z0);
                if (Numeric.LT(v2, d2))
                {
                    z2 = z;
                    d2 = v2;
                    alpha = r1.Real;
                }
            }
            if (!r2.IsUndefined() && Numeric.EQ(r2.Imaginary, 0))
            {
                double x = x0 * a * a / (a * a - r2.Real);
                double y = y0 * a * a / (a * a - r2.Real);
                double z = z0 * b * b / (b * b - r2.Real);
                double v2 = (x - x0) * (x - x0) + (y - y0) * (y - y0) + (z - z0) * (z - z0);
                if (Numeric.LT(v2, d2))
                {
                    z2 = z;
                    d2 = v2;
                    alpha = r2.Real;
                }
            }
            if (!r3.IsUndefined() && Numeric.EQ(r3.Imaginary, 0))
            {
                double x = x0 * a * a / (a * a - r3.Real);
                double y = y0 * a * a / (a * a - r3.Real);
                double z = z0 * b * b / (b * b - r3.Real);
                double v2 = (x - x0) * (x - x0) + (y - y0) * (y - y0) + (z - z0) * (z - z0);
                if (Numeric.LT(v2, d2))
                {
                    z2 = z;
                    d2 = v2;
                    alpha = r3.Real;
                }
            }
            if (!r4.IsUndefined() && Numeric.EQ(r4.Imaginary, 0))
            {
                double x = x0 * a * a / (a * a - r4.Real);
                double y = y0 * a * a / (a * a - r4.Real);
                double z = z0 * b * b / (b * b - r4.Real);
                double v2 = (x - x0) * (x - x0) + (y - y0) * (y - y0) + (z - z0) * (z - z0);
                if (Numeric.LT(v2, d2))
                {
                    z2 = z;
                    d2 = v2;
                    alpha = r4.Real;
                }
            }
            if (Numeric.EQ(d2, 0, 0.000001))
            {
                FromGeocentric(datum, geocentricLatitude, geocentricLongitude, geocentricRadius + 1, out geodeticLatitude, out geodeticLongitude, out geodeticElevation);
                geodeticElevation -= 1;
            }
            else if (Numeric.IsDefined(alpha))
            {
                geodeticElevation = SMath.Sqrt(d2);
                geocentricLatitude = SMath.Asin((z0 - z2) / geodeticElevation);
                FromFundamental(datum, geocentricLatitude, geocentricLongitude, geodeticElevation, out geodeticLatitude, out geodeticLongitude, out geodeticElevation);
            }
        }

        private static void MolodenskyShift(Spheroid? spheroidIn, Spheroid? spheroidOut,
                                     double latitudeIn, double longitudeIn, double elevationIn,
                                     double deltaX, double deltaY, double deltaZ,
                                     out double latitudeOut, out double longitudeOut, out double elevationOut)
        {
            if (spheroidIn != null &&
                spheroidOut != null &&
                spheroidIn.SemiMajorAxis != null &&
                spheroidIn.SemiMinorAxis != null &&
                spheroidOut.SemiMajorAxis != null &&
                spheroidOut.SemiMinorAxis != null)
            {
                if (
                    Numeric.EQ(spheroidIn!.SemiMajorAxis.ScalarValue, spheroidOut!.SemiMajorAxis.ScalarValue, 0.0001) &&
                    Numeric.EQ(spheroidIn!.SemiMinorAxis.ScalarValue, spheroidOut!.SemiMinorAxis.ScalarValue, 0.0001) &&
                    Numeric.EQ(deltaX, 0, 0.0001) &&
                    Numeric.EQ(deltaY, 0, 0.0001) &&
                    Numeric.EQ(deltaZ, 0, 0.0001))
                {
                    latitudeOut = latitudeIn;
                    longitudeOut = longitudeIn;
                    elevationOut = elevationIn;
                }
                else
                {
                    if (Numeric.IsDefined(spheroidIn.SemiMajorAxis.ScalarValue) &&
                        Numeric.IsDefined(spheroidOut.SemiMajorAxis.ScalarValue) &&
                        spheroidIn.Flattening != null &&
                        Numeric.IsDefined(spheroidIn.Flattening.ScalarValue) &&
                        spheroidOut.Flattening != null &&
                        Numeric.IsDefined(spheroidOut.Flattening.ScalarValue) &&
                        spheroidIn.SquaredEccentricity != null &&
                        Numeric.IsDefined(spheroidIn.SquaredEccentricity.ScalarValue))
                    {
                        double sinLat = SMath.Sin(latitudeIn);
                        double cosLat = SMath.Cos(latitudeIn);
                        double sinLon = SMath.Sin(longitudeIn);
                        double cosLon = SMath.Cos(longitudeIn);
                        double n = NormalRadius(spheroidIn, latitudeIn);
                        double m = MeridianRadius(spheroidIn, latitudeIn);
                        double a1 = spheroidIn.SemiMajorAxis.ScalarValue!.Value;
                        double da = spheroidOut.SemiMajorAxis.ScalarValue!.Value - a1;
                        double f1 = spheroidIn.Flattening.ScalarValue!.Value;
                        double df = spheroidOut.Flattening.ScalarValue!.Value - f1;
                        double es = spheroidIn.SquaredEccentricity.ScalarValue!.Value;

                        double tmp = deltaZ * cosLat;
                        tmp -= deltaX * sinLat * cosLon;
                        tmp -= deltaY * sinLat * sinLon;
                        tmp += da * n * es / a1 + df * (m / (1 - f1) + n * (1 - f1)) * sinLat * cosLat;
                        tmp /= m + elevationIn;
                        double deltaLat = tmp;
                        tmp = deltaY * cosLon - deltaX * sinLon;
                        tmp /= (n + elevationIn) * cosLat;
                        double deltaLon = tmp;
                        tmp = deltaX * cosLat * cosLon;
                        tmp += deltaY * cosLat * sinLon;
                        tmp += deltaZ * sinLat;
                        tmp -= da * a1 / n;
                        tmp += df * (1 - f1) * n * sinLat * sinLat;
                        double deltaHeight = tmp;
                        latitudeOut = latitudeIn + deltaLat;
                        longitudeOut = longitudeIn + deltaLon;
                        elevationOut = elevationIn + deltaHeight;
                    }
                    else
                    {
                        latitudeOut = Numeric.UNDEF_DOUBLE;
                        longitudeOut = Numeric.UNDEF_DOUBLE;
                        elevationOut = Numeric.UNDEF_DOUBLE;
                    }
                }
            }
            else
            {
                latitudeOut = Numeric.UNDEF_DOUBLE;
                longitudeOut = Numeric.UNDEF_DOUBLE;
                elevationOut = Numeric.UNDEF_DOUBLE;
            }
        }
        #endregion
        #region computations on spheroids
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static double NormalRadius(Spheroid? spheroid, double latitude)
        {
            if (spheroid != null && Numeric.IsDefined(latitude) && 
                spheroid.SquaredEccentricity != null && 
                Numeric.IsDefined(spheroid.SquaredEccentricity.ScalarValue) &&
                spheroid.SemiMajorAxis != null &&
                Numeric.IsDefined(spheroid.SemiMajorAxis.ScalarValue))
            {
                double sinLat = SMath.Sin(latitude);
                double squaredEccentricity = spheroid.SquaredEccentricity.ScalarValue!.Value;
                return spheroid.SemiMajorAxis.ScalarValue!.Value / SMath.Sqrt((1.0 - squaredEccentricity * squaredEccentricity * sinLat * sinLat));
            }
            else
            {
                return Numeric.UNDEF_DOUBLE;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static double MeridianRadius(Spheroid? spheroid, double latitude)
        {
            if (spheroid != null && Numeric.IsDefined(latitude) &&
                spheroid.SquaredEccentricity != null &&
                Numeric.IsDefined(spheroid.SquaredEccentricity.ScalarValue) &&
                spheroid.SemiMajorAxis != null &&
                Numeric.IsDefined(spheroid.SemiMajorAxis.ScalarValue))
            {
                double squaredEccentricity = spheroid.SquaredEccentricity.ScalarValue!.Value;
                double sinLat = SMath.Sin(latitude);
                double w = SMath.Sqrt(1.0 - squaredEccentricity * sinLat * sinLat);
                return spheroid.SemiMajorAxis.ScalarValue!.Value * (1 - squaredEccentricity) / (w * w * w);
            }
            else
            {
                return Numeric.UNDEF_DOUBLE;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="elevation"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void ToCartesian(Spheroid? spheroid, double latitude, double longitude, double elevation,
                                out double x, out double y, out double z)
        {
            x = Numeric.UNDEF_DOUBLE;
            y = Numeric.UNDEF_DOUBLE;
            z = Numeric.UNDEF_DOUBLE;
            if (spheroid != null && spheroid.SquaredEccentricity != null && Numeric.IsDefined(spheroid.SquaredEccentricity.ScalarValue))
            {
                double cosLat = SMath.Cos(latitude);
                double sinLat = SMath.Sin(latitude);
                double cosLon = SMath.Cos(longitude);
                //double sinLon = SMath.Sin(longitude);
                double n = NormalRadius(spheroid, latitude);
                x = (n + elevation) * cosLat * cosLon;
                y = (n + elevation) * cosLat * sinLat;
                z = (n * (1 - spheroid.SquaredEccentricity.ScalarValue!.Value) + elevation) * sinLat;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="elevation"></param>
        public static void FromCartesian(Spheroid? spheroid, double x, double y, double z,
                                  out double latitude, out double longitude, out double elevation)
        {
            latitude = Numeric.UNDEF_DOUBLE;
            longitude = Numeric.UNDEF_DOUBLE;
            elevation = Numeric.UNDEF_DOUBLE;
            if (spheroid != null && 
                spheroid.SemiMajorAxis != null &&
                Numeric.IsDefined(spheroid.SemiMajorAxis.ScalarValue) &&
                spheroid.SemiMinorAxis != null &&
                Numeric.IsDefined(spheroid.SemiMinorAxis.ScalarValue) &&
                spheroid.SquaredEccentricity != null &&
                Numeric.IsDefined(spheroid.SquaredEccentricity.ScalarValue))
            {
                double a = spheroid.SemiMajorAxis.ScalarValue!.Value;
                double b = spheroid.SemiMinorAxis.ScalarValue!.Value;
                double squareEcc = spheroid.SquaredEccentricity.ScalarValue!.Value;
                double b2 = b * b;
                double eMark2 = (a * a - b2) / b2;
                double rho = SMath.Sqrt(x * x + y * y);
                double theta = SMath.Atan(z * a / (rho * b));
                double sinTheta = SMath.Sin(theta);
                double sin3Theta = sinTheta * sinTheta * sinTheta;
                double cosTheta = SMath.Cos(theta);
                double cos3Theta = cosTheta * cosTheta * cosTheta;
                double exp1 = z + eMark2 * b * sin3Theta;
                double exp2 = rho - squareEcc * a * cos3Theta;
                latitude = SMath.Atan(exp1 / exp2);
                longitude = SMath.Atan(y / x);
                if ((x < 0) && (y < 0) || (x < 0) && (y > 0))
                {
                    longitude = -longitude;
                }
                double cosLat = SMath.Cos(latitude);
                double sinLat = SMath.Sin(latitude);
                double sin2Lat = sinLat * sinLat;
                double NLat = a / SMath.Sqrt(1 - squareEcc * sin2Lat);
                elevation = rho / cosLat - NLat;
            }
        }
        /// <summary>
        /// Build the proj4 query string defining the given spheroid
        /// </summary>
        /// <returns></returns>
        public static string GetProj4String(Spheroid? spheroid)
        {
            if (spheroid != null)
            {
                string sval = "";
                if (spheroid.SemiMajorAxis != null)
                {
                    sval += " +a=" + spheroid.SemiMajorAxis.ToString();
                }
                if (spheroid.SemiMinorAxis != null)
                {
                    sval += " +b=" + spheroid.SemiMinorAxis.ToString();
                }
                if (spheroid.Flattening != null)
                {
                    sval += " +f=" + spheroid.Flattening;
                }
                if (spheroid.InverseFlattening != null)
                {
                    sval += " +rf=" + spheroid.InverseFlattening;
                }
                if (spheroid.Eccentricity != null)
                {
                    sval += " +e=" + spheroid.Eccentricity;
                }
                if (spheroid.SquaredEccentricity != null)
                {
                    sval += " +es=" + spheroid.SquaredEccentricity;
                }
                return sval;
            }
            return string.Empty;
        }
        #endregion
    }
}
