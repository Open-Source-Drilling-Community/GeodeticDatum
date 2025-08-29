using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using OSDC.DotnetLibraries.General.Common;
using OSDC.DotnetLibraries.General.DataManagement;
using System;
using System.Linq;
using SMath = System.Math;

namespace NORCE.Drilling.GeodeticDatum.Model
{
    public class Spheroid
    {
        /// <summary>
        /// a MetaInfo for the Spheroid
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
        /// true if it is a default spheroid
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// the length of the semi major axis
        /// </summary>
        public ScalarDrillingProperty? SemiMajorAxis { get; set; } = null;
        /// <summary>
        /// flag indicating whether semi major axis is set by the user
        /// </summary>
        public bool IsSemiMajorAxisSet { get; set; }
        /// <summary>
        /// the liength of the semi minor axis
        /// </summary>
        public ScalarDrillingProperty? SemiMinorAxis { get; set; } = null;
        /// <summary>
        /// flag indicating whether semi minor axis is set by the user
        /// </summary>
        public bool IsSemiMinorAxisSet { get; set; } 
        /// <summary>
        /// the eccentricity characterizing the difference between the semi major and minor axes
        /// </summary>
        public ScalarDrillingProperty? Eccentricity { get; set; } = null;
        /// <summary>
        /// flag indicating whether eccentricity is set by the user
        /// </summary>
        public bool IsEccentricitySet { get; set; }
        /// <summary>
        /// the squared value of the eccentricity in order to gain in precision
        /// </summary>
        public ScalarDrillingProperty? SquaredEccentricity { get; set; } = null;
        /// <summary>
        /// flag indicating whether squared eccentricity is set by the user
        /// </summary>
        public bool IsSquaredEccentricitySet { get; set; }
        /// <summary>
        /// yet another way to express the difference between the semi major and minor axes
        /// </summary>
        public ScalarDrillingProperty? Flattening { get; set; } = null;
        /// <summary>
        /// flag indicating whether flattening is set by the user
        /// </summary>
        public bool IsFlatteningSet { get; set; }
        /// <summary>
        /// the inverse value of the flatening, in order to gain in prevision
        /// </summary>
        public ScalarDrillingProperty? InverseFlattening { get; set; } = null;
        /// <summary>
        /// flag indicating whether inverse flattening is set by the user
        /// </summary>
        public bool IsInverseFlatteningSet { get; set; }

        /// <summary>
        /// default constructor required for JSON serialization
        /// </summary>
        public Spheroid() : base()
        {
        }

