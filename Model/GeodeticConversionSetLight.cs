using OSDC.DotnetLibraries.General.DataManagement;
using System;

namespace NORCE.Drilling.GeodeticDatum.Model
{
    /// <summary>
    /// Light weight version of a GeodeticConversionSet
    /// Used to avoid loading the complete GeodeticConversionSet (heavy weight data) each time we only need contextual info on the data
    /// Typically used for listing, sorting and filtering purposes
    /// </summary>
    public class GeodeticConversionSetLight
    {
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
        /// the name of the reference geodetic datum
        /// </summary>
        public string? GeodeticDatumName { get; set; }
        /// <summary>
        /// the description of the reference geodetic datum
        /// </summary>
        public string? GeodeticDatumDescription { get; set; }
        /// <summary>
        /// default constructor
        /// </summary>
        public GeodeticConversionSetLight() : base()
        {
        }
        /// <summary>
        /// base constructor
        /// </summary>
        public GeodeticConversionSetLight(MetaInfo? metaInfo, string? name, string? descr, DateTimeOffset? creationDate, DateTimeOffset? modifDate, string? geodeticDatumName, string? geodeticDatumDescr)
        {
            MetaInfo = metaInfo;
            Name = name;
            Description = descr;
            CreationDate = creationDate;
            LastModificationDate = modifDate;
            GeodeticDatumName = geodeticDatumName;
            GeodeticDatumDescription = geodeticDatumDescr;
        }
    }
}
