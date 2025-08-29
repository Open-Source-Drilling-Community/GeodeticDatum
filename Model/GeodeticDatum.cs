using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using OSDC.DotnetLibraries.General.DataManagement;
using System;

namespace NORCE.Drilling.GeodeticDatum.Model
{
    public class GeodeticDatum
    {
        /// <summary>
        /// a MetaInfo for the GeodeticDatum
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
        /// true if it is a default geodetic datum
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// an input Spheroid
        /// </summary>
        public Spheroid? Spheroid { get; set; }
        /// <summary>
        /// the X translation of the spheroid
        /// </summary>
        public ScalarDrillingProperty? DeltaX { get; set; } = null;
        /// <summary>
        /// the Y translation of the spheroid
        /// </summary>
        public ScalarDrillingProperty? DeltaY { get; set; }
        /// <summary>
        /// the Z translation of the spheroid
        /// </summary>
        public ScalarDrillingProperty? DeltaZ { get; set; }
        /// <summary>
        /// the X rotation of the spheroid
        /// </summary>
        public ScalarDrillingProperty? RotationX { get; set; }
        /// <summary>
        /// the Y rotation of the spheroid
        /// </summary>
        public ScalarDrillingProperty? RotationY { get; set; }
        /// <summary>
        /// the Z rotation of the spheroid
        /// </summary>
        public ScalarDrillingProperty? RotationZ { get; set; }
        /// <summary>
        /// the scale factor of the spheroid
        /// </summary>
        public ScalarDrillingProperty? ScaleFactor { get; set; }
        /// <summary>
        /// default constructor required for parsing the data model as a json file
        /// </summary>
        public GeodeticDatum() : base()
        {
        }