        /// <summary>
        /// 2 spheroid parameters are needed to compute the full spheroid, including at least one of SemiMinorAxis or SemiMajorAxis
        /// </summary>
        /// <returns></returns>
        public bool Calculate()
        {
            if (new[] { SemiMajorAxis, SemiMinorAxis, Eccentricity, SquaredEccentricity, Flattening, InverseFlattening }
                    .Count(x => x != null && Numeric.IsDefined(x.ScalarValue)) >= 2 &&
                new[] { SemiMajorAxis, SemiMinorAxis }
                    .Any(x => x != null && Numeric.IsDefined(x.ScalarValue) && !Numeric.EQ(x.ScalarValue,0)))
            {
                #region set SemiMajorAxis
                if (SemiMajorAxis == null || Numeric.IsUndefined(SemiMajorAxis.ScalarValue))
                {
                    SemiMajorAxis ??= new ScalarDrillingProperty();
                    IsSemiMajorAxisSet = false;
                    if (Flattening != null && Numeric.IsDefined(Flattening.ScalarValue) && !Numeric.EQ(Flattening.ScalarValue, 1))
                    {
                        SemiMajorAxis.ScalarValue = SemiMinorAxis!.ScalarValue!.Value / (1 - Flattening.ScalarValue!.Value); // a = b / (1 - f)
                    }
                    else if (InverseFlattening != null && Numeric.IsUndefined(InverseFlattening.ScalarValue) && !Numeric.EQ(InverseFlattening.ScalarValue,1))
                    {
                        SemiMajorAxis.ScalarValue = SemiMinorAxis!.ScalarValue!.Value * InverseFlattening.ScalarValue!.Value / (InverseFlattening.ScalarValue!.Value - 1); // a = b * invf / (invf - 1)
                    }
                    else if (Eccentricity != null && Numeric.IsDefined(Eccentricity.ScalarValue) && !Numeric.EQ(Eccentricity.ScalarValue, 1))
                    {
                        SemiMajorAxis.ScalarValue = SemiMinorAxis!.ScalarValue!.Value / SMath.Sqrt(1 - Eccentricity.ScalarValue!.Value * Eccentricity.ScalarValue!.Value); // a = b / sqrt(1 - e * e)
                    }
                    else if (SquaredEccentricity != null && Numeric.IsDefined(SquaredEccentricity.ScalarValue) && Numeric.LE(SquaredEccentricity.ScalarValue, 1))
                    {
                        SemiMajorAxis.ScalarValue = SemiMinorAxis!.ScalarValue!.Value / SMath.Sqrt(1 - SquaredEccentricity.ScalarValue!.Value); // a = b / sqrt(1 - sqe)
                    }
                }
                #endregion
                #region set SemiMinorAxis
                if (SemiMinorAxis == null || Numeric.IsUndefined(SemiMinorAxis.ScalarValue))
                {
                    SemiMinorAxis ??= new ScalarDrillingProperty();
                    IsSemiMinorAxisSet = false;
                    if (Flattening != null && Numeric.IsDefined(Flattening.ScalarValue))
                    {
                        SemiMinorAxis.ScalarValue = SemiMajorAxis.ScalarValue!.Value * (1 - Flattening.ScalarValue!.Value); // b = a * (1 - f)
                    }
                    else if (InverseFlattening != null && Numeric.IsDefined(InverseFlattening.ScalarValue) && !Numeric.EQ(InverseFlattening.ScalarValue, 0))
                    {
                        SemiMinorAxis.ScalarValue = SemiMajorAxis.ScalarValue!.Value * (1 - 1 / InverseFlattening.ScalarValue!.Value); // b = a * (1 - 1 / invf)
                    }
                    else if (Eccentricity != null && Numeric.IsDefined(Eccentricity.ScalarValue))
                    {
                        SemiMinorAxis.ScalarValue = SemiMajorAxis.ScalarValue!.Value * SMath.Sqrt(1 - Eccentricity.ScalarValue!.Value * Eccentricity.ScalarValue!.Value); // b = a * sqrt(1 - e * e)
                    }
                    else if (SquaredEccentricity != null && Numeric.IsDefined(SquaredEccentricity.ScalarValue) && Numeric.LE(SquaredEccentricity.ScalarValue, 1))
                    {
                        SemiMinorAxis.ScalarValue = SemiMajorAxis.ScalarValue!.Value * SMath.Sqrt(1 - SquaredEccentricity.ScalarValue!.Value); // b = a * sqrt(1 - sqe)
                    }
                }
                #endregion
                #region set Flattening
                if ((Flattening == null || Numeric.IsUndefined(Flattening.ScalarValue)) && 
                    SemiMinorAxis != null && 
                    SemiMajorAxis != null && 
                    Numeric.IsDefined(SemiMinorAxis.ScalarValue) && 
                    Numeric.IsDefined(SemiMajorAxis.ScalarValue) &&
                    !Numeric.EQ(SemiMajorAxis.ScalarValue, 0))
                {
                    Flattening ??= new ScalarDrillingProperty();
                    IsFlatteningSet = false;
                    Flattening.ScalarValue = 1 - SemiMinorAxis.ScalarValue!.Value / SemiMajorAxis.ScalarValue!.Value; // f = 1 - b / a
                }
                #endregion
                #region set InverseFlattening
                if ((InverseFlattening == null || Numeric.IsUndefined(InverseFlattening.ScalarValue)) && Flattening != null && Numeric.IsDefined(Flattening.ScalarValue) && !Numeric.EQ(Flattening.ScalarValue, 0))
                {
                    InverseFlattening ??= new ScalarDrillingProperty();
                    IsInverseFlatteningSet = false;
                    InverseFlattening.ScalarValue = 1 / Flattening.ScalarValue!.Value; // invf = 1 / f
                }
                #endregion
                #region set SquaredEccentricity
                if ((SquaredEccentricity == null || Numeric.IsUndefined(SquaredEccentricity.ScalarValue)) && 
                    SemiMinorAxis != null &&
                    SemiMajorAxis != null &&
                    Numeric.IsDefined(SemiMinorAxis.ScalarValue) &&
                    Numeric.IsDefined(SemiMajorAxis.ScalarValue) &&
                    !Numeric.EQ(SemiMajorAxis.ScalarValue, 0))
                {
                    SquaredEccentricity ??= new ScalarDrillingProperty();
                    IsSquaredEccentricitySet = false;
                    SquaredEccentricity.ScalarValue = 1 - SemiMinorAxis.ScalarValue!.Value * SemiMinorAxis.ScalarValue!.Value / (SemiMajorAxis.ScalarValue!.Value * SemiMajorAxis.ScalarValue!.Value); // sqe = 1 - (b * b) / (a * a)
                }
                #endregion
                #region set Eccentricity
                if ((Eccentricity == null || Numeric.IsUndefined(Eccentricity.ScalarValue)) && 
                    SquaredEccentricity != null && 
                    Numeric.IsDefined(SquaredEccentricity.ScalarValue) && 
                    Numeric.GE(SquaredEccentricity.ScalarValue, 0))
                {
                    Eccentricity ??= new ScalarDrillingProperty();
                    IsEccentricitySet = false;
                    Eccentricity.ScalarValue = SMath.Sqrt(SquaredEccentricity.ScalarValue!.Value); // e = sqrt(sqe)
                }
                #endregion
                return true;
            }
            else
            {
                return false;
            }
        }

