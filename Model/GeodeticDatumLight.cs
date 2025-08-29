using OSDC.DotnetLibraries.General.DataManagement;
using System;

namespace NORCE.Drilling.GeodeticDatum.Model
{
    /// <summary>
    /// Light weight version of a GeodeticDatum
    /// Used to avoid loading the complete GeodeticDatum (heavy weight data) each time we only need contextual info on the data
    /// Typically used for listing, sorting and filtering purposes
    /// </summary>
    public class GeodeticDatumLight
    {
        /// <summary>
        /// a MetaInfo for the GeodeticDatumLight
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
        /// default constructor required for parsing the data model as a json file
        /// </summary>
        public GeodeticDatumLight() : base()
        {
        }

        /// <summary>
        /// base constructor
        /// </summary>
        public GeodeticDatumLight(MetaInfo? metaInfo, string? name, string? descr, DateTimeOffset? creationDate, DateTimeOffset? modifDate, bool isDefault)
        {
            MetaInfo = metaInfo;
            Name = name;
            Description = descr;
            CreationDate = creationDate;
            LastModificationDate = modifDate;
            IsDefault = isDefault;
        }
    }
}
