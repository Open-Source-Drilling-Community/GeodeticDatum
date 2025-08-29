using NUnit.Framework;
using NORCE.Drilling.GeodeticDatum.Model;
using System;
using System.Linq;

namespace NORCE.Drilling.GeodeticDatum.ModelTest
{
    public class GeodeticConversionSetTests
    {
        [Test]
        public void Calculate_FromDatum_ToWGS84_IdentityForWGS84()
        {
            // Arrange
            var set = new GeodeticConversionSet
            {
                GeodeticDatum = Model.GeodeticDatum.WGS84
            };
            var coord = new GeodeticCoordinate
            {
                LatitudeDatum = 0.5,
                LongitudeDatum = 1.0,
                VerticalDepthDatum = 123.4,
                OctreeDepth = 5
            };
            set.GeodeticCoordinates.Add(coord);

            // Act
            var ok = set.Calculate();

            // Assert
            Assert.That(ok, Is.True);
            Assert.That(coord.LatitudeWGS84, Is.Not.Null);
            Assert.That(coord.LongitudeWGS84, Is.Not.Null);
            Assert.That(coord.VerticalDepthWGS84, Is.Not.Null);
            Assert.That(coord.LatitudeWGS84!.Value, Is.EqualTo(coord.LatitudeDatum!.Value).Within(1e-12));
            Assert.That(coord.LongitudeWGS84!.Value, Is.EqualTo(coord.LongitudeDatum!.Value).Within(1e-12));
            Assert.That(coord.VerticalDepthWGS84!.Value, Is.EqualTo(coord.VerticalDepthDatum!.Value).Within(1e-12));
            Assert.That(coord.OctreeCode.HasValue, "Octree code should be computed");
            Assert.That(coord.OctreeCode!.Value.Depth, Is.EqualTo(5));
        }

        [Test]
        public void Calculate_FromWGS84_ToDatum_IdentityForWGS84()
        {
            // Arrange
            var set = new GeodeticConversionSet
            {
                GeodeticDatum = Model.GeodeticDatum.WGS84
            };
            var coord = new GeodeticCoordinate
            {
                LatitudeWGS84 = 0.25,
                LongitudeWGS84 = 0.75,
                VerticalDepthWGS84 = 10.0,
                OctreeDepth = 6
            };
            set.GeodeticCoordinates.Add(coord);

            // Act
            var ok = set.Calculate();

            // Assert
            Assert.That(ok, Is.True);
            Assert.That(coord.LatitudeDatum, Is.EqualTo(coord.LatitudeWGS84).Within(1e-12));
            Assert.That(coord.LongitudeDatum, Is.EqualTo(coord.LongitudeWGS84).Within(1e-12));
            Assert.That(coord.VerticalDepthDatum, Is.EqualTo(coord.VerticalDepthWGS84).Within(1e-12));
            Assert.That(coord.OctreeCode.HasValue);
            Assert.That(coord.OctreeCode!.Value.Depth, Is.EqualTo(6));
        }
    }
}