        #region spheroid Australian National 1966
        private static Spheroid? _australian_1966 = null;
        public static Spheroid? Australian_1966
        {
            get
            {
                _australian_1966 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("6e4d5195-66c7-4a8e-bd9f-438e52c1e314") },
                    Name = "Australian National (1966)",
                    Description = "Australian National (1966)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty() { ScalarValue = 6378160 },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty() { ScalarValue = 298.25 },
                    IsInverseFlatteningSet = true
                };
                _australian_1966.Calculate();
                return _australian_1966;
            }
        }
        #endregion

        #region spheroid Clarke 1866
        private static Spheroid? _clarke_1866 = null;
        public static Spheroid? Clarke_1866
        {
            get
            {
                _clarke_1866 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("58d96cb5-d57e-4489-8295-1dd5c8985be9") },
                    Name = "Clarke (1866)",
                    Description = "Clarke (1866)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty() { ScalarValue = 6378206.4 },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty() { ScalarValue = 6356583.8 },
                    IsSemiMinorAxisSet = true
                };
                _clarke_1866.Calculate();
                return _clarke_1866;
            }
        }
        #endregion

        #region spheroid Clarke 1880
        private static Spheroid? _clarke_1880 = null;
        public static Spheroid? Clarke_1880
        {
            get
            {
                _clarke_1880 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("4d5c5b86-2e0d-46e0-bf02-4c62a24cc027") },
                    Name = "Clarke (1880)",
                    Description = "Clarke (1880)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378249.145
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 293.465
                    },
                    IsInverseFlatteningSet = true
                };
                _clarke_1880.Calculate();
                return _clarke_1880;
            }
        }
        #endregion

        #region spheroid Everest 1830
        private static Spheroid? _everest_1830 = null;
        public static Spheroid? Everest_1830
        {
            get
            {
                _everest_1830 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("f7c936e2-8d98-42bd-9d5d-6f6f7387b4d3") },
                    Name = "Everest 1830",
                    Description = "Everest 1830",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377276.35
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356075.41
                    },
                    IsSemiMinorAxisSet = true
                };
                _everest_1830.Calculate();
                return _everest_1830;
            }
        }
        #endregion

        #region spheroid Hayford 1910
        private static Spheroid? _hayford_1910 = null;
        public static Spheroid? Hayford_1910
        {
            get
            {
                _hayford_1910 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("977849e4-3379-4e2e-a9bc-aa1d9e6702e0") },
                    Name = "Hayford (1910)",
                    Description = "Hayford (1910)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378388
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 297
                    },
                    IsInverseFlatteningSet = true
                };
                _hayford_1910.Calculate();
                return _hayford_1910;
            }
        }
        #endregion

        #region spheroid Helmert 1906
        private static Spheroid? _helmert_1906 = null;
        public static Spheroid? Helmert_1906
        {
            get
            {
                _helmert_1906 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("0b17ee6a-51c1-4dcb-baac-990f27cda613") },
                    Name = "Helmert (1906)",
                    Description = "Helmert (1906)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378200
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.3
                    },
                    IsInverseFlatteningSet = true
                };
                _helmert_1906.Calculate();
                return _helmert_1906;
            }
        }
        #endregion

        #region spheroid Hough
        private static Spheroid? _hough = null;
        public static Spheroid? Hough
        {
            get
            {
                _hough ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("13d2b5a0-2575-4d30-9ba7-73fdea5dcfdf") },
                    Name = "Hough",
                    Description = "Hough",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378270
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356794.34
                    },
                    IsSemiMinorAxisSet = true
                };
                _hough.Calculate();
                return _hough;
            }
        }
        #endregion

        #region spheroid International 1924
        private static Spheroid? _international_1924 = null;
        public static Spheroid? International_1924
        {
            get
            {
                _international_1924 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("6bc2a4f8-0145-481b-b4b1-57e8d5d34189") },
                    Name = "International (1924)",
                    Description = "International (1924)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378388
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 297
                    },
                    IsInverseFlatteningSet = true
                };
                _international_1924.Calculate();
                return _international_1924;
            }
        }
        #endregion

        #region spheroid Mercury
        private static Spheroid? _mercury = null;
        public static Spheroid? Mercury
        {
            get
            {
                _mercury ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("1318f1cf-e2be-47d0-9d73-67e70ad2dc2f") },
                    Name = "Mercury",
                    Description = "Mercury",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378166
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356784.28
                    },
                    IsSemiMinorAxisSet = true
                };
                _mercury.Calculate();
                return _mercury;
            }
        }
        #endregion

        #region spheroid Merit
        private static Spheroid? _merit = null;
        public static Spheroid? Merit
        {
            get
            {
                _merit ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("0f904e71-c8d3-4647-a734-77c246387daa") },
                    Name = "MERIT",
                    Description = "MERIT",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378137
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.257
                    },
                    IsInverseFlatteningSet = true
                };
                _merit.Calculate();
                return _merit;
            }
        }
        #endregion

        #region spheroid Modified Airy
        private static Spheroid? _modified_airy = null;
        public static Spheroid? ModifiedAiry
        {
            get
            {
                _modified_airy ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("b3d6eaff-817a-40be-9b7d-05f170ef8a16") },
                    Name = "ModifiedAiry",
                    Description = "ModifiedAiry",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377341.89
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356036.14
                    },
                    IsSemiMinorAxisSet = true
                };
                _modified_airy.Calculate();
                return _modified_airy;
            }
        }
        #endregion

        #region spheroid Modified Everest Malaya Revised Kertau
        private static Spheroid? _modified_everest_malaya = null;
        public static Spheroid? ModifiedEverest_Malaya_RevisedKertau
        {
            get
            {
                _modified_everest_malaya ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("a3a6e76c-6a6e-4a17-8859-d600ecb910c4") },
                    Name = "Modified Everest (Malaya) Revised Kertau",
                    Description = "Modified Everest (Malaya) Revised Kertau",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377304.063
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356103.04
                    },
                    IsSemiMinorAxisSet = true
                };
                _modified_everest_malaya.Calculate();
                return _modified_everest_malaya;
            }
        }
        #endregion

        #region spheroid Modified Mercury
        private static Spheroid? _modified_mercury = null;
        public static Spheroid? ModifiedMercury
        {
            get
            {
                _modified_mercury ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("8fb6e984-7a61-4bd7-83fc-0cf2a1970f80") },
                    Name = "Modified Mercury",
                    Description = "Modified Mercury",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378150
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356768.34
                    },
                    IsSemiMinorAxisSet = true
                };
                _modified_mercury.Calculate();
                return _modified_mercury;
            }
        }
        #endregion

        #region spheroid NAD 27
        private static Spheroid? _nad27 = null;
        public static Spheroid? NAD27
        {
            get
            {
                _nad27 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("ef51b7f4-1f3f-4bbc-b964-e9fbd9e1e755") },
                    Name = "NAD 27",
                    Description = "NAD 27",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378206.4
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356583.8
                    },
                    IsSemiMinorAxisSet = true
                };
                _nad27.Calculate();
                return _nad27;
            }
        }
        #endregion

        #region spheroid NAD 83
        private static Spheroid? _nad83 = null;
        public static Spheroid? NAD83
        {
            get
            {
                _nad83 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("eee8c2e7-adab-4879-8721-cabc17c4d6ac") },
                    Name = "NAD 83",
                    Description = "NAD 83",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378137
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356752.3
                    },
                    IsSemiMinorAxisSet = true
                };
                _nad83.Calculate();
                return _nad83;
            }
        }
        #endregion

        #region spheroid New International 1967
        private static Spheroid? _new_international_1967 = null;
        public static Spheroid? NewInternational_1967
        {
            get
            {
                _new_international_1967 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("c00cddac-1b87-4318-a2d6-ff5880343ef3") },
                    Name = "New International (1967)",
                    Description = "New International (1967)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378157.5
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356772.2
                    },
                    IsSemiMinorAxisSet = true
                };
                _new_international_1967.Calculate();
                return _new_international_1967;
            }
        }
        #endregion

        #region spheroid South American 1969
        private static Spheroid? _south_american_1969 = null;
        public static Spheroid? SouthAmerican_1969
        {
            get
            {
                _south_american_1969 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("6fd246ad-3e44-4f3e-b290-4f75e2d504e4") },
                    Name = "South American (1969)",
                    Description = "South American (1969)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378160
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.25
                    },
                    IsInverseFlatteningSet = true
                };
                _south_american_1969.Calculate();
                return _south_american_1969;
            }
        }
        #endregion

        #region spheroid South East Asia
        private static Spheroid? _south_east_asia = null;
        public static Spheroid? SouthEastAsia
        {
            get
            {
                _south_east_asia ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("62c0dec3-764c-41c3-9f77-e4f55d44f103") },
                    Name = "South East Asia",
                    Description = "South East Asia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378155
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356773.32
                    },
                    IsSemiMinorAxisSet = true
                };
                _south_east_asia.Calculate();
                return _south_east_asia;
            }
        }
        #endregion

        #region spheroid Sphere
        private static Spheroid? _sphere = null;
        public static Spheroid? Sphere
        {
            get
            {
                _sphere ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("2b824542-e107-4fd5-9b93-6bcdbb962631") },
                    Name = "Sphere",
                    Description = "Sphere",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6370997
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6370997
                    },
                    IsSemiMinorAxisSet = true
                };
                _sphere.Calculate();
                return _sphere;
            }
        }
        #endregion

        #region spheroid Timbalai
        private static Spheroid? _timbalai = null;
        public static Spheroid? Timbalai
        {
            get
            {
                _timbalai ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("b434e990-d749-4322-9c3d-7d13fcaa1392") },
                    Name = "Timbalai",
                    Description = "Timbalai",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377304.063
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356097.55
                    },
                    IsSemiMinorAxisSet = true
                };
                _timbalai.Calculate();
                return _timbalai;
            }
        }
        #endregion

        #region spheroid Walbeck
        private static Spheroid? _walbeck = null;
        public static Spheroid? Walbeck
        {
            get
            {
                _walbeck ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("49f0861d-e7ec-4fdd-a8e7-3271d4c8da43") },
                    Name = "Walbeck",
                    Description = "Walbeck",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6376896
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6355934.85
                    },
                    IsSemiMinorAxisSet = true
                };
                _walbeck.Calculate();
                return _walbeck;
            }
        }
        #endregion

        #region spheroid WGS66
        private static Spheroid? _wgs66 = null;
        public static Spheroid? WGS66
        {
            get
            {
                _wgs66 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("0ebbee9e-3ff4-4229-8875-2391f16d1454") },
                    Name = "WGS66",
                    Description = "WGS66",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378145
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.25
                    },
                    IsInverseFlatteningSet = true
                };
                _wgs66.Calculate();
                return _wgs66;
            }
        }
        #endregion

        #region spheroid WGS72
        private static Spheroid? _wgs72 = null;
        public static Spheroid? WGS72
        {
            get
            {
                _wgs72 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("3942fdfe-cb56-4f2f-87be-6c0de6eeef0b") },
                    Name = "WGS72",
                    Description = "WGS72",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378135
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356750.52
                    },
                    IsSemiMinorAxisSet = true
                };
                _wgs72.Calculate();
                return _wgs72;
            }
        }
        #endregion

        #region spheroid WGS84
        private static Spheroid? _wgs84 = null;
        public static Spheroid? WGS84
        {
            get
            {
                _wgs84 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("11ebf419-eaa2-4f09-83bc-28c31e66b9d6") },
                    Name = "WGS84",
                    Description = "WGS84",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378137
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356752.3142
                    },
                    IsSemiMinorAxisSet = true
                };
                _wgs84.Calculate();
                return _wgs84;
            }
        }
        #endregion

        #region spheroid Bessel 1841
        private static Spheroid? _bessel_1841 = null;
        public static Spheroid? Bessel_1841
        {
            get
            {
                _bessel_1841 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("8c5d8e64-4c07-4fb9-9e5b-9eb6eebdbd66") },
                    Name = "Bessel (1841)",
                    Description = "Bessel (1841)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377397.155
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 299.1528128
                    },
                    IsInverseFlatteningSet = true
                };
                _bessel_1841.Calculate();
                return _bessel_1841;
            }
        }
        #endregion

        #region spheroid GRS80
        private static Spheroid? _grs80 = null;
        public static Spheroid? GRS80
        {
            get
            {
                _grs80 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("c0d4aba6-0ea0-43d3-b24a-34627abe248e") },
                    Name = "GRS80",
                    Description = "GRS80",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378137
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356752.3141
                    },
                    IsSemiMinorAxisSet = true
                };
                _grs80.Calculate();
                return _grs80;
            }
        }
        #endregion

        #region spheroid GRS67
        private static Spheroid? _grs67 = null;
        public static Spheroid? GRS67
        {
            get
            {
                _grs67 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("a664f8fa-4228-4603-9ea9-4f0060cf676a") },
                    Name = "GRS67",
                    Description = "GRS67",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378160
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356774.516
                    },
                    IsSemiMinorAxisSet = true
                };
                _grs67.Calculate();
                return _grs67;
            }
        }
        #endregion

        #region spheroid IAU76
        private static Spheroid? _iau76 = null;
        public static Spheroid? IAU76
        {
            get
            {
                _iau76 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("6bcaf330-bac8-4e83-a013-34dddb710e8a") },
                    Name = "IAU76",
                    Description = "IAU76",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378140
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.257
                    },
                    IsInverseFlatteningSet = true
                };
                _iau76.Calculate();
                return _iau76;
            }
        }
        #endregion

        #region spheroid IERS 1989
        private static Spheroid? _iers_1989 = null;
        public static Spheroid? IERS_1989
        {
            get
            {
                _iers_1989 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("e9e9d846-44ab-4fb5-9aa9-6d7538a3d75e") },
                    Name = "IERS (1989)",
                    Description = "IERS (1989)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378136
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.257
                    },
                    IsInverseFlatteningSet = true
                };
                _iers_1989.Calculate();
                return _iers_1989;
            }
        }
        #endregion

        #region spheroid IAU64
        private static Spheroid? _iau64 = null;
        public static Spheroid? IAU64
        {
            get
            {
                _iau64 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("81408958-3629-4b4c-bc3e-c3f69308ee8d") },
                    Name = "IAU64",
                    Description = "IAU64",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378160
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.25
                    },
                    IsInverseFlatteningSet = true
                };
                _iau64.Calculate();
                return _iau64;
            }
        }
        #endregion

        #region spheroid Krassovsky 1940
        private static Spheroid? _krassovsky_1940 = null;
        public static Spheroid? Krassovsky_1940
        {
            get
            {
                _krassovsky_1940 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("9644e127-f2e5-470d-b922-7491cc379079") },
                    Name = "Krassovsky (1940)",
                    Description = "Krassovsky (1940)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378245
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.3
                    },
                    IsInverseFlatteningSet = true
                };
                _krassovsky_1940.Calculate();
                return _krassovsky_1940;
            }
        }
        #endregion

        #region spheroid Airy 1830
        private static Spheroid? _airy_1830 = null;
        public static Spheroid? Airy_1830
        {
            get
            {
                _airy_1830 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("c5402ff4-9c0e-4dd1-88a5-3b7b4b020678") },
                    Name = "Airy (1830)",
                    Description = "Airy (1830)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377563.396
                    },
                    IsSemiMajorAxisSet = true,
                    SemiMinorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6356256.909
                    },
                    IsSemiMinorAxisSet = true
                };
                _airy_1830.Calculate();
                return _airy_1830;
            }
        }
        #endregion

        #region spheroid Maupertuis 1738
        private static Spheroid? _maupertuis_1738 = null;
        public static Spheroid? Maupertuis_1738
        {
            get
            {
                _maupertuis_1738 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("b56bb7f6-1f97-4a5a-8e19-86c8e2abf340") },
                    Name = "Maupertuis (1738)",
                    Description = "Maupertuis (1738)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6397300
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 191
                    },
                    IsInverseFlatteningSet = true
                };
                _maupertuis_1738.Calculate();
                return _maupertuis_1738;
            }
        }
        #endregion

        #region spheroid Bessel 1841 Namibia
        private static Spheroid? _bessel_1841_namibia = null;
        public static Spheroid? Bessel_1841_Namibia
        {
            get
            {
                _bessel_1841_namibia ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("4f90c972-658a-4d77-9a5e-03824e2f3f01") },
                    Name = "Bessel (1841) Namibia",
                    Description = "Bessel (1841) Namibia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377483.865
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 299.1528128
                    },
                    IsInverseFlatteningSet = true
                };
                _bessel_1841_namibia.Calculate();
                return _bessel_1841_namibia;
            }
        }
        #endregion

        #region spheroid Everest India 1956
        private static Spheroid? _everest_india_1956 = null;
        public static Spheroid? Everest_India_1956
        {
            get
            {
                _everest_india_1956 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("18aa784d-9b6c-4216-9861-d9d09d2dfe26") },
                    Name = "Everest (India 1956)",
                    Description = "Everest (India 1956)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377301.243
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 300.8017
                    },
                    IsInverseFlatteningSet = true
                };
                _everest_india_1956.Calculate();
                return _everest_india_1956;
            }
        }
        #endregion

        #region spheroid Everest Pakistan
        private static Spheroid? _everest_pakistan = null;
        public static Spheroid? Everest_Pakistan
        {
            get
            {
                _everest_pakistan ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("7279e5cc-cbf6-4bc0-a654-1af33aef28c9") },
                    Name = "Everest (Pakistan)",
                    Description = "Everest (Pakistan)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377309.613
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 300.8017
                    },
                    IsInverseFlatteningSet = true
                };
                _everest_pakistan.Calculate();
                return _everest_pakistan;
            }
        }
        #endregion

        #region spheroid Everest Malay and Sing
        private static Spheroid? _everest_malay_sing = null;
        public static Spheroid? Everest_Malay_Sing
        {
            get
            {
                _everest_malay_sing ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("d5c00fe9-129d-45af-82ff-ef51445a4a5b") },
                    Name = "Everest (Malay. & Sing)",
                    Description = "Everest (Malay. & Sing)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377304.063
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 300.8017
                    },
                    IsInverseFlatteningSet = true
                };
                _everest_malay_sing.Calculate();
                return _everest_malay_sing;
            }
        }
        #endregion

        #region spheroid Everest Sabah Sarawak
        private static Spheroid? _everest_sabah_sarawak = null;
        public static Spheroid? Everest_Sabah_Sarawak
        {
            get
            {
                _everest_sabah_sarawak ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("64893fc7-ea26-49ac-9ebb-be0e838b6bbb") },
                    Name = "Everest (Sabah Sarawak)",
                    Description = "Everest (Sabah Sarawak)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6377298.556
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 300.8017
                    },
                    IsInverseFlatteningSet = true
                };
                _everest_sabah_sarawak.Calculate();
                return _everest_sabah_sarawak;
            }
        }
        #endregion

        #region spheroid Indonesian 1974
        private static Spheroid? _indonesian_1974 = null;
        public static Spheroid? Indonesian_1974
        {
            get
            {
                _indonesian_1974 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("fcb2e372-e73e-4e3c-bc45-244ed5e73645") },
                    Name = "Indonesian 1974",
                    Description = "Indonesian 1974",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378160
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.247
                    },
                    IsInverseFlatteningSet = true
                };
                _indonesian_1974.Calculate();
                return _indonesian_1974;
            }
        }
        #endregion

        #region spheroid Modified Fischer 1960
        private static Spheroid? _modified_fischer_1960 = null;
        public static Spheroid? ModifiedFischer_1960
        {
            get
            {
                _modified_fischer_1960 ??= new Spheroid
                {
                    MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "Spheroid/", ID = new Guid("adf8a00e-3fbc-4526-9a54-2f4be935e0b5") },
                    Name = "Modified Fischer 1960",
                    Description = "Modified Fischer 1960",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,

                    SemiMajorAxis = new ScalarDrillingProperty()
                    {
                        ScalarValue = 6378155
                    },
                    IsSemiMajorAxisSet = true,
                    InverseFlattening = new ScalarDrillingProperty()
                    {
                        ScalarValue = 298.3
                    },
                    IsInverseFlatteningSet = true
                };
                _modified_fischer_1960.Calculate();
                return _modified_fischer_1960;
            }
        }
        #endregion
    }
}
