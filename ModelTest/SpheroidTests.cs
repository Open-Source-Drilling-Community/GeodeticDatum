using NUnit.Framework;
using NORCE.Drilling.GeodeticDatum.Model;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using System;

namespace NORCE.Drilling.GeodeticDatum.ModelTest
{
    public class SpheroidTests
    {
        [Test]
        public void Calculate_CompletesParameters_FromSemiAxes()
        {
            // Arrange: WGS84-like axes
            var sph = new Spheroid
            {
                SemiMajorAxis = new ScalarDrillingProperty { ScalarValue = 6378137.0 },
                SemiMinorAxis = new ScalarDrillingProperty { ScalarValue = 6356752.314245 }
            };

            // Act
            var ok = sph.Calculate();

            // Assert
            Assert.That(ok, Is.True);
            Assert.That(sph.Flattening, Is.Not.Null);
            Assert.That(sph.InverseFlattening, Is.Not.Null);
            Assert.That(sph.Eccentricity, Is.Not.Null);
            Assert.That(sph.SquaredEccentricity, Is.Not.Null);

            // Reference values for WGS84
            double f = 1.0 / 298.257223563;
            double e2 = 6.6943799901413165e-3; // eccentricity squared
            double e = Math.Sqrt(e2);

            Assert.That(sph.Flattening!.ScalarValue!.Value, Is.EqualTo(f).Within(1e-12));
            Assert.That(sph.InverseFlattening!.ScalarValue!.Value, Is.EqualTo(298.257223563).Within(1e-8));
            Assert.That(sph.SquaredEccentricity!.ScalarValue!.Value, Is.EqualTo(e2).Within(1e-12));
            Assert.That(sph.Eccentricity!.ScalarValue!.Value, Is.EqualTo(e).Within(1e-12));
        }

        [Test]
        public void BuiltIn_WGS84_IsConsistent()
        {
            var wgs = Spheroid.WGS84;
            Assert.That(wgs, Is.Not.Null);
            Assert.That(wgs!.SemiMajorAxis, Is.Not.Null);
            Assert.That(wgs.SemiMinorAxis, Is.Not.Null);
            Assert.That(wgs.Flattening, Is.Not.Null);
            Assert.That(wgs.InverseFlattening, Is.Not.Null);

            // Basic checks around expected magnitudes
            Assert.That(wgs.SemiMajorAxis!.ScalarValue!.Value, Is.EqualTo(6378137.0).Within(1e-6));
            Assert.That(wgs.SemiMinorAxis!.ScalarValue!.Value, Is.InRange(6356752.0, 6356753.0));
        }
    }
}