        #region geodetic datum WGS84
        private static GeodeticDatum? _wgs84 = null;
        public static GeodeticDatum? WGS84
        {
            get
            {
                _wgs84 ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("a3f1d727-4e9b-4f19-8b06-1234567890ab") },
                    Name = "WGS84",
                    Description = "WGS84",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.WGS84,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 },
                    RotationX = new () { ScalarValue = 0 },
                    RotationY = new () { ScalarValue = 0 },
                    RotationZ = new () { ScalarValue = 0 },
                    ScaleFactor = new () { ScalarValue = 1 }
                    };
                return _wgs84;
            }
        }
        #endregion

        #region geodetic datum ED50 Norway, Finland
        private static GeodeticDatum? _ed50norwayfinland = null;
        public static GeodeticDatum? ED50NorwayFinland
        {
            get
            {
                _ed50norwayfinland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b7c5f9e3-d2a1-4de5-9f8b-abcdef123456") },
                    Name = "ED50 Norway, Finland",
                    Description = "ED50 Norway, Finland",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -87.0 },
                    DeltaY = new () { ScalarValue = -95.0 },
                    DeltaZ = new () { ScalarValue = -120.0 }
                };
                return _ed50norwayfinland;
            }
        }
        #endregion

        #region geodetic datum ED50 England, Channel Islands, Scotland, Shetland Islands
        private static GeodeticDatum? _ed50englandchannelislandsscottlandshetlandislands = null;
        public static GeodeticDatum? ED50UK
        {
            get
            {
                _ed50englandchannelislandsscottlandshetlandislands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("c8d4eaf2-fa01-4ba2-9cde-001122334455") },
                    Name = "ED50 England, Channel Islands, Scotland, Shetland Islands",
                    Description = "ED50 England, Channel Islands, Scotland, Shetland Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue =  -86.0 },
                    DeltaY = new () { ScalarValue = -96.0 },
                    DeltaZ = new () { ScalarValue = -120.0 }
                };
                return _ed50englandchannelislandsscottlandshetlandislands;
            }
        }
        #endregion

        #region geodetic datum Adindan Burkina Faso
        private static GeodeticDatum? _adindanburkinafaso = null;
        public static GeodeticDatum? AdindanBurkinaFaso
        {
            get
            {
                _adindanburkinafaso ??= new()
                {
                      MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d9e3a1b7-2f34-4c5e-9e78-223344556677") },
                    Name = "Adindan Burkina Faso",
                    Description = "Adindan Burkina Faso",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -118 },
                    DeltaY = new () { ScalarValue = -14 },
                    DeltaZ = new () { ScalarValue = 218 }
                };
                return _adindanburkinafaso;
            }
        }
        #endregion

        #region geodetic datum Adindan Cameroon
        private static GeodeticDatum? _adindancameroon = null;
        public static GeodeticDatum? AdindanCameroon
        {
            get
            {
                _adindancameroon ??= new()
                {
                      MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("eaf2c3d4-5b66-4a77-9abc-334455667788") },
                    Name = "Adindan Cameroon",
                    Description = "Adindan Cameroon",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -134 },
                    DeltaY = new () { ScalarValue = -2 },
                    DeltaZ = new () { ScalarValue = 210 }
                };
                return _adindancameroon;
            }
        }
        #endregion

        #region geodetic datum Adindan Ethiopia
        private static GeodeticDatum? _adindanethiopia = null;
        public static GeodeticDatum? AdindanEthiopia
        {
            get
            {
                _adindanethiopia ??= new()
                {
                      MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f0b1c2d3-4e5f-6789-0abc-445566778899") },
                    Name = "Adindan Ethiopia",
                    Description = "Adindan Ethiopia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -165 },
                    DeltaY = new () { ScalarValue = -11 },
                    DeltaZ = new () { ScalarValue = 206 }
                };
                return _adindanethiopia;
            }
        }
        #endregion

        #region geodetic datum Adindan Mali
        private static GeodeticDatum? _adindanmali = null;
        public static GeodeticDatum? AdindanMali
        {
            get
            {
                _adindanmali ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("01234567-89ab-cdef-0123-5566778899aa") },
                    Name = "Adindan Mali",
                    Description = "Adindan Mali",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -123 },
                    DeltaY = new () { ScalarValue = -20 },
                    DeltaZ = new () { ScalarValue = 220 }
                };
                return _adindanmali;
            }
        }
        #endregion

        #region geodetic datum Adindan "MEAN FOR Ethiopia; Sudan"
        private static GeodeticDatum? _adindanMEANFOREthiopiaSudan = null;
        public static GeodeticDatum? AdindanMeanForEthiopiaSudan
        {
            get
            {
                _adindanMEANFOREthiopiaSudan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("abcdef12-3456-7890-abcd-66778899aabb") },
                    Name = "Adindan \"MEAN FOR Ethiopia; Sudan\"",
                    Description = "Adindan \"MEAN FOR Ethiopia; Sudan\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -166 },
                    DeltaY = new () { ScalarValue = -15 },
                    DeltaZ = new () { ScalarValue = 204 }
                };
                return _adindanMEANFOREthiopiaSudan;
            }
        }
        #endregion

        #region geodetic datum Adindan Senegal
        private static GeodeticDatum? _adindansenegal = null;
        public static GeodeticDatum? AdindanSenegal
        {
            get
            {
                _adindansenegal ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1234abcd-5678-ef90-1234-778899aabbcc") },
                    Name = "Adindan Senegal",
                    Description = "Adindan Senegal",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -128 },
                    DeltaY = new () { ScalarValue = -18 },
                    DeltaZ = new () { ScalarValue = 224 }
                };
                return _adindansenegal;
            }
        }
        #endregion

        #region geodetic datum Adindan Sudan
        private static GeodeticDatum? _adindansudan = null;
        public static GeodeticDatum? AdindanSudan
        {
            get
            {
                _adindansudan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("21f4a9e3-5d6b-4c78-9cda-334455667700") },
                    Name = "Adindan Sudan",
                    Description = "Adindan Sudan",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -161 },
                    DeltaY = new () { ScalarValue = -14 },
                    DeltaZ = new () { ScalarValue = 205 }
                };
                return _adindansudan;
            }
        }
        #endregion

        #region geodetic datum Afgooye Somalia
        private static GeodeticDatum? _afgooyesomalia = null;
        public static GeodeticDatum? AfgooyeSomalia
        {
            get
            {
                _afgooyesomalia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("32c5b1a4-6e7f-4a12-a9bc-556677889900") },
                    Name = "Afgooye Somalia",
                    Description = "Afgooye Somalia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue =  -43 },
                    DeltaY = new () { ScalarValue = -163 },
                    DeltaZ = new () { ScalarValue = 45 }
                };
                return _afgooyesomalia;
            }
        }
        #endregion

        #region geodetic datum Ain el Abd 1970 Bahrain
        private static GeodeticDatum? _ainelabd1970bahrain = null;
        public static GeodeticDatum? AinElAbd1970Bahrain
        {
            get
            {
                _ainelabd1970bahrain ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("43d6c2b5-7f8e-4b23-badc-667788990011") },
                    Name = "Ain el Abd 1970 Bahrain",
                    Description = "Ain el Abd 1970 Bahrain",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue =  -150 },
                    DeltaY = new () { ScalarValue = -250 },
                    DeltaZ = new () { ScalarValue = -1 }
                };
                return _ainelabd1970bahrain;
            }
        }
        #endregion

        #region geodetic datum Ain el Abd 1970 Saudi Arabia
        private static GeodeticDatum? _ainelabd1970saudiarabia = null;
        public static GeodeticDatum? AinElAbd1970SaudiArabia
        {
            get
            {
                _ainelabd1970saudiarabia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("54e7d3c6-809f-4c34-bfde-778899001122") },
                    Name = "Ain el Abd 1970 Saudi Arabia",
                    Description = "Ain el Abd 1970 Saudi Arabia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue =  -143 },
                    DeltaY = new () { ScalarValue = -236 },
                    DeltaZ = new () { ScalarValue = 7 }
                };
                return _ainelabd1970saudiarabia;
            }
        }
        #endregion

        #region geodetic datum American Samoa 1962 American Samoa Islands
        private static GeodeticDatum? _americansamoa1962americansamoaislands = null;
        public static GeodeticDatum? AmericanSamoa1962AmericanSamoaIslands
        {
            get
            {
                _americansamoa1962americansamoaislands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("65f8e4d7-91a0-4c45-c0ef-889900112233") },
                    Name = "American Samoa 1962 American Samoa Islands",
                    Description = "American Samoa 1962 American Samoa Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue =  -115 },
                    DeltaY = new () { ScalarValue = 118 },
                    DeltaZ = new () { ScalarValue = 426 }
                };
                return _americansamoa1962americansamoaislands;
            }
        }
        #endregion

        #region geodetic datum Anna 1 Astro 1965 Cocos Islands
        private static GeodeticDatum? _anna1astro1965cocosislands = null;
        public static GeodeticDatum? Anna1Astro1965CocosIslands
        {
            get
            {
                _anna1astro1965cocosislands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("76a9f5e8-a2b1-4d56-d1f0-990011223344") },
                    Name = "Anna 1 Astro 1965 Cocos Islands",
                    Description = "Anna 1 Astro 1965 Cocos Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Australian_1966,
                    DeltaX = new () { ScalarValue =  -491 },
                    DeltaY = new () { ScalarValue = -22 },
                    DeltaZ = new () { ScalarValue = 435 }
                };
                return _anna1astro1965cocosislands;
            }
        }
        #endregion

        #region geodetic datum Antigua Island Astro 1943 Antigua (Leeward Islands)
        private static GeodeticDatum? _antiguaislandastro1943antigualeewardislands = null;
        public static GeodeticDatum? AntiguaIslandAstro1943AntiguaLeewardIslands
        {
            get
            {
                _antiguaislandastro1943antigualeewardislands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("cd9c4bfb-3914-4524-afe7-77c9907c6248") },
                    Name = "Antigua Island Astro 1943 Antigua (Leeward Islands)",
                    Description = "Antigua Island Astro 1943 Antigua (Leeward Islands)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -270 },
                    DeltaY = new () { ScalarValue = 13 },
                    DeltaZ = new () { ScalarValue = 62 }
                };
                return _antiguaislandastro1943antigualeewardislands;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Botswana
        private static GeodeticDatum? _arc1950botswana = null;
        public static GeodeticDatum? Arc1950Botswana
        {
            get
            {
                _arc1950botswana ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("c119d7ef-7bab-42a6-a22b-d34c61d1a415") },
                    Name = "Arc 1950 Botswana",
                    Description = "Arc 1950 Botswana",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -138 },
                    DeltaY = new () { ScalarValue = -105 },
                    DeltaZ = new () { ScalarValue = -289 }
                };
                return _arc1950botswana;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Burundi
        private static GeodeticDatum? _arc1950burundi = null;
        public static GeodeticDatum? Arc1950Burundi
        {
            get
            {
                _arc1950burundi ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1c48e7cb-d61e-4807-a078-931cf716775d") },
                    Name = "Arc 1950 Burundi",
                    Description = "Arc 1950 Burundi",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -153 },
                    DeltaY = new () { ScalarValue = -5 },
                    DeltaZ = new () { ScalarValue = -292 }
                };
                return _arc1950burundi;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Lesotho
        private static GeodeticDatum? _arc1950lesotho = null;
        public static GeodeticDatum? Arc1950Lesotho
        {
            get
            {
                _arc1950lesotho ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0e1f2a3-0000-1111-2222-333344445555") },
                    Name = "Arc 1950 Lesotho",
                    Description = "Arc 1950 Lesotho",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -125 },
                    DeltaY = new () { ScalarValue = -108 },
                    DeltaZ = new () { ScalarValue = -295 }
                };
                return _arc1950lesotho;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Malawi
        private static GeodeticDatum? _arc1950malawi = null;
        public static GeodeticDatum? Arc1950Malawi
        {
            get
            {
                _arc1950malawi ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("e1f2a3b4-1111-2222-3333-444455556666") },
                    Name = "Arc 1950 Malawi",
                    Description = "Arc 1950 Malawi",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -161 },
                    DeltaY = new () { ScalarValue = -73 },
                    DeltaZ = new () { ScalarValue = -317 }
                };
                return _arc1950malawi;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 "MEAN FOR Botswana; Lesotho; Malawi; Swaziland; Zaire; Zambia; Zimbabwe"
        private static GeodeticDatum? _arc1950meanforbotswanalesothomalawiswazilandzairezambiazimbabwe = null;
        public static GeodeticDatum? Arc1950MeanForBotswanaLesothoMalawiSwazilandZaireZambiaZimbabwe
        {
            get
            {
                _arc1950meanforbotswanalesothomalawiswazilandzairezambiazimbabwe ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f2a3b4c5-2222-3333-4444-555566667777") },
                    Name = "Arc 1950 \"MEAN FOR Botswana; Lesotho; Malawi; Swaziland; Zaire; Zambia; Zimbabwe\"",
                    Description = "Arc 1950 \"MEAN FOR Botswana; Lesotho; Malawi; Swaziland; Zaire; Zambia; Zimbabwe\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -143 },
                    DeltaY = new () { ScalarValue = -90 },
                    DeltaZ = new () { ScalarValue = -294 }
                };
                return _arc1950meanforbotswanalesothomalawiswazilandzairezambiazimbabwe;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Swaziland
        private static GeodeticDatum? _arc1950swaziland = null;
        public static GeodeticDatum? Arc1950Swaziland
        {
            get
            {
                _arc1950swaziland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("a3b4c5d6-3333-4444-5555-666677778888") },
                    Name = "Arc 1950 Swaziland",
                    Description = "Arc 1950 Swaziland",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -134 },
                    DeltaY = new () { ScalarValue = -105 },
                    DeltaZ = new () { ScalarValue = -295 }
                };
                return _arc1950swaziland;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Zaire
        private static GeodeticDatum? _arc1950zaire = null;
        public static GeodeticDatum? Arc1950Zaire
        {
            get
            {
                _arc1950zaire ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b4c5d6e7-4444-5555-6666-777788889999") },
                    Name = "Arc 1950 Zaire",
                    Description = "Arc 1950 Zaire",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -169 },
                    DeltaY = new () { ScalarValue = -19 },
                    DeltaZ = new () { ScalarValue = -278 }
                };
                return _arc1950zaire;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Zambia
        private static GeodeticDatum? _arc1950zambia = null;
        public static GeodeticDatum? Arc1950Zambia
        {
            get
            {
                _arc1950zambia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("c5d6e7f8-5555-6666-7777-888899990000") },
                    Name = "Arc 1950 Zambia",
                    Description = "Arc 1950 Zambia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -147 },
                    DeltaY = new () { ScalarValue = -74 },
                    DeltaZ = new () { ScalarValue = -283 }
                };
                return _arc1950zambia;
            }
        }
        #endregion

        #region geodetic datum Arc 1950 Zimbabwe
        private static GeodeticDatum? _arc1950zimbabwe = null;
        public static GeodeticDatum? Arc1950Zimbabwe
        {
            get
            {
                _arc1950zimbabwe ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d6e7f8a9-6666-7777-8888-999900001111") },
                    Name = "Arc 1950 Zimbabwe",
                    Description = "Arc 1950 Zimbabwe",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue =  -142 },
                    DeltaY = new () { ScalarValue = -96 },
                    DeltaZ = new () { ScalarValue = -293 }
                };
                return _arc1950zimbabwe;
            }
        }
        #endregion

        #region Arc1960MeanForKenyaTanzania
        private static GeodeticDatum? _arc1960MeanForKenyaTanzania;
        public static GeodeticDatum Arc1960MeanForKenyaTanzania
        {
            get
            {
                _arc1960MeanForKenyaTanzania ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f1a11111-2222-3333-4444-555566667777") },
                    Name = "Arc 1960 \"MEAN FOR Kenya; Tanzania\"",
                    Description = "Arc 1960 \"MEAN FOR Kenya; Tanzania\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -160 },
                    DeltaY = new () { ScalarValue = -6 },
                    DeltaZ = new () { ScalarValue = -302 }
                };
                return _arc1960MeanForKenyaTanzania;
            }
        }
        #endregion

        #region Arc1960Kenya
        private static GeodeticDatum? _arc1960Kenya;
        public static GeodeticDatum Arc1960Kenya
        {
            get
            {
                _arc1960Kenya ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f1a22222-3333-4444-5555-666677778888") },
                    Name = "Arc 1960 Kenya",
                    Description = "Arc 1960 Kenya",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -157 },
                    DeltaY = new () { ScalarValue = -2 },
                    DeltaZ = new () { ScalarValue = -299 }
                };
                return _arc1960Kenya;
            }
        }
        #endregion

        #region Arc1960Tanzania
        private static GeodeticDatum? _arc1960Tanzania;
        public static GeodeticDatum Arc1960Tanzania
        {
            get
            {
                _arc1960Tanzania ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f1a33333-4444-5555-6666-777788889999") },
                    Name = "Arc 1960 Tanzania",
                    Description = "Arc 1960 Tanzania",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -175 },
                    DeltaY = new () { ScalarValue = -23 },
                    DeltaZ = new () { ScalarValue = -303 }
                };
                return _arc1960Tanzania;
            }
        }
        #endregion

        #region AscensionIsland1958AscensionIsland
        private static GeodeticDatum? _ascensionIsland1958AscensionIsland;
        public static GeodeticDatum AscensionIsland1958AscensionIsland
        {
            get
            {
                _ascensionIsland1958AscensionIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f1a44444-5555-6666-7777-888899990000") },
                    Name = "Ascension Island 1958 Ascension Island",
                    Description = "Ascension Island 1958 Ascension Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -205 },
                    DeltaY = new () { ScalarValue = 107 },
                    DeltaZ = new () { ScalarValue = 53 }
                };
                return _ascensionIsland1958AscensionIsland;
            }
        }
        #endregion

        #region AstroBeaconE1945IwoJima
        private static GeodeticDatum? _astroBeaconE1945IwoJima;
        public static GeodeticDatum AstroBeaconE1945IwoJima
        {
            get
            {
                _astroBeaconE1945IwoJima ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f1a55555-6666-7777-8888-99990000aaaa") },
                    Name = "Astro Beacon E 1945 Iwo Jima",
                    Description = "Astro Beacon E 1945 Iwo Jima",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 145 },
                    DeltaY = new () { ScalarValue = 75 },
                    DeltaZ = new () { ScalarValue = -272 }
                };
                return _astroBeaconE1945IwoJima;
            }
        }
        #endregion

        #region AstroDOS714StHelenaIsland
        private static GeodeticDatum? _astroDOS714StHelenaIsland;
        public static GeodeticDatum AstroDOS714StHelenaIsland
        {
            get
            {
                _astroDOS714StHelenaIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f1a66666-7777-8888-9999-aaaa11112222") },
                    Name = "Astro DOS 71/4 St Helena Island",
                    Description = "Astro DOS 71/4 St Helena Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -320 },
                    DeltaY = new () { ScalarValue = 550 },
                    DeltaZ = new () { ScalarValue = -494 }
                };
                return _astroDOS714StHelenaIsland;
            }
        }
        #endregion

        #region geodetic datum Astro Tern Island (FRIG) 1961 Tern Island
        private static GeodeticDatum? _astroTernIslandFRIG1961TernIsland;
        public static GeodeticDatum? AstroTernIslandFRIG1961TernIsland
        {
            get
            {
                _astroTernIslandFRIG1961TernIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("e8a9f5d2-4c1b-4f3a-9b0d-2c59f1a8bc47") },
                    Name = "Astro Tern Island (FRIG) 1961 Tern Island",
                    Description = "Astro Tern Island (FRIG) 1961 Tern Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 114 },
                    DeltaY = new () { ScalarValue = -116 },
                    DeltaZ = new () { ScalarValue = -333 }
                };
                return _astroTernIslandFRIG1961TernIsland;
            }
        }
        #endregion

        #region geodetic datum Astronomical Station 1952 Marcus Island
        private static GeodeticDatum? _astronomicalStation1952MarcusIsland;
        public static GeodeticDatum? AstronomicalStation1952MarcusIsland
        {
            get
            {
                _astronomicalStation1952MarcusIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("a1d3e8f4-7b92-4c6e-8d2f-b9e5c3d7a6f0") },
                    Name = "Astronomical Station 1952 Marcus Island",
                    Description = "Astronomical Station 1952 Marcus Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 124 },
                    DeltaY = new () { ScalarValue = -234 },
                    DeltaZ = new () { ScalarValue = -25 }
                };
                return _astronomicalStation1952MarcusIsland;
            }
        }
        #endregion

        #region geodetic datum Australian Geodetic 1966 "Australia; Tasmania"
        private static GeodeticDatum? _australianGeodetic1966AustraliaTasmania;
        public static GeodeticDatum? AustralianGeodetic1966AustraliaTasmania
        {
            get
            {
                _australianGeodetic1966AustraliaTasmania ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d4c7b2e1-f3a9-4d6b-8c1e-5a2f9b7e0d3c") },
                    Name = "Australian Geodetic 1966 \"Australia; Tasmania\"",
                    Description = "Australian Geodetic 1966 \"Australia; Tasmania\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Australian_1966,
                    DeltaX = new () { ScalarValue = -133 },
                    DeltaY = new () { ScalarValue = -48 },
                    DeltaZ = new () { ScalarValue = 148 }
                };
                return _australianGeodetic1966AustraliaTasmania;
            }
        }
        #endregion

        #region geodetic datum Australian Geodetic 1984 "Australia; Tasmania"
        private static GeodeticDatum? _australianGeodetic1984AustraliaTasmania;
        public static GeodeticDatum? AustralianGeodetic1984AustraliaTasmania
        {
            get
            {
                _australianGeodetic1984AustraliaTasmania ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b96e5c2f-1a4b-4e79-9d3a-6e7c8f2d0b1a") },
                    Name = "Australian Geodetic 1984 \"Australia; Tasmania\"",
                    Description = "Australian Geodetic 1984 \"Australia; Tasmania\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Australian_1966,
                    DeltaX = new () { ScalarValue = -134 },
                    DeltaY = new () { ScalarValue = -48 },
                    DeltaZ = new () { ScalarValue = 149 }
                };
                return _australianGeodetic1984AustraliaTasmania;
            }
        }
        #endregion

        #region geodetic datum Ayabelle Lighthouse Djibouti
        private static GeodeticDatum? _ayabelleLighthouseDjibouti;
        public static GeodeticDatum? AyabelleLighthouseDjibouti
        {
            get
            {
                _ayabelleLighthouseDjibouti ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("c3f2a7d8-5b1c-4e9f-8a7d-2b0e3c6f9d5a") },
                    Name = "Ayabelle Lighthouse Djibouti",
                    Description = "Ayabelle Lighthouse Djibouti",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -79 },
                    DeltaY = new () { ScalarValue = -129 },
                    DeltaZ = new () { ScalarValue = 145 }
                };
                return _ayabelleLighthouseDjibouti;
            }
        }
        #endregion

        #region geodetic datum Bellevue (IGN) Efate & Erromango Islands
        private static GeodeticDatum? _bellevueIGNEfateErromangoIslands;
        public static GeodeticDatum? BellevueIGNEfateErromangoIslands
        {
            get
            {
                _bellevueIGNEfateErromangoIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("e0d5b3c7-8f1a-4b2d-9c3e-7a5f2b1d8c9e") },
                    Name = "Bellevue (IGN) Efate & Erromango Islands",
                    Description = "Bellevue (IGN) Efate & Erromango Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -127 },
                    DeltaY = new () { ScalarValue = -769 },
                    DeltaZ = new () { ScalarValue = 472 }
                };
                return _bellevueIGNEfateErromangoIslands;
            }
        }
        #endregion

        #region geodetic datum Bermuda 1957 Bermuda
        private static GeodeticDatum? _bermuda1957Bermuda;
        public static GeodeticDatum? Bermuda1957Bermuda
        {
            get
            {
                _bermuda1957Bermuda ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("f1a2b3c4-d5e6-4f70-8a9b-0c1d2e3f4a5b") },
                    Name = "Bermuda 1957 Bermuda",
                    Description = "Bermuda 1957 Bermuda",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -73 },
                    DeltaY = new () { ScalarValue = 213 },
                    DeltaZ = new () { ScalarValue = 296 }
                };
                return _bermuda1957Bermuda;
            }
        }
        #endregion

        #region geodetic datum BissauGuineaBissau
        private static GeodeticDatum? _bissauGuineaBissau = null;
        public static GeodeticDatum? BissauGuineaBissau
        {
            get
            {
                _bissauGuineaBissau ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce101") },
                    Name = "Bissau Guinea-Bissau",
                    Description = "Bissau Guinea-Bissau",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -173 },
                    DeltaY = new () { ScalarValue = 253 },
                    DeltaZ = new () { ScalarValue = 27 }
                };
                return _bissauGuineaBissau;
            }
        }
        #endregion

        #region geodetic datum BogotaObservatoryColombia
        private static GeodeticDatum? _bogotaObservatoryColombia = null;
        public static GeodeticDatum? BogotaObservatoryColombia
        {
            get
            {
                _bogotaObservatoryColombia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce102") },
                    Name = "Bogota Observatory Colombia",
                    Description = "Bogota Observatory Colombia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 307 },
                    DeltaY = new () { ScalarValue = 304 },
                    DeltaZ = new () { ScalarValue = -318 }
                };
                return _bogotaObservatoryColombia;
            }
        }
        #endregion

        #region geodetic datum BukitRimpahIndonesiaBangkaBelitungIds
        private static GeodeticDatum? _bukitRimpahIndonesiaBangkaBelitungIds = null;
        public static GeodeticDatum? BukitRimpahIndonesiaBangkaBelitungIds
        {
            get
            {
                _bukitRimpahIndonesiaBangkaBelitungIds ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce103") },
                    Name = "Bukit Rimpah Indonesia (Bangka & Belitung Ids)",
                    Description = "Bukit Rimpah Indonesia (Bangka & Belitung Ids)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = -384 },
                    DeltaY = new () { ScalarValue = 664 },
                    DeltaZ = new () { ScalarValue = -48 }
                };
                return _bukitRimpahIndonesiaBangkaBelitungIds;
            }
        }
        #endregion

        #region geodetic datum CampAreaAstroAntarcticaMcMurdoCampArea
        private static GeodeticDatum? _campAreaAstroAntarcticaMcMurdoCampArea = null;
        public static GeodeticDatum? CampAreaAstroAntarcticaMcMurdoCampArea
        {
            get
            {
                _campAreaAstroAntarcticaMcMurdoCampArea ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce104") },
                    Name = "Camp Area Astro Antarctica (McMurdo Camp Area)",
                    Description = "Camp Area Astro Antarctica (McMurdo Camp Area)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -104 },
                    DeltaY = new () { ScalarValue = -129 },
                    DeltaZ = new () { ScalarValue = 239 }
                };
                return _campAreaAstroAntarcticaMcMurdoCampArea;
            }
        }
        #endregion

        #region geodetic datum CampoInchauspeArgentina
        private static GeodeticDatum? _campoInchauspeArgentina = null;
        public static GeodeticDatum? CampoInchauspeArgentina
        {
            get
            {
                _campoInchauspeArgentina ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce105") },
                    Name = "Campo Inchauspe Argentina",
                    Description = "Campo Inchauspe Argentina",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -148 },
                    DeltaY = new () { ScalarValue = 136 },
                    DeltaZ = new () { ScalarValue = 90 }
                };
                return _campoInchauspeArgentina;
            }
        }
        #endregion

        #region geodetic datum CantonAstro1966PhoenixIslands
        private static GeodeticDatum? _cantonAstro1966PhoenixIslands = null;
        public static GeodeticDatum? CantonAstro1966PhoenixIslands
        {
            get
            {
                _cantonAstro1966PhoenixIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce106") },
                    Name = "Canton Astro 1966 Phoenix Islands",
                    Description = "Canton Astro 1966 Phoenix Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 298 },
                    DeltaY = new () { ScalarValue = -304 },
                    DeltaZ = new () { ScalarValue = -375 }
                };
                return _cantonAstro1966PhoenixIslands;
            }
        }
        #endregion

        #region geodetic datum CapeSouthAfrica
        private static GeodeticDatum? _capeSouthAfrica = null;
        public static GeodeticDatum? CapeSouthAfrica
        {
            get
            {
                _capeSouthAfrica ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce107") },
                    Name = "Cape South Africa",
                    Description = "Cape South Africa",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -136 },
                    DeltaY = new () { ScalarValue = -108 },
                    DeltaZ = new () { ScalarValue = -292 }
                };
                return _capeSouthAfrica;
            }
        }
        #endregion

        #region geodetic datum CapeCanaveralBahamasFlorida
        private static GeodeticDatum? _capeCanaveralBahamasFlorida = null;
        public static GeodeticDatum? CapeCanaveralBahamasFlorida
        {
            get
            {
                _capeCanaveralBahamasFlorida ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce108") },
                    Name = "Cape Canaveral \"Bahamas; Florida\"",
                    Description = "Cape Canaveral \"Bahamas; Florida\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -2 },
                    DeltaY = new () { ScalarValue = 151 },
                    DeltaZ = new () { ScalarValue = 181 }
                };
                return _capeCanaveralBahamasFlorida;
            }
        }
        #endregion

        #region geodetic datum CarthageTunisia
        private static GeodeticDatum? _carthageTunisia = null;
        public static GeodeticDatum? CarthageTunisia
        {
            get
            {
                _carthageTunisia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce109") },
                    Name = "Carthage Tunisia",
                    Description = "Carthage Tunisia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -263 },
                    DeltaY = new () { ScalarValue = 6 },
                    DeltaZ = new () { ScalarValue = 431 }
                };
                return _carthageTunisia;
            }
        }
        #endregion

        #region geodetic datum ChathamIslandAstro1971NewZealandChathamIsland
        private static GeodeticDatum? _chathamIslandAstro1971NewZealandChathamIsland = null;
        public static GeodeticDatum? ChathamIslandAstro1971NewZealandChathamIsland
        {
            get
            {
                _chathamIslandAstro1971NewZealandChathamIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce10a") },
                    Name = "Chatham Island Astro 1971 New Zealand (Chatham Island)",
                    Description = "Chatham Island Astro 1971 New Zealand (Chatham Island)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 175 },
                    DeltaY = new () { ScalarValue = -38 },
                    DeltaZ = new () { ScalarValue = 113 }
                };
                return _chathamIslandAstro1971NewZealandChathamIsland;
            }
        }
        #endregion

        #region geodetic datum ChuaAstroParaguay
        private static GeodeticDatum? _chuaAstroParaguay = null;
        public static GeodeticDatum? ChuaAstroParaguay
        {
            get
            {
                _chuaAstroParaguay ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce10b") },
                    Name = "Chua Astro Paraguay",
                    Description = "Chua Astro Paraguay",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -134 },
                    DeltaY = new () { ScalarValue = 229 },
                    DeltaZ = new () { ScalarValue = -29 }
                };
                return _chuaAstroParaguay;
            }
        }
        #endregion

        #region geodetic datum CorregoAlegreBrazil
        private static GeodeticDatum? _corregoAlegreBrazil = null;
        public static GeodeticDatum? CorregoAlegreBrazil
        {
            get
            {
                _corregoAlegreBrazil ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce10c") },
                    Name = "Corrego Alegre Brazil",
                    Description = "Corrego Alegre Brazil",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -206 },
                    DeltaY = new () { ScalarValue = 172 },
                    DeltaZ = new () { ScalarValue = -6 }
                };
                return _corregoAlegreBrazil;
            }
        }
        #endregion

        #region geodetic datum DabolaGuinea
        private static GeodeticDatum? _dabolaGuinea = null;
        public static GeodeticDatum? DabolaGuinea
        {
            get
            {
                _dabolaGuinea ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce10d") },
                    Name = "Dabola Guinea",
                    Description = "Dabola Guinea",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -83 },
                    DeltaY = new () { ScalarValue = 37 },
                    DeltaZ = new () { ScalarValue = 124 }
                };
                return _dabolaGuinea;
            }
        }
        #endregion

        #region geodetic datum DeceptionIslandDeceptionIslandAntarctia
        private static GeodeticDatum? _deceptionIslandDeceptionIslandAntarctia = null;
        public static GeodeticDatum? DeceptionIslandDeceptionIslandAntarctia
        {
            get
            {
                _deceptionIslandDeceptionIslandAntarctia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce10e") },
                    Name = "Deception Island \"Deception Island; Antarctia\"",
                    Description = "Deception Island \"Deception Island; Antarctia\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = 260 },
                    DeltaY = new () { ScalarValue = 12 },
                    DeltaZ = new () { ScalarValue = -147 }
                };
                return _deceptionIslandDeceptionIslandAntarctia;
            }
        }
        #endregion

        #region geodetic datum DjakartaBataviaIndonesiaSumatra
        private static GeodeticDatum? _djakartaBataviaIndonesiaSumatra = null;
        public static GeodeticDatum? DjakartaBataviaIndonesiaSumatra
        {
            get
            {
                _djakartaBataviaIndonesiaSumatra ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce10f") },
                    Name = "Djakarta (Batavia) Indonesia (Sumatra)",
                    Description = "Djakarta (Batavia) Indonesia (Sumatra)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = -377 },
                    DeltaY = new () { ScalarValue = 681 },
                    DeltaZ = new () { ScalarValue = -50 }
                };
                return _djakartaBataviaIndonesiaSumatra;
            }
        }
        #endregion

        #region geodetic datum DOS1968NewGeorgiaIslandsGizoIsland
        private static GeodeticDatum? _dos1968NewGeorgiaIslandsGizoIsland = null;
        public static GeodeticDatum? DOS1968NewGeorgiaIslandsGizoIsland
        {
            get
            {
                _dos1968NewGeorgiaIslandsGizoIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce110") },
                    Name = "DOS 1968 New Georgia Islands (Gizo Island)",
                    Description = "DOS 1968 New Georgia Islands (Gizo Island)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 230 },
                    DeltaY = new () { ScalarValue = -199 },
                    DeltaZ = new () { ScalarValue = -752 }
                };
                return _dos1968NewGeorgiaIslandsGizoIsland;
            }
        }
        #endregion

        #region geodetic datum EasterIsland1967EasterIsland
        private static GeodeticDatum? _easterIsland1967EasterIsland = null;
        public static GeodeticDatum? EasterIsland1967EasterIsland
        {
            get
            {
                _easterIsland1967EasterIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce111") },
                    Name = "Easter Island 1967 Easter Island",
                    Description = "Easter Island 1967 Easter Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 211 },
                    DeltaY = new () { ScalarValue = 147 },
                    DeltaZ = new () { ScalarValue = 111 }
                };
                return _easterIsland1967EasterIsland;
            }
        }
        #endregion

        #region geodetic datum EstoniaCoordinateSystem1937Estonia
        private static GeodeticDatum? _estoniaCoordinateSystem1937Estonia = null;
        public static GeodeticDatum? EstoniaCoordinateSystem1937Estonia
        {
            get
            {
                _estoniaCoordinateSystem1937Estonia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce112") },
                    Name = "\"Estonia; Coordinate System 1937\" Estonia",
                    Description = "\"Estonia; Coordinate System 1937\" Estonia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = 374 },
                    DeltaY = new () { ScalarValue = 150 },
                    DeltaZ = new () { ScalarValue = 588 }
                };
                return _estoniaCoordinateSystem1937Estonia;
            }
        }
        #endregion

        #region geodetic datum European1950Cyprus
        private static GeodeticDatum? _european1950Cyprus = null;
        public static GeodeticDatum? European1950Cyprus
        {
            get
            {
                _european1950Cyprus ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce113") },
                    Name = "European 1950 Cyprus",
                    Description = "European 1950 Cyprus",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -104 },
                    DeltaY = new () { ScalarValue = -101 },
                    DeltaZ = new () { ScalarValue = -140 }
                };
                return _european1950Cyprus;
            }
        }
        #endregion

        #region geodetic datum European1950Egypt
        private static GeodeticDatum? _european1950Egypt = null;
        public static GeodeticDatum? European1950Egypt
        {
            get
            {
                _european1950Egypt ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce114") },
                    Name = "European 1950 Egypt",
                    Description = "European 1950 Egypt",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -130 },
                    DeltaY = new () { ScalarValue = -117 },
                    DeltaZ = new () { ScalarValue = -151 }
                };
                return _european1950Egypt;
            }
        }
        #endregion

        #region geodetic datum European1950EnglandIrelandScotlandShetlandIslands
        private static GeodeticDatum? _european1950EnglandIrelandScotlandShetlandIslands = null;
        public static GeodeticDatum? European1950EnglandIrelandScotlandShetlandIslands
        {
            get
            {
                _european1950EnglandIrelandScotlandShetlandIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce115") },
                    Name = "European 1950 \"England; Ireland; Scotland; Shetland Islands\"",
                    Description = "European 1950 \"England; Ireland; Scotland; Shetland Islands\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -86 },
                    DeltaY = new () { ScalarValue = -96 },
                    DeltaZ = new () { ScalarValue = -120 }
                };
                return _european1950EnglandIrelandScotlandShetlandIslands;
            }
        }
        #endregion

        #region geodetic datum European1950Greece
        private static GeodeticDatum? _european1950Greece = null;
        public static GeodeticDatum? European1950Greece
        {
            get
            {
                _european1950Greece ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce116") },
                    Name = "European 1950 Greece",
                    Description = "European 1950 Greece",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -84 },
                    DeltaY = new () { ScalarValue = -95 },
                    DeltaZ = new () { ScalarValue = -130 }
                };
                return _european1950Greece;
            }
        }
        #endregion

        #region geodetic datum European1950Iran
        private static GeodeticDatum? _european1950Iran = null;
        public static GeodeticDatum? European1950Iran
        {
            get
            {
                _european1950Iran ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce117") },
                    Name = "European 1950 Iran",
                    Description = "European 1950 Iran",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -117 },
                    DeltaY = new () { ScalarValue = -132 },
                    DeltaZ = new () { ScalarValue = -164 }
                };
                return _european1950Iran;
            }
        }
        #endregion

        #region geodetic datum European1950ItalySardinia
        private static GeodeticDatum? _european1950ItalySardinia = null;
        public static GeodeticDatum? European1950ItalySardinia
        {
            get
            {
                _european1950ItalySardinia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce118") },
                    Name = "European 1950 Italy (Sardinia)",
                    Description = "European 1950 Italy (Sardinia)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -97 },
                    DeltaY = new () { ScalarValue = -103 },
                    DeltaZ = new () { ScalarValue = -120 }
                };
                return _european1950ItalySardinia;
            }
        }
        #endregion

        #region geodetic datum European1950ItalySicily
        private static GeodeticDatum? _european1950ItalySicily = null;
        public static GeodeticDatum? European1950ItalySicily
        {
            get
            {
                _european1950ItalySicily ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce119") },
                    Name = "European 1950 Italy (Sicily)",
                    Description = "European 1950 Italy (Sicily)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -97 },
                    DeltaY = new () { ScalarValue = -88 },
                    DeltaZ = new () { ScalarValue = -135 }
                };
                return _european1950ItalySicily;
            }
        }
        #endregion

        #region geodetic datum European1950Malta
        private static GeodeticDatum? _european1950Malta = null;
        public static GeodeticDatum? European1950Malta
        {
            get
            {
                _european1950Malta ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce11a") },
                    Name = "European 1950 Malta",
                    Description = "European 1950 Malta",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -107 },
                    DeltaY = new () { ScalarValue = -88 },
                    DeltaZ = new () { ScalarValue = -149 }
                };
                return _european1950Malta;
            }
        }
        #endregion

        #region geodetic datum European1950MeanForAustriaBelgiumDenmarkFinlandFranceWGermanyGibraltarGreeceItalyLuxembourgNetherlandsNorwayPortugalSpainSwedenSwitzerland
        private static GeodeticDatum? _european1950MeanForAustriaBelgiumDenmarkFinlandFranceWGermanyGibraltarGreeceItalyLuxembourgNetherlandsNorwayPortugalSpainSwedenSwitzerland = null;
        public static GeodeticDatum? European1950MeanForAustriaBelgiumDenmarkFinlandFranceWGermanyGibraltarGreeceItalyLuxembourgNetherlandsNorwayPortugalSpainSwedenSwitzerland
        {
            get
            {
                _european1950MeanForAustriaBelgiumDenmarkFinlandFranceWGermanyGibraltarGreeceItalyLuxembourgNetherlandsNorwayPortugalSpainSwedenSwitzerland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce11b") },
                    Name = "European 1950 \"MEAN FOR Austria; Belgium; Denmark; Finland; France; W Germany; Gibraltar; Greece; Italy; Luxembourg; Netherlands; Norway; Portugal; Spain; Sweden; Switzerland\"",
                    Description = "European 1950 \"MEAN FOR Austria; Belgium; Denmark; Finland; France; W Germany; Gibraltar; Greece; Italy; Luxembourg; Netherlands; Norway; Portugal; Spain; Sweden; Switzerland\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -87 },
                    DeltaY = new () { ScalarValue = -98 },
                    DeltaZ = new () { ScalarValue = -121 }
                };
                return _european1950MeanForAustriaBelgiumDenmarkFinlandFranceWGermanyGibraltarGreeceItalyLuxembourgNetherlandsNorwayPortugalSpainSwedenSwitzerland;
            }
        }
        #endregion

        #region geodetic datum European1950MeanForAustriaDenmarkFranceWGermanyNetherlandsSwitzerland
        private static GeodeticDatum? _european1950MeanForAustriaDenmarkFranceWGermanyNetherlandsSwitzerland = null;
        public static GeodeticDatum? European1950MeanForAustriaDenmarkFranceWGermanyNetherlandsSwitzerland
        {
            get
            {
                _european1950MeanForAustriaDenmarkFranceWGermanyNetherlandsSwitzerland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce11c") },
                    Name = "European 1950 \"MEAN FOR Austria; Denmark; France; W Germany; Netherlands; Switzerland\"",
                    Description = "European 1950 \"MEAN FOR Austria; Denmark; France; W Germany; Netherlands; Switzerland\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -87 },
                    DeltaY = new () { ScalarValue = -96 },
                    DeltaZ = new () { ScalarValue = -120 }
                };
                return _european1950MeanForAustriaDenmarkFranceWGermanyNetherlandsSwitzerland;
            }
        }
        #endregion

        #region geodetic datum European1950MeanForIraqIsraelJordanLebanonKuwaitSaudiArabiaSyria
        private static GeodeticDatum? _european1950MeanForIraqIsraelJordanLebanonKuwaitSaudiArabiaSyria = null;
        public static GeodeticDatum? European1950MeanForIraqIsraelJordanLebanonKuwaitSaudiArabiaSyria
        {
            get
            {
                _european1950MeanForIraqIsraelJordanLebanonKuwaitSaudiArabiaSyria ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("1d2437b6-ff42-4b62-8d07-fdc1e91ce11d") },
                    Name = "European 1950 \"MEAN FOR Iraq; Israel; Jordan; Lebanon; Kuwait; Saudi Arabia; Syria\"",
                    Description = "European 1950 \"MEAN FOR Iraq; Israel; Jordan; Lebanon; Kuwait; Saudi Arabia; Syria\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -103 },
                    DeltaY = new () { ScalarValue = -106 },
                    DeltaZ = new () { ScalarValue = -141 }
                };
                return _european1950MeanForIraqIsraelJordanLebanonKuwaitSaudiArabiaSyria;
            }
        }
        #endregion

        #region geodetic datum European1950PortugalSpain
        private static GeodeticDatum? _european1950PortugalSpain = null;
        public static GeodeticDatum? European1950PortugalSpain
        {
            get
            {
                _european1950PortugalSpain ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("2a5b7f90-1e2a-4cb8-a1d5-01f8c9b4d101") },
                    Name = "European 1950 \"Portugal; Spain\"",
                    Description = "European 1950 \"Portugal; Spain\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -84 },
                    DeltaY = new () { ScalarValue = -107 },
                    DeltaZ = new () { ScalarValue = -120 }
                };
                return _european1950PortugalSpain;
            }
        }
        #endregion

        #region geodetic datum European1950Tunisia
        private static GeodeticDatum? _european1950Tunisia = null;
        public static GeodeticDatum? European1950Tunisia
        {
            get
            {
                _european1950Tunisia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("3c9d8b71-2c34-427e-9fbe-02e3a4c5e202") },
                    Name = "European 1950 Tunisia",
                    Description = "European 1950 Tunisia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -112 },
                    DeltaY = new () { ScalarValue = -77 },
                    DeltaZ = new () { ScalarValue = -145 }
                };
                return _european1950Tunisia;
            }
        }
        #endregion

        #region geodetic datum European1979MeanForAustriaFinlandNetherlandsNorwaySpainSwedenSwitzerland
        private static GeodeticDatum? _european1979MeanForAustriaFinlandNetherlandsNorwaySpainSwedenSwitzerland = null;
        public static GeodeticDatum? European1979MeanForAustriaFinlandNetherlandsNorwaySpainSwedenSwitzerland
        {
            get
            {
                _european1979MeanForAustriaFinlandNetherlandsNorwaySpainSwedenSwitzerland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("4d0ebb12-3f56-4f21-8c85-13b5d6e7f303") },
                    Name = "European 1979 \"MEAN FOR Austria; Finland; Netherlands; Norway; Spain; Sweden; Switzerland\"",
                    Description = "European 1979 \"MEAN FOR Austria; Finland; Netherlands; Norway; Spain; Sweden; Switzerland\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -86 },
                    DeltaY = new () { ScalarValue = -98 },
                    DeltaZ = new () { ScalarValue = -119 }
                };
                return _european1979MeanForAustriaFinlandNetherlandsNorwaySpainSwedenSwitzerland;
            }
        }
        #endregion

        #region geodetic datum FortThomas1955NevisStKittsLeewardIslands
        private static GeodeticDatum? _fortThomas1955NevisStKittsLeewardIslands = null;
        public static GeodeticDatum? FortThomas1955NevisStKittsLeewardIslands
        {
            get
            {
                _fortThomas1955NevisStKittsLeewardIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("55afbe64-4a78-407a-9d76-24c8f7a9b404") },
                    Name = "Fort Thomas 1955 \"Nevis; St. Kitts (Leeward Islands)\"",
                    Description = "Fort Thomas 1955 \"Nevis; St. Kitts (Leeward Islands)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -7 },
                    DeltaY = new () { ScalarValue = 215 },
                    DeltaZ = new () { ScalarValue = 225 }
                };
                return _fortThomas1955NevisStKittsLeewardIslands;
            }
        }
        #endregion

        #region geodetic datum Gan1970RepublicOfMaldives
        private static GeodeticDatum? _gan1970RepublicOfMaldives = null;
        public static GeodeticDatum? Gan1970RepublicOfMaldives
        {
            get
            {
                _gan1970RepublicOfMaldives ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("66c0dc15-5b9a-4ad2-8e67-35d9f8bac505") },
                    Name = "Gan 1970 Republic of Maldives",
                    Description = "Gan 1970 Republic of Maldives",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -133 },
                    DeltaY = new () { ScalarValue = -321 },
                    DeltaZ = new () { ScalarValue = 50 }
                };
                return _gan1970RepublicOfMaldives;
            }
        }
        #endregion

        #region geodetic datum GeodeticDatum1949NewZealand
        private static GeodeticDatum? _geodeticDatum1949NewZealand = null;
        public static GeodeticDatum? GeodeticDatum1949NewZealand
        {
            get
            {
                _geodeticDatum1949NewZealand ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("77d1ed26-6cba-40c3-9f58-46eaf9cbd606") },
                    Name = "Geodetic Datum 1949 New Zealand",
                    Description = "Geodetic Datum 1949 New Zealand",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 84 },
                    DeltaY = new () { ScalarValue = -22 },
                    DeltaZ = new () { ScalarValue = 209 }
                };
                return _geodeticDatum1949NewZealand;
            }
        }
        #endregion

        #region geodetic datum GraciosaBaseSW1948AzoresFaialGraciosaPicoSaoJorgeTerceira
        private static GeodeticDatum? _graciosaBaseSW1948AzoresFaialGraciosaPicoSaoJorgeTerceira = null;
        public static GeodeticDatum? GraciosaBaseSW1948AzoresFaialGraciosaPicoSaoJorgeTerceira
        {
            get
            {
                _graciosaBaseSW1948AzoresFaialGraciosaPicoSaoJorgeTerceira ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("88e2fe37-7dcb-40d4-8059-57fb0adce707") },
                    Name = "Graciosa Base SW 1948 \"Azores (Faial; Graciosa; Pico; Sao Jorge; Terceira)\"",
                    Description = "Graciosa Base SW 1948 \"Azores (Faial; Graciosa; Pico; Sao Jorge; Terceira)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -104 },
                    DeltaY = new () { ScalarValue = 167 },
                    DeltaZ = new () { ScalarValue = -38 }
                };
                return _graciosaBaseSW1948AzoresFaialGraciosaPicoSaoJorgeTerceira;
            }
        }
        #endregion

        #region geodetic datum Guam1963Guam
        private static GeodeticDatum? _guam1963Guam = null;
        public static GeodeticDatum? Guam1963Guam
        {
            get
            {
                _guam1963Guam ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("99f30f48-8edc-4195-9e4a-68fc1bedf808") },
                    Name = "Guam 1963 Guam",
                    Description = "Guam 1963 Guam",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -100 },
                    DeltaY = new () { ScalarValue = -248 },
                    DeltaZ = new () { ScalarValue = 259 }
                };
                return _guam1963Guam;
            }
        }
        #endregion

        #region geodetic datum GunungSegaraIndonesiaKalimantan
        private static GeodeticDatum? _gunungSegaraIndonesiaKalimantan = null;
        public static GeodeticDatum? GunungSegaraIndonesiaKalimantan
        {
            get
            {
                _gunungSegaraIndonesiaKalimantan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("aaf42159-9fed-42a6-8f3b-79ed2cfe0919") },
                    Name = "Gunung Segara Indonesia (Kalimantan)",
                    Description = "Gunung Segara Indonesia (Kalimantan)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = -403 },
                    DeltaY = new () { ScalarValue = 684 },
                    DeltaZ = new () { ScalarValue = 41 }
                };
                return _gunungSegaraIndonesiaKalimantan;
            }
        }
        #endregion

        #region geodetic datum GUX1AstroGuadalcanalIsland
        private static GeodeticDatum? _gux1AstroGuadalcanalIsland = null;
        public static GeodeticDatum? GUX1AstroGuadalcanalIsland
        {
            get
            {
                _gux1AstroGuadalcanalIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("bb05426a-af0e-43b7-9a2c-8afe3d0f1a1a") },
                    Name = "GUX 1 Astro Guadalcanal Island",
                    Description = "GUX 1 Astro Guadalcanal Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 252 },
                    DeltaY = new () { ScalarValue = -209 },
                    DeltaZ = new () { ScalarValue = -751 }
                };
                return _gux1AstroGuadalcanalIsland;
            }
        }
        #endregion

        #region geodetic datum HeratNorthAfghanistan
        private static GeodeticDatum? _heratNorthAfghanistan = null;
        public static GeodeticDatum? HeratNorthAfghanistan
        {
            get
            {
                _heratNorthAfghanistan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("cc16347b-b02f-44c8-8b1d-9b0f4e1f2b2b") },
                    Name = "Herat North Afghanistan",
                    Description = "Herat North Afghanistan",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -333 },
                    DeltaY = new () { ScalarValue = -222 },
                    DeltaZ = new () { ScalarValue = 114 }
                };
                return _heratNorthAfghanistan;
            }
        }
        #endregion

        #region geodetic datum HermannskogelDatumCroatiaSerbiaBosniaHerzegovina
        private static GeodeticDatum? _hermannskogelDatumCroatiaSerbiaBosniaHerzegovina = null;
        public static GeodeticDatum? HermannskogelDatumCroatiaSerbiaBosniaHerzegovina
        {
            get
            {
                _hermannskogelDatumCroatiaSerbiaBosniaHerzegovina ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("dd27558c-c140-45d9-992e-ac104f203c3c") },
                    Name = "Hermannskogel Datum Croatia -Serbia, Bosnia-Herzegovina",
                    Description = "Hermannskogel Datum Croatia -Serbia, Bosnia-Herzegovina",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841_Namibia,
                    DeltaX = new () { ScalarValue = 653 },
                    DeltaY = new () { ScalarValue = -212 },
                    DeltaZ = new () { ScalarValue = 449 }
                };
                return _hermannskogelDatumCroatiaSerbiaBosniaHerzegovina;
            }
        }
        #endregion

        #region geodetic datum Hjorsey1955Iceland
        private static GeodeticDatum? _hjorsey1955Iceland = null;
        public static GeodeticDatum? Hjorsey1955Iceland
        {
            get
            {
                _hjorsey1955Iceland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("ee38769d-d251-46ea-983f-bd2150314d4d") },
                    Name = "Hjorsey 1955 Iceland",
                    Description = "Hjorsey 1955 Iceland",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -73 },
                    DeltaY = new () { ScalarValue = 46 },
                    DeltaZ = new () { ScalarValue = -86 }
                };
                return _hjorsey1955Iceland;
            }
        }
        #endregion

        #region geodetic datum HongKong1963HongKong
        private static GeodeticDatum? _hongKong1963HongKong = null;
        public static GeodeticDatum? HongKong1963HongKong
        {
            get
            {
                _hongKong1963HongKong ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("ff4987ae-e362-47fb-9740-ce3261425e5e") },
                    Name = "Hong Kong 1963 Hong Kong",
                    Description = "Hong Kong 1963 Hong Kong",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -156 },
                    DeltaY = new () { ScalarValue = -271 },
                    DeltaZ = new () { ScalarValue = -189 }
                };
                return _hongKong1963HongKong;
            }
        }
        #endregion

        #region geodetic datum HuTzuShanTaiwan
        private static GeodeticDatum? _huTzuShanTaiwan = null;
        public static GeodeticDatum? HuTzuShanTaiwan
        {
            get
            {
                _huTzuShanTaiwan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("001a98bf-f473-480c-9651-df4372536f6f") },
                    Name = "Hu-Tzu-Shan Taiwan",
                    Description = "Hu-Tzu-Shan Taiwan",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -637 },
                    DeltaY = new () { ScalarValue = -549 },
                    DeltaZ = new () { ScalarValue = -203 }
                };
                return _huTzuShanTaiwan;
            }
        }
        #endregion

        #region geodetic datum IndianBangladesh
        private static GeodeticDatum? _indianBangladesh = null;
        public static GeodeticDatum? IndianBangladesh
        {
            get
            {
                _indianBangladesh ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("112ba9d0-0644-491d-9562-e05483648080") },
                    Name = "Indian Bangladesh",
                    Description = "Indian Bangladesh",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_1830,
                    DeltaX = new () { ScalarValue = 282 },
                    DeltaY = new () { ScalarValue = 726 },
                    DeltaZ = new () { ScalarValue = 254 }
                };
                return _indianBangladesh;
            }
        }
        #endregion

        #region geodetic datum IndianIndiaNepal
        private static GeodeticDatum? _indianIndiaNepal = null;
        public static GeodeticDatum? IndianIndiaNepal
        {
            get
            {
                _indianIndiaNepal ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("223cbae1-1755-4a2e-9473-f16594759191") },
                    Name = "Indian \"India; Nepal\"",
                    Description = "Indian \"India; Nepal\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_India_1956,
                    DeltaX = new () { ScalarValue = 295 },
                    DeltaY = new () { ScalarValue = 736 },
                    DeltaZ = new () { ScalarValue = 257 }
                };
                return _indianIndiaNepal;
            }
        }
        #endregion

        #region geodetic datum IndianPakistan
        private static GeodeticDatum? _indianPakistan = null;
        public static GeodeticDatum? IndianPakistan
        {
            get
            {
                _indianPakistan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("334dbbf2-2866-4b3f-8a84-0266a586a2a2") },
                    Name = "Indian Pakistan",
                    Description = "Indian Pakistan",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_Pakistan,
                    DeltaX = new () { ScalarValue = 283 },
                    DeltaY = new () { ScalarValue = 682 },
                    DeltaZ = new () { ScalarValue = 231 }
                };
                return _indianPakistan;
            }
        }
        #endregion

        #region geodetic datum Indian1954Thailand
        private static GeodeticDatum? _indian1954Thailand = null;
        public static GeodeticDatum? Indian1954Thailand
        {
            get
            {
                _indian1954Thailand ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("445ecc03-3977-4c50-9a95-1377b697b3b3") },
                    Name = "Indian 1954 Thailand",
                    Description = "Indian 1954 Thailand",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_1830,
                    DeltaX = new () { ScalarValue = 217 },
                    DeltaY = new () { ScalarValue = 823 },
                    DeltaZ = new () { ScalarValue = 299 }
                };
                return _indian1954Thailand;
            }
        }
        #endregion

        #region geodetic datum Indian1960VietnamConSonIsland
        private static GeodeticDatum? _indian1960VietnamConSonIsland = null;
        public static GeodeticDatum? Indian1960VietnamConSonIsland
        {
            get
            {
                _indian1960VietnamConSonIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("556fdd14-4a88-4d61-8ca6-2488c7a8c4c4") },
                    Name = "Indian 1960 Vietnam (Con Son Island)",
                    Description = "Indian 1960 Vietnam (Con Son Island)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_1830,
                    DeltaX = new () { ScalarValue = 182 },
                    DeltaY = new () { ScalarValue = 915 },
                    DeltaZ = new () { ScalarValue = 344 }
                };
                return _indian1960VietnamConSonIsland;
            }
        }
        #endregion

        #region geodetic datum Indian1960VietnamNear16N
        private static GeodeticDatum? _indian1960VietnamNear16N = null;
        public static GeodeticDatum? Indian1960VietnamNear16N
        {
            get
            {
                _indian1960VietnamNear16N ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6670ee25-5b99-4e72-9db7-3599d8b9d5d5") },
                    Name = "Indian 1960 Vietnam (Near 16N)",
                    Description = "Indian 1960 Vietnam (Near 16N)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_1830,
                    DeltaX = new () { ScalarValue = 198 },
                    DeltaY = new () { ScalarValue = 881 },
                    DeltaZ = new () { ScalarValue = 317 }
                };
                return _indian1960VietnamNear16N;
            }
        }
        #endregion

        #region geodetic datum Indian1975Thailand
        private static GeodeticDatum? _indian1975Thailand = null;
        public static GeodeticDatum? Indian1975Thailand
        {
            get
            {
                _indian1975Thailand ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("7781ff36-6caa-4f83-8ec8-46aadecac6d6") },
                    Name = "Indian 1975 Thailand",
                    Description = "Indian 1975 Thailand",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_1830,
                    DeltaX = new () { ScalarValue = 210 },
                    DeltaY = new () { ScalarValue = 814 },
                    DeltaZ = new () { ScalarValue = 289 }
                };
                return _indian1975Thailand;
            }
        }
        #endregion

        #region geodetic datum Indonesian1974Indonesia
        private static GeodeticDatum? _indonesian1974Indonesia = null;
        public static GeodeticDatum? Indonesian1974Indonesia
        {
            get
            {
                _indonesian1974Indonesia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("88921047-7dbb-4094-9fd9-57bbefdbe7e7") },
                    Name = "Indonesian 1974 Indonesia",
                    Description = "Indonesian 1974 Indonesia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Indonesian_1974,
                    DeltaX = new () { ScalarValue = -24 },
                    DeltaY = new () { ScalarValue = -15 },
                    DeltaZ = new () { ScalarValue = 5 }
                };
                return _indonesian1974Indonesia;
            }
        }
        #endregion

        #region geodetic datum Ireland1965Ireland
        private static GeodeticDatum? _ireland1965Ireland = null;
        public static GeodeticDatum? Ireland1965Ireland
        {
            get
            {
                _ireland1965Ireland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("99a32158-8ecc-41a5-8fea-68cc0fecf8f8") },
                    Name = "Ireland 1965 Ireland",
                    Description = "Ireland 1965 Ireland",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.ModifiedAiry,
                    DeltaX = new () { ScalarValue = 506 },
                    DeltaY = new () { ScalarValue = -122 },
                    DeltaZ = new () { ScalarValue = 611 }
                };
                return _ireland1965Ireland;
            }
        }
        #endregion

        #region geodetic datum ISTS061Astro1968SouthGeorgiaIslands
        private static GeodeticDatum? _ists061Astro1968SouthGeorgiaIslands = null;
        public static GeodeticDatum? ISTS061Astro1968SouthGeorgiaIslands
        {
            get
            {
                _ists061Astro1968SouthGeorgiaIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("aab43269-9fdd-42b6-9afb-79dd10fd0909") },
                    Name = "ISTS 061 Astro 1968 South Georgia Islands",
                    Description = "ISTS 061 Astro 1968 South Georgia Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -794 },
                    DeltaY = new () { ScalarValue = 119 },
                    DeltaZ = new () { ScalarValue = -298 }
                };
                return _ists061Astro1968SouthGeorgiaIslands;
            }
        }
        #endregion

        #region geodetic datum ISTS073Astro1969DiegoGarcia
        private static GeodeticDatum? _ists073Astro1969DiegoGarcia = null;
        public static GeodeticDatum? ISTS073Astro1969DiegoGarcia
        {
            get
            {
                _ists073Astro1969DiegoGarcia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e501") },
                    Name = "ISTS 073 Astro 1969 Diego Garcia",
                    Description = "ISTS 073 Astro 1969 Diego Garcia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 208 },
                    DeltaY = new () { ScalarValue = -435 },
                    DeltaZ = new () { ScalarValue = -229 }
                };
                return _ists073Astro1969DiegoGarcia;
            }
        }
        #endregion

        #region geodetic datum JohnstonIsland1961JohnstonIsland
        private static GeodeticDatum? _johnstonIsland1961JohnstonIsland = null;
        public static GeodeticDatum? JohnstonIsland1961JohnstonIsland
        {
            get
            {
                _johnstonIsland1961JohnstonIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e502") },
                    Name = "Johnston Island 1961 Johnston Island",
                    Description = "Johnston Island 1961 Johnston Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 189 },
                    DeltaY = new () { ScalarValue = -79 },
                    DeltaZ = new () { ScalarValue = -202 }
                };
                return _johnstonIsland1961JohnstonIsland;
            }
        }
        #endregion

        #region geodetic datum KandawalaSriLanka
        private static GeodeticDatum? _kandawalaSriLanka = null;
        public static GeodeticDatum? KandawalaSriLanka
        {
            get
            {
                _kandawalaSriLanka ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e503") },
                    Name = "Kandawala Sri Lanka",
                    Description = "Kandawala Sri Lanka",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_1830,
                    DeltaX = new () { ScalarValue = -97 },
                    DeltaY = new () { ScalarValue = 787 },
                    DeltaZ = new () { ScalarValue = 86 }
                };
                return _kandawalaSriLanka;
            }
        }
        #endregion

        #region geodetic datum KerguelenIsland1949KerguelenIsland
        private static GeodeticDatum? _kerguelenIsland1949KerguelenIsland = null;
        public static GeodeticDatum? KerguelenIsland1949KerguelenIsland
        {
            get
            {
                _kerguelenIsland1949KerguelenIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e504") },
                    Name = "Kerguelen Island 1949 Kerguelen Island",
                    Description = "Kerguelen Island 1949 Kerguelen Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 145 },
                    DeltaY = new () { ScalarValue = -187 },
                    DeltaZ = new () { ScalarValue = 103 }
                };
                return _kerguelenIsland1949KerguelenIsland;
            }
        }
        #endregion

        #region geodetic datum Kertau1948WestMalaysiaSingapore
        private static GeodeticDatum? _kertau1948WestMalaysiaSingapore = null;
        public static GeodeticDatum? Kertau1948WestMalaysiaSingapore
        {
            get
            {
                _kertau1948WestMalaysiaSingapore ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e505") },
                    Name = "Kertau 1948 West Malaysia & Singapore",
                    Description = "Kertau 1948 West Malaysia & Singapore",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_Malay_Sing,
                    DeltaX = new () { ScalarValue = -11 },
                    DeltaY = new () { ScalarValue = 851 },
                    DeltaZ = new () { ScalarValue = 5 }
                };
                return _kertau1948WestMalaysiaSingapore;
            }
        }
        #endregion

        #region geodetic datum KusaieAstro1951CarolineIslands
        private static GeodeticDatum? _kusaieAstro1951CarolineIslands = null;
        public static GeodeticDatum? KusaieAstro1951CarolineIslands
        {
            get
            {
                _kusaieAstro1951CarolineIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e506") },
                    Name = "Kusaie Astro 1951 Caroline Islands",
                    Description = "Kusaie Astro 1951 Caroline Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 647 },
                    DeltaY = new () { ScalarValue = 1777 },
                    DeltaZ = new () { ScalarValue = -1124 }
                };
                return _kusaieAstro1951CarolineIslands;
            }
        }
        #endregion

        #region geodetic datum KoreanGeodeticSystemSouthKorea
        private static GeodeticDatum? _koreanGeodeticSystemSouthKorea = null;
        public static GeodeticDatum? KoreanGeodeticSystemSouthKorea
        {
            get
            {
                _koreanGeodeticSystemSouthKorea ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e507") },
                    Name = "Korean Geodetic System South Korea",
                    Description = "Korean Geodetic System South Korea",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.GRS80,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 }
                };
                return _koreanGeodeticSystemSouthKorea;
            }
        }
        #endregion

        #region geodetic datum LC5Astro1961CaymanBracIsland
        private static GeodeticDatum? _lc5Astro1961CaymanBracIsland = null;
        public static GeodeticDatum? LC5Astro1961CaymanBracIsland
        {
            get
            {
                _lc5Astro1961CaymanBracIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e508") },
                    Name = "L. C. 5 Astro 1961 Cayman Brac Island",
                    Description = "L. C. 5 Astro 1961 Cayman Brac Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 42 },
                    DeltaY = new () { ScalarValue = 124 },
                    DeltaZ = new () { ScalarValue = 147 }
                };
                return _lc5Astro1961CaymanBracIsland;
            }
        }
        #endregion

        #region geodetic datum LeigonGhana
        private static GeodeticDatum? _leigonGhana = null;
        public static GeodeticDatum? LeigonGhana
        {
            get
            {
                _leigonGhana ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e509") },
                    Name = "Leigon Ghana",
                    Description = "Leigon Ghana",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -130 },
                    DeltaY = new () { ScalarValue = 29 },
                    DeltaZ = new () { ScalarValue = 364 }
                };
                return _leigonGhana;
            }
        }
        #endregion

        #region geodetic datum Liberia1964Liberia
        private static GeodeticDatum? _liberia1964Liberia = null;
        public static GeodeticDatum? Liberia1964Liberia
        {
            get
            {
                _liberia1964Liberia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e50a") },
                    Name = "Liberia 1964 Liberia",
                    Description = "Liberia 1964 Liberia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -90 },
                    DeltaY = new () { ScalarValue = 40 },
                    DeltaZ = new () { ScalarValue = 88 }
                };
                return _liberia1964Liberia;
            }
        }
        #endregion

        #region geodetic datum LuzonPhilippinesExcludingMindanao
        private static GeodeticDatum? _luzonPhilippinesExcludingMindanao = null;
        public static GeodeticDatum? LuzonPhilippinesExcludingMindanao
        {
            get
            {
                _luzonPhilippinesExcludingMindanao ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e50b") },
                    Name = "Luzon Philippines (Excluding Mindanao)",
                    Description = "Luzon Philippines (Excluding Mindanao)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -133 },
                    DeltaY = new () { ScalarValue = -77 },
                    DeltaZ = new () { ScalarValue = -51 }
                };
                return _luzonPhilippinesExcludingMindanao;
            }
        }
        #endregion

        #region geodetic datum LuzonPhilippinesMindanao
        private static GeodeticDatum? _luzonPhilippinesMindanao = null;
        public static GeodeticDatum? LuzonPhilippinesMindanao
        {
            get
            {
                _luzonPhilippinesMindanao ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e50c") },
                    Name = "Luzon Philippines (Mindanao)",
                    Description = "Luzon Philippines (Mindanao)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -133 },
                    DeltaY = new () { ScalarValue = -79 },
                    DeltaZ = new () { ScalarValue = -72 }
                };
                return _luzonPhilippinesMindanao;
            }
        }
        #endregion

        #region geodetic datum MPoralokoGabon
        private static GeodeticDatum? _mPoralokoGabon = null;
        public static GeodeticDatum? MPoralokoGabon
        {
            get
            {
                _mPoralokoGabon ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e50d") },
                    Name = "M''Poraloko Gabon",
                    Description = "M''Poraloko Gabon",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -74 },
                    DeltaY = new () { ScalarValue = -130 },
                    DeltaZ = new () { ScalarValue = 42 }
                };
                return _mPoralokoGabon;
            }
        }
        #endregion

        #region geodetic datum Mahe1971MaheIsland
        private static GeodeticDatum? _mahe1971MaheIsland = null;
        public static GeodeticDatum? Mahe1971MaheIsland
        {
            get
            {
                _mahe1971MaheIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e50e") },
                    Name = "Mahe 1971 Mahe Island",
                    Description = "Mahe 1971 Mahe Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = 41 },
                    DeltaY = new () { ScalarValue = -220 },
                    DeltaZ = new () { ScalarValue = -134 }
                };
                return _mahe1971MaheIsland;
            }
        }
        #endregion

        #region geodetic datum MassawaEthiopiaEritrea
        private static GeodeticDatum? _massawaEthiopiaEritrea = null;
        public static GeodeticDatum? MassawaEthiopiaEritrea
        {
            get
            {
                _massawaEthiopiaEritrea ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e50f") },
                    Name = "Massawa Ethiopia (Eritrea)",
                    Description = "Massawa Ethiopia (Eritrea)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = 639 },
                    DeltaY = new () { ScalarValue = 405 },
                    DeltaZ = new () { ScalarValue = 60 }
                };
                return _massawaEthiopiaEritrea;
            }
        }
        #endregion

        #region geodetic datum MerchichMorocco
        private static GeodeticDatum? _merchichMorocco = null;
        public static GeodeticDatum? MerchichMorocco
        {
            get
            {
                _merchichMorocco ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e510") },
                    Name = "Merchich Morocco",
                    Description = "Merchich Morocco",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = 31 },
                    DeltaY = new () { ScalarValue = 146 },
                    DeltaZ = new () { ScalarValue = 47 }
                };
                return _merchichMorocco;
            }
        }
        #endregion

        #region geodetic datum MidwayAstro1961MidwayIslands
        private static GeodeticDatum? _midwayAstro1961MidwayIslands = null;
        public static GeodeticDatum? MidwayAstro1961MidwayIslands
        {
            get
            {
                _midwayAstro1961MidwayIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e511") },
                    Name = "Midway Astro 1961 Midway Islands",
                    Description = "Midway Astro 1961 Midway Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 912 },
                    DeltaY = new () { ScalarValue = -58 },
                    DeltaZ = new () { ScalarValue = 1227 }
                };
                return _midwayAstro1961MidwayIslands;
            }
        }
        #endregion

        #region geodetic datum MinnaCameroon
        private static GeodeticDatum? _minnaCameroon = null;
        public static GeodeticDatum? MinnaCameroon
        {
            get
            {
                _minnaCameroon ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e512") },
                    Name = "Minna Cameroon",
                    Description = "Minna Cameroon",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -81 },
                    DeltaY = new () { ScalarValue = -84 },
                    DeltaZ = new () { ScalarValue = 115 }
                };
                return _minnaCameroon;
            }
        }
        #endregion

        #region geodetic datum MinnaNigeria
        private static GeodeticDatum? _minnaNigeria = null;
        public static GeodeticDatum? MinnaNigeria
        {
            get
            {
                _minnaNigeria ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e513") },
                    Name = "Minna Nigeria",
                    Description = "Minna Nigeria",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -92 },
                    DeltaY = new () { ScalarValue = -93 },
                    DeltaZ = new () { ScalarValue = 122 }
                };
                return _minnaNigeria;
            }
        }
        #endregion

        #region geodetic datum MontserratIslandAstro1958MontserratLeewardIslands
        private static GeodeticDatum? _montserratIslandAstro1958MontserratLeewardIslands = null;
        public static GeodeticDatum? MontserratIslandAstro1958MontserratLeewardIslands
        {
            get
            {
                _montserratIslandAstro1958MontserratLeewardIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e514") },
                    Name = "Montserrat Island Astro 1958 Montserrat (Leeward Islands)",
                    Description = "Montserrat Island Astro 1958 Montserrat (Leeward Islands)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = 174 },
                    DeltaY = new () { ScalarValue = 359 },
                    DeltaZ = new () { ScalarValue = 365 }
                };
                return _montserratIslandAstro1958MontserratLeewardIslands;
            }
        }
        #endregion

        #region geodetic datum NahrwanOmanMasirahIsland
        private static GeodeticDatum? _nahrwanOmanMasirahIsland = null;
        public static GeodeticDatum? NahrwanOmanMasirahIsland
        {
            get
            {
                _nahrwanOmanMasirahIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e515") },
                    Name = "Nahrwan Oman (Masirah Island)",
                    Description = "Nahrwan Oman (Masirah Island)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -247 },
                    DeltaY = new () { ScalarValue = -148 },
                    DeltaZ = new () { ScalarValue = 369 }
                };
                return _nahrwanOmanMasirahIsland;
            }
        }
        #endregion

        #region geodetic datum NahrwanSaudiArabia
        private static GeodeticDatum? _nahrwanSaudiArabia = null;
        public static GeodeticDatum? NahrwanSaudiArabia
        {
            get
            {
                _nahrwanSaudiArabia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e516") },
                    Name = "Nahrwan Saudi Arabia",
                    Description = "Nahrwan Saudi Arabia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -243 },
                    DeltaY = new () { ScalarValue = -192 },
                    DeltaZ = new () { ScalarValue = 477 }
                };
                return _nahrwanSaudiArabia;
            }
        }
        #endregion

        #region geodetic datum NahrwanUnitedArabEmirates
        private static GeodeticDatum? _nahrwanUnitedArabEmirates = null;
        public static GeodeticDatum? NahrwanUnitedArabEmirates
        {
            get
            {
                _nahrwanUnitedArabEmirates ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e517") },
                    Name = "Nahrwan United Arab Emirates",
                    Description = "Nahrwan United Arab Emirates",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -249 },
                    DeltaY = new () { ScalarValue = -156 },
                    DeltaZ = new () { ScalarValue = 381 }
                };
                return _nahrwanUnitedArabEmirates;
            }
        }
        #endregion

        #region geodetic datum NaparimaBWITrinidadTobago
        private static GeodeticDatum? _naparimaBWITrinidadTobago = null;
        public static GeodeticDatum? NaparimaBWITrinidadTobago
        {
            get
            {
                _naparimaBWITrinidadTobago ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e518") },
                    Name = "Naparima BWI Trinidad & Tobago",
                    Description = "Naparima BWI Trinidad & Tobago",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -10 },
                    DeltaY = new () { ScalarValue = 375 },
                    DeltaZ = new () { ScalarValue = 165 }
                };
                return _naparimaBWITrinidadTobago;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927AlaskaExcludingAleutianIds
        private static GeodeticDatum? _northAmerican1927AlaskaExcludingAleutianIds = null;
        public static GeodeticDatum? NorthAmerican1927AlaskaExcludingAleutianIds
        {
            get
            {
                _northAmerican1927AlaskaExcludingAleutianIds ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e519") },
                    Name = "North American 1927 Alaska (Excluding Aleutian Ids)",
                    Description = "North American 1927 Alaska (Excluding Aleutian Ids)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -5 },
                    DeltaY = new () { ScalarValue = 135 },
                    DeltaZ = new () { ScalarValue = 172 }
                };
                return _northAmerican1927AlaskaExcludingAleutianIds;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927AlaskaAleutianIdsEastOf180W
        private static GeodeticDatum? _northAmerican1927AlaskaAleutianIdsEastOf180W = null;
        public static GeodeticDatum? NorthAmerican1927AlaskaAleutianIdsEastOf180W
        {
            get
            {
                _northAmerican1927AlaskaAleutianIdsEastOf180W ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e51a") },
                    Name = "North American 1927 Alaska (Aleutian Ids East of 180W)",
                    Description = "North American 1927 Alaska (Aleutian Ids East of 180W)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -2 },
                    DeltaY = new () { ScalarValue = 152 },
                    DeltaZ = new () { ScalarValue = 149 }
                };
                return _northAmerican1927AlaskaAleutianIdsEastOf180W;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927AlaskaAleutianIdsWestOf180W
        private static GeodeticDatum? _northAmerican1927AlaskaAleutianIdsWestOf180W = null;
        public static GeodeticDatum? NorthAmerican1927AlaskaAleutianIdsWestOf180W
        {
            get
            {
                _northAmerican1927AlaskaAleutianIdsWestOf180W ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e51b") },
                    Name = "North American 1927 Alaska (Aleutian Ids West of 180W)",
                    Description = "North American 1927 Alaska (Aleutian Ids West of 180W)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 2 },
                    DeltaY = new () { ScalarValue = 204 },
                    DeltaZ = new () { ScalarValue = 105 }
                };
                return _northAmerican1927AlaskaAleutianIdsWestOf180W;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927BahamasExceptSanSalvadorId
        private static GeodeticDatum? _northAmerican1927BahamasExceptSanSalvadorId = null;
        public static GeodeticDatum? NorthAmerican1927BahamasExceptSanSalvadorId
        {
            get
            {
                _northAmerican1927BahamasExceptSanSalvadorId ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e51c") },
                    Name = "North American 1927 Bahamas (Except San Salvador Id)",
                    Description = "North American 1927 Bahamas (Except San Salvador Id)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -4 },
                    DeltaY = new () { ScalarValue = 154 },
                    DeltaZ = new () { ScalarValue = 178 }
                };
                return _northAmerican1927BahamasExceptSanSalvadorId;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927BahamasSanSalvadorIsland
        private static GeodeticDatum? _northAmerican1927BahamasSanSalvadorIsland = null;
        public static GeodeticDatum? NorthAmerican1927BahamasSanSalvadorIsland
        {
            get
            {
                _northAmerican1927BahamasSanSalvadorIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e51d") },
                    Name = "North American 1927 Bahamas (San Salvador Island)",
                    Description = "North American 1927 Bahamas (San Salvador Island)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 1 },
                    DeltaY = new () { ScalarValue = 140 },
                    DeltaZ = new () { ScalarValue = 165 }
                };
                return _northAmerican1927BahamasSanSalvadorIsland;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927CanadaAlbertaBritishColumbia
        private static GeodeticDatum? _northAmerican1927CanadaAlbertaBritishColumbia = null;
        public static GeodeticDatum? NorthAmerican1927CanadaAlbertaBritishColumbia
        {
            get
            {
                _northAmerican1927CanadaAlbertaBritishColumbia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e51e") },
                    Name = "North American 1927 \"Canada (Alberta; British Columbia)\"",
                    Description = "North American 1927 \"Canada (Alberta; British Columbia)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -7 },
                    DeltaY = new () { ScalarValue = 162 },
                    DeltaZ = new () { ScalarValue = 188 }
                };
                return _northAmerican1927CanadaAlbertaBritishColumbia;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927CanadaManitobaOntario
        private static GeodeticDatum? _northAmerican1927CanadaManitobaOntario = null;
        public static GeodeticDatum? NorthAmerican1927CanadaManitobaOntario
        {
            get
            {
                _northAmerican1927CanadaManitobaOntario ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e51f") },
                    Name = "North American 1927 \"Canada (Manitoba; Ontario)\"",
                    Description = "North American 1927 \"Canada (Manitoba; Ontario)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -9 },
                    DeltaY = new () { ScalarValue = 157 },
                    DeltaZ = new () { ScalarValue = 184 }
                };
                return _northAmerican1927CanadaManitobaOntario;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927CanadaNewBrunswickNewfoundlandNovaScotiaQuebec
        private static GeodeticDatum? _northAmerican1927CanadaNewBrunswickNewfoundlandNovaScotiaQuebec = null;
        public static GeodeticDatum? NorthAmerican1927CanadaNewBrunswickNewfoundlandNovaScotiaQuebec
        {
            get
            {
                _northAmerican1927CanadaNewBrunswickNewfoundlandNovaScotiaQuebec ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e520") },
                    Name = "North American 1927 \"Canada (New Brunswick; Newfoundland; Nova Scotia; Quebec)\"",
                    Description = "North American 1927 \"Canada (New Brunswick; Newfoundland; Nova Scotia; Quebec)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -22 },
                    DeltaY = new () { ScalarValue = 160 },
                    DeltaZ = new () { ScalarValue = 190 }
                };
                return _northAmerican1927CanadaNewBrunswickNewfoundlandNovaScotiaQuebec;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927CanadaNorthwestTerritoriesSaskatchewan
        private static GeodeticDatum? _northAmerican1927CanadaNorthwestTerritoriesSaskatchewan = null;
        public static GeodeticDatum? NorthAmerican1927CanadaNorthwestTerritoriesSaskatchewan
        {
            get
            {
                _northAmerican1927CanadaNorthwestTerritoriesSaskatchewan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e521") },
                    Name = "North American 1927 \"Canada (Northwest Territories; Saskatchewan)\"",
                    Description = "North American 1927 \"Canada (Northwest Territories; Saskatchewan)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 4 },
                    DeltaY = new () { ScalarValue = 159 },
                    DeltaZ = new () { ScalarValue = 188 }
                };
                return _northAmerican1927CanadaNorthwestTerritoriesSaskatchewan;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927CanadaYukon
        private static GeodeticDatum? _northAmerican1927CanadaYukon = null;
        public static GeodeticDatum? NorthAmerican1927CanadaYukon
        {
            get
            {
                _northAmerican1927CanadaYukon ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("b0a1d2e3-f404-41b5-8100-a1b2c3d4e522") },
                    Name = "North American 1927 Canada (Yukon)",
                    Description = "North American 1927 Canada (Yukon)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -7 },
                    DeltaY = new () { ScalarValue = 139 },
                    DeltaZ = new () { ScalarValue = 181 }
                };
                return _northAmerican1927CanadaYukon;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927CanalZone
        private static GeodeticDatum? _northAmerican1927CanalZone = null;
        public static GeodeticDatum? NorthAmerican1927CanalZone
        {
            get
            {
                _northAmerican1927CanalZone ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6901") },
                    Name = "North American 1927 Canal Zone",
                    Description = "North American 1927 Canal Zone",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 125 },
                    DeltaZ = new () { ScalarValue = 201 }
                };
                return _northAmerican1927CanalZone;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927Cuba
        private static GeodeticDatum? _northAmerican1927Cuba = null;
        public static GeodeticDatum? NorthAmerican1927Cuba
        {
            get
            {
                _northAmerican1927Cuba ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6902") },
                    Name = "North American 1927 Cuba",
                    Description = "North American 1927 Cuba",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -9 },
                    DeltaY = new () { ScalarValue = 152 },
                    DeltaZ = new () { ScalarValue = 178 }
                };
                return _northAmerican1927Cuba;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927GreenlandHayesPeninsula
        private static GeodeticDatum? _northAmerican1927GreenlandHayesPeninsula = null;
        public static GeodeticDatum? NorthAmerican1927GreenlandHayesPeninsula
        {
            get
            {
                _northAmerican1927GreenlandHayesPeninsula ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6903") },
                    Name = "North American 1927 Greenland (Hayes Peninsula)",
                    Description = "North American 1927 Greenland (Hayes Peninsula)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 11 },
                    DeltaY = new () { ScalarValue = 114 },
                    DeltaZ = new () { ScalarValue = 195 }
                };
                return _northAmerican1927GreenlandHayesPeninsula;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927MeanForAntiguaBarbadosBarbudaCaicosIslandsCubaDominicanRepublicGrandCaymanJamaicaTurksIslands
        private static GeodeticDatum? _northAmerican1927MeanForAntiguaEtc = null;
        public static GeodeticDatum? NorthAmerican1927MeanForAntiguaBarbadosBarbudaCaicosIslandsCubaDominicanRepublicGrandCaymanJamaicaTurksIslands
        {
            get
            {
                _northAmerican1927MeanForAntiguaEtc ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6904") },
                    Name = "North American 1927 \"MEAN FOR Antigua; Barbados; Barbuda; Caicos Islands; Cuba; Dominican Reprivate; Grand Cayman; Jamaica; Turks Islands\"",
                    Description = "North American 1927 MEAN FOR multiple Caribbean islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -3 },
                    DeltaY = new () { ScalarValue = 142 },
                    DeltaZ = new () { ScalarValue = 183 }
                };
                return _northAmerican1927MeanForAntiguaEtc;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927MeanForBelizeCostaRicaElSalvadorGuatemalaHondurasNicaragua
        private static GeodeticDatum? _northAmerican1927MeanForBelizeEtc = null;
        public static GeodeticDatum? NorthAmerican1927MeanForBelizeCostaRicaElSalvadorGuatemalaHondurasNicaragua
        {
            get
            {
                _northAmerican1927MeanForBelizeEtc ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6905") },
                    Name = "North American 1927 \"MEAN FOR Belize; Costa Rica; El Salvador; Guatemala; Honduras; Nicaragua\"",
                    Description = "North American 1927 MEAN FOR Central America",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 125 },
                    DeltaZ = new () { ScalarValue = 194 }
                };
                return _northAmerican1927MeanForBelizeEtc;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927MeanForCanada
        private static GeodeticDatum? _northAmerican1927MeanForCanada = null;
        public static GeodeticDatum? NorthAmerican1927MeanForCanada
        {
            get
            {
                _northAmerican1927MeanForCanada ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6906") },
                    Name = "North American 1927 MEAN FOR Canada",
                    Description = "North American 1927 MEAN FOR Canada",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -10 },
                    DeltaY = new () { ScalarValue = 158 },
                    DeltaZ = new () { ScalarValue = 187 }
                };
                return _northAmerican1927MeanForCanada;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927MeanForCONUS
        private static GeodeticDatum? _northAmerican1927MeanForCONUS = null;
        public static GeodeticDatum? NorthAmerican1927MeanForCONUS
        {
            get
            {
                _northAmerican1927MeanForCONUS ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6907") },
                    Name = "North American 1927 MEAN FOR CONUS",
                    Description = "North American 1927 MEAN FOR CONUS",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -8 },
                    DeltaY = new () { ScalarValue = 160 },
                    DeltaZ = new () { ScalarValue = 176 }
                };
                return _northAmerican1927MeanForCONUS;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927MeanForCONUSEastOfMississippiRiverIncludingLouisianaMissouriMinnesota
        private static GeodeticDatum? _northAmerican1927MeanForCONUSEast = null;
        public static GeodeticDatum? NorthAmerican1927MeanForCONUSEastOfMississippiRiverIncludingLouisianaMissouriMinnesota
        {
            get
            {
                _northAmerican1927MeanForCONUSEast ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6908") },
                    Name = "North American 1927 \"MEAN FOR CONUS (East of Mississippi; River Including Louisiana; Missouri; Minnesota)\"",
                    Description = "North American 1927 MEAN FOR eastern CONUS",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -9 },
                    DeltaY = new () { ScalarValue = 161 },
                    DeltaZ = new () { ScalarValue = 179 }
                };
                return _northAmerican1927MeanForCONUSEast;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927MeanForCONUSWestOfMississippiRiverExcludingLouisianaMinnesotaMissouri
        private static GeodeticDatum? _northAmerican1927MeanForCONUSWest = null;
        public static GeodeticDatum? NorthAmerican1927MeanForCONUSWestOfMississippiRiverExcludingLouisianaMinnesotaMissouri
        {
            get
            {
                _northAmerican1927MeanForCONUSWest ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6909") },
                    Name = "North American 1927 \"MEAN FOR CONUS (West of Mississippi; River Excluding Louisiana; Minnesota; Missouri)\"",
                    Description = "North American 1927 MEAN FOR western CONUS",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -8 },
                    DeltaY = new () { ScalarValue = 159 },
                    DeltaZ = new () { ScalarValue = 175 }
                };
                return _northAmerican1927MeanForCONUSWest;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1927Mexico
        private static GeodeticDatum? _northAmerican1927Mexico = null;
        public static GeodeticDatum? NorthAmerican1927Mexico
        {
            get
            {
                _northAmerican1927Mexico ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6910") },
                    Name = "North American 1927 Mexico",
                    Description = "North American 1927 Mexico",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = -12 },
                    DeltaY = new () { ScalarValue = 130 },
                    DeltaZ = new () { ScalarValue = 190 }
                };
                return _northAmerican1927Mexico;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1983AlaskaExcludingAleutianIds
        private static GeodeticDatum? _northAmerican1983AlaskaExcludingAleutianIds = null;
        public static GeodeticDatum? NorthAmerican1983AlaskaExcludingAleutianIds
        {
            get
            {
                _northAmerican1983AlaskaExcludingAleutianIds ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6911") },
                    Name = "North American 1983 Alaska (Excluding Aleutian Ids)",
                    Description = "North American 1983 Alaska (Excluding Aleutian Ids)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.GRS80,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 }
                };
                return _northAmerican1983AlaskaExcludingAleutianIds;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1983AleutianIds
        private static GeodeticDatum? _northAmerican1983AleutianIds = null;
        public static GeodeticDatum? NorthAmerican1983AleutianIds
        {
            get
            {
                _northAmerican1983AleutianIds ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6912") },
                    Name = "North American 1983 Aleutian Ids",
                    Description = "North American 1983 Aleutian Ids",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.GRS80,
                    DeltaX = new () { ScalarValue = -2 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 4 }
                };
                return _northAmerican1983AleutianIds;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1983Canada
        private static GeodeticDatum? _northAmerican1983Canada = null;
        public static GeodeticDatum? NorthAmerican1983Canada
        {
            get
            {
                _northAmerican1983Canada ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6913") },
                    Name = "North American 1983 Canada",
                    Description = "North American 1983 Canada",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.GRS80,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 }
                };
                return _northAmerican1983Canada;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1983CONUS
        private static GeodeticDatum? _northAmerican1983CONUS = null;
        public static GeodeticDatum? NorthAmerican1983CONUS
        {
            get
            {
                _northAmerican1983CONUS ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6914") },
                    Name = "North American 1983 CONUS",
                    Description = "North American 1983 CONUS",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.GRS80,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 }
                };
                return _northAmerican1983CONUS;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1983Hawaii
        private static GeodeticDatum? _northAmerican1983Hawaii = null;
        public static GeodeticDatum? NorthAmerican1983Hawaii
        {
            get
            {
                _northAmerican1983Hawaii ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6915") },
                    Name = "North American 1983 Hawaii",
                    Description = "North American 1983 Hawaii",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.GRS80,
                    DeltaX = new () { ScalarValue = 1 },
                    DeltaY = new () { ScalarValue = 1 },
                    DeltaZ = new () { ScalarValue = -1 }
                };
                return _northAmerican1983Hawaii;
            }
        }
        #endregion

        #region geodetic datum NorthAmerican1983MexicoCentralAmerica
        private static GeodeticDatum? _northAmerican1983MexicoCentralAmerica = null;
        public static GeodeticDatum? NorthAmerican1983MexicoCentralAmerica
        {
            get
            {
                _northAmerican1983MexicoCentralAmerica ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6916") },
                    Name = "North American 1983 \"Mexico; Central America\"",
                    Description = "North American 1983 Mexico & Central America",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.GRS80,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 }
                };
                return _northAmerican1983MexicoCentralAmerica;
            }
        }
        #endregion

        #region geodetic datum NorthSahara1959Algeria
        private static GeodeticDatum? _northSahara1959Algeria = null;
        public static GeodeticDatum? NorthSahara1959Algeria
        {
            get
            {
                _northSahara1959Algeria ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6917") },
                    Name = "North Sahara 1959 Algeria",
                    Description = "North Sahara 1959 Algeria",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -186 },
                    DeltaY = new () { ScalarValue = -93 },
                    DeltaZ = new () { ScalarValue = 310 }
                };
                return _northSahara1959Algeria;
            }
        }
        #endregion

        #region geodetic datum ObservatorioMeteorologico1939AzoresCorvoFloresIslands
        private static GeodeticDatum? _observatorioMeteorologico1939Azores = null;
        public static GeodeticDatum? ObservatorioMeteorologico1939AzoresCorvoFloresIslands
        {
            get
            {
                _observatorioMeteorologico1939Azores ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6918") },
                    Name = "Observatorio Meteorologico 1939 Azores (Corvo & Flores Islands)",
                    Description = "Observatorio Meteorologico 1939 Azores (Corvo & Flores Islands)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -425 },
                    DeltaY = new () { ScalarValue = -169 },
                    DeltaZ = new () { ScalarValue = 81 }
                };
                return _observatorioMeteorologico1939Azores;
            }
        }
        #endregion

        #region geodetic datum OldEgyptian1907Egypt
        private static GeodeticDatum? _oldEgyptian1907Egypt = null;
        public static GeodeticDatum? OldEgyptian1907Egypt
        {
            get
            {
                _oldEgyptian1907Egypt ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6919") },
                    Name = "Old Egyptian 1907 Egypt",
                    Description = "Old Egyptian 1907 Egypt",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Helmert_1906,
                    DeltaX = new () { ScalarValue = -130 },
                    DeltaY = new () { ScalarValue = 110 },
                    DeltaZ = new () { ScalarValue = -13 }
                };
                return _oldEgyptian1907Egypt;
            }
        }
        #endregion

        #region geodetic datum OldHawaiianHawaii
        private static GeodeticDatum? _oldHawaiianHawaii = null;
        public static GeodeticDatum? OldHawaiianHawaii
        {
            get
            {
                _oldHawaiianHawaii ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6920") },
                    Name = "Old Hawaiian Hawaii",
                    Description = "Old Hawaiian Hawaii",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 89 },
                    DeltaY = new () { ScalarValue = -279 },
                    DeltaZ = new () { ScalarValue = -183 }
                };
                return _oldHawaiianHawaii;
            }
        }
        #endregion

        #region geodetic datum OldHawaiianKauai
        private static GeodeticDatum? _oldHawaiianKauai = null;
        public static GeodeticDatum? OldHawaiianKauai
        {
            get
            {
                _oldHawaiianKauai ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6921") },
                    Name = "Old Hawaiian Kauai",
                    Description = "Old Hawaiian Kauai",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 45 },
                    DeltaY = new () { ScalarValue = -290 },
                    DeltaZ = new () { ScalarValue = -172 }
                };
                return _oldHawaiianKauai;
            }
        }
        #endregion

        #region geodetic datum OldHawaiianMaui
        private static GeodeticDatum? _oldHawaiianMaui = null;
        public static GeodeticDatum? OldHawaiianMaui
        {
            get
            {
                _oldHawaiianMaui ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6922") },
                    Name = "Old Hawaiian Maui",
                    Description = "Old Hawaiian Maui",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 65 },
                    DeltaY = new () { ScalarValue = -290 },
                    DeltaZ = new () { ScalarValue = -190 }
                };
                return _oldHawaiianMaui;
            }
        }
        #endregion

        #region geodetic datum OldHawaiianMeanForHawaiiKauaiMauiOahu
        private static GeodeticDatum? _oldHawaiianMeanForHawaiiKauaiMauiOahu = null;
        public static GeodeticDatum? OldHawaiianMeanForHawaiiKauaiMauiOahu
        {
            get
            {
                _oldHawaiianMeanForHawaiiKauaiMauiOahu ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6923") },
                    Name = "Old Hawaiian \"MEAN FOR Hawaii; Kauai; Maui; Oahu\"",
                    Description = "Old Hawaiian MEAN FOR main islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 61 },
                    DeltaY = new () { ScalarValue = -285 },
                    DeltaZ = new () { ScalarValue = -181 }
                };
                return _oldHawaiianMeanForHawaiiKauaiMauiOahu;
            }
        }
        #endregion

        #region geodetic datum OldHawaiianOahu
        private static GeodeticDatum? _oldHawaiianOahu = null;
        public static GeodeticDatum? OldHawaiianOahu
        {
            get
            {
                _oldHawaiianOahu ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6924") },
                    Name = "Old Hawaiian Oahu",
                    Description = "Old Hawaiian Oahu",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 58 },
                    DeltaY = new () { ScalarValue = -283 },
                    DeltaZ = new () { ScalarValue = -182 }
                };
                return _oldHawaiianOahu;
            }
        }
        #endregion

        #region geodetic datum OmanOman
        private static GeodeticDatum? _omanOman = null;
        public static GeodeticDatum? OmanOman
        {
            get
            {
                _omanOman ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6925") },
                    Name = "Oman Oman",
                    Description = "Oman Oman",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -346 },
                    DeltaY = new () { ScalarValue = -1 },
                    DeltaZ = new () { ScalarValue = 224 }
                };
                return _omanOman;
            }
        }
        #endregion

        #region geodetic datum OrdnanceSurveyGreatBritain1936England
        private static GeodeticDatum? _osgb1936England = null;
        public static GeodeticDatum? OrdnanceSurveyGreatBritain1936England
        {
            get
            {
                _osgb1936England ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6926") },
                    Name = "Ordnance Survey Great Britain 1936 England",
                    Description = "Ordnance Survey Great Britain 1936 England",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Airy_1830,
                    DeltaX = new () { ScalarValue = 371 },
                    DeltaY = new () { ScalarValue = -112 },
                    DeltaZ = new () { ScalarValue = 434 }
                };
                return _osgb1936England;
            }
        }
        #endregion

        #region geodetic datum OrdnanceSurveyGreatBritain1936EnglandIsleOfManWales
        private static GeodeticDatum? _osgb1936EnglandIsleOfManWales = null;
        public static GeodeticDatum? OrdnanceSurveyGreatBritain1936EnglandIsleOfManWales
        {
            get
            {
                _osgb1936EnglandIsleOfManWales ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6927") },
                    Name = "Ordnance Survey Great Britain 1936 \"England; Isle of Man; Wales\"",
                    Description = "Ordnance Survey Great Britain 1936 England, Isle of Man & Wales",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Airy_1830,
                    DeltaX = new () { ScalarValue = 371 },
                    DeltaY = new () { ScalarValue = -111 },
                    DeltaZ = new () { ScalarValue = 434 }
                };
                return _osgb1936EnglandIsleOfManWales;
            }
        }
        #endregion

        #region geodetic datum OrdnanceSurveyGreatBritain1936MeanForEnglandIsleOfManScotlandShetlandIslandsWales
        private static GeodeticDatum? _osgb1936Mean = null;
        public static GeodeticDatum? OrdnanceSurveyGreatBritain1936MeanForEnglandIsleOfManScotlandShetlandIslandsWales
        {
            get
            {
                _osgb1936Mean ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6928") },
                    Name = "Ordnance Survey Great Britain 1936 \"MEAN FOR England; Isle of Man; Scotland; Shetland Islands; Wales\"",
                    Description = "Ordnance Survey Great Britain 1936 mean for GB",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Airy_1830,
                    DeltaX = new () { ScalarValue = 375 },
                    DeltaY = new () { ScalarValue = -111 },
                    DeltaZ = new () { ScalarValue = 431 }
                };
                return _osgb1936Mean;
            }
        }
        #endregion

        #region geodetic datum OrdnanceSurveyGreatBritain1936ScotlandShetlandIslands
        private static GeodeticDatum? _osgb1936ScotlandShetland = null;
        public static GeodeticDatum? OrdnanceSurveyGreatBritain1936ScotlandShetlandIslands
        {
            get
            {
                _osgb1936ScotlandShetland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6929") },
                    Name = "Ordnance Survey Great Britain 1936 \"Scotland; Shetland Islands\"",
                    Description = "Ordnance Survey Great Britain 1936 Scotland & Shetland",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Airy_1830,
                    DeltaX = new () { ScalarValue = 384 },
                    DeltaY = new () { ScalarValue = -111 },
                    DeltaZ = new () { ScalarValue = 425 }
                };
                return _osgb1936ScotlandShetland;
            }
        }
        #endregion

        #region geodetic datum OrdnanceSurveyGreatBritain1936Wales
        private static GeodeticDatum? _osgb1936Wales = null;
        public static GeodeticDatum? OrdnanceSurveyGreatBritain1936Wales
        {
            get
            {
                _osgb1936Wales ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("6aa37377-7f73-41e4-a4d5-d37e1d7b6930") },
                    Name = "Ordnance Survey Great Britain 1936 Wales",
                    Description = "Ordnance Survey Great Britain 1936 Wales",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Airy_1830,
                    DeltaX = new () { ScalarValue = 370 },
                    DeltaY = new () { ScalarValue = -108 },
                    DeltaZ = new () { ScalarValue = 434 }
                };
                return _osgb1936Wales;
            }
        }
        #endregion

        #region geodetic datum PicoDeLasNievesCanaryIslands
        private static GeodeticDatum? _picoDeLasNievesCanaryIslands = null;
        public static GeodeticDatum? PicoDeLasNievesCanaryIslands
        {
            get
            {
                _picoDeLasNievesCanaryIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5001") },
                    Name = "Pico de las Nieves Canary Islands",
                    Description = "Pico de las Nieves Canary Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -307 },
                    DeltaY = new () { ScalarValue = -92 },
                    DeltaZ = new () { ScalarValue = 127 }
                };
                return _picoDeLasNievesCanaryIslands;
            }
        }
        #endregion

        #region geodetic datum PitcairnAstro1967PitcairnIsland
        private static GeodeticDatum? _pitcairnAstro1967PitcairnIsland = null;
        public static GeodeticDatum? PitcairnAstro1967PitcairnIsland
        {
            get
            {
                _pitcairnAstro1967PitcairnIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5002") },
                    Name = "Pitcairn Astro 1967 Pitcairn Island",
                    Description = "Pitcairn Astro 1967 Pitcairn Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 185 },
                    DeltaY = new () { ScalarValue = 165 },
                    DeltaZ = new () { ScalarValue = 42 }
                };
                return _pitcairnAstro1967PitcairnIsland;
            }
        }
        #endregion

        #region geodetic datum Point58MeanForBurkinaFasoNiger
        private static GeodeticDatum? _point58MeanForBurkinaFasoNiger = null;
        public static GeodeticDatum? Point58MeanForBurkinaFasoNiger
        {
            get
            {
                _point58MeanForBurkinaFasoNiger ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5003") },
                    Name = "Point 58 MEAN FOR Burkina Faso & Niger",
                    Description = "Point 58 MEAN FOR Burkina Faso & Niger",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -106 },
                    DeltaY = new () { ScalarValue = -129 },
                    DeltaZ = new () { ScalarValue = 165 }
                };
                return _point58MeanForBurkinaFasoNiger;
            }
        }
        #endregion

        #region geodetic datum PointeNoire1948Congo
        private static GeodeticDatum? _pointeNoire1948Congo = null;
        public static GeodeticDatum? PointeNoire1948Congo
        {
            get
            {
                _pointeNoire1948Congo ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5004") },
                    Name = "Pointe Noire 1948 Congo",
                    Description = "Pointe Noire 1948 Congo",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -148 },
                    DeltaY = new () { ScalarValue = 51 },
                    DeltaZ = new () { ScalarValue = -291 }
                };
                return _pointeNoire1948Congo;
            }
        }
        #endregion

        #region geodetic datum PortoSanto1936PortoSantoMadeiraIslands
        private static GeodeticDatum? _portoSanto1936PortoSantoMadeiraIslands = null;
        public static GeodeticDatum? PortoSanto1936PortoSantoMadeiraIslands
        {
            get
            {
                _portoSanto1936PortoSantoMadeiraIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5005") },
                    Name = "Porto Santo 1936 \"Porto Santo; Madeira Islands\"",
                    Description = "Porto Santo 1936 \"Porto Santo; Madeira Islands\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -499 },
                    DeltaY = new () { ScalarValue = -249 },
                    DeltaZ = new () { ScalarValue = 314 }
                };
                return _portoSanto1936PortoSantoMadeiraIslands;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956Bolivia
        private static GeodeticDatum? _provisionalSouthAmerican1956Bolivia = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956Bolivia
        {
            get
            {
                _provisionalSouthAmerican1956Bolivia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5006") },
                    Name = "Provisional South American 1956 Bolivia",
                    Description = "Provisional South American 1956 Bolivia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -270 },
                    DeltaY = new () { ScalarValue = 188 },
                    DeltaZ = new () { ScalarValue = -388 }
                };
                return _provisionalSouthAmerican1956Bolivia;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956ChileNorthernNear19S
        private static GeodeticDatum? _provisionalSouthAmerican1956ChileNorthernNear19S = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956ChileNorthernNear19S
        {
            get
            {
                _provisionalSouthAmerican1956ChileNorthernNear19S ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5007") },
                    Name = "Provisional South American 1956 \"Chile (Northern; Near 19S)\"",
                    Description = "Provisional South American 1956 \"Chile (Northern; Near 19S)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -270 },
                    DeltaY = new () { ScalarValue = 183 },
                    DeltaZ = new () { ScalarValue = -390 }
                };
                return _provisionalSouthAmerican1956ChileNorthernNear19S;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956ChileSouthernNear43S
        private static GeodeticDatum? _provisionalSouthAmerican1956ChileSouthernNear43S = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956ChileSouthernNear43S
        {
            get
            {
                _provisionalSouthAmerican1956ChileSouthernNear43S ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5008") },
                    Name = "Provisional South American 1956 \"Chile (Southern; Near 43S)\"",
                    Description = "Provisional South American 1956 \"Chile (Southern; Near 43S)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -305 },
                    DeltaY = new () { ScalarValue = 243 },
                    DeltaZ = new () { ScalarValue = -442 }
                };
                return _provisionalSouthAmerican1956ChileSouthernNear43S;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956Colombia
        private static GeodeticDatum? _provisionalSouthAmerican1956Colombia = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956Colombia
        {
            get
            {
                _provisionalSouthAmerican1956Colombia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5009") },
                    Name = "Provisional South American 1956 Colombia",
                    Description = "Provisional South American 1956 Colombia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -282 },
                    DeltaY = new () { ScalarValue = 169 },
                    DeltaZ = new () { ScalarValue = -371 }
                };
                return _provisionalSouthAmerican1956Colombia;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956Ecuador
        private static GeodeticDatum? _provisionalSouthAmerican1956Ecuador = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956Ecuador
        {
            get
            {
                _provisionalSouthAmerican1956Ecuador ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5010") },
                    Name = "Provisional South American 1956 Ecuador",
                    Description = "Provisional South American 1956 Ecuador",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -278 },
                    DeltaY = new () { ScalarValue = 171 },
                    DeltaZ = new () { ScalarValue = -367 }
                };
                return _provisionalSouthAmerican1956Ecuador;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956Guyana
        private static GeodeticDatum? _provisionalSouthAmerican1956Guyana = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956Guyana
        {
            get
            {
                _provisionalSouthAmerican1956Guyana ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5011") },
                    Name = "Provisional South American 1956 Guyana",
                    Description = "Provisional South American 1956 Guyana",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -298 },
                    DeltaY = new () { ScalarValue = 159 },
                    DeltaZ = new () { ScalarValue = -369 }
                };
                return _provisionalSouthAmerican1956Guyana;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956MeanForBoliviaChileColombiaEcuadorGuyanaPeruVenezuela
        private static GeodeticDatum? _provisionalSouthAmerican1956MeanForBoliviaChileColombiaEcuadorGuyanaPeruVenezuela = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956MeanForBoliviaChileColombiaEcuadorGuyanaPeruVenezuela
        {
            get
            {
                _provisionalSouthAmerican1956MeanForBoliviaChileColombiaEcuadorGuyanaPeruVenezuela ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5012") },
                    Name = "Provisional South American 1956 \"MEAN FOR Bolivia; Chile; Colombia; Ecuador; Guyana; Peru; Venezuela\"",
                    Description = "Provisional South American 1956 \"MEAN FOR Bolivia; Chile; Colombia; Ecuador; Guyana; Peru; Venezuela\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -288 },
                    DeltaY = new () { ScalarValue = 175 },
                    DeltaZ = new () { ScalarValue = -376 }
                };
                return _provisionalSouthAmerican1956MeanForBoliviaChileColombiaEcuadorGuyanaPeruVenezuela;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956Peru
        private static GeodeticDatum? _provisionalSouthAmerican1956Peru = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956Peru
        {
            get
            {
                _provisionalSouthAmerican1956Peru ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5013") },
                    Name = "Provisional South American 1956 Peru",
                    Description = "Provisional South American 1956 Peru",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -279 },
                    DeltaY = new () { ScalarValue = 175 },
                    DeltaZ = new () { ScalarValue = -379 }
                };
                return _provisionalSouthAmerican1956Peru;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthAmerican1956Venezuela
        private static GeodeticDatum? _provisionalSouthAmerican1956Venezuela = null;
        public static GeodeticDatum? ProvisionalSouthAmerican1956Venezuela
        {
            get
            {
                _provisionalSouthAmerican1956Venezuela ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5014") },
                    Name = "Provisional South American 1956 Venezuela",
                    Description = "Provisional South American 1956 Venezuela",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -295 },
                    DeltaY = new () { ScalarValue = 173 },
                    DeltaZ = new () { ScalarValue = -371 }
                };
                return _provisionalSouthAmerican1956Venezuela;
            }
        }
        #endregion

        #region geodetic datum ProvisionalSouthChilean1963ChileNear53SHitoXVIII
        private static GeodeticDatum? _provisionalSouthChilean1963ChileNear53SHitoXVIII = null;
        public static GeodeticDatum? ProvisionalSouthChilean1963ChileNear53SHitoXVIII
        {
            get
            {
                _provisionalSouthChilean1963ChileNear53SHitoXVIII ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5015") },
                    Name = "Provisional South Chilean 1963 Chile (Near 53S) (Hito XVIII)",
                    Description = "Provisional South Chilean 1963 Chile (Near 53S) (Hito XVIII)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 16 },
                    DeltaY = new () { ScalarValue = 196 },
                    DeltaZ = new () { ScalarValue = 93 }
                };
                return _provisionalSouthChilean1963ChileNear53SHitoXVIII;
            }
        }
        #endregion

        #region geodetic datum PuertoRicoPuertoRicoVirginIslands
        private static GeodeticDatum? _puertoRicoPuertoRicoVirginIslands = null;
        public static GeodeticDatum? PuertoRicoPuertoRicoVirginIslands
        {
            get
            {
                _puertoRicoPuertoRicoVirginIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5016") },
                    Name = "Puerto Rico \"Puerto Rico; Virgin Islands\"",
                    Description = "Puerto Rico \"Puerto Rico; Virgin Islands\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1866,
                    DeltaX = new () { ScalarValue = 11 },
                    DeltaY = new () { ScalarValue = 72 },
                    DeltaZ = new () { ScalarValue = -101 }
                };
                return _puertoRicoPuertoRicoVirginIslands;
            }
        }
        #endregion

        #region geodetic datum Pulkovo1942Russia
        private static GeodeticDatum? _pulkovo1942Russia = null;
        public static GeodeticDatum? Pulkovo1942Russia
        {
            get
            {
                _pulkovo1942Russia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5017") },
                    Name = "Pulkovo 1942 Russia",
                    Description = "Pulkovo 1942 Russia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 28 },
                    DeltaY = new () { ScalarValue = -130 },
                    DeltaZ = new () { ScalarValue = -95 }
                };
                return _pulkovo1942Russia;
            }
        }
        #endregion

        #region geodetic datum QatarNationalQatar
        private static GeodeticDatum? _qatarNationalQatar = null;
        public static GeodeticDatum? QatarNationalQatar
        {
            get
            {
                _qatarNationalQatar ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5018") },
                    Name = "Qatar National Qatar",
                    Description = "Qatar National Qatar",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -128 },
                    DeltaY = new () { ScalarValue = -283 },
                    DeltaZ = new () { ScalarValue = 22 }
                };
                return _qatarNationalQatar;
            }
        }
        #endregion

        #region geodetic datum QornoqGreenlandSouth
        private static GeodeticDatum? _qornoqGreenlandSouth = null;
        public static GeodeticDatum? QornoqGreenlandSouth
        {
            get
            {
                _qornoqGreenlandSouth ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5019") },
                    Name = "Qornoq Greenland (South)",
                    Description = "Qornoq Greenland (South)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 164 },
                    DeltaY = new () { ScalarValue = 138 },
                    DeltaZ = new () { ScalarValue = -189 }
                };
                return _qornoqGreenlandSouth;
            }
        }
        #endregion

        #region geodetic datum ReunionMascareneIslands
        private static GeodeticDatum? _reunionMascareneIslands = null;
        public static GeodeticDatum? ReunionMascareneIslands
        {
            get
            {
                _reunionMascareneIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5020") },
                    Name = "Reunion Mascarene Islands",
                    Description = "Reunion Mascarene Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 94 },
                    DeltaY = new () { ScalarValue = -948 },
                    DeltaZ = new () { ScalarValue = -1262 }
                };
                return _reunionMascareneIslands;
            }
        }
        #endregion

        #region geodetic datum Rome1940ItalySardinia
        private static GeodeticDatum? _rome1940ItalySardinia = null;
        public static GeodeticDatum? Rome1940ItalySardinia
        {
            get
            {
                _rome1940ItalySardinia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5021") },
                    Name = "Rome 1940 Italy (Sardinia)",
                    Description = "Rome 1940 Italy (Sardinia)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -225 },
                    DeltaY = new () { ScalarValue = -65 },
                    DeltaZ = new () { ScalarValue = 9 }
                };
                return _rome1940ItalySardinia;
            }
        }
        #endregion

        #region geodetic datum S42Pulkovo1942Hungary
        private static GeodeticDatum? _s42Pulkovo1942Hungary = null;
        public static GeodeticDatum? S42Pulkovo1942Hungary
        {
            get
            {
                _s42Pulkovo1942Hungary ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5022") },
                    Name = "S-42 (Pulkovo 1942) Hungary",
                    Description = "S-42 (Pulkovo 1942) Hungary",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 28 },
                    DeltaY = new () { ScalarValue = -121 },
                    DeltaZ = new () { ScalarValue = -77 }
                };
                return _s42Pulkovo1942Hungary;
            }
        }
        #endregion

        #region geodetic datum S42Pulkovo1942Poland
        private static GeodeticDatum? _s42Pulkovo1942Poland = null;
        public static GeodeticDatum? S42Pulkovo1942Poland
        {
            get
            {
                _s42Pulkovo1942Poland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5023") },
                    Name = "S-42 (Pulkovo 1942) Poland",
                    Description = "S-42 (Pulkovo 1942) Poland",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 23 },
                    DeltaY = new () { ScalarValue = -124 },
                    DeltaZ = new () { ScalarValue = -82 }
                };
                return _s42Pulkovo1942Poland;
            }
        }
        #endregion

        #region geodetic datum S42Pulkovo1942Czechoslavakia
        private static GeodeticDatum? _s42Pulkovo1942Czechoslavakia = null;
        public static GeodeticDatum? S42Pulkovo1942Czechoslavakia
        {
            get
            {
                _s42Pulkovo1942Czechoslavakia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5024") },
                    Name = "S-42 (Pulkovo 1942) Czechoslavakia",
                    Description = "S-42 (Pulkovo 1942) Czechoslavakia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 26 },
                    DeltaY = new () { ScalarValue = -121 },
                    DeltaZ = new () { ScalarValue = -78 }
                };
                return _s42Pulkovo1942Czechoslavakia;
            }
        }
        #endregion

        #region geodetic datum S42Pulkovo1942Latvia
        private static GeodeticDatum? _s42Pulkovo1942Latvia = null;
        public static GeodeticDatum? S42Pulkovo1942Latvia
        {
            get
            {
                _s42Pulkovo1942Latvia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5025") },
                    Name = "S-42 (Pulkovo 1942) Latvia",
                    Description = "S-42 (Pulkovo 1942) Latvia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 24 },
                    DeltaY = new () { ScalarValue = -124 },
                    DeltaZ = new () { ScalarValue = -82 }
                };
                return _s42Pulkovo1942Latvia;
            }
        }
        #endregion

        #region geodetic datum S42Pulkovo1942Kazakhstan
        private static GeodeticDatum? _s42Pulkovo1942Kazakhstan = null;
        public static GeodeticDatum? S42Pulkovo1942Kazakhstan
        {
            get
            {
                _s42Pulkovo1942Kazakhstan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5026") },
                    Name = "S-42 (Pulkovo 1942) Kazakhstan",
                    Description = "S-42 (Pulkovo 1942) Kazakhstan",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 15 },
                    DeltaY = new () { ScalarValue = -130 },
                    DeltaZ = new () { ScalarValue = -84 }
                };
                return _s42Pulkovo1942Kazakhstan;
            }
        }
        #endregion

        #region geodetic datum S42Pulkovo1942Albania
        private static GeodeticDatum? _s42Pulkovo1942Albania = null;
        public static GeodeticDatum? S42Pulkovo1942Albania
        {
            get
            {
                _s42Pulkovo1942Albania ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5027") },
                    Name = "S-42 (Pulkovo 1942) Albania",
                    Description = "S-42 (Pulkovo 1942) Albania",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 24 },
                    DeltaY = new () { ScalarValue = -130 },
                    DeltaZ = new () { ScalarValue = -92 }
                };
                return _s42Pulkovo1942Albania;
            }
        }
        #endregion

        #region geodetic datum S42Pulkovo1942Romania
        private static GeodeticDatum? _s42Pulkovo1942Romania = null;
        public static GeodeticDatum? S42Pulkovo1942Romania
        {
            get
            {
                _s42Pulkovo1942Romania ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5028") },
                    Name = "S-42 (Pulkovo 1942) Romania",
                    Description = "S-42 (Pulkovo 1942) Romania",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Krassovsky_1940,
                    DeltaX = new () { ScalarValue = 28 },
                    DeltaY = new () { ScalarValue = -121 },
                    DeltaZ = new () { ScalarValue = -77 }
                };
                return _s42Pulkovo1942Romania;
            }
        }
        #endregion

        #region geodetic datum SJTSKCzechoslavakiaPrior1JAN1993
        private static GeodeticDatum? _sJTSKCzechoslavakiaPrior1JAN1993 = null;
        public static GeodeticDatum? SJTSKCzechoslavakiaPrior1JAN1993
        {
            get
            {
                _sJTSKCzechoslavakiaPrior1JAN1993 ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5029") },
                    Name = "S-JTSK Czechoslavakia (Prior 1 JAN 1993)",
                    Description = "S-JTSK Czechoslavakia (Prior 1 JAN 1993)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = 589 },
                    DeltaY = new () { ScalarValue = 76 },
                    DeltaZ = new () { ScalarValue = 480 }
                };
                return _sJTSKCzechoslavakiaPrior1JAN1993;
            }
        }
        #endregion

        #region geodetic datum SantoDOS1965EspiritoSantoIsland
        private static GeodeticDatum? _santoDOS1965EspiritoSantoIsland = null;
        public static GeodeticDatum? SantoDOS1965EspiritoSantoIsland
        {
            get
            {
                _santoDOS1965EspiritoSantoIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5030") },
                    Name = "Santo (DOS) 1965 Espirito Santo Island",
                    Description = "Santo (DOS) 1965 Espirito Santo Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 170 },
                    DeltaY = new () { ScalarValue = 42 },
                    DeltaZ = new () { ScalarValue = 84 }
                };
                return _santoDOS1965EspiritoSantoIsland;
            }
        }
        #endregion

        #region geodetic datum SaoBrazAzoresSaoMiguelSantaMariaIds
        private static GeodeticDatum? _saoBrazAzoresSaoMiguelSantaMariaIds = null;
        public static GeodeticDatum? SaoBrazAzoresSaoMiguelSantaMariaIds
        {
            get
            {
                _saoBrazAzoresSaoMiguelSantaMariaIds ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5031") },
                    Name = "Sao Braz \"Azores (Sao Miguel; Santa Maria Ids)\"",
                    Description = "Sao Braz \"Azores (Sao Miguel; Santa Maria Ids)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -203 },
                    DeltaY = new () { ScalarValue = 141 },
                    DeltaZ = new () { ScalarValue = 53 }
                };
                return _saoBrazAzoresSaoMiguelSantaMariaIds;
            }
        }
        #endregion

        #region geodetic datum SapperHill1943EastFalklandIsland
        private static GeodeticDatum? _sapperHill1943EastFalklandIsland = null;
        public static GeodeticDatum? SapperHill1943EastFalklandIsland
        {
            get
            {
                _sapperHill1943EastFalklandIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5032") },
                    Name = "Sapper Hill 1943 East Falkland Island",
                    Description = "Sapper Hill 1943 East Falkland Island",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -355 },
                    DeltaY = new () { ScalarValue = 21 },
                    DeltaZ = new () { ScalarValue = 72 }
                };
                return _sapperHill1943EastFalklandIsland;
            }
        }
        #endregion

        #region geodetic datum SchwarzeckNamibia
        private static GeodeticDatum? _schwarzeckNamibia = null;
        public static GeodeticDatum? SchwarzeckNamibia
        {
            get
            {
                _schwarzeckNamibia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5033") },
                    Name = "Schwarzeck Namibia",
                    Description = "Schwarzeck Namibia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841_Namibia,
                    DeltaX = new () { ScalarValue = 616 },
                    DeltaY = new () { ScalarValue = 97 },
                    DeltaZ = new () { ScalarValue = -251 }
                };
                return _schwarzeckNamibia;
            }
        }
        #endregion

        #region geodetic datum SelvagemGrande1938SalvageIslands
        private static GeodeticDatum? _selvagemGrande1938SalvageIslands = null;
        public static GeodeticDatum? SelvagemGrande1938SalvageIslands
        {
            get
            {
                _selvagemGrande1938SalvageIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5034") },
                    Name = "Selvagem Grande 1938 Salvage Islands",
                    Description = "Selvagem Grande 1938 Salvage Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -289 },
                    DeltaY = new () { ScalarValue = -124 },
                    DeltaZ = new () { ScalarValue = 60 }
                };
                return _selvagemGrande1938SalvageIslands;
            }
        }
        #endregion

        #region geodetic datum SierraLeone1960SierraLeone
        private static GeodeticDatum? _sierraLeone1960SierraLeone = null;
        public static GeodeticDatum? SierraLeone1960SierraLeone
        {
            get
            {
                _sierraLeone1960SierraLeone ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5035") },
                    Name = "Sierra Leone 1960 Sierra Leone",
                    Description = "Sierra Leone 1960 Sierra Leone",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -88 },
                    DeltaY = new () { ScalarValue = 4 },
                    DeltaZ = new () { ScalarValue = 101 }
                };
                return _sierraLeone1960SierraLeone;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Argentina
        private static GeodeticDatum? _southAmerican1969Argentina = null;
        public static GeodeticDatum? SouthAmerican1969Argentina
        {
            get
            {
                _southAmerican1969Argentina ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5036") },
                    Name = "South American 1969 Argentina",
                    Description = "South American 1969 Argentina",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -62 },
                    DeltaY = new () { ScalarValue = -1 },
                    DeltaZ = new () { ScalarValue = -37 }
                };
                return _southAmerican1969Argentina;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Bolivia
        private static GeodeticDatum? _southAmerican1969Bolivia = null;
        public static GeodeticDatum? SouthAmerican1969Bolivia
        {
            get
            {
                _southAmerican1969Bolivia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5037") },
                    Name = "South American 1969 Bolivia",
                    Description = "South American 1969 Bolivia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -61 },
                    DeltaY = new () { ScalarValue = 2 },
                    DeltaZ = new () { ScalarValue = -48 }
                };
                return _southAmerican1969Bolivia;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Brazil
        private static GeodeticDatum? _southAmerican1969Brazil = null;
        public static GeodeticDatum? SouthAmerican1969Brazil
        {
            get
            {
                _southAmerican1969Brazil ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5038") },
                    Name = "South American 1969 Brazil",
                    Description = "South American 1969 Brazil",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -60 },
                    DeltaY = new () { ScalarValue = -2 },
                    DeltaZ = new () { ScalarValue = -41 }
                };
                return _southAmerican1969Brazil;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Chile
        private static GeodeticDatum? _southAmerican1969Chile = null;
        public static GeodeticDatum? SouthAmerican1969Chile
        {
            get
            {
                _southAmerican1969Chile ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5039") },
                    Name = "South American 1969 Chile",
                    Description = "South American 1969 Chile",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -75 },
                    DeltaY = new () { ScalarValue = -1 },
                    DeltaZ = new () { ScalarValue = -44 }
                };
                return _southAmerican1969Chile;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Colombia
        private static GeodeticDatum? _southAmerican1969Colombia = null;
        public static GeodeticDatum? SouthAmerican1969Colombia
        {
            get
            {
                _southAmerican1969Colombia ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5040") },
                    Name = "South American 1969 Colombia",
                    Description = "South American 1969 Colombia",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -44 },
                    DeltaY = new () { ScalarValue = 6 },
                    DeltaZ = new () { ScalarValue = -36 }
                };
                return _southAmerican1969Colombia;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Ecuador
        private static GeodeticDatum? _southAmerican1969Ecuador = null;
        public static GeodeticDatum? SouthAmerican1969Ecuador
        {
            get
            {
                _southAmerican1969Ecuador ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5041") },
                    Name = "South American 1969 Ecuador",
                    Description = "South American 1969 Ecuador",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -48 },
                    DeltaY = new () { ScalarValue = 3 },
                    DeltaZ = new () { ScalarValue = -44 }
                };
                return _southAmerican1969Ecuador;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969EcuadorBaltraGalapagos
        private static GeodeticDatum? _southAmerican1969EcuadorBaltraGalapagos = null;
        public static GeodeticDatum? SouthAmerican1969EcuadorBaltraGalapagos
        {
            get
            {
                _southAmerican1969EcuadorBaltraGalapagos ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5042") },
                    Name = "South American 1969 \"Ecuador (Baltra; Galapagos)\"",
                    Description = "South American 1969 \"Ecuador (Baltra; Galapagos)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -47 },
                    DeltaY = new () { ScalarValue = 26 },
                    DeltaZ = new () { ScalarValue = -42 }
                };
                return _southAmerican1969EcuadorBaltraGalapagos;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Guyana
        private static GeodeticDatum? _southAmerican1969Guyana = null;
        public static GeodeticDatum? SouthAmerican1969Guyana
        {
            get
            {
                _southAmerican1969Guyana ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5043") },
                    Name = "South American 1969 Guyana",
                    Description = "South American 1969 Guyana",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -53 },
                    DeltaY = new () { ScalarValue = 3 },
                    DeltaZ = new () { ScalarValue = -47 }
                };
                return _southAmerican1969Guyana;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969MeanForArgentinaBoliviaBrazilChileColombiaEcuadorGuyanaParaguayPeruTrinidadTobagoVenezuelaenezuela
        private static GeodeticDatum? _southAmerican1969MeanForArgentinaBoliviaBrazilChileColombiaEcuadorGuyanaParaguayPeruTrinidadTobagoVenezuelaenezuela = null;
        public static GeodeticDatum? SouthAmerican1969MeanForArgentinaBoliviaBrazilChileColombiaEcuadorGuyanaParaguayPeruTrinidadTobagoVenezuelaenezuela
        {
            get
            {
                _southAmerican1969MeanForArgentinaBoliviaBrazilChileColombiaEcuadorGuyanaParaguayPeruTrinidadTobagoVenezuelaenezuela ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5044") },
                    Name = "South American 1969 \"MEAN FOR Argentina; Bolivia; Brazil; Chile; Colombia; Ecuador; Guyana; Paraguay; Peru; Trinidad & Tobago; Venezuela\"",
                    Description = "South American 1969 \"MEAN FOR Argentina; Bolivia; Brazil; Chile; Colombia; Ecuador; Guyana; Paraguay; Peru; Trinidad & Tobago; Venezuela\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -57 },
                    DeltaY = new () { ScalarValue = 1 },
                    DeltaZ = new () { ScalarValue = -41 }
                };
                return _southAmerican1969MeanForArgentinaBoliviaBrazilChileColombiaEcuadorGuyanaParaguayPeruTrinidadTobagoVenezuelaenezuela;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Paraguay
        private static GeodeticDatum? _southAmerican1969Paraguay = null;
        public static GeodeticDatum? SouthAmerican1969Paraguay
        {
            get
            {
                _southAmerican1969Paraguay ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5045") },
                    Name = "South American 1969 Paraguay",
                    Description = "South American 1969 Paraguay",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -61 },
                    DeltaY = new () { ScalarValue = 2 },
                    DeltaZ = new () { ScalarValue = -33 }
                };
                return _southAmerican1969Paraguay;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Peru
        private static GeodeticDatum? _southAmerican1969Peru = null;
        public static GeodeticDatum? SouthAmerican1969Peru
        {
            get
            {
                _southAmerican1969Peru ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5046") },
                    Name = "South American 1969 Peru",
                    Description = "South American 1969 Peru",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -58 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = -44 }
                };
                return _southAmerican1969Peru;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969TrinidadTobago
        private static GeodeticDatum? _southAmerican1969TrinidadTobago = null;
        public static GeodeticDatum? SouthAmerican1969TrinidadTobago
        {
            get
            {
                _southAmerican1969TrinidadTobago ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5047") },
                    Name = "South American 1969 Trinidad & Tobago",
                    Description = "South American 1969 Trinidad & Tobago",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -45 },
                    DeltaY = new () { ScalarValue = 12 },
                    DeltaZ = new () { ScalarValue = -33 }
                };
                return _southAmerican1969TrinidadTobago;
            }
        }
        #endregion

        #region geodetic datum SouthAmerican1969Venezuela
        private static GeodeticDatum? _southAmerican1969Venezuela = null;
        public static GeodeticDatum? SouthAmerican1969Venezuela
        {
            get
            {
                _southAmerican1969Venezuela ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5048") },
                    Name = "South American 1969 Venezuela",
                    Description = "South American 1969 Venezuela",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.SouthAmerican_1969,
                    DeltaX = new () { ScalarValue = -45 },
                    DeltaY = new () { ScalarValue = 8 },
                    DeltaZ = new () { ScalarValue = -33 }
                };
                return _southAmerican1969Venezuela;
            }
        }
        #endregion

        #region geodetic datum SouthAsiaSingapore
        private static GeodeticDatum? _southAsiaSingapore = null;
        public static GeodeticDatum? SouthAsiaSingapore
        {
            get
            {
                _southAsiaSingapore ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5049") },
                    Name = "South Asia Singapore",
                    Description = "South Asia Singapore",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.ModifiedFischer_1960,
                    DeltaX = new () { ScalarValue = 7 },
                    DeltaY = new () { ScalarValue = -10 },
                    DeltaZ = new () { ScalarValue = -26 }
                };
                return _southAsiaSingapore;
            }
        }
        #endregion

        #region geodetic datum TananariveObservatory1925Madagascar
        private static GeodeticDatum? _tananariveObservatory1925Madagascar = null;
        public static GeodeticDatum? TananariveObservatory1925Madagascar
        {
            get
            {
                _tananariveObservatory1925Madagascar ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5050") },
                    Name = "Tananarive Observatory 1925 Madagascar",
                    Description = "Tananarive Observatory 1925 Madagascar",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -189 },
                    DeltaY = new () { ScalarValue = -242 },
                    DeltaZ = new () { ScalarValue = -91 }
                };
                return _tananariveObservatory1925Madagascar;
            }
        }
        #endregion

        #region geodetic datum Timbalai1948BruneiEMalaysiaSabahSarawak
        private static GeodeticDatum? _timbalai1948BruneiEMalaysiaSabahSarawak = null;
        public static GeodeticDatum? Timbalai1948BruneiEMalaysiaSabahSarawak
        {
            get
            {
                _timbalai1948BruneiEMalaysiaSabahSarawak ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5051") },
                    Name = "Timbalai 1948 \"Brunei; E. Malaysia (Sabah Sarawak)\"",
                    Description = "Timbalai 1948 \"Brunei; E. Malaysia (Sabah Sarawak)\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Everest_Sabah_Sarawak,
                    DeltaX = new () { ScalarValue = -679 },
                    DeltaY = new () { ScalarValue = 669 },
                    DeltaZ = new () { ScalarValue = -48 }
                };
                return _timbalai1948BruneiEMalaysiaSabahSarawak;
            }
        }
        #endregion

        #region geodetic datum TokyoJapan
        private static GeodeticDatum? _tokyoJapan = null;
        public static GeodeticDatum? TokyoJapan
        {
            get
            {
                _tokyoJapan ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5052") },
                    Name = "Tokyo Japan",
                    Description = "Tokyo Japan",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = -148 },
                    DeltaY = new () { ScalarValue = 507 },
                    DeltaZ = new () { ScalarValue = 685 }
                };
                return _tokyoJapan;
            }
        }
        #endregion

        #region geodetic datum TokyoMeanForJapanSouthKoreaOkinawa
        private static GeodeticDatum? _tokyoMeanForJapanSouthKoreaOkinawa = null;
        public static GeodeticDatum? TokyoMeanForJapanSouthKoreaOkinawa
        {
            get
            {
                _tokyoMeanForJapanSouthKoreaOkinawa ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5053") },
                    Name = "Tokyo \"MEAN FOR Japan; South Korea; Okinawa\"",
                    Description = "Tokyo \"MEAN FOR Japan; South Korea; Okinawa\"",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = -148 },
                    DeltaY = new () { ScalarValue = 507 },
                    DeltaZ = new () { ScalarValue = 685 }
                };
                return _tokyoMeanForJapanSouthKoreaOkinawa;
            }
        }
        #endregion

        #region geodetic datum TokyoOkinawa
        private static GeodeticDatum? _tokyoOkinawa = null;
        public static GeodeticDatum? TokyoOkinawa
        {
            get
            {
                _tokyoOkinawa ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5054") },
                    Name = "Tokyo Okinawa",
                    Description = "Tokyo Okinawa",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = -158 },
                    DeltaY = new () { ScalarValue = 507 },
                    DeltaZ = new () { ScalarValue = 676 }
                };
                return _tokyoOkinawa;
            }
        }
        #endregion

        #region geodetic datum TokyoSouthKorea
        private static GeodeticDatum? _tokyoSouthKorea = null;
        public static GeodeticDatum? TokyoSouthKorea
        {
            get
            {
                _tokyoSouthKorea ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5055") },
                    Name = "Tokyo South Korea",
                    Description = "Tokyo South Korea",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Bessel_1841,
                    DeltaX = new () { ScalarValue = -147 },
                    DeltaY = new () { ScalarValue = 506 },
                    DeltaZ = new () { ScalarValue = 687 }
                };
                return _tokyoSouthKorea;
            }
        }
        #endregion

        #region geodetic datum TristanAstro1968TristanDaCunha
        private static GeodeticDatum? _tristanAstro1968TristanDaCunha = null;
        public static GeodeticDatum? TristanAstro1968TristanDaCunha
        {
            get
            {
                _tristanAstro1968TristanDaCunha ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5056") },
                    Name = "Tristan Astro 1968 Tristan da Cunha",
                    Description = "Tristan Astro 1968 Tristan da Cunha",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -632 },
                    DeltaY = new () { ScalarValue = 438 },
                    DeltaZ = new () { ScalarValue = -609 }
                };
                return _tristanAstro1968TristanDaCunha;
            }
        }
        #endregion

        #region geodetic datum VitiLevu1916FijiVitiLevuIsland
        private static GeodeticDatum? _vitiLevu1916FijiVitiLevuIsland = null;
        public static GeodeticDatum? VitiLevu1916FijiVitiLevuIsland
        {
            get
            {
                _vitiLevu1916FijiVitiLevuIsland ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5057") },
                    Name = "Viti Levu 1916 Fiji (Viti Levu Island)",
                    Description = "Viti Levu 1916 Fiji (Viti Levu Island)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = 51 },
                    DeltaY = new () { ScalarValue = 391 },
                    DeltaZ = new () { ScalarValue = -36 }
                };
                return _vitiLevu1916FijiVitiLevuIsland;
            }
        }
        #endregion

        #region geodetic datum Voirol1960Algeria
        private static GeodeticDatum? _voirol1960Algeria = null;
        public static GeodeticDatum? Voirol1960Algeria
        {
            get
            {
                _voirol1960Algeria ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5058") },
                    Name = "Voirol 1960 Algeria",
                    Description = "Voirol 1960 Algeria",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Clarke_1880,
                    DeltaX = new () { ScalarValue = -123 },
                    DeltaY = new () { ScalarValue = -206 },
                    DeltaZ = new () { ScalarValue = 219 }
                };
                return _voirol1960Algeria;
            }
        }
        #endregion

        #region geodetic datum WakeIslandAstro1952WakeAtoll
        private static GeodeticDatum? _wakeIslandAstro1952WakeAtoll = null;
        public static GeodeticDatum? WakeIslandAstro1952WakeAtoll
        {
            get
            {
                _wakeIslandAstro1952WakeAtoll ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5059") },
                    Name = "Wake Island Astro 1952 Wake Atoll",
                    Description = "Wake Island Astro 1952 Wake Atoll",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = 276 },
                    DeltaY = new () { ScalarValue = -57 },
                    DeltaZ = new () { ScalarValue = 149 }
                };
                return _wakeIslandAstro1952WakeAtoll;
            }
        }
        #endregion

        #region geodetic datum WakeEniwetok1960MarshallIslands
        private static GeodeticDatum? _wakeEniwetok1960MarshallIslands = null;
        public static GeodeticDatum? WakeEniwetok1960MarshallIslands
        {
            get
            {
                _wakeEniwetok1960MarshallIslands ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("d0f5b7a0-3e25-4d6f-a1c6-1c9f5a5a5060") },
                    Name = "Wake-Eniwetok 1960 Marshall Islands",
                    Description = "Wake-Eniwetok 1960 Marshall Islands",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.Hough,
                    DeltaX = new () { ScalarValue = 102 },
                    DeltaY = new () { ScalarValue = 52 },
                    DeltaZ = new () { ScalarValue = -38 }
                };
                return _wakeEniwetok1960MarshallIslands;
            }
        }
        #endregion

        #region geodetic datum WGS1972GlobalDefinition
        private static GeodeticDatum? _wgs1972GlobalDefinition = null;
        public static GeodeticDatum? WGS1972GlobalDefinition
        {
            get
            {
                _wgs1972GlobalDefinition ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("3c4ba2de-4f1f-4bbd-95e2-9f6dec39504c") },
                    Name = "WGS 1972 Global Definition",
                    Description = "World Geodetic System 1972 – global, zero-shift datum",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.WGS72,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 }
                };
                return _wgs1972GlobalDefinition;
            }
        }
        #endregion

        #region geodetic datum WGS1984GlobalDefinition
        private static GeodeticDatum? _wgs1984GlobalDefinition = null;
        public static GeodeticDatum? WGS1984GlobalDefinition
        {
            get
            {
                _wgs1984GlobalDefinition ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("97e2a2bd-e538-4a57-9e52-aa3128c1f7fa") },
                    Name = "WGS 1984 Global Definition",
                    Description = "World Geodetic System 1984 – global, zero-shift datum",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.WGS84,
                    DeltaX = new () { ScalarValue = 0 },
                    DeltaY = new () { ScalarValue = 0 },
                    DeltaZ = new () { ScalarValue = 0 }
                };
                return _wgs1984GlobalDefinition;
            }
        }
        #endregion

        #region geodetic datum YacareUruguay
        private static GeodeticDatum? _yacareUruguay = null;
        public static GeodeticDatum? YacareUruguay
        {
            get
            {
                _yacareUruguay ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("a6c8ddb0-9c69-441e-9fb8-b233e5b6dfe3") },
                    Name = "Yacare Uruguay",
                    Description = "Regional datum for Uruguay (International 1924 ellipsoid)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -155 },
                    DeltaY = new () { ScalarValue = 171 },
                    DeltaZ = new () { ScalarValue = 37 }
                };
                return _yacareUruguay;
            }
        }
        #endregion

        #region geodetic datum ZanderijSuriname
        private static GeodeticDatum? _zanderijSuriname = null;
        public static GeodeticDatum? ZanderijSuriname
        {
            get
            {
                _zanderijSuriname ??= new()
                {
                     MetaInfo = new MetaInfo() { HttpHostName = "https://dev.digiwells.no/", HttpHostBasePath = "GeodeticDatum/api/", HttpEndPoint = "GeodeticDatum/", ID = new Guid("bdb3e766-7d1c-43a1-8e7b-509a68e2db88") },
                    Name = "Zanderij Suriname",
                    Description = "Regional datum for Suriname (International 1924 ellipsoid)",
                    CreationDate = DateTimeOffset.UtcNow,
                    LastModificationDate = DateTimeOffset.UtcNow,
                    IsDefault = true,
                    Spheroid = Spheroid.International_1924,
                    DeltaX = new () { ScalarValue = -265 },
                    DeltaY = new () { ScalarValue = 120 },
                    DeltaZ = new () { ScalarValue = -358 }
                };
                return _zanderijSuriname;
            }
        }
        #endregion
    }
}
