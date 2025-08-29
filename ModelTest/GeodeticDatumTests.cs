using NUnit.Framework;
using NORCE.Drilling.GeodeticDatum.Model;
using System;

namespace NORCE.Drilling.GeodeticDatum.ModelTest
{
    public class GeodeticDatumTests
    {
        [Test]
        public void WGS84_Defaults_AreIdentity()
        {
            var d = Model.GeodeticDatum.WGS84;
            Assert.That(d, Is.Not.Null);
            Assert.That(d!.Spheroid, Is.Not.Null);

            // Identity transform params
            Assert.That(d.DeltaX, Is.Not.Null); Assert.That(d.DeltaY, Is.Not.Null); Assert.That(d.DeltaZ, Is.Not.Null);
            Assert.That(d.RotationX, Is.Not.Null); Assert.That(d.RotationY, Is.Not.Null); Assert.That(d.RotationZ, Is.Not.Null);
            Assert.That(d.ScaleFactor, Is.Not.Null);

            Assert.That(d.DeltaX!.ScalarValue!.Value, Is.EqualTo(0).Within(1e-12));
            Assert.That(d.DeltaY!.ScalarValue!.Value, Is.EqualTo(0).Within(1e-12));
            Assert.That(d.DeltaZ!.ScalarValue!.Value, Is.EqualTo(0).Within(1e-12));
            Assert.That(d.RotationX!.ScalarValue!.Value, Is.EqualTo(0).Within(1e-12));
            Assert.That(d.RotationY!.ScalarValue!.Value, Is.EqualTo(0).Within(1e-12));
            Assert.That(d.RotationZ!.ScalarValue!.Value, Is.EqualTo(0).Within(1e-12));
            Assert.That(d.ScaleFactor!.ScalarValue!.Value, Is.EqualTo(1).Within(1e-12));
        }

        [Test]
        public void ED50_NorwayFinland_HasExpectedParameters()
        {
            var d = Model.GeodeticDatum.ED50NorwayFinland;
            Assert.That(d, Is.Not.Null);
            Assert.That(d!.Spheroid, Is.Not.Null);
            // Known translation-only parameters
            Assert.That(d.DeltaX!.ScalarValue!.Value, Is.EqualTo(-87.0).Within(1e-9));
            Assert.That(d.DeltaY!.ScalarValue!.Value, Is.EqualTo(-95.0).Within(1e-9));
            Assert.That(d.DeltaZ!.ScalarValue!.Value, Is.EqualTo(-120.0).Within(1e-9));
        }
    }
}

