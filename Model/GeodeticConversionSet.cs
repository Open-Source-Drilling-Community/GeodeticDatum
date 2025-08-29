using OSDC.DotnetLibraries.General.Common;
using OSDC.DotnetLibraries.General.DataManagement;
using OSDC.DotnetLibraries.General.Octree;
using System;
using System.Collections.Generic;

namespace NORCE.Drilling.GeodeticDatum.Model
{
    /// <summary>
    /// A geodetic conversion does computations on a list of GeodeticCoordinate's.
    /// The geodetic data are converted to the target geodetic datum or WGS84, i.e., the complementary geodetic datum.
    /// The octree code for the WGS84 geodetic position can also calculated at the requested level of details.
    /// it is also possible to pass the octree code (in the WGS84 datum), and then the geodetic coordinates in the reference and WGS84 datum are calculated
    /// </summary>
    public class GeodeticConversionSet
    {
        public static readonly double MaxElevation = 20000.0;
        /// <summary>
        /// a MetaInfo for the GeodeticConversionSet
        /// </summary>
        public MetaInfo? MetaInfo { get; set; }
        /// <summary>
        /// name of the data
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// a description of the data
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// the date when the data was created
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }
        /// <summary>
        /// the date when the data was last modified
        /// </summary>
        public DateTimeOffset? LastModificationDate { get; set; }
        /// <summary>
        /// the reference geodetic datum
        /// </summary>
        public GeodeticDatum? GeodeticDatum { get; set; }
        /// <summary>
        /// the bounds used in the octree encoding
        /// </summary>
        public Bounds? OctreeBounds { get; set; }
        /// <summary>
        /// the list of geodetic coordinates
        /// </summary>
        public List<GeodeticCoordinate> GeodeticCoordinates { get; set; } = [];
        /// <summary>
        /// default constructor
        /// </summary>
        public GeodeticConversionSet() : base()
        {
        }

        /// <summary>
        /// Calculation method for GeodeticConversionSet
        /// if the 3 coordinates in the reference datum are defined, then they are converted to WGS4 and the octree code is calculated
        /// if the 3 coordinates in the WGS84 datum are defined then they are converted to the reference datum and the octree code is calculated
        /// if the octree code is defined, then the geodetic coordinates in the reference and WGS84 datum are calculated
        /// </summary>
        /// <returns></returns>
        public bool Calculate()
        {
            bool success = false;
            if (GeodeticDatum != null)
            {
                if (GeodeticCoordinates != null)
                {
                    success = true;
                    foreach (GeodeticCoordinate coordinate in GeodeticCoordinates)
                    {
                        if (coordinate != null)
                        {
                            if (coordinate.LatitudeDatum != null && coordinate.LongitudeDatum != null && coordinate.VerticalDepthDatum != null)
                            {
                                coordinate.ToFundamental(GeodeticDatum);
                                if (coordinate.OctreeDepth == 0)
                                    coordinate.OctreeDepth = 24;
                                byte[] octree = GetOctree(coordinate.OctreeDepth, (double)coordinate.LatitudeWGS84!, (double)coordinate.LongitudeWGS84!, -(double)coordinate.VerticalDepthWGS84!);
                                coordinate.OctreeCode = new OctreeCodeLong(octree);
                            }
                            else if (coordinate.LatitudeWGS84 != null && coordinate.LongitudeWGS84 != null && coordinate.VerticalDepthWGS84 != null)
                            {
                                GeodeticCoordinate.FromFundamental(GeodeticDatum, (double)coordinate.LatitudeWGS84, (double)coordinate.LongitudeWGS84, -(double)coordinate.VerticalDepthWGS84,
                                    out double latitudeDatum, out double longitudeDatum, out double elevationDatum);
                                coordinate.LatitudeDatum = latitudeDatum;
                                coordinate.LongitudeDatum = longitudeDatum;
                                coordinate.VerticalDepthDatum = -elevationDatum;
                                if (coordinate.OctreeDepth == 0)
                                    coordinate.OctreeDepth = 24;
                                byte[] octree = GetOctree(coordinate.OctreeDepth, (double)coordinate.LatitudeWGS84, (double)coordinate.LongitudeWGS84, -(double)coordinate.VerticalDepthWGS84);
                                coordinate.OctreeCode = new OctreeCodeLong(octree);
                            }
                            else if (coordinate.OctreeDepth > 0 && coordinate.OctreeCode.HasValue && coordinate.OctreeCode!.Value.Depth == coordinate.OctreeDepth)
                            {
                                byte[] octree = coordinate.OctreeCode!.Value.Decode();
                                GetGeodeticCoordinate(octree, out double latitudeWGS84, out double longitudeWGS84, out double elevationWGS4);

                                coordinate.LatitudeWGS84 = latitudeWGS84;
                                coordinate.LongitudeWGS84 = longitudeWGS84;
                                coordinate.VerticalDepthWGS84 = -elevationWGS4;

                                GeodeticCoordinate.FromFundamental(GeodeticDatum, (double)coordinate.LatitudeWGS84, (double)coordinate.LongitudeWGS84, -(double)coordinate.VerticalDepthWGS84,
                                    out double latitudeDatum, out double longitudeDatum, out double elevationDatum);
                                coordinate.LatitudeDatum = latitudeDatum;
                                coordinate.LongitudeDatum = longitudeDatum;
                                coordinate.VerticalDepthDatum = -elevationDatum;
                            }
                            else
                            {
                                success = false;
                            }
                        }
                    }
                }
            }
            return success;
        }

        private byte[] GetOctree(byte depth, double latitude, double longitude, double elevation)
        {
            byte[] result = new byte[depth];
            OctreeBounds ??= new Bounds(-Numeric.PI / 2.0, Numeric.PI / 2.0, -Numeric.PI, Numeric.PI, -MaxElevation, MaxElevation);
            Bounds topBound = new(OctreeBounds);
            for (int i = 0; i < result.Length; i++)
            {
                for (byte j = 0; j < 8; j++)
                {
                    Bounds? bound = topBound!.CalculateBounds(j);
                    if (bound!.Contains(latitude, longitude, elevation))
                    {
                        topBound = bound;
                        result[i] = j;
                        break;
                    }
                }
            }
            return result;
        }

        private void GetGeodeticCoordinate(byte[] octree, out double latitude, out double longitude, out double elevation)
        {
            OctreeBounds ??= new Bounds(-Numeric.PI / 2.0, Numeric.PI / 2.0, -Numeric.PI, Numeric.PI, -MaxElevation, MaxElevation);
            Bounds? topBound = new(OctreeBounds);
            foreach (byte b in octree)
            {
                topBound = topBound!.CalculateBounds(b);
            }
            latitude = (double)topBound!.MiddleX!;
            longitude = (double)topBound!.MiddleY!;
            elevation = (double)topBound!.MiddleZ!;
        }
    }
}
