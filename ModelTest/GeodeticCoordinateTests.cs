using NUnit.Framework;
using NORCE.Drilling.GeodeticDatum.Model;
using System;

namespace NORCE.Drilling.GeodeticDatum.ModelTest
{
    public class GeodeticCoordinateTests
    {
        [Test]
        public void NormalRadius_AtPole_UsesSquaredEccentricityOnce()
        {
            Spheroid wgs84 = Spheroid.WGS84 ?? throw new InvalidOperationException("WGS84 spheroid is not defined.");
            double semiMajorAxis = wgs84.SemiMajorAxis?.ScalarValue ?? throw new InvalidOperationException("WGS84 semi-major axis is not defined.");
            double squaredEccentricity = wgs84.SquaredEccentricity?.ScalarValue ?? throw new InvalidOperationException("WGS84 squared eccentricity is not defined.");
            double expected = semiMajorAxis / Math.Sqrt(1.0 - squaredEccentricity);

            double normalRadius = GeodeticCoordinate.NormalRadius(wgs84, Math.PI / 2.0);

            Assert.That(normalRadius, Is.EqualTo(expected).Within(1e-9));
        }

        [Test]
        public void ToCartesian_EquatorAtNinetyDegreesEast_MapsToYAxis()
        {
            Spheroid wgs84 = Spheroid.WGS84 ?? throw new InvalidOperationException("WGS84 spheroid is not defined.");
            double semiMajorAxis = wgs84.SemiMajorAxis?.ScalarValue ?? throw new InvalidOperationException("WGS84 semi-major axis is not defined.");

            GeodeticCoordinate.ToCartesian(
                wgs84,
                latitude: 0.0,
                longitude: Math.PI / 2.0,
                elevation: 0.0,
                out double x,
                out double y,
                out double z);

            Assert.That(x, Is.EqualTo(0.0).Within(1e-9));
            Assert.That(y, Is.EqualTo(semiMajorAxis).Within(1e-9));
            Assert.That(z, Is.EqualTo(0.0).Within(1e-9));
        }
    }
}
